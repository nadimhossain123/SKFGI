<%@ Page Title="Receipt Payment Account Report" Language="C#" MasterPageFile="~/MasterAdmin.Master" AutoEventWireup="true" CodeBehind="RTPReceiptPaymentAccount.aspx.cs" Inherits="CollegeERP.Accounts.RTPReceiptPaymentAccount" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../UserControl/Message.ascx" TagName="Message" TagPrefix="uc3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function Validation() {
            if (document.getElementById('<%=txtFromDate.ClientID%>').value == '') {
                alert('Enter From Date');
                document.getElementById('<%=txtFromDate.ClientID%>').focus();
                return false;
            }
            else if (document.getElementById('<%=txtToDate.ClientID%>').value == '') {
                alert('Enter To Date');
                document.getElementById('<%=txtToDate.ClientID%>').focus();
                return false;
            }
            else { return true; }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ToolkitScriptManager ID="toolScript1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="title">
        <h5>
            Receipt Payment Account Report</h5>
    </div>
    <div style="width: 550px;">
        <uc3:Message ID="Message" runat="server" />
        <br />
        <table width="100%" align="center" class="table">
            <tr>
                <td width="30%" align="left" class="label">
                    From Date
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
                <td width="30%" align="left" class="label">
                    To Date
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
                    <asp:GridView ID="gvReceiptPaymentAccount" runat="server" Width="100%" AutoGenerateColumns="False"
                        AllowPaging="false">
                        <Columns>
                            <asp:BoundField DataField="ReceiptDetail" HeaderText="Receipt" HtmlEncode="false"></asp:BoundField>
                            <asp:BoundField DataField="ReceiptAmount" HeaderText="Amount"
                                HtmlEncode="false" DataFormatString="{0:n}" ItemStyle-HorizontalAlign="Right">
                            </asp:BoundField>
                            <asp:BoundField DataField="ReceiptTotal" HeaderText="Total" HtmlEncode="false" DataFormatString="{0:n}" ItemStyle-HorizontalAlign="Right"></asp:BoundField>
                            <asp:BoundField DataField="PaymentDetail" HeaderText="Payment" HtmlEncode="false"></asp:BoundField>
                            <asp:BoundField DataField="PaymentAmount" HeaderText="Amount"
                                HtmlEncode="false" DataFormatString="{0:n}" ItemStyle-HorizontalAlign="Right">
                            </asp:BoundField>
                            <asp:BoundField DataField="PaymentTotal" HeaderText="Total" HtmlEncode="false" DataFormatString="{0:n}" ItemStyle-HorizontalAlign="Right"></asp:BoundField>
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
                        onclick="btnPrint_Click" />&nbsp;
                    <asp:Button ID="btnExportExcel" runat="server" CssClass="button" 
                        Text="Export To excel" onclick="btnExportExcel_Click"
                        />
                </td>
            </tr>
        </table>
</asp:Content>
