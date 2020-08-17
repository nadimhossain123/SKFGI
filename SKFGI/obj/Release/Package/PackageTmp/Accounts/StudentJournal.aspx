<%@ Page Language="C#" MasterPageFile="~/MasterAdmin.Master" AutoEventWireup="true"
 CodeBehind="StudentJournal.aspx.cs" Inherits="CollegeERP.Accounts.StudentJournal" Title="Student Journal" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../UserControl/Message.ascx" TagName="Message" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ToolkitScriptManager ID="toolScript1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="title">
        <h5>
            Student Journal
        </h5>
    </div>
    <div>
         <asp:UpdatePanel ID="UpdatePanelVoucherEntry" runat="server">
                    <ContentTemplate>
                        <uc3:Message ID="Message" runat="server" />
                        <br />
                        <h6 align="left" style="color: #00356A;">
                            Journal Voucher Information</h6>
                        <table width="95%" align="center" class="table">
                            <tbody>
                                <tr>
                                    <td style="width: 10%" class="label" valign="middle" align="left">
                                        Voucher No. :
                                    </td>
                                    <td style="width: 20%" valign="middle" align="left">
                                        <asp:TextBox ID="txtVchNo" CssClass="textbox_pink" runat="server" Width="180px"
                                            Enabled="false"></asp:TextBox>
                                        <input id="hdnJournalID" type="hidden" value="0" runat="server" />
                                        <input id="hdndtlid" type="hidden" value="0" runat="server" />
                                    </td>
                                    <td style="width: 15%" class="label" valign="middle" align="center">
                                        Voucher Date :
                                    </td>
                                    <td style="width: 15%" valign="middle" align="left">
                                        <asp:TextBox ID="txtVchDate" runat="server" CssClass="textbox_pink" Width="120px">
                                        </asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" CssClass="cal_Theme1"
                                            PopupPosition="BottomRight" Format="dd MMM yyyy" TargetControlID="txtVchDate"
                                            OnClientDateSelectionChanged="" Enabled="True">
                                        </asp:CalendarExtender>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                        <h6 align="left" style="color: #00356A;">
                            Transaction</h6>
                        <table width="95%" align="center" class="table">
                            <tr>
                                <td class="label" valign="top">
                                    By/To
                                </td>
                                <td class="label" valign="top">
                                    Ledger Name
                                </td>
                                <td class="label" valign="top">
                                    Cur Balance
                                    <input id="hdnCCApp" type="hidden" runat="server" />
                                </td>
                                <td class="label" valign="top">
                                    Cost Center
                                </td>
                                <td style="width: 10%;" class="label" valign="top">
                                    Reference No.
                                </td>
                                <td style="width: 10%;" class="label" valign="top">
                                    Amount Dr
                                </td>
                                <td style="width: 10%;" class="label" valign="top">
                                    Amount Cr
                                </td>
                                <td style="width: 5%;" valign="top">
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:DropDownList ID="ddlbyto" runat="server" CssClass="dropdownList" Width="40px"
                                        AutoPostBack="True" OnSelectedIndexChanged="ddlbyto_SelectedIndexChanged">
                                        <asp:ListItem Selected="True">To</asp:ListItem>
                                        <asp:ListItem>By</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td valign="top">
                                    <asp:ComboBox ID="ddlledgernm" runat="server" CssClass="WindowsStyle" AutoPostBack="true"
                                        DropDownStyle="DropDown" AutoCompleteMode="SuggestAppend" CaseSensitive="false"
                                        Width="240px" OnSelectedIndexChanged="ddlledgernm_SelectedIndexChanged" DataTextField="LedgerName"
                                        DataValueField="LedgerID">
                                    </asp:ComboBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtbalance" CssClass="textbox" ReadOnly="true" runat="server" Width="100px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlcostcntr" runat="server" CssClass="dropdownList" AutoPostBack="false"
                                        Width="70px">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtrfno" CssClass="textbox" runat="server" Width="100px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtamnrdr" onkeypress="return AmountOnly('txtamnrdr',this);" runat="server"
                                        CssClass="textbox" Width="120px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtamnrcr" onkeypress="return AmountOnly('txtamnrcr',this);" runat="server"
                                        CssClass="textbox" Width="120px">
                                    </asp:TextBox>
                                </td>
                                <td>
                                    <asp:Button ID="btntsctnadd" OnClick="btntsctnadd_Click" runat="server" Text="Add"
                                        CssClass="button"></asp:Button>
                                </td>
                            </tr>
                        </table>
                        <br />
                        <table width="95%" align="center" class="table">
                            <tbody>
                                <tr>
                                    <td align="center" valign="top">
                                        <asp:GridView ID="grdvwtrnsctn" runat="server" Width="100%" AllowSorting="false"
                                            AllowPaging="false" AutoGenerateColumns="False" OnRowEditing="grdvwtrnsctn_RowEditing">
                                            <Columns>
                                                <asp:BoundField DataField="Serial No" HeaderText="Serial No."></asp:BoundField>
                                                <asp:BoundField DataField="By/To" HeaderText="By/To"></asp:BoundField>
                                                <asp:BoundField DataField="Ledger Name" HeaderText="Ledger Name"></asp:BoundField>
                                                <asp:BoundField DataField="Cost Center" HeaderText="Cost Center"></asp:BoundField>
                                                <asp:BoundField DataField="Reference No" HeaderText="Reference No."></asp:BoundField>
                                                <asp:BoundField HtmlEncode="False" DataFormatString="{0:F2}" DataField="Amount Dr"
                                                    HeaderText="Amount DR">
                                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField HtmlEncode="False" DataFormatString="{0:F2}" DataField="Amount Cr"
                                                    HeaderText="Amount CR">
                                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Ledger ID" Visible="False" HeaderText="Ledger ID"></asp:BoundField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="imgbtntrnpopulate" CommandName="Edit" ImageUrl="~/Images/edit_icon.gif"
                                                            runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField Visible="False"></asp:TemplateField>
                                            </Columns>
                                            <HeaderStyle CssClass="HeaderStyle" />
                                            <RowStyle CssClass="RowStyle" />
                                            <AlternatingRowStyle CssClass="AltRowStyle" />
                                            <PagerStyle CssClass="PagerStyle" />
                                        </asp:GridView>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                        <br />
                        <table width="95%" align="center" class="table">
                            <tbody>
                                <tr>
                                    <td style="width: 57%; height: 26px">
                                    </td>
                                    <td style="width: 15%; height: 26px; text-align: right" class="label">
                                        Balance Amount
                                    </td>
                                    <td style="width: 10%; height: 26px">
                                        <asp:TextBox ID="txtblncdr" CssClass="textbox" onkeypress="return AmountOnly('txtblncdr',this);"
                                            runat="server" Width="120px" ReadOnly="true"></asp:TextBox>
                                    </td>
                                    <td style="width: 10%; height: 26px">
                                        <asp:TextBox ID="txtblnccr" CssClass="textbox" onkeypress="return AmountOnly('txtblnccr',this);"
                                            runat="server" Width="120px" ReadOnly="true"></asp:TextBox>
                                    </td>
                                    <td style="width: 8%; height: 26px">
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                        <table width="95%">
                            <tbody>
                                <tr>
                                    <td style="width: 7%; text-align: right" class="label">
                                        Narration :
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtNarration" CssClass="textbox" runat="server" TextMode="MultiLine"
                                            Width="91%" Height="30px" Style="resize: none;"></asp:TextBox>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                        <br />
                        <table align="center" width="95%" class="table">
                            <tbody>
                                <tr>
                                    <td align="center">
                                        <asp:Button ID="btnSave" OnClick="btnSave_Click" runat="server" Text="Save" CssClass="button">
                                        </asp:Button>
                                        &nbsp;
                                        <asp:Button ID="btnReset" OnClick="btnReset_Click" runat="server" Text="Reset" CssClass="button">
                                        </asp:Button>
                                        &nbsp;
                                        <asp:Button ID="btnPrint" runat="server" CssClass="button" Text="Print"></asp:Button>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
    </div>
</asp:Content>
