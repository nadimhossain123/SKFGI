<%@ Page Language="C#" MasterPageFile="~/MasterAdmin.Master" AutoEventWireup="true" CodeBehind="ExpenseType.aspx.cs" Inherits="CollegeERP.HR.ExpenseType" Title="Expense Type" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register src="../UserControl/Message.ascx" tagname="Message" tagprefix="uc3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function Validation()
        {
            if (document.getElementById('<%=txtExpenseType.ClientID%>').value == '') {
            alert("Enter Expense Type");
            return false;
            }
            else {return true;}
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<asp:ToolkitScriptManager ID="toolScript1" runat="server"></asp:ToolkitScriptManager>

    <div class="title">
		<h5>Expense Type Setting</h5>
    </div>
    <asp:UpdatePanel ID="UP1" runat="server">
    <ContentTemplate>
       
       <div style="width:740px;">
            <uc3:Message ID="Message" runat="server" />
            <br />
            <table width="100%" align="center" class="table">
                            <tr>
                                <td align="left" width="20%" class="label">Expense Type<span class="req">*</span></td>
                                <td align="left" width="30%">
                                    <asp:TextBox ID="txtExpenseType" runat="server" CssClass="textbox_required" Width="140px"></asp:TextBox>
                                </td>
                                <td align="left">
                                    <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="button" 
                                        OnClientClick="javascript:return Validation()" onclick="btnSave_Click" />&nbsp;
                                    <asp:Button ID="btnCancel" runat="server" Text="Reset" CssClass="button" 
                                        onclick="btnCancel_Click" />    
                                </td>
                            </tr>
            </table>
            <br />
                        <table width="100%" align="center" class="table">
                            <tr>
                                <td align="center">
                                     <asp:GridView ID="dgvExpenseType" runat="server" AutoGenerateColumns="false" 
                                        Width="100%" AllowPaging="false"
                                        DataKeyNames="ExpenseTypeId" onrowediting="dgvExpenseType_RowEditing">
                                    <Columns>
                                        <asp:BoundField DataField="ExpenseTypeName" HeaderText="Expense Type" />                                        
                                        <asp:TemplateField ShowHeader="false">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit_icon.gif" CommandName="Edit" />
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
    </ContentTemplate>
    </asp:UpdatePanel>                        
</asp:Content>
