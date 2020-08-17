<%@ Page Language="C#" MasterPageFile="~/MasterAdmin.Master" AutoEventWireup="true" CodeBehind="LeaveList.aspx.cs" Inherits="CollegeERP.HR.LeaveList" Title="Leave Request" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script type="text/javascript">
    function openpopup(poplocation, querystring, popheight, popwidth,poptop, popleft)
     {                
        var popposition='left = 200, top=20, width=950,align=center, height=640,menubar=no, scrollbars=yes, resizable=no';                
               
        var NewWindow = window.open(poplocation,'',popposition);                
        if (NewWindow.focus!=null){                        
        NewWindow.focus();                
        }        
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
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ToolkitScriptManager ID="toolScript1" runat="server"></asp:ToolkitScriptManager>

    <div class="title">
		<h5>Leave Requests</h5>
    </div>
<asp:UpdatePanel ID="UP1" runat="server">
    <ContentTemplate>       
       
            <table width="95%" align="center" class="table">
                   <tr>
                        <td align="center" width="7%" class="label">First Name</td>
                        <td align="left" width="12%">
                            <asp:TextBox ID="txtFName" runat="server" CssClass="textbox" Width="110px"></asp:TextBox>
                        </td>
                        <td align="center" width="7%" class="label">Status</td>
                        <td align="left" width="12%">
                            <asp:DropDownList ID="ddlStatus" runat="server" CssClass="dropdownList" Width="120px" DataTextField="LeaveStatus" DataValueField="LeaveStatusId"></asp:DropDownList>
                        </td>
                        <td align="center" width="10%" class="label">Leave Type</td>
                        <td align="left" width="12%">
                            <asp:DropDownList ID="ddlLeaveType" runat="server" CssClass="dropdownList" Width="120px" DataValueField="LeaveTypeId" DataTextField="LeaveTypeName"></asp:DropDownList>
                        </td>
                        <td align="center" width="4%" class="label">From</td>
                        <td align="left" width="12%">
                            <asp:TextBox ID="txtFromDate" runat="server" CssClass="textbox" Width="110px"></asp:TextBox>
                            <asp:CalendarExtender ID="CalendarExtender3" runat="server" CssClass="cal_Theme1"
                                PopupPosition="BottomRight" Format="dd/MM/yyyy" TargetControlID="txtFromDate" OnClientDateSelectionChanged="" Enabled="True">
                            </asp:CalendarExtender>
                        </td>
                        <td align="center" width="4%" class="label">To</td>
                        <td align="left" width="12%">
                            <asp:TextBox ID="txtToDate" runat="server" CssClass="textbox" Width="110px"></asp:TextBox>
                            <asp:CalendarExtender ID="CalendarExtender4" runat="server" CssClass="cal_Theme1"
                                PopupPosition="BottomRight" Format="dd/MM/yyyy" TargetControlID="txtToDate" OnClientDateSelectionChanged="" Enabled="True">
                            </asp:CalendarExtender>
                        </td>
                        <td align="left">
                            <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="button" OnClientClick="return Validation()" 
                                onclick="btnSearch_Click" />
                        </td>
                    </tr> 
                  </table>
                  <br />
                  <table width="95%" align="center">
                    <tr>
                        <td align="center">
                            <asp:GridView ID="dgvLeave" runat="server" AutoGenerateColumns="false" 
                                 Width="100%" AllowPaging="true" PageSize="50"
                                 DataKeyNames="LeaveId" onpageindexchanging="dgvLeave_PageIndexChanging" 
                                onrowdatabound="dgvLeave_RowDataBound">
                            <Columns>
                                <asp:BoundField DataField="EmpCode" HeaderText="Employee Code" />
                                <asp:BoundField DataField="FullName" HeaderText="Employee Name" />
                                <asp:BoundField DataField="LeaveTypeName" HeaderText="Leave Type" />
                                <asp:BoundField DataField="StartDate" HeaderText="From" DataFormatString="{0:dd/MM/yyyy}" />
                                <asp:BoundField DataField="EndDate" HeaderText="To"  DataFormatString="{0:dd/MM/yyyy}" />
                                <asp:BoundField DataField="NoOfDays" HeaderText="Days" />
                                <asp:BoundField DataField="Purpose" HeaderText="Reason" />
                                <asp:BoundField DataField="CreatedOn" HeaderText="Applied On" DataFormatString="{0:dd/MM/yyyy}" />    
                                <asp:BoundField DataField="LeaveStatus" HeaderText="Manager Approval" />            
                                <asp:BoundField DataField="IsDirectorApproved" HeaderText="Director Approval" />
                                <asp:TemplateField ShowHeader="false">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit_icon.gif" />
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
