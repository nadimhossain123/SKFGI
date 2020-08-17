using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DataAccess.Student
{
    public class HostelFeesConfig
    {
        public HostelFeesConfig()
        {
        }

        public static int Save(Entity.Student.HostelFeesConfig Hostel)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@id", SqlDbType.Int, Hostel.id);
                oDm.Add("@batch_id", SqlDbType.Int, Hostel.batch_id);
                oDm.Add("@fees_name", SqlDbType.VarChar,200, Hostel.fees_name);
                oDm.Add("@feesdetailsxml", SqlDbType.Xml, Hostel.feesdetailsxml);

                oDm.CommandType = CommandType.StoredProcedure;
                return oDm.ExecuteNonQuery("usp_hostel_fees_header_Save");
            }
        }

        public static DataTable GetAll(int? batch_id)
        {
            using (DataManager oDm = new DataManager())
            {
                if (batch_id == null)
                    oDm.Add("@batch_id", SqlDbType.Int, DBNull.Value);
                else
                    oDm.Add("@batch_id", SqlDbType.Int, batch_id);

                oDm.CommandType = CommandType.StoredProcedure;
                return oDm.ExecuteDataTable("usp_hostel_fees_header_GetAll");
            }
        }

        public static DataSet GetAllById(int id)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@id", SqlDbType.Int, id);
                oDm.CommandType = CommandType.StoredProcedure;

                DataSet ds = new DataSet();
                return oDm.GetDataSet("usp_hostel_fees_header_GetAllById", ref ds, "table");
            }
        }
    }
}
