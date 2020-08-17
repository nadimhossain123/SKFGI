<%@ Page Language="C#" MasterPageFile="~/MasterAdmin.Master" AutoEventWireup="true"
    CodeBehind="AllEmployeesPaySlip.aspx.cs" Inherits="CollegeERP.Payroll.AllEmployeesPaySlip"
    Title="All Employees PaySlip" %>

<%@ Register Src="../UserControl/Message.ascx" TagName="Message" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">
        function Validation()
        {
            if (document.getElementById('<%=ddlEmployee.ClientID%>').selectedIndex == 0)
               return ShowMsg('Please Select Employee');
            else if (document.getElementById('<%=ddlYear.ClientID%>').selectedIndex == 0)
               return ShowMsg('Please Select Year');
            else if (document.getElementById('<%=ddlMonth.ClientID%>').selectedIndex == 0)
               return ShowMsg('Please Select Month');
            else 
                return true;
        }
        
        function ShowMsg(str)
        {
            alert(str);
            return false;
        }    
    
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="title">
        <h5>
            All Employees Monthly Pay Slip</h5>
    </div>
    <div style="width: 900px;">
        <uc3:Message ID="Message" runat="server" />
        <br />
        <table width="100%" align="center" class="table">
            <tr>
                <td align="left" width="30%" class="label">
                    Employee
                </td>
                <td align="left" width="30%" class="label">
                    Year
                </td>
                <td align="left" width="30%" class="label">
                    Month
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td align="left" width="30%">
                    <asp:DropDownList ID="ddlEmployee" runat="server" CssClass="dropdownList" Width="200px"
                        DataValueField="EmployeeId" DataTextField="FullName" AutoPostBack="true" OnSelectedIndexChanged="ddlEmployee_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <td align="left" width="30%">
                    <asp:DropDownList ID="ddlYear" runat="server" AutoPostBack="true" CssClass="dropdownList"
                        Width="150px" DataValueField="YearNo" DataTextField="YearName" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <td align="left" width="30%">
                    <asp:DropDownList ID="ddlMonth" runat="server" CssClass="dropdownList" Width="150px"
                        DataValueField="MonthNo" DataTextField="MonthName">
                    </asp:DropDownList>
                </td>
                <td align="center">
                    <asp:Button ID="btnShow" runat="server" CssClass="button" Text="Download" OnClientClick="return Validation()"
                        OnClick="btnShow_Click" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
