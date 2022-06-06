using zdravstvena_ustanova.Model;

namespace zdravstvena_ustanova.Service.ServiceInterface;

public interface IAccountService : IService<Account>
{
    Person Login(string username, string password);
    bool IsUniqueUsername(string username);
}