using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elearning.Database
{
    public class DatabaseAccessor
    {
        private static readonly ProjectDBEntities entities;
        
        static DatabaseAccessor()
        {
            entities = new ProjectDBEntities();
            entities.Database.Connection.Open();
        }

        public static ProjectDBEntities Instance
        {
            get
            {
                return entities;
            }
        }
    }
}
