using System.Text.Json;
using BO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FE.Pages.BranchAccountPages;
public class DetailsModel : PageModel
{
    private const string BaseUrl = "https://localhost:7292/api/Branch";


    public BranchAccount BranchAccount { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null) return NotFound();

        using var client = new HttpClient();
        var response = await client.GetAsync($"{BaseUrl}/{id}");
        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            BranchAccount = JsonSerializer.Deserialize<BranchAccount>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }
        else
        {
            return NotFound();
        }
        return Page();
    }
}