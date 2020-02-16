using Surfprize.Entity;
using Surfprize.Models.Account;
using System.Threading.Tasks;

namespace Surfprize.Service.Interfaces
{
    public interface IUserService : IService<User>
    {
        Task<User> GetByEmailAsync(string email);
        User GetByEmail(string email);
        Task<User> AddUser(SignUpRequestModel model);
        User Delete(DeleteRequestModel model);
        Task<User> EditUser(EditRequestModel model);
    }
}
