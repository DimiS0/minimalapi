using System.ComponentModel.DataAnnotations;

public class CreateProjectRequest
{
    [Required]
    [StringLength(100)]
    public string Title {get; set;} = "";

    [Required]
    [StringLength(250)]
   public string Beschrijving { get; set; } = "";

    [Required]
    [StringLength(500)]
       public string Afbeedling { get; set; } = "";

    [Required]
    [StringLength(250)]
       public string Alt { get; set; } = "";

    [Required]
    [Url]
    [StringLength(500)]
       public string ProjectLink { get; set; } = "";

    [Required]
    [StringLength(30)]
    public string Tag { get; set; }

       public DateTime Datum { get; set; }
}