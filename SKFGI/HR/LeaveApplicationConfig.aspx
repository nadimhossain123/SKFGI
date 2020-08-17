<%@ Page Title="Leave Application Config" Language="C#" MasterPageFile="~/MasterAdmin.Master"
    AutoEventWireup="true" CodeBehind="LeaveApplicationConfig.aspx.cs" Inherits="CollegeERP.HR.LeaveApplicationConfig" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../UserControl/Message.ascx" TagName="Message" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">
        function Validation() {
            if (!checkforvaliddata(eval('document.forms[0].' + '<%=txtMaxDayLimit.ClientID%>'), "Max Day Limit", 1)) return false;
            return true;
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="Script1" runat="server">
    </asp:ScriptManager>
    <div class="title">
        <h5>
            Leave Application Config</h5>
    </div>
    <asp:UpdatePanel ID="UP1" runat="server">
        <ContentTemplate>
            <div style="width: 400px;">
                <uc3:Message ID="Message" runat="server" />
                <br />
                <table width="100%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td width="30%" class="label">
                            Max Day Limit:<span class="req">*</span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtMaxDayLimit" runat="server" CssClass="textbox_required" Width="120px"
                                MaxLength="3" onKeyPress="return NumbersOnly(event)"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            <asp:Button ID="btnSave" runat="server" CssClass="button" Text="Update" OnClientClick="return Validation();"
                                OnClick="btnSave_Click" />
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
