<%@ Page Language="C#" MasterPageFile="~/MasterAdmin.Master" AutoEventWireup="true" CodeBehind="PtaxChallan.aspx.cs" 
Inherits="CollegeERP.Payroll.PtaxChallan" Title="P Tax Challan" %>
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
		<h5>P Tax Challan</h5>
    </div>
    <%--<asp:UpdatePanel ID="UP1" runat="server">
    <ContentTemplate>--%>
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
                    <td align="left" width="20%" class="label">P Tax</td>
                    <td align="left" width="30%">
                        <asp:TextBox ID="txtPtax" runat="server" CssClass="textbox" Width="110px" onkeypress="return AmountOnly('txtPtax',this);"></asp:TextBox>
                    </td>
                    <td align="left" width="20%" class="label">Penalty</td>
                    <td align="left" width="30%">
                        <asp:TextBox ID="txtPenalty" runat="server" CssClass="textbox" Width="110px" onkeypress="return AmountOnly('txtPenalty',this);"></asp:TextBox>
                           
                    </td>  
                </tr>
                  <tr>
                    <td align="left" width="20%" class="label">Comp. Money</td>
                    <td align="left" width="30%">
                       <asp:TextBox ID="txtCompMoney" runat="server" CssClass="textbox" Width="110px" onkeypress="return AmountOnly('txtCompMoney',this);"></asp:TextBox>
                    </td>
                    <td align="left" width="20%" class="label">Late Fees</td>
                    <td align="left" width="30%">
                       <asp:TextBox ID="txtLateFees" runat="server" CssClass="textbox" Width="110px" onkeypress="return AmountOnly('txtLateFees',this);"></asp:TextBox>  
                    </td>  
                </tr>
                </table>
                     
          <table width="100%" align="center" class="table">
            <tr>
                    <td width="10%" align="left"></td>
                    <td width="30%" align="center">
                        <asp:CheckBox ID="chkFinalize" runat="server" Text="Finalize" />
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
          <table style="width:80%">
            <tr>
                <td>
                    <input id="hdnPTaxId" type="hidden" runat="server" />
                     <asp:GridView ID="dgvPO" runat="server" AllowPaging="false" AutoGenerateColumns="false"
                                GridLines="None" Width="100%" DataKeyNames="Id" 
                                onrowediting="dgvPO_RowEditing">
                                <Columns>
                                    <asp:BoundField DataField="ChequeNo" HeaderText="Cheque No" />
                                    <asp:BoundField DataField="ChequeDate" HeaderText="Cheque Date" DataFormatString="{0:dd MMM yyyy}" />
                                    <asp:BoundField DataField="LateFees" HeaderText="Late Fees" />
                                    <asp:BoundField DataField="Penalty" HeaderText="Penalty" />
                                    <asp:BoundField DataField="Tax" HeaderText="Tax" />                                    
                                    <asp:TemplateField ShowHeader="false">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit_icon.gif" CommandName="Edit" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <PagerSettings Mode="Numeric" PageButtonCount="8" />
                                <HeaderStyle CssClass="HeaderStyle" />
                                <RowStyle CssClass="RowStyle" />
                                <AlternatingRowStyle CssClass="AltRowStyle" />
                            </asp:GridView>
                </td>
            </tr>
          </table>     
     </div>  
  <%--   </ContentTemplate>
     </asp:UpdatePanel>--%>
         
</asp:Content>
