using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using TestWebApi.Entities;

namespace TestWebApi.Requests.Books;

public class CreateBookRequest : IValidatableObject
{
    public int AuthorId { get; set; }
    [StringLength(256,MinimumLength =10)]
    public string Title { get; set; }
    public Genre Genre { get; set; }
    public decimal Price { get; set; }
    public DateTime PublishDate { get; set; }
    public string Description { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if(PublishDate < new DateTime(2000, 1, 1))
        {
            yield return new ValidationResult("La data di pubblicazione non può essere inferiore al 01/01/2000", new[] {nameof(PublishDate)});
        }
        if(PublishDate > DateTime.Now.Date)
        {
            yield return new ValidationResult($"Non può essere pubblicato un libro con data maggiore di oggi {DateTime.Now.Date}", new[] { nameof(PublishDate) });
        }
    }
}
