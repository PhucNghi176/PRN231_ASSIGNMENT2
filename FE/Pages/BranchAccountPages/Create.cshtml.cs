using System.Text;
using System.Text.Json;
using BO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FE.Pages.BranchAccountPages;
public class CreateModel : PageModel
{
    private const string BaesUrl = "https://localhost:7292/api/Branch";

    [BindProperty] public BranchAccount BranchAccount { get; set; } = default!;

    public IActionResult OnGet()
    {
        return Page();
    }

    // For more information, see https://aka.ms/RazorPagesCRUD.
    public async Task<IActionResult> OnPostAsync()
    {
        using var httpClient = new HttpClient();
        var response = await httpClient.PostAsync($"{BaesUrl}",
            new StringContent(JsonSerializer.Serialize(BranchAccount), Encoding.UTF8, "application/json"));
        if (!response.IsSuccessStatusCode) return Page();


        return RedirectToPage("./Index");
    }
}