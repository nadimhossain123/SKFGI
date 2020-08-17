<%@ Page Language="C#" MasterPageFile="~/MasterAdmin.Master" AutoEventWireup="true"
    CodeBehind="ITaxEmployeePrevEmplDetails.aspx.cs" Inherits="CollegeERP.Payroll.ITaxEmployeePrevEmplDetails"
    Title="Employee PrevEmpl Details" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../UserControl/Message.ascx" TagName="Message" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">
        function Validation()
        {
           
            if (!checkforvaliddata(eval('document.forms[0].' + '<%=txtITaxPrevEmplHeadAmount.ClientID%>'), "Enter Head Amount", 1)) return false;
            if (!checkforvaliddata(eval('document.forms[0].' + '<%=ddlPreviusEmp.ClientID%>'), "Prev Empl Head", 0)) return false;
            return true;

        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ToolkitScriptManager ID="ToolScript1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="title">
        <h5>
            Employee Previous Employer</h5>
    </div>
    <asp:UpdatePanel ID="UP1" runat="server">
        <ContentTemplate>
            <uc3:Message ID="Message" runat="server" />
            <br />
            <center>
                <div style="width: 740px;">
                    <table width="100%" align="center" class="table">
                        <tr>
                            <td align="left" width="20%" class="label">
                                Previous Empl Head Name<span class="req">*</span>
                            </td>
                            <td align="left" width="30%">
                                <asp:DropDownList ID="ddlPreviusEmp" runat="server" CssClass="dropdownList" Width="180px"
                                    DataValueField="ITaxPrevEmplHeadId" DataTextField="ITaxPrevEmplHeadName">
                                </asp:DropDownList>
                            </td>
                            <td width="15%" align="left" class="label">
                                Prev Empl Head Amount<span class="req"></span>
                            </td>
                            <td width="40%" align="left">
                                <asp:TextBox ID="txtITaxPrevEmplHeadAmount" runat="server" CssClass="textbox_required"
                                    Width="200px"></asp:TextBox>
                                <asp:FilteredTextBoxExtender ID="ftb1" runat="server" FilterType="Custom" ValidChars="0123456789."
                                    TargetControlID="txtITaxPrevEmplHeadAmount">
                                </asp:FilteredTextBoxExtender>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" width="20%" class="label">
                                Financial Year<span class="req"></span>
                            </td>
                            <td align="left" width="30%">
                                <asp:Literal ID="ltrFinancialYear" runat="server" Mode="PassThrough"></asp:Literal>
                            </td>
                            <td align="left" width="20%" class="label">
                            </td>
                            <td align="left" width="30%">
                                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="button" OnClientClick="javascript:return Validation()"
                                    OnClick="btnSave_Click" />&nbsp;
                                <asp:Button ID="btnCancel" runat="server" CssClass="button" Text="Close" OnClick="btnCancel_Click" />
                            </td>
                        </tr>
                    </table>
                    <br />
                    <table width="100%" align="center">
                        <tr>
                            <td align="center">
                                <asp:GridView ID="dgvITaxEmployeePrevEmplDetails" runat="server" AutoGenerateColumns="false"
                                    Width="100%" AllowPaging="false" DataKeyNames="ITaxPrevEmplHeadId,EmployeeId"
                                    OnRowDeleting="dgvITaxEmployeePrevEmplDetails_RowDeleting" OnRowEditing="dgvITaxEmployeePrevEmplDetails_RowEditing">
                                    <Columns>
                                        <asp:BoundField DataField="EmpCode" HeaderText="Employee Code" />
                                        <asp:BoundField DataField="EmpName" HeaderText="Employee Name" />
                                        <asp:BoundField DataField="ITaxPrevEmplHeadName" HeaderText="Header Name" />
                                        <asp:BoundField DataField="ITaxPrevEmplHeadAmount" HeaderText="PrevEmplHeadAmount" />
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
