<%@ Page Language="C#" MasterPageFile="~/MasterAdmin.Master" AutoEventWireup="true"
    CodeBehind="MonthlyPTaxRegister.aspx.cs" Inherits="CollegeERP.Payroll.MonthlyPTaxRegister"
    Title="Monthly PTax Register" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script language="javascript" type="text/javascript">
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
            Monthly PTax Register</h5>
    </div>
    <div style="width: 780px;">
        <table width="100%" align="center" class="table">
            <tr>
                <td align="center" width="15%" class="label">
                    Year<span class="req">*</span>
                </td>
                <td align="left" width="20%">
                    <asp:DropDownList ID="ddlYear" runat="server" AutoPostBack="true" CssClass="dropdownList"
                        Width="150px" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <td align="center" width="15%" class="label">
                    Month
                </td>
                <td align="left" width="20%">
                    <asp:DropDownList ID="ddlMonth" runat="server" CssClass="dropdownList" Width="150px"
                        DataValueField="MonthNo" DataTextField="MonthName">
                    </asp:DropDownList>
                </td>
                <td align="center">
                    <asp:Button ID="btnShow" CssClass="button" runat="server" Text="Show" OnClientClick="return Validation()"
                        OnClick="btnShow_Click" />
                </td>
            </tr>
        </table>
        <br />
        <table width="100%" align="center" class="table">
            <tr>
                <td align="center">
                    <asp:GridView ID="dgvReport" runat="server" Width="100%" AutoGenerateColumns="false"
                        AllowPaging="false" GridLines="None">
                        <Columns>
                            <asp:BoundField DataField="EmpCode" HeaderText="Emp Code" />
                            <asp:BoundField DataField="EmpName" HeaderText="Emp Name" />
                            <asp:BoundField DataField="GrossAmt" HeaderText="Gross Salary" DataFormatString="{0:F0}"
                                ItemStyle-HorizontalAlign="Right" />
                            <asp:BoundField DataField="PTaxAmt" HeaderText="Professional Tax" DataFormatString="{0:F0}"
                                ItemStyle-HorizontalAlign="Right" />
                        </Columns>
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
            <tr id="trButton" runat="server" visible="false">
                <td align="right">
                    <asp:Button ID="btnPrint" runat="server" CssClass="button" Text="Print" OnClick="btnPrint_Click" />&nbsp;
                    <asp:Button ID="btnExport" runat="server" CssClass="button" Text="Export To Excel"
                        OnClick="btnExport_Click" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
