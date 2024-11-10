using BO;
using Microsoft.EntityFrameworkCore;

namespace DAO;
public class SilverJewelryDao : ISilverJewelryDao
{
    // add singleton
    private readonly SilverJewelry2023DbContext _context = new();
    private static SilverJewelryDao instance = null;
    private static readonly object instacelock = new();

    private SilverJewelryDao()
    {
    }

    public static SilverJewelryDao Instance
    {
        get
        {
            lock (instacelock)
            {
                instance ??= new SilverJewelryDao();
                return instance;
            }
        }
    }

    public async Task AddAsync(SilverJewelry entity)
    {
        var id = _context.SilverJewelries.Select(x => x.SilverJewelryId).Max() + 1;
        entity.CreatedDate = DateTime.Now;
        entity.SilverJewelryId = id;
        var cate = await _context.Categories.FindAsync(entity.CategoryId);
        entity.Category = cate;
        try
        {
            _context.SilverJewelries.Add(entity);
        }
        catch (Exception e)
        {
            Console.BackgroundColor = ConsoleColor.Red;
            Console.WriteLine(e);
            throw;
        }
        
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(SilverJewelry entity)
    {
        var existingEntity = await _context.SilverJewelries.FindAsync(entity.SilverJewelryId);
        if (existingEntity != null)
        {
            _context.Entry(existingEntity).State = EntityState.Detached;
        }

        _context.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await _context.SilverJewelries.FindAsync(id);
        if (entity != null)
        {
            _context.SilverJewelries.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<SilverJewelry> GetByIdAsync(int id)
    {
        var item = await _context.SilverJewelries
            .Include(x => x.Category)
            .FirstOrDefaultAsync(x => x.SilverJewelryId == id);
        return item;
    }

    public async Task<PagedResult<SilverJewelry>> GetAllAsync(int page, int pageSize)
    {
        var totalItems = await _context.SilverJewelries.CountAsync();
        var items = await _context.SilverJewelries
            .Include(x => x.Category)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PagedResult<SilverJewelry>
        {
            TotalCount = totalItems,
            Items = items
        };
    }

    public Task<int> CountAsync()
    {
        throw new NotImplementedException();
    }

    public Task<bool> SaveAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<SilverJewelry>> GetAllAsync()
    {
        return await _context.SilverJewelries.Include(x => x.Category).ToListAsync();
    }
}