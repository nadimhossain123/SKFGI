<%@ Page Language="C#" MasterPageFile="~/MasterAdmin.Master" AutoEventWireup="true" CodeBehind="TeacherSubjectMapping.aspx.cs" Inherits="CollegeERP.Student.TeacherSubjectMapping" Title="Teacher Subject Mapping" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .Img
        {
        	cursor:pointer;
        	width:38px;
        	height:26px;
        }
    </style>
    <script type="text/javascript">
    function Validation()
    {
        if (document.getElementById('<%=txtEmployeeName.ClientID%>').value == '')
            {
                alert('Please Select an Employee');
                return false;
            }
        else if (document.getElementById('<%=ddlSubject.ClientID%>').selectedIndex == 0)
            {
                alert('Please Select Subject');
                return false;
            }   
         else {return true;}    
    }
    
     function openpopup(poplocation, querystring, popheight, popwidth,poptop, popleft)
        {                
        var popposition='left = 300, top=90, width=850,align=center, height=600,menubar=no, scrollbars=yes, resizable=no';                
               
        var NewWindow = window.open(poplocation,'',popposition);                
        if (NewWindow.focus!=null){                        
        NewWindow.focus();                
        } 
        return false;       
     } 
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<asp:ScriptManager ID="Script1" runat="server"></asp:ScriptManager>
    <div class="title">
		<h5>Teacher Subject Mapping</h5>
    </div>
    
    <asp:UpdatePanel ID="Up1" runat="server">
        <ContentTemplate>
            <br />
            <h6 align="left" style="color:#00356A;">Mapping Details</h6>  
            <div style="width:680px;">
                <table width="100%" align="center" class="table">
                  <tr>
                    <td align="left" width="20%" class="label">Employee Name</td>
                    <td align="left" width="30%">
                        <asp:TextBox ID="txtEmployeeName" runat="server" CssClass="textbox_disabled" Width="180px" Enabled="false"></asp:TextBox>
                    </td>
                    <td align="left" width="20%" class="label">Employee Code</td>
                    <td align="left" width="30%">
                        <asp:TextBox ID="txtEmployeeCode" runat="server" CssClass="textbox_disabled" Width="180px" Enabled="false"></asp:TextBox>
                    </td>  
                 </tr>
                 <tr>
                    <td align="left" width="20%" class="label">Subject<span class="req">*</span></td>
                    <td align="left" width="30%">
                        <asp:DropDownList ID="ddlSubject" runat="server" CssClass="dropdownList" Width="140px" DataValueField="SubjectId" DataTextField="SubjectName"></asp:DropDownList>
                        <img id="btnNewSubject" runat="server" src="~/Images/newLeft.gif" style="cursor:pointer; padding-top:5px;" />
                    </td>
                    <td align="left" colspan="2">
                        <asp:Button ID="btnSave" runat="server" CssClass="button" Text="Add" 
                            OnClientClick="return Validation()" onclick="btnSave_Click" />
                        <a onclick="javascript:return openpopup('BulkSubjectAttendance.aspx')">Min Attendance Bulk Update</a>
                    </td>
                </tr>
                </table>
                <br />
                <table width="100%" align="center" class="table">
                <tr>
                    <td align="center">
                        <asp:GridView ID="dgvSubject" runat="server" AutoGenerateColumns="false" AllowPaging="false"
                            DataKeyNames="EmployeeSubjectMappingId" Width="100%" 
                            onrowdeleting="dgvSubject_RowDeleting">
                            <Columns>
                                <asp:BoundField DataField="SubjectCode" HeaderText="Subject Code" />
                                <asp:BoundField DataField="SubjectName" HeaderText="Subject Name" />
                                <asp:TemplateField ShowHeader="false">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/Images/Delete_icon.gif" CommandName="Delete" />
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
            </div>   
            
            <br />
            <h6 align="left" style="color:#00356A;">Mapping Summary</h6>  
            <div style="width:680px;">
                <table width="100%" align="center" class="table">
                    <tr>
                        <td align="left" width="15%" class="label">First Name</td>
                        <td align="left" width="18%">
                            <asp:TextBox ID="txtFName" runat="server" CssClass="textbox" Width="110px"></asp:TextBox>
                        </td>
                        <td align="left">
                            <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="button" 
                                onclick="btnSearch_Click" />
                        </td>
                    </tr> 
                </table>
                <br />
                <table width="100%" align="center">
                    <tr>
                        <td align="center">
                            <asp:GridView ID="dgvMapping" runat="server" AutoGenerateColumns="false" 
                                 Width="100%" AllowPaging="false" DataKeyNames="EmployeeId" 
                                onrowediting="dgvMapping_RowEditing">
                            <Columns>
                                <asp:BoundField DataField="EmpCode" HeaderText="Emp Code" />
                                <asp:BoundField DataField="FullName" HeaderText="Name" />
                                <asp:BoundField DataField="SubjectCount" HeaderText="Total Subject" />
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
            </div>  
            
            <br />
            <br />
            <br />
            <br />   
        </ContentTemplate>
    </asp:UpdatePanel>    
</asp:Content>
