using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entity.Common
{
    public class EmployeeRole
    {
        public EmployeeRole()
        {
        }

        public int EmployeeRoleMappingId { get; set; }
        public int EmployeeId { get; set; }
        public int RoleId { get; set; }
    }
}
