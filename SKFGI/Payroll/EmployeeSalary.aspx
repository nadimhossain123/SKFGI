<%@ Page Language="C#" MasterPageFile="~/MasterAdmin.Master" AutoEventWireup="true"
    CodeBehind="EmployeeSalary.aspx.cs" Inherits="CollegeERP.Payroll.EmployeeSalary"
    Title="Salary Setting" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../UserControl/Message.ascx" TagName="Message" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .Img
        {
            cursor: pointer;
            width: 38px;
            height: 26px;
        }
    </style>

    <script type="text/javascript">
    
        
        function Validation()
        {
            var HasPTax=document.getElementById('<%=HidHasPTax.ClientID%>').value;
        
            if (document.getElementById('<%=txtEmployeeName.ClientID%>').value == '')
                return ShowMsg('Please Select an Employee to Edit');
            else if (document.getElementById('<%=txtBasic.ClientID%>').value == '' || parseFloat(document.getElementById('<%=txtBasic.ClientID%>').value) == 0)
                return ShowMsg('Enter a Valid Basic Amount');
            else if (document.getElementById('<%=txtGradePay.ClientID%>').value == '')
                return ShowMsg('Enter Grade Pay');
            else if (HasPTax == 'Y' && document.getElementById('<%=ddlPTax.ClientID%>').selectedIndex == 0)
                return ShowMsg('You Must Select a Valid P.Tax Zone When P.Tax Is Eligible For The Employee');
            else 
                return true;
        }
        
        function SalaryHeadValidation()
        {
            if (document.getElementById('<%=txtEmployeeName.ClientID%>').value == '')
                return ShowMsg('Please Select an Employee to Edit');
            else if (document.getElementById('<%=ddlSalaryHead.ClientID%>').selectedIndex == 0)
                return ShowMsg('Please Selecet Salary Head');
            else if (document.getElementById('<%=txtHeadPercent.ClientID%>').value == '')
                return ShowMsg('Enter Head Percentage');
            else if (document.getElementById('<%=txtHeadAmount.ClientID%>').value == '')
                return ShowMsg('Enter Head Amount');
            else 
                return true;
        }
        
        function ShowMsg(msg)
        {
            alert(msg);
            return false;
        }
        
        function OneTextToOther() 
        {
            var FixedPF= document.getElementById('<%=ChkIsFixedPF.ClientID%>').checked;
            var PFPer= document.getElementById('<%=txtPFPercent.ClientID%>').value;
            var EmployerPFPer= document.getElementById('<%=txtEmployerPFPercent.ClientID%>').value;
            var Basic= document.getElementById('<%=txtBasic.ClientID%>').value;
            
            if (isNumber(Basic))
            {
               var EmployeePF= (Basic * PFPer)/100;
               var EmployerPF= (Basic * EmployerPFPer)/100;
               if (FixedPF)
               {
                    EmployeePF=(EmployeePF > 1800) ? 1800 : EmployeePF;
                    EmployerPF=(EmployerPF > 1800) ? 1800 : EmployerPF;
               }  
               document.getElementById('<%=txtEmployeePFAmt.ClientID%>').value = EmployeePF;
               document.getElementById('<%=txtEmployerPFAmt.ClientID%>').value = EmployerPF;          
            }
              
        } 
        
     function SubHeadAmt() 
      {

            var Percentage= document.getElementById('<%=txtHeadPercent.ClientID%>').value;
            var Basic= document.getElementById('<%=txtBasic.ClientID%>').value;
            
            if (isNumber(Percentage) && isNumber(Basic))
            {
                document.getElementById('<%=txtHeadAmount.ClientID%>').value = (Basic * Percentage)/100;
            }
            
             
      }  
       function isNumber(n) {
        return !isNaN(parseFloat(n)) && isFinite(n);
        }
        
     function openpopup(poplocation, querystring, popheight, popwidth,poptop, popleft)
     {                
        var popposition='left = 300, top=90, width=680,align=center, height=400,menubar=no, scrollbars=yes, resizable=no';                
               
        var NewWindow = window.open(poplocation,'',popposition);                
        if (NewWindow.focus!=null){                        
        NewWindow.focus();                
        } 
        return false;       
     } 
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ToolkitScriptManager ID="ToolScript1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="title">
        <h5>
            Salary Setting</h5>
    </div>
    <asp:UpdatePanel ID="Up1" runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="HidHasPTax" runat="server" />
            <br />
            <uc3:Message ID="Message" runat="server" />
            <br />
            <h6 align="left" style="color: #00356A;">
                Salary Basic Details</h6>
            <div style="width: 680px;">
                <table width="100%" align="center" class="table">
                    <tr>
                        <td align="left" width="20%" class="label">
                            Employee Name
                        </td>
                        <td align="left" width="30%">
                            <asp:TextBox ID="txtEmployeeName" runat="server" CssClass="textbox_disabled" Width="180px"
                                Enabled="false"></asp:TextBox>
                        </td>
                        <td align="left" width="20%" class="label">
                            Payable Basic<span class="req">*</span>
                        </td>
                        <td align="left" width="30%">
                            <asp:TextBox ID="txtBasic" runat="server" CssClass="textbox_required" Width="180px"
                                onkeyup="OneTextToOther();" onkeypress="return AmountOnly('txtBasic',this);"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" width="20%" class="label">
                            Employee PF(%)
                        </td>
                        <td align="left" width="30%">
                            <asp:TextBox ID="txtPFPercent" runat="server" CssClass="textbox_disabled" Width="180px"
                                Enabled="false"></asp:TextBox>
                        </td>
                        <td align="left" width="20%" class="label">
                            Employee PF Amount
                        </td>
                        <td align="left" width="30%">
                            <asp:TextBox ID="txtEmployeePFAmt" runat="server" CssClass="textbox_disabled" Width="180px"
                                Enabled="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" width="20%" class="label">
                            Employer PF(%)
                        </td>
                        <td align="left" width="30%">
                            <asp:TextBox ID="txtEmployerPFPercent" runat="server" CssClass="textbox_disabled"
                                Width="180px" Enabled="false"></asp:TextBox>
                        </td>
                        <td align="left" width="20%" class="label">
                            Employer PF Amount
                        </td>
                        <td align="left" width="30%">
                            <asp:TextBox ID="txtEmployerPFAmt" runat="server" CssClass="textbox_disabled" Width="180px"
                                Enabled="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" width="20%" class="label">
                            PTax<span class="req">*</span>
                        </td>
                        <td align="left" width="30%">
                            <asp:DropDownList ID="ddlPTax" runat="server" CssClass="dropdownList" Width="180px"
                                DataValueField="PTaxId" DataTextField="PTaxStateDescription">
                            </asp:DropDownList>
                        </td>
                        <td align="left" width="20%" class="label">
                            Grade Pay<span class="req">*</span>
                        </td>
                        <td align="left" width="30%">
                            <asp:TextBox ID="txtGradePay" runat="server" CssClass="textbox_required" Width="180px"
                                onkeypress="return AmountOnly('txtGradePay',this);"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" width="20%" class="label">
                            Fixed PF
                        </td>
                        <td align="left" width="30%">
                            <asp:CheckBox ID="ChkIsFixedPF" runat="server" onclick="OneTextToOther()" />
                        </td>
                        <td align="left" width="20%">
                        </td>
                        <td align="left" width="30%">
                            <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="button" OnClientClick="javascript:return Validation()"
                                OnClick="btnSave_Click" />
                            &nbsp;
                            <asp:Button ID="btnCancel" runat="server" CssClass="button" Text="Reset" OnClick="btnCancel_Click" />
                        </td>
                    </tr>
                </table>
            </div>
            <br />
            <h6 align="left" style="color: #00356A;">
                Salary Head Details</h6>
            <div style="width: 680px;">
                <table width="100%" align="center" class="table">
                    <tr>
                        <td align="left" width="50%" class="label">
                            Salary Head<span class="req">*</span>
                        </td>
                        <td align="left" class="label" width="20%">
                            Percent(%)
                        </td>
                        <td align="left" class="label" width="20%">
                            Amount
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" width="50%">
                            <asp:DropDownList ID="ddlSalaryHead" runat="server" CssClass="dropdownList" Width="240px"
                                DataValueField="SalaryHeadId" DataTextField="SalaryHeadDetails" 
                                AutoPostBack="true" onselectedindexchanged="ddlSalaryHead_SelectedIndexChanged">
                            </asp:DropDownList>
                            <img id="btnNewSalaryHead" runat="server" src="~/Images/newLeft.gif" style="cursor: pointer;
                                padding-top: 5px;" />
                        </td>
                        <td align="left" width="20%">
                            <asp:TextBox ID="txtHeadPercent" runat="server" CssClass="textbox" Width="100px"
                                MaxLength="6" onkeyup="SubHeadAmt();" onkeypress="return AmountOnly('txtHeadPercent',this);"></asp:TextBox>
                        </td>
                        <td align="left" width="20%">
                            <asp:TextBox ID="txtHeadAmount" runat="server" CssClass="textbox" Width="100px" onkeypress="return AmountOnly('txtHeadAmount',this);"></asp:TextBox>
                        </td>
                        <td align="left">
                            <asp:Button ID="btnAdd" runat="server" CssClass="button" Text="Add" OnClientClick="javascript:return SalaryHeadValidation()"
                                OnClick="btnAdd_Click" />
                        </td>
                    </tr>
                </table>
                <table width="100%" align="center" class="table">
                    <tr>
                        <td align="center">
                            <asp:GridView ID="dgvSalaryHead" runat="server" AutoGenerateColumns="false" AllowPaging="false"
                                DataKeyNames="EmployeeSalaryHeadId" Width="100%" OnRowDeleting="dgvSalaryHead_RowDeleting"
                                >
                                <Columns>
                                    <asp:BoundField DataField="SalaryHeadDetails" HeaderText="Head" />
                                    <asp:BoundField DataField="EmployeeSalaryHeadPercent" HeaderText="Percent(%)" />
                                    <asp:BoundField DataField="EmployeeSalaryHeadAmount" HeaderText="Amount" DataFormatString="{0:F0}" />
                                    <asp:TemplateField ShowHeader="false">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/Images/Delete_icon.gif"
                                                CommandName="Delete" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <HeaderStyle CssClass="HeaderStyle" />
                                <RowStyle CssClass="RowStyle" />
                                <AlternatingRowStyle CssClass="AltRowStyle" />
                                <PagerStyle CssClass="PagerStyle" />
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </div>
            <br />
            <h6 align="left" style="color: #00356A;">
                Employee List</h6>
            <div style="width: 980px;">
                <table width="100%" align="center" class="table">
                    <tr>
                        <td align="left" width="15%" class="label">
                            Emp Code
                        </td>
                        <td align="left" width="18%">
                            <asp:TextBox ID="txtEmpCode" runat="server" CssClass="textbox" Width="110px"></asp:TextBox>
                        </td>
                        <td align="left" width="15%" class="label">
                            First Name
                        </td>
                        <td align="left" width="18%">
                            <asp:TextBox ID="txtFNameSearch" runat="server" CssClass="textbox" Width="110px"></asp:TextBox>
                        </td>
                        <td align="left" width="15%" class="label">
                            <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="button" OnClick="btnSearch_Click" />
                        </td>
                        <td align="left" width="18%">
                        </td>
                    </tr>
                </table>
                <br />
                <table width="100%" align="center">
                    <tr>
                        <td align="center">
                            <asp:GridView ID="dgvEmployee" runat="server" AutoGenerateColumns="false" Width="100%"
                                AllowPaging="false" DataKeyNames="EmployeeId" OnRowEditing="dgvEmployee_RowEditing">
                                <Columns>
                                    <asp:BoundField DataField="EmpCode" HeaderText="Employee Code" />
                                    <asp:BoundField DataField="FullName" HeaderText="Employee Name" />
                                    <asp:BoundField DataField="EmployeeType" HeaderText="Employee Type" />
                                    <asp:BoundField DataField="IsActive" HeaderText="Status" />
                                    <asp:BoundField DataField="SixPayBasic" HeaderText="6th Pay Basic" DataFormatString="{0:F0}" />
                                    <asp:BoundField DataField="GradePay" HeaderText="Grade Pay" DataFormatString="{0:F0}" />
                                    <asp:BoundField DataField="PayableBasic" HeaderText="Payable Basic" DataFormatString="{0:F0}" />
                                    <asp:BoundField DataField="GrossAmount" HeaderText="Gross" DataFormatString="{0:F0}" />
                                    <asp:BoundField DataField="EmployerPF" HeaderText="Employer PF" DataFormatString="{0:F0}" />
                                    <asp:BoundField DataField="CTC" HeaderText="CTC" DataFormatString="{0:F0}" />
                                    <asp:TemplateField ShowHeader="false">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit_icon.gif" CommandName="Edit" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <HeaderStyle CssClass="HeaderStyle" />
                                <RowStyle CssClass="RowStyle" />
                                <AlternatingRowStyle CssClass="AltRowStyle" />
                                <PagerStyle CssClass="PagerStyle" />
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </div>
            <br />
            <br />
            <br />
            <br />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
