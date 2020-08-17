using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BusinessLayer.Common
{
     public  class Branch
    {
         public Branch()
         {
         }

         public int Save( Entity.Common.Branch Branch)
         {
            return DataAccess.Common.Branch.Save( Branch);
         }

         public DataTable GetAll()
         {
             return DataAccess.Common.Branch.GetAll();
         }

         public Entity.Common.Branch GetAllById(int BranchId)
         {
             return DataAccess.Common.Branch.GetAllById(BranchId);
         }

         public int Delete(int BranchId)
         {
             return DataAccess.Common.Branch.Delete(BranchId);
         }
    }
}
