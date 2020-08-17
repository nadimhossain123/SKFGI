<%@ Page Language="C#" MasterPageFile="~/MasterAdmin.Master" AutoEventWireup="true"
    CodeBehind="StudentCautionMoneyRefund.aspx.cs" Inherits="CollegeERP.Accounts.StudentCautionMoneyRefund"
    Title="Student Caution Money Refund" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../UserControl/Message.ascx" TagName="Message" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">
               
        function Validation() {
            var gv = document.getElementById('<%=dgvFeesHead.ClientID%>');
            var rowCount = gv.rows.length - 1;
            
            if (document.getElementById('<%=txtVoucherDate.ClientID%>').value == '') 
                return ShowMsg("Please Enter Voucher Date");
            else if (document.getElementById('<%=ddlSemester.ClientID%>').selectedIndex == 0) 
                return ShowMsg("Please Select Semester");    
            else if (rowCount == 0) 
                return ShowMsg("No Advance To Refund");
            else if (parseFloat(document.getElementById('<%=txtTotalAmt.ClientID%>').value) == 0) 
                return ShowMsg("Zero Amount is Not Allowed");
            //else if (document.getElementById('<%=txtFeesBookNo.ClientID%>').value == '')
            //    return ShowMsg("Please Enter Fees Book Number");
            else 
               return confirm('Do you want to save?');
        }
        
    function ShowMsg(str)
    {
        alert(str);
        return false;
    } 
    
     function CheckAmount(Obj)
    {
    
       
        //***************************************
       var row = Obj.parentNode.parentNode;
       var rowIndex = row.rowIndex - 1;
       var PaidBefore=row.cells[2].innerHTML;
       var TotalRefundAmt=row.cells[1].innerHTML;
       var TotalProjPaid=TotalRefundAmt-PaidBefore;//**Total Projected Paid Amount
       var Amount=(Obj.value == '') ? '0' : Obj.value;     
       if (parseFloat(Amount) > parseFloat(TotalProjPaid))
        {
          alert("Amout Should Not Be More Than Refund Amount");
          Obj.focus();
        }     
        TotalAmount();       
     }
        
    function TotalAmount()
    {
        var amt=0;
        var gv = document.getElementById('<%=dgvFeesHead.ClientID%>');
        var rCount = gv.rows.length;
        var rowIdx=1;
       
        for (rowIdx; rowIdx <= rCount-1; rowIdx ++)
        {
          var rowElement = gv.rows[rowIdx];
          var txtBox=rowElement.cells[3].getElementsByTagName("input")[0];
          amt = amt + ((txtBox.value == '') ? 0 : parseFloat(txtBox.value));      
        }
        
        document.getElementById('<%=txtTotalAmt.ClientID%>').value= amt.toFixed(2);   
    }
    
     function moveEnter(rowIndex)
        {
        if ((event.which && event.which == 13) || (event.keyCode && event.keyCode == 13))
        {            
            var gv = document.getElementById('<%=dgvFeesHead.ClientID%>');
            var rCount = gv.rows.length;
            
            if (rowIndex <= rCount)
            {
                var rowElement = gv.rows[parseInt(rowIndex + 1)];
                var txtBox=rowElement.cells[3].getElementsByTagName("input")[0].focus();                
            }
          event.preventDefault();         
        }  
            TotalAmount();     
        }
    
    function openPopup(poplocation, querystring, popheight, popwidth,poptop, popleft)
        {                
            var popposition='left = 200, top=15, width=950,align=center, height=640,menubar=no, scrollbars=yes, resizable=no';                
            var NewWindow = window.open(poplocation,'',popposition);                
            if (NewWindow.focus!=null){                        
            NewWindow.focus();                
        }        
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
            Student Caution Money Refund</h5>
    </div>
    <asp:UpdatePanel ID="UP1" runat="server">
        <ContentTemplate>
            <uc3:Message ID="Message" runat="server" />
            <br />
            <h6 align="left" style="color: #00356A;">
                Money Receipt Information</h6>
            <table width="97%" align="center" class="table">
                <tbody>
                    <tr>
                        <td align="left" class="label" width="10%">
                            Money Receipt No :
                        </td>
                        <td align="left" width="20%">
                            <asp:TextBox ID="txtReceiptNo" runat="server" CssClass="textbox_pink" Enabled="false"
                                Width="170px"></asp:TextBox>
                        </td>
                        <td align="left" class="label" width="10%">
                            Voucher Date :
                        </td>
                        <td align="left" width="20%">
                            <asp:TextBox ID="txtVoucherDate" runat="server" CssClass="textbox_pink" Width="120px">
                            </asp:TextBox>
                            <asp:CalendarExtender ID="CalendarExtender2" runat="server" CssClass="cal_Theme1"
                                PopupPosition="BottomRight" Format="dd MMM yyyy" TargetControlID="txtVoucherDate"
                                OnClientDateSelectionChanged="" Enabled="True">
                            </asp:CalendarExtender>
                        </td>
                        <td align="left" class="label" width="10%">
                            Mode Of Payment :
                        </td>
                        <td align="left" width="20%">
                            <asp:DropDownList ID="ddlReceiptMode" runat="server" CssClass="dropdownList" Width="120px"
                                AutoPostBack="true" OnSelectedIndexChanged="ddlReceiptMode_SelectedIndexChanged">
                                <asp:ListItem Value="CASH" Text="CASH"></asp:ListItem>
                                <asp:ListItem Value="CHEQUE" Text="CHEQUE"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" class="label" width="10%">
                            Cash/Bank Ledger :
                        </td>
                        <td align="left" width="20%">
                            <asp:ComboBox ID="ddlCashBankLedger" runat="server" CssClass="WindowsStyle" DropDownStyle="DropDown"
                                AutoCompleteMode="SuggestAppend" CaseSensitive="false" Width="170px" DataValueField="LedgerID"
                                DataTextField="LedgerName" AutoPostBack="true" OnSelectedIndexChanged="ddlCashBankLedger_SelectedIndexChanged">
                            </asp:ComboBox>
                        </td>
                        <td align="left" class="label" width="10%">
                            Ch. No :
                        </td>
                        <td align="left" width="20%">
                            <asp:TextBox ID="txtChequeNo" runat="server" CssClass="textbox" Width="120px" MaxLength="6"></asp:TextBox>
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
                        <td width="10%">
                        </td>
                        <td align="left" colspan="5" class="text">
                            <asp:Literal ID="ltrLedgerBalance" runat="server" Mode="PassThrough"></asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <td width="10%" align="left" class="label">
                            Fees Book No
                           <%-- <span class="req">*</span>--%>
                        </td>
                        <td align="left" width="20%" style="padding-top: 10px;">
                            <asp:TextBox ID="txtFeesBookNo" runat="server" CssClass="textbox" Width="120px"></asp:TextBox>
                        </td>
                        <td width="10%" align="left" class="label">
                            Drawn On :
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtDrawnOn" runat="server" CssClass="textbox" Width="120px"></asp:TextBox>
                        </td>
                        <td align="left" width="10%" class="label">
                            Semester<span class="req">*</span>
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="ddlSemester" runat="server" CssClass="dropdownList" Width="120px">
                                <asp:ListItem Value="0" Text="---SELECT---"></asp:ListItem>
                                <asp:ListItem Value="1" Text="SEM 1"></asp:ListItem>
                                <asp:ListItem Value="2" Text="SEM 2"></asp:ListItem>
                                <asp:ListItem Value="3" Text="SEM 3"></asp:ListItem>
                                <asp:ListItem Value="4" Text="SEM 4"></asp:ListItem>
                                <asp:ListItem Value="5" Text="SEM 5"></asp:ListItem>
                                <asp:ListItem Value="6" Text="SEM 6"></asp:ListItem>
                                <asp:ListItem Value="7" Text="SEM 7"></asp:ListItem>
                                <asp:ListItem Value="8" Text="SEM 8"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                </tbody>
            </table>
            <br />
            <h6 align="left" style="color: #00356A;">
                Student Information</h6>
            <table width="97%" align="center" class="table">
                <tr>
                    <td width="15%" align="left" class="label">
                      Batch :
                    </td>
                    <td align="left" class="style2">
                        <asp:DropDownList ID="ddlBatch" runat="server" Width="192px" CssClass="dropdownList"
                                    AutoPostBack="True" OnSelectedIndexChanged="ddlBatch_SelectedIndexChanged" DataValueField="id" DataTextField="batch_name">
                                </asp:DropDownList>
                    </td>
                    <td width="15%" align="left" class="label">
                        Course :
                    </td>
                    <td align="left" width="15%">
                    <asp:DropDownList ID="ddlCourse" runat="server" AutoPostBack="true" CssClass="dropdownList"
                        Width="130px" DataValueField="CourseId" DataTextField="CourseName" OnSelectedIndexChanged="ddlCourse_SelectedIndexChanged">
                    </asp:DropDownList>
                    </td>
                    <td width="15%" align="left" class="label">
                        Stream :
                    </td>
                    <td align="left" width="15%">
                    <asp:DropDownList ID="ddlStream" runat="server" CssClass="dropdownList" Width="130px"
                        DataTextField="stream_name" DataValueField="StreamId" AutoPostBack="true" 
                            onselectedindexchanged="ddlStream_SelectedIndexChanged">
                    </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td width="15%" align="left" class="label">
                        Student Name :
                    </td>
                    <td width="40%" align="left" colspan="3">
                        <asp:ComboBox ID="ddlStudent" runat="server" CssClass="WindowsStyle" AutoPostBack="True"
                            DropDownStyle="DropDown" AutoCompleteMode="SuggestAppend" CaseSensitive="false"
                            Width="340px" DataValueField="id" DataTextField="StudentName" 
                            onselectedindexchanged="ddlStudent_SelectedIndexChanged1">
                        </asp:ComboBox>
                        <asp:Label ID = "lblDropout" runat="server" Font-Bold="true" ForeColor="Red"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:Button ID="btnSearch" runat="server" CssClass="button" Text="Get Refund List"
                            OnClick="btnSearch_Click" />
                    </td>
                </tr>
            </table>
            <table width="97%" align="center">
                <tr>
                    <td align="center" width="15%" valign="top">
                        <asp:Image ID="ImgPhoto" runat="server" Width="120px" Height="130px" />
                    </td>
                    <td align="center" valign="top" colspan="2">
                        <asp:GridView ID="dgvFeesHead" runat="server" Width="100%" GridLines="None" AutoGenerateColumns="false"
                            AllowPaging="false" DataKeyNames="id,IncomeLedgerID_FK">
                            <Columns>
                                <asp:BoundField DataField="fees" HeaderText="Fees Head" />
                              <asp:BoundField DataField="RefundableAmount" HeaderText="Refundable Amount" DataFormatString="{0:F2}"
                                    ItemStyle-HorizontalAlign="Right" ItemStyle-Width="110px" />
                                <asp:BoundField DataField="RefundedAmount" HeaderText="College Paid" DataFormatString="{0:F2}"
                                    ItemStyle-HorizontalAlign="Right" ItemStyle-Width="90px" />
                                <asp:TemplateField HeaderText="Amount" ItemStyle-Width="90px">
                                    <ItemTemplate>
                                       <asp:TextBox ID="txtAmount" runat="server" CssClass="textbox_green" Text="0.00" onkeyup="CheckAmount(this)"
                                            onblur="CheckAmount(this)" onkeypress="return AmountOnly('txtAmount',this);"></asp:TextBox>
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
                                            Sorry! No Refundable fees.
                                        </td>
                                </table>
                            </EmptyDataTemplate>
                            <HeaderStyle CssClass="HeaderStyle" />
                            <RowStyle CssClass="RowStyle" />
                            <AlternatingRowStyle CssClass="AltRowStyle" />
                        </asp:GridView>
                    </td>
                </tr>
                <tr class="RowStyle">
                    <td width="15%">
                    </td>
                    <td align="left" class="label">
                        Total
                    </td>
                    <td align="left" width="98px">
                        <asp:TextBox ID="txtTotalAmt" runat="server" CssClass="textbox_yellow" Text="0.00"
                            Enabled="false"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <table width="97%" align="center" class="table">
                <tr>
                    <td align="left" width="15%" valign="top" class="label">
                        Narration:
                    </td>
                    <td align="left" width="80%">
                        <asp:TextBox ID="txtNarration" runat="server" TextMode="MultiLine" CssClass="textbox"
                            Width="950px" Height="40px" Style="resize: none;"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <br />
            <table align="center" class="table" width="60%">
                <tr>
                    <td align="center">
                        <asp:Button ID="btnSave" runat="server" CssClass="button" OnClick="btnSave_Click"
                            OnClientClick="return Validation()" Text="Save" />
                        &nbsp;
                        <asp:Button ID="btnCancel" runat="server" CssClass="button" OnClick="btnCancel_Click"
                            Text="Reset" />
                        &nbsp;
                        <asp:Button ID="btnPrint" runat="server" CssClass="button" Text="Print" />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
