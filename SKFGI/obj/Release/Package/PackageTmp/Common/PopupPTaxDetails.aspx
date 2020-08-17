<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PopupPTaxDetails.aspx.cs" Inherits="CollegeERP.Common.PopupPTaxDetails" Title="PTax Details" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register src="../UserControl/Message.ascx" tagname="Message" tagprefix="uc3" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>PTax Details</title>
    <link href="../Styles/style.css" rel="Stylesheet" type="text/css" />
    <link href="../Styles/Control.css" rel="Stylesheet" type="text/css" />
    <link href="../Styles/blue.css" rel="Stylesheet" type="text/css" />
    <link href="../Styles/reset.css" rel="Stylesheet" type="text/css" />
</head>
<body style="background-color:#fff;">
    <script type="text/javascript">
        function Validation()
        {
        if (document.getElementById('<%=txtSlabNo.ClientID%>').value == '' || parseFloat(document.getElementById('<%=txtSlabNo.ClientID%>').value) == 0)
        {
            alert("Slab No Can Not Be Empty Or Zero");
            return false;
        }
        else if (isNumber(document.getElementById('<%=txtAmountRangeFrom.ClientID%>').value) == false || parseFloat(document.getElementById('<%=txtAmountRangeFrom.ClientID%>').value) == 0)
        {
            alert("Amount Range From Value Is Not Proper");
            return false;
        }
        else if (isNumber(document.getElementById('<%=txtAmountRangeTo.ClientID%>').value) == false || parseFloat(document.getElementById('<%=txtAmountRangeTo.ClientID%>').value) == 0)
        {
            alert("Amount Range To Value Is Not Proper");
            return false;
        }
        else if (isNumber(document.getElementById('<%=txtPTaxAmount.ClientID%>').value) == false)
        {
            alert("P.Tax Amount Is Not Proper");
            return false;
        }
        else if (parseFloat(document.getElementById('<%=txtAmountRangeFrom.ClientID%>').value) > parseFloat(document.getElementById('<%=txtAmountRangeTo.ClientID%>').value))
        {
            alert("Amount Range Is Not Correct");
            return false;
        }
        else {return true;}
        }
 
    function isNumber(n) {
        return !isNaN(parseFloat(n)) && isFinite(n);
    }
    </script>
    <form id="form1" runat="server">
        <asp:ToolkitScriptManager ID="Script1" runat="server">
        </asp:ToolkitScriptManager>
        <asp:UpdatePanel ID="UP1" runat="server">
            <ContentTemplate>
                <center>
                <div style="padding:8px 0 8px 0; background-color:#FADC76;" id="divtitle" runat="server"></div>
                </center>  
                <uc3:Message ID="Message" runat="server" />
                <br />
                <center>
                    <div style="width:660px;">
                        <table width="95%" align="center" style="padding:4px; background-color:#fff;">
                            <tr>
                                <td align="left" width="20%" class="label">Slab No<span class="req">*</span></td>
                                <td align="left" width="30%">
                                    <asp:TextBox ID="txtSlabNo" runat="server" CssClass="textbox_required" Width="140px"></asp:TextBox>
                                    <asp:FilteredTextBoxExtender ID="ftb1" runat="server" FilterType="Numbers" TargetControlID="txtSlabNo"></asp:FilteredTextBoxExtender>
                                </td>
                                <td align="left" width="20%" class="label">Range From<span class="req">*</span></td>
                                <td align="left" width="30%">
                                    <asp:TextBox ID="txtAmountRangeFrom" runat="server" CssClass="textbox_required" Width="140px"></asp:TextBox>
                                    <asp:FilteredTextBoxExtender ID="ftb2" runat="server" FilterType="Custom" ValidChars="0123456789." TargetControlID="txtAmountRangeFrom"></asp:FilteredTextBoxExtender>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" width="20%" class="label">Range To<span class="req">*</span></td>
                                <td align="left" width="30%">
                                    <asp:TextBox ID="txtAmountRangeTo" runat="server" CssClass="textbox_required" Width="140px"></asp:TextBox>
                                    <asp:FilteredTextBoxExtender ID="ftb3" runat="server" FilterType="Custom" ValidChars="0123456789." TargetControlID="txtAmountRangeTo"></asp:FilteredTextBoxExtender>
                                </td>
                                <td align="left" width="20%" class="label">Amount Of P.Tax<span class="req">*</span></td>
                                <td align="left" width="30%">
                                    <asp:TextBox ID="txtPTaxAmount" runat="server" CssClass="textbox_required" Width="140px"></asp:TextBox>
                                    <asp:FilteredTextBoxExtender ID="ftb4" runat="server" FilterType="Custom" ValidChars="0123456789." TargetControlID="txtPTaxAmount"></asp:FilteredTextBoxExtender>
                                </td>
                            </tr>
                            <tr>
                            <td width="20%"></td>
                            <td width="30%"></td>
                            <td width="20%"></td>
                            <td align="left" width="30%">
                                <asp:Button ID="btnSave" runat="server" Text="Add" CssClass="button" 
                                OnClientClick="javascript:return Validation()" onclick="btnSave_Click" />
                                &nbsp;
                                <asp:Button ID="btnCancel" runat="server" CssClass="button" Text="Close" OnClientClick="window.close()" />
                            </td>
                            </tr>
                        </table>
                        <br />
                        <table width="95%" align="center">
                            <tr>
                                <td align="center">
                                    <asp:GridView ID="dgvPTaxDetails" runat="server" AutoGenerateColumns="false" 
                                        Width="100%" AllowPaging="false"
                                        DataKeyNames="PTaxDetailsId" onrowdeleting="dgvPTaxDetails_RowDeleting">
                                    <Columns>
                                        <asp:BoundField DataField="PTaxDetailsSlabNo" HeaderText="Slab No" />
                                        <asp:BoundField DataField="StateCode" HeaderText="State Code" />
                                        <asp:BoundField DataField="PTaxDetailsFromAmount" HeaderText="Amount Range Form" />
                                        <asp:BoundField DataField="PTaxDetailsToAmount" HeaderText="Amount Range To" />
                                        <asp:BoundField DataField="PTaxDetailsAmount" HeaderText="Rate Of Tax" />
                                        <asp:TemplateField ShowHeader="false">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/Images/delete_icon.gif" CommandName="Delete" />
                                        </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                        
                                <HeaderStyle CssClass="HeaderStyle"  />
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
    </form>
</body>
</html>
