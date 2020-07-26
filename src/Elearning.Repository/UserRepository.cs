using Elearning.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elearning.Repository
{
    public interface IUserRepository
    {
        UserModel Login(string email, string password);
        UserModel Register(string email, string password);

    }

    public class UserModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class UserRepository : IUserRepository
    {
        public UserModel Login(string email, string password)
        {
            var user = DatabaseAccessor.Instance.Users
                 .FirstOrDefault(x => x.UserEmail.ToLower() == email.ToLower() && x.UserPassword == password.ToLower());
            if(user!=null)
            {
                return new UserModel { Id = user.UserId, Name = user.UserEmail };
            }
            return null;
        }

        public UserModel Register(string email, string password)
        {
            var user = DatabaseAccessor.Instance.Users
                        .Add(new Elearning.Database.User
                        {
                            UserEmail = email,
                            UserPassword = password
                        });
            DatabaseAccessor.Instance.SaveChanges();
            return new UserModel { Id = user.UserId, Name = user.UserEmail };
        }
    }
}
