using System.ComponentModel.DataAnnotations;

namespace Lim_PreFinals_Quiz.Models;

public class Comment
{
    public int Id { get; set; }
    public int ProjectId { get; set; }

    [Required, StringLength(50, MinimumLength = 2)]
    public string Author { get; set; } = string.Empty;

    [Required, StringLength(500, MinimumLength = 2)]
    public string Content { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
