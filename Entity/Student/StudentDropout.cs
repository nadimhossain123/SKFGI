using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entity.Student
{
    public class StudentDropout
    {

        public int Student_Id { get; set; }
        public int Sem { get; set; }
        public DateTime DODate { get; set; }
        public string Reason { get; set; }
        public string login_id { get; set; }
        public int intMode { get; set; }


    }
}
