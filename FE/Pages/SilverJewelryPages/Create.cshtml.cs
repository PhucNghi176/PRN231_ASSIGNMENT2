using System.Text;
using System.Text.Json;
using BO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FE.Pages.SilverJewelryPages;

public class CreateModel : PageModel
{
    private readonly string _baseUrl = "https://localhost:7292/api/";
    [BindProperty] public IList<Category> Categories { get; set; } = default!;
    [BindProperty] public SilverJewelry SilverJewelry { get; set; } = default!;

    public async Task<IActionResult> OnGet()
    {
        using var httpClient = new HttpClient();
        var response2 = await httpClient.GetAsync($"{_baseUrl}Category");
        if (!response2.IsSuccessStatusCode) return Page();
        {
            var content = await response2.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            Categories = JsonSerializer.Deserialize<List<Category>>(content, options);
        }

        return Page();
    }

    // For more information, see https://aka.ms/RazorPagesCRUD.
    public async Task<IActionResult> OnPostAsync()
    {
        using var httpClient = new HttpClient();
        var content = new StringContent(JsonSerializer.Serialize(SilverJewelry), Encoding.UTF8, "application/json");
        var response = await httpClient.PostAsync($"{_baseUrl}Silver", content);
        TempData["Message"] = "Create success!";
        return response.IsSuccessStatusCode ? RedirectToPage("/SilverJewelryPages/Index") : NotFound();
        
        
    }
}