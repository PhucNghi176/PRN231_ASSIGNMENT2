using BO;
using DAO;

namespace REPO;
public class CategoryRepo : ICategoryRepo
{
    public async Task AddAsync(Category entity)
    {
        await CategoryDao.Instance.AddAsync(entity);
    }

    public async Task UpdateAsync(Category entity)
    {
        await CategoryDao.Instance.UpdateAsync(entity);
    }

    public async Task DeleteAsync(int id)
    {
        await CategoryDao.Instance.DeleteAsync(id);
    }

    public async Task<Category> GetByIdAsync(int id)
    {
        return await CategoryDao.Instance.GetByIdAsync(id);
    }

    public Task<PagedResult<Category>> GetAllAsync(int page, int pageSize)
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
        return await CategoryDao.Instance.GetAllAsync();
    }
}