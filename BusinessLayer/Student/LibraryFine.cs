using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BusinessLayer.Student
{
    public class LibraryFine
    {
        public LibraryFine()
        {
        }

        public void Save(Entity.Student.LibraryFine Fine)
        {
            DataAccess.student.LibraryFine.Save(Fine);
        }

        public DataTable GetAll(string VoucherNo,DateTime From,DateTime To)
        {
            return DataAccess.student.LibraryFine.GetAll(VoucherNo,From,To);
        }

        

        public Entity.Student.LibraryFine GetAllById(int LibraryFineId)
        {
            return DataAccess.student.LibraryFine.GetAllById(LibraryFineId);
        }

        public DataTable GetApprovedStudentList()
        {
            return DataAccess.student.LibraryFine.GetApprovedStudentList();
        }

        public DataTable GetNoyDropoutList()
        {
            return DataAccess.student.LibraryFine.GetNotDropoutList();
        }

        public int LibraryFineDelete(int LibraryFineId)
        {
            return DataAccess.student.LibraryFine.LibraryFineDelete(LibraryFineId);
        }
    }
}
