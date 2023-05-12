using System.ComponentModel.DataAnnotations;

namespace TestWebApi.Requests.Authors
{
    public class UpdateAuthorRequest
    {
        [StringLength(256)]
        [Required(AllowEmptyStrings = false)]
        public string Nome { get; set; }
        public int Id { get; set; }
    }
}