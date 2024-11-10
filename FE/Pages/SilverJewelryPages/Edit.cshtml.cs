using System.Text;
using System.Text.Json;
using BO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;

namespace FE.Pages.SilverJewelryPages;
[Authorize(Policy = "2")]
public class EditModel : PageModel
{
    private readonly string _baseUrl = "https://localhost:7292/api/";

    [BindProperty] public SilverJewelry SilverJewelry { get; set; } = default!;

    [BindProperty] public IList<Category> Categories { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int id)
    {
        if (id == null) return NotFound();

        using var httpClient = new HttpClient();
        var response = await httpClient.GetAsync($"{_baseUrl}Silver/{id}");
        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            SilverJewelry = JsonSerializer.Deserialize<SilverJewelry>(content, options);
        }

        if (SilverJewelry == null) return NotFound();

        var response2 = await httpClient.GetAsync($"{_baseUrl}Category");
        if (!response2.IsSuccessStatusCode) return Page();
        {
            var content = await response2.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            Categories = JsonSerializer.Deserialize<List<Category>>(content, options);
        }

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (SilverJewelry.Price < 0 || SilverJewelry.MetalWeight < 0 ||
            SilverJewelry.SilverJewelryName.IsNullOrEmpty() || SilverJewelry.SilverJewelryDescription.IsNullOrEmpty())
        {
            ModelState.AddModelError("SilverJewelry.Price", "Price must be greater than 0");
            ModelState.AddModelError("SilverJewelry.MetalWeight", "MetalWeight must be greater than 0");
            ModelState.AddModelError("SilverJewelry.Quantity", "Quantity must be greater than 0");
            return Page();
        }


        using var httpClient = new HttpClient();
        var response = await httpClient.PutAsync($"{_baseUrl}Silver/{SilverJewelry.SilverJewelryId}",
            new StringContent(JsonSerializer.Serialize(SilverJewelry), Encoding.UTF8, "application/json"));
        TempData["Message"] = "Edit success!";
        return response.IsSuccessStatusCode ? RedirectToPage("./Index") : NotFound();
    }
}