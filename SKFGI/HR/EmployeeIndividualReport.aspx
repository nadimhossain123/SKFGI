<%@ Page Language="C#" MasterPageFile="~/MasterAdmin.Master" AutoEventWireup="true" CodeBehind="EmployeeIndividualReport.aspx.cs" 
Inherits="CollegeERP.HR.EmployeeIndividualReport" Title="Employee Individual Report" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../UserControl/Message.ascx" TagName="Message" TagPrefix="uc3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script type="text/javascript">
    function openpopup(poplocation, querystring, popheight, popwidth, poptop, popleft) {
            if (popheight == '') {
                popheight = 500
            }
            var popposition = 'left = 300, top=60, width=' + popwidth + ',align=center, height='+ popheight +',menubar=no, scrollbars=yes, resizable=no';

            var NewWindow = window.open(poplocation + querystring, '', popposition);
            if (NewWindow.focus != null) {
                NewWindow.focus();
            }
            return false;
        }
     
   
    


</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ToolkitScriptManager ID="toolScript1" runat="server"></asp:ToolkitScriptManager>

    <div class="title">
		<h5>Employee Individual Report</h5>
    </div>
<asp:UpdatePanel ID="UP1" runat="server">
    <ContentTemplate>     
    <br />
    <uc3:Message ID="Message" runat="server" />
    <br />  
       
            <table width="95%" align="center" class="table">
                   <tr>
                        <td align="center" width="7%" class="label"><%--First Name--%></td>
                        <td align="left" width="12%">
                            <asp:TextBox ID="txtFName" runat="server" CssClass="textbox" Width="110px" Visible="false"></asp:TextBox>
                        </td>
                        <td align="center" width="17%" class="label" style="padding-top:7px">Employee</td>
                        <td align="left" width="12%">
                            <%--<asp:DropDownList ID="ddlEmployee" runat="server" CssClass="dropdownList" 
                                Width="120px" DataTextField="FullName" DataValueField="EmployeeId" 
                                onselectedindexchanged="ddlEmployee_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>--%>
                            <asp:ComboBox ID="ddlEmployee" runat="server" CssClass="WindowsStyle" DropDownStyle="DropDown"
                                AutoCompleteMode="SuggestAppend" CaseSensitive="false" Width="360px" DataValueField="EmployeeId"
                                DataTextField="FullName">
                            </asp:ComboBox>
                        </td>                       
                       
                       
                        <td align="left" style="padding-top:7px">
                            <asp:Button ID="btnShow" runat="server" Text="Show" CssClass="button" 
                                OnClientClick="return Validation()" onclick="btnShow_Click" />
                        </td>
                    </tr> 
                  </table>
                  <br />
                  
      
    </ContentTemplate>
</asp:UpdatePanel>       
</asp:Content>
