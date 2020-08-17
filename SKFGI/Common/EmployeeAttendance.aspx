<%@ Page Language="C#" MasterPageFile="~/MasterAdmin.Master" AutoEventWireup="true"
    CodeBehind="EmployeeAttendance.aspx.cs" Inherits="CollegeERP.Common.EmployeeAttendance"
    Title="Employee Attendance" %>

<%@ Register Src="../UserControl/Message.ascx" TagName="Message" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">
        function Validation()
        {
            if (document.getElementById('<%=ddlYear.ClientID%>').selectedIndex == 0)
            {
                alert('Please Select Year');
                return false;
            }
            else if (document.getElementById('<%=ddlMonth.ClientID%>').selectedIndex == 0)
            {
                alert('Please Select Month');
                return false;
            }
            else {return true;}
        }
    
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="title">
        <h5>
            Employee Attendance</h5>
    </div>
    <uc3:Message ID="Message" runat="server" />
    <div style="width: 740px;">
        <table width="100%" align="center" class="table">
            <tr>
                <td align="left" width="20%" class="label">
                    Year<span class="req">*</span>
                </td>
                <td align="left" width="30%">
                    <asp:DropDownList ID="ddlYear" runat="server" AutoPostBack="true" CssClass="dropdownList"
                        Width="150px" DataValueField="YearNo" DataTextField="YearName" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <td align="left" width="20%" class="label">
                    Month
                </td>
                <td align="left" width="30%">
                    <asp:DropDownList ID="ddlMonth" runat="server" CssClass="dropdownList" Width="150px"
                        DataValueField="MonthNo" DataTextField="MonthName">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td align="left" width="20%" class="label">
                    Upload<span class="req">*</span>
                </td>
                <td align="left" colspan="3">
                    <asp:FileUpload ID="uploadExcel" runat="server" class="label" />
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <br />
                </td>
            </tr>
            <tr>
                <td align="center" colspan="4">
                    <asp:Button ID="btnImport" runat="server" CssClass="button" Text="Import Attendance"
                        OnClientClick="return Validation()" onclick="btnImport_Click" />
                    &nbsp;
                    <asp:Button ID="btnShow" runat="server" CssClass="button" Text="Show Attendance"
                        OnClientClick="return Validation()" onclick="btnShow_Click" />
                </td>
            </tr>
        </table>
        <br />
        <table width="100%" align="center" class="table">
            <tr>
                <td align="center">
                    <asp:GridView ID="dgvAttendance" runat="server" AutoGenerateColumns="false" GridLines="None"
                        Width="100%" DataKeyNames="EmployeeAttendanceId">
                        <Columns>
                            <asp:BoundField DataField="EmpCode" HeaderText="Employee Code" />
                            <asp:BoundField DataField="Present" HeaderText="Present Days" />
                            <asp:BoundField DataField="Absent" HeaderText="Absent Days" />
                        </Columns>
                        <HeaderStyle CssClass="HeaderStyle" />
                        <RowStyle CssClass="RowStyle" />
                        <EmptyDataRowStyle CssClass="EditRowStyle" />
                        <AlternatingRowStyle CssClass="AltRowStyle" />
                        <PagerStyle CssClass="PagerStyle" />
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
