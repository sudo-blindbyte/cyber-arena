using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using cyber_arena.Models;

namespace cyber_arena.Controllers;

public class HomeController : Controller
{
    /// <summary>
    /// Redirects root URL to the login page.
    /// TODO: When ASP.NET Core Identity is connected, redirect to Dashboard if already authenticated.
    /// Example: if (User.Identity?.IsAuthenticated == true) return RedirectToAction("Index", "Dashboard");
    /// </summary>
    public IActionResult Index()
    {
        return RedirectToAction("Login", "Account");
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
