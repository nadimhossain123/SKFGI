<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PopupQualification.aspx.cs" Inherits="CollegeERP.Common.PopupQualification" Title="Qualification Details" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register src="../UserControl/Message.ascx" tagname="Message" tagprefix="uc3" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Qualification Details</title>
    <link href="../Styles/style.css" rel="Stylesheet" type="text/css" />
    <link href="../Styles/Control.css" rel="Stylesheet" type="text/css" />
    <link href="../Styles/blue.css" rel="Stylesheet" type="text/css" />
    <link href="../Styles/reset.css" rel="Stylesheet" type="text/css" />
</head>
<body style=" background-color:#fff;">
    <script type="text/javascript">
        function Validation()
        {
            if (document.getElementById('<%=txtQualificationName.ClientID%>').value == '') {
            alert("Enter Qualification Name");
            return false;
            }
            else if (document.getElementById('<%=txtYearOfPassing.ClientID%>').value.length != 4) {
            alert("Enter Year Of Passing In Correct Format");
            return false;
            }
            else if (document.getElementById('<%=txtMarksPercent.ClientID%>').value != '' && isNumber(document.getElementById('<%=txtMarksPercent.ClientID%>').value) == false) {
            alert("Enter Marks(%) In Correct Format");
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
                                <td align="left" width="20%" class="label">Qualification Name<span class="req">*</span></td>
                                <td align="left" width="30%">
                                    <asp:TextBox ID="txtQualificationName" runat="server" CssClass="textbox_required" Width="140px"></asp:TextBox>
                                </td>
                                <td align="left" width="20%" class="label">Board/Authority</td>
                                <td align="left" width="30%">
                                    <asp:TextBox ID="txtBoard" runat="server" CssClass="textbox" Width="140px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" width="20%" class="label">Year Of Passing</td>
                                <td align="left" width="30%">
                                    <asp:TextBox ID="txtYearOfPassing" runat="server" CssClass="textbox" Width="140px" MaxLength="4"></asp:TextBox>
                                    <asp:FilteredTextBoxExtender ID="ftb1" runat="server" FilterType="Numbers" TargetControlID="txtYearOfPassing"></asp:FilteredTextBoxExtender>
                                </td>
                                <td align="left" width="20%" class="label">Marks(%)</td>
                                <td align="left" width="30%">
                                    <asp:TextBox ID="txtMarksPercent" runat="server" CssClass="textbox" Width="140px" MaxLength="5"></asp:TextBox>
                                    <asp:FilteredTextBoxExtender ID="ftb2" runat="server" FilterType="Custom" ValidChars="0123456789." TargetControlID="txtMarksPercent"></asp:FilteredTextBoxExtender>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" width="20%" class="label">Stream</td>
                                <td align="left" width="30%">
                                    <asp:TextBox ID="txtStream" runat="server" CssClass="textbox" Width="140px"></asp:TextBox>
                                </td>
                                <td align="left" width="20%" class="label">Qualification Type</td>
                                <td align="left" width="30%">
                                    <asp:DropDownList ID="ddlQualificationType" runat="server" CssClass="dropdownList" Width="140px">
                                        <asp:ListItem Value="Academic" Text="Academic"></asp:ListItem>
                                        <asp:ListItem Value="Professional" Text="Professional"></asp:ListItem>
                                        <asp:ListItem Value="Other Relevent Course" Text="Other Relevent Course"></asp:ListItem>
                                    </asp:DropDownList>
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
                                     <asp:GridView ID="dgvQualification" runat="server" AutoGenerateColumns="false" 
                                        Width="100%" AllowPaging="false"
                                        DataKeyNames="EmployeeQualificationId" 
                                         onrowdeleting="dgvQualification_RowDeleting" 
                                         onrowediting="dgvQualification_RowEditing">
                                    <Columns>
                                        <asp:BoundField DataField="QualificationName" HeaderText="Qualification Name" />
                                        <asp:BoundField DataField="QualificationBoard" HeaderText="Board/University" />
                                        <asp:BoundField DataField="QualificationStream" HeaderText="Stream" />
                                        <asp:BoundField DataField="QualificationPassingYear" HeaderText="Year Of Passing" />
                                        <asp:BoundField DataField="QualificationPercOfMarks" HeaderText="Marks(%)" />
                                        
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
