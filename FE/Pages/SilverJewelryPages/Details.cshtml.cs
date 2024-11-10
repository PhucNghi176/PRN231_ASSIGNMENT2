using System.Text.Json;
using BO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FE.Pages.SilverJewelryPages;
public class DetailsModel : PageModel
{
    private readonly string _baseUrl = "https://localhost:7292/api/Silver";

    public SilverJewelry SilverJewelry { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(string id)
    {
        if (id == null) return NotFound();

        using var httpClient = new HttpClient();
        var response = await httpClient.GetAsync($"{_baseUrl}/{id}");
        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            SilverJewelry = JsonSerializer.Deserialize<SilverJewelry>(content, options);
        }

        if (SilverJewelry == null) return NotFound();
        return Page();
    }
}