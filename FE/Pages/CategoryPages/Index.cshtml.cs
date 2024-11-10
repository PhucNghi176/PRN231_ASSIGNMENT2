using System.Text.Json;
using BO;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FE.Pages.CategoryPages;
public class IndexModel : PageModel
{
    private const string _baseUrl = "https://localhost:7292/api/Category";


    public IList<Category> Category { get; set; } = default!;

    public async Task OnGetAsync()
    {
        using var httpClient = new HttpClient();
        var response = await httpClient.GetAsync(_baseUrl);
        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            Category = JsonSerializer.Deserialize<List<Category>>(content, options);
        }
    }
}