using Elearning.Database;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Elearning.Repository
{
    public interface IUserClassRepository
    {
        void Add(int userid, int classid);
        UserClassModel[] ForUser(int userid);// try it with ClassModel
    }

    public class UserClassModel
    {
        public int UserId { get; set; }
        public int ClassId { get; set; }
        public string ClassName { get; set; }
        public string ClassDescription { get; set; }
        public decimal ClassPrice { get; set; }
    }

    public class UserClassRepository : IUserClassRepository
    {
        public void Add(int userid, int classid)
        {
            //var checkuserclasscombination = DatabaseAccessor.Instance.Users.FirstOrDefault(x => x.UserId == userid).Classes.FirstOrDefault(x => x.ClassId == sharedclass.ClassId);

            var checkuserclasscombination = DatabaseAccessor.Instance.Users.Include(n => n.Classes).FirstOrDefault(x => x.UserId == userid && x.Classes.Any(n=>n.ClassId== classid));

            if (checkuserclasscombination!=null)
            {
                throw new ArgumentException("User already has the class selected");
            }
            else
            {
                var user = (Elearning.Database.User)DatabaseAccessor.Instance.Users.First(x => x.UserId == userid);
                var newclass = (Elearning.Database.Class)DatabaseAccessor.Instance.Classes.First(x => x.ClassId == classid);

                user.Classes.Add(newclass);
                DatabaseAccessor.Instance.SaveChanges();
            }
        }

        public UserClassModel[] ForUser(int userid)
        {
            return DatabaseAccessor.Instance.Users.First(x => x.UserId == userid)
                    .Classes.Select(x =>
                    new UserClassModel
                    {
                        ClassId = x.ClassId,
                        ClassName = x.ClassName,
                        ClassDescription = x.ClassDescription,
                        ClassPrice = x.ClassPrice
                    }).ToArray();
        }
    }
}
