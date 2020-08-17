<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PopupFamilyDetails.aspx.cs" Inherits="CollegeERP.Common.PopupFamilyDetails" Title="Family Details" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register src="../UserControl/Message.ascx" tagname="Message" tagprefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Family Details</title>
    <link href="../Styles/style.css" rel="Stylesheet" type="text/css" />
    <link href="../Styles/Control.css" rel="Stylesheet" type="text/css" />
    <link href="../Styles/blue.css" rel="Stylesheet" type="text/css" />
    <link href="../Styles/reset.css" rel="Stylesheet" type="text/css" />
</head>
<body style=" background-color:#fff;">
    <script type="text/javascript">
        function Validation()
        {
            if (document.getElementById('<%=txtMemberName.ClientID%>').value == '') {
            alert("Enter Member Name");
            return false;
            }
            else if (document.getElementById('<%=txtEmail.ClientID%>').value != '' && IsValidEmail(document.getElementById('<%=txtEmail.ClientID%>').value) == false) {
            alert("Enter Email ID in Proper Format");
            return false;
            }
            else {return true;}
        }
       
         function IsValidEmail(s)
            {
            var filter =/^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$/;
            if (!filter.test(s)) {
                return false;
                }
                else
                {return true;}
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
                                <td align="left" width="20%" class="label">Member Name<span class="req">*</span></td>
                                <td align="left" width="30%">
                                    <asp:TextBox ID="txtMemberName" runat="server" CssClass="textbox_required" Width="140px"></asp:TextBox>
                                </td>
                                <td align="left" width="20%" class="label">Occupation</td>
                                <td align="left" width="30%">
                                    <asp:TextBox ID="txtOccupation" runat="server" CssClass="textbox" Width="140px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" width="20%" class="label">Relation</td>
                                <td align="left" width="30%">
                                    <asp:TextBox ID="txtRelation" runat="server" CssClass="textbox" Width="140px"></asp:TextBox>
                                </td>
                                <td align="left" width="20%" class="label">Gender</td>
                                <td align="left" width="30%">
                                    <asp:DropDownList ID="ddlGender" runat="server" CssClass="dropdownList" Width="140px">
                                        <asp:ListItem Value="Male" Text="Male"></asp:ListItem>
                                        <asp:ListItem Value="Female" Text="Female"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" width="20%" class="label">Age</td>
                                <td align="left" width="30%">
                                    <asp:TextBox ID="txtAge" runat="server" CssClass="textbox" Width="140px" MaxLength="2"></asp:TextBox>
                                    <asp:FilteredTextBoxExtender ID="ftb1" runat="server" FilterType="Numbers" TargetControlID="txtAge"></asp:FilteredTextBoxExtender>
                                </td>
                                <td align="left" width="20%" class="label">Contact Person(Y/N)</td>
                                <td align="left" width="30%">
                                    <asp:DropDownList ID="ddlHasContactPerson" runat="server" CssClass="dropdownList" Width="140px">
                                        <asp:ListItem Value="Y" Text="Y"></asp:ListItem>
                                        <asp:ListItem Value="N" Text="N"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" width="20%" class="label">Email Id</td>
                                <td align="left" width="30%">
                                    <asp:TextBox ID="txtEmail" runat="server" CssClass="textbox" Width="140px"></asp:TextBox>
                                </td>
                                <td align="left" width="20%" class="label">Contact No</td>
                                <td align="left" width="30%">
                                    <asp:TextBox ID="txtContactNo" runat="server" CssClass="textbox" Width="140px" MaxLength="15"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" width="20%" class="label"></td>
                                <td align="left" width="30%"></td>
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
                                     <asp:GridView ID="dgvFamily" runat="server" AutoGenerateColumns="false" 
                                        Width="100%" AllowPaging="false"
                                        DataKeyNames="EmployeeFamilyId" onrowdeleting="dgvFamily_RowDeleting" 
                                         onrowediting="dgvFamily_RowEditing">
                                    <Columns>
                                        <asp:BoundField DataField="MemberName" HeaderText="Member Name" />
                                        <asp:BoundField DataField="MemberRelation" HeaderText="Relation" />
                                        <asp:BoundField DataField="MemberOccupation" HeaderText="Occupation" />
                                        <asp:BoundField DataField="HasMemberContact" HeaderText="Contact Person" />
                                        <asp:BoundField DataField="MemberContactNo" HeaderText="Contact No" />
                                        <asp:BoundField DataField="MemberContactEmail" HeaderText="Email Id" />
                                        
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
