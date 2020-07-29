using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Elearning.Website.Models
{
    public class EnrollClassList
    {
        public int ClassId { get; set; }
        public List<ClassModel> ClassList { get; set; }
        public EnrollClassList()
        {
            ClassList = new List<ClassModel>();
        }
    }
}