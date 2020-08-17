<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PopUpTermsAndCondition.aspx.cs"
    Inherits="CollegeERP.Common.PopUpTermsAndCondition" Title="Terms & Condition" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Terms & Condition</title>
    <link href="../Styles/style.css" rel="Stylesheet" type="text/css" />
    <link href="../Styles/Control.css" rel="Stylesheet" type="text/css" />
    <link href="../Styles/blue.css" rel="Stylesheet" type="text/css" />
    <link href="../Styles/reset.css" rel="Stylesheet" type="text/css" />
</head>
<body>

    <script type="text/javascript">
        function Validation()
        {
            if (document.getElementById('<%=txtTerms.ClientID%>').value == '') {
            alert("Enter Terms");
            return false;
            }
            else {return true;}
        }
 
    </script>

    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ToolScript1" runat="server">
    </asp:ToolkitScriptManager>
    <asp:UpdatePanel ID="UP1" runat="server">
        <ContentTemplate>
            <center>
               
                    <br />
                    <table width="98%" align="center" class="table">
                        <tr>
                            <td align="left" width="20%" class="label" valign="top">
                                Terms 
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtTerms" runat="server" CssClass="textbox_required" TextMode="MultiLine"
                                    Width="500px" Height="45px" Style="resize: none;"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" colspan="2">
                                <asp:Button ID="btnSave" runat="server" CssClass="button" Text="Save" 
                                    OnClientClick="return Validation()" onclick="btnSave_Click" />
                                
                            </td>
                        </tr>
                    </table>
                    
                    <table width="98%">
                        <tr>
                            <td align="center">
                                <asp:GridView ID="dgvTerms" runat="server" AutoGenerateColumns="false" AllowPaging="false"
                                    Width="100%" DataKeyNames="TermsId" onrowediting="dgvTerms_RowEditing">
                                    <Columns>
                                        <asp:BoundField DataField="TermsName" HeaderText="Terms & Condition" />
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
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
               
            </center>
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
