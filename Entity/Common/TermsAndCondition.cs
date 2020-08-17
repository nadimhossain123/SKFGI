using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entity.Common
{
    public class TermsAndCondition
    {
        public TermsAndCondition()
        {
        }

        public int TermsId { get; set; }
        public string TermsName { get; set; }
        public int EmployeeId { get; set; }
        public bool IsChecked { get; set; }

    }
}
