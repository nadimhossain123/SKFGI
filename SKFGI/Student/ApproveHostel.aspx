<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAdmin.Master" AutoEventWireup="true"
    CodeBehind="ApproveHostel.aspx.cs" Inherits="CollegeERP.Student.ApproveHostel" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../UserControl/Message.ascx" TagName="Message" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">
        function openIDpopup(poplocation, querystring, popheight, popwidth,poptop, popleft)
     {                
        var popposition='left = 200, top=15, width=850,align=center, height=640,menubar=no, scrollbars=yes, resizable=no';                
               
        var NewWindow = window.open(poplocation,'',popposition);                
        if (NewWindow.focus!=null){                        
        NewWindow.focus();                
        }        
     }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ToolkitScriptManager ID="ToolScript1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="title">
        <h5>
           Student Hostel Approval</h5>
    </div>
  
   <asp:UpdatePanel ID="Up1" runat="server">
        <ContentTemplate>
    <div>
          <uc3:Message ID="Message" runat="server" />
        <table width="95%" align="center" class="table">
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
                    &nbsp;
                    <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="button" OnClick="btnUpdate_Click" Visible="false" />
                </td>
            </tr>
            <tr>
                <td colspan="6">
                    <br />
                </td>
            </tr>
        </table>
       
        <br />
        <div>
            <table>
            <tr>
                <td></td>
                <td></td>
                <td></td>
                <td> 
                    <asp:Literal ID="ltTotal" runat="server" Mode="PassThrough"></asp:Literal></td>
            </tr>
            </table>
        </div>
        <h6 align="left" style="color: #00356A;">
            Student List</h6>
        <table width="95%">
            <tr>
                <td align="center">
                   <asp:GridView ID="dgvStudent" runat="server" AutoGenerateColumns="false" Width="100%"
                                AllowPaging="true" PageSize="100" DataKeyNames="id" 
                        onrowcommand="dgvStudent_RowCommand" OnPageIndexChanging="dgvStudent_PageIndexChanging" 
                        onrowdatabound="dgvStudent_RowDataBound">
                                <Columns>
                                    <%--<asp:TemplateField HeaderText="Select">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="ChkSelectStu" runat="server"   />
                                        </ItemTemplate>
                                        </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Approved">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="ChkSelect" runat="server" Checked='<%#Bind("IsHostelFacility") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
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
                                     <asp:TemplateField HeaderText="Date From/To">
                                        <ItemTemplate>
                                        <%--<div>--%>
                                           <asp:TextBox ID="txtDatefrom" runat="server" CssClass="textbox_required" Width="90px"></asp:TextBox>
                                            
                                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" CssClass="cal_Theme1"
                                             PopupPosition="BottomRight" Format="dd MMM yyyy" TargetControlID="txtDatefrom"
                                             OnClientDateSelectionChanged="" Enabled="True">
                                             
                                            </asp:CalendarExtender>
                                           <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                                ErrorMessage="Please Enter Date" ValidationGroup="acc" ControlToValidate="txtDatefrom"></asp:RequiredFieldValidator>
                                            </div> --%>   
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                      <%--<asp:TemplateField HeaderText="Date To">
                                        <ItemTemplate>
                                          <asp:TextBox ID="txtDateTo" runat="server" CssClass="textbox_required" Width="90px"></asp:TextBox>
                                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" CssClass="cal_Theme1"
                                             PopupPosition="BottomRight" Format="dd MMM yyyy" TargetControlID="txtDateTo"
                                             OnClientDateSelectionChanged="" Enabled="True">
                                            </asp:CalendarExtender>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                     <asp:TemplateField HeaderText="Hostel Details">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtHostelRoomNo" runat="server" CssClass="textbox"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Approved">
                                        <ItemTemplate>
                                            <asp:Button ID="btnAllotHostel" runat="server" Text="Allot" CssClass="button" 
                                                onclick="btnAllotHostel_Click" CommandName="Approve"  
                                                CommandArgument="<%#((GridViewRow) Container).RowIndex %>" 
                                                ValidationGroup="acc" />
                                               <%-- <asp:CheckBox ID="ChkSelect" runat="server" Checked='<%#Bind("IsHostelFacility") %>' />--%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Release">
                                        <ItemTemplate>
                                            <asp:Button ID="btnReleaseHostel" runat="server" Text="Release" CssClass="button" CommandName="Release"  
                                            CommandArgument="<%#((GridViewRow) Container).RowIndex %>" onclick="btnReleaseHostel_Click" />
                                               <%-- <asp:CheckBox ID="ChkSelect" runat="server" Checked='<%#Bind("IsHostelFacility") %>' />--%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <EmptyDataTemplate>
                                    <table style="height: 10px; width: 100%;">
                                        <tr align="left" class="HeaderStyle">
                                            <th scope="col">
                                                No Student Found
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
                                <EmptyDataRowStyle CssClass="EditRowStyle" />
                                <AlternatingRowStyle CssClass="AltRowStyle" />
                                <PagerStyle CssClass="PagerStyle" />
                            </asp:GridView>
                </td>
            </tr>
        </table>
    </div>
   </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
