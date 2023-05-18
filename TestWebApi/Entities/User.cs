namespace TestWebApi.Entities
{
    public class User
    {
        public Guid Id { get; set; }

        public string Username { get; set; }
        public ICollection<Role> Roles { get; set; } = new List<Role>();
    }
}
