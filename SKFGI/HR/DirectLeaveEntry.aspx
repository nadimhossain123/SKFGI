<%@ Page Language="C#" MasterPageFile="~/MasterAdmin.Master" AutoEventWireup="true"
    CodeBehind="DirectLeaveEntry.aspx.cs" Inherits="CollegeERP.HR.DirectLeaveEntry"
    Title="Direct Leave Entry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../UserControl/Message.ascx" TagName="Message" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">
        function Validation()
        {
           if (document.getElementById('<%=ddlLeaveType.ClientID%>').selectedIndex == 0)
            {
                alert("Select Leave Type");
                return false;
            }
          else if (isValidDate(document.getElementById('<%=txtFrom.ClientID%>').value) == false) {
            alert("Enter From Date in DD/MM/YYYY format");
            return false;
            } 
          else if (isValidDate(document.getElementById('<%=txtTo.ClientID%>').value) == false)
          {
            alert("Enter To Date in DD/MM/YYYY format");
            return false;
          } 
          else if (document.getElementById('<%=txtPurpose.ClientID%>').value == '')
            {
                alert("Enter Reason");
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
    <asp:ToolkitScriptManager ID="toolScript1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="title">
        <h5>
            Direct Leave Entry</h5>
    </div>
    <asp:UpdatePanel ID="UP1" runat="server">
        <ContentTemplate>
            <uc3:Message ID="Message" runat="server" />
            <br />
            <h6 align="left" style="color: #00356A;">
                Select Employee</h6>
            <div style="width: 740px">
                <table width="100%" align="center" class="table">
                    <tr>
                        <td style="width: 20%">
                        </td>
                        <td style="width: 20%" class="label">
                            Employee :
                        </td>
                        <td style="width: 20%">
                            <asp:ComboBox ID="ddlEmployee" runat="server" CssClass="WindowsStyle" DropDownStyle="DropDown"
                                AutoCompleteMode="SuggestAppend" CaseSensitive="false" Width="170px" DataValueField="EmployeeId"
                                DataTextField="FullName" AutoPostBack="true" OnSelectedIndexChanged="ddlEmployee_SelectedIndexChanged">
                            </asp:ComboBox>
                        </td>
                        <td style="width: 20%">
                        </td>
                    </tr>
                </table>
            </div>
            <h6 align="left" style="color: #00356A;">
                My Leave Balance</h6>
            <div style="width: 740px;">
                <table width="100%" align="center" class="table">
                    <tr>
                        <td align="center">
                            <asp:GridView ID="dgvStock" runat="server" AutoGenerateColumns="false" AllowPaging="false"
                                Width="100%">
                                <Columns>
                                    <asp:BoundField DataField="LeaveTypeName" HeaderText="Leave" />
                                    <asp:BoundField DataField="LeaveCredit" HeaderText="Leave Credit" />
                                    <asp:BoundField DataField="LeaveTaken" HeaderText="Leave Taken" />
                                    <asp:BoundField DataField="LeaveBalance" HeaderText="Leave Balance" />
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
            <br />
            <h6 align="left" style="color: #00356A;">
                Apply Leave</h6>
            <div style="width: 740px;">
                <table width="100%" align="center" class="table">
                    <tr>
                        <td align="left" width="20%" class="label">
                            Leave Apply Date
                        </td>
                        <td align="left" width="30%">
                            <asp:TextBox ID="txtCreatedOn" runat="server" CssClass="textbox_disabled" Width="140px"
                                Enabled="false"></asp:TextBox>
                        </td>
                        <td align="left" width="20%" class="label">
                            Leave<span class="req">*</span>
                        </td>
                        <td align="left" width="30%">
                            <asp:DropDownList ID="ddlLeaveType" runat="server" CssClass="dropdownList" Width="140px"
                                DataValueField="LeaveTypeId" DataTextField="LeaveTypeName" AutoPostBack="true"
                                OnSelectedIndexChanged="ddlLeaveType_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" width="20%" class="label">
                            From<span class="req">*</span>
                        </td>
                        <td align="left" width="30%">
                            <asp:TextBox ID="txtFrom" runat="server" CssClass="textbox" Width="140px"></asp:TextBox>
                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" CssClass="cal_Theme1"
                                PopupPosition="BottomRight" Format="dd/MM/yyyy" TargetControlID="txtFrom" OnClientDateSelectionChanged=""
                                Enabled="True">
                            </asp:CalendarExtender>
                        </td>
                        <td align="left" width="20%" class="label">
                            To
                        </td>
                        <td align="left" width="30%">
                            <asp:TextBox ID="txtTo" runat="server" CssClass="textbox" Width="140px"></asp:TextBox>
                            <asp:CalendarExtender ID="CalendarExtender2" runat="server" CssClass="cal_Theme1"
                                PopupPosition="BottomRight" Format="dd/MM/yyyy" TargetControlID="txtTo" OnClientDateSelectionChanged=""
                                Enabled="True">
                            </asp:CalendarExtender>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" width="20%" class="label">
                            Leave Format
                        </td>
                        <td align="left" width="30%">
                            <asp:DropDownList ID="ddlLeaveFormat" runat="server" CssClass="dropdownList" Width="70px">
                                <asp:ListItem Value="Full" Text="Full"></asp:ListItem>
                                <asp:ListItem Value="Half" Text="Half"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td align="left" width="20%" class="label">
                            Adjustment
                        </td>
                        <td align="left" width="30%">
                            <asp:CheckBox ID="ChkIsAdjustment" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td align="left" width="20%" class="label">
                            Reason<span class="req">*</span>
                        </td>
                        <td align="left" colspan="3">
                            <asp:TextBox ID="txtPurpose" runat="server" CssClass="textbox_required" Width="500px"
                                Height="35px" TextMode="MultiLine" Style="resize: none"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" width="20%" class="label">
                            Class Adjusted
                        </td>
                        <td align="left" width="30%">
                            <asp:CheckBox ID="ChkIsClassAdjusted" runat="server" />
                        </td>
                        <td align="left" width="20%" class="label">
                            Exam Duty During Leave
                        </td>
                        <td align="left" width="30%">
                            <asp:CheckBox ID="ChkIsExamDutyDuringLeave" runat="server" />
                        </td>
                    </tr>
                </table>
                <br />
                <br />
                <table>
                    <tr>
                        <td align="center">
                            <asp:Button ID="btnSave" runat="server" Text="Apply" CssClass="button" OnClientClick="javascript:return Validation()"
                                OnClick="btnSave_Click" />&nbsp;
                            <asp:Button ID="btnCancel" runat="server" CssClass="button" Text="Reset" OnClick="btnCancel_Click" />
                        </td>
                    </tr>
                </table>
            </div>
            <br />
            <h6 align="left" style="color: #00356A;">
                My Leave History</h6>
            <table width="95%" align="center" class="table">
                <tr>
                    <td align="center" width="10%" class="label">
                        Manager Approval
                    </td>
                    <td align="left" width="12%">
                        <asp:DropDownList ID="ddlStatus" runat="server" CssClass="dropdownList" Width="120px"
                            DataTextField="LeaveStatus" DataValueField="LeaveStatusId">
                        </asp:DropDownList>
                    </td>
                    <td align="center" width="10%" class="label">
                        Leave Type
                    </td>
                    <td align="left" width="12%">
                        <asp:DropDownList ID="ddlLeaveType_Search" runat="server" CssClass="dropdownList"
                            Width="120px" DataValueField="LeaveTypeId" DataTextField="LeaveTypeName">
                        </asp:DropDownList>
                    </td>
                    <td align="center" width="6%" class="label">
                        From
                    </td>
                    <td align="left" width="12%">
                        <asp:TextBox ID="txtFromDate" runat="server" CssClass="textbox" Width="110px"></asp:TextBox>
                        <asp:CalendarExtender ID="CalendarExtender3" runat="server" CssClass="cal_Theme1"
                            PopupPosition="BottomRight" Format="dd/MM/yyyy" TargetControlID="txtFromDate"
                            OnClientDateSelectionChanged="" Enabled="True">
                        </asp:CalendarExtender>
                    </td>
                    <td align="center" width="6%" class="label">
                        To
                    </td>
                    <td align="left" width="12%">
                        <asp:TextBox ID="txtToDate" runat="server" CssClass="textbox" Width="110px"></asp:TextBox>
                        <asp:CalendarExtender ID="CalendarExtender4" runat="server" CssClass="cal_Theme1"
                            PopupPosition="BottomRight" Format="dd/MM/yyyy" TargetControlID="txtToDate" OnClientDateSelectionChanged=""
                            Enabled="True">
                        </asp:CalendarExtender>
                    </td>
                    <td align="left" class="label">
                        <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="button" OnClientClick="return SearchValidation()"
                            OnClick="btnSearch_Click" />
                    </td>
                </tr>
            </table>
            <br />
            <table width="95%" align="center" class="table">
                <tr>
                    <td align="center">
                        <asp:GridView ID="dgvLeave" runat="server" AutoGenerateColumns="false" Width="100%"
                            AllowPaging="false" DataKeyNames="LeaveId">
                            <Columns>
                                <asp:BoundField DataField="LeaveTypeName" HeaderText="Leave Type" />
                                <asp:BoundField DataField="StartDate" HeaderText="From" DataFormatString="{0:dd/MM/yyyy}" />
                                <asp:BoundField DataField="EndDate" HeaderText="To" DataFormatString="{0:dd/MM/yyyy}" />
                                <asp:BoundField DataField="NoOfDays" HeaderText="Days" />
                                <asp:BoundField DataField="Purpose" HeaderText="Reason" />
                                <asp:BoundField DataField="CreatedOn" HeaderText="Applied On" DataFormatString="{0:dd/MM/yyyy}" />
                                <asp:BoundField DataField="LeaveManager" HeaderText="Leave Manager" />
                                <asp:BoundField DataField="LeaveStatus" HeaderText="Manager Approval" />
                                <asp:BoundField DataField="Comment" HeaderText="Manager Remarks" />
                                <asp:BoundField DataField="IsDirectorApproved" HeaderText="Director Approval" />
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
            <br />
            <br />
            <br />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
