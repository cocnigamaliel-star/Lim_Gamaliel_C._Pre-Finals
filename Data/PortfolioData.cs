using Lim_PreFinals_Quiz.Models;

namespace Lim_PreFinals_Quiz.Data;

/// <summary>
/// In-memory portfolio store featuring the owner's real GitHub projects.
/// Thumbnails live in wwwroot/images/.
/// </summary>
public static class PortfolioData
{
    public static readonly List<Project> Projects = new()
    {
        new Project
        {
            Id = 1,
            Title = "IT Elective 2 — A1 Prefinals Portfolio (MVC)",
            Slug = "it-elective-2-a1-prefinals",
            ShortDescription = "Modern red MVC portfolio with hardcoded login, table of contents, project detail pages and per-project comments.",
            LongDescription = "This repository (IT_ELECTIVE_2_A1_PREFINALS_LIM_GAMALIEL) is the prefinals activity itself: an ASP.NET Core MVC portfolio application. It presents the author's GitHub projects with thumbnails and descriptions, organises them with a table of contents, provides a dedicated detail page per project, and adds a comment section on every project. Access to commenting is gated behind a hardcoded session login documented in the README.",
            TechStack = new[] { "ASP.NET Core MVC", "C#", "Razor", "Bootstrap", "Session Auth" },
            GitHubUrl = "https://github.com/cocnigamaliel-star/IT_ELECTIVE_2_A1_PREFINALS_LIM_GAMALIEL.git",
            LiveDemoUrl = null,
            Thumbnail = "~/images/project-portfolio.svg",
            Category = "Web App · Portfolio",
            Year = 2026,
            Features = new[]
            {
                "Table of contents organising all projects",
                "Detail page for each project with GitHub link + thumbnail",
                "Comment section on every project (login required)",
                "Hardcoded login with session + anti-forgery protection",
                "Modern red responsive UI"
            }
        },
        new Project
        {
            Id = 2,
            Title = "31E1 Prefinal Exam — ExamApp",
            Slug = "31e1-prefinal-exam-examapp",
            ShortDescription = "Prefinal exam build (ExamApp) for IT Elective 2, Section 31E1 — Lim, Gamaliel.",
            LongDescription = "This repository (cocnigamaliel-star-IT_ELECTIVE_2_31E1_PREFINAL_EXAM_Lim_Gamaliel) contains ExamApp, the prefinal examination project for IT Elective 2, Section 31E1 by Lim, Gamaliel. It demonstrates exam requirements implemented as a working application: structured code, versioned commits, and a documented README. This portfolio links out to the full source and summarises the build below.",
            TechStack = new[] { "ASP.NET Core", "C#", "MVC", "Git & GitHub" },
            GitHubUrl = "https://github.com/cocnigamaliel-star/cocnigamaliel-star-IT_ELECTIVE_2_31E1_PREFINAL_EXAM_Lim_Gamaliel.git",
            LiveDemoUrl = null,
            Thumbnail = "~/images/project-examapp.svg",
            Category = "Exam · Coursework",
            Year = 2026,
            Features = new[]
            {
                "ExamApp working application",
                "Clean repo structure with README",
                "Versioned commit history (23 commits)",
                "Coursework for IT Elective 2 · 31E1",
                "Full source available on GitHub"
            }
        },
    };

    // In-memory comments seeded with examples. New comments append here.
    private static readonly List<Comment> _comments = new()
    {
        new Comment { Id = 1, ProjectId = 1, Author = "Gamaliel", Content = "Welcome to my portfolio! Log in and leave a comment on any project.", CreatedAt = DateTime.UtcNow.AddDays(-2) },
        new Comment { Id = 2, ProjectId = 2, Author = "Classmate", Content = "Nice ExamApp build — clean structure!", CreatedAt = DateTime.UtcNow.AddDays(-1) },
    };
    private static int _nextCommentId = 3;
    private static readonly object _lock = new();

    public static Project? GetById(int id) => Projects.FirstOrDefault(p => p.Id == id);

    public static List<Comment> GetComments(int projectId)
    {
        lock (_lock)
        {
            return _comments.Where(c => c.ProjectId == projectId)
                            .OrderByDescending(c => c.CreatedAt).ToList();
        }
    }

    public static Comment AddComment(int projectId, string author, string content)
    {
        lock (_lock)
        {
            var c = new Comment
            {
                Id = _nextCommentId++,
                ProjectId = projectId,
                Author = author.Trim(),
                Content = content.Trim(),
                CreatedAt = DateTime.UtcNow
            };
            _comments.Add(c);
            return c;
        }
    }
}
