using BO;

namespace REPO;

public interface IBaseRepo<T> where T : class
{
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(int id);
    Task<T> GetByIdAsync(int id);
    Task<PagedResult<T>> GetAllAsync(int page,int pageSize);
    Task<IEnumerable<T>> GetAllAsync();
}
// implement this interface in all the repo classes

public interface IBranchAccountRepo : IBaseRepo<BranchAccount>
{
    BranchAccount GetByEmail(string email);
    Task BanAsync(int id);
    
    Task UnBanAsync(int id);
}
public interface ICategoryRepo : IBaseRepo<Category>;
public interface ISilverJewelryRepo : IBaseRepo<SilverJewelry>;
