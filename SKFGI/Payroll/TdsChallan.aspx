<%@ Page Language="C#" MasterPageFile="~/MasterAdmin.Master" AutoEventWireup="true" CodeBehind="TdsChallan.aspx.cs" 
Inherits="CollegeERP.Payroll.TdsChallan" Title="TDS Challan" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
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
<asp:ToolkitScriptManager ID="toolScript1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="title">
		<h5>TDS Challan</h5>
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
                            DataTextField="MonthName" AutoPostBack="true"
                            onselectedindexchanged="ddlMonth_SelectedIndexChanged"></asp:DropDownList>
                    </td>  
                </tr>
                 <tr>
                    <td align="left" width="20%" class="label">Cheque No</td>
                    <td align="left" width="30%">
                       <asp:TextBox ID="txtChequeNo" runat="server" CssClass="textbox" Width="110px"></asp:TextBox>
                    </td>
                    <td align="left" width="20%" class="label">Cheque date</td>
                    <td align="left" width="30%">
                        <asp:TextBox ID="txtChequeDate" runat="server" CssClass="textbox" Width="110px"></asp:TextBox>
                            <asp:CalendarExtender ID="CalendarExtender2" runat="server" CssClass="cal_Theme1"
                                PopupPosition="BottomRight" Format="dd MMM yyyy" TargetControlID="txtChequeDate"
                                OnClientDateSelectionChanged="" Enabled="True">
                        </asp:CalendarExtender>
                    </td>  
                </tr>
                  <tr>
                    <td align="left" width="20%" class="label">Income tax</td>
                    <td align="left" width="30%">
                        <asp:TextBox ID="txtIncomeTax" runat="server" CssClass="textbox" Width="110px" onkeypress="return AmountOnly('txtIncomeTax',this);"></asp:TextBox>
                    </td>
                    <td align="left" width="20%" class="label">Surcharge</td>
                    <td align="left" width="30%">
                        <asp:TextBox ID="txtSurcharge" runat="server" CssClass="textbox" Width="110px" onkeypress="return AmountOnly('txtSurcharge',this);"></asp:TextBox>
                           
                    </td>  
                </tr>
                  <tr>
                    <td align="left" width="20%" class="label">Education Cess</td>
                    <td align="left" width="30%">
                       <asp:TextBox ID="txtEducess" runat="server" CssClass="textbox" Width="110px" onkeypress="return AmountOnly('txtEducess',this);"></asp:TextBox>
                    </td>
                    <td align="left" width="20%" class="label">Interest</td>
                    <td align="left" width="30%">
                       <asp:TextBox ID="txtInterest" runat="server" CssClass="textbox" Width="110px" onkeypress="return AmountOnly('txtInterest',this);"></asp:TextBox>  
                    </td>  
                </tr>
                 <tr>
                    <td align="left" width="20%" class="label">Penalty</td>
                    <td align="left" width="30%">
                       <asp:TextBox ID="txtPenalty" runat="server" CssClass="textbox" Width="110px" onkeypress="return AmountOnly('txtPenalty',this);"></asp:TextBox>
                    </td>
                    <td align="left" width="20%" class="label"></td>
                    <td align="left" width="30%">
                       <asp:CheckBox ID="chkFinalize" runat="server" Text="Finalize" />
                    </td>  
                </tr>
                </table>
                     
          <table width="100%" align="center" class="table">
            <tr>
                    <td width="10%" align="left"></td>
                    <td width="30%" align="center">
                        
                    </td>
                    <td width="30%" align="right">
                        <asp:Button ID="btnSave" runat="server" CssClass="button" Text="Save" 
                            OnClientClick="return Validation()" onclick="btnSave_Click" />
                    </td>
                    <td width="30%" align="left">
                        <asp:Button ID="btnCancel" runat="server" CssClass="button" Text="Cancel" 
                             onclick="btnCancel_Click" />
                    </td>
                </tr>
          </table>     
     </div>           
</asp:Content>
