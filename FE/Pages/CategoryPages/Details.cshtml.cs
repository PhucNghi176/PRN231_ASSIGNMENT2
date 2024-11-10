using System.Text.Json;
using BO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FE.Pages.CategoryPages;
[Authorize(Policy = "2")]
public class DetailsModel : PageModel
{
    private const string baseUrl = "https://localhost:7292/api/Category";

    public Category Category { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null) return NotFound();

        using var httpClient = new HttpClient();
        using var response = await httpClient.GetAsync($"{baseUrl}/{id}");
        if (!response.IsSuccessStatusCode) return NotFound();
        var content = await response.Content.ReadAsStringAsync();
        var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
        Category = JsonSerializer.Deserialize<Category>(content, options);

        return Page();
    }
}