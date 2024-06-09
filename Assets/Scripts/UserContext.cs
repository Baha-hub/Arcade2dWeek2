public class UserContext 
{
    public class UserMain
    {
        public UserInformations userInformations { get; set; }
        public Ship ship { get; set; }
        public Bullet bullet  {get; set; }
    }

    public class UserInformations
    {
        public string name { get; set; }
        public string connectionId { get; set; }
        public string skor {  get; set; }
        public string roomName { get; set; }
        public bool isHost { get; set; }
        public bool isReady { get; set; }
    }
    public class Ship
    {
        public int life { get; set; }
        public int skor { get; set; }
        public float locx { get; set; }
        public float locy { get; set; }
    }
    public class Bullet
    {
        public int bulletSpeed { get; set; }
    }
}
