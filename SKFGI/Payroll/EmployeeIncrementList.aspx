<%@ Page Language="C#" MasterPageFile="~/MasterAdmin.Master" AutoEventWireup="true"
    CodeBehind="EmployeeIncrementList.aspx.cs" Inherits="CollegeERP.Payroll.EmployeeIncrementList"
    Title="Employee Increment List" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../UserControl/Message.ascx" TagName="Message" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script language="javascript" type="text/javascript">
        function DateValidation() {
            if (document.getElementById('<%=txtDateFrom.ClientID%>').value == '') {
                alert("Please Enter Date Range");
                return false;
            }
            if (document.getElementById('<%=txtDateTo.ClientID%>').value == '') {
                alert("Please Enter Date Range");
                return false;
            }
        }

        function CheckAll(checkbox) {

            var gv = document.getElementById('<%=dgvEmployee.ClientID%>');
            var arr = gv.getElementsByTagName("input");
            for (var i = 0; i < arr.length; i++) {
                if (arr[i].type == 'checkbox') {
                    if (checkbox.checked == true) {
                        arr[i].checked = true;
                        arr[i].parentNode.parentNode.className = 'SelectedRowStyle';
                    }
                    else {
                        arr[i].checked = false;
                        arr[i].parentNode.parentNode.className = 'RowStyle';
                    }
                }
            }

        }

        function ChangeCSS(Obj) {
            var row = Obj.parentNode.parentNode;
            if (Obj.checked)
                row.className = 'SelectedRowStyle';
            else
                row.className = 'RowStyle';
        }

        function Validation() {
            var gv = document.getElementById('<%=dgvEmployee.ClientID%>');
            var rowCount = gv.rows.length - 1;
            if (rowCount == 0) {
                alert("No Employee Found");
                return false;
            }
            else if (!Checkbox_Validation()) {
                alert("Please Select One Employee");
                return false;
            }
            else if (document.getElementById('<%=txtIncrementDate.ClientID%>').value == '') {
                alert("Please Enter Increment Date");
                return false;
            }
            else {
                return confirm('Are You Sure?');
            }
        }

        function Checkbox_Validation() {
            var flag = 0;
            var gv = document.getElementById('<%=dgvEmployee.ClientID%>');
            var arr = gv.getElementsByTagName("input");
            for (var i = 0; i < arr.length; i++) {
                if (arr[i].type == 'checkbox' && arr[i].checked == true) {
                    flag = 1;
                    break;
                }
            }

            if (flag == 0) {
                return false;
            }
            else {
                return true;
            }
        } 

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ToolkitScriptManager ID="toolScript1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="title">
        <h5>
            Employee Increment List</h5>
    </div>
    <div style="width: 900px;">
        <asp:UpdatePanel ID="UP1" runat="server">
            <ContentTemplate>
                <uc3:Message ID="Message" runat="server" />
                <br />
                <table width="100%">
                    <tr>
                        <td width="10%" class="label">
                            From:<span class="req">*</span>
                        </td>
                        <td width="20%">
                            <asp:TextBox ID="txtDateFrom" runat="server" CssClass="textbox_required" Width="110px"
                                onkeydown="return false;"></asp:TextBox>
                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" CssClass="cal_Theme1"
                                PopupPosition="BottomRight" Format="dd MMM yyyy" TargetControlID="txtDateFrom"
                                OnClientDateSelectionChanged="" Enabled="True">
                            </asp:CalendarExtender>
                        </td>
                        <td width="10%" class="label">
                            To:<span class="req">*</span>
                        </td>
                        <td width="20%">
                            <asp:TextBox ID="txtDateTo" runat="server" CssClass="textbox_required" Width="110px"
                                onkeydown="return false;"></asp:TextBox>
                            <asp:CalendarExtender ID="CalendarExtender2" runat="server" CssClass="cal_Theme1"
                                PopupPosition="BottomRight" Format="dd MMM yyyy" TargetControlID="txtDateTo"
                                OnClientDateSelectionChanged="" Enabled="True">
                            </asp:CalendarExtender>
                        </td>
                        <td>
                            <asp:Button ID="btnShow" CssClass="button" runat="server" Text="Show" OnClientClick="return DateValidation();"
                                OnClick="btnShow_Click" />
                            &nbsp
                            <asp:Button ID="btnCancel" CssClass="button" runat="server" Text="Reset" OnClick="btnCancel_Click" />
                        </td>
                    </tr>
                </table>
                <br />
                <table width="100%">
                    <tr>
                        <td>
                            <asp:CheckBox ID="ChkSelect" runat="server" Text="All" onclick="javascript:CheckAll(this);" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView ID="dgvEmployee" runat="server" AutoGenerateColumns="false" Width="100%" GridLines="None"
                                AllowPaging="false" CellPadding="0" CellSpacing="0" DataKeyNames="EmployeeId">
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="ChkSelect" runat="server" onclick="ChangeCSS(this);" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="EmpCode" HeaderText="Employee Code" />
                                    <asp:BoundField DataField="FullName" HeaderText="Employee Name" />
                                    <asp:BoundField DataField="CategoryName" HeaderText="Category" />
                                    <asp:BoundField DataField="LastEvaluationDate" HeaderText="Last Evaluation Date"
                                        DataFormatString="{0:dd/MM/yyyy}" NullDisplayText="" />
                                    <asp:BoundField DataField="EffectiveDate" HeaderText="Increment Date" DataFormatString="{0:dd/MM/yyyy}"
                                        NullDisplayText="" />
                                    <asp:BoundField DataField="EmployeeSalaryBasicAmount" HeaderText="Payable Basic"
                                        DataFormatString="{0:F0}" />
                                    <asp:BoundField DataField="IncrementAmount" HeaderText="Increment Amount" DataFormatString="{0:F0}" />
                                    <asp:BoundField DataField="GrossAmount" HeaderText="Gross" DataFormatString="{0:F0}" />
                                    <asp:BoundField DataField="CTC" HeaderText="CTC" DataFormatString="{0:F0}" />
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
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
                <br />
                <table width="100%">
                    <tr>
                        <td width="90%" class="label" align="right">
                            Increment Date:
                        </td>
                        <td align="right">
                            <asp:TextBox ID="txtIncrementDate" runat="server" CssClass="textbox_required" Width="110px"
                                onkeydown="return false;"></asp:TextBox>
                            <asp:CalendarExtender ID="CalendarExtender3" runat="server" CssClass="cal_Theme1"
                                PopupPosition="BottomRight" Format="dd MMM yyyy" TargetControlID="txtIncrementDate"
                                OnClientDateSelectionChanged="" Enabled="True">
                            </asp:CalendarExtender>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="right">
                            <asp:Button ID="btnUpdate" CssClass="button" runat="server" Text="Update" OnClientClick="return Validation();"
                                OnClick="btnUpdate_Click" />
                        </td>
                    </tr>
                </table>
                <br />
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
