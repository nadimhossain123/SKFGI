<%@ Page Language="C#" MasterPageFile="~/MasterAdmin.Master" AutoEventWireup="true"
    CodeBehind="MonthlySalaryGeneration.aspx.cs" Inherits="CollegeERP.Payroll.MonthlySalaryGeneration"
    Title="Monthly Salary Generation" %>

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
            Monthly Salary Generation</h5>
    </div>
    <uc3:Message ID="Message" runat="server" />
    <div style="width: 830px;">
        <table width="100%" align="center" class="table">
            <tr>
                <td align="center" width="20%" class="label">
                    Year<span class="req">*</span>
                </td>
                <td align="left" width="30%">
                    <asp:DropDownList ID="ddlYear" runat="server" AutoPostBack="true" CssClass="dropdownList"
                        Width="150px" DataValueField="YearNo" DataTextField="YearName" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <td align="center" width="20%" class="label">
                    Month
                </td>
                <td align="left" width="30%">
                    <asp:DropDownList ID="ddlMonth" runat="server" CssClass="dropdownList" Width="150px"
                        DataValueField="MonthNo" DataTextField="MonthName">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <br />
                </td>
            </tr>
            <tr>
                <td align="center" colspan="4">
                    <asp:Button ID="btnGenerate" runat="server" CssClass="button" Text="Generate Salary"
                        OnClientClick="return Validation()" OnClick="btnGenerate_Click" />
                    &nbsp;
                    <asp:Button ID="btnShow" runat="server" CssClass="button" Text="Salary Registration"
                        OnClientClick="return Validation()" OnClick="btnShow_Click" />
                    &nbsp;
                    <asp:Button ID="btnPaymentAdvise" runat="server" CssClass="button" Text="Payment Advise (A/C)"
                        OnClientClick="return Validation()" OnClick="btnPaymentAdvise_Click" />
                    &nbsp;
                    <asp:Button ID="btnPaymentAdviseOTH" runat="server" CssClass="button" Text="Payment Advise (OTH)"
                        OnClientClick="return Validation()" OnClick="btnPaymentAdviseOTH_Click" />
                    &nbsp;
                    <asp:Button ID="btnFinalize" runat="server" CssClass="button" Text="Finalize Salary"
                        OnClientClick="return Validation()" OnClick="btnFinalize_Click" />
                </td>
            </tr>
        </table>
        <br />
        <br />
    </div>
    <table width="98%" align="center" class="table">
        <tr>
            <td align="center">
                <asp:Panel ID="PNLGrid" runat="server" ScrollBars="Both" Width="980px" Height="300px">
                    <asp:GridView ID="dgvSalary" runat="server" AllowPaging="false" Width="100%">
                        <HeaderStyle CssClass="HeaderStyle" />
                        <RowStyle CssClass="RowStyle" />
                        <AlternatingRowStyle CssClass="AltRowStyle" />
                        <PagerStyle CssClass="PagerStyle" />
                    </asp:GridView>
                </asp:Panel>
            </td>
        </tr>
        <tr><td><br /></td></tr>
        <tr>
            <td align="center">
                <asp:Button ID="btnPrint" runat="server" CssClass="button" Text="Print" 
                    onclick="btnPrint_Click" />&nbsp;
                <asp:Button ID="btnDownload" runat="server" CssClass="button" Text="Export in Excel" OnClick="btnDownload_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
