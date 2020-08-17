<%@ Page Language="C#" MasterPageFile="~/MasterAdmin.Master" AutoEventWireup="true" CodeBehind="DirectorLeaveApproval.aspx.cs" Inherits="CollegeERP.HR.DirectorLeaveApproval" Title="Director Leave Approval" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register src="../UserControl/Message.ascx" tagname="Message" tagprefix="uc3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
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
    
    function Validation()
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
        
        
    function CheckAll(checkbox) {

            var gv = document.getElementById('<%=dgvLeave.ClientID%>');
            var arr = gv.getElementsByTagName("input");
            for (var i = 0; i < arr.length; i++) {
                if (arr[i].type == 'checkbox') {
                    if (checkbox.checked == true) {
                        arr[i].checked = true;

                    }
                    else {
                        arr[i].checked = false;

                    }
                }
            }

        } 
        
     function ApprovalValidation() {
            var gv = document.getElementById('<%=dgvLeave.ClientID%>');
            var rowCount = gv.rows.length - 1;
            if (rowCount == 0) {
                alert("No Request to Save");
                return false;
            }
            else if (Checkbox_Validation() == false) {
                alert("Please Select Leave Request");
                return false;
            }
            else {
               return confirm('Are You Sure?');
            }
        }
        function Checkbox_Validation() {
            var flag = 0;
            var gv = document.getElementById('<%=dgvLeave.ClientID%>');
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
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ToolkitScriptManager ID="toolScript1" runat="server"></asp:ToolkitScriptManager>

    <div class="title">
		<h5>Director Leave Approval</h5>
    </div>
<asp:UpdateProgress ID="UProgress1" runat="server" AssociatedUpdatePanelID="UP1">
    <ProgressTemplate>
       <div class="overlay">
          <img style="top:50%; position:relative;" src="../Images/ajax-loader.gif" />
       </div>
   </ProgressTemplate>
</asp:UpdateProgress>    
<asp:UpdatePanel ID="UP1" runat="server">
    <ContentTemplate>       
       
            <uc3:Message ID="Message" runat="server" />
            <br />
            <table width="95%" align="center" class="table">
                   <tr>
                        <td align="left" width="5%" class="label">From</td>
                        <td align="left" width="12%">
                            <asp:TextBox ID="txtFromDate" runat="server" CssClass="textbox" Width="110px"></asp:TextBox>
                            <asp:CalendarExtender ID="CalendarExtender3" runat="server" CssClass="cal_Theme1"
                                PopupPosition="BottomRight" Format="dd/MM/yyyy" TargetControlID="txtFromDate" OnClientDateSelectionChanged="" Enabled="True">
                            </asp:CalendarExtender>
                        </td>
                        <td align="left" width="5%" class="label">To</td>
                        <td align="left" width="12%">
                            <asp:TextBox ID="txtToDate" runat="server" CssClass="textbox" Width="110px"></asp:TextBox>
                            <asp:CalendarExtender ID="CalendarExtender4" runat="server" CssClass="cal_Theme1"
                                PopupPosition="BottomRight" Format="dd/MM/yyyy" TargetControlID="txtToDate" OnClientDateSelectionChanged="" Enabled="True">
                            </asp:CalendarExtender>
                        </td>
                        <td align="left">
                            <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="button" 
                                OnClientClick="return Validation()" onclick="btnSearch_Click" />
                            &nbsp;
                            <asp:Button ID="btnApprove" runat="server" Text="Approve" CssClass="button" 
                                OnClientClick="return ApprovalValidation()" onclick="btnApprove_Click" />
                            &nbsp;
                            <asp:Button ID="btnReject" runat="server" Text="Reject" CssClass="button" 
                                OnClientClick="return ApprovalValidation()" onclick="btnReject_Click" />
                        </td>
                    </tr> 
                  </table>
                  <br />
                  <table width="95%" align="center" class="table">
                    <tr>
                        <td align="left">
                            <asp:CheckBox ID="ChkSelect" runat="server" Text="Select All" onclick="javascript:CheckAll(this);" />
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <asp:GridView ID="dgvLeave" runat="server" AutoGenerateColumns="false" 
                                 Width="100%" AllowPaging="false" GridLines="None" 
                                 DataKeyNames="LeaveId,LeaveTaken,LeaveBalance" 
                                onrowdatabound="dgvLeave_RowDataBound">
                            <Columns>
                                <asp:TemplateField HeaderText="">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="ChkSelect" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="EmpCode" HeaderText="Employee Code" />
                                <asp:BoundField DataField="FullName" HeaderText="Employee Name" />
                                <asp:BoundField DataField="LeaveTypeName" HeaderText="Leave Type" />
                                <asp:BoundField DataField="StartDate" HeaderText="From" DataFormatString="{0:dd/MM/yyyy}" />
                                <asp:BoundField DataField="EndDate" HeaderText="To" DataFormatString="{0:dd/MM/yyyy}"/>
                                <asp:BoundField DataField="NoOfDays" HeaderText="Days" />
                                <asp:BoundField DataField="Purpose" HeaderText="Reason" />
                                <asp:BoundField DataField="CreatedOn" HeaderText="Applied On" DataFormatString="{0:dd/MM/yyyy}" />    
                                <asp:BoundField DataField="LeaveManager" HeaderText="Leave Manager" />            
                                <asp:BoundField DataField="Comment" HeaderText="Manager Remarks" />
                                <asp:TemplateField HeaderText="">
                                    <ItemTemplate>
                                        <asp:Literal ID="ltrWarning" runat="server" Mode="PassThrough"></asp:Literal>
                                    </ItemTemplate>
                                </asp:TemplateField>
                        </Columns>
                        <EmptyDataTemplate>
                        <table style="height:10px;width:100%;">
			                  <tr align="left" class="HeaderStyle">
				              <th scope="col">No Records Found</th>
			                 </tr><tr class="RowStyle">
				             <td>Sorry! No Records Found.</td>
		                </table>
                    </EmptyDataTemplate>
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
