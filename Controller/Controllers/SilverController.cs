using BO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using REPO;

namespace Controller.Controllers;
[Route("api/[controller]")]
[ApiController]
public class SilverController : ControllerBase
{
    private readonly ISilverJewelryRepo _silverJewelryRepository;

    public SilverController(ISilverJewelryRepo silverJewelryRepository)
    {
        _silverJewelryRepository = silverJewelryRepository;
    }

    // [HttpGet]
    // [EnableQuery]
    // public async Task<IActionResult> GetSivler([FromODataUri] int page, [FromODataUri] int pageSize)
    // {
    //     var result = await _silverJewelryRepository.GetAllAsync(page, pageSize);
    //     return Ok(result);
    // }
    
    
    [HttpGet]
    [EnableQuery]
    public async Task<IActionResult> GetSivler()
    {
        var result = await _silverJewelryRepository.GetAllAsync();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetSivler(int id)
    {
        var result = await _silverJewelryRepository.GetByIdAsync(id);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSivler(int id)
    {
        await _silverJewelryRepository.DeleteAsync(id);
        return NoContent();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutSivler(int id, [FromBody] SilverJewelry entity)
    {
        if (id != entity.SilverJewelryId)
        {
            return BadRequest();
        }

        await _silverJewelryRepository.UpdateAsync(entity);
        return NoContent();
    }

    [HttpPost]
    [EnableQuery]
    public async Task<IActionResult> PostSivler([FromBody] SilverJewelry entity)
    {
        await _silverJewelryRepository.AddAsync(entity);
        return Ok();
    }
}