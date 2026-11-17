using Microsoft.AspNetCore.Mvc;

namespace LabWebApp.Controllers;

public sealed class HomeController : Controller
{
    public IActionResult Index() => View();

    [Route("login")]
    public IActionResult Login() => View();

    [Route("health")]
    public IActionResult Health() => Json(new { status = "healthy", service = "lab-web-app" });

    public IActionResult Error() => View();
}
