using BO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Attributes;
using REPO;

namespace Controller.Controllers;
[Route("api/[controller]")]
[ODataRouteComponent]
[ApiController]
public class CategoryController : ControllerBase
{
    private readonly ICategoryRepo _categoryRepository;

    public CategoryController(ICategoryRepo categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }
    [HttpGet]
    [EnableQuery]
    public async Task<IActionResult> GetCate()
    {
        var result = await _categoryRepository.GetAllAsync();
        return Ok(result);
    }
  
    [HttpPost]
    public async Task<IActionResult> PostCate([FromBody] Category category)
    {
        await _categoryRepository.AddAsync(category);
        return Ok();
    }
    [HttpPut]
    public async Task<IActionResult> PutCate([FromBody] Category category)
    {
        await _categoryRepository.UpdateAsync(category);
        return Ok();
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCate(int id)
    {
        await _categoryRepository.DeleteAsync(id);
        return Ok();
    }
    
}