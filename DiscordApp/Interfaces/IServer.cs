using DiscordApp.Channels;
using DiscordApp.Enums;

namespace DiscordApp.Interfaces
{
    /// <summary>
    /// Серверийн интерфэйс
    /// </summary>
    public interface IServer
    {
        Guid Id { get; }
        string Name { get; }
        Guid OwnerId { get; }
        DateTime CreatedAt { get; }
        IReadOnlyList<Guid> MemberIds { get; }
        IReadOnlyList<IChannel> Channels { get; }
        void AddMember(Guid userId, RoleType role);
        void RemoveMember(Guid userId);
        RoleType? GetRole(Guid userId);
        TextChannel AddTextChannel(string name);
        VoiceChannel AddVoiceChannel(string name);
    }
}
