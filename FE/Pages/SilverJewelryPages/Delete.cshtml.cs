using System.Text.Json;
using BO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FE.Pages.SilverJewelryPages;
[Authorize(Policy = "2")]
public class DeleteModel : PageModel
{
    private readonly string _baseUrl = "https://localhost:7292/api/Silver";

    [BindProperty] public SilverJewelry SilverJewelry { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int id)
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

    public async Task<IActionResult> OnPostAsync(int id)
    {
        if (id == null) return NotFound();

        using var httpClient = new HttpClient();
        var response = await httpClient.DeleteAsync($"{_baseUrl}/{id}");
        if (!response.IsSuccessStatusCode) return NotFound();
        TempData["Message"] = "Delete Success!";
        return RedirectToPage("./Index");


    }
}