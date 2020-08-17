using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BusinessLayer.Payroll
{
    public class MonthYearList
    {
        string[] Month = new string[] { "JANUARY", "FEBRUARY", "MARCH", "APRIL", "MAY", "JUNE", "JULY", "AUGUST", "SEPTEMBER", "OCTOBER", "NOVEMBER", "DECEMBER" };
        string[] Year = new string[] {"2012", "2013", "2014", "2015", "2016","2017","2018","2019","2020" };
        
        public MonthYearList()
        {
            
        }
        

        public DataTable GetMonth()
        {
            DataTable DTMonth=new DataTable();
            DTMonth.Columns.Add("MonthNo", typeof(int));
            DTMonth.Columns.Add("MonthName", typeof(string));
            DataRow DR;
            for (int i = 0; i < Month.Length; i++)
            {
                DR = DTMonth.NewRow();
                DR["MonthNo"] = i + 1;
                DR["MonthName"] = Month[i].Trim();
                DTMonth.Rows.Add(DR);
                DTMonth.AcceptChanges();
            }

            return DTMonth;
        }

        public DataTable GetYear()
        {
            DataTable DTYear=new DataTable();
            DTYear.Columns.Add("YearNo", typeof(int));
            DTYear.Columns.Add("YearName", typeof(string));
            DataRow DR;
            for (int i = 0; i < Year.Length; i++)
            {
                DR = DTYear.NewRow();
                DR["YearNo"] = int.Parse(Year[i].Trim());
                DR["YearName"] = Year[i].Trim();
                DTYear.Rows.Add(DR);
                DTYear.AcceptChanges();
            }

            return DTYear;
        }

        public DataTable GetYearList(DateTime JoiningDate)
        {
            int JoiningYear = JoiningDate.Year;
            int CurrentYear = DateTime.Now.Year;
            DataView dv = new DataView(GetYear());
            dv.RowFilter = "YearNo >= " + JoiningYear + " and YearNo <= " + CurrentYear;
            DataTable dt = dv.ToTable();

            DataRow dr = dt.NewRow();
            dr["YearNo"] = 0;
            dr["YearName"] = "--Select Year--";
            dt.Rows.InsertAt(dr, 0);
            dt.AcceptChanges();
            return dt;
        }

        public DataTable GetBlankYearList()
        {

            DataView dv = new DataView(GetYear());
            dv.RowFilter = "YearNo = 1900";
            DataTable dt = dv.ToTable();

            DataRow dr = dt.NewRow();
            dr["YearNo"] = 0;
            dr["YearName"] = "--Select Year--";
            dt.Rows.InsertAt(dr, 0);
            dt.AcceptChanges();
            return dt;
        }

        public DataTable GetMonthList(DateTime JoiningDate, int SelectedYear)
        {
            DataView dv = new DataView(GetMonth());
            if (SelectedYear == JoiningDate.Year)
            {
                if (SelectedYear == DateTime.Now.Year)
                {
                    dv.RowFilter = "MonthNo >= " + JoiningDate.Month + " and MonthNo < " + DateTime.Now.Month;
                }
                else {
                    dv.RowFilter = "MonthNo >= " + JoiningDate.Month;
                }
            }
            else if (SelectedYear == DateTime.Now.Year)
            {
                dv.RowFilter = "MonthNo <= " + DateTime.Now.Month;
            }
            else
            {
                dv.RowFilter = "MonthNo=MonthNo";
            }

            DataTable dt = dv.ToTable();

            DataRow dr = dt.NewRow();
            dr["MonthNo"] = 0;
            dr["MonthName"] = "--Select Month--";
            dt.Rows.InsertAt(dr, 0);
            dt.AcceptChanges();
            return dt;
        }

        public DataTable GetBlankMonthList()
        {

            DataView dv = new DataView(GetMonth());
            dv.RowFilter = "MonthNo = 0";
            DataTable dt = dv.ToTable();

            DataRow dr = dt.NewRow();
            dr["MonthNo"] = 0;
            dr["MonthName"] = "--Select Month--";
            dt.Rows.InsertAt(dr, 0);
            dt.AcceptChanges();
            return dt;
        }
    }
}
