using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BusinessLayer.HR
{
    public class ExpenseType
    {
        public ExpenseType()
        {
        }

        public int Save(Entity.HR.ExpenseType ObjType)
        {
            return DataAccess.HR.ExpenseType.Save(ObjType);
        }

        public DataTable GetAll()
        {
            return DataAccess.HR.ExpenseType.GetAll();
        }

        public Entity.HR.ExpenseType GetAllById(int ExpenseTypeId)
        {
            return DataAccess.HR.ExpenseType.GetAllById(ExpenseTypeId);
        }

        public int Delete(int ExpenseTypeId)
        {
            return DataAccess.HR.ExpenseType.Delete(ExpenseTypeId);
        }
    }
}
