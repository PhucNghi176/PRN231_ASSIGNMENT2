using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using REPO;

namespace Controller.Controllers;
[Route("api/[controller]")]
[ApiController]
public class BranchController(IBranchAccountRepo branchRepository, IConfiguration configuration) : ControllerBase
{
    [HttpPost("login")]
    public async Task<IActionResult> Post([FromBody] LoginModel loginModel)
    {
        var acc = ValidUser(loginModel);
        if (acc == null) return Unauthorized("Invalid credentials.");
        var token = GenerateJwtToken(acc);
        return Ok(new { token });
    }

    private BranchAccount ValidUser(LoginModel login)
    {
        var account = Authenticate(login.AccountEmail, login.AccountPassword);
        return account;
    }

    private BranchAccount Authenticate(string email, string password)
    {
        var account = branchRepository.GetByEmail(email);
        if (account == null || account.AccountPassword != password)
        {
            return null;
        }

        return account;
    }

    private string GenerateJwtToken(BranchAccount account, bool isAdmin = false)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(configuration["JWT:Secret"]);
        var timeZoneById = TimeZoneInfo.FindSystemTimeZoneById(configuration["TimeZoneId"]);

        var role = account.Role.ToString();

        var claims = new List<Claim>
        {
            new Claim("Id", account.AccountId.ToString()),
            new Claim(ClaimTypes.NameIdentifier, account.AccountId.ToString()),
            new Claim(ClaimTypes.Role, role),
            new Claim(JwtRegisteredClaimNames.Email, account.EmailAddress),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Iat,
                TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, timeZoneById).ToUniversalTime().ToString("o")),
            new Claim(JwtRegisteredClaimNames.Exp,
                TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, timeZoneById).AddHours(7).ToUniversalTime()
                    .ToString("o"))
        };

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        var tokenString = tokenHandler.WriteToken(token);

        return tokenString;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var branches = await branchRepository.GetAllAsync();
        return Ok(branches);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await branchRepository.DeleteAsync(id);
        return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> Put([FromBody] BranchAccount branch)
    {
        await branchRepository.UpdateAsync(branch);
        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] BranchAccount branch)
    {
        await branchRepository.AddAsync(branch);
        return Ok();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var branch = await branchRepository.GetByIdAsync(id);
        return Ok(branch);
    }

    [HttpGet("ban/{id}")]
    public async Task<IActionResult> Ban(int id)
    {
        await branchRepository.BanAsync(id);
        return Ok();
    }
}