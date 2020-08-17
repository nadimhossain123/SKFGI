using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BusinessLayer.Common
{
    public class Category
    {
        public Category()
        {
        }

        public int  Save(Entity.Common.Category Category)
        {
           return DataAccess.Common.Category.Save(Category);    
        }
        
        
        public DataTable GetAll()
        {
            return DataAccess.Common.Category.GetAll();
        }

        public Entity.Common.Category GetAllById(int CategoryId)
        {
            return DataAccess.Common.Category.GetAllById(CategoryId);
        }

        public int Delete( int CategoryId)
        {
            return DataAccess.Common.Category.Delete(CategoryId);
        }
    }
}
