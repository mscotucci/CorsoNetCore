using System.ComponentModel.DataAnnotations;

namespace TestWebApi.Requests.Books;

public class CreateBookRequest
{
    public int AuthorId { get; set; }
    [StringLength(256,MinimumLength =10)]
    public string Title { get; set; }
    public string Genre { get; set; }
    public decimal Price { get; set; }
    public DateTime PublishDate { get; set; }
    public string Description { get; set; }
}
