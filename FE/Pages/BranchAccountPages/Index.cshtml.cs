using System.Text.Json;
using BO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace FE.Pages.BranchAccountPages;
[Authorize(Roles = "1")]
public class IndexModel : PageModel
{
    private const string BaseUrl = "https://localhost:7292/api/Branch";

    public int Role { get; set; }

    [BindProperty]
    public List<SelectListItem> Roles { get; set; } =
    [
        new SelectListItem { Value = "1", Text = "Admin" },
        new SelectListItem { Value = "2", Text = "Staff" },
        new SelectListItem { Value = "3", Text = "Manager" }
    ];

    public IList<BranchAccount> BranchAccount { get; set; } = default!;

    public async Task OnGetAsync()
    {
        using var httpClient = new HttpClient();
        var response = await httpClient.GetAsync(BaseUrl);
        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            BranchAccount = JsonSerializer.Deserialize<IList<BranchAccount>>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }
    }
}