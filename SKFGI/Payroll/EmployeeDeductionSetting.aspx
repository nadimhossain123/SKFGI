<%@ Page Language="C#" MasterPageFile="~/MasterAdmin.Master" AutoEventWireup="true"
    CodeBehind="EmployeeDeductionSetting.aspx.cs" Inherits="CollegeERP.Payroll.EmployeeDeductionSetting"
    Title="Deduction Setting" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">
        function Validation()
        {
            if (document.getElementById('<%=ddlEmployee.ClientID%>').selectedIndex == 0)
                return ShowMsg('Please Select Employee');
            else if (document.getElementById('<%=ddlDeductionHead.ClientID%>').selectedIndex == 0)
                return ShowMsg('Please Select Deduction Head');
            else if (document.getElementById('<%=ddlYear.ClientID%>').selectedIndex == 0)
                return ShowMsg('Please Select Deduction Year');
            else if (document.getElementById('<%=ddlMonth.ClientID%>').selectedIndex == 0)
                return ShowMsg('Please Select Deduction Month'); 
            else if (document.getElementById('<%=txtAmount.ClientID%>').value == '' ||  parseFloat(document.getElementById('<%=txtAmount.ClientID%>').value) == 0)
                return ShowMsg('Please Enter Deduction Amount');
            else
                return confirm('Are You Sure?');               
        }
        
        function ShowMsg(msg)
        {
            alert(msg);
            return false;
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ToolkitScriptManager ID="ToolScript1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="title">
        <h5>
            Salary Deduction Setting</h5>
    </div>
    <asp:UpdatePanel ID="Up1" runat="server">
        <ContentTemplate>
            <div style="width: 740px;">
                <table width="100%" align="center" class="table">
                    <tr>
                        <td align="left" class="label" width="20%">
                            Employee
                        </td>
                        <td align="left" width="30%">
                            <asp:DropDownList ID="ddlEmployee" runat="server" CssClass="dropdownList" Width="150px"
                                DataValueField="EmployeeId" DataTextField="FullName">
                            </asp:DropDownList>
                        </td>
                        <td align="left" class="label" width="20%">
                            Deduction Head
                        </td>
                        <td align="left" width="30%">
                            <asp:DropDownList ID="ddlDeductionHead" runat="server" CssClass="dropdownList" Width="150px"
                                DataValueField="SalaryDeductionHeadId" DataTextField="SalaryDeductionHeadDetails">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" class="label" width="20%">
                            Deduction Year
                        </td>
                        <td align="left" width="30%">
                            <asp:DropDownList ID="ddlYear" runat="server" CssClass="dropdownList" Width="150px">
                            </asp:DropDownList>
                        </td>
                        <td align="left" class="label" width="20%">
                            Deduction Month
                        </td>
                        <td align="left" width="30%">
                            <asp:DropDownList ID="ddlMonth" runat="server" CssClass="dropdownList" Width="150px">
                                <asp:ListItem Value="0" Text="Select"></asp:ListItem>
                                <asp:ListItem Value="1" Text="JAN"></asp:ListItem>
                                <asp:ListItem Value="2" Text="FEB"></asp:ListItem>
                                <asp:ListItem Value="3" Text="MARCH"></asp:ListItem>
                                <asp:ListItem Value="4" Text="APRIL"></asp:ListItem>
                                <asp:ListItem Value="5" Text="MAY"></asp:ListItem>
                                <asp:ListItem Value="6" Text="JUN"></asp:ListItem>
                                <asp:ListItem Value="7" Text="JULY"></asp:ListItem>
                                <asp:ListItem Value="8" Text="AUG"></asp:ListItem>
                                <asp:ListItem Value="9" Text="SEP"></asp:ListItem>
                                <asp:ListItem Value="10" Text="OCT"></asp:ListItem>
                                <asp:ListItem Value="11" Text="NOV"></asp:ListItem>
                                <asp:ListItem Value="12" Text="DEC"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" class="label" width="20%">
                            Deduction Amount
                        </td>
                        <td align="left" width="30%">
                            <asp:TextBox ID="txtAmount" runat="server" CssClass="textbox" Width="150px" onkeypress="return AmountOnly('txtAmount',this);"></asp:TextBox>
                        </td>
                        <td>
                        </td>
                        <td align="left">
                            <asp:Button ID="btnSave" runat="server" CssClass="button" Text="Save" 
                                OnClientClick="return Validation()" onclick="btnSave_Click" />&nbsp;
                            <asp:Button ID="btnSearch" runat="server" CssClass="button" Text="Search" 
                                onclick="btnSearch_Click" />
                        </td>
                    </tr>
                </table>
                <table width="100%" align="center" class="table">
                    <tr>
                        <td align="center">
                            <asp:GridView ID="dgvDeduction" runat="server" AutoGenerateColumns="false" Width="100%"
                                GridLines="None" AllowPaging="false" 
                                DataKeyNames="EmployeeSalaryDeductionHeadId" 
                                onrowdeleting="dgvDeduction_RowDeleting">
                                <Columns>
                                    <asp:BoundField DataField="EmpCode" HeaderText="Emp Code" />
                                    <asp:BoundField DataField="FullName" HeaderText="Employee Name" />
                                    <asp:BoundField DataField="SalaryDeductionHeadDetails" HeaderText="Deduction Head" />
                                    <asp:BoundField DataField="DeductionMonth" HeaderText="Month" />
                                    <asp:BoundField DataField="DeductionYear" HeaderText="Year" />
                                    <asp:BoundField DataField="DeductionAmount" HeaderText="Amount" DataFormatString="{0:F0}" />
                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/Images/Delete_icon.gif"
                                                CommandName="Delete" OnClientClick="return confirm('Are You Sure?')" />
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
