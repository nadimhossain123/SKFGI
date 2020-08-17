<%@ Page Language="C#" MasterPageFile="~/MasterAdmin.Master" AutoEventWireup="true"
    CodeBehind="LibraryFine.aspx.cs" Inherits="CollegeERP.Student.LibraryFine" Title="Library Fine" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../UserControl/Message.ascx" TagName="Message" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">
        function Validation()
        {
            if (document.getElementById('<%=btnSave.ClientID%>').value == 'Save' && document.getElementById('<%=ddlFeesHead.ClientID%>').selectedIndex == 0)
            {
                alert("Select Fees Head");
                return false;
            }
            else if (document.getElementById('<%=ddlSemNo.ClientID%>').selectedIndex == 0)
            {
                alert('Select Sem No');
                return false;
            }
            else if (document.getElementById('<%=txtReason.ClientID%>').value == '') {
            alert("Enter Fine Reason");
            return false;
            }
            else if (document.getElementById('<%=txtAmount.ClientID%>').value == '' || parseFloat(document.getElementById('<%=txtAmount.ClientID%>').value) == 0) {
            alert("Enter a Valid Fine Amount");
            return false;
            }
            else {return confirm('Are You Confirm?');}
        }
        
        function ValidationSearch()
        {
            if (document.getElementById('<%=txtFrom.ClientID%>').value == '' || document.getElementById('<%=txtTo.ClientID%>').value == '')
            {
                alert("Enter Date Range");
                return false;
            }
            else {return true;}
        }
        
        function openpopup(poplocation, querystring, popheight, popwidth,poptop, popleft)
        {                
            var popposition='left = 200, top=20, width=950,align=center, height=650,menubar=no, scrollbars=yes, resizable=no';                
               
            var NewWindow = window.open(poplocation,'',popposition);                
            if (NewWindow.focus!=null){                        
            NewWindow.focus();                
            }   
        
          
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ToolkitScriptManager ID="toolScript1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="title">
        <h5>
            Library Fine</h5>
    </div>
    <asp:UpdatePanel ID="UP1" runat="server">
        <ContentTemplate>
            <uc3:Message ID="Message" runat="server" />
            <br />
            <h6 align="left" style="color: #00356A;">
                Fine Entry</h6>
            <div style="width: 780px;">
                <table width="100%" align="center" class="table">
                    <tr>
                        <td align="left" width="20%" class="label">
                            Student Name
                        </td>
                        <td align="left" colspan="3" valign="top">
                            <asp:ComboBox ID="ddlStudent" runat="server" CssClass="WindowsStyle" AutoPostBack="true"
                                DropDownStyle="DropDown" AutoCompleteMode="SuggestAppend" CaseSensitive="false"
                                Width="340px" DataValueField="id" DataTextField="StudentName" OnSelectedIndexChanged="ddlStudent_SelectedIndexChanged">
                            </asp:ComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td align="left" width="20%" class="label">
                            Semester
                        </td>
                        <td align="left" colspan="3" valign="top">
                            <asp:DropDownList ID="ddlSemNo" runat="server" CssClass="dropdownList" Width="140px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" width="20%" class="label">
                            Fees Head
                        </td>
                        <td align="left" colspan="3" valign="top">
                            <asp:DropDownList ID="ddlFeesHead" runat="server" CssClass="dropdownList" Width="150px"
                                DataValueField="id" DataTextField="fees">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" width="20%" class="label">
                            Reason<span class="req">*</span>
                        </td>
                        <td align="left" colspan="3">
                            <asp:TextBox ID="txtReason" runat="server" CssClass="textbox_required" Width="510px"
                                Height="35px" TextMode="MultiLine" Style="resize: none"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" width="20%" class="label">
                            Amount<span class="req">*</span>
                        </td>
                        <td align="left" width="30%">
                            <asp:TextBox ID="txtAmount" runat="server" CssClass="textbox_required" Width="140px"></asp:TextBox>
                            <asp:FilteredTextBoxExtender ID="ftb1" runat="server" FilterType="Custom" ValidChars="0123456789."
                                TargetControlID="txtAmount">
                            </asp:FilteredTextBoxExtender>
                        </td>
                        <td align="left" width="20%" class="label">
                        </td>
                        <td align="left" width="30%">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td align="center" colspan="4">
                            <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="button" OnClientClick="javascript:return Validation()"
                                OnClick="btnSave_Click" />&nbsp;
                            <asp:Button ID="btnCancel" runat="server" CssClass="button" Text="Reset" OnClick="btnCancel_Click" />&nbsp;
                            <asp:Button ID="btnPrint" runat="server" CssClass="button" Text="Print" />
                        </td>
                    </tr>
                </table>
            </div>
            <h6 align="left" style="color: #00356A;">
                Fine List</h6>
            <br />
            <br />
            <table width="95%" align="center" class="table">
                <tr>
                    <td align="left" width="12%" class="label">
                        Voucher No
                    </td>
                    <td align="left" width="15%">
                        <asp:TextBox ID="txtVoucherNo" runat="server" CssClass="textbox" Width="140px"></asp:TextBox>
                    </td>
                    <td align="left" width="7%" class="label">
                        From
                    </td>
                    <td align="left" width="15%">
                        <asp:TextBox ID="txtFrom" runat="server" CssClass="textbox" Width="110px"></asp:TextBox>
                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" CssClass="cal_Theme1"
                            PopupPosition="BottomRight" Format="dd MMM yyyy" TargetControlID="txtFrom" OnClientDateSelectionChanged=""
                            Enabled="True">
                        </asp:CalendarExtender>
                    </td>
                    <td align="left" width="7%" class="label">
                        To
                    </td>
                    <td align="left" width="15%">
                        <asp:TextBox ID="txtTo" runat="server" CssClass="textbox" Width="110px"></asp:TextBox>
                        <asp:CalendarExtender ID="CalendarExtender2" runat="server" CssClass="cal_Theme1"
                            PopupPosition="BottomRight" Format="dd MMM yyyy" TargetControlID="txtTo" OnClientDateSelectionChanged=""
                            Enabled="True">
                        </asp:CalendarExtender>
                    </td>
                    <td align="left">
                        <asp:Button ID="btnSearchList" runat="server" CssClass="button" Text="Search" OnClientClick="return ValidationSearch()" OnClick="btnSearchList_Click" />
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="7">
                        <asp:GridView ID="dgvFine" runat="server" AutoGenerateColumns="false" Width="100%"
                            AllowPaging="false" DataKeyNames="LibraryFineId" OnRowDataBound="dgvFine_RowDataBound"
                            OnRowEditing="dgvFine_RowEditing" onrowcommand="dgvFine_RowCommand" 
                            onrowdeleting="dgvFine_RowDeleting">
                            <Columns>
                                <asp:BoundField DataField="VoucherNo" HeaderText="VoucherNo" />
                                <asp:BoundField DataField="student_code" HeaderText="Student Code" />
                                <asp:BoundField DataField="name" HeaderText="Student Name" />
                                <asp:BoundField DataField="SemNo" HeaderText="Semester" />
                                <asp:BoundField DataField="FineAmount" HeaderText="Amount" />
                                <asp:BoundField DataField="CreatedOn" HeaderText="Date" DataFormatString="{0:dd/MM/yyyy}" />
                                <asp:BoundField DataField="ReasonForFine" HeaderText="Reason" />
                                <asp:TemplateField ShowHeader="false" Visible="false">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit_icon.gif" CommandName="Edit" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnPrint" runat="server" ImageUrl="~/Images/print-icon.JPG"
                                            Width="20px" Height="20px" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnDelete" CommandName="Delete" runat="server" ImageUrl="~/Images/NO.JPG"
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
