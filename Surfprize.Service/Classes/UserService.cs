using Surfprize.Core;
using Surfprize.DAL;
using Surfprize.Entity;
using Surfprize.Models.Account;
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

        public async Task<User> AddUser(SignUpRequestModel model)
        {
            User user = new User
            {
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Password = PasswordHasher.HashPassword(model.Password),
                PhoneNumber = model.PhoneNumber
            };

            await AddAsync(user);
            
            return user;
        }


        public async Task<User> EditUser(EditRequestModel model)
        {

            User user = FindById(model.UserId);

            user.Email = model.Email;
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Password = PasswordHasher.HashPassword(model.Password);
            user.PhoneNumber = model.PhoneNumber;
            
            await AddAsync(user);
            return user;
        }


        public User Delete(DeleteRequestModel model)
        {

            User user = FindById(model.UserId);           
            Delete(user);
            return user;
        }
    }
}
