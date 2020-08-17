<%@ Page Language="C#" MasterPageFile="~/MasterAdmin.Master" AutoEventWireup="true"
    CodeBehind="ApproveHostelList.aspx.cs" Inherits="CollegeERP.Student.ApproveHostelList"
    Title="Approved Hostel List" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">
        function Validation()
        {
                       
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
            Approved/Rejected Hostel List</h5>
    </div>
    <div style="width: 950px;">
        <table width="100%" align="center" class="table">
          <%--  <tr>
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
              <%--  <td width="20%" align="left">
                    <asp:DropDownList ID="ddlEmployee" runat="server" CssClass="dropdownList" Width="170px"
                        DataValueField="EmployeeId" DataTextField="FullName" Visible="False">
                    </asp:DropDownList>
                </td>
                <td width="20%" align="left">
                    <asp:CheckBox ID="ChkIsApproved" runat="server" Checked="true" 
                        Visible="False" />
                </td>
                <td align="left">
                    <asp:Button ID="btnSearch" runat="server" CssClass="button" Text="Search" OnClientClick="return Validation()"
                        OnClick="btnSearch_Click"></asp:Button>
                </td>
            </tr>--%>
             <tr>
                <td align="left" width="15%" class="label">
                    <%--Application No--%>
                </td>
                <td align="left" width="15%" class="label">
                    Student Name
                </td>
                <td align="left" width="15%" class="label">
                    Course
                </td>
                <td align="left" width="15%" class="label">
                    Stream
                </td>
                <td align="left" width="15%" class="label">
                    Batch
                </td>
                <td align="left" width="25%" >
                </td>
            </tr>
            <tr>
                <td align="left" width="15%">
                    <asp:TextBox ID="txtApplicationNo" runat="server" CssClass="textbox" Width="130px" Visible="false"></asp:TextBox>
                </td>
                <td align="left" width="15%">
                    <asp:TextBox ID="txtApplicantName" runat="server" CssClass="textbox" Width="130px"></asp:TextBox>
                </td>
                <td align="left" width="15%">
                    <asp:DropDownList ID="ddlCourse" runat="server" AutoPostBack="true" CssClass="dropdownList"
                        Width="130px" DataValueField="CourseId" DataTextField="CourseName" OnSelectedIndexChanged="ddlCourse_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <td align="left" width="15%">
                    <asp:DropDownList ID="ddlStream" runat="server" CssClass="dropdownList" Width="130px"
                        DataTextField="stream_name" DataValueField="StreamId">
                    </asp:DropDownList>
                </td>
                <td align="left" width="15%">
                    <asp:DropDownList ID="ddlBatch" runat="server" CssClass="dropdownList" Width="130px"
                        DataValueField="id" DataTextField="batch_name">
                    </asp:DropDownList>
                </td>
                <td align="left" width="25%">
                    <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="button" OnClick="btnSearch_Click" />
                    <%--&nbsp;
                    <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="button" OnClick="btnUpdate_Click" Visible="false" />--%>
                </td>
            </tr>
        </table>
        <table width="100%" align="center" class="table">
            <tr>
                <td align="center">
                    <asp:GridView ID="dgvStudent" runat="server" AutoGenerateColumns="False" AllowPaging="false"
                        Width="100%" DataKeyNames="id">
                        <Columns>
                           <asp:TemplateField HeaderText="Sl No" >
                                    <ItemTemplate>    
                                            <%# ((GridViewRow)Container).RowIndex + 1%>
                                    </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="student_code" HeaderText="Student Code" />
                                    <asp:BoundField DataField="name" HeaderText="Student Name" />
                                    <asp:BoundField DataField="p_Address" HeaderText="Address" 
                                        ItemStyle-Width="250px" >
                                        <ItemStyle Width="250px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ContactNo" HeaderText="Contact No" 
                                        ItemStyle-Width="150px" >
                                        <ItemStyle Width="150px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="approvedDate" HeaderText="Approved Date" />
                                    <asp:BoundField DataField="gender" HeaderText="Gender" />
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
