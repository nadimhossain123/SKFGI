<%@ Page Language="C#" MasterPageFile="~/MasterAdmin.Master" AutoEventWireup="true" CodeBehind="ClaimList.aspx.cs" Inherits="CollegeERP.HR.ClaimList" Title="Claim Request" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register src="../UserControl/Message.ascx" tagname="Message" tagprefix="uc3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function Validation()
        {
            if (!checkforvaliddata(eval('document.forms[0].' + '<%=txtEmpName.ClientID%>'), "Employee Name", 1)) return false;
            if (!checkforvaliddata(eval('document.forms[0].' + '<%=ddlStatus.ClientID%>'), "Claim Status", 0)) return false;
            if (!checkforvaliddata(eval('document.forms[0].' + '<%=txtApproverComment.ClientID%>'), "Approver Comment", 1)) return false;
            return true;
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
    </script> 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ToolkitScriptManager ID="ToolScript1" runat="server"></asp:ToolkitScriptManager>

    <div class="title">
		<h5>Claim Requests</h5>
    </div>
 <asp:UpdatePanel ID="UP1" runat="server">
    <ContentTemplate> 
       <uc3:Message ID="Message" runat="server" />
       <br />
       <h6 align="left" style="color:#00356A;">Edit Claim</h6>
       <div style="width:740px;">
          <table width="100%" align="center" class="table">
                <tr>
                      <td align="left" width="20%" class="label">Employee Name</td>
                      <td align="left" width="30%">
                         <asp:TextBox ID="txtEmpName" runat="server" CssClass="textbox_disabled" Width="160px" Enabled="false"></asp:TextBox>
                      </td>
                      <td align="left" width="20%" class="label">Employee Code</td>
                      <td align="left" width="30%">
                           <asp:TextBox ID="txtEmpCode" runat="server" CssClass="textbox_disabled" Width="160px" Enabled="false"></asp:TextBox>
                      </td>
               </tr> 
               <tr>
                      <td align="left" width="20%" class="label">Claim Status<span class="req">*</span></td>
                      <td align="left" width="30%">
                           <asp:DropDownList ID="ddlStatus" runat="server" CssClass="dropdownList" Width="140px" DataTextField="ClaimStatus" DataValueField="ClaimStatusId"></asp:DropDownList>
                      </td>
                      <td align="left" width="20%" class="label">Bill Received</td>
                      <td align="left" width="30%">
                           <asp:CheckBox ID="ChkBillReceived" runat="server" />
                      </td>
               </tr>
               <tr>
                  <td align="left" width="20%" class="label">Comment<span class="req">*</span></td>
                  <td align="left" colspan="3">
                     <asp:TextBox ID="txtApproverComment" runat="server" CssClass="textbox_required" Width="530px" Height="35px" TextMode="MultiLine" Style="resize: none"></asp:TextBox>
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
                            onclick="btnSave_Click"  />&nbsp;
                        <asp:Button ID="btnCancel" runat="server" CssClass="button" Text="Reset" 
                            onclick="btnCancel_Click"  />
                    </td>
               </tr>
            </table>  
       </div>
        <br />
        <h6 align="left" style="color:#00356A;">Claim Request History</h6>
            <table width="95%" align="center" class="table">
                   <tr>
                        <td align="left" width="7%" class="label">First Name</td>
                        <td align="left" width="12%">
                            <asp:TextBox ID="txtFName" runat="server" CssClass="textbox" Width="110px"></asp:TextBox>
                        </td>
                        <td align="left" width="7%" class="label">Status</td>
                        <td align="left" width="12%">
                            <asp:DropDownList ID="ddlStatus_Search" runat="server" CssClass="dropdownList" Width="140px" DataTextField="ClaimStatus" DataValueField="ClaimStatusId"></asp:DropDownList>
                        </td>
                        <td align="left" width="7%" class="label">From</td>
                        <td align="left" width="12%">
                            <asp:TextBox ID="txtFromDate" runat="server" CssClass="textbox" Width="110px"></asp:TextBox>
                            <asp:CalendarExtender ID="CalendarExtender3" runat="server" CssClass="cal_Theme1"
                                PopupPosition="BottomRight" Format="dd/MM/yyyy" TargetControlID="txtFromDate" OnClientDateSelectionChanged="" Enabled="True">
                            </asp:CalendarExtender>
                        </td>
                        <td align="left" width="7%" class="label">To</td>
                        <td align="left" width="12%">
                            <asp:TextBox ID="txtToDate" runat="server" CssClass="textbox" Width="110px"></asp:TextBox>
                            <asp:CalendarExtender ID="CalendarExtender4" runat="server" CssClass="cal_Theme1"
                                PopupPosition="BottomRight" Format="dd/MM/yyyy" TargetControlID="txtToDate" OnClientDateSelectionChanged="" Enabled="True">
                            </asp:CalendarExtender>
                        </td>
                        <td align="left">
                            <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="button" 
                                OnClientClick="return SearchValidation()" onclick="btnSearch_Click" />
                        </td>
                    </tr> 
                  </table>
                  <br />
                  <table width="95%" align="center">
                    <tr>
                        <td align="center">
                            <asp:GridView ID="dgvClaim" runat="server" AutoGenerateColumns="false" 
                                 Width="100%" AllowPaging="false"
                                 DataKeyNames="ExpenseClaimId" onrowdatabound="dgvClaim_RowDataBound" 
                                onrowediting="dgvClaim_RowEditing">
                            <Columns>
                                <asp:TemplateField HeaderText="Employee Name">
                                    <ItemTemplate>
                                        <%# DataBinder.Eval(Container.DataItem, "FullName")%><br />
                                        (<%# DataBinder.Eval(Container.DataItem, "EmpCode")%>)
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Claim No">
                                    <ItemTemplate>
                                        <%# DataBinder.Eval(Container.DataItem, "ClaimNo")%><br />
                                        (<%# DataBinder.Eval(Container.DataItem, "ExpenseTypeName")%>)
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Claim Details">
                                    <ItemTemplate>
                                        <br />
                                        <b><%# DataBinder.Eval(Container.DataItem, "ClaimTitle")%></b><br />
                                        <%# DataBinder.Eval(Container.DataItem, "ClaimDescription")%><br /><br />
                                        <b>Bill Submitted: </b><%# DataBinder.Eval(Container.DataItem, "BillSubmitted")%><br />
                                        <b>Bill Received: </b><%# DataBinder.Eval(Container.DataItem, "BillReceived")%><br />
                                        <b>Claim Amount: </b><%# DataBinder.Eval(Container.DataItem, "ExpenseAmount")%><br />
                                        <b>Expense Date: </b><%# DataBinder.Eval(Container.DataItem, "ExpenseDate","{0:dd/MM/yyyy}")%><br /><br />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="CreatedOn" HeaderText="Claim Date" DataFormatString="{0:dd/MM/yyyy}" />
                                <asp:TemplateField HeaderText="Approver Status">
                                    <ItemTemplate>
                                        <asp:Literal ID="ltrStatus" runat="server" Mode="PassThrough" Text='<%#Bind("ClaimStatus") %>'></asp:Literal>
                                    </ItemTemplate>    
                                </asp:TemplateField>
                                <asp:BoundField DataField="ApproverComment" HeaderText="Remarks" />
                                <asp:BoundField DataField="IsDirectorApproved" HeaderText="Director Approval" />
                                <asp:TemplateField ShowHeader="false">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit_icon.gif" CommandName="Edit" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                        
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
      
    </ContentTemplate>
</asp:UpdatePanel>      
</asp:Content>
