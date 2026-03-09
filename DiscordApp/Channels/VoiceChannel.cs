using DiscordApp.Cores;


namespace DiscordApp.Channels;
public class VoiceChannel : Channel
{
    private readonly List<Guid> _activeUsers = new();
    public IReadOnlyList<Guid> ActiveUsers => _activeUsers;

    public int ActiveUserCount => _activeUsers.Count;

    public VoiceChannel(string name)
        : base(name) { }

    public void Join(Guid userId) => _activeUsers.Add(userId);
    public void Leave(Guid userId) => _activeUsers.Remove(userId);
}