<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PopUpLeave.aspx.cs" Inherits="CollegeERP.HR.PopUpLeave"
    Title="Leave Details" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../UserControl/Message.ascx" TagName="Message" TagPrefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Leave Details</title>
    <link href="../Styles/style.css" rel="Stylesheet" type="text/css" />
    <link href="../Styles/Control.css" rel="Stylesheet" type="text/css" />
    <link href="../Styles/blue.css" rel="Stylesheet" type="text/css" />
    <link href="../Styles/reset.css" rel="Stylesheet" type="text/css" />
</head>
<body>

    <script type="text/javascript">
        function Validation() {
            if (document.getElementById('<%=ddlLeaveType.ClientID%>').selectedIndex == 0) {
                alert("Select Leave Type");
                return false;
            }
            else if (isValidDate(document.getElementById('<%=txtFrom.ClientID%>').value) == false) {
                alert("Enter From Date in DD/MM/YYYY format");
                return false;
            }
            else if (isValidDate(document.getElementById('<%=txtTo.ClientID%>').value) == false) {
                alert("Enter To Date in DD/MM/YYYY format");
                return false;
            }
            else if (document.getElementById('<%=txtComment.ClientID%>').value == '') {
                alert("Enter Comment");
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

    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="Script1" runat="server">
    </asp:ToolkitScriptManager>
    <asp:UpdatePanel ID="UP1" runat="server">
        <ContentTemplate>
            <br />
            <uc3:Message ID="Message" runat="server" />
            <br />
            <center>
                <div style="width: 800px;">
                    <table width="100%" align="center" class="table">
                        <tr>
                            <td align="left" width="20%">
                                <h7>Employee Name</h7>
                            </td>
                            <td align="left" width="30%" class="label">
                                <asp:Literal ID="ltrEmpName" runat="server" Mode="PassThrough"></asp:Literal>
                            </td>
                            <td align="left" width="20%">
                                <h7>Employee Code</h7>
                            </td>
                            <td align="left" width="30%" class="label">
                                <asp:Literal ID="ltrEmpCode" runat="server" Mode="PassThrough"></asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" width="20%">
                                <h7>Designation</h7>
                            </td>
                            <td align="left" width="30%" class="label">
                                <asp:Literal ID="ltrDesignation" runat="server" Mode="PassThrough"></asp:Literal>
                            </td>
                            <td align="left" width="20%">
                                <h7>Department</h7>
                            </td>
                            <td align="left" width="30%" class="label">
                                <asp:Literal ID="ltrDepartment" runat="server" Mode="PassThrough"></asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="4">
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
            </center>
            <br />
            <center>
                <div style="width: 800px;">
                    <table width="100%" align="center" class="table">
                        <tr>
                            <td align="left" width="20%" class="label">
                                Apply Date
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
                                Status
                            </td>
                            <td align="left" width="30%">
                                <asp:DropDownList ID="ddlStatus" runat="server" CssClass="dropdownList" Width="140px"
                                    DataTextField="LeaveStatus" DataValueField="LeaveStatusId">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" width="20%" class="label">
                                Reason
                            </td>
                            <td align="left" colspan="3">
                                <asp:TextBox ID="txtPurpose" runat="server" CssClass="textbox_disabled" Width="500px"
                                    Height="35px" TextMode="MultiLine" Enabled="false"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" width="20%" class="label">
                                Comment<span class="req">*</span>
                            </td>
                            <td align="left" colspan="3">
                                <asp:TextBox ID="txtComment" runat="server" CssClass="textbox_required" Width="500px"
                                    Height="35px" TextMode="MultiLine" Style="resize: none"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" width="20%" class="label">
                                Class Adjusted
                            </td>
                            <td align="left" width="30%">
                                <asp:CheckBox ID="ChkIsClassAdjusted" runat="server" Enabled="false" />
                            </td>
                            <td align="left" width="20%" class="label">
                                Exam Duty During Leave
                            </td>
                            <td align="left" width="30%">
                                <asp:CheckBox ID="ChkIsExamDutyDuringLeave" runat="server" Enabled="false" />
                            </td>
                        </tr>
                        <tr>
                            <td align="left" width="20%" class="label">
                                Adjustment
                            </td>
                            <td align="left" colspan="3">
                                <asp:CheckBox ID="ChkIsAdjustment" runat="server" Enabled="false" />
                            </td>
                        </tr>
                    </table>
                    <br />
                    <br />
                    <table>
                        <tr>
                            <td align="center">
                                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="button" OnClientClick="javascript:return Validation()"
                                    OnClick="btnSave_Click" />&nbsp;
                                <input type="button" value="Close" class="button" onclick="window.close();opener.location.reload();" />
                            </td>
                        </tr>
                    </table>
                </div>
            </center>
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
