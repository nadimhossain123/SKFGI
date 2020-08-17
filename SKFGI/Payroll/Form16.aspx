<%@ Page Language="C#" MasterPageFile="~/MasterAdmin.Master" AutoEventWireup="true"
    CodeBehind="Form16.aspx.cs" Inherits="CollegeERP.Payroll.Form16" Title="Form16" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function openpopup(poplocation, querystring, popheight, popwidth, poptop, popleft) {
            if (popheight == '') {
                popheight = 500
            }
            var popposition = 'left = 300, top=60, width=' + popwidth + ',align=center, height='+ popheight +',menubar=no, scrollbars=yes, resizable=no';

            var NewWindow = window.open(poplocation + querystring, '', popposition);
            if (NewWindow.focus != null) {
                NewWindow.focus();
            }
            return false;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ToolkitScriptManager ID="ToolScript1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="title">
        <h5>
            Form16</h5>
    </div>
    <asp:UpdatePanel ID="Up1" runat="server">
        <ContentTemplate>
            <div style="width: 720px;">
                <table width="100%" align="center" class="table">
                    <tr>
                        <td align="center">
                            <asp:GridView ID="dgvForm16Generate" runat="server" AutoGenerateColumns="false" Width="100%"
                                AllowPaging="false" DataKeyNames="EmployeeId" 
                                onrowdatabound="dgvForm16Generate_RowDataBound">
                                <Columns>
                                    <asp:BoundField DataField="EmployeeName" HeaderText="Employee Name" />
                                    <asp:BoundField DataField="Section17_1" HeaderText="Gross Salary" />
                                    <asp:BoundField DataField="TaxOnTotalIncome" HeaderText="Tax On Total Income" />
                                    <asp:BoundField DataField="TaxPaid" HeaderText="Tax Paid" />
                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnPrint" runat="server" ImageUrl="~/Images/print-icon.JPG"
                                                Width="20px" Height="20px" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
