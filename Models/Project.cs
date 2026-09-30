namespace Lim_PreFinals_Quiz.Models;

public class Project
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string ShortDescription { get; set; } = string.Empty;
    public string LongDescription { get; set; } = string.Empty;
    public string[] TechStack { get; set; } = Array.Empty<string>();
    public string GitHubUrl { get; set; } = string.Empty;
    public string? LiveDemoUrl { get; set; }
    public string Thumbnail { get; set; } = string.Empty; // ~/images/xxx.svg
    public string Category { get; set; } = "Web App";
    public int Year { get; set; }
    public string[] Features { get; set; } = Array.Empty<string>();
}
