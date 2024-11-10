using BO;

namespace DAO;
public interface IBaseDao<T> where T : class
{
    Task AddAsync(T entity);
    Task? UpdateAsync(T entity);
    Task? DeleteAsync(string id);
    Task<T>? GetByIdAsync(int id);
    Task<PagedResult<T>> GetAllAsync(int page, int pageSize);
    Task<IEnumerable<T>> GetAllAsync();
}

public interface IBranchAccountDao : IBaseDao<BranchAccount>
{
    BranchAccount GetByEmail(string email);
    Task BanAsync(int id);
}

public interface ICategoryDao : IBaseDao<Category>;

public interface ISilverJewelryDao : IBaseDao<SilverJewelry>;