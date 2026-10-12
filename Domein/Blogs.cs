public class Blog
{
    public int Id { get; set; }

    public string Title { get; set; } = "";

    public string Beschrijving { get; set; } = "";

    public DateOnly Datum { get; set; }

    public string ContentLink { get; set; } = "";
}