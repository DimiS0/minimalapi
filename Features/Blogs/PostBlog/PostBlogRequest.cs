using System.ComponentModel.DataAnnotations;

public class PostBlogRequest
{
    [Required]
    [StringLength(30)]
     public string Title { get; set; } = "";
     [Required]
     [StringLength(250)]
    public string Beschrijving { get; set; } = "";

    public DateOnly Datum { get; set; }

    [Required]
    [Url]
    public string ContentLink { get; set; } = "";
}