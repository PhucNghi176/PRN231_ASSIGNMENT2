using System.Text;
using System.Text.Json;
using BO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FE.Pages.CategoryPages;
[Authorize(Policy = "2")]
public class CreateModel : PageModel
{
    private const string baseUrl = "https://localhost:7292/api/Category";

    [BindProperty] public Category Category { get; set; } = default!;

    public IActionResult OnGet()
    {
        return Page();
    }

    // For more information, see https://aka.ms/RazorPagesCRUD.
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid) return Page();

        using var httpClient = new HttpClient();
        var content = new StringContent(JsonSerializer.Serialize(Category), Encoding.UTF8, "application/json");
        using var response = await httpClient.PostAsync(baseUrl, content);
        if (!response.IsSuccessStatusCode) return Page();
        return RedirectToPage("./Index");
    }
}