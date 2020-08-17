<%@ Page Language="C#" MasterPageFile="~/MasterAdmin.Master" AutoEventWireup="true"
    CodeBehind="GeneralLedger.aspx.cs" Inherits="CollegeERP.Accounts.GeneralLedger"
    Title="General Ledger" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../UserControl/Message.ascx" TagName="Message" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">
        function Validation()
        {
            if (!checkforvaliddata(eval('document.forms[0].' + '<%=txtLedgerName.ClientID%>'), "Ledger Name", 1)) return false;
            if (!checkforvaliddata(eval('document.forms[0].' + '<%=ddlLedgerType.ClientID%>'), "Ledger Type", 0)) return false;
            if (!checkforvaliddata(eval('document.forms[0].' + '<%=txtOpeningBalance.ClientID%>'), "Opening Balance", 1)) return false;
            return confirm('Are You Sure?');
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ToolkitScriptManager ID="toolScript1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="title">
        <h5>
            General Ledger</h5>
    </div>
    <asp:UpdatePanel ID="UP1" runat="server">
        <ContentTemplate>
            <uc3:Message ID="Message" runat="server" />
            <br />
            <h6 align="left" style="color: #00356A;">
                General Ledger Entry</h6>
            <div style="width: 840px;">
                <table width="100%" align="center" class="table">
                    <tr>
                        <td align="left" width="20%" class="label">
                            Ledger Name<span class="req">*</span>
                        </td>
                        <td align="left" width="30%">
                            <asp:TextBox ID="txtLedgerName" runat="server" CssClass="textbox_required" Width="150px"></asp:TextBox>
                        </td>
                        <td align="left" width="20%" class="label">
                            Opening Balance Date
                        </td>
                        <td align="left" width="30%">
                            <asp:TextBox ID="txtOpBalDate" runat="server" CssClass="textbox_required" Width="150px"></asp:TextBox>
                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" CssClass="cal_Theme1"
                                PopupPosition="BottomRight" Format="dd MMM yyyy" TargetControlID="txtOpBalDate"
                                OnClientDateSelectionChanged="" Enabled="True">
                            </asp:CalendarExtender>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" width="20%" class="label">
                            Ledger Type<span class="req">*</span>
                        </td>
                        <td align="left" width="30%">
                            <asp:DropDownList ID="ddlLedgerType" runat="server" CssClass="dropdownList" Width="150px">
                                <asp:ListItem Value="0">Select</asp:ListItem>
                                <asp:ListItem Value="CASH">CASH</asp:ListItem>
                                <asp:ListItem Value="CUST">CUSTOMER</asp:ListItem>
                                <asp:ListItem Value="SUP">SUPPLIER</asp:ListItem>
                                <asp:ListItem Value="PUR">PURCHASE</asp:ListItem>
                                <asp:ListItem Value="REIM">REIMBURSEMENT</asp:ListItem>
                                <asp:ListItem Value="TAX">TAX</asp:ListItem>
                                <asp:ListItem Value="OTH">OTHER</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td align="left" width="20%" class="label">
                            Opening Balance<span class="req">*</span>
                        </td>
                        <td align="left" width="30%">
                            <asp:TextBox ID="txtOpeningBalance" runat="server" CssClass="textbox_required" Width="150px"></asp:TextBox>
                            <asp:FilteredTextBoxExtender ID="ftb1" runat="server" FilterType="Custom" ValidChars="0123456789."
                                TargetControlID="txtOpeningBalance">
                            </asp:FilteredTextBoxExtender>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" width="20%" class="label">
                            A/c Group<span class="req">*</span>
                        </td>
                        <td align="left" width="30%">
                            <asp:ComboBox ID="ddlGroup" runat="server" CssClass="WindowsStyle" AutoPostBack="true"
                                DropDownStyle="DropDown" AutoCompleteMode="SuggestAppend" CaseSensitive="false"
                                Width="150px" OnSelectedIndexChanged="ddlGroup_SelectedIndexChanged">
                            </asp:ComboBox>
                        </td>
                        <td align="left" width="20%" class="label">
                            Opening Balance Type<span class="req">*</span>
                        </td>
                        <td align="left" width="30%">
                            <asp:DropDownList ID="ddlOpeningBalanceType" CssClass="dropdownList" Width="150px"
                                runat="server">
                                <asp:ListItem Value="CR" Text="CR"></asp:ListItem>
                                <asp:ListItem Value="DR" Text="DR"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" width="20%" class="label">
                            A/c Sub Group<span class="req">*</span>
                        </td>
                        <td align="left" width="30%">
                            <asp:ComboBox ID="ddlSubGroup" runat="server" CssClass="WindowsStyle" AutoPostBack="false"
                                DropDownStyle="DropDown" AutoCompleteMode="SuggestAppend" CaseSensitive="false"
                                DataValueField="GroupID" DataTextField="GroupName" Width="150px">
                            </asp:ComboBox>
                        </td>
                        <td align="left" width="20%" class="label">
                            Cost Center Applicable
                        </td>
                        <td align="left" width="30%">
                            <asp:CheckBox ID="chkCostCenter" runat="server"></asp:CheckBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" width="20%" class="label">
                            Building No
                        </td>
                        <td align="left" width="30%">
                            <asp:TextBox ID="txtBuildingNo" runat="server" CssClass="textbox" MaxLength="15"></asp:TextBox>
                        </td>
                        <td align="left" width="20%" class="label">
                            Active
                        </td>
                        <td align="left" width="30%">
                            <asp:CheckBox ID="chkActive" runat="server" Checked="true"></asp:CheckBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" width="20%" class="label">
                            Building Name
                        </td>
                        <td align="left" width="30%">
                            <asp:TextBox ID="txtBName" runat="server" CssClass="textbox" MaxLength="100"></asp:TextBox>
                        </td>
                        <td align="left" width="20%" class="label">
                            Other
                        </td>
                        <td align="left" width="30%">
                            <asp:TextBox ID="txtOther" runat="server" CssClass="textbox" MaxLength="200"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" width="20%" class="label">
                            Flat No
                        </td>
                        <td align="left" width="30%">
                            <asp:TextBox ID="txtFlatNo" runat="server" CssClass="textbox" MaxLength="6"></asp:TextBox>
                        </td>
                        <td align="left" width="20%" class="label">
                            Pin Code
                        </td>
                        <td align="left" width="30%">
                            <asp:TextBox ID="txtPinCode" runat="server" CssClass="textbox" MaxLength="10"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" width="20%" class="label">
                            Floor No
                        </td>
                        <td align="left" width="30%">
                            <asp:TextBox ID="txtFloor" runat="server" CssClass="textbox" MaxLength="10"></asp:TextBox>
                        </td>
                        <td align="left" width="20%" class="label">
                            Mobile No
                        </td>
                        <td align="left" width="30%">
                            <asp:TextBox ID="txtMobileNo" runat="server" CssClass="textbox" MaxLength="20"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" width="20%" class="label">
                            Street Name
                        </td>
                        <td align="left" width="30%">
                            <asp:TextBox ID="txtStreet" runat="server" CssClass="textbox" MaxLength="100"></asp:TextBox>
                        </td>
                        <td align="left" width="20%" class="label">
                            Home Phone No
                        </td>
                        <td align="left" width="30%">
                            <asp:TextBox ID="txtResidentialContactNo" runat="server" CssClass="textbox" MaxLength="25"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" width="20%" class="label">
                            Country
                        </td>
                        <td align="left" width="30%">
                            <asp:DropDownList ID="ddlCountry" runat="server" CssClass="dropdownList" Width="150px"
                                AutoPostBack="true" DataValueField="CountryId" DataTextField="CountryName" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td align="left" width="20%" class="label">
                            Office Phone No
                        </td>
                        <td align="left" width="30%">
                            <asp:TextBox ID="txtOfficeContactNo" runat="server" CssClass="textbox" MaxLength="25"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" width="20%" class="label">
                            State
                        </td>
                        <td align="left" width="30%">
                            <asp:DropDownList ID="ddlState" runat="server" CssClass="dropdownList" Width="150px"
                                AutoPostBack="true" DataValueField="StateId" DataTextField="StateNameWithCode"
                                OnSelectedIndexChanged="ddlState_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td align="left" width="20%" class="label">
                            VAT No
                        </td>
                        <td align="left" width="30%">
                            <asp:TextBox ID="txtVATNo" runat="server" CssClass="textbox" MaxLength="20"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" width="20%" class="label">
                            City
                        </td>
                        <td align="left" width="30%">
                            <asp:DropDownList ID="ddlCity" runat="server" CssClass="dropdownList" Width="150px"
                                DataValueField="CityId" DataTextField="CityName">
                            </asp:DropDownList>
                        </td>
                        <td align="left" width="20%" class="label">
                            PAN No
                        </td>
                        <td align="left" width="30%">
                            <asp:TextBox ID="txtPANNo" runat="server" CssClass="textbox" MaxLength="20"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" align="center">
                            <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="button" OnClientClick="return Validation()"
                                OnClick="btnSave_Click"></asp:Button>
                            &nbsp;
                            <asp:Button ID="btnReset" runat="server" Text="Reset" CssClass="button" OnClick="btnReset_Click">
                            </asp:Button>
                        </td>
                    </tr>
                </table>
            </div>
            <br />
            <h6 align="left" style="color: #00356A;">
                General Ledger Details</h6>
            <table width="95%" align="center" class="table">
                <tr>
                    <td align="left" width="10%" class="label">
                        Ledger Name
                    </td>
                    <td align="left" width="15%">
                        <asp:TextBox ID="txtLedgerNameVw" runat="server" CssClass="textbox" Width="150px"></asp:TextBox>
                    </td>
                    <td align="left" class="label">
                        <asp:Button ID="btnSearch" runat="server" CssClass="button" Text="Search" OnClick="btnSearch_Click">
                        </asp:Button>
                    </td>
                </tr>
            </table>
            <br />
            <table width="95%" align="center" class="table">
                <tr>
                    <td align="center">
                        <asp:GridView ID="gdGenLedger" runat="server" AllowSorting="false" AllowPaging="true"
                            DataKeyNames="LedgerID" PageSize="15" AutoGenerateColumns="False" Width="100%"
                            OnPageIndexChanging="gdGenLedger_PageIndexChanging" OnRowEditing="gdGenLedger_RowEditing">
                            <Columns>
                                <asp:BoundField DataField="LedgerName" HeaderText="Ledger Name"></asp:BoundField>
                                <asp:BoundField DataField="LedgerType" HeaderText="Ledger Type"></asp:BoundField>
                                <asp:BoundField DataField="MAINGROUP" HeaderText="Main Group"></asp:BoundField>
                                <asp:BoundField DataField="SUBGROUP" HeaderText="Sub Group"></asp:BoundField>
                                <asp:BoundField DataField="OpeningDate" HeaderText="OP. Date"></asp:BoundField>
                                <asp:BoundField DataField="OpeningBalance" HeaderText="Opening Balance"></asp:BoundField>
                                <asp:BoundField DataField="ClosingBalance" HeaderText="Closing Balance"></asp:BoundField>
                                <asp:BoundField DataField="CostCenterApplied" HeaderText="Cost Center"></asp:BoundField>
                                <asp:BoundField DataField="Active" HeaderText="Active"></asp:BoundField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnEdit" CommandName="Edit" ImageUrl="~/Images/edit_icon.gif"
                                            runat="server" />
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
