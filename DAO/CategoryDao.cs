using BO;
using Microsoft.EntityFrameworkCore;

namespace DAO;
public class CategoryDao : ICategoryDao
{
    private readonly SilverJewelry2023DbContext _context = new();
    private static CategoryDao instance = null;
    private static readonly object instacelock = new();

    private CategoryDao()
    {
    }

    public static CategoryDao Instance
    {
        get
        {
            lock (instacelock)
            {
                instance ??= new CategoryDao();
                return instance;
            }
        }
    }

    public async Task AddAsync(Category entity)
    {
        var id = _context.Categories.Select(x => x.CategoryId).Max() + 1;
        entity.CategoryId = id;
        _context.Categories.Add(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Category entity)
    {
        var existingEntity = await _context.Categories.FindAsync(entity.CategoryId);
        if (existingEntity != null)
        {
            _context.Entry(existingEntity).State = EntityState.Detached;
        }

        _context.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var category = await GetByIdAsync(id);
        _context.Categories.Remove(category);
        await _context.SaveChangesAsync();
    }

    public async Task<Category> GetByIdAsync(int id)
    {
        return await _context.Categories.FirstOrDefaultAsync(x => x.CategoryId == id);
    }

    public async Task<PagedResult<Category>> GetAllAsync(int page, int pageSize)
    {
        throw new NotImplementedException();
    }

    public Task<int> CountAsync()
    {
        throw new NotImplementedException();
    }

    public Task<bool> SaveAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Category>> GetAllAsync()
    {
        return await _context.Categories.ToListAsync();
    }
}