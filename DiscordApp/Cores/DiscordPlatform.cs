using System;
using SocialNetworkingPlatform.Interfaces;
using SocialNetworkingPlatform.Models;
using SocialNetworkingPlatform.Repositories;

namespace DiscordApp.Cores
{
    /// <summary>
    /// Discord платформ — Platform-аас удамшсан
    /// </summary>
    public class DiscordPlatform : Platform
    {
        public DiscordPlatform(string name, IUserRepository userRepo, IPostRepository postRepo)
            : base(name, userRepo, postRepo)
        {
        }

        /// <summary>
        /// Discord хэрэглэгч бүртгэх — base-ийг override хийнэ
        /// </summary>
        public override IUser SignUp(string name, string username,
                                     string email, string password,
                                     DateTime dateOfBirth)
        {
            if (UserRepository.GetByEmail(email) != null)
                throw new Exception($"{email} has already registered.");

            var user = new DiscordUser(name, username, email, password, dateOfBirth);
            UserRepository.Add(user);
            return user;
        }

        /// <summary>
        /// Discord пост үүсгэх
        /// </summary>
        public override IPost CreatePost(Guid authorId, string content)
        {
            var post = new Post(authorId, content);
            PostRepository.Add(post);
            return post;
        }
    }
}