using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace TestWebApi.Requests.Authors
{
    public class CreateAuthorRequest
    {
        [StringLength(256)]
        [Required(AllowEmptyStrings = false)]
        public string Nome { get; set; }
    }
}
