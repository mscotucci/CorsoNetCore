using System.ComponentModel.DataAnnotations;

namespace TestWebApi.Requests.Users
{
    public class CreateUserRequest
    {
        [Required]
        [MaxLength(256)]
        public string Username { get; set; }
        public string[]? Roles { get; set; } 
    }
}
