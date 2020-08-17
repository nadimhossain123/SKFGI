using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess.Payroll
{
    public class EmployeeSalary
    {
        public EmployeeSalary()
        {
        }

        public static int Save(Entity.Payroll.EmployeeSalary Salary)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@pEmployeeSalaryId", SqlDbType.Int, ParameterDirection.Input, Salary.EmployeeSalaryId);
                oDm.Add("@pEmployeeSalary_EmpId", SqlDbType.Int, ParameterDirection.Input, Salary.EmployeeSalary_EmpId);
                oDm.Add("@pEmployeeSalaryBasicAmount", SqlDbType.Decimal, ParameterDirection.Input, Salary.EmployeeSalaryBasicAmount);
                oDm.Add("@pEmployeeSalaryPFAmount", SqlDbType.Decimal, ParameterDirection.Input, Salary.EmployeeSalaryPFAmount);
                oDm.Add("@pEmployeeSalary_PTaxId", SqlDbType.Int, ParameterDirection.Input, Salary.EmployeeSalary_PTaxId);
                oDm.Add("@pEmployeeSalaryEmployerPF", SqlDbType.Decimal, ParameterDirection.Input, Salary.EmployeeSalaryEmployerPF);
                oDm.Add("@pModifiedBy", SqlDbType.Int, ParameterDirection.Input, Salary.ModifiedBy);
                oDm.Add("@pIsFixedPF", SqlDbType.Bit, ParameterDirection.Input, Salary.IsFixedPF);
                oDm.Add("@pEmployeeSalaryGradePay", SqlDbType.Decimal, ParameterDirection.Input, Salary.EmployeeSalaryGradePay);

                string SalaryHeadDetailsXML = string.Empty;
                if (Salary.EmployeeSalaryHeadDTO != null && Salary.EmployeeSalaryHeadDTO.Length > 0)
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add("EmployeeSalaryHead_SalaryHeadId");
                    dt.Columns.Add("EmployeeSalaryHeadPercent");
                    dt.Columns.Add("EmployeeSalaryHeadAmount");

                    for (int i = 0; i < Salary.EmployeeSalaryHeadDTO.Length; i++)
                    {
                        DataRow dr = dt.NewRow();
                        dr["EmployeeSalaryHead_SalaryHeadId"] = Salary.EmployeeSalaryHeadDTO[i].EmployeeSalaryHead_SalaryHeadId;
                        dr["EmployeeSalaryHeadPercent"] = Salary.EmployeeSalaryHeadDTO[i].EmployeeSalaryHeadPercent;
                        dr["EmployeeSalaryHeadAmount"] = Salary.EmployeeSalaryHeadDTO[i].EmployeeSalaryHeadAmount;
                        
                        dt.Rows.Add(dr);
                        dt.AcceptChanges();
                    }

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        using (DataSet ds = new DataSet())
                        {
                            ds.Tables.Add(dt);

                            SalaryHeadDetailsXML = ds.GetXml();
                        }
                    }
                }

                if (SalaryHeadDetailsXML.Length == 0)
                    oDm.Add("@pSalaryHeadDetails", SqlDbType.Xml, DBNull.Value);
                else
                    oDm.Add("@pSalaryHeadDetails", SqlDbType.Xml, SalaryHeadDetailsXML);
                
                oDm.CommandType = CommandType.StoredProcedure;
                return oDm.ExecuteNonQuery("usp_EmployeeSalary_Save");
            }
        }

        public static DataTable GetAll(string EmpCode, string FirstName)
        {
            using (DataManager oDm = new DataManager())
            {
                if (EmpCode.Trim().Length == 0)
                { oDm.Add("@pEmpCode", SqlDbType.VarChar, 100, DBNull.Value); }
                else { oDm.Add("@pEmpCode", SqlDbType.VarChar, 100, EmpCode); }

                if (FirstName.Trim().Length == 0)
                { oDm.Add("@pFirstName", SqlDbType.VarChar, 100, DBNull.Value); }
                else { oDm.Add("@pFirstName", SqlDbType.VarChar, 100, FirstName); }

                oDm.CommandType = CommandType.StoredProcedure;
                return oDm.ExecuteDataTable("usp_EmployeeSalary_GetAll");
            }
        }

        public static DataTable GetAllSalaryHead(int EmployeeId)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@pEmployeeSalaryHead_EmployeeId", SqlDbType.Int, EmployeeId);
                oDm.CommandType = CommandType.StoredProcedure;
                return oDm.ExecuteDataTable("usp_EmployeeSalaryHead_GetAll");
            }
        }

        public static Entity.Payroll.EmployeeSalary GetAllById(int EmployeeId)
        {
            using (DataManager oDm = new DataManager())
            {

                oDm.CommandType = CommandType.StoredProcedure;

                oDm.Add("@pEmployeeSalary_EmpId", SqlDbType.Int, ParameterDirection.Input, EmployeeId);

                SqlDataReader dr = oDm.ExecuteReader("usp_EmployeeSalary_GetAllById");
                Entity.Payroll.EmployeeSalary Salary = new Entity.Payroll.EmployeeSalary();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Salary.EmployeeSalaryId = (dr[0] == DBNull.Value) ? 0 : int.Parse(dr[0].ToString());
                        Salary.EmployeeSalaryBasicAmount = (dr[1] == DBNull.Value) ? 0 : decimal.Parse(dr[1].ToString());

                        Salary.EmployeeSalaryPFAmount = (dr[2] == DBNull.Value) ? 0 : decimal.Parse(dr[2].ToString());
                        Salary.EmployeeSalary_PTaxId = (dr[3] == DBNull.Value) ? 0 : int.Parse(dr[3].ToString());
                        Salary.EmployeeSalaryEmployerPF = (dr[4] == DBNull.Value) ? 0 : decimal.Parse(dr[4].ToString());
                        Salary.IsFixedPF = (dr[5] == DBNull.Value) ? false : bool.Parse(dr[5].ToString());
                        Salary.EmployeeSalaryGradePay = (dr[6] == DBNull.Value) ? 0 : decimal.Parse(dr[6].ToString());

                    }
                }
                return Salary;
            }
        }


    }
}
