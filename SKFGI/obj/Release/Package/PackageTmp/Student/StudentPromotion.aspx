<%@ Page Language="C#" MasterPageFile="~/MasterAdmin.Master" AutoEventWireup="true"
    CodeBehind="StudentPromotion.aspx.cs" Inherits="CollegeERP.Student.StudentPromotion"
    Title="Student Promotion" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../UserControl/Message.ascx" TagName="Message" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">
        function SearchValidation()
        {
            if (document.getElementById('<%=ddlCourse.ClientID%>').selectedIndex == 0)
            {
                alert("Select Course");
                return false;
            }
            else {return true;}
        }
        
        function Validation()
        {
            var gv = document.getElementById('<%=dgvStudent.ClientID%>');
            var rowCount = gv.rows.length - 1;
            
            if (document.getElementById('<%=ddlNewSemNo.ClientID%>').selectedIndex == 0)
            {
                alert("Select New Sem");
                return false;
            }
            else if (rowCount == 0) {
                alert("No Student to Update");
                return false;
            }
            else if (Checkbox_Validation() == false) {
                alert("Please Select Atleast One Student");
                return false;
            }
            else 
               return confirm('Do you want to proceed?');
        }
        
        function Checkbox_Validation() {
            var flag = 0;
            var gv = document.getElementById('<%=dgvStudent.ClientID%>');
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
        
        function CheckAll(checkbox) {

            var gv = document.getElementById('<%=dgvStudent.ClientID%>');
            var arr = gv.getElementsByTagName("input");
            for (var i = 0; i < arr.length; i++) {
                if (arr[i].type == 'checkbox') {
                    if (checkbox.checked == true) {
                        arr[i].checked = true;
                        arr[i].parentNode.parentNode.className='SelectedRowStyle';
                    }
                    else {
                        arr[i].checked = false;
                        arr[i].parentNode.parentNode.className='RowStyle';
                    }
                }
            }

        }
        
        function ChangeCSS(Obj)
        {
            var row = Obj.parentNode.parentNode;
            if(Obj.checked)
              row.className='SelectedRowStyle';
            else
              row.className='RowStyle';     
    
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="Sc1" runat="server">
    </asp:ScriptManager>
    <div class="title">
        <h5>
            Student Promotion</h5>
    </div>
    <asp:UpdateProgress ID="UProgress1" runat="server" AssociatedUpdatePanelID="UP1">
        <ProgressTemplate>
            <div class="overlay">
                <img style="top: 50%; position: relative;" src="../Images/ajax-loader.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="UP1" runat="server">
        <ContentTemplate>
            <div style="width: 780px">
                <uc3:Message ID="Message" runat="server" />
            </div>
            <div style="width: 780px">
                <table width="100%" align="center" class="table">
                    <tr>
                        <td align="left" class="label" width="30%">
                            Course
                        </td>
                        <td align="left" class="label" width="30%">
                            Stream
                        </td>
                        <td align="left" class="label" width="30%">
                            Batch
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" width="30%">
                            <asp:DropDownList ID="ddlCourse" runat="server" AutoPostBack="true" CssClass="dropdownList"
                                Width="160px" DataValueField="CourseId" DataTextField="CourseName" OnSelectedIndexChanged="ddlCourse_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td align="left" width="30%">
                            <asp:DropDownList ID="ddlStream" runat="server" CssClass="dropdownList" Width="160px"
                                DataTextField="stream_name" DataValueField="StreamId">
                            </asp:DropDownList>
                        </td>
                        <td align="left" width="30%">
                            <asp:DropDownList ID="ddlBatch" runat="server" CssClass="dropdownList" Width="160px"
                                DataValueField="id" DataTextField="batch_name">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="button" OnClientClick="return SearchValidation()"
                                OnClick="btnSearch_Click" />
                        </td>
                    </tr>
                </table>
                <table width="100%" align="center" class="table">
                    <tr>
                        <td align="left" colspan="3">
                            <asp:CheckBox ID="ChkSelect" runat="server" Text="All" onclick="javascript:CheckAll(this);" />
                        </td>
                    </tr>
                    <tr>
                        <td align="center" colspan="3">
                            <asp:GridView ID="dgvStudent" runat="server" Width="100%" AutoGenerateColumns="false"
                                GridLines="None" AllowPaging="false" DataKeyNames="id">
                                <Columns>
                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="ChkSelect" runat="server" onclick="ChangeCSS(this)" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:ImageField DataImageUrlField="Photo" HeaderText="Photo" ControlStyle-Height="50px"
                                        ControlStyle-Width="50px">
                                    </asp:ImageField>
                                    <asp:BoundField DataField="student_code" HeaderText="Roll No" />
                                    <asp:BoundField DataField="name" HeaderText="Name" />
                                    <asp:BoundField DataField="CurrentSem" HeaderText="Sem" />
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
                                <AlternatingRowStyle CssClass="AltRowStyle" />
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td width="65%" align="right" class="label">
                            New Sem No:
                        </td>
                        <td align="center">
                            <asp:DropDownList ID="ddlNewSemNo" runat="server" CssClass="dropdownList" Width="130px">
                            </asp:DropDownList>
                        </td>
                        <td align="right">
                            <asp:Button ID="btnSave" runat="server" CssClass="button" Text="Update Now" OnClientClick="return Validation()"
                                OnClick="btnSave_Click" />
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
