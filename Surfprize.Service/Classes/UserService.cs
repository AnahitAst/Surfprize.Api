using Surfprize.DAL;
using Surfprize.Entity;
using Surfprize.Service.Interfaces;
using System.Threading.Tasks;

namespace Surfprize.Service.Classes
{
    public class UserService : Service<User>, IUserService
    {
        public UserService(IRepository<User> repository) : base(repository)
        {
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            return await Repository
                .SelectOneAsync(u => u.Email == email && !u.IsDeleted);
        }

        public User GetByEmail(string email)
        {
            return Repository
                .SelectOne(u => u.Email == email && !u.IsDeleted);
        }
    }
}
