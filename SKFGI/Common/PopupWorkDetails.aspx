<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PopupWorkDetails.aspx.cs" Inherits="CollegeERP.Common.PopupWorkDetails" Title="Work Details" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register src="../UserControl/Message.ascx" tagname="Message" tagprefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Work Details</title>
    <link href="../Styles/style.css" rel="Stylesheet" type="text/css" />
    <link href="../Styles/Control.css" rel="Stylesheet" type="text/css" />
    <link href="../Styles/blue.css" rel="Stylesheet" type="text/css" />
    <link href="../Styles/reset.css" rel="Stylesheet" type="text/css" />
</head>
<body style=" background-color:#fff;">
    <script type="text/javascript">
        function Validation()
        {
            if (document.getElementById('<%=txtCompanyName.ClientID%>').value == '') {
            alert("Enter Company Name");
            return false;
            }
            else if (document.getElementById('<%=txtSalary.ClientID%>').value != '' && isNumber(document.getElementById('<%=txtSalary.ClientID%>').value) == false) {
            alert("Enter Salary in Proper Format");
            return false;
            }
            else {return true;}
        }
       
    function isNumber(n) {
        return !isNaN(parseFloat(n)) && isFinite(n);
    }
    </script>
    <form id="form1" runat="server">
        <asp:ToolkitScriptManager ID="ToolScript1" runat="server"></asp:ToolkitScriptManager> 
        <asp:UpdatePanel ID="UP1" runat="server">
            <ContentTemplate>
                <center>
                
                <div style="padding:8px 0 8px 0; background-color:#FADC76;" id="divtitle" runat="server"></div>
                </center>
                <br />  
                <uc3:Message ID="Message" runat="server" />
                <br />
                <center>
                    <div style="width:800px;">
                        <table width="100%" align="center" style="padding:4px; background-color:#fff;">
                            <tr>
                                <td align="left" width="20%" class="label">Company Name<span class="req">*</span></td>
                                <td align="left" width="30%">
                                    <asp:TextBox ID="txtCompanyName" runat="server" CssClass="textbox_required" Width="140px"></asp:TextBox>
                                </td>
                                <td align="left" width="20%" class="label">Working Period</td>
                                <td align="left" width="30%">
                                    <asp:TextBox ID="txtWorkPeriod" runat="server" CssClass="textbox" Width="140px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" width="20%" class="label">Designation</td>
                                <td align="left" width="30%">
                                    <asp:TextBox ID="txtDesignation" runat="server" CssClass="textbox" Width="140px"></asp:TextBox>
                                </td>
                                <td align="left" width="20%" class="label">Responsibilities</td>
                                <td align="left" width="30%">
                                    <asp:TextBox ID="txtResponsibilities" runat="server" CssClass="textbox" Width="140px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" width="20%" class="label">Take Home Salary</td>
                                <td align="left" width="30%">
                                    <asp:TextBox ID="txtSalary" runat="server" CssClass="textbox" Width="140px"></asp:TextBox>
                                    <asp:FilteredTextBoxExtender ID="ftb1" runat="server" FilterType="Custom" ValidChars="0123456789." TargetControlID="txtSalary"></asp:FilteredTextBoxExtender>
                                </td>
                                <td align="left" width="20%" class="label"></td>
                                <td align="left" width="30%">
                                    <asp:Button ID="btnSave" runat="server" Text="Add" CssClass="button" 
                                        OnClientClick="javascript:return Validation()" onclick="btnSave_Click" />&nbsp;
                                    <asp:Button ID="btnCancel" runat="server" CssClass="button" Text="Close" OnClientClick="window.close();"     
                                    />
                                </td>
                            </tr>
                            
                        </table>
                        <br />
                        <table width="100%" align="center" class="table">
                            <tr>
                                <td align="center">
                                     <asp:GridView ID="dgvWork" runat="server" AutoGenerateColumns="false" 
                                        Width="100%" AllowPaging="false"
                                        DataKeyNames="EmployeeWorkId" onrowdeleting="dgvWork_RowDeleting" 
                                         onrowediting="dgvWork_RowEditing">
                                    <Columns>
                                        <asp:BoundField DataField="CompanyName" HeaderText="Company Name" />
                                        <asp:BoundField DataField="WorkPeriod" HeaderText="Working Period" />
                                        <asp:BoundField DataField="WorkDesignation" HeaderText="Designation" />
                                        <asp:BoundField DataField="WorkSalary" HeaderText="Salary" />
                                        
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
    </form>
</body>
</html>
