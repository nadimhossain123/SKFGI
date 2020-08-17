<%@ Page Language="C#" MasterPageFile="~/MasterAdmin.Master" AutoEventWireup="true"
    CodeBehind="PaybandMaster.aspx.cs" Inherits="CollegeERP.Common.PaybandMaster"
    Title="Payband" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../UserControl/Message.ascx" TagName="Message" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">
        function Validation()
        {
            if (!checkforvaliddata(eval('document.forms[0].' + '<%=txtPayband.ClientID%>'), "Payband", 1)) return false;
            if (!checkforvaliddata(eval('document.forms[0].' + '<%=txtScaleFrom.ClientID%>'), "Scale From", 1)) return false;
            if (!checkforvaliddata(eval('document.forms[0].' + '<%=txtScaleTo.ClientID%>'), "Scale To", 1)) return false;
            if (!checkforvaliddata(eval('document.forms[0].' + '<%=txtGradePay.ClientID%>'), "Grade Pay", 1)) return false;
            return true;
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ToolkitScriptManager ID="ToolScript1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="title">
        <h5>
            Payband</h5>
    </div>
    <asp:UpdatePanel ID="Up1" runat="server">
        <ContentTemplate>
            <div style="width: 750px;">
                <uc3:Message ID="Message" runat="server" />
                <br />
                <table align="center" width="100%" class="table">
                    <tr>
                        <td align="left" width="20%" class="label">
                            Payband Name<span class="req">*</span>
                        </td>
                        <td align="left" width="30%">
                            <asp:TextBox ID="txtPayband" runat="server" CssClass="textbox_required" Width="140px"></asp:TextBox>
                        </td>
                        <td align="left" width="20%" class="label">
                            Scale From<span class="req">*</span>
                        </td>
                        <td align="left" width="30%">
                            <asp:TextBox ID="txtScaleFrom" runat="server" CssClass="textbox_required" Width="140px"
                                onkeypress="return AmountOnly('txtScaleFrom',this);"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" width="20%" class="label">
                            Scale To<span class="req">*</span>
                        </td>
                        <td align="left" width="30%">
                            <asp:TextBox ID="txtScaleTo" runat="server" CssClass="textbox_required" Width="140px"
                                onkeypress="return AmountOnly('txtScaleTo',this);"></asp:TextBox>
                        </td>
                        <td align="left" width="20%" class="label">
                            Grade Pay<span class="req">*</span>
                        </td>
                        <td align="left" width="30%">
                            <asp:TextBox ID="txtGradePay" runat="server" CssClass="textbox_required" Width="140px"
                                onkeypress="return AmountOnly('txtGradePay',this);"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td align="center" colspan="4">
                            <asp:Button ID="btnSave" runat="server" CssClass="button" Text="Save" 
                                OnClientClick="return Validation()" onclick="btnSave_Click" />
                            &nbsp;
                            <asp:Button ID="btnCancel" runat="server" CssClass="button" Text="Reset" 
                                onclick="btnCancel_Click" />
                        </td>
                    </tr>
                </table>
                <br />
                <table width="100%" align="center" class="table">
                    <tr>
                        <td align="center">
                            <asp:GridView ID="dgvPayband" runat="server" AutoGenerateColumns="false" AllowPaging="false"
                                Width="100%" DataKeyNames="PayBandId" onrowediting="dgvPayband_RowEditing">
                                <Columns>
                                    <asp:BoundField DataField="PayBandName" HeaderText="Payband Name" />
                                    <asp:BoundField DataField="Scale" HeaderText="Payband Scale" />
                                    <asp:BoundField DataField="GradePay" HeaderText="Grade Pay" />
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
