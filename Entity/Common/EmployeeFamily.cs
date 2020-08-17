using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entity.Common
{
    public class EmployeeFamily
    {
        public EmployeeFamily()
        {
        }

        public int EmployeeFamilyId { get; set; }
        public int EmployeeFamily_EmployeeId { get; set; }
        public string MemberName { get; set; }
        public string MemberOccupation { get; set; }
        public string MemberRelation { get; set; }
        public string MemberGender { get; set; }
        public string HasMemberContact { get; set; }
        public string MemberContactEmail { get; set; }
        public string MemberContactNo { get; set; }
        public int MemberAge { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
    }
}
