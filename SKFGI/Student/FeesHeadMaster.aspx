<%@ Page Language="C#" MasterPageFile="~/MasterAdmin.Master" AutoEventWireup="true"
    CodeBehind="FeesHeadMaster.aspx.cs" Inherits="CollegeERP.Student.FeesHeadMaster"
    Title="Fees Head Master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../UserControl/Message.ascx" TagName="Message" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">
        function Validation()
        {
            if (!checkforvaliddata(eval('document.forms[0].' + '<%=txtFeesHead.ClientID%>'), "Head Name", 1)) return false;
            if (!checkforvaliddata(eval('document.forms[0].' + '<%=ddlAssestLedger.ClientID%>'), "Assets Ledger", 0)) return false;
            if (!checkforvaliddata(eval('document.forms[0].' + '<%=ddlIncomeLedger.ClientID%>'), "Income Ledger", 0)) return false;
            return true;
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ToolkitScriptManager ID="ToolScript1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="title">
        <h5>
            Fees Head Master</h5>
    </div>
    <asp:UpdatePanel ID="Up1" runat="server">
        <ContentTemplate>
            <div style="width: 750px;">
                <uc3:Message ID="Message" runat="server" />
                <br />
                <table align="center" width="100%" class="table">
                    <tr>
                        <td align="left" width="30%" class="label">
                            Head Name<span class="req">*</span>
                        </td>
                        <td align="left" width="70%">
                            <asp:TextBox ID="txtFeesHead" runat="server" CssClass="textbox_required" Width="220px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" width="30%" class="label">
                            Head Type
                        </td>
                        <td align="left" width="70%">
                            <asp:RadioButtonList ID="rbtnListHeadType" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="SEM" Text="Semester" Selected="True"></asp:ListItem>
                                <asp:ListItem Value="HOS" Text="Hostel"></asp:ListItem>
                                <asp:ListItem Value="OTH" Text="Other"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" width="30%" class="label">
                            Assets Ledger<span class="req">*</span>
                        </td>
                        <td align="left" width="70%">
                            <asp:DropDownList ID="ddlAssestLedger" runat="server" CssClass="dropdownList" Width="220px"
                                DataValueField="LedgerID" DataTextField="LedgerName">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" width="30%" class="label">
                            Income Ledger<span class="req">*</span>
                        </td>
                        <td align="left" width="70%">
                            <asp:DropDownList ID="ddlIncomeLedger" runat="server" CssClass="dropdownList" Width="220px"
                                DataValueField="LedgerID" DataTextField="LedgerName">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" width="30%" class="label">
                            Refundable
                        </td>
                        <td align="left" width="70%">
                            <asp:CheckBox ID="ChkIsRefundable" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td align="left" width="30%" class="label">
                            One Time Applicable
                        </td>
                        <td align="left" width="70%">
                            <asp:CheckBox ID="ChkIsOneTimeApplicable" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td align="center" colspan="2">
                            <asp:Button ID="btnSave" runat="server" CssClass="button" Text="Save" OnClientClick="return Validation()"
                                OnClick="btnSave_Click" />
                            &nbsp;
                            <asp:Button ID="btnCancel" runat="server" CssClass="button" Text="Reset" OnClick="btnCancel_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td align="left" colspan="2" class="label">
                            (During Editing, if it is found that there is any data dependency on this head then
                            its type will not be changed from Semester to Other and vice-versa.)
                        </td>
                    </tr>
                </table>
                <br />
                <table width="100%" align="center" class="table">
                    <tr>
                        <td align="center">
                            <asp:GridView ID="dgvFeesHead" runat="server" AutoGenerateColumns="false" AllowPaging="false"
                                Width="100%" DataKeyNames="id" OnRowEditing="dgvFeesHead_RowEditing">
                                <Columns>
                                    <asp:BoundField DataField="fees" HeaderText="Fees Head" />
                                    <asp:BoundField DataField="FeesHeadType" HeaderText="Head Type" />
                                    <asp:TemplateField HeaderText="Refundable" ItemStyle-Width="60px" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="ChkIsRefundable" runat="server" Enabled="false" Checked='<%# Convert.ToBoolean(Eval("IsRefundable")) %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="One Time" ItemStyle-Width="60px" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="ChkIsOneTimeApplicable" runat="server" Enabled="false" Checked='<%# Convert.ToBoolean(Eval("IsOneTimeApplicable")) %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ShowHeader="false">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit_icon.gif" CommandName="Edit" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <HeaderStyle CssClass="HeaderStyle" />
                                <RowStyle CssClass="RowStyle" />
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
