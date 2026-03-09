using SocialNetworkingPlatform.Models;
using DiscordApp.Enums;
using DiscordApp.Interfaces;

namespace DiscordApp.Channels
{
    /// <summary>
    /// Discord channel
    /// </summary>
    public abstract class Channel: IChannel
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public string Name { get; private set; }

        public Channel(string name)
        {
            Name = name;
        }
    }
}