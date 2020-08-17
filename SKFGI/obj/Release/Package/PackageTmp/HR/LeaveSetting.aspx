<%@ Page Language="C#" MasterPageFile="~/MasterAdmin.Master" AutoEventWireup="true" CodeBehind="LeaveSetting.aspx.cs" Inherits="CollegeERP.HR.LeaveSetting" Title="Permissible Leave Setting" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register src="../UserControl/Message.ascx" tagname="Message" tagprefix="uc3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function Validation()
        {
            if (document.getElementById('<%=txtLeaveType.ClientID%>').value == '') {
            alert("Enter Leave Type");
            return false;
            }
            else if (document.getElementById('<%=txtDescription.ClientID%>').value == '') {
            alert("Enter Leave Description");
            return false;
            }
            else {return true;}
        }
        
       function LeavePerMonth() 
        {

            var LeavePerYear= document.getElementById('<%=txtLeavePerYear.ClientID%>').value;
            
            if (LeavePerYear != '')
            {
                document.getElementById('<%=txtLeavePerMonth.ClientID%>').value = roundNumber(LeavePerYear / 12);
            }
            else
            {
                document.getElementById('<%=txtLeavePerMonth.ClientID%>').value='';
            }
       }  
     
       function roundNumber(num) 
        {
	    var result = Math.round(num*Math.pow(10,1))/Math.pow(10,1);
	    return result;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ToolkitScriptManager ID="toolScript1" runat="server"></asp:ToolkitScriptManager>

    <div class="title">
		<h5>Permissible Leave Setting</h5>
    </div>
    
    <asp:UpdatePanel ID="UP1" runat="server">
    <ContentTemplate>
       <uc3:Message ID="Message" runat="server" />
       <div style="width:740px;">
            <table width="100%" align="center" class="table">
                            <tr>
                                <td align="left" width="20%" class="label">Leave Type<span class="req">*</span></td>
                                <td align="left" width="30%">
                                    <asp:TextBox ID="txtLeaveType" runat="server" CssClass="textbox_required" Width="140px"></asp:TextBox>
                                </td>
                                <td align="left" width="20%" class="label">Leave Per Year</td>
                                <td align="left" width="30%">
                                    <asp:TextBox ID="txtLeavePerYear" runat="server" CssClass="textbox" Width="140px" MaxLength="3" onkeyup="LeavePerMonth()"></asp:TextBox>
                                    <asp:FilteredTextBoxExtender ID="ftb1" runat="server" FilterType="Numbers" TargetControlID="txtLeavePerYear"></asp:FilteredTextBoxExtender>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" width="20%" class="label">Description<span class="req">*</span></td>
                                <td align="left" colspan="3">
                                    <asp:TextBox ID="txtDescription" runat="server" CssClass="textbox_required" Width="510px" Height="35px" TextMode="MultiLine"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" width="20%" class="label">Leave Per Month</td>
                                <td align="left" width="30%">
                                    <asp:TextBox ID="txtLeavePerMonth" runat="server" CssClass="textbox_disabled" Width="140px" MaxLength="6" Enabled="false"></asp:TextBox>
                                </td>
                                <td align="left" width="20%" class="label">Carry Forwarded</td>
                                <td align="left" width="30%">
                                    <asp:CheckBox ID="ChkIsCarryForwarded" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td align="left" width="20%" class="label">Max Carry Fwd Limit</td>
                                <td align="left" width="30%">
                                    <asp:TextBox ID="txtMaxCarryFwdLimit" runat="server" CssClass="textbox" Width="140px" MaxLength="3"></asp:TextBox>
                                    <asp:FilteredTextBoxExtender ID="ftb4" runat="server" FilterType="Numbers" TargetControlID="txtMaxCarryFwdLimit"></asp:FilteredTextBoxExtender>
                                </td>
                                <td align="left" width="20%" class="label">Encashable</td>
                                <td align="left" width="30%">
                                    <asp:CheckBox ID="ChkIsEncashable" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td align="left" width="20%" class="label">Paid</td>
                                <td align="left" width="30%">
                                    <asp:CheckBox ID="ChkIsPaid" runat="server" />
                                </td>
                                <td colspan="2"></td>
                            </tr>
                            <tr>
                                <td align="left" width="20%" class="label"></td>
                                <td align="left" width="30%"></td>
                                <td align="left" width="20%" class="label"></td>
                                <td align="left" width="30%">
                                    <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="button" 
                                        OnClientClick="javascript:return Validation()" onclick="btnSave_Click" Visible="false" />&nbsp;
                                    <asp:Button ID="btnCancel" runat="server" CssClass="button" Text="Reset" 
                                        onclick="btnCancel_Click" />
                                </td>
                            </tr>
            </table>
            
            <br />
                        <table width="100%" align="center" class="table">
                            <tr>
                                <td align="center">
                                     <asp:GridView ID="dgvLeaveType" runat="server" AutoGenerateColumns="false" 
                                        Width="100%" AllowPaging="false"
                                        DataKeyNames="LeaveTypeId" onrowediting="dgvLeaveType_RowEditing">
                                    <Columns>
                                        <asp:BoundField DataField="LeaveTypeName" HeaderText="Leave Type" />
                                        <asp:BoundField DataField="Description" HeaderText="Description" />
                                        <asp:BoundField DataField="LeavePerMonth" HeaderText="Per Month" />
                                        <asp:BoundField DataField="LeavePerYear" HeaderText="Per Year" />
                                        <asp:BoundField DataField="IsCarryForwarded" HeaderText="Carry Forwarded" />
                                        <asp:BoundField DataField="IsEncashable" HeaderText="Encashable" />
                                        <asp:BoundField DataField="MaxCarryFwdLimit" HeaderText="MaxCarryFwdLimit" />
                                        <asp:BoundField DataField="IsPaid" HeaderText="Paid" />
                                        
                                        <asp:TemplateField ShowHeader="false">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit_icon.gif" CommandName="Edit" Visible="false" />
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
