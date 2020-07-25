using Elearning.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elearning.Business
{
    public interface IClassManager
    {
        ClassModel[] Classes { get; }
        ClassModel IndividualClass(int classId);
    }
    public class ClassModel
    {
        public int ClassId { get; set; }
        public string ClassName { get; set; }
        public string ClassDescription { get; set; }
        public decimal ClassPrice { get; set; }

        public ClassModel(int id, string name, string description, decimal price)
        {
            ClassId = id;
            ClassName = name;
            ClassDescription = description;
            ClassPrice = price;
        }
    }

    public class ClassManager : IClassManager
    {
        private readonly IClassRepository classRepository;

        public ClassManager(IClassRepository classRepository)
        {
            this.classRepository = classRepository;
        }
        
        public ClassModel[] Classes
        {
            get
            {
                return classRepository.Classes.Select(x => new ClassModel(x.Id, x.Name, x.Description, x.Price)).ToArray();
            }
        }

        public ClassModel IndividualClass(int classId)
        {
            var classModel = classRepository.IndividualClass(classId);
            return new ClassModel(classModel.Id, classModel.Name, classModel.Description, classModel.Price);
        }
    }
}
