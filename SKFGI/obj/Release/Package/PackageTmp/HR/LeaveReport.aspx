<%@ Page Language="C#" MasterPageFile="~/MasterAdmin.Master" AutoEventWireup="true"
    CodeBehind="LeaveReport.aspx.cs" Inherits="CollegeERP.HR.LeaveReport" Title="Employee Wise Leave Report" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">
        function Validation()
        {
             if (document.getElementById('<%=txtFrom.ClientID%>').value == '')
             {
                alert("Enter From Date");
                return false;
             }
             else  if (document.getElementById('<%=txtTo.ClientID%>').value == '')
             {
                alert("Enter To Date");
                return false;
             }
             else {return true;}
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ToolkitScriptManager ID="ToolScript1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="title">
        <h5>
            Datewise Leave Report</h5>
    </div>
    <div style="width: 740px;">
    
      <asp:UpdatePanel ID="Up1" runat="server">
        <ContentTemplate>
        <table width="100%" align="center" class="table">
            <tr>
                <td align="left" class="label" width="25%">
                Department
                </td>
                <td align="left"  width="25%">
                <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="dropdownList" 
                                Width="120px" DataTextField="DepartmentName" DataValueField="DepartmentId" 
                                 AutoPostBack="True" 
                        onselectedindexchanged="ddlDepartment_SelectedIndexChanged"></asp:DropDownList>
                </td>
                <td align="left" class="label" width="25%">
                Employee
                </td>
                <td align="left"  width="25%">
                <asp:DropDownList ID="ddlEmployee" runat="server" CssClass="dropdownList" 
                                Width="120px" DataTextField="FullName" DataValueField="EmployeeId" 
                                onselectedindexchanged="ddlEmployee_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                </td>
            </tr>
        </table>
         </ContentTemplate>
        </asp:UpdatePanel>
        <table width="100%" align="center" class="table">
            <tr>
                <td align="left" class="label" width="15%">
                    From Date
                </td>
                <td align="left" width="25%">
                    <asp:TextBox ID="txtFrom" runat="server" CssClass="textbox" Width="140px"></asp:TextBox>
                    <asp:CalendarExtender ID="CalendarExtender1" runat="server" CssClass="cal_Theme1"
                        PopupPosition="BottomRight" Format="dd MMM yyyy" TargetControlID="txtFrom" OnClientDateSelectionChanged=""
                        Enabled="True">
                    </asp:CalendarExtender>
                </td>
                <td align="left" class="label" width="15%">
                    To Date
                </td>
                <td align="left" width="25%">
                    <asp:TextBox ID="txtTo" runat="server" CssClass="textbox" Width="140px"></asp:TextBox>
                    <asp:CalendarExtender ID="CalendarExtender2" runat="server" CssClass="cal_Theme1"
                        PopupPosition="BottomRight" Format="dd MMM yyyy" TargetControlID="txtTo" OnClientDateSelectionChanged=""
                        Enabled="True">
                    </asp:CalendarExtender>
                </td>
                <td>
                    <asp:Button ID="btnSearch" runat="server" CssClass="button" Text="Search" OnClientClick="return Validation()"
                        OnClick="btnSearch_Click" />
                </td>
            </tr>
        </table>
       
        <br />
        </div>
        <div style="width: 1000px;">
        <table width="100%" align="center" class="table">
            <tr>
                <td align="center">
                    <asp:Panel ID="PNLGrid" runat="server" ScrollBars="Both" Width="1000px" Height="400px">
                        <asp:GridView ID="dgvReport" runat="server" AutoGenerateColumns="false" Width="100%"
                            AllowPaging="false" GridLines="None">
                            <Columns>
                                <asp:BoundField DataField="EmpCode" HeaderText="Emp Code" />
                                <asp:BoundField DataField="EmpName" HeaderText="Emp Name" />
                                <asp:BoundField DataField="CL" HeaderText="CL" />
                                <asp:BoundField DataField="CL_DATE" HeaderText="CL Dates" ItemStyle-Wrap="true" />
                                <asp:BoundField DataField="ML" HeaderText="ML" />
                                <asp:BoundField DataField="ML_DATE" HeaderText="ML Dates" ItemStyle-Wrap="true" />
                                <asp:BoundField DataField="EL" HeaderText="EL" />
                                <asp:BoundField DataField="EL_DATE" HeaderText="EL Dates" ItemStyle-Wrap="true" />
                                <asp:BoundField DataField="OTHER" HeaderText="OTHER" />
                                <asp:BoundField DataField="OTHER_DATE" HeaderText="Other Dates" ItemStyle-Wrap="true" />
                            </Columns>
                            <HeaderStyle CssClass="HeaderStyle" />
                            <RowStyle CssClass="RowStyle" />
                            <AlternatingRowStyle CssClass="AltRowStyle" />
                        </asp:GridView>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Button ID="btnExport" runat="server" CssClass="button" Text="Export To Excel"
                        OnClick="btnExport_Click" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
