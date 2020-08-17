<%@ Page Language="C#" MasterPageFile="~/MasterAdmin.Master" AutoEventWireup="true"
    CodeBehind="EmployeeIncrementReport.aspx.cs" Inherits="CollegeERP.Payroll.EmployeeIncrementReport"
    Title="Employee Increment Report" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script language="javascript" type="text/javascript">
        function DateValidation() {
            if (document.getElementById('<%=txtDateFrom.ClientID%>').value == '') {
                alert("Please Enter Date Range");
                return false;
            }
            if (document.getElementById('<%=txtDateTo.ClientID%>').value == '') {
                alert("Please Enter Date Range");
                return false;
            }
        }
     
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ToolkitScriptManager ID="toolScript1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="title">
        <h5>
            Employee Increment Report</h5>
    </div>
    <div style="width: 98%;">
        <table width="100%" align="center" class="table">
            <tr>
                <td align="center" width="15%" class="label">
                    From:<span class="req">*</span>
                </td>
                <td align="left" width="20%">
                    <asp:TextBox ID="txtDateFrom" runat="server" CssClass="textbox_required" Width="110px"
                        onkeydown="return false;"></asp:TextBox>
                    <asp:CalendarExtender ID="CalendarExtender1" runat="server" CssClass="cal_Theme1"
                        PopupPosition="BottomRight" Format="dd MMM yyyy" TargetControlID="txtDateFrom"
                        OnClientDateSelectionChanged="" Enabled="True">
                    </asp:CalendarExtender>
                </td>
                <td align="center" width="15%" class="label">
                    To:<span class="req">*</span>
                </td>
                <td align="left" width="20%">
                    <asp:TextBox ID="txtDateTo" runat="server" CssClass="textbox_required" Width="110px"
                        onkeydown="return false;"></asp:TextBox>
                    <asp:CalendarExtender ID="CalendarExtender2" runat="server" CssClass="cal_Theme1"
                        PopupPosition="BottomRight" Format="dd MMM yyyy" TargetControlID="txtDateTo"
                        OnClientDateSelectionChanged="" Enabled="True">
                    </asp:CalendarExtender>
                </td>
                <td align="center">
                    <asp:Button ID="btnShow" CssClass="button" runat="server" Text="Show" OnClientClick="return DateValidation()"
                        OnClick="btnShow_Click" />
                </td>
            </tr>
        </table>
        <br />
        <table width="100%" align="center" class="table">
            <tr>
                <td align="center">
                    <asp:GridView ID="dgvEmployee" runat="server" AutoGenerateColumns="false" Width="100%"
                        AllowPaging="false" GridLines="None" CellPadding="0" CellSpacing="0" DataKeyNames="EmployeeId">
                        <Columns>
                            <asp:BoundField DataField="EmpCode" HeaderText="Employee Code" />
                            <asp:BoundField DataField="FullName" HeaderText="Employee Name" />
                            <asp:BoundField DataField="CategoryName" HeaderText="Category" />
                            <asp:BoundField DataField="DepartmentName" HeaderText="Department" />
                            <asp:BoundField DataField="DesignationName" HeaderText="Designation" />
                            <asp:BoundField DataField="LastEvaluationDate" HeaderText="Last Evaluation Date"
                                DataFormatString="{0:dd/MM/yyyy}" NullDisplayText="" />
                            <asp:BoundField DataField="EffectiveDate" HeaderText="Increment Date" DataFormatString="{0:dd/MM/yyyy}"
                                NullDisplayText="" />
                            <asp:BoundField DataField="EmployeeSalaryBasicAmount" HeaderText="Payable Basic"
                                DataFormatString="{0:F0}" />
                            <asp:BoundField DataField="IncrementAmount" HeaderText="Increment Amount" DataFormatString="{0:F0}" />
                            <asp:BoundField DataField="GrossAmount" HeaderText="Gross" DataFormatString="{0:F0}" />
                            <asp:BoundField DataField="CTC" HeaderText="CTC" DataFormatString="{0:F0}" />
                        </Columns>
                        <EmptyDataTemplate>
                            <table style="height: 10px; width: 100%;">
                                <tr align="left" class="HeaderStyle">
                                    <th scope="col">
                                    </th>
                                </tr>
                                <tr class="RowStyle">
                                    <td>
                                        Sorry! No Employee Found.
                                    </td>
                            </table>
                        </EmptyDataTemplate>
                        <HeaderStyle CssClass="HeaderStyle" />
                        <RowStyle CssClass="RowStyle" />
                        <AlternatingRowStyle CssClass="AltRowStyle" />
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td>
                    <br />
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Button ID="btnPrint" runat="server" CssClass="button" Text="Print" OnClick="btnPrint_Click" />&nbsp;
                    <asp:Button ID="btnExport" runat="server" CssClass="button" Text="Export To Excel"
                        OnClick="btnExport_Click" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
