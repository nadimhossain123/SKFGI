using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entity.Common
{
    public class StatutorySalaryConfig
    {
        public StatutorySalaryConfig()
        {
        }
        public int StatutorySalaryConfigId { get; set; }
        public DateTime EffectiveDate { get; set; }
        public decimal EmployersPFCntrb { get; set; }
        public decimal EmployeesPFCntrb { get; set; }
        public decimal EmployersESICntrb { get; set; }
        public decimal EmployeesESICntrb { get; set; }
        public decimal ESILimit { get; set; }
        public decimal EmployersPension { get; set; }
        public decimal PFAdminCharges { get; set; }
        public decimal EDLICharges { get; set; }
        public decimal EDLIAdminCharges { get; set; }
        public int StatutorySalaryConfigCUser_UserId { get; set; }


    }
}
