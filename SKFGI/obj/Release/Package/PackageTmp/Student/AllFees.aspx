<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAdmin.Master" AutoEventWireup="true"
    CodeBehind="AllFees.aspx.cs" Inherits="CollegeERP.Student.AllFees" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../UserControl/Message.ascx" TagName="Message" TagPrefix="uc3" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script language="javascript" type="text/javascript" src="../Scripts/ssjscript.js"></script>
    <asp:ToolkitScriptManager ID="ToolScript1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="title">
        <h5>
            ALL Fees</h5>
    </div>
    <asp:UpdatePanel ID="Up1" runat="server">
        <ContentTemplate>
            <div style="width:800px;">
                <table width="100%" align="center" class="table">
                    <tr>
                        <td align="left">
                            <asp:DropDownList ID="ddlCourse" runat="server" AutoPostBack="true" CssClass="dropdownList"
                                Width="140px" DataValueField="CourseId" DataTextField="CourseName" 
                                onselectedindexchanged="ddlCourse_SelectedIndexChanged" >
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr><td><br /></td></tr>
                    <tr>
                        <td align="center">
                            <asp:GridView ID="dgvAllFees" runat="server" AutoGenerateColumns="false" Width="100%"
                                AllowPaging="true" PageSize="100" DataKeyNames="id" 
                                onrowediting="dgvAllFees_RowEditing">
                                <Columns>
                                    <asp:BoundField DataField="batch_Name" HeaderText=" Batch Name " />
                                    <asp:BoundField DataField="CourseName" HeaderText=" Course Name " />
                                    <asp:BoundField DataField="stream_name" HeaderText=" Stream Name " />
                                    <asp:BoundField DataField="fees_name" HeaderText=" Fees Name " />
                                    <asp:TemplateField ShowHeader="false">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit_icon.gif" CommandName="Edit" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
