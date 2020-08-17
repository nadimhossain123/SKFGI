using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BusinessLayer.Student
{
    public class SemFeesGeneration
    {
        public SemFeesGeneration()
        {
        }

        public void GenerateSemFees(Entity.Student.SemFeesGeneration SemFees)
        {
            DataAccess.Student.SemFeesGeneration.GenerateSemFees(SemFees);
        }

        public DataTable GetConsolidated_StudentOutstandingReport(Entity.Student.SemFeesGeneration SemFees)
        {
            return DataAccess.Student.SemFeesGeneration.GetConsolidated_StudentOutstandingReport(SemFees);
        }

    }
}
