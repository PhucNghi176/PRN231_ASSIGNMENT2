using BO;
using DAO;

namespace REPO;
public class BranchAccountRepo : IBranchAccountRepo
{
    public async Task AddAsync(BranchAccount entity)
    {
        await BranchDao.Instance.AddAsync(entity);
    }

    public async Task UpdateAsync(BranchAccount entity)
    {
        await BranchDao.Instance.UpdateAsync(entity);
    }

    public async Task DeleteAsync(int id)
    {
        await BranchDao.Instance.DeleteAsync(id);
    }

    public async Task<BranchAccount> GetByIdAsync(int id)
    {
        return await BranchDao.Instance.GetByIdAsync(id);
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
        return await BranchDao.Instance.GetAllAsync();
    }

    public BranchAccount GetByEmail(string email)
    {
        return BranchDao.Instance.GetByEmail(email);
    }

    public async Task BanAsync(int id)
    {
        await BranchDao.Instance.BanAsync(id);
    }

    public Task UnBanAsync(int id)
    {
        throw new NotImplementedException();
    }
}