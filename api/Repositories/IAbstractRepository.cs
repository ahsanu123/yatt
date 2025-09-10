namespace YATT.Api.Repositories;

public interface IAbstractRepository<T>
{
    public Task<bool> CheckIfIdExists(long id);
    public Task<T?> GetById(long id);
    public Task<IEnumerable<T>> GetAll();
    public Task<long> Insert(T data);
    public Task<long> InsertAll(IEnumerable<T> data);
    public Task<long> Update(T data);
    public Task<long> UpdateAll(IEnumerable<T> data);
    public Task<long> Delete(int id);
    public Task<long> Count();
    public Task<IEnumerable<T>> GetPaged(int index, int pageSize);
}
