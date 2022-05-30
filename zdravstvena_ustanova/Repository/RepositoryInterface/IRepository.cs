using System.Collections;
using System.Collections.Generic;

namespace zdravstvena_ustanova.Repository.RepositoryInterface;

public interface IRepository<T>
{
    public IEnumerable<T> GetAll();
    public T Get(long id);
    public T Create(T t);
    public bool Update(T t);
    public bool Delete(long id);
}