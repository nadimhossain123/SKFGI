<%@ Page Title="P/L Transfer" Language="C#" MasterPageFile="~/MasterAdmin.Master"
    AutoEventWireup="true" CodeBehind="PLTransfer.aspx.cs" Inherits="SKFGI.Accounts.PLTransfer" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../UserControl/Message.ascx" TagName="Message" TagPrefix="uc3" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ToolkitScriptManager ID="Tool1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="title">
        <h5>
            P/L Transfer</h5>
    </div>
    <asp:UpdatePanel ID="UP1" runat="server">
        <ContentTemplate>
            <uc3:Message ID="Message" runat="server" />
            <br />
            <div style="width: 97%;">
                <h6 align="left" style="color: #00356A;">
                    P/L Balance</h6>
                <asp:Literal ID="ltrBalanceSheetHeading" runat="server" Mode="PassThrough"></asp:Literal>
                <br />
                <br />
                <asp:GridView ID="gvBalanceSheet" runat="server" AutoGenerateColumns="False" CellPadding="0"
                    Width="97%" OnRowDataBound="gvBalanceSheet_RowDataBound">
                    <Columns>
                        <asp:BoundField DataField="LiabilitiesName" HeaderText="Liabilities" HtmlEncode="False">
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Amount">
                            <ItemTemplate>
                                <asp:Label ID="lblamount1" runat="server" Text='<%# Bind("LiabilitiesAmount", "{0:n}") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="LiabilitiesTotalAmount" HeaderText="Total" DataFormatString="{0:n}"
                            HtmlEncode="False">
                            <ItemStyle Font-Bold="True" HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="AssetsName" HeaderText="Assests" HtmlEncode="False">
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Amount">
                            <ItemTemplate>
                                <asp:Label ID="lblamnt2" runat="server" Text='<%# Bind("AssetsAmount", "{0:n}") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="AssetsTotalAmount" HeaderText="Total" DataFormatString="{0:n}"
                            HtmlEncode="False">
                            <ItemStyle Font-Bold="True" HorizontalAlign="Right" />
                        </asp:BoundField>
                    </Columns>
                    <HeaderStyle CssClass="HeaderStyle" />
                    <RowStyle CssClass="RowStyle" />
                    <AlternatingRowStyle CssClass="AltRowStyle" />
                    <PagerStyle CssClass="PagerStyle" />
                </asp:GridView>
            </div>
            <br />
            <div style="width: 97%;">
                <h6 align="left" style="color: #00356A;">
                    Transfer Details</h6>
                <table width="80%" align="center" class="table">
                    <tr>
                        <td align="left" class="label">
                            Ledger Name
                        </td>
                        <td align="left" class="label">
                            DR/CR
                        </td>
                        <td align="left" class="label">
                            Amt.
                        </td>
                        <td align="left" class="label">
                            Transfer Date
                        </td>
                    </tr>
                    <tr>
                        <td class="label" align="left" valign="top">
                            <asp:ComboBox ID="ddlLedg" runat="server" CssClass="WindowsStyle" DropDownStyle="DropDown"
                                DataTextField="LedgerName" DataValueField="LedgerID" AutoCompleteMode="SuggestAppend"
                                CaseSensitive="false" Width="240px">
                            </asp:ComboBox>
                        </td>
                        <td class="label" align="left">
                            <asp:DropDownList ID="ddlDRCR" runat="server" CssClass="dropdownList" Width="100px">
                                <asp:ListItem Value="RECEIVE" Text="DR"></asp:ListItem>
                                <asp:ListItem Value="PAYMENT" Text="CR"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td class="label" align="left">
                            <asp:TextBox ID="txtAmt" CssClass="textbox" onkeypress="return AmountOnly('txtAmt',this);"
                                runat="server"></asp:TextBox>
                        </td>
                        <td class="label" align="left">
                            <asp:TextBox ID="txtTransferDate" CssClass="textbox" runat="server"></asp:TextBox>
                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" PopupPosition="BottomRight"
                                Format="dd MMM yyyy" TargetControlID="txtTransferDate" OnClientDateSelectionChanged=""
                                Enabled="True">
                            </asp:CalendarExtender>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" class="label" valign="top">
                             To P/L Adjust Ledger</td>
                        <td align="left" class="label">
                            &nbsp;</td>
                        <td align="left" class="label">
                            &nbsp;</td>
                        <td align="left" class="label">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td align="left" class="label" valign="top">
                            <asp:ComboBox ID="ddlAdjustmentLedg" runat="server" AutoCompleteMode="SuggestAppend" 
                                CaseSensitive="false" CssClass="WindowsStyle" DataTextField="LedgerName" 
                                DataValueField="LedgerID" DropDownStyle="DropDown" Width="240px">
                            </asp:ComboBox>
                        </td>
                        <td align="left" class="label">
                            &nbsp;</td>
                        <td align="left" class="label">
                            &nbsp;</td>
                        <td align="left" class="label">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td align="left" class="label">
                            Narration
                        </td>
                        <td class="label" align="left">
                        </td>
                    </tr>
                    <tr>
                        <td class="label" align="left" colspan="3">
                            <asp:TextBox ID="txtNarration" CssClass="textbox" TextMode="MultiLine" Height="50px"
                                Width="95%" MaxLength="500" runat="server"></asp:TextBox>
                        </td>
                        <td class="label" align="left">
                            <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="button" OnClick="btnSave_Click"
                                Width="70px"></asp:Button>
                        </td>
                    </tr>
                </table>
            </div>
            <div style="width: 97%;">
                <h6 align="left" style="color: #00356A;">
                    P/L Balance</h6>
                <asp:Literal ID="Literal1" runat="server" Mode="PassThrough"></asp:Literal>
                <br />
                <br />
                <asp:GridView ID="gvCBVView" runat="server" AutoGenerateColumns="False" Width="97%"
                    GridLines="None" OnRowDeleting="gvCBVView_RowDeleting" AllowSorting="false" DataKeyNames="ALTransactionID">
                    <Columns>
                        <asp:BoundField HtmlEncode="False" DataFormatString="{0:dd MMM yyyy}" DataField="TransactionDate"
                            HeaderText="Transaction Date"></asp:BoundField>
                        <asp:BoundField DataField="FinYear" HeaderText="Financial Year"></asp:BoundField>
                        <asp:BoundField DataField="TransactionCode" HeaderText="Transfer Code"></asp:BoundField>
                        <asp:BoundField DataField="LedgerName" HeaderText="Ledger Name"></asp:BoundField>
                        <asp:BoundField DataFormatString="{0:F2}" DataField="DRAmount" HeaderText="DR Amount">
                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataFormatString="{0:F2}" DataField="CRAmount" HeaderText="CR Amount">
                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        </asp:BoundField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:ImageButton ID="btnDelete" CommandName="Delete" ImageUrl="~/Images/delete_icon.gif"
                                    runat="server" OnClientClick="return confirm('Are You Sure?')" /></ItemTemplate>
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
                    <PagerSettings Mode="Numeric" PageButtonCount="8" />
                    <HeaderStyle CssClass="HeaderStyle" />
                    <RowStyle CssClass="RowStyle" />
                    <AlternatingRowStyle CssClass="AltRowStyle" />
                    <PagerStyle CssClass="PagerStyle" />
                </asp:GridView>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
