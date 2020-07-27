using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Elearning.Website.Models
{
    public class UserClassModel
    {
        public int ClassID { get; set; }
        public string ClassName { get; set; }
        public string  ClassDescription { get; set; }
        public decimal ClassPrice { get; set; }
    }
}