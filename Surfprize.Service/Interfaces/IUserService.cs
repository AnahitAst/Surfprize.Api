using Surfprize.Entity;
using System.Threading.Tasks;

namespace Surfprize.Service.Interfaces
{
    public interface IUserService : IService<User>
    {
        Task<User> GetByEmailAsync(string email);
        User GetByEmail(string email);
    }
}
