using CreepyApi.Layers.Core.Models;

namespace CreepyApi.Layers.Application.Abstractions;

public interface IGenericRepository<T> where T : class
{

  List<T> dokumente { get; }
  T? Get(Guid id);
  IEnumerable<T> GetAll();
  IEnumerable<T> Find(Func<T, bool> func);
  void Add(T entity);
  void Remove(T entity);
  void Save();
}



