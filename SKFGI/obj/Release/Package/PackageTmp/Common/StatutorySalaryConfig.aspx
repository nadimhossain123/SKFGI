<%@ Page Language="C#" MasterPageFile="~/MasterAdmin.Master" AutoEventWireup="true" CodeBehind="StatutorySalaryConfig.aspx.cs" Inherits="CollegeERP.Common.StatutorySalaryConfig" Title="Statutory Salary Fixation" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register src="../UserControl/Message.ascx" tagname="Message" tagprefix="uc3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script type="text/javascript">

    function Validation()
    {
         if (isValidDate(document.getElementById('<%=txtEffectiveDate.ClientID%>').value) == false) {
            alert("Enter Effective Date in DD/MM/YYYY format");
            return false;
            } 
         else if (isNumber(document.getElementById('<%=txtEmployersPFCntrb.ClientID%>').value) == false || isNumber(document.getElementById('<%=txtEmployeesPFCntrb.ClientID%>').value) == false || isNumber(document.getElementById('<%=txtEmployersESICntrb.ClientID%>').value) == false || isNumber(document.getElementById('<%=txtEmployeesESICntrb.ClientID%>').value) == false || isNumber(document.getElementById('<%=txtESILimit.ClientID%>').value) == false || isNumber(document.getElementById('<%=txtEmployerPension.ClientID%>').value) == false || isNumber(document.getElementById('<%=txtPFAdminCharges.ClientID%>').value) == false || isNumber(document.getElementById('<%=txtEDLICharges.ClientID%>').value) == false || isNumber(document.getElementById('<%=txtEDLIAdminCharges.ClientID%>').value) == false) {
            alert("Enter All Amount Field in Proper Format");
            return false;
            }  
         else {return true;}            
    }
    
    function isValidDate(s) {
        // format D(D)/M(M)/(YY)YY
        var dateFormat = /^\d{1,4}[\.|\/|-]\d{1,2}[\.|\/|-]\d{1,4}$/;

        if (dateFormat.test(s)) {
            // remove any leading zeros from date values
            s = s.replace(/0*(\d*)/gi, "$1");
            var dateArray = s.split(/[\.|\/|-]/);

            // correct month value
            dateArray[1] = dateArray[1] - 1;

            // correct year value
            if (dateArray[2].length < 4) {
                // correct year value
                dateArray[2] = (parseInt(dateArray[2]) < 50) ? 2000 + parseInt(dateArray[2]) : 1900 + parseInt(dateArray[2]);
            }

            var testDate = new Date(dateArray[2], dateArray[1], dateArray[0]);
            if (testDate.getDate() != dateArray[0] || testDate.getMonth() != dateArray[1] || testDate.getFullYear() != dateArray[2]) {
                return false;
            } else {
                return true;
            }
        } else {
            return false;
        }
    }
    
    function isNumber(n) {
        return !isNaN(parseFloat(n)) && isFinite(n);
    }
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  <asp:ToolkitScriptManager ID="Tool1" runat="server"></asp:ToolkitScriptManager>  
  
    <div class="title">
		<h5>Statutory Salary Fixation</h5>
    </div>
    <asp:UpdatePanel ID="UP1" runat="server">
    <ContentTemplate>
    <div style="width:740px;">
      <uc3:Message ID="Message" runat="server" />  
      <br />
      <table width="100%" align="center" class="table">
        <tr>
           <td align="left" width="30%" class="label">Effective Date (dd/mm/yyyy)<span class="req">*</span></td>
           <td align="left" width="20%">
                <asp:TextBox ID="txtEffectiveDate" runat="server" CssClass="textbox_required" Width="120px"></asp:TextBox>
                <asp:CalendarExtender ID="CalendarExtender1" runat="server" CssClass="cal_Theme1"
                         PopupPosition="BottomRight" Format="dd/MM/yyyy" TargetControlID="txtEffectiveDate" OnClientDateSelectionChanged="" Enabled="True">
                 </asp:CalendarExtender>
           </td>
           <td align="left" width="30%" class="label">Employer's Contribution To PF(%)</td>
           <td align="left" width="20%">
             <asp:TextBox ID="txtEmployersPFCntrb" runat="server" CssClass="textbox" Width="120px"></asp:TextBox>
           </td>  
        </tr>
        
       
        
        <tr>
           <td align="left" width="30%" class="label">Employee's Contribution To PF(%)</td>
           <td align="left" width="20%">
                <asp:TextBox ID="txtEmployeesPFCntrb" runat="server" CssClass="textbox" Width="120px"></asp:TextBox>
           </td>
           <td align="left" width="30%" class="label">Employer's Contribution To ESI(%)</td>
           <td align="left" width="20%">
             <asp:TextBox ID="txtEmployersESICntrb" runat="server" CssClass="textbox" Width="120px"></asp:TextBox>
           </td>
        </tr>
        
        <tr>
           <td align="left" width="30%" class="label">Employee's Contribution To ESI(%)</td>
           <td align="left" width="20%">
                <asp:TextBox ID="txtEmployeesESICntrb" runat="server" CssClass="textbox" Width="120px"></asp:TextBox>
           </td>
           <td align="left" width="30%" class="label">ESI Limit</td>
           <td align="left" width="20%">
             <asp:TextBox ID="txtESILimit" runat="server" CssClass="textbox" Width="120px"></asp:TextBox>
           </td>
        </tr>    
        
        
        <tr>
           <td align="left" width="30%" class="label">Employer's Pension(%)</td>
           <td align="left" width="20%">
                <asp:TextBox ID="txtEmployerPension" runat="server" CssClass="textbox" Width="120px"></asp:TextBox>
           </td>
           <td align="left" width="30%" class="label">PF Admin Chargese(%)</td>
           <td align="left" width="20%">
             <asp:TextBox ID="txtPFAdminCharges" runat="server" CssClass="textbox" Width="120px"></asp:TextBox>
           </td>
        </tr>   
        
       
        
        <tr>
           <td align="left" width="30%" class="label">EDLI Charges(%)</td>
           <td align="left" width="20%">
                <asp:TextBox ID="txtEDLICharges" runat="server" CssClass="textbox" Width="120px"></asp:TextBox>
           </td>
           <td align="left" width="30%" class="label">EDLI Admin Chargese(%)</td>
           <td align="left" width="30%">
             <asp:TextBox ID="txtEDLIAdminCharges" runat="server" CssClass="textbox" Width="120px"></asp:TextBox>
           </td>
        </tr>  
        
               
         
    </table>
    
    <br />
    <table width="100%" align="center">
        <tr>
            <td width="30%" align="left"></td>
            <td width="70%" align="right">
                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="button" 
                   OnClientClick="javascript:return Validation()" onclick="btnSave_Click"   />
                &nbsp;
                <asp:Button ID="btnCancel" runat="server" CssClass="button" Text="Reset" onclick="btnCancel_Click"     
                    />
            </td>
        </tr>
    </table>
   </div> 
   </ContentTemplate>
    </asp:UpdatePanel>
    
</asp:Content>
