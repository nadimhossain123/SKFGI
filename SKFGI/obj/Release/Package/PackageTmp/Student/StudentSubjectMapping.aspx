<%@ Page Title="Student Subject Mapping" Language="C#" MasterPageFile="~/MasterAdmin.Master"
    AutoEventWireup="true" CodeBehind="StudentSubjectMapping.aspx.cs" Inherits="CollegeERP.Student.StudentSubjectMapping" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../UserControl/Message.ascx" TagName="Message" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">

        function openpopup(poplocation, querystring, popheight, popwidth, poptop, popleft) {
            var popposition = 'left = 300, top=90, width=600,align=center, height=400,menubar=no, scrollbars=yes, resizable=no';

            var NewWindow = window.open(poplocation, '', popposition);
            if (NewWindow.focus != null) {
                NewWindow.focus();
            }
            return false;
        } 
       
        function SearchValidation() {
            if (!checkforvaliddata(eval('document.forms[0].' + '<%=ddlBatch.ClientID%>'), "Batch", 0)) return false;
            if (!checkforvaliddata(eval('document.forms[0].' + '<%=ddlCourse.ClientID%>'), "Course", 0)) return false;
            if (!checkforvaliddata(eval('document.forms[0].' + '<%=ddlStream.ClientID%>'), "Stream", 0)) return false;
            return true;
        }

        function Validation() {
            var gv = document.getElementById('<%=dgvStudent.ClientID%>');
            var rowCount = gv.rows.length - 1;

            if (document.getElementById('<%=ddlElectiveSubject.ClientID%>').selectedIndex == 0) {
                alert("Select Elective Subject");
                return false;
            }
            else if (rowCount == 0) {
                alert("No Student to Update");
                return false;
            }
            else if (!Checkbox_Validation()) {
                alert("Please Select Atleast One Student");
                return false;
            }
            else
                return confirm('Are You Sure?');
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
                        arr[i].parentNode.parentNode.className = 'SelectedRowStyle';
                    }
                    else {
                        arr[i].checked = false;
                        arr[i].parentNode.parentNode.className = 'RowStyle';
                    }
                }
            }

        }

        function ChangeCSS(Obj) {
            var row = Obj.parentNode.parentNode;
            if (Obj.checked)
                row.className = 'SelectedRowStyle';
            else
                row.className = 'RowStyle';

        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="Script1" runat="server">
    </asp:ScriptManager>
    <div class="title">
        <h5>
            Student Subject Mapping</h5>
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
                <br />
            </div>
            <div style="width: 780px">
                <table width="100%" align="center" class="table">
                    <tr>
                        <td class="label" width="20%">
                            Batch
                        </td>
                        <td class="label" width="20%">
                            Course
                        </td>
                        <td class="label" width="20%">
                            Stream
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:DropDownList ID="ddlBatch" runat="server" CssClass="dropdownList" Width="160px"
                                DataValueField="id" DataTextField="batch_name">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlCourse" runat="server" AutoPostBack="true" CssClass="dropdownList"
                                Width="160px" DataValueField="CourseId" DataTextField="CourseName" OnSelectedIndexChanged="ddlCourse_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlStream" runat="server" CssClass="dropdownList" Width="160px"
                                DataTextField="stream_name" DataValueField="StreamId">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="button" OnClientClick="return SearchValidation();"
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
                                GridLines="None" AllowPaging="false" CellPadding="0" CellSpacing="0" DataKeyNames="id">
                                <Columns>
                                    <asp:TemplateField ItemStyle-Width="15px">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="ChkSelect" runat="server" onclick="ChangeCSS(this)" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="UniversityRollNo" HeaderText="University Roll No." />
                                    <asp:BoundField DataField="name" HeaderText="Student Name" />
                                    <asp:BoundField DataField="student_code" HeaderText="Student Code" />
                                    <asp:BoundField DataField="CurrentSem" HeaderText="Current Sem" />
                                    <asp:TemplateField HeaderText="Elective Subject">
                                        <ItemTemplate>
                                            <a href="#" onclick="return openpopup('<%# String.Format("PopUpStudentSubjectMappingUpdate.aspx?StudentId={0}",Eval("id")) %>');"><%#Eval("ElectiveSubjectCount") %>&nbsp;Subject</a>
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
                                <AlternatingRowStyle CssClass="AltRowStyle" />
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td width="75%" align="right" class="label">
                            Elective Subject:
                        </td>
                        <td align="center">
                            <asp:DropDownList ID="ddlElectiveSubject" runat="server" CssClass="dropdownList"
                                Width="130px" DataValueField="SubjectId" DataTextField="SubjectNameWithCode">
                            </asp:DropDownList>
                        </td>
                        <td align="right">
                            <asp:Button ID="btnSave" runat="server" CssClass="button" Text="Save" OnClientClick="return Validation();"
                                OnClick="btnSave_Click" />
                        </td>
                    </tr>
                </table>
                <br />
                <br />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
