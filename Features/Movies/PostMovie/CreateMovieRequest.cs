using System.ComponentModel.DataAnnotations;

public class CreateMovieRequest
{
    [Required]
    [StringLength(100)]
    public string Title {get; set;} = "";

    [Range(1888,2100)]
    public int Year {get; set;}
}