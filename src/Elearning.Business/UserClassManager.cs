using Elearning.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elearning.Business
{
    public interface IUserClassManager
    {
        void Add(int userid, int classid);
        UserClassModel[] ForUser(int userid);
    }

    public class UserClassModel
    {
        public int userId { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }

    public class UserClassManager : IUserClassManager
    {
        private readonly IUserClassRepository userClassRepository;

        public UserClassManager(IUserClassRepository userClassRepository)
        {
            this.userClassRepository = userClassRepository;
        }
        public void Add(int userid, int classid)
        {
            userClassRepository.Add(userid, classid);
        }

        public UserClassModel[] ForUser(int userid)
        {
            return userClassRepository.ForUser(userid).Select(x =>
                new UserClassModel
                {
                    Id = x.ClassId,
                    Name = x.ClassName,
                    Description = x.ClassDescription,
                    Price = x.ClassPrice
                }).ToArray();
        }
    }
}
