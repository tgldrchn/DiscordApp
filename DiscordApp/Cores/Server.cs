using DiscordApp.Channels;
using DiscordApp.Enums;
using DiscordApp.Interfaces;

namespace DiscordApp.Cores
{
    /// <summary>
    /// Discord сервер
    /// </summary>
    public class Server : IServer
    {
        private readonly Dictionary<Guid, RoleType> _memberRoles = new();
        private readonly List<IChannel> _channels = new();
        public Guid Id { get; private set; } = Guid.NewGuid();
        public string Name { get; set; }
        public Guid OwnerId { get; private set; }
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
        public IReadOnlyDictionary<Guid, RoleType> MemberRoles => _memberRoles;
        public IReadOnlyList<Guid> MemberIds => _memberRoles.Keys.ToList();
        public IReadOnlyList<IChannel> Channels => _channels;

    
        public Server(string name, Guid ownerId)
        {
            Name = name;
            OwnerId = ownerId;
            _memberRoles[ownerId] = RoleType.Owner;
        }

        public void AddMember(Guid userId, RoleType role = RoleType.Member)
        {
            if (!_memberRoles.ContainsKey(userId))
                _memberRoles[userId] = role;
        }

        /// <summary>Гишүүн хасах</summary>
        public void RemoveMember(Guid userId) =>
            _memberRoles.Remove(userId);

        /// <summary>Гишүүний Role авах</summary>
        public RoleType? GetRole(Guid userId) =>
            _memberRoles.TryGetValue(userId, out var role) ? role : null;

        /// <summary>Role солих</summary>
        public void ChangeMemberRole(Guid userId, RoleType newRole)
        {
            if (_memberRoles.ContainsKey(userId))
                _memberRoles[userId] = newRole;
        }

        /// <summary>TextChannel нэмэх</summary>
        public TextChannel AddTextChannel(string name)
        {
            var channel = new TextChannel(name);
            _channels.Add(channel);
            return channel;
        }

        /// <summary>VoiceChannel нэмэх</summary>
        public VoiceChannel AddVoiceChannel(string name)
        {
            var channel = new VoiceChannel(name);
            _channels.Add(channel);
            return channel;
        }

        /// <summary>Гишүүн мөн эсэх</summary>
        public bool IsMember(Guid userId) =>
            _memberRoles.ContainsKey(userId);

        public override string ToString() =>
            $"[Server] {Name} | 👥{_memberRoles.Count} | #{_channels.Count}";
    }
}