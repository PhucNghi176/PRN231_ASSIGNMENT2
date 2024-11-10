using System.Text;
using System.Text.Json;
using BO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FE.Pages.CategoryPages;
[Authorize(Policy = "2")]
public class EditModel : PageModel
{
    private const string baseUrl = "https://localhost:7292/api/Category";

    [BindProperty] public Category Category { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(string id)
    {
        if (id == null) return NotFound();

        using var httpClient = new HttpClient();
        var response = await httpClient.GetAsync($"{baseUrl}/{id}");
        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            Category = JsonSerializer.Deserialize<Category>(content, options);
        }

        if (Category == null) return NotFound();
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        using var httpClient = new HttpClient();
        var response = await httpClient.PutAsync($"{baseUrl}",
            new StringContent(JsonSerializer.Serialize(Category), Encoding.UTF8, "application/json"));
        return response.IsSuccessStatusCode ? RedirectToPage("./Index") : NotFound();
    }
}