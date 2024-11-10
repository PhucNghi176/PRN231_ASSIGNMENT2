using BO;
using DAO;

namespace REPO;
public class SilverJewelryRepo : ISilverJewelryRepo
{
    public async Task AddAsync(SilverJewelry entity)
    {
        await SilverJewelryDao.Instance.AddAsync(entity);
    }

    public Task<int> CountAsync()
    {
        throw new NotImplementedException();
    }

    public async Task DeleteAsync(int id)
        => await SilverJewelryDao.Instance.DeleteAsync(id);

    public async Task<PagedResult<SilverJewelry>> GetAllAsync(int page, int pageSize)
        => await SilverJewelryDao.Instance.GetAllAsync(page, pageSize);

    public async Task<SilverJewelry> GetByIdAsync(int id)
        => await SilverJewelryDao.Instance.GetByIdAsync(id);

    public Task<bool> SaveAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<SilverJewelry>> GetAllAsync()
    {
        return await SilverJewelryDao.Instance.GetAllAsync();
    }

    public async Task UpdateAsync(SilverJewelry entity)
        => await SilverJewelryDao.Instance.UpdateAsync(entity);
}