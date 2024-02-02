using Shop.Core.Entities;

namespace Shop.Business.Interfaces
{
    public interface IUserService
    {
        void Register(User user);
        void Delete(int id);
        void ShowAll();
        void SearchUser(string Name);
        User GetUserById(int id);
        bool UserLogin (string UserName, string Password);
    }
}