using Shop.Business.Interfaces;
using Shop.Core.Entities;
using Shop.DataAccess.DataAccess;

namespace Shop.Business.Services
{
    public class UserService : IUserService
    {
        AppDbContext appDbContext = new AppDbContext();
        public void Register(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            User checkUser = appDbContext.users.Where(x => x.UserName == user.UserName).FirstOrDefault();
            if (checkUser == null)
            {
                appDbContext.users.Add(user);
                appDbContext.SaveChanges();
            }
            else
            {
                Console.WriteLine("bu adda istifadeci movcuddur");
            }
        }
        public string GetUserRole(int id)
        {
            User checkUser = appDbContext.users.Where(x => x.Id==id).FirstOrDefault();
            if (checkUser.IsAdmin == 1)
            {
                return "admin";
            }
            else
            {
                return "user";
            }
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public User GetUserById(int id)
        {
            throw new NotImplementedException();
        }

        public void SearchUser(string Name)
        {
            throw new NotImplementedException();
        }

        public void ShowAll()
        {
            List<User> list = new List<User>();
            list = appDbContext.users.ToList();
            foreach (var item in list) 
            {
                Console.WriteLine("id"+item.Id+" name"+item.UserName+" surname"+item.UserSurname+" email"+item.Email);
            }
        }

        public bool UserLogin(string UserName, string Password)
        {
            User user = new User();
            user=appDbContext.users.Where(x=>x.UserName==UserName && x.Password==Password).FirstOrDefault();
            if (user!=null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public int GetUserId(string UserName, string Password)
        {
            User user = new User();
            user = appDbContext.users.Where(x => x.UserName == UserName && x.Password == Password).FirstOrDefault();
            return user.Id; 
        }
    }
}
