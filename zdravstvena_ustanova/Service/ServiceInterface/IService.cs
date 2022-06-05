using System.Collections;
using System.Collections.Generic;

namespace zdravstvena_ustanova.Service.ServiceInterface;

public interface IService<T>
{
    public IEnumerable<T> GetAll();
    public T Get(long id);
    public T Create(T t);
    public bool Update(T t);
    public bool Delete(long id);
}