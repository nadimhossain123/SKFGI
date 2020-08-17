<%@ Page Language="C#" MasterPageFile="~/MasterAdmin.Master" AutoEventWireup="true" CodeBehind="ApplyClaim.aspx.cs" Inherits="CollegeERP.HR.ApplyClaim" Title="Apply Claim" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register src="../UserControl/Message.ascx" tagname="Message" tagprefix="uc3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function Validation()
        {
            if (document.getElementById('<%=txtTitle.ClientID%>').value == '')
            {
                alert("Enter Claim Title");
                return false;
            }
            else if (document.getElementById('<%=ddlExpenseType.ClientID%>').selectedIndex == 0)
            {
                alert("Select Expense Type");
                return false;
            }
            else if (document.getElementById('<%=txtDescription.ClientID%>').value == '')
            {
                alert("Enter Claim Description");
                return false;
            }
            else if (isNumber(document.getElementById('<%=txtExpAmount.ClientID%>').value) == false || parseFloat(document.getElementById('<%=txtExpAmount.ClientID%>').value) == 0) {
                alert("Enter a Valid Expense Amount");
                return false;
            }
            else if (isValidDate(document.getElementById('<%=txtExpDate.ClientID%>').value) == false) {
                alert("Enter Expense Date in DD/MM/YYYY format");
                return false;
            }
            else {return true;}
        }
        
        function isValidDate(s) {
        // format D(D)/M(M)/(YY)YY
        var dateFormat = /^\d{1,4}[\.|\/|-]\d{1,2}[\.|\/|-]\d{1,4}$/;

        if (dateFormat.test(s)) {
            // remove any leading zeros from date values
            s = s.replace(/0*(\d*)/gi, "$1");
            var dateArray = s.split(/[\.|\/|-]/);

            // correct month value
            dateArray[1] = dateArray[1] - 1;

            // correct year value
            if (dateArray[2].length < 4) {
                // correct year value
                dateArray[2] = (parseInt(dateArray[2]) < 50) ? 2000 + parseInt(dateArray[2]) : 1900 + parseInt(dateArray[2]);
            }

            var testDate = new Date(dateArray[2], dateArray[1], dateArray[0]);
            if (testDate.getDate() != dateArray[0] || testDate.getMonth() != dateArray[1] || testDate.getFullYear() != dateArray[2]) {
                return false;
            } else {
                return true;
            }
        } else {
            return false;
        }
    }
    
    
    function isNumber(n) {
        return !isNaN(parseFloat(n)) && isFinite(n);
        }
    function SearchValidation()
        {
            var field = document.getElementById('<%=txtFromDate.ClientID%>');
            var field1 = document.getElementById('<%=txtToDate.ClientID%>');
                 if (field.value != '' && isValidDate(field.value) == false) {
                     alert('Enter From Date In DD/MM/YYYY Format');
                    return false;
            }
                else if (field1.value != '' && isValidDate(field1.value) == false) {
                    alert('Enter To Date In DD/MM/YYYY Format');
                     return false;
            }
            else { return true; }
        }    
   </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<asp:ToolkitScriptManager ID="toolScript1" runat="server"></asp:ToolkitScriptManager>

    <div class="title">
		<h5>Expense Claim</h5>
    </div>
<asp:UpdatePanel ID="UP1" runat="server">
    <ContentTemplate>
       <uc3:Message ID="Message" runat="server" />
       <br />
       <h6 align="left" style="color:#00356A;">Apply Claim</h6>
       <div style="width:740px;">    
           <table width="100%" align="center" class="table">
                <tr>
                      <td align="left" width="20%" class="label">Claim Title<span class="req">*</span></td>
                      <td align="left" width="30%">
                         <asp:TextBox ID="txtTitle" runat="server" CssClass="textbox_required" Width="160px"></asp:TextBox>
                      </td>
                      <td align="left" width="20%" class="label">Claim Type<span class="req">*</span></td>
                      <td align="left" width="30%">
                            <asp:DropDownList ID="ddlExpenseType" runat="server" CssClass="dropdownList" Width="170px" DataValueField="ExpenseTypeId" DataTextField="ExpenseTypeName"></asp:DropDownList>
                      </td>
               </tr>
               <tr>
                  <td align="left" width="20%" class="label">Claim Details<span class="req">*</span></td>
                  <td align="left" colspan="3">
                     <asp:TextBox ID="txtDescription" runat="server" CssClass="textbox_required" Width="530px" Height="35px" TextMode="MultiLine" Style="resize: none"></asp:TextBox>
                  </td>
                </tr>
                <tr>
                   <td align="left" width="20%" class="label">Expense Amount<span class="req">*</span></td>
                   <td align="left" width="30%">
                     <asp:TextBox ID="txtExpAmount" runat="server" CssClass="textbox_required" Width="160px"></asp:TextBox>
                     <asp:FilteredTextBoxExtender ID="ftb1" runat="server" FilterType="Custom" ValidChars="0123456789." TargetControlID="txtExpAmount"></asp:FilteredTextBoxExtender>
                   </td>
                   <td align="left" width="20%" class="label">Expense Date<span class="req">*</span></td>
                   <td align="left" width="30%">
                     <asp:TextBox ID="txtExpDate" runat="server" CssClass="textbox_required" Width="160px"></asp:TextBox>
                     <asp:CalendarExtender ID="CalendarExtender1" runat="server" CssClass="cal_Theme1"
                          PopupPosition="BottomRight" Format="dd/MM/yyyy" TargetControlID="txtExpDate" OnClientDateSelectionChanged="" Enabled="True">
                     </asp:CalendarExtender>
                   </td>
                </tr>
                <tr>
                  <td align="left" width="20%" class="label">Bill Submitted</td>
                  <td align="left" colspan="3">
                    <asp:CheckBox ID="ChkBillSubmitted" runat="server" />
                  </td>
                </tr>
           </table>
            <br />
            <br />
            <table>
                <tr>
                    <td align="center">
                        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="button" 
                                        OnClientClick="javascript:return Validation()" 
                            onclick="btnSave_Click" />&nbsp;
                        <asp:Button ID="btnCancel" runat="server" CssClass="button" Text="Reset" 
                            onclick="btnCancel_Click" />
                    </td>
               </tr>
            </table> 
       </div>
       <br />
       
       <h6 align="left" style="color:#00356A;">My Claim History</h6>
       
            <table width="95%" align="center" class="table">
                   <tr>
                        <td align="left" width="10%" class="label">Claim Status</td>
                        <td align="left" width="15%">
                            <asp:DropDownList ID="ddlStatus" runat="server" CssClass="dropdownList" Width="140px" DataTextField="ClaimStatus" DataValueField="ClaimStatusId"></asp:DropDownList>
                        </td>
                        <td align="left" width="10%" class="label">From</td>
                        <td align="left" width="15%">
                            <asp:TextBox ID="txtFromDate" runat="server" CssClass="textbox" Width="110px"></asp:TextBox>
                            <asp:CalendarExtender ID="CalendarExtender3" runat="server" CssClass="cal_Theme1"
                                PopupPosition="BottomRight" Format="dd/MM/yyyy" TargetControlID="txtFromDate" OnClientDateSelectionChanged="" Enabled="True">
                            </asp:CalendarExtender>
                        </td>
                        <td align="left" width="10%" class="label">To</td>
                        <td align="left" width="15%">
                            <asp:TextBox ID="txtToDate" runat="server" CssClass="textbox" Width="110px"></asp:TextBox>
                            <asp:CalendarExtender ID="CalendarExtender4" runat="server" CssClass="cal_Theme1"
                                PopupPosition="BottomRight" Format="dd/MM/yyyy" TargetControlID="txtToDate" OnClientDateSelectionChanged="" Enabled="True">
                            </asp:CalendarExtender>
                        </td>
                        <td align="left" class="label">
                            <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="button" 
                                OnClientClick="return SearchValidation()" onclick="btnSearch_Click"  />
                        </td>
                    </tr> 
                  </table>
           <br />    
           <table width="95%" align="center" class="table">
                <tr>
                    <td align="center">
                        <asp:GridView ID="dgvClaim" runat="server" AutoGenerateColumns="false" Width="100%" AllowPaging="false" DataKeyNames="ExpenseClaimId">
                            <Columns>
                                <asp:TemplateField HeaderText="Claim No">
                                    <ItemTemplate>
                                        <br />
                                        <b><u><%# DataBinder.Eval(Container.DataItem, "ClaimNo")%></u></b><br />
                                        <div style="padding-top:5px;">
                                          <%# DataBinder.Eval(Container.DataItem, "ExpenseTypeName")%>
                                        </div>  
                                        <br /><br />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Claim Details">
                                    <ItemTemplate>
                                        <br />
                                        <b><u><%# DataBinder.Eval(Container.DataItem, "ClaimTitle")%></u></b><br />
                                        <div style="padding-top:5px;">
                                          <%# DataBinder.Eval(Container.DataItem, "ClaimDescription")%>
                                        </div>  
                                        <br /><br />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="ExpenseAmount" HeaderText="Claim Amount" />
                                <asp:BoundField DataField="ExpenseDate" HeaderText="Expense Date" DataFormatString="{0:dd/MM/yyyy}" />
                                <asp:BoundField DataField="CreatedOn" HeaderText="Claim Date" DataFormatString="{0:dd/MM/yyyy}" />
                                <asp:BoundField DataField="ClaimApprover" HeaderText="Approver" />
                                <asp:BoundField DataField="ClaimStatus" HeaderText="Approver Status" />
                                <asp:BoundField DataField="ApproverComment" HeaderText="Remarks" />
                                <asp:BoundField DataField="IsDirectorApproved" HeaderText="Director Approval" />
                            </Columns>
                            <HeaderStyle CssClass="HeaderStyle"  />
	                        <RowStyle CssClass="RowStyle" />
	                        <EmptyDataRowStyle CssClass="EditRowStyle" />
	                        <AlternatingRowStyle CssClass="AltRowStyle" />
	                        <PagerStyle CssClass="PagerStyle" />
                        </asp:GridView>
                    </td>
                </tr>
           </table>
         <br />
         <br />
         <br />         
    </ContentTemplate>
</asp:UpdatePanel>       
</asp:Content>
