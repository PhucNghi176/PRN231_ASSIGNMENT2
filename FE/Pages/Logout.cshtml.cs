using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FE.Pages;
public class Logout : PageModel
{
    public void OnGet()
    {
        foreach (var cookie in Request.Cookies.Keys) Response.Cookies.Delete(cookie);
        Response.Redirect("/");
    }
}