<%@ Page Language="C#" MasterPageFile="~/MasterAdmin.Master" AutoEventWireup="true"
    CodeBehind="IndividualSalaryDeatails.aspx.cs" Inherits="CollegeERP.Payroll.IndividualSalaryDeatails"
    Title="Individual Salary Details" %>
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ToolkitScriptManager ID="toolScript1" runat="server"></asp:ToolkitScriptManager>
    <div class="title">
        <h5>
            Individual Salary Details</h5>
    </div>
    <br />
    <table width="98%" align="center" class="table">
        <tr>
            <td align="left" width="15%" class="label">
                Employee :
            </td>
            <td align="left" width="30%">
                <%--<asp:DropDownList ID="ddlEmployee" runat="server" CssClass="dropdownList" Width="330px"
                    DataValueField="EmployeeId" DataTextField="FullName">
                </asp:DropDownList>--%>
                 <asp:ComboBox ID="ddlEmployee" runat="server" CssClass="WindowsStyle" DropDownStyle="DropDown"
                                AutoCompleteMode="SuggestAppend" CaseSensitive="false" Width="360px" DataValueField="EmployeeId"
                                DataTextField="FullName">
                 </asp:ComboBox>
            </td>
            <td align="center" width="10%" class="label">
                Financial Year :
            </td>
            <td align="left" width="15%">
                <asp:DropDownList ID="ddlFinYear" runat="server" CssClass="dropdownList" Width="150px">
                    <asp:ListItem Value="2009" Text="2009-2010"></asp:ListItem>
                    <asp:ListItem Value="2010" Text="2010-2011"></asp:ListItem>
                    <asp:ListItem Value="2011" Text="2011-2012"></asp:ListItem>
                    <asp:ListItem Value="2012" Text="2012-2013" Selected="True"></asp:ListItem>
                    <asp:ListItem Value="2013" Text="2013-2014"></asp:ListItem>
                    <asp:ListItem Value="2014" Text="2014-2015"></asp:ListItem>
                    <asp:ListItem Value="2015" Text="2015-2016"></asp:ListItem>
                    <asp:ListItem Value="2016" Text="2016-2017"></asp:ListItem>
                    <asp:ListItem Value="2017" Text="2017-2018"></asp:ListItem>
                    <asp:ListItem Value="2018" Text="2018-2019"></asp:ListItem>
                    <asp:ListItem Value="2019" Text="2019-2020"></asp:ListItem>
                </asp:DropDownList>
            </td>
            <td align="left">
                <asp:Button ID="btnSearch" runat="server" CssClass="button" Text="Search" OnClick="btnSearch_Click" />
            </td>
        </tr>
    </table>
    <br />
    <h6 align="left" style="color: #00356A;">
        Employee Details</h6>
    <table width="98%" align="center" class="table">
        <tr>
            <td align="left" width="15%" class="label">
                Employee :
            </td>
            <td align="left" width="18%">
                <asp:TextBox ID="txtEmployeeName" runat="server" CssClass="textbox_disabled" Width="120px"
                    Enabled="false"></asp:TextBox>
            </td>
            <td align="left" width="15%" class="label">
                EMP Code :
            </td>
            <td align="left" width="18%">
                <asp:TextBox ID="txtEmpCode" runat="server" CssClass="textbox_disabled" Width="120px"
                    Enabled="false"></asp:TextBox>
            </td>
            <td align="left" width="15%" class="label">
                Department :
            </td>
            <td align="left" width="18%">
                <asp:TextBox ID="txtDepartment" runat="server" CssClass="textbox_disabled" Width="120px"
                    Enabled="false"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="left" width="15%" class="label">
                Designation :
            </td>
            <td align="left" width="18%">
                <asp:TextBox ID="txtDesignation" runat="server" CssClass="textbox_disabled" Width="120px"
                    Enabled="false"></asp:TextBox>
            </td>
            <td align="left" width="15%" class="label">
                Joining Date :
            </td>
            <td align="left" width="18%">
                <asp:TextBox ID="txtDOJ" runat="server" CssClass="textbox_disabled" Width="120px"
                    Enabled="false"></asp:TextBox>
            </td>
            <td align="left" width="15%" class="label">
                PAN :
            </td>
            <td align="left" width="18%">
                <asp:TextBox ID="txtPAN" runat="server" CssClass="textbox_disabled" Width="120px"
                    Enabled="false"></asp:TextBox>
            </td>
        </tr>
    </table>
    <br />
    <h6 align="left" style="color: #00356A;">
        Salary Details</h6>
    <table width="98%" align="center" class="table">
        <tr>
            <td align="center">
                <asp:Panel ID="PNLGrid" runat="server" ScrollBars="Horizontal" Width="980px" Height="380px">
                    <asp:GridView ID="dgvSalary" runat="server" AllowPaging="false" Width="100%">
                        <HeaderStyle CssClass="HeaderStyle" />
                        <RowStyle CssClass="RowStyle" />
                        <AlternatingRowStyle CssClass="AltRowStyle" />
                        <PagerStyle CssClass="PagerStyle" />
                    </asp:GridView>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td align="center">
                <asp:Button ID="btnPrint" runat="server" CssClass="button" Text="Print" OnClick="btnPrint_Click" />&nbsp;
                <asp:Button ID="btnDownload" runat="server" CssClass="button" Text="Download" OnClick="btnDownload_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
