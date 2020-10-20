namespace StreamServer.Model
{
    public class User
    {
        public readonly string UserId;
        public volatile bool IsConnected;
        public volatile MinimumAvatarPacket CurrentPacket;
        public volatile DateTimeBox DateTimeBox;

        public User(string userId)
        {
            UserId = userId;
        }
        
        public User(User instance)
        {
            UserId = instance.UserId;
            IsConnected = instance.IsConnected;
            DateTimeBox = instance.DateTimeBox;
        }
    }
}
