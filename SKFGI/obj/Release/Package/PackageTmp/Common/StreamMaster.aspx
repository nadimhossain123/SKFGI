<%@ Page Language="C#" MasterPageFile="~/MasterAdmin.Master" AutoEventWireup="true"
    CodeBehind="StreamMaster.aspx.cs" Inherits="CollegeERP.Common.StreamMaster" Title="Stream Master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../UserControl/Message.ascx" TagName="Message" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">
        function Validation()
        {
            if (!checkforvaliddata(eval('document.forms[0].' + '<%=ddlCourse.ClientID%>'), "Course", 0)) return false;
            if (!checkforvaliddata(eval('document.forms[0].' + '<%=txtStream.ClientID%>'), "Stream", 1)) return false;
            if (!checkforvaliddata(eval('document.forms[0].' + '<%=txtCapacity.ClientID%>'), "Capacity", 1)) return false;
            return true;
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ToolkitScriptManager ID="ToolScript1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="title">
        <h5>
            Stream</h5>
    </div>
    <asp:UpdatePanel ID="Up1" runat="server">
        <ContentTemplate>
            <div style="width: 750px;">
                <uc3:Message ID="Message" runat="server" />
                <br />
                <table align="center" width="100%" class="table">
                    <tr>
                        <td align="left" width="20%" class="label">
                            Course<span class="req">*</span>
                        </td>
                        <td align="left" width="30%">
                            <asp:DropDownList ID="ddlCourse" runat="server" CssClass="dropdownList" AutoPostBack="true"
                                Width="140px" DataValueField="CourseId" DataTextField="CourseName" 
                                onselectedindexchanged="ddlCourse_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td align="left" width="20%" class="label">
                            Stream<span class="req">*</span>
                        </td>
                        <td align="left" width="30%">
                            <asp:TextBox ID="txtStream" runat="server" CssClass="textbox_required" Width="140px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" width="20%" class="label">
                            Capacity<span class="req">*</span>
                        </td>
                        <td align="left" width="30%">
                            <asp:TextBox ID="txtCapacity" runat="server" CssClass="textbox_required" Width="140px"
                                onKeyPress="return NumbersOnly(event)"></asp:TextBox>
                        </td>
                        <td align="left" width="20%" class="label">
                        </td>
                        <td align="left" width="30%">
                            <asp:Button ID="btnSave" runat="server" CssClass="button" Text="Save" 
                                OnClientClick="return Validation()" onclick="btnSave_Click" />
                        </td>
                    </tr>
                </table>
                <br />
                <table width="100%" align="center" class="table">
                    <tr>
                        <td align="center">
                            <asp:GridView ID="dgvStream" runat="server" Width="100%" AllowPaging="false" AutoGenerateColumns="false"
                                DataKeyNames="id" onrowcommand="dgvStream_RowCommand" 
                                onrowdatabound="dgvStream_RowDataBound">
                                <Columns>
                                    <asp:BoundField DataField="CourseName" HeaderText="Course" />
                                    <asp:BoundField DataField="stream_name" HeaderText="Stream" />
                                    <asp:TemplateField HeaderText="Capacity">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtCap" runat="server" CssClass="textbox_required" Width="80px"
                                                Text='<%#Bind("Capacity") %>' onKeyPress="return NumbersOnly(event)"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <asp:Button ID="btnChangeCapacity" runat="server" CssClass="button3" Text="Update Capacity"
                                                CommandName="Capacity" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <HeaderStyle CssClass="HeaderStyle" />
                                <RowStyle CssClass="RowStyle" />
                                <EmptyDataRowStyle CssClass="EditRowStyle" />
                                <AlternatingRowStyle CssClass="AltRowStyle" />
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
