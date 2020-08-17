using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entity.Student
{
   public class StudentPromotion
    {
       public StudentPromotion()
       {
       }

       public int CourseId { get; set; }
       public int batch_id { get; set; }
       public int StreamId { get; set; }
       public int NewSemNo { get; set; }
       public string StudentIDXML { get; set; }
    }
}
