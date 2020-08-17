<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAdmin.Master" AutoEventWireup="true"
    CodeBehind="allStudent.aspx.cs" Inherits="CollegeERP.Student.allStudent" %>

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
      function openChangepopup(poplocation, querystring, popheight, popwidth,poptop, popleft)
     {                
        var popposition='left = 400, top=100, width=450,align=center, height=240,menubar=no, scrollbars=yes, resizable=no, ';                
               
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
            ALL Student</h5>
    </div>
    <uc3:Message ID="Message" runat="server" />
    <%--<asp:UpdatePanel ID="Up1" runat="server">
        <ContentTemplate>--%>
    <div>
        <table width="95%" align="center" class="table">
            <tr>
                <td align="left" class="label">
                    Application No
                </td>
                <td align="left" class="label">
                    Student Name
                </td>
                <td align="left" class="label">
                    Course
                </td>
                <td align="left" class="label">
                    Stream
                </td>
                <td align="left" class="label">
                    Batch
                </td>
                <td align="left" class="label">
                    Submitted By</td>
                <td align="left">
                </td>
            </tr>
            <tr>
                <td align="left">
                    <asp:TextBox ID="txtApplicationNo" runat="server" CssClass="textbox" Width="130px"></asp:TextBox>
                </td>
                <td align="left">
                    <asp:TextBox ID="txtApplicantName" runat="server" CssClass="textbox" Width="130px"></asp:TextBox>
                </td>
                <td align="left">
                    <asp:DropDownList ID="ddlCourse" runat="server" AutoPostBack="true" CssClass="dropdownList"
                        Width="130px" DataValueField="CourseId" DataTextField="CourseName" OnSelectedIndexChanged="ddlCourse_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <td align="left">
                    <asp:DropDownList ID="ddlStream" runat="server" CssClass="dropdownList" Width="130px"
                        DataTextField="stream_name" DataValueField="StreamId">
                    </asp:DropDownList>
                </td>
                <td align="left" >
                    <asp:DropDownList ID="ddlBatch" runat="server" CssClass="dropdownList" Width="130px"
                        DataValueField="id" DataTextField="batch_name">
                    </asp:DropDownList>
                </td>
                <td align="left">
                    <asp:TextBox ID="txtSubmittedBy" runat="server" CssClass="textbox" 
                        Width="130px"></asp:TextBox>
                </td>
                <td align="right">
                    <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="button" OnClick="btnSearch_Click" />
                </td>
            </tr>
        </table>
        <table width="100%" id="tblPhoto" runat="server">
            <tr>
                <td colspan="3" align="left">
                    <h6 align="left" style="color: #00356A;">
                        Change Student Photo</h6>
                </td>
            </tr>
            <tr>
                <td align="right" class="label" width="25%">
                    Photo
                </td>
                <td align="left" width="15%">
                    <asp:FileUpload ID="uploadImage" runat="server" class="label" />
                </td>
                <td align="left">
                    <asp:Button ID="btnChangePhoto" runat="server" CssClass="button" Text="Change Photo"
                        OnClick="btnChangePhoto_Click" />
                    &nbsp;
                    <asp:Button ID="btnCancel" runat="server" CssClass="button" Text="Cancel" OnClick="btnCancel_Click" />
                </td>
            </tr>
        </table>
        <br />
        <h6 align="left" style="color: #00356A;">
            Student List</h6>
        <table width="95%">
            <tr>
                <td align="center">
                    <asp:GridView ID="dgvAllStudent" runat="server" AutoGenerateColumns="false" Width="100%"
                        AllowPaging="true" PageSize="100" DataKeyNames="id,CourseId" GridLines="None"
                        OnRowEditing="dgvAllStudent_RowEditing" OnPageIndexChanging="dgvAllStudent_PageIndexChanging"
                        OnRowCommand="dgvAllStudent_RowCommand" OnRowDataBound="dgvAllStudent_RowDataBound">
                        <Columns>
                            <asp:TemplateField HeaderText="Photo">
                                <ItemTemplate>
                                    <asp:Image ID="ImgIDCard" runat="server" ImageUrl='<%#Bind("Photo") %>' Width="50px"
                                        Height="50px" ToolTip="Click For ID Card" />
                                    <br />
                                    <asp:LinkButton ID="lnkChangePhoto" runat="server" Text="Change" CommandArgument='<%#Bind("id") %>'
                                        CommandName="Photo"></asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="appliation_no" HeaderText="Application No" />
                            <asp:BoundField DataField="student_code" HeaderText="Student Code" />
                            <asp:BoundField DataField="name" HeaderText="Applicant Name" />
                            <asp:BoundField DataField="CourseName" HeaderText="Course" />
                            <%--<asp:BoundField DataField="stream_name" HeaderText="Stream" />--%>

                             <asp:BoundField DataField="adhar_no" HeaderText="Adhar No." />

                             <asp:TemplateField HeaderText="Stream">
                                <ItemTemplate>                                   
                                    <a href="#" onclick="openChangepopup('PopUpChangeStudentDetail.aspx?Id=<%# DataBinder.Eval (Container.DataItem, "id") %>&Course_id=<%# DataBinder.Eval (Container.DataItem, "CourseId") %>');">
                                        <%# DataBinder.Eval (Container.DataItem, "stream_name") %>
                                    </a>                                                                     
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="batch_name" HeaderText="Addmission In" />
                            <asp:BoundField DataField="SubmittedBy" HeaderText="Submitted By" />
                            <asp:BoundField DataField="IsActive" HeaderText="Active" />
                            <asp:BoundField DataField="IsApproved" HeaderText="Approved" />
                            <asp:TemplateField ShowHeader="false">
                                <ItemTemplate>
                                    <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit_icon.gif" CommandName="Edit" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%-- <asp:TemplateField ShowHeader="false">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/Images/delete_icon.gif"
                                                CommandName="Delete" OnClientClick="return confirm('Are You Sure?');" />
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:ImageButton ID="btnPrint" runat="server" ImageUrl="~/Images/print.gif" Width="17px"
                                        Height="17px" />
                                </ItemTemplate>
                            </asp:TemplateField>
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
                                        Sorry! No Records Found.
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
    <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
