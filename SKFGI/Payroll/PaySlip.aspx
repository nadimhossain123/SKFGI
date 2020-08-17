<%@ Page Language="C#" MasterPageFile="~/MasterAdmin.Master" AutoEventWireup="true" CodeBehind="PaySlip.aspx.cs" Inherits="CollegeERP.Payroll.PaySlip" Title="Monthly Pay Slip" %>
<%@ Register Src="../UserControl/Message.ascx" TagName="Message" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function Validation()
        {
            if (document.getElementById('<%=ddlYear.ClientID%>').selectedIndex == 0)
            {
                alert('Please Select Year');
                return false;
            }
            else if (document.getElementById('<%=ddlMonth.ClientID%>').selectedIndex == 0)
            {
                alert('Please Select Month');
                return false;
            }
            else {return true;}
        }
        
        
    function openpopup(poplocation, querystring, popheight, popwidth,poptop, popleft)
     {                
        var popposition='left = 200, top=20, width=950,align=center, height=650,menubar=no, scrollbars=yes, resizable=no';                
               
        var NewWindow = window.open(poplocation,'',popposition);                
        if (NewWindow.focus!=null){                        
        NewWindow.focus();                
        }   
        
          
     }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="title">
		<h5>Monthly Pay Slip</h5>
    </div>

    <div style="width:740px;">
            <uc3:Message ID="Message" runat="server" />
            <br />
            <table width="100%" align="center" class="table">
                 <tr>
                    <td align="left" width="20%" class="label">Year<span class="req">*</span></td>
                    <td align="left" width="30%">
                        <asp:DropDownList ID="ddlYear" runat="server" AutoPostBack="true" 
                            CssClass="dropdownList" Width="150px" DataValueField="YearNo" 
                            DataTextField="YearName" 
                            onselectedindexchanged="ddlYear_SelectedIndexChanged"></asp:DropDownList>
                    </td>
                    <td align="left" width="20%" class="label">Month</td>
                    <td align="left" width="30%">
                        <asp:DropDownList ID="ddlMonth" runat="server"
                            CssClass="dropdownList" Width="150px" DataValueField="MonthNo" 
                            DataTextField="MonthName"></asp:DropDownList>
                    </td>  
                </tr>
                
                </table>
                     
          <table width="100%" align="center" class="table">
            <tr>
                    <td colspan="3"></td>
                    <td width="30%" align="left">
                        <asp:Button ID="btnShow" runat="server" CssClass="button" Text="Download" 
                            OnClientClick="return Validation()" onclick="btnShow_Click" />
                    </td>
                </tr>
          </table>     
     </div>           
</asp:Content>
