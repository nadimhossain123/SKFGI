using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entity.Student
{
   public class SearchStudent
    {
       public int id { get; set; }
        public int intMode { get; set; }
        public int intCompanyId { get; set; }
        public string login_id { get; set; }
        public string studentID { get; set; }

        //Modified by Pritam on 06.09.2012
        public string appliation_no { get; set; }
        public string name { get; set; }
        public int CourseId { get; set; }
        public int StreamId { get; set; }
        public int batch_id { get; set; }
        public string Photo { get; set; }

        public int StateId { get; set; }
        public int DistrictId { get; set; }
        public int CityId { get; set; }
        public int SchoolId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
    }
}
