using System.Text.Json;
using BO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;

namespace FE.Pages.SilverJewelryPages;
[Authorize]
public class IndexModel : PageModel
{
    private readonly string _baseUrl = "https://localhost:7292/api/Silver";
    public IList<SilverJewelry> SilverJewelry { get; set; } = default!;
    public int CurrentPage { get; set; } = 1;
    public int TotalPages { get; set; }
    public int PageSize { get; set; } = 7;
   

    [BindProperty(SupportsGet = true)] public string Query { get; set; }

    public async Task OnGetAsync(int? pageNumber)
    {
        CurrentPage = pageNumber ?? 1;
        using var httpClient = new HttpClient();

        // Build the OData query
        var baseQuery = $"{_baseUrl}?$top={PageSize}&$skip={(CurrentPage - 1) * PageSize}";

        // Add search filter if query is provided
        if (!string.IsNullOrWhiteSpace(Query))
        {
            // Search in both Name and Description using OData contains function
            baseQuery +=
                $"&$filter=contains(tolower({nameof(BO.SilverJewelry.SilverJewelryName)}),tolower('{Query}')) or contains(tolower({nameof(BO.SilverJewelry.SilverJewelryDescription)}),tolower('{Query}'))";
        }

        var response = await httpClient.GetAsync(baseQuery);
        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

            // Assuming the API response includes total count and items (adjust accordingly)
            var pagedResponse = JsonSerializer.Deserialize<List<SilverJewelry>>(content, options);
            SilverJewelry = pagedResponse;
            
            //   TotalPages = (int)Math.Ceiling((double)pagedResponse / PageSize);
        }
    }
}