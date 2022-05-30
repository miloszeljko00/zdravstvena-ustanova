using zdravstvena_ustanova.Model;

namespace zdravstvena_ustanova.Repository.RepositoryInterface;

public interface IAccountRepository : IRepository<Account>
{
    public Account GetByUsername(string username);
}