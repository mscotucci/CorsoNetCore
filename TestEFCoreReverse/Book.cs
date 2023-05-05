using System;
using System.Collections.Generic;

namespace TestEFCoreReverse;

public class Book
{
    public int Id { get; set; }

    public int AuthorId { get; set; }

    public string Title { get; set; } = null!;

    public string? Genre { get; set; }

    public decimal Price { get; set; }

    public DateTime? PublishDate { get; set; }

    public string? Description { get; set; }

    public virtual Author Author { get; set; } = null!;
}
