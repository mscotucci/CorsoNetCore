using System.ComponentModel.DataAnnotations;

namespace TestWebApi.Requests.Roles
{
    public class CreateRoleRequest
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
    }
}
