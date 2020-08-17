using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.UI;
using BusinessLayer.Accounts;

namespace BusinessLayer.Common
{
    public class Excel : Page
    {
        public Excel()
        {
        }

        public static void SaveExcel(string[] header, System.Web.UI.WebControls.GridView DataGridView, string[] footer, string filename)
        {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", filename + ".xls"));
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";

            Table table = new Table();
            TableRow row;
            TableCell cell;
            int ColCount = DataGridView.HeaderRow.Cells.Count;

            //Header
            if (header.Length > 0)
            {
                for (int i = 0; i < header.Length; i++)
                {
                    row = new TableRow();
                    cell = new TableCell();
                    cell.ColumnSpan = ColCount;
                    cell.Text = header[i].Trim();
                    cell.Wrap = true;
                    cell.VerticalAlign = VerticalAlign.Middle;
                    cell.HorizontalAlign = HorizontalAlign.Left;
                    cell.Font.Bold = true;
                    row.Cells.Add(cell);
                    table.Rows.Add(row);
                }
            }
            //Grid
            for (int i = 0; i < DataGridView.HeaderRow.Cells.Count; i++)
            {
                DataGridView.HeaderRow.Cells[i].Style.Add("background", "#4CB449");
                DataGridView.HeaderRow.Cells[i].Style.Add("color", "#fff");
                DataGridView.HeaderRow.Cells[i].Style.Add("text-decoration", "none");
            }

            table.GridLines = GridLines.Both;
            if (DataGridView.HeaderRow != null)
            {
                table.Rows.Add(DataGridView.HeaderRow);
            }



            foreach (GridViewRow DgvRow in DataGridView.Rows)
            {
                if (DgvRow.Controls.GetType() == typeof(CheckBox))
                {
                    //gv.Controls.Remove(gv.Controls[i]);
                    //gv.Controls.AddAt(i, l);
                }
                else
                {
                    DgvRow.VerticalAlign = VerticalAlign.Top;
                    table.Rows.Add(DgvRow);
                }
            }

            if (DataGridView.FooterRow != null)
            {
                table.Rows.Add(DataGridView.FooterRow);
            }


            //Footer
            if (footer.Length > 0)
            {
                for (int i = 0; i < footer.Length; )
                {
                    row = new TableRow();
                    if (row.Controls.GetType() == typeof(CheckBox))
                    {
                        i++;
                        continue;
                    }
                    else
                    {
                        cell = new TableCell();
                        cell.ColumnSpan = ColCount;
                        cell.Text = footer[i].Trim();
                        cell.Wrap = true;
                        cell.HorizontalAlign = HorizontalAlign.Left;
                        cell.VerticalAlign = VerticalAlign.Middle;
                        cell.Font.Bold = true;
                        row.Cells.Add(cell);
                        table.Rows.Add(row);
                        i++;
                    }

                }
            }

            using (StringWriter sw = new StringWriter())
            {
                {
                    using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                    {
                        table.RenderControl(htw);
                        HttpContext.Current.Response.Write(sw.ToString());
                        HttpContext.Current.Response.End();
                    }
                }

            }
        }

    }
}
