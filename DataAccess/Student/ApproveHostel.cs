using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DataAccess.Student
{
   public  class ApproveHostel
    {
       public static DataSet GetStudentDetails(int IntMode)
       {
           using (DataManager oDm = new DataManager())
           {
               DataSet ds = new DataSet();
               oDm.CommandType = CommandType.StoredProcedure;
               oDm.Add("@IntMode", SqlDbType.Int, IntMode);

               return oDm.GetDataSet("usp_ApprovedHostel", ref ds, "table");
           }
       }
       public static DataSet GetStudentDetailsByDate(int IntMode,DateTime Datefrom,DateTime DateTo)
       {
           using (DataManager oDm = new DataManager())
           {
               DataSet ds = new DataSet();
               oDm.CommandType = CommandType.StoredProcedure;
               oDm.Add("@IntMode", SqlDbType.Int, IntMode);
               oDm.Add("@HostelDate", SqlDbType.DateTime, Datefrom);
               oDm.Add("@HostelTo", SqlDbType.DateTime, DateTo);
               return oDm.GetDataSet("usp_ApprovedHostel", ref ds, "table");
           }
       }
       public static int UpdateHostelApproveList(string ApproveHostelListXML)
       {
           using (DataManager oDm = new DataManager())
           {

               oDm.Add("@IntMode", SqlDbType.Int, 1);
               oDm.Add("@ApproveHostelListXML", SqlDbType.Xml, ApproveHostelListXML);

               oDm.CommandType = CommandType.StoredProcedure;
               return oDm.ExecuteNonQuery("usp_ApprovedHostel");

           }
       }
       public static int UpdateHostelStudentApproveById(Entity.Student.ApproveHostel objAHE)
       {
           using (DataManager oDm = new DataManager())
           {

               oDm.Add("@IntMode", SqlDbType.Int, 2);
               oDm.Add("@Student_Id", SqlDbType.Int, objAHE.StudentId);
               oDm.Add("@HostelDetails", SqlDbType.VarChar, objAHE.Hosteldetail );
               oDm.Add("@HostelDate", SqlDbType.DateTime, objAHE.HostelDate);
               oDm.Add("@IsHostel_Approved", SqlDbType.Bit, objAHE.IsHostelApproved);
               oDm.Add("@IsHostelRelease", SqlDbType.Bit, objAHE.IsHostelRelease);

               oDm.CommandType = CommandType.StoredProcedure;
               return oDm.ExecuteNonQuery("usp_ApprovedHostel");
           }
       }
       public static int UpdateHostelStudentReleaseById(Entity.Student.ApproveHostel objAHE)
       {
           using (DataManager oDm = new DataManager())
           {
               oDm.Add("@IntMode", SqlDbType.Int, 3);
               oDm.Add("@Student_Id", SqlDbType.Int, objAHE.StudentId);
               oDm.Add("@HostelDetails", SqlDbType.VarChar, objAHE.Hosteldetail);
               oDm.Add("@HostelDate", SqlDbType.DateTime, objAHE.HostelDate);
               oDm.Add("@IsHostel_Approved", SqlDbType.Bit, objAHE.IsHostelApproved);
               oDm.Add("@IsHostelRelease", SqlDbType.Bit, objAHE.IsHostelRelease);

               oDm.CommandType = CommandType.StoredProcedure;
               return oDm.ExecuteNonQuery("usp_ApprovedHostel");

           }
       }
    }
}
