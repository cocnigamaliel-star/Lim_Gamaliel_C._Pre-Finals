using Lim_PreFinals_Quiz.Data;
using Lim_PreFinals_Quiz.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Lim_PreFinals_Quiz.Controllers;

public class HomeController : Controller
{
    public IActionResult Index(string? q)
    {
        ViewData["Query"] = q;
        var projects = string.IsNullOrWhiteSpace(q)
            ? PortfolioData.Projects
            : PortfolioData.Projects.Where(p =>
                p.Title.Contains(q, StringComparison.OrdinalIgnoreCase) ||
                p.ShortDescription.Contains(q, StringComparison.OrdinalIgnoreCase) ||
                p.Category.Contains(q, StringComparison.OrdinalIgnoreCase) ||
                p.TechStack.Any(t => t.Contains(q, StringComparison.OrdinalIgnoreCase))).ToList();
        return View(projects);
    }

    [Route("Project/{id:int}")]
    public IActionResult Details(int id)
    {
        var project = PortfolioData.GetById(id);
        if (project is null) return NotFound();
        ViewData["Comments"] = PortfolioData.GetComments(id);
        return View(project);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Route("Project/{id:int}/comment")]
    public IActionResult AddComment(int id, [Bind("Author,Content")] Comment input)
    {
        var project = PortfolioData.GetById(id);
        if (project is null) return NotFound();

        // Security: only logged-in users may comment
        if (string.IsNullOrEmpty(HttpContext.Session.GetString("Username")))
        {
            TempData["Error"] = "Please log in to post a comment.";
            return RedirectToAction("Login", "Account", new { returnUrl = $"/Project/{id}" });
        }

        if (!ModelState.IsValid)
        {
            ViewData["Comments"] = PortfolioData.GetComments(id);
            TempData["Error"] = "Comment must be 2–500 chars with a valid name.";
            return View("Details", project);
        }

        // Server-side length guard (defense in depth) + trim
        var author = (input.Author ?? "").Trim();
        var content = (input.Content ?? "").Trim();
        if (author.Length < 2 || author.Length > 50 || content.Length < 2 || content.Length > 500)
        {
            TempData["Error"] = "Invalid comment length.";
            return Redirect($"/Project/{id}");
        }

        PortfolioData.AddComment(id, author, content);
        TempData["Success"] = "Comment posted!";
        return Redirect($"/Project/{id}#comments");
    }

    public IActionResult Privacy() => View();

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
