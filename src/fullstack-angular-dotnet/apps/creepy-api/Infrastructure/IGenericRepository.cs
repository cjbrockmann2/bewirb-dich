using CreepyApi.Domain;

namespace CreepyApi.Infrastructure;

public interface IGenericRepository<T>
{
  T? Get(Guid id);
  IEnumerable<T> GetAll();
  IEnumerable<T> Find(Func<T, bool> func);
  void Add(T entity);
  void Remove(T entity);
  void Save();
}



