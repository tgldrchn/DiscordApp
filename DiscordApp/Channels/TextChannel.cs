using System;
using System.Collections.Generic;
using SocialNetworkingPlatform.Interfaces;
using SocialNetworkingPlatform.Models;

namespace DiscordApp.Channels
{
    /// <summary>
    /// Текст суваг — мессеж илгээх боломжтой
    /// </summary>
    public class TextChannel : Channel, IMessageable
    {
        private readonly List<IMessage> _messages = new();
        private readonly List<IPost> _posts = new();
        public IReadOnlyList<IMessage> Messages => _messages;
        public IReadOnlyList<IPost> Posts => _posts;

        public TextChannel(string name)
            : base(name) { }


        /// <summary>Мессеж илгээх</summary>
        public IMessage SendMessage(Guid senderId, string content)
        {
            var message = new Message(senderId, Id, content);
            _messages.Add(message);
            return message;
        }

        // Post нийтлэх — урт агуулга
        public IPost Publish(Guid authorId, string content)
        {
            var post = new Post(authorId, content);
            _posts.Add(post);
            return post;
        }
    }
}