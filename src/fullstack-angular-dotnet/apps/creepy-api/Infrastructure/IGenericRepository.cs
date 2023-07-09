using CreepyApi.Domain;

namespace CreepyApi.Infrastructure;

public interface IGenericRepository<T>
{
  T? Find(Guid id);
  List<T> List();
  void Add(T dokument);
  void Save();
}



