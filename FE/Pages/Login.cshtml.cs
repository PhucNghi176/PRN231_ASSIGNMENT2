using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FE.Pages;
public class LoginModel : PageModel
{
    private readonly string _baseUrl = "https://localhost:7292/api/Branch/login";
    private readonly HttpClient _httpClient;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public LoginModel(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
    {
        _httpClient = httpClient;
        _httpContextAccessor = httpContextAccessor;
    }

    [BindProperty] public string Email { get; set; }

    [BindProperty] public string Password { get; set; }

    public string ErrorMessage { get; set; }
    public string Token { get; set; }

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password))
        {
            ErrorMessage = "Email and Password cannot be empty.";
            return Page();
        }

        var loginModel = new
        {
            accountEmail = Email,
            accountPassword = Password
        };

        var jsonContent = new StringContent(JsonSerializer.Serialize(loginModel), Encoding.UTF8, "application/json");

        try
        {
            var response = await _httpClient.PostAsync(_baseUrl, jsonContent);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var jsonResponse = JsonDocument.Parse(responseContent);
                Token = jsonResponse.RootElement.GetProperty("token").GetString();

                var handler = new JwtSecurityTokenHandler();

                if (handler.ReadToken(Token) is not JwtSecurityToken jsonToken)
                    return RedirectToPage("SilverJewelryPages/Index");
                var accountEmail = jsonToken.Claims.First(claim => claim.Type == "email").Value;
                var accountId = jsonToken.Claims.First(claim => claim.Type == "Id").Value;
                var role = jsonToken.Claims.First(claim => claim.Type == "role").Value;

                _httpContextAccessor.HttpContext.Session.SetString("JWTToken", Token);

                AddRoleClaim(role, accountId, accountEmail);

                return RedirectToPage("SilverJewelryPages/Index");
            }

            ErrorMessage = "Case login unsuccessfully, display: You are not allowed to access this function!";
        }
        catch (HttpRequestException e)
        {
            ErrorMessage = $"Request error: {e.Message}";
        }

        return Page();
    }

    private void AddRoleClaim(string role, string userId, string email)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.Role, role),
            new(ClaimTypes.NameIdentifier, userId),
            new(JwtRegisteredClaimNames.Email, email)
        };
        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var principal = new ClaimsPrincipal(identity);
        HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
    }
}