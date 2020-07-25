using Elearning.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elearning.Repository
{
    public interface IClassRepository
    {
        ClassModel[] Classes{ get; }
        ClassModel IndividualClass(int classId);
    }

    public class ClassModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
    public class ClassRespository : IClassRepository
    {
        public ClassModel[] Classes
        {
            get
            {
                return DatabaseAccessor.Instance.Classes
                    .Select(x => new ClassModel { Id = x.ClassId, Name = x.ClassName, Description = x.ClassDescription, Price = x.ClassPrice }).ToArray();
            }
        }
        public ClassModel IndividualClass(int classId)
        {
            var item = DatabaseAccessor.Instance.Classes.Where(x => x.ClassId == classId)
                .Select(t => new ClassModel { Id = t.ClassId, Description = t.ClassDescription, Name = t.ClassName, Price = t.ClassPrice })
                .FirstOrDefault();
            return item;

        }
    }
}
