<%@ Page Language="C#" MasterPageFile="~/MasterAdmin.Master" AutoEventWireup="true"
    CodeBehind="StudentJournalTran.aspx.cs" Inherits="CollegeERP.Accounts.StudentJournalTran"
    Title="Student Journal" %>

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
                return ShowMsg("No Payment To Save");
            else if (parseFloat(document.getElementById('<%=txtTotalAmt.ClientID%>').value) == 0) 
                return ShowMsg("Zero Amount is Not Allowed");
            else if (parseFloat(document.getElementById('<%=txtAmount.ClientID%>').value) == 0) 
                return ShowMsg("Please Enter Amount");
            else 
               return confirm('Do you want to save?');
        }
        
    function ShowMsg(str)
    {
        alert(str);
        return false;
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
            var txtBox=rowElement.cells[4].getElementsByTagName("input")[0];
                amt = amt + ((txtBox.value == '') ? 0 : parseFloat(txtBox.value));
        }
        document.getElementById('<%=txtTotalAmt.ClientID%>').value= amt.toFixed(2); 
        document.getElementById('<%=txtAmount.ClientID%>').value= amt.toFixed(2);   
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
                var txtBox=rowElement.cells[4].getElementsByTagName("input")[0].focus();                
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
     function changeTab() {
            var tabBehavior = $get('<%=TabContainer1.ClientID%>').control;
            //Set the Currently Visible Tab 
            tabBehavior.set_activeTabIndex(0);
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
            Student Journal</h5>
    </div>
     <asp:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" CssClass="ajax__tab_orange-theme">
        <asp:TabPanel ID="TabPanel2" runat="server">
            <ContentTemplate>
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
                                  <input id="hdnJournalID" type="hidden" value="0" runat="server" />
                                        <input id="hdndtlid" type="hidden" value="0" runat="server" />
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
                            Semester<span class="req">*</span>
                        </td>
                        <td align="left" width="20%">
                            <asp:DropDownList ID="ddlSemester" runat="server" CssClass="dropdownList" 
                                Width="120px">
                                <asp:ListItem Text="---SELECT---" Value="0"></asp:ListItem>
                                <asp:ListItem Text="SEM 1" Value="1"></asp:ListItem>
                                <asp:ListItem Text="SEM 2" Value="2"></asp:ListItem>
                                <asp:ListItem Text="SEM 3" Value="3"></asp:ListItem>
                                <asp:ListItem Text="SEM 4" Value="4"></asp:ListItem>
                                <asp:ListItem Text="SEM 5" Value="5"></asp:ListItem>
                                <asp:ListItem Text="SEM 6" Value="6"></asp:ListItem>
                                <asp:ListItem Text="SEM 7" Value="7"></asp:ListItem>
                                <asp:ListItem Text="SEM 8" Value="8"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" class="label" width="10%">
                             Ledger :
                        </td>
                        <td align="left" width="20%">
                            <asp:ComboBox ID="ddlCashBankLedger" runat="server" CssClass="WindowsStyle" DropDownStyle="DropDown"
                                AutoCompleteMode="SuggestAppend" CaseSensitive="false" Width="170px" DataValueField="LedgerID"
                                DataTextField="LedgerName" AutoPostBack="true" OnSelectedIndexChanged="ddlCashBankLedger_SelectedIndexChanged">
                            </asp:ComboBox>
                        </td>
                        <td align="left" class="label" width="10%">
                            Dr / Cr</td>
                        <td align="left" width="20%">
                            <asp:DropDownList ID="ddlDrCr" runat="server" CssClass="dropdownList" Width="50px"
                                        AutoPostBack="true" OnSelectedIndexChanged="ddlDrCr_SelectedIndexChanged">
                                        <asp:ListItem Value="DR" Text="DR"></asp:ListItem>
                                        <asp:ListItem Value="CR" Text="CR"></asp:ListItem>
                                    </asp:DropDownList>
                            </td>
                        <td align="left" class="label" width="10%">
                             Amount<span class="req">*</span></td>
                        <td align="left" width="20%">
                            <asp:TextBox ID="txtAmount" runat="server" CssClass="textbox_required" Width="120px">
                            </asp:TextBox></td>
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
                            &nbsp;</td>
                        <td align="left" width="20%" style="padding-top: 10px;">
                            &nbsp;</td>
                        <td width="10%" align="left" class="label">
                            &nbsp;</td>
                        <td align="left">
                            &nbsp;</td>
                        <td align="left" width="10%" class="label">
                            &nbsp;</td>
                        <td align="left">
                            &nbsp;</td>
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
                        &nbsp
                        <br />
                        <asp:Literal ID="ltrDrCr" runat="server" Mode="PassThrough"></asp:Literal>
                        <asp:Label runat="server" ID="lblDropOut" Font-Bold="true" ForeColor="Red"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:Button ID="btnSearch" runat="server" CssClass="button" Text="Get Unpaid List"
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
                            AllowPaging="false" DataKeyNames="id,AssestLedgerID_FK" 
                            onrowdatabound="dgvFeesHead_RowDataBound" 
                            onrowcommand="dgvFeesHead_RowCommand">
                            <Columns>
                                <asp:BoundField DataField="fees" HeaderText="Fees Head" />
                                <asp:BoundField DataField="BillAmount" HeaderText="Bill" DataFormatString="{0:F2}"
                                    ItemStyle-HorizontalAlign="Right" ItemStyle-Width="90px" />
                                <asp:BoundField DataField="Due" HeaderText="Due" DataFormatString="{0:F2}" ItemStyle-HorizontalAlign="Right"
                                    ItemStyle-Width="90px" />
                                <asp:BoundField DataField="Advance" HeaderText="Advance" DataFormatString="{0:F2}"
                                    ItemStyle-HorizontalAlign="Right" ItemStyle-Width="90px" />
                                <asp:TemplateField HeaderText="Amount" ItemStyle-Width="90px">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtAmount" runat="server" CssClass="textbox_green" Text="0.00" onkeyup="TotalAmount()"
                                            onkeypress="return AmountOnly('txtAmount',this);"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>
                                <table style="height: 10px; width: 100%;">
                                    <tr align="left" class="HeaderStyle">
                                        <th scope="col">
                                            
                                        </th>
                                    </tr>
                                    <tr class="RowStyle">
                                        <td>
                                            Sorry! No Student Found.
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
    </ContentTemplate>
            <HeaderTemplate>
                <strong>Student Journal Information Entry</strong>
            </HeaderTemplate>
        </asp:TabPanel>
        <asp:TabPanel ID="TabPanel1" runat="server" HeaderText="TabPanel1">
            <ContentTemplate>
            <asp:UpdatePanel runat="server" ID="UpdatePanelVoucherSearch">
                    <ContentTemplate>
                        <table width="95%" align="center" class="table">
                            <tbody>
                                <tr>
                                    <td valign="top">
                                        <table width="100%">
                                            <tbody>
                                                <tr>
                                                    <td class="label" valign="middle" align="right">
                                                        Voucher No. :
                                                    </td>
                                                    <td valign="middle" align="left">
                                                        <asp:TextBox ID="txtVchNoSearch" runat="server" CssClass="textbox" Width="180px"></asp:TextBox>
                                                        <input id="Hidden1" type="hidden" value="0" runat="server" />
                                                    </td>
                                                    <td class="label" valign="middle" align="right">
                                                        Voucher Date :
                                                    </td>
                                                    <td valign="middle" align="left">
                                                        <asp:TextBox ID="txtVchDateSearch" runat="server" CssClass="textbox" Width="120px"></asp:TextBox>
                                                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" CssClass="cal_Theme1"
                                                            PopupPosition="BottomRight" Format="dd MMM yyyy" TargetControlID="txtVchDateSearch"
                                                            OnClientDateSelectionChanged="" Enabled="True">
                                                        </asp:CalendarExtender>
                                                    </td>
                                                    <td class="label" valign="middle" align="right">
                                                        To :
                                                    </td>
                                                    <td valign="middle" align="left">
                                                        <asp:TextBox ID="txtVchDateSearchTo" runat="server" CssClass="textbox" Width="120px"></asp:TextBox>
                                                        <asp:CalendarExtender ID="CalendarExtender3" runat="server" CssClass="cal_Theme1"
                                                            PopupPosition="BottomRight" Format="dd MMM yyyy" TargetControlID="txtVchDateSearchTo"
                                                            OnClientDateSelectionChanged="" Enabled="True">
                                                        </asp:CalendarExtender>
                                                    </td>
                                                    <td valign="middle" align="left">
                                                        <asp:Button ID="btnSearchJV" OnClick="btnSearchJV_Click" runat="server" Text="Search"
                                                            CssClass="button"></asp:Button>
                                                    </td>
                                                    <td>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <asp:GridView ID="grdvwtrnsctnsearch" runat="server" Width="100%" AllowPaging="false"
                                            GridLines="None" AllowSorting="false" OnRowCommand="grdvwtrnsctnsearch_RowCommand"
                                            OnRowDataBound="grdvwtrnsctnsearch_RowDataBound" AutoGenerateColumns="False"
                                            CellPadding="0">
                                            <Columns>
                                                <asp:BoundField DataField="JVoucherNo" HeaderText="Voucher No."></asp:BoundField>
                                                <asp:BoundField HtmlEncode="False" DataFormatString="{0:dd MMM yyyy}" DataField="JVoucherDate"
                                                    HeaderText="Voucher Date"></asp:BoundField>
                                                <asp:BoundField HtmlEncode="False" DataField="JVNarration" HeaderText="Narration">
                                                </asp:BoundField>
                                                <asp:BoundField DataFormatString="{0:F2}" DataField="DRAmount" HeaderText="Amount DR">
                                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataFormatString="{0:F2}" DataField="CRAmount" HeaderText="Amount CR">
                                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="imgbtneditsearch" CommandName="imgbtneditsearch" CommandArgument='<%# Eval("JVHeaderID")%>'
                                                            ImageUrl="~/Images/edit_icon.gif" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="imgbtndeletesearch" CommandName="imgbtndeletesearch" ImageUrl="~/Images/delete_icon.gif" CommandArgument='<%# Eval("JVHeaderID")%>'
                                                            runat="server" OnClientClick="return confirm('Do you want to delete this voucher?')" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="btnPrint" runat="server" ImageUrl="~/Images/print.gif" Width="17px"
                                                            Height="17px" />
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
                            </tbody>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </ContentTemplate>
            <HeaderTemplate>
                <strong>Show Student Journal Information</strong>
            </HeaderTemplate>
        </asp:TabPanel>
    </asp:TabContainer>
</asp:Content>
