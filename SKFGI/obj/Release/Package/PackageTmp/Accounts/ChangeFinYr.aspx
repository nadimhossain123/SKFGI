﻿<%@ Page Language="C#" MasterPageFile="~/MasterAdmin.Master" AutoEventWireup="true"
    CodeBehind="ChangeFinYr.aspx.cs" Inherits="CollegeERP.Accounts.ChangeFinYr" Title="Change Fin Year" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ToolkitScriptManager ID="toolScript1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="title">
        <h5>
            Change Financial Year / Company</h5>
    </div>
    <asp:UpdatePanel ID="UP1" runat="server">
        <ContentTemplate>
            <div style="width: 600px;">
                <table width="100%" align="center" class="table">
                    <tr>
                        <td align="left" width="40%" class="label">
                            Select Financial Year <span class="req">*</span>
                        </td>
                        <td align="left" width="60%">
                            <asp:DropDownList ID="ddlFinancialYear" runat="server" CssClass="dropdownList" Width="180px"
                                AppendDataBoundItems="true">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr><td colspan="2"><br /></td></tr>
                    <tr>
                        <td align="left" width="40%">
                        </td>
                        <td align="left" width="60%">
                            <asp:Button ID="btnSave" runat="server" CssClass="button" 
                                Text="Change Now" onclick="btnSave_Click" />
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
