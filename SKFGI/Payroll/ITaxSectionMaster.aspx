<%@ Page Language="C#" MasterPageFile="~/MasterAdmin.Master" AutoEventWireup="true" CodeBehind="ITaxSectionMaster.aspx.cs" Inherits="CollegeERP.Payroll.ITaxSectionMaster" Title="ITax Section Details" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register src="../UserControl/Message.ascx" tagname="Message" tagprefix="uc3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function Validation()
        {
            if (document.getElementById('<%=txtITaxSectionName.ClientID%>').value == '') {
                alert("Enter Section Name");
            return false;
            }
            else if (document.getElementById('<%=txtITaxMaxExemption.ClientID%>').value == '' || parseFloat(document.getElementById('<%=txtITaxMaxExemption.ClientID%>').value) == 0) {
            alert("Enter Max Exemption");
                return false;
            }
            else {return true;}
        }
    </script>
    </asp:Content>
    <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ToolkitScriptManager ID="ToolScript1" runat="server"></asp:ToolkitScriptManager>
    <div class="title">
		<h5>ITax Section Details</h5>
    </div>
          
      <asp:UpdatePanel ID="UP1" runat="server">
        <ContentTemplate>
        <br />  
        <uc3:Message ID="Message" runat="server" />
        <br />
        <center>
            <div style="width:680px;">
                     <table width="100%" align="center" class="table">
                            <tr>
                                <td width="20%" align="left" class="label">Section Name<span class="req">*</span></td>
                                <td width="35%" align="left">
                                    <asp:TextBox ID="txtITaxSectionName" runat="server" CssClass="textbox_required" Width="200px"></asp:TextBox>
                                </td>
                              
                                <td width="20%" align="left" class="label">Max.Exemption<span class="req">*</span></td>
                                <td width="35%" align="left">
                                    <asp:TextBox ID="txtITaxMaxExemption" runat="server" CssClass="textbox_required" Width="200px"></asp:TextBox>
                                    <asp:FilteredTextBoxExtender ID="ftb1" runat="server" FilterType="Custom" ValidChars="0123456789." TargetControlID="txtITaxMaxExemption"></asp:FilteredTextBoxExtender>
                                </td>
                                
                             </tr>
                             <tr>
                                 <td align="left" colspan="3" class="label"></td>
                                <td align="left" >
                                    <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="button" 
                                        OnClientClick="javascript:return Validation()" onclick="btnSave_Click" />&nbsp;
                                    <asp:Button ID="btnCancel" runat="server" CssClass="button" Text="Cancel" onclick="btnCancel_Click" 
                                    />
                                </td>
                            </tr>
                        </table>
                        
                        <br />
                        <table width="100%" align="center">
                            <tr>
                                <td align="center">
                                    <asp:GridView ID="dgvITaxSection" runat="server" AutoGenerateColumns="false" 
                                        Width="100%" AllowPaging="false"
                                        DataKeyNames="ITaxSectionId" onrowdeleting="dgvITaxSection_RowDeleting" 
                                        onrowediting="dgvITaxSection_RowEditing">
                                    <Columns>
                                        <asp:BoundField DataField="ITaxSectionName" HeaderText="Section Name" />
                                        <asp:BoundField DataField="ITaxMaxExemption" HeaderText="Max Exemption" />
                                        <asp:TemplateField ShowHeader="false">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit_icon.gif" CommandName="Edit" />
                                        </ItemTemplate>
                                         </asp:TemplateField>
                                        <asp:TemplateField ShowHeader="false">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/Images/delete_icon.gif" CommandName="Delete" OnClientClick="return confirm('Are You Sure?');" />
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
</asp:Content>

