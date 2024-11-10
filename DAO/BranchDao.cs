using BO;
using Microsoft.EntityFrameworkCore;

namespace DAO;
public class BranchDao : IBranchAccountDao
{
    private readonly SilverJewelry2023DbContext _context = new();
    private static BranchDao instance = null;
    private static readonly object instacelock = new();

    private BranchDao()
    {
    }

    public static BranchDao Instance
    {
        get
        {
            lock (instacelock)
            {
                instance ??= new BranchDao();
                return instance;
            }
        }
    }

    public async Task AddAsync(BranchAccount entity)
    {
        var id = _context.BranchAccounts.Max(x => x.AccountId) + 1;
        entity.AccountId = id;
        _context.BranchAccounts.Add(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(BranchAccount entity)
    {
        var existingEntity = _context.BranchAccounts.Find(entity.AccountId);
        if (existingEntity != null)
        {
            _context.Entry(existingEntity).State = EntityState.Detached;
        }

        _context.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        _context.BranchAccounts.Remove(_context.BranchAccounts.Find(id));
        await _context.SaveChangesAsync();
    }

    public async Task<BranchAccount> GetByIdAsync(int id)
    {
        var result = await _context.BranchAccounts.FindAsync(id);
        return result;
    }

    public Task<PagedResult<BranchAccount>> GetAllAsync(int page, int pageSize)
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

    public async Task<IEnumerable<BranchAccount>> GetAllAsync()
    {
        return await _context.BranchAccounts.ToListAsync();
    }

    public BranchAccount GetByEmail(string email)
    {
        return _context.BranchAccounts.FirstOrDefault(x => x.EmailAddress == email && x.Role != 0);
    }

    public async Task BanAsync(int id)
    {
        var entity = await _context.BranchAccounts.FindAsync(id);
        entity.Role = 0;
        await _context.SaveChangesAsync();
    }

    
}