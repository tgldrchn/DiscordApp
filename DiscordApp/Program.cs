using System;
using DiscordApp.Channels;
using DiscordApp.Cores;
using DiscordApp.Enums;
using SocialNetworkingPlatform.Chats;
using SocialNetworkingPlatform.Interfaces;
using SocialNetworkingPlatform.Repositories;
using SocialNetworkingPlatform.Services;

namespace DiscordApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IUserRepository userRepo = new UserRepository();
            IPostRepository postRepo = new PostRepository();
            IChatRepository chatRepo = new ChatRepository();

            var userService = new UserService(userRepo);
            var postService = new PostService(postRepo);
            var chatService = new ChatService(chatRepo);

            var platform = new DiscordPlatform("Discord", userRepo, postRepo);
            Console.WriteLine($"{platform}");
            Console.WriteLine("\nSign up users");

            var user1 = (DiscordUser)platform.SignUp("Tuguldur", "tguldurr", "tuguldur@gmail.com","pass123", new DateTime(2000, 1, 1));
            var user2 = (DiscordUser)platform.SignUp( "Bold", "bold456", "bold@gmail.com","pass456", new DateTime(1999, 5, 15));


            Console.WriteLine($"{user1}");
            Console.WriteLine($"{user2}");

            Console.WriteLine("\nLogin");

            var loggedIn = userService.Login("tuguldur@gmail.com", "pass123");
            Console.WriteLine(loggedIn != null ? $"Successul: {loggedIn}" : "Unsuccessful");

            Console.WriteLine("\nAdd a friend");

            user1.AddFriend(user2.Id);
            user2.AddFriend(user1.Id);
            Console.WriteLine($"{user1.Username} friend: {user1.FriendCount}");

           
            Console.WriteLine("\nServer");
            var server = new Server("Mongolian Devs", user1.Id);
            server.AddMember(user2.Id, RoleType.Member);
            Console.WriteLine($"{server}");
            Console.WriteLine($"{user1.Username}: {server.GetRole(user1.Id)}");
            Console.WriteLine($"{user2.Username}: {server.GetRole(user2.Id)}");

            
            Console.WriteLine("\nChannel");

            var textChannel = server.AddTextChannel("general");
            var voiceChannel = server.AddVoiceChannel("voice");
            Console.WriteLine($"{textChannel.Name}");
            Console.WriteLine($"{voiceChannel.Name}");

            
            Console.WriteLine("\nMessage");

            var msg1 = textChannel.SendMessage(user1.Id, "Hi");
            var msg2 = textChannel.SendMessage(user2.Id, "Hi, How are you");
            Console.WriteLine($"{user1.Username}: {msg1.Content}");
            Console.WriteLine($"{user2.Username}: {msg2.Content}");

 
            Console.WriteLine("\nReaction");

            msg1.AddReaction(user2.Id, "👍");
            msg1.AddReaction(user1.Id, "❤️");
            Console.WriteLine($"Reactions: {msg1.Reactions.Count}");

           
            Console.WriteLine("\nChange a role");

            server.ChangeMemberRole(user2.Id, RoleType.Moderator);
            Console.WriteLine($"{user2.Username} role: {server.GetRole(user2.Id)}");

            Console.WriteLine("\nVoice Channel");

            voiceChannel.Join(user1.Id);
            voiceChannel.Join(user2.Id);
            Console.WriteLine($"{voiceChannel.Name}: {voiceChannel.ActiveUserCount} users");

            
            Console.WriteLine("\nDM");

            var dm = chatService.CreateSingleChat(user1.Id, user2.Id);
            chatService.SendMessage(dm.Id, user1.Id, "Hi Bob!");
            chatService.SendMessage(dm.Id, user2.Id, "Bye Alice!");

            foreach (var msg in dm.Messages)
            {
                var sender = userRepo.GetById(msg.SenderId);
                Console.WriteLine($"{sender?.Username}: {msg.Content}");
            }

            Console.WriteLine("\nGroup Chat");

            var group = (GroupChat)chatService.CreateGroupChat(user1.Id, "Dev Friends");
            group.AddMember(user2.Id);
            chatService.SendMessage(group.Id, user1.Id, "Hello Everybody! 🎉");
            Console.WriteLine($"{group}");

            
            Console.WriteLine("\n══════════════════════════");
            Console.WriteLine($"Users: {userRepo.GetAll().Count}");
            Console.WriteLine($"Serversr: {server.Name} | 👥{server.MemberIds.Count}");
            Console.WriteLine($"DM: {dm.Messages.Count}");
            Console.WriteLine($"Voice: {voiceChannel.ActiveUserCount} users");
            Console.WriteLine("══════════════════════════");
        }
    }
}