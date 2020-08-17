<%@ Page Language="C#" MasterPageFile="~/MasterAdmin.Master" AutoEventWireup="true"
    CodeBehind="TrialBalance.aspx.cs" Inherits="CollegeERP.Accounts.TrialBalance"
    Title="Trial Balance" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../UserControl/Message.ascx" TagName="Message" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ToolkitScriptManager ID="toolScript1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="title">
        <h5>
            Trial Balance</h5>
    </div>
    <asp:UpdatePanel ID="UpdatePanel" runat="server">
        <ContentTemplate>
            <div style="width: 670px">
                <br />
                <table width="100%" align="center" class="table">
                    <tr>
                        <td align="left" class="label" width="30%">
                            Trial Balance Type
                        </td>
                        <td align="left" width="70%">
                            <asp:DropDownList ID="ddlBalanceType" CssClass="dropdownList" runat="server" Width="160px"
                                OnSelectedIndexChanged="ddlBalanceType_SelectedIndexChanged" AutoPostBack="True">
                                <asp:ListItem Value="CLOSING">CLOSING</asp:ListItem>
                                <asp:ListItem>OPENING</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" class="label" width="30%">
                            Till Date
                        </td>
                        <td align="left" width="70%">
                            <asp:TextBox ID="txtToDate" CssClass="textbox" runat="server" Width="130px"></asp:TextBox>
                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" CssClass="cal_Theme1"
                                PopupPosition="BottomRight" Format="dd MMM yyyy" TargetControlID="txtToDate"
                                OnClientDateSelectionChanged="" Enabled="True">
                            </asp:CalendarExtender>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" class="label" width="30%">
                            Financial Year
                        </td>
                        <td align="left" width="70%">
                            <asp:DropDownList ID="ddlFinancialYear" runat="server" CssClass="dropdownList" Width="160px">
                                <asp:ListItem Value="0">Select</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" class="label" width="30%">
                            A/c Group
                        </td>
                        <td align="left" width="70%">
                            <asp:ComboBox ID="ddlGroupVw" runat="server" CssClass="WindowsStyle" AutoPostBack="true" AppendDataBoundItems="true" onselectedindexchanged="ddlGroupVw_SelectedIndexChanged"
                                DropDownStyle="DropDown" AutoCompleteMode="SuggestAppend" CaseSensitive="false"
                                Width="160px">
                                <asp:ListItem Value="0">Select</asp:ListItem>
                            </asp:ComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" class="label" width="30%">
                            A/c Sub Group
                        </td>
                        <td align="left" width="70%">
                            <asp:ComboBox ID="ddlSubGroupVw" runat="server" CssClass="WindowsStyle" AutoPostBack="false" AppendDataBoundItems="true"
                                DropDownStyle="DropDown" AutoCompleteMode="SuggestAppend" CaseSensitive="false"
                                Width="160px">
                                <asp:ListItem Value="0">Select</asp:ListItem>
                            </asp:ComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td width="30%"></td>
                        <td align="left" width="70%">
                            <asp:Button ID="btnSearch" OnClick="btnSearch_Click" runat="server" Text="Search"
                                CssClass="button"></asp:Button>
                            <asp:Button ID="btnPrint" runat="server" CssClass="button" Text="Print"></asp:Button>
                        </td>
                    </tr>
                </table>
            </div>
            <br />
            <table width="85%" align="center" class="table">
                <tr>
                    <td align="center">
                        <asp:GridView ID="gvTrialBalnce" runat="server" AllowPaging="false" AllowSorting="false"
                            Width="100%" AutoGenerateColumns="False" GridLines="None">
                            <Columns>
                                <asp:BoundField DataField="LedgerID_FK" HeaderText="Sl.No."></asp:BoundField>
                                <asp:BoundField HtmlEncode="False" DataField="LedgerName" HeaderText="Head of Accounts">
                                </asp:BoundField>
                                <asp:BoundField HtmlEncode="False" DataFormatString="{0:F2}" DataField="Debit" HeaderText="Debit">
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField HtmlEncode="False" DataFormatString="{0:F2}" DataField="Credit" HeaderText="Credit">
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField HtmlEncode="False" Visible="false" DataField="" HeaderText="Head of Accounts">
                                </asp:BoundField>
                                <asp:BoundField HtmlEncode="False" Visible="false" DataField="" HeaderText="Head of Accounts">
                                </asp:BoundField>
                                <asp:BoundField HtmlEncode="False" Visible="false" DataField="" HeaderText="Head of Accounts">
                                </asp:BoundField>
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
