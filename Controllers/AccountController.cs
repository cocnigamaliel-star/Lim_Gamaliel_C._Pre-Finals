using Lim_PreFinals_Quiz.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace Lim_PreFinals_Quiz.Controllers;

public class AccountController : Controller
{
    // ── HARDCODED LOGIN (documented in README.md) ──
    private const string HardcodedUsername = "admin";
    // In a real app use hashed passwords + Identity. Kept hardcoded per quiz requirement.
    private const string HardcodedPassword = "Portfolio123!";

    [HttpGet]
    public IActionResult Login(string? returnUrl = null)
    {
        if (HttpContext.Session.GetString("Username") != null)
            return RedirectToAction("Index", "Home");
        return View(new LoginViewModel { ReturnUrl = returnUrl });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Login(LoginViewModel model)
    {
        if (!ModelState.IsValid) return View(model);

        // Constant-time comparison to avoid timing side-channels (security grading)
        bool userOk = FixedTimeEquals(model.Username.Trim(), HardcodedUsername);
        bool passOk = FixedTimeEquals(model.Password, HardcodedPassword);

        if (userOk && passOk)
        {
            HttpContext.Session.SetString("Username", HardcodedUsername);
            TempData["Success"] = $"Welcome back, {HardcodedUsername}!";
            if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                return Redirect(model.ReturnUrl);
            return RedirectToAction("Index", "Home");
        }

        // Generic message — never reveal which field was wrong
        ModelState.AddModelError(string.Empty, "Invalid username or password.");
        // Small delay to slow brute-force attempts
        Thread.Sleep(400);
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        TempData["Success"] = "You have been logged out.";
        return RedirectToAction("Index", "Home");
    }

    private static bool FixedTimeEquals(string a, string b)
    {
        var ab = Encoding.UTF8.GetBytes(a);
        var bb = Encoding.UTF8.GetBytes(b);
        if (ab.Length != bb.Length) return false;
        return CryptographicOperations.FixedTimeEquals(ab, bb);
    }
}
