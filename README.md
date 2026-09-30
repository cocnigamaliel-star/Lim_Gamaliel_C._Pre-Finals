# RED//PORTFOLIO — Lim, Gamaliel (IT Elective 2 · 31E1)

Modern **red** ASP.NET Core MVC portfolio showcasing the author's GitHub projects.

## Featured Projects

| # | Project | GitHub |
|---|---------|--------|
| 1 | IT Elective 2 — A1 Prefinals Portfolio (MVC) | https://github.com/cocnigamaliel-star/IT_ELECTIVE_2_A1_PREFINALS_LIM_GAMALIEL.git |
| 2 | 31E1 Prefinal Exam — ExamApp | https://github.com/cocnigamaliel-star/cocnigamaliel-star-IT_ELECTIVE_2_31E1_PREFINAL_EXAM_Lim_Gamaliel.git |

Profile: https://github.com/cocnigamaliel-star

## 🔐 Hardcoded Login (required by quiz)

| Field | Value |
|-------|-------|
| Username | `admin` |
| Password | `Portfolio123!` |
| Login URL | `/Account/Login` |

> Only logged-in users can post comments. Session cookie: `.Portfolio.Session` (60-min idle timeout).

## Features (quiz requirements)

- [x] GitHub project links + short descriptions + thumbnail images (`wwwroot/images/`)
- [x] Hardcoded login (documented here)
- [x] Table of contents organising projects (`#toc` sidebar on home page)
- [x] Detail page for each project (`/Project/{id}`)
- [x] Comment section for each project (login-gated, per-project in-memory store)
- [x] Modern red UI (dark + ember-red theme, responsive, Bootstrap Icons)

## Run

```bash
dotnet restore
dotnet run
```

Then open the printed `http://localhost:xxxx` URL.

## Security notes

- `ValidateAntiForgeryToken` on login, logout and comment posts
- Constant-time credential comparison + generic error message + small brute-force delay
- Session `HttpOnly` cookie; comment author is taken from the session, lengths validated server-side (2–50 name, 2–500 content)
- Razor auto-encodes comment output (XSS-safe); `ReturnUrl` restricted to local URLs
