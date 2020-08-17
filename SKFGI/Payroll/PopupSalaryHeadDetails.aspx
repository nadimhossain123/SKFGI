<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PopupSalaryHeadDetails.aspx.cs"
    Inherits="CollegeERP.Payroll.PopupSalaryHeadDetails" Title="Salary Head Details" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../UserControl/Message.ascx" TagName="Message" TagPrefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Salary Head Details</title>
    <link href="../Styles/style.css" rel="Stylesheet" type="text/css" />
    <link href="../Styles/Control.css" rel="Stylesheet" type="text/css" />
    <link href="../Styles/blue.css" rel="Stylesheet" type="text/css" />
    <link href="../Styles/reset.css" rel="Stylesheet" type="text/css" />
</head>
<body style="background-color: #fff;">

    <script type="text/javascript">
        function Validation()
        {
            if (document.getElementById('<%=txtHead.ClientID%>').value == '') {
            alert("Enter Salary Head Name");
            return false;
            }
            else {return true;}
        }
 
        function RefreshParent()
        {
            window.close();
            opener.location.reload();
        }
    </script>

    <form id="form1" runat="server">
    <asp:ScriptManager ID="Script1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UP1" runat="server">
        <ContentTemplate>
            <br />
            <uc3:Message ID="Message" runat="server" />
            <br />
            <center>
                <div style="width: 580px;">
                    <table width="100%" align="center" style="padding: 4px; background-color: #fff;">
                        <tr>
                            <td width="15%" align="left" class="label">
                                Salary Head<span class="req">*</span>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtHead" runat="server" CssClass="textbox_required" Width="200px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td width="15%" align="left" class="label">
                                Max Range
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtMaxRange" runat="server" CssClass="textbox" Width="160px" onkeypress="return AmountOnly('txtMaxRange',this);"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td width="15%" align="left" class="label">
                                Fixed
                            </td>
                            <td align="left">
                                <asp:CheckBox ID="ChkIsFixed" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td align="left">
                                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="button" OnClientClick="javascript:return Validation()"
                                    OnClick="btnSave_Click" />&nbsp;
                                <asp:Button ID="btnCancel" runat="server" CssClass="button" Text="Close" OnClientClick="RefreshParent();" />
                            </td>
                        </tr>
                    </table>
                    <br />
                    <table width="100%" align="center">
                        <tr>
                            <td align="center">
                                <asp:Panel ID="PNLGrid" runat="server" ScrollBars="Vertical" Height="210px" BackColor="#FFFFFF">
                                    <asp:GridView ID="dgvHead" runat="server" AutoGenerateColumns="false" Width="100%"
                                        AllowPaging="false" DataKeyNames="SalaryHeadId" OnRowEditing="dgvHead_RowEditing">
                                        <Columns>
                                            <asp:BoundField DataField="SalaryHeadDetails" HeaderText="Salary Head Name" />
                                            <asp:BoundField DataField="IsFixed" HeaderText="Type" />
                                            <asp:BoundField DataField="MaxRange" HeaderText="Max Range" DataFormatString="{0:n}" />
                                            <asp:TemplateField ShowHeader="false">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit_icon.gif" CommandName="Edit" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <HeaderStyle CssClass="HeaderStyle" />
                                        <RowStyle CssClass="RowStyle" />
                                        <EmptyDataRowStyle CssClass="EditRowStyle" />
                                        <AlternatingRowStyle CssClass="AltRowStyle" />
                                        <PagerStyle CssClass="PagerStyle" />
                                    </asp:GridView>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                </div>
            </center>
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
