using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess.Common
{
    public class StatutorySalaryConfig
    {
        public StatutorySalaryConfig()
        {
        }

        public static void Save(Entity.Common.StatutorySalaryConfig Config)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@pEffectiveDate", SqlDbType.DateTime, Config.EffectiveDate);
                oDm.Add("@pEmployersPFCntrb", SqlDbType.Decimal, Config.EmployersPFCntrb);
                oDm.Add("@pEmployeesPFCntrb", SqlDbType.Decimal, Config.EmployeesPFCntrb);
                oDm.Add("@pEmployersESICntrb", SqlDbType.Decimal, Config.EmployersESICntrb);
                oDm.Add("@pEmployeesESICntrb", SqlDbType.Decimal, Config.EmployeesESICntrb);
                oDm.Add("@pESILimit", SqlDbType.Decimal, Config.ESILimit);
                oDm.Add("@pEmployersPension", SqlDbType.Decimal, Config.EmployersPension);
                oDm.Add("@pPFAdminCharges", SqlDbType.Decimal, Config.PFAdminCharges);
                oDm.Add("@pEDLICharges", SqlDbType.Decimal, Config.EDLICharges);
                oDm.Add("@pEDLIAdminCharges", SqlDbType.Decimal, Config.EDLIAdminCharges);
                oDm.Add("@pStatutorySalaryConfigCUser_UserId", SqlDbType.Int, Config.StatutorySalaryConfigCUser_UserId);

                oDm.CommandType = CommandType.StoredProcedure;

                oDm.ExecuteNonQuery("usp_StatutorySalaryConfig_Save");
            }
        }

        public static Entity.Common.StatutorySalaryConfig GetAll()
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.CommandType = CommandType.StoredProcedure;
                SqlDataReader dr = oDm.ExecuteReader("usp_StatutorySalaryConfig_GetAll");
                Entity.Common.StatutorySalaryConfig Config = new Entity.Common.StatutorySalaryConfig();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Config.EffectiveDate = (dr[0] == DBNull.Value) ? DateTime.MinValue : DateTime.Parse(dr[0].ToString());
                        Config.EmployersPFCntrb = (dr[1] == DBNull.Value) ? 0 : decimal.Parse(dr[1].ToString());
                        Config.EmployeesPFCntrb = (dr[2] == DBNull.Value) ? 0 : decimal.Parse(dr[2].ToString());
                        Config.EmployersESICntrb = (dr[3] == DBNull.Value) ? 0 : decimal.Parse(dr[3].ToString());
                        Config.EmployeesESICntrb = (dr[4] == DBNull.Value) ? 0 : decimal.Parse(dr[4].ToString());
                        Config.ESILimit = (dr[5] == DBNull.Value) ? 0 : decimal.Parse(dr[5].ToString());
                        Config.EmployersPension = (dr[6] == DBNull.Value) ? 0 : decimal.Parse(dr[6].ToString());
                        Config.PFAdminCharges = (dr[7] == DBNull.Value) ? 0 : decimal.Parse(dr[7].ToString());
                        Config.EDLICharges = (dr[8] == DBNull.Value) ? 0 : decimal.Parse(dr[8].ToString());
                        Config.EDLIAdminCharges = (dr[9] == DBNull.Value) ? 0 : decimal.Parse(dr[9].ToString());

                    }
                }
                return Config;
            }
        }
    }
}
