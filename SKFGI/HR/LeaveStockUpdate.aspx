<%@ Page Title="Leave Stock Update" Language="C#" MasterPageFile="~/MasterAdmin.Master"
    AutoEventWireup="true" CodeBehind="LeaveStockUpdate.aspx.cs" Inherits="CollegeERP.HR.LeaveStockUpdate" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../UserControl/Message.ascx" TagName="Message" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="Script1" runat="server">
    </asp:ScriptManager>
    <div class="title">
        <h5>
            Leave Stock Update</h5>
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
            <div style="width: 400px;">
                <uc3:Message ID="Message" runat="server" />
                <br />
                <table width="100%">
                    <tr>
                        <td align="center">
                            <asp:Button ID="btnUpdate" runat="server" CssClass="button" OnClick="btnUpdate_Click" />
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
