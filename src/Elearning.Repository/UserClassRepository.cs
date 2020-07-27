using Elearning.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elearning.Repository
{
    public interface IUserClassRepository
    {
        UserClassModel Add(int userid, int classid);
        UserClassModel[] ForUser(int userid);// try it with ClassModel
    }

    public class UserClassModel
    {
        //public int UserId { get; set; }
        public int ClassId { get; set; }
        public string ClassName { get; set; }
        public string ClassDescription { get; set; }
        public decimal ClassPrice { get; set; }
    }

    public class UserClassRepository : IUserClassRepository
    {
        public UserClassModel Add(int userid, int classid)
        {
            throw new NotImplementedException();
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
