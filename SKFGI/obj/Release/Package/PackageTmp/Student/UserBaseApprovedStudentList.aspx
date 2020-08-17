<%@ Page Language="C#" MasterPageFile="~/MasterAdmin.Master" AutoEventWireup="true"
    CodeBehind="UserBaseApprovedStudentList.aspx.cs" Inherits="CollegeERP.Student.UserBaseApprovedStudentList"
    Title="User Base Approved/Reject Student List" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">
        function Validation()
        {
            if (document.getElementById('<%=txtFromDate.ClientID%>').value == '')
                return ShowMsg("Enter Form Date");
            else if (document.getElementById('<%=txtToDate.ClientID%>').value == '')
                return ShowMsg("Enter To Date");
            else if (document.getElementById('<%=ddlEmployee.ClientID%>').selectedIndex == 0)
                return ShowMsg("Please Select User");
            else
                return true;            
        }
        
        function ShowMsg(str)
        {
            alert(str);
            return false;
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ToolkitScriptManager ID="toolScript1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="title">
        <h5>
            User Base Approved/Rejected Student List</h5>
    </div>
    <div style="width: 950px;">
        <table width="100%" align="center" class="table">
            <tr>
                <td width="20%" align="left" class="label">
                    From
                </td>
                <td width="20%" align="left" class="label">
                    To
                </td>
                <td width="20%" align="left" class="label">
                    User
                </td>
                <td width="20%" align="left" class="label">
                    Approved
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td width="20%" align="left">
                    <asp:TextBox ID="txtFromDate" runat="server" CssClass="textbox" Width="130px"></asp:TextBox>
                    <asp:CalendarExtender ID="CalendarExtender1" runat="server" CssClass="cal_Theme1"
                        PopupPosition="BottomRight" Format="dd MMM yyyy" TargetControlID="txtFromDate"
                        OnClientDateSelectionChanged="" Enabled="True">
                    </asp:CalendarExtender>
                </td>
                <td width="20%" align="left">
                    <asp:TextBox ID="txtToDate" runat="server" CssClass="textbox" Width="130px"></asp:TextBox>
                    <asp:CalendarExtender ID="CalendarExtender2" runat="server" CssClass="cal_Theme1"
                        PopupPosition="BottomRight" Format="dd MMM yyyy" TargetControlID="txtToDate"
                        OnClientDateSelectionChanged="" Enabled="True">
                    </asp:CalendarExtender>
                </td>
                <td width="20%" align="left">
                    <asp:DropDownList ID="ddlEmployee" runat="server" CssClass="dropdownList" Width="170px"
                        DataValueField="EmployeeId" DataTextField="FullName">
                    </asp:DropDownList>
                </td>
                <td width="20%" align="left">
                    <asp:CheckBox ID="ChkIsApproved" runat="server" Checked="true" />
                </td>
                <td align="left">
                    <asp:Button ID="btnSearch" runat="server" CssClass="button" Text="Search" OnClientClick="return Validation()"
                        OnClick="btnSearch_Click"></asp:Button>
                </td>
            </tr>
        </table>
        <table width="100%" align="center" class="table">
            <tr>
                <td align="center">
                    <asp:GridView ID="dgvStudent" runat="server" AutoGenerateColumns="False" AllowPaging="false"
                        Width="100%" DataKeyNames="id">
                        <Columns>
                            <asp:BoundField DataField="SrNo" HeaderText="SL" />
                            <asp:BoundField DataField="student_code" HeaderText="Student Code" />
                            <asp:BoundField DataField="name" HeaderText="Name" />
                            <asp:BoundField DataField="appliation_no" HeaderText="Appliation No" />
                            <asp:BoundField DataField="enrollmentn_no" HeaderText="Enrollmentn No" />
                            <asp:BoundField DataField="CourseName" HeaderText="Course" />
                            <asp:BoundField DataField="stream_name" HeaderText="Stream" />
                            <asp:BoundField DataField="rank" HeaderText="Option" />
                            <asp:BoundField DataField="rankid" HeaderText="Rank" />
                            <asp:BoundField DataField="IsLateral" HeaderText="Lateral" />
                            <asp:BoundField DataField="IsHostelFacility" HeaderText="Hostel" />
                            <asp:BoundField DataField="TFW" HeaderText="TFW" />
                            <asp:BoundField DataField="IsReAdmission" HeaderText="Re-Admission" />
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
                                        Sorry! No Student Found.
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
                <td align="right">
                    <asp:Button ID="btnPrint" runat="server" CssClass="button" Text="Print" Visible="false"
                        OnClick="btnPrint_Click" />&nbsp;
                    <asp:Button ID="btnExportExcel" runat="server" CssClass="button" Text="Export To excel"
                        OnClick="btnExportExcel_Click" Visible="false" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
