<%@ Page Language="C#" MasterPageFile="~/MasterAdmin.Master" AutoEventWireup="true"
    CodeBehind="HostelFees.aspx.cs" Inherits="CollegeERP.Student.HostelFees" Title="Hostel Fees" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../UserControl/Message.ascx" TagName="Message" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">
        function Validation()
        {
             if (!checkforvaliddata(eval('document.forms[0].' + '<%=ddlBatch.ClientID%>'), "Batch", 0)) return false; 
             if (!checkforvaliddata(eval('document.forms[0].' + '<%=txtFeesName.ClientID%>'), "Fees Name", 1)) return false;
             return confirm('Are You Sure?');           
        }  
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ToolkitScriptManager ID="ToolScript1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="title">
        <h5>
            Hostel Fees
        </h5>
    </div>
    <asp:UpdatePanel ID="Up1" runat="server">
        <ContentTemplate>
            <h6 align="left" style="color: #00356A;">
                Hostel Fees Configuration
            </h6>
            <div style="width: 530px;">
                <uc3:Message ID="Message" runat="server" />
                <br />
                <table width="100%" align="center" class="table">
                    <tr>
                        <td align="left" width="30%" class="label">
                            Batch <span class="req">*</span>
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="ddlBatch" runat="server" Width="192px" CssClass="dropdownList"
                                DataValueField="id" DataTextField="batch_name">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" width="30%" class="label">
                            Fees Name<span class="req">*</span>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtFeesName" runat="server" CssClass="textbox_required" Width="192px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2"><br /></td>
                    </tr>
                    <tr>
                        <td align="center" colspan="2">
                            <asp:GridView ID="dgvFeesHead" runat="server" Width="100%" AutoGenerateColumns="false"
                                GridLines="None" AllowPaging="false" DataKeyNames="id">
                                <Columns>
                                    <asp:BoundField DataField="fees" HeaderText="Fees Head" />
                                    <asp:TemplateField HeaderText="Amount" ItemStyle-Width="90px">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtAmount" runat="server" CssClass="textbox_green" Text='<%#Bind("amount") %>'
                                                onkeypress="return AmountOnly('txtAmount',this);" style="text-align:right; padding-right:6px;"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <HeaderStyle CssClass="HeaderStyle" />
                                <RowStyle CssClass="RowStyle" />
                                <AlternatingRowStyle CssClass="AltRowStyle" />
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2"><br /></td>
                    </tr>
                    <tr>
                        <td align="center" colspan="2">
                            <asp:Button ID="btnSave" runat="server" CssClass="button" Text="Save" OnClientClick="return Validation()"
                                OnClick="btnSave_Click" />
                            &nbsp;
                            <asp:Button ID="btnCancel" runat="server" CssClass="button" Text="Reset" OnClick="btnCancel_Click" />
                        </td>
                    </tr>
                </table>
            </div>
            <br />
            <h6 align="left" style="color: #00356A;">
                All Hostel Fees
            </h6>
            <div style="width: 530px">
                <table width="100%" align="center" class="table">
                    <tr>
                        <td align="center">
                            <asp:GridView ID="dgvFeesList" runat="server" Width="100%" AutoGenerateColumns="false"
                                GridLines="None" AllowPaging="false" DataKeyNames="id" OnRowEditing="dgvFeesList_RowEditing">
                                <Columns>
                                    <asp:BoundField DataField="batch_name" HeaderText="Batch" />
                                    <asp:BoundField DataField="fees_name" HeaderText="Fees Name" />
                                    <asp:TemplateField ShowHeader="false">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit_icon.gif" CommandName="Edit" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <HeaderStyle CssClass="HeaderStyle" />
                                <RowStyle CssClass="RowStyle" />
                                <AlternatingRowStyle CssClass="AltRowStyle" />
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
