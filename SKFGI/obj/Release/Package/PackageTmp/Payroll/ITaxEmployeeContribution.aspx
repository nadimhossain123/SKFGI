<%@ Page Language="C#" MasterPageFile="~/MasterAdmin.Master" AutoEventWireup="true"
    CodeBehind="ITaxEmployeeContribution.aspx.cs" Inherits="CollegeERP.Payroll.ITaxEmployeeContribution"
    Title="IT Employee Contribution" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../UserControl/Message.ascx" TagName="Message" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">
        function Validation()
        {
            if (!checkforvaliddata(eval('document.forms[0].' + '<%=ddlInvestment.ClientID%>'), "Investment Details", 0)) return false;
            if (!checkforvaliddata(eval('document.forms[0].' + '<%=txtProposedAmount.ClientID%>'), "Proposed Amount", 1)) return false;
            return true;

        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ToolkitScriptManager ID="ToolScript1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="title">
        <h5>
            Income Tax Employee Contribution</h5>
    </div>
    <asp:UpdatePanel ID="UP1" runat="server">
        <ContentTemplate>
            <uc3:Message ID="Message" runat="server" />
            <br />
            <h6 align="left" style="color: #00356A;">
                Employee Contribution</h6>
            <center>
                <div style="width: 740px;">
                    <table width="100%" align="center" class="table">
                        <tr>
                            <td align="left" width="20%" class="label">
                                Investment Name<span class="req">*</span>
                            </td>
                            <td align="left" width="30%">
                                <asp:DropDownList ID="ddlInvestment" runat="server" CssClass="dropdownList" Width="160px"
                                    DataValueField="ITaxInvestmentHeadId" DataTextField="ITaxInvestmentHeadName">
                                </asp:DropDownList>
                            </td>
                            <td width="15%" align="left" class="label">
                                Proposed Amount<span class="req"></span>
                            </td>
                            <td width="40%" align="left">
                               <asp:TextBox ID="txtProposedAmount" runat="server" CssClass="textbox_required" Width="160px"></asp:TextBox>
                                <asp:FilteredTextBoxExtender ID="ftb1" runat="server" FilterType="Custom" ValidChars="0123456789."
                                    TargetControlID="txtProposedAmount">
                                </asp:FilteredTextBoxExtender>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" width="20%" class="label">
                                ApprovedAmount<span class="req"></span>
                            </td>
                            <td align="left" width="30%">
                                <asp:TextBox ID="txtApprovedAmount" runat="server" CssClass="textbox_required" Width="160px"></asp:TextBox>
                                <asp:FilteredTextBoxExtender ID="ftb2" runat="server" FilterType="Custom" ValidChars="0123456789."
                                    TargetControlID="txtApprovedAmount">
                                </asp:FilteredTextBoxExtender>
                            </td>
                            <td width="15%" align="left" class="label">
                                 Financial Year<span class="req"></span>
                            </td>
                            <td width="40%" align="left">
                                <asp:Literal ID="ltrFinancialYear" runat="server" Mode="PassThrough"></asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" width="20%" class="label">
                               
                            </td>
                            <td align="left" width="30%">
                                
                            </td>
                            <td width="15%" align="left" class="label"></td>
                            <td align="left" width="40%">
                                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="button" OnClientClick="javascript:return Validation()"
                                    OnClick="btnSave_Click" />&nbsp;
                                <asp:Button ID="btnCancel" runat="server" CssClass="button" Text="Cancel" OnClick="btnCancel_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
                <br />
                <h6 align="left" style="color: #00356A;">
                    Employee Contribution List</h6>
                <div style="width: 800px;">
                    <table width="100%" align="center" class="table" id="Searchtable" runat="server">
                        <tr>
                            <td align="left" width="10%" class="label">
                                Employee Code
                            </td>
                            <td align="left" width="15%">
                                <asp:TextBox ID="txtEmpCodeSearch" runat="server" CssClass="textbox" Width="160px"></asp:TextBox>
                            </td>
                            <td align="left" width="10%" class="label">
                                First Name
                            </td>
                            <td align="left" width="15%">
                                <asp:TextBox ID="txtFName" runat="server" CssClass="textbox" Width="160px"></asp:TextBox>
                            </td>
                            <td align="left">
                                <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="button" OnClick="btnSearch_Click" />
                            </td>
                        </tr>
                    </table>
                    <br />
                    <table width="100%" align="center" class="table">
                        <tr>
                            <td align="center">
                                <asp:GridView ID="dgvITaxEmployeeContribution" runat="server" AutoGenerateColumns="false"
                                    Width="100%" AllowPaging="false" DataKeyNames="ITaxEmployeeContributionId" OnRowDeleting="dgvITaxEmployeeContribution_RowDeleting"
                                    OnRowEditing="dgvITaxEmployeeContribution_RowEditing">
                                    <Columns>
                                        <asp:BoundField DataField="EmpCode" HeaderText="Employee Code" />
                                        <asp:BoundField DataField="EmpName" HeaderText="Employee Name" />
                                        <asp:BoundField DataField="ITaxSectionName" HeaderText="Section" />
                                        <asp:BoundField DataField="ITaxInvestmentHeadName" HeaderText="Investment Head" />
                                        <asp:BoundField DataField="ProposedAmount" HeaderText="Proposed Amount" />
                                        <asp:BoundField DataField="ApprovedAmount" HeaderText="Approved Amount" />
                                        <asp:BoundField DataField="FinancialYear" HeaderText="Financial Year" />
                                        <asp:TemplateField ShowHeader="false">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit_icon.gif" CommandName="Edit" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ShowHeader="false">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/Images/delete_icon.gif"
                                                    CommandName="Delete" OnClientClick="return confirm('Are You Sure?');" />
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
            </center>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
