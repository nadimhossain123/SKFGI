<%@ Page Language="C#" MasterPageFile="~/MasterAdmin.Master" AutoEventWireup="true" CodeBehind="RptStudentFeesCollection.aspx.cs" Inherits="CollegeERP.Accounts.RptStudentFeesCollection" Title="Student Fees Collection" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../UserControl/Message.ascx" TagName="Message" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script type="text/javascript">
        function Validation()
        {
            if (document.getElementById('<%=txtFromDate.ClientID%>').value == '')
            {
                alert('Enter From Date');
                return false;
            }
            else if (document.getElementById('<%=txtToDate.ClientID%>').value == '')
            {
                alert('Enter To Date');
                return false;
            }
            else {return true;}
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ToolkitScriptManager ID="toolScript1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="title">
        <h5>
            Student Fees Collection Report</h5>
    </div>
    <div style="width: 550px;">
        <br />
        <table width="100%" align="center" class="table">
             <tr>
                <td class="label" align="right">
                    Voucher Type :
                </td>
                <td align="left">
                    <asp:DropDownList ID="ddlVoucherType" CssClass="dropdownList" runat="server" Width="140px">
                        <asp:ListItem Value="0" Text="Select"></asp:ListItem>
                        <asp:ListItem Value="PAYMENT" Text="PAYMENT"></asp:ListItem>
                        <asp:ListItem Value="RECEIVE" Text="RECEIVE"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td nowrap align="left">
                </td>
            </tr>
             <tr>
                <td class="label" nowrap align="right">
                    C/B Book&#160;Name :
                </td>
                <td align="left">
                    <asp:DropDownList ID="ddlLedgerVw" CssClass="dropdownList" runat="server" Width="140px">
                    </asp:DropDownList>
                </td>
                <td align="left">
                </td>
            </tr>
            <tr>
                <td width="30%" align="right" class="label">
                    Voucher Date From
                </td>
                <td width="30%" align="left">
                    <asp:TextBox ID="txtFromDate" runat="server" CssClass="textbox" Width="130px"></asp:TextBox>
                    <asp:CalendarExtender ID="CalendarExtender1" runat="server" CssClass="cal_Theme1"
                        PopupPosition="BottomRight" Format="dd MMM yyyy" TargetControlID="txtFromDate"
                        OnClientDateSelectionChanged="" Enabled="True">
                    </asp:CalendarExtender>
                </td>
            </tr>
            <tr>
                <td width="30%" align="right" class="label">
                    Voucher Date To
                </td>
                <td width="30%" align="left">
                    <asp:TextBox ID="txtToDate" runat="server" CssClass="textbox" Width="130px"></asp:TextBox>
                    <asp:CalendarExtender ID="CalendarExtender2" runat="server" CssClass="cal_Theme1"
                        PopupPosition="BottomRight" Format="dd MMM yyyy" TargetControlID="txtToDate"
                        OnClientDateSelectionChanged="" Enabled="True">
                    </asp:CalendarExtender>
                </td>
            </tr>
            <tr>
                <td width="30%" align="right" class="label">
                    Cheque Date From
                </td>
                <td width="30%" align="left">
                    <asp:TextBox ID="txtChequeDateFrom" runat="server" CssClass="textbox" Width="130px"></asp:TextBox>
                    <asp:CalendarExtender ID="CalendarExtender3" runat="server" CssClass="cal_Theme1"
                        PopupPosition="BottomRight" Format="dd MMM yyyy" TargetControlID="txtChequeDateFrom"
                        OnClientDateSelectionChanged="" Enabled="True">
                    </asp:CalendarExtender>
                </td>
            </tr>
            <tr>
                <td width="30%" align="right" class="label">
                    Cheque Date To
                </td>
                <td width="30%" align="left">
                    <asp:TextBox ID="txtChequeDateTo" runat="server" CssClass="textbox" Width="130px"></asp:TextBox>
                    <asp:CalendarExtender ID="CalendarExtender4" runat="server" CssClass="cal_Theme1"
                        PopupPosition="BottomRight" Format="dd MMM yyyy" TargetControlID="txtChequeDateTo"
                        OnClientDateSelectionChanged="" Enabled="True">
                    </asp:CalendarExtender>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <br />
                </td>
            </tr>
            <tr>
                <td width="30%">
                </td>
                <td width="30%" align="left">
                    <asp:Button ID="btnSearch" runat="server" CssClass="button" Text="Search" OnClientClick="return Validation()"
                        OnClick="btnSearch_Click"></asp:Button>
                </td>
            </tr>
        </table>
    </div>
    <br />
    <table width="95%" align="center" class="table">
        <tr>
            <td align="center">
                <asp:GridView ID="dgvStudentFeesCollection" runat="server" AutoGenerateColumns="False" AllowPaging="false"
                    Width="100%">
                    <Columns>
                        <asp:TemplateField HeaderText="Sl No">
                            <ItemTemplate>
                                <%# Container.DataItemIndex+1 %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="CBVoucherNo" HeaderText="Voucher No" />
                        <asp:BoundField DataField="VoucherDate" HeaderText="Voucher Date" DataFormatString="{0:dd MMM yyyy}" />
                        <asp:BoundField DataField="student_code" HeaderText="Student Code" />
                        <asp:BoundField DataField="LedgerName" HeaderText="Ledger Name" />
                        <asp:BoundField DataField="name" HeaderText="Student Name" />
                        <asp:BoundField DataField="SemNo" HeaderText="SemNo" />
                        <asp:BoundField DataField="ChequeNo" HeaderText="ChequeNo" />
                        <asp:BoundField DataField="ChequeDate" HeaderText="Cheque Date" DataFormatString="{0:dd MMM yyyy}" />
                        <asp:BoundField DataField="AmountDr" HeaderText="Amount" DataFormatString="{0:n}" />
                    </Columns>
                    <HeaderStyle CssClass="HeaderStyle" />
                    <RowStyle CssClass="RowStyle" />
                    <AlternatingRowStyle CssClass="AltRowStyle" />
                    <PagerStyle CssClass="PagerStyle" />
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td align="right">
                <asp:Button ID="btnPrint" runat="server" CssClass="button" Text="Print" 
                    onclick="btnPrint_Click"  />&nbsp;
                <asp:Button ID="btnExportExcel" runat="server" CssClass="button" 
                    Text="Export To excel" onclick="btnExportExcel_Click"
                    />
            </td>
        </tr>
    </table>
</asp:Content>
