<%@ Page Language="C#" MasterPageFile="~/MasterAdmin.Master" AutoEventWireup="true"
    CodeBehind="SalaryHeadUpdation.aspx.cs" Inherits="CollegeERP.Payroll.SalaryHeadUpdation"
    Title="Salary Head Modification" %>
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../UserControl/Message.ascx" TagName="Message" TagPrefix="uc3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script type="text/javascript">
        function SalaryHeadValidation()
        {
            if (document.getElementById('<%=txtAmount.ClientID%>').value == '')
                return ShowMsg('Please Enter Amount');
            else if (document.getElementById('<%=ddlSalaryHead.ClientID%>').selectedIndex == 0)
                return ShowMsg('Please Select Salary Head');
            else
                return true;
        }
         function ShowMsg(msg)
        {
            alert(msg);
            return false;
        }
         function CheckAll(checkbox) {

            var gv = document.getElementById('<%=dgvEmployee.ClientID%>');
            var arr = gv.getElementsByTagName("input");
            for (var i = 0; i < arr.length; i++) {
                if (arr[i].type == 'checkbox') {
                    if (checkbox.checked == true) {
                        arr[i].checked = true;

                    }
                    else {
                        arr[i].checked = false;

                    }
                }
            }

        }
</script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ToolkitScriptManager ID="ToolScript1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="title">
        <h5>
            Salary Head Updation</h5>
    </div>
    <br />
      <asp:UpdatePanel ID="Up1" runat="server">
        <ContentTemplate>
        <br />
        <uc3:Message ID="Message" runat="server" />
        <br />
    <table width="78%" align="center" class="table">
        <tr>
            <td align="left" width="15%" class="label">
                Salary Head :
            </td>
            
            <td align="left" width="30%">
                            <asp:DropDownList ID="ddlSalaryHead" runat="server" 
                    CssClass="dropdownList" Width="180px"
                                DataValueField="SalaryHeadId" DataTextField="SalaryHeadDetails" 
                                AutoPostBack="true" onselectedindexchanged="ddlSalaryHead_SelectedIndexChanged" 
                                >
                            </asp:DropDownList>
            </td>
            <td align="center" width="15%" class="label">
                Enter Percentage :
            </td>
            <td align="left" width="15%">
              <%--  <asp:DropDownList ID="ddlFinYear" runat="server" CssClass="dropdownList" Width="150px">
                    <asp:ListItem Value="2009" Text="2009-2010"></asp:ListItem>
                    <asp:ListItem Value="2010" Text="2010-2011"></asp:ListItem>
                    <asp:ListItem Value="2011" Text="2011-2012"></asp:ListItem>
                    <asp:ListItem Value="2012" Text="2012-2013" Selected="True"></asp:ListItem>
                    <asp:ListItem Value="2013" Text="2013-2014"></asp:ListItem>
                    <asp:ListItem Value="2014" Text="2014-2015"></asp:ListItem>
                    <asp:ListItem Value="2015" Text="2015-2016"></asp:ListItem>
                    <asp:ListItem Value="2016" Text="2016-2017"></asp:ListItem>
                </asp:DropDownList>--%>
                <asp:TextBox ID="txtAmount" runat="server" Width="50px"></asp:TextBox>
            </td>
            <td align="left">
                <asp:Button ID="btnUpdate" runat="server" CssClass="button" Text="Update Amount"  OnClientClick="javascript:return SalaryHeadValidation()"
                    onclick="btnUpdate_Click" />
                    &nbsp;&nbsp;
                      <asp:Button ID="btnCancel" runat="server" CssClass="button" Text="Reset" 
                    onclick="btnCancel_Click" />
            </td>
        </tr>
    </table>
    
    <br />
    <br />
    <h6 align="left" style="color: #00356A;">
        Salary Details</h6>
    <table width="78%" align="center" class="table">
        <tr>
            <td align="center">
                &nbsp;</td>
        </tr>
         <tr>
                        <td align="left">
                            <asp:CheckBox ID="ChkSelect" runat="server" Text="Select All" onclick="javascript:CheckAll(this);" />
                        </td>
                    </tr>
        <tr>
            <td align="center">
                 <asp:GridView ID="dgvEmployee" runat="server" AutoGenerateColumns="false" Width="100%"
                                AllowPaging="false" DataKeyNames="EmployeeId" OnRowEditing="dgvEmployee_RowEditing">
                                <Columns>
                                    <asp:TemplateField HeaderText="Select">
                                        <ItemTemplate >
                                             <asp:CheckBox ID="ChkSelect" runat="server"  />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="EmpCode" HeaderText="Employee Code" />
                                    <asp:BoundField DataField="FullName" HeaderText="Employee Name" />
                                   <%-- <asp:BoundField DataField="EmployeeType" HeaderText="Employee Type" />--%>
                                    <asp:BoundField DataField="PayableBasic" HeaderText="Payable Basic" HeaderStyle-HorizontalAlign="Right" />
                                    <asp:BoundField DataField="Percentage" HeaderText="Percentage(%)" HeaderStyle-HorizontalAlign="Right" DataFormatString="{0:F2}" />
                                    <asp:BoundField DataField="Amount" HeaderText="Payable Amount" HeaderStyle-HorizontalAlign="Right" DataFormatString="{0:F2}" />
                                  
                                </Columns>
                                 <EmptyDataTemplate>
                            <table style="height: 10px; width: 100%;">
                                <tr align="left" class="HeaderStyle">
                                    <th scope="col">
                                        
                                    </th>
                                </tr>
                                <tr class="RowStyle">
                                    <td>
                                        Sorry! No Employee Found.
                                    </td>
                            </table>
                        </EmptyDataTemplate>
                                <HeaderStyle CssClass="HeaderStyle" />
                                <RowStyle CssClass="RowStyle" />
                                <AlternatingRowStyle CssClass="AltRowStyle" />
                                <PagerStyle CssClass="PagerStyle" />
                            </asp:GridView>
            </td>
        </tr>
    </table>
    </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
