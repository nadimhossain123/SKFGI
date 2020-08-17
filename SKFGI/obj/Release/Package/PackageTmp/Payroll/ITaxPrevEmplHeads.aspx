<%@ Page Language="C#" MasterPageFile="~/MasterAdmin.Master" AutoEventWireup="true" CodeBehind="ITaxPrevEmplHeads.aspx.cs" Inherits="CollegeERP.Payroll.ITaxPrevEmplHeads" Title="ITax Previous Emp. Details" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register src="../UserControl/Message.ascx" tagname="Message" tagprefix="uc3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function Validation()
        {
            if (document.getElementById('<%=txtITaxPrevEmplHeadName.ClientID%>').value == '') {
                alert("Enter ITax Previous Head Name");
            return false;
            }
           
            else {return true;}
        }
 
    </script>
    </asp:Content>
    <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ToolkitScriptManager ID="ToolScript1" runat="server"></asp:ToolkitScriptManager>
    <div class="title">
		<h5>Income Tax Previous Emp. Head Details</h5>
    </div>
          
      <asp:UpdatePanel ID="UP1" runat="server">
        <ContentTemplate>
        <br />  
        <uc3:Message ID="Message" runat="server" />
        <br />
        <center>
            <div style="width:680px;">
                     <table width="90%" align="center" class="table">
                            <tr>
                                <td width="30%" align="left" class="label">ITax Previous Emp.<span class="req">*</span></td>
                                <td width="40%" align="left">
                                    <asp:TextBox ID="txtITaxPrevEmplHeadName" runat="server" CssClass="textbox_required" Width="200px"></asp:TextBox>
                                </td>
                                 <td width="30%" align="left" class="label">
                                     <asp:RadioButton ID="rbAddition" runat="server" Text="Addition" GroupName="Flag" />
                                        &nbsp;&nbsp;
                                     <asp:RadioButton ID="rbDeduction" runat="server" Text="Deduction" GroupName="Flag" />
                                        
                                 </td>
                                </tr>
                                <tr>                                              
                             
                                <td width="60%" align="right" colspan="3" >
                                    <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="button" 
                                        OnClientClick="javascript:return Validation()" onclick="btnSave_Click" />&nbsp;
                                    <asp:Button ID="btnCancel" runat="server" CssClass="button" Text="Reset" onclick="btnCancel_Click"     
                                    />
                                </td>
                              </tr>       
                        </table>
                        
                        <br />
                        <table width="100%" align="center">
                            <tr>
                                <td align="center">
                                    <asp:GridView ID="dgvITaxPrevEmplHead" runat="server" AutoGenerateColumns="false" 
                                        Width="100%" AllowPaging="false"
                                        DataKeyNames="ITaxPrevEmplHeadId" onrowdeleting="dgvITaxPrevEmplHead_RowDeleting" 
                                        onrowediting="dgvITaxPrevEmplHead_RowEditing">
                                    <Columns>
                                        <asp:BoundField DataField="ITaxPrevEmplHeadName" HeaderText="ITax PrevEmpl HeadName" />
                                        <asp:BoundField DataField="ITaxType" HeaderText="ITax PrevEmpl Type" />
                                      
                                        <asp:TemplateField ShowHeader="false" ><%--Visible="false"--%>
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

