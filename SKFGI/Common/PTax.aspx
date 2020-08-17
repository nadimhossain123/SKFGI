<%@ Page Language="C#" MasterPageFile="~/MasterAdmin.Master" AutoEventWireup="true" CodeBehind="PTax.aspx.cs" Inherits="CollegeERP.Common.PTax" Title="Untitled Page" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register src="../UserControl/Message.ascx" tagname="Message" tagprefix="uc3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script type="text/javascript">
    function Validation()
    {
        if (document.getElementById('<%=txtStateCode.ClientID%>').value == '')
        {
            alert("Enter State Code");
            return false;
        }
        else if (document.getElementById('<%=txtStateName.ClientID%>').value == '')
        {
            alert("Enter State Name");
            return false;
        }
        
        else {return true;}
    }
    
    
    function openpopup(poplocation, querystring, popheight, popwidth,poptop, popleft)
     {                
        var popposition='left = 300, top=90, width=760,align=center, height=420,menubar=no, scrollbars=yes, resizable=no';                
               
        var NewWindow = window.open(poplocation,'',popposition);                
        if (NewWindow.focus!=null){                        
        NewWindow.focus();                
        } 
        return false;       
     }
    
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<asp:ToolkitScriptManager ID="ToolScript1" runat="server"></asp:ToolkitScriptManager>

    <div class="title">
		<h5>P.Tax Slab Fixation</h5>
    </div>
    
    <asp:UpdatePanel ID="Up1" runat="server">
        <ContentTemplate>
            <div style="width:740px;">
                <uc3:Message ID="Message" runat="server" />
                <br />
                <table width="100%" align="center" class="table">
                    <tr>
                        <td align="left" width="20%" class="label">State Code<span class="req">*</span></td>
                        <td align="left" width="30%">
                            <asp:TextBox ID="txtStateCode" runat="server" CssClass="textbox_required" Width="140px"></asp:TextBox>
                        </td>
                        <td align="left" width="20%" class="label">State Name<span class="req">*</span></td>
                        <td align="left" width="30%">
                            <asp:TextBox ID="txtStateName" runat="server" CssClass="textbox_required" Width="140px"></asp:TextBox>
                        </td>
                    </tr>
                    
                    <tr>
                        <td width="20%"></td>
                        <td width="30%"></td>
                        <td width="20%"></td>
                        <td align="left" width="30%">
                            <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="button" 
                                OnClientClick="javascript:return Validation()" onclick="btnSave_Click" />
                         &nbsp;
                        <asp:Button ID="btnCancel" runat="server" CssClass="button" Text="Reset" onclick="btnCancel_Click"      
                        />
                        </td>
                </tr>
                </table>
                
                <br />
        <br />
        
        <table width="100%" align="center" class="table">
            <tr>
                <td align="center">
                    <asp:GridView ID="dgvPTax" runat="server" AutoGenerateColumns="false" 
                         Width="100%" AllowPaging="false" 
                         DataKeyNames="PTaxId"
                        onrowediting="dgvPTax_RowEditing" onrowdatabound="dgvPTax_RowDataBound">
                        <Columns>
                            <asp:BoundField DataField="PTaxStateCode" HeaderText="State Code" />
                            <asp:BoundField DataField="PTaxStateDescription" HeaderText="State Name" />
                         <asp:TemplateField HeaderText="Slab Details">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkSlab" runat="server" Text="Slab Details"></asp:LinkButton> 
                                    </ItemTemplate>
                                </asp:TemplateField>   
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
