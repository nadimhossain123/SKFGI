using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess.Common
{
    public class AppointmentLetter
    {
        public AppointmentLetter()
        {
        }

        public static DataSet GetAppointmentLetter(Entity.Common.AppointmentLetter Appointment)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@EmployeeId", SqlDbType.Int, Appointment.EmployeeId);
                oDm.Add("@IssuedBy", SqlDbType.Int, Appointment.IssuedBy);

                oDm.CommandType = CommandType.StoredProcedure;
                DataSet ds = new DataSet();
                return oDm.GetDataSet("usp_AppointmentLetter", ref ds, "tbl");
            }
        }
    }
}
