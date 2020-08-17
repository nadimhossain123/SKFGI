<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAdmin.Master" AutoEventWireup="true" CodeBehind="PTaxSummeryReport.aspx.cs" Inherits="CollegeERP.Payroll.PTaxSummeryReport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script type="text/javascript">
    function validation() {
        if (document.getElementById('<%=ddlFromToYear.ClientID %>').selectedIndex == 0) {
            alert('Please select year');
            document.getElementById('<%=ddlFromToYear.ClientID %>').focus();
        }
    }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div style="width: 740px;">
        <table width="40%" align="center" class="table">
            <tr>
                <td>
                    <asp:DropDownList ID="ddlFromToYear" runat="server" Width="192px" CssClass="dropdownList">
                        <asp:ListItem Value="0" Text="Select"></asp:ListItem>
                        <asp:ListItem Value="2009-2010" Text="2009-2010"></asp:ListItem>
                        <asp:ListItem Value="2010-2011" Text="2010-2011"></asp:ListItem>
                        <asp:ListItem Value="2011-2012" Text="2011-2012"></asp:ListItem>
                        <asp:ListItem Value="2012-2013" Text="2012-2013"></asp:ListItem>
                        <asp:ListItem Value="2013-2014" Text="2013-2014"></asp:ListItem>
                        <asp:ListItem Value="2014-2015" Text="2014-2015"></asp:ListItem>
                        <asp:ListItem Value="2015-2016" Text="2015-2016"></asp:ListItem>
                        <asp:ListItem Value="2016-2017" Text="2016-2017"></asp:ListItem>
                        <asp:ListItem Value="2017-2018" Text="2017-2018"></asp:ListItem>
                        <asp:ListItem Value="2018-2019" Text="2018-2019"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="button" OnClick="btnSearch_Click" OnClientClick="javascript:validation();" />
                </td>
                <td>
                    <asp:Button ID="btnExport" runat="server" Text="Export" CssClass="button" 
                        onclick="btnExport_Click" OnClientClick="javascript:validation();"/>
                </td>
            </tr>
        </table>
    </div>
    <asp:Panel ID="Panel1" runat="server" Width="100%" Height="350px" ScrollBars="Auto">
        <div id="divPTaxReport" runat="server">
        </div>
    </asp:Panel>
</asp:Content>
