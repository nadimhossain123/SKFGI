using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entity.Common
{
    public class EmployeeOfficial
    {
        public EmployeeOfficial()
        {
        }

        public int EmployeeOfficialId { get; set; }
        public int EmployeeOfficial_EmployeeId { get; set; }
        public int EmployeeOfficial_CategoryId { get; set; }
        public int EmployeeOfficial_DepartmentId { get; set; }
        public int EmployeeOfficial_DesignationId { get; set; }
        public int EmployeeOfficial_BranchId { get; set; }
        public int EmployeeOfficial_GradeId { get; set; }
        public DateTime DOJ { get; set; }
        public DateTime? ConfDate { get; set; }
        public DateTime? EffectiveDate { get; set; }
        public string PTax { get; set; }
        public string EvaluationType { get; set; }
        public DateTime? LastEvaluationDate { get; set; }
        public string HasPF { get; set; }
        public DateTime? PFEffectiveDate { get; set; }
        public string PFNo { get; set; }
        public string HasESI { get; set; }
        public DateTime? ESIEffectiveDate { get; set; }
        public string ESINo { get; set; }
        public string HasTDS { get; set; }
        public DateTime? TDSEffectiveDate { get; set; }
        public string PANNo { get; set; }
        public string HasHealthCard { get; set; }
        public string MediclaimNo { get; set; }
        public DateTime? DOR { get; set; }
        public DateTime? DOL { get; set; }
        public int NoticePeriod { get; set; }
        public string ReasonForLeaving { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
        public int PayBandId { get; set; }
        public string EmployeeType { get; set; }
        public int WorkingDays { get; set; }
        public string FileNo { get; set; }
        public string UNANo { get; set; }
        public string Aadhaar { get; set; }
    }
}
