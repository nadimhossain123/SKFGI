using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BusinessLayer.Common
{
    public class Grade
    {
        public Grade()
        {
        }
        public int Save(Entity.Common.Grade Grade)
        {
            return DataAccess.Common.Grade.Save(Grade);
        }
        public DataTable GetAll()
        {
            return DataAccess.Common.Grade.GetAll();
        }

        public Entity.Common.Grade GetAllById(int GradeId)
        {
            return DataAccess.Common.Grade.GetAllById(GradeId);
        }
        public int Delete(int GradeId)
        {
            return DataAccess.Common.Grade.Delete(GradeId);
        }


    }
}
