<%@ Page Title="District Master" Language="C#" MasterPageFile="~/MasterAdmin.Master"
    AutoEventWireup="true" CodeBehind="DistrictMaster.aspx.cs" Inherits="CollegeERP.Common.DistrictMaster" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../UserControl/Message.ascx" TagName="Message" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
 <script type="text/javascript">
     function Validation() {
         if (!checkforvaliddata(eval('document.forms[0].' + '<%=ddlstate.ClientID%>'), "State", 0)) return false;
         if (!checkforvaliddata(eval('document.forms[0].' + '<%=txtDistrict.ClientID%>'), "District Name", 1)) return false;

         return true;
     }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ToolkitScriptManager ID="ToolScript1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="title">
        <h5>
            District Master</h5>
    </div>
    <asp:UpdatePanel ID="Up1" runat="server">
        <ContentTemplate>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div style="width: 750px;">
        <uc3:Message ID="Message" runat="server" />
        <br />
        <table align="center" width="100%" class="table">
            <tr>
                <td align="left" width="20%" class="label">
                    State <span class="req">*</span>
                </td>
                <td align="left" width="30%">
                    
                    <asp:DropDownList ID="ddlstate" runat="server" CssClass="dropdownList" Width="140px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td align="left" width="20%" class="label">
                    District Name<span class="req">*</span>
                </td>
                <td align="left" width="30%">
                    <asp:TextBox ID="txtDistrict" runat="server" CssClass="textbox_required" Width="140px"
                        MaxLength="99"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <br />
                </td>
            </tr>
            <tr>
                <td align="center" colspan="4">
                    <asp:Button ID="btnSave" runat="server" CssClass="button" Text="Save" OnClientClick="return Validation()"
                        OnClick="btnSave_Click" />
                    &nbsp;
                    <asp:Button ID="btnCancel" runat="server" CssClass="button" Text="Reset" OnClick="btnCancel_Click" />
                </td>
            </tr>
        </table>
        <br />
        <table width="100%" align="center" class="table">
            <tr>
                <td align="left" width="10%" class="label">
                    District Name
                </td>
                <td align="left" width="25%">
                    <asp:TextBox ID="txtdistrictsearch" runat="server" CssClass="textbox" Width="140px"
                        MaxLength="99"></asp:TextBox>
                </td>
                 <td align="left" width="10%" class="label">
                    State 
                </td>
                 <td align="left" width="25%">
                    <asp:DropDownList ID="ddlstatesearch" runat="server" CssClass="dropdownList" Width="140px">
                    </asp:DropDownList>
                </td>
                  
                <td align="left">
                    <asp:Button ID="Button1" runat="server" CssClass="button" Text="Search" OnClick="Button1_Click" />
                </td>
            </tr>
            <tr>
                <td colspan="3">
                </td>
            </tr>
            <tr>
                <td align="center" colspan="5">
                    <asp:GridView ID="dgvState" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                        Width="100%" DataKeyNames="DistrictID" PageSize="15" OnPageIndexChanging="dgvState_PageIndexChanging"
                        OnRowCommand="dgvState_RowCommand" EnableModelValidation="True">
                        <Columns>
                            <asp:BoundField DataField="SlNo" HeaderText="Sl No">
                                <HeaderStyle Width="30px" />
                                <ItemStyle Width="30px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="State" HeaderText="State Name" />
                             <asp:BoundField DataField="District" HeaderText="District Name" />
                            <asp:TemplateField ShowHeader="false">
                                <ItemTemplate>
                                    <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit_icon.gif" CommandName="Ed"
                                        CommandArgument='<%#Eval("DistrictID") %>' />
                                </ItemTemplate>
                                <HeaderStyle Width="20px" />
                                <ItemStyle Width="20px" />
                            </asp:TemplateField>
                        </Columns>
                        <EmptyDataTemplate>
                            <table style="height: 10px; width: 100%;">
                                <tr class="RowStyle">
                                    <td>
                                        Sorry! No Records Found
                                    </td>
                            </table>
                        </EmptyDataTemplate>
                        <HeaderStyle CssClass="HeaderStyle" />
                        <RowStyle CssClass="RowStyle" />
                        <EmptyDataRowStyle CssClass="EditRowStyle" />
                        <AlternatingRowStyle CssClass="AltRowStyle" />
                        <PagerStyle CssClass="PagerStyle" />
                    </asp:GridView>
                    <br />
                    <br />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
