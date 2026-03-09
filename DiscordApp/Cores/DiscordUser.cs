using DiscordApp.Cores;
using SocialNetworkingPlatform.Models;


namespace DiscordApp.Cores
{
    /// <summary>
    /// Discord хэрэглэгч.
    /// User классаас удамшина.
    /// </summary>
    public class DiscordUser : User
    {
        private readonly List<Guid> _friendIds = new();
        public string Discriminator { get; private set; }
        public string DiscordTag => $"{Username}#{Discriminator}";
        public IReadOnlyList<Guid> FriendIds => _friendIds;
        public int FriendCount => _friendIds.Count;

       
        public DiscordUser(string name, string username,
                           string email, string password,
                           DateTime dateOfBirth,
                           string discriminator = "0001")
            : base(name, username, email, password, dateOfBirth)
        {
            Discriminator = discriminator;
        }


        /// <summary>Найз нэмэх</summary>
        public void AddFriend(Guid friendId)
        {
            if (friendId == Id)
                throw new InvalidOperationException("Өөрөө өөрийгөө найз болгох боломжгүй.");

            if (!_friendIds.Contains(friendId))
                _friendIds.Add(friendId);
        }

        /// <summary>Найз хасах</summary>
        public void RemoveFriend(Guid friendId) =>
            _friendIds.Remove(friendId);

        /// <summary>Найз мөн эсэх</summary>
        public bool IsFriend(Guid friendId) =>
            _friendIds.Contains(friendId);

        public override string ToString() =>
            $"[DiscordUser] {DiscordTag} | {FriendCount}";
    }
}
