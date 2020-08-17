<%@ Page Language="C#" MasterPageFile="~/MasterAdmin.Master" AutoEventWireup="true"
    CodeBehind="ExpenseReimbursement.aspx.cs" Inherits="CollegeERP.Accounts.ExpenseReimbursement"
    Title="Expense Reimbursement" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../UserControl/Message.ascx" TagName="Message" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">
        function Validation() {
            var gv = document.getElementById('<%=dgvClaim.ClientID%>');
            var VDate=document.getElementById('<%=txtPaymentDate.ClientID%>').value;
            var rowCount = gv.rows.length - 1;
            
            if (VDate == '') {
                alert("Please Enter Payment Date");
                return false;
            }
            else if (rowCount == 0) {
                alert("No Claim to Save");
                return false;
            }
            else if (Checkbox_Validation() == false) {
                alert("Please Select Atleast One Item");
                return false;
            }
            else {
               return confirm('Are You Sure?');
            }
        }
        function Checkbox_Validation() {
            var flag = 0;
            var gv = document.getElementById('<%=dgvClaim.ClientID%>');
            var arr = gv.getElementsByTagName("input");
            for (var i = 0; i < arr.length; i++) {
                if (arr[i].type == 'checkbox' && arr[i].checked == true) {
                    flag = 1;
                    break;
                }
            }

            if (flag == 0) {
                return false;
            }
            else {
                return true;
            }
        }   
        
        
        function ReadOnlyTextbox(Obj) {
            var row = Obj.parentNode.parentNode;
            var rowIndex = row.rowIndex - 1;
            var txt=row.cells[6].getElementsByTagName("input")[0];
            
            if (Obj.checked == true)
            {
                txt.value=row.cells[5].getElementsByTagName("span")[0].innerHTML;
            }
            else 
            {
                txt.value='0';
            }
            TotalAmount();
            return false;
        }
        
        function TotalAmount()
        {
        var amt=0;
        var gv = document.getElementById('<%=dgvClaim.ClientID%>');
            var arr = gv.getElementsByTagName("input");
            for (var i = 0; i < arr.length; i++) {
                if (arr[i].type == 'text') {
                  amt = amt + parseFloat(arr[i].value);  
                }
            }
        document.getElementById('<%=txtTotalAmount.ClientID%>').value= amt.toFixed(2);   
        }
    </script>

    <style type="text/css">
        .text
        {
            font-family: Helvetica;
            font-size: 13px;
            font-weight: normal;
            padding: 7px 0 0 15px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ToolkitScriptManager ID="toolScript1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="title">
        <h5>
            Expense Reimbursement</h5>
    </div>
    <asp:UpdatePanel ID="UP1" runat="server">
        <ContentTemplate>
            <uc3:Message ID="Message" runat="server" />
            <br />
            <h6 align="left" style="color: #00356A;">
                Payment Information</h6>
            <table width="97%" align="center" class="table">
                <tr>
                    <td width="10%" align="left" class="label">
                        Cash/Bank Book:
                    </td>
                    <td align="left" width="20%">
                        <asp:ComboBox ID="ddlCashBankLedger" runat="server" CssClass="WindowsStyle" AutoPostBack="true"
                            DropDownStyle="DropDown" AutoCompleteMode="SuggestAppend" CaseSensitive="false"
                            Width="200px" DataValueField="LedgerID" DataTextField="LedgerName" OnSelectedIndexChanged="ddlCashBankLedger_SelectedIndexChanged">
                        </asp:ComboBox>
                    </td>
                    <td align="left" class="label" width="10%">
                        Mode Of Payment :
                    </td>
                    <td align="left" width="20%">
                        <asp:DropDownList ID="ddlPaymentMode" runat="server" CssClass="dropdownList" Width="120px"
                            AutoPostBack="true" OnSelectedIndexChanged="ddlPaymentMode_SelectedIndexChanged">
                            <asp:ListItem Value="CASH" Text="CASH"></asp:ListItem>
                            <asp:ListItem Value="CHEQUE" Text="CHEQUE"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td align="left" class="label" width="10%">
                        Ch. No :
                    </td>
                    <td align="left" width="20%">
                        <asp:TextBox ID="txtChequeNo" runat="server" CssClass="textbox" Width="120px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td width="10%">
                    </td>
                    <td align="left" colspan="5" class="text">
                        <asp:Literal ID="ltrLedgerBalance" runat="server" Mode="PassThrough"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td width="10%" align="left" class="label">
                        Reimbursement Ledger:
                    </td>
                    <td align="left" width="20%">
                        <asp:ComboBox ID="ddlClaimLedger" runat="server" CssClass="WindowsStyle" AutoPostBack="false"
                            DropDownStyle="DropDown" AutoCompleteMode="SuggestAppend" CaseSensitive="false"
                            Width="200px" DataValueField="LedgerID" DataTextField="LedgerName">
                        </asp:ComboBox>
                    </td>
                    <td align="left" class="label" width="10%">
                        Drawn On :
                    </td>
                    <td align="left" width="20%">
                        <asp:TextBox ID="txtDrawnOn" runat="server" CssClass="textbox" Width="120px"></asp:TextBox>
                    </td>
                    <td align="left" class="label" width="10%">
                        Ch. Date :
                    </td>
                    <td align="left" width="20%">
                        <asp:TextBox ID="txtChequeDate" runat="server" CssClass="textbox" Width="120px">
                        </asp:TextBox>
                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" CssClass="cal_Theme1"
                            PopupPosition="BottomRight" Format="dd MMM yyyy" TargetControlID="txtChequeDate"
                            OnClientDateSelectionChanged="" Enabled="True">
                        </asp:CalendarExtender>
                    </td>
                </tr>
                <tr>
                    <td width="10%" align="left" class="label">
                        Payment Date:
                    </td>
                    <td align="left" colspan="5" style="padding-top:10px;">
                        <asp:TextBox ID="txtPaymentDate" runat="server" CssClass="textbox_pink" Width="120px">
                        </asp:TextBox>
                        <asp:CalendarExtender ID="CalendarExtender2" runat="server" CssClass="cal_Theme1"
                            PopupPosition="BottomRight" Format="dd MMM yyyy" TargetControlID="txtPaymentDate"
                            OnClientDateSelectionChanged="" Enabled="True">
                        </asp:CalendarExtender>
                    </td>
                </tr>
            </table>
            <br />
            <h6 align="left" style="color: #00356A;">
                Claim Information</h6>
            <table width="97%" align="center" class="table">
                <tr>
                    <td align="center" colspan="2">
                        <asp:GridView ID="dgvClaim" runat="server" AutoGenerateColumns="false" Width="100%"
                            AllowPaging="false" GridLines="None" DataKeyNames="ExpenseClaimId" OnRowDataBound="dgvClaim_RowDataBound">
                            <Columns>
                                <asp:TemplateField HeaderText="">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="ChkSelect" runat="server" Checked="true" onclick="ReadOnlyTextbox(this)" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="ClaimNo" HeaderText="Claim No" />
                                <asp:BoundField DataField="EmpCode" HeaderText="Emp. Code" />
                                <asp:BoundField DataField="EmployeeName" HeaderText="Emp. Name" />
                                <asp:BoundField DataField="CreatedOn" HeaderText="Claim Date" DataFormatString="{0:dd/MM/yyyy}" />
                                <asp:TemplateField HeaderText="Expense Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lblExpenseAmt" runat="server" Text='<%#Bind("ExpenseAmount") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Amount">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtAmount" runat="server" CssClass="textbox_disabled" Width="70px"
                                            Enabled="false" Text='<%#Bind("ExpenseAmount") %>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>
                                <table style="height: 10px; width: 100%;">
                                    <tr align="left" class="HeaderStyle">
                                        <th scope="col">
                                            No Records Found
                                        </th>
                                    </tr>
                                    <tr class="RowStyle">
                                        <td>
                                            Sorry! No Records Found.
                                        </td>
                                </table>
                            </EmptyDataTemplate>
                            <HeaderStyle CssClass="HeaderStyle" />
                            <RowStyle CssClass="RowStyle" />
                            <AlternatingRowStyle CssClass="AltRowStyle" />
                            <PagerStyle CssClass="PagerStyle" />
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td align="right" class="label" width="84%">
                        Total Amount :
                    </td>
                    <td align="left" width="16%">
                        <asp:TextBox ID="txtTotalAmount" runat="server" CssClass="textbox_disabled" Enabled="false"
                            Width="100px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <br />
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="center">
                        <asp:Button ID="btnSave" runat="server" CssClass="button" Text="Save Payment" OnClientClick="return Validation()"
                            OnClick="btnSave_Click" />
                        &nbsp;
                        <asp:Button ID="btnPrint" runat="server" CssClass="button" Text="Print" />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
