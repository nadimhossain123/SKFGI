<%@ Page Language="C#" MasterPageFile="~/MasterAdmin.Master" AutoEventWireup="true"
    CodeBehind="ITaxInvestmentHeads.aspx.cs" Inherits="CollegeERP.Payroll.ITaxInvestmentHeads"
    Title="ITax Investment Details" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../UserControl/Message.ascx" TagName="Message" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">
        function Validation()
        {
            if ((document.getElementById('<%=ddlSection.ClientID%>').selectedIndex == 0)) {
                alert("Select Section Name");
            return false;
            }
            else if (document.getElementById('<%=txtITaxInvestmentHeadName.ClientID%>').value == '') {
            alert("Enter Investment Name");
                return false;
            }
            else {return true;}
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ToolkitScriptManager ID="ToolScript1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="title">
        <h5>
            Income Tax Investment Heads</h5>
    </div>
    <asp:UpdatePanel ID="UP1" runat="server">
        <ContentTemplate>
            <br />
            <uc3:Message ID="Message" runat="server" />
            <br />
            <center>
                <div style="width: 680px;">
                    <table width="100%" align="center" class="table">
                        <tr>
                            <td align="left" width="10%" class="label">
                                Section<span class="req">*</span>
                            </td>
                            <td align="left" width="20%">
                                <asp:DropDownList ID="ddlSection" runat="server" CssClass="dropdownList" Width="180px"
                                    DataValueField="ITaxSectionId" DataTextField="ITaxSectionName" AutoPostBack="true"
                                    OnSelectedIndexChanged="ddlSection_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                            <td width="25%" align="center" class="label">
                                Investment Name<span class="req">*</span>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtITaxInvestmentHeadName" runat="server" CssClass="textbox_required"
                                    Width="200px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" colspan="3" class="label">
                            <asp:RadioButton ID="rbAddition" runat="server" Text="Addition" GroupName="Flag" />
                            &nbsp;&nbsp;
                            <asp:RadioButton ID="rbDeduction" runat="server" Text="Deduction" GroupName="Flag" />
                            </td>
                            <td align="left">
                                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="button" OnClientClick="javascript:return Validation()"
                                    OnClick="btnSave_Click" />&nbsp;
                                <asp:Button ID="btnCancel" runat="server" CssClass="button" Text="Reset" OnClick="btnCancel_Click" />
                            </td>
                        </tr>
                    </table>
                    <br />
                    <table width="100%" align="center">
                        <tr>
                            <td align="center">
                                <asp:GridView ID="dgvITaxInvestment" runat="server" AutoGenerateColumns="false" Width="100%"
                                    AllowPaging="false" DataKeyNames="ITaxInvestmentHeadId" OnRowDeleting="dgvITaxInvestment_RowDeleting"
                                    OnRowEditing="dgvITaxInvestment_RowEditing" GridLines="None">
                                    <Columns>
                                        <asp:BoundField DataField="ITaxSectionName" HeaderText="Section Name" />
                                        <asp:BoundField DataField="ITaxInvestmentHeadName" HeaderText="Investment Details" />
                                         <asp:BoundField DataField="ITaxType" HeaderText="Investment Type" />
                                        <asp:TemplateField ShowHeader="false">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit_icon.gif" CommandName="Edit" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ShowHeader="false">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/Images/delete_icon.gif"
                                                    CommandName="Delete" OnClientClick="return confirm('Are You Sure?');" />
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
            </center>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
