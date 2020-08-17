<%@ Page Language="C#" MasterPageFile="~/MasterAdmin.Master" AutoEventWireup="true"
    CodeBehind="DbBackupRestore.aspx.cs" Inherits="CollegeERP.Common.DbBackupRestore"
    Title="Databse Backup" %>

<%@ Register Src="../UserControl/Message.ascx" TagName="Message" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="title">
        <h5>Databse Backup</h5>
    </div>
    <br />
    <br />
    <div style="width: 800px;">
        <uc3:Message ID="message" runat="server" />
        <br />
        <table width="100%" align="center" class="table">
            <tr>
                <td align="left">
                    <asp:Button ID="btnBackup" runat="server" CssClass="button" Text="Backup Database"
                        OnClientClick="return confirm('Do you want to backup database?');" OnClick="btnBackup_Click" />
                </td>
            </tr>
            <tr><td><br /></td></tr>
            <tr>
                <td>
                    <asp:GridView ID="dgvBackup" runat="server" AutoGenerateColumns="False" Width="100%"
                        GridLines="None" AllowPaging="true" PageSize="10" DataKeyNames="BackupId" OnPageIndexChanging="dgvBackup_PageIndexChanging">
                        <Columns>
                            <asp:TemplateField HeaderText="SL No" ItemStyle-Width="15px">
                                <ItemTemplate>
                                    <%#Container.DataItemIndex+1 %>
                                </ItemTemplate>
                                <ItemStyle Width="52px" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="BackupDate" HeaderText="Backup Date" DataFormatString="{0:dd/MM/yyyy hh:mm tt}" ReadOnly="true" />
                            <asp:BoundField DataField="EmployeeName" HeaderText="Employee Name" ReadOnly="true" />
                            <asp:BoundField DataField="BackupPath" HeaderText="Backup Path" ReadOnly="true" />
                        </Columns>
                        <EmptyDataTemplate>
                            <table style="height: 10px; width: 100%;">
                                <tr class="RowStyle">
                                    <td>Sorry! No Records Found.</td>
                            </table>
                        </EmptyDataTemplate>
                        <HeaderStyle CssClass="HeaderStyle" />
                        <RowStyle CssClass="RowStyle" />
                        <AlternatingRowStyle CssClass="AltRowStyle" />
                        <PagerStyle CssClass="PagerStyle" HorizontalAlign="Left" />
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </div>
    <br />
    <br />
</asp:Content>
