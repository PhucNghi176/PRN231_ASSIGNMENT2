using System.Text;
using System.Text.Json;
using BO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FE.Pages.BranchAccountPages;
public class EditModel : PageModel
{
    private const string BaseUrl = "https://localhost:7292/api/Branch";

    [BindProperty] public BranchAccount BranchAccount { get; set; } = default!;


    public int Role { get; set; }

    [BindProperty]
    public List<SelectListItem> Roles { get; set; } =
    [
        new SelectListItem { Value = "1", Text = "Admin" },
        new SelectListItem { Value = "2", Text = "Staff" },
        new SelectListItem { Value = "3", Text = "Manager" }
    ];


    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null) return NotFound();
        using var httpClient = new HttpClient();
        var response = await httpClient.GetAsync($"{BaseUrl}/{id}");
        if (!response.IsSuccessStatusCode) return Page();
        var content = await response.Content.ReadAsStringAsync();
        BranchAccount = JsonSerializer.Deserialize<BranchAccount>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });


        return Page();
    }

    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more information, see https://aka.ms/RazorPagesCRUD.
    public async Task<IActionResult> OnPostAsync()
    {
        using var httpClient = new HttpClient();
        var response = await httpClient.PutAsync($"{BaseUrl}",
            new StringContent(JsonSerializer.Serialize(BranchAccount), Encoding.UTF8, "application/json"));
        ;
        if (!response.IsSuccessStatusCode) return Page();
        return RedirectToPage("./Index");
    }
}