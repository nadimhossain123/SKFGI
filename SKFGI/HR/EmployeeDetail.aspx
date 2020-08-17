<%@ Page Language="C#" MasterPageFile="~/MasterAdmin.Master" AutoEventWireup="true"
    CodeBehind="EmployeeDetail.aspx.cs" Inherits="CollegeERP.HR.EmployeeDetail" Title="Employee Details Report" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">
        function Validation()
        {
            
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ToolkitScriptManager ID="ToolScript1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="title">
        <h5>
            Employee Detail Report</h5>
    </div>
    <div style="width: 940px;">
    
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
                Designation
                </td>
                <td align="left"  width="25%">
                <asp:DropDownList ID="ddlDesignation" runat="server" CssClass="dropdownList" 
                                Width="120px" DataTextField="DesignationName" DataValueField="DesignationId" 
                                onselectedindexchanged="ddlDesignation_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                </td>
                
            </tr>
        </table>
         </ContentTemplate>
        </asp:UpdatePanel>
        
       
        <br />
       <table width="100%">
        <tr>
            <td align="right">
                    <asp:Button ID="btnSearch" runat="server" CssClass="button" Text="Search" OnClientClick="return Validation()"
                    OnClick="btnSearch_Click" />
                </td>
        </tr>
       </table>
        </div>
        <div style="width: 1000px;">
        <table width="100%" align="center" class="table">
            <tr>
                <td align="center">
                    <asp:Panel ID="PNLGrid" runat="server" ScrollBars="Both" Width="1000px" Height="400px">
                      <asp:GridView ID="dgvReport" runat="server" AutoGenerateColumns="true" Width="100%"
                            AllowPaging="false" GridLines="None" 
                            onrowdatabound="dgvReport_RowDataBound">
                           
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
