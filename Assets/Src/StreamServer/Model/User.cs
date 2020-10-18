namespace StreamServer.Model
{
    public class User
    {
        public readonly string UserId;
        public readonly PacketContainer Data = new PacketContainer();

        public User(string userId)
        {
            UserId = userId;
        }
    }
}
