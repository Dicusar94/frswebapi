using Data.MoackData;
using Data.Repositories.Interfaces;
using Entity.Authorization;

namespace Data.Repositories.Implementations
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(IDataBase context) : base(context)
        {
        }
    }
}
