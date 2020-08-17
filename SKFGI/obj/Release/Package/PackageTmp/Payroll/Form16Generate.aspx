<%@ Page Language="C#" MasterPageFile="~/MasterAdmin.Master" AutoEventWireup="true"
    CodeBehind="Form16Generate.aspx.cs" Inherits="CollegeERP.Payroll.Form16Generate"
    Title="Generate Form16" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../UserControl/Message.ascx" TagName="Message" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ToolkitScriptManager ID="ToolScript1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="title">
        <h5>
            Form16 Generation</h5>
    </div>
    <asp:UpdatePanel ID="UP1" runat="server">
        <ContentTemplate>
            <br />
            <uc3:Message ID="Message" runat="server" />
            <br />
            <center>
                <div style="width: 620px;">
                    <table width="100%" align="center" class="table">
                        <tr>
                            <td align="left" width="20%" class="label">
                                Employee :<span class="req"></span>
                            </td>
                            <td align="left" width="30%">
                                <asp:ComboBox ID="ddlEmployee" runat="server" CssClass="WindowsStyle" DropDownStyle="DropDown"
                                AutoCompleteMode="SuggestAppend" CaseSensitive="false" Width="260px" DataValueField="EmployeeId"
                                DataTextField="FullName">
                                </asp:ComboBox>
                            </td>
                            <td align="left" width="20%" class="label">
                                Financial Year:<span class="req"></span>
                            </td>
                            <td align="left" width="30%">
                                <asp:Literal ID="ltrFinancialYear" runat="server" Mode="PassThrough"></asp:Literal>
                            </td>
                            </tr>
                            <tr>
                            <td align="right" colspan="4">
                                <asp:Button ID="btnGenerate" runat="server" Text="Generate" CssClass="button" 
                                    onclick="btnGenerate_Click" />&nbsp;
                            </td>
                        </tr>
                    </table>
                </div>
            </center>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
