<%@ Page Language="C#" MasterPageFile="~/MasterAdmin.Master" AutoEventWireup="true" CodeBehind="LeaveManagerChange.aspx.cs" 
Inherits="CollegeERP.HR.LeaveManagerChange" Title="Leave Manager Change" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../UserControl/Message.ascx" TagName="Message" TagPrefix="uc3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script type="text/javascript">
    function openpopup(poplocation, querystring, popheight, popwidth,poptop, popleft)
     {                
        var popposition='left = 200, top=20, width=950,align=center, height=640,menubar=no, scrollbars=yes, resizable=no';                
               
        var NewWindow = window.open(poplocation,'',popposition);                
        if (NewWindow.focus!=null){                        
        NewWindow.focus();                
        }        
     }
     
    function Validation()
        {
             if (document.getElementById('<%=ddlLeaveManager.ClientID%>').selectedIndex == 0)
                return ShowMsg('Please Select Leave Manager');
            else
                return true;
        }
         function ShowMsg(msg)
        {
            alert(msg);
            return false;
        }
     function CheckAll(checkbox) {

            var gv = document.getElementById('<%=dgvLeave.ClientID%>');
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
    <asp:ToolkitScriptManager ID="toolScript1" runat="server"></asp:ToolkitScriptManager>

    <div class="title">
		<h5>Leave Manager Change</h5>
    </div>
<asp:UpdatePanel ID="UP1" runat="server">
    <ContentTemplate>     
    <br />
    <uc3:Message ID="Message" runat="server" />
    <br />  
       
            <table width="95%" align="center" class="table">
                   <tr>
                        <td align="center" width="7%" class="label"><%--First Name--%></td>
                        <td align="left" width="12%">
                            <asp:TextBox ID="txtFName" runat="server" CssClass="textbox" Width="110px" Visible="false"></asp:TextBox>
                        </td>
                        <td align="center" width="17%" class="label">Recent Leave Manager</td>
                        <td align="left" width="12%">
                            <asp:DropDownList ID="ddlEmployee" runat="server" CssClass="dropdownList" 
                                Width="120px" DataTextField="FullName" DataValueField="EmployeeId" 
                                onselectedindexchanged="ddlEmployee_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                        </td>
                        <td align="center" width="10%" class="label">New Leave Manager</td>
                        <td align="left" width="12%">
                            <asp:DropDownList ID="ddlLeaveManager" runat="server" CssClass="dropdownList" Width="120px" DataValueField="EmployeeId" DataTextField="FullName"></asp:DropDownList>
                        </td>
                       
                       
                        <td align="left">
                            <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="button" 
                                OnClientClick="return Validation()" onclick="btnUpdate_Click" 
                                 />
                        </td>
                    </tr> 
                  </table>
                  <br />
                  <table width="95%" align="center">
                  <tr>
                        <td align="left">
                            <asp:CheckBox ID="ChkSelect" runat="server" Text="Select All" onclick="javascript:CheckAll(this);" />
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <asp:GridView ID="dgvLeave" runat="server" AutoGenerateColumns="false" 
                                 Width="100%" AllowPaging="true" PageSize="50"
                                 DataKeyNames="EmployeeId" onpageindexchanging="dgvLeave_PageIndexChanging" 
                                 onrowdatabound="dgvLeave_RowDataBound">
                            <Columns>
                                 <asp:TemplateField HeaderText="Select">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="ChkSelect" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                <asp:BoundField DataField="EmpCode" HeaderText="Employee Code" />
                                <asp:BoundField DataField="EmpName" HeaderText="Employee Name" />
                                <asp:BoundField DataField="LeaveManager" HeaderText="Leave Type" />
                              
                                <%--<asp:TemplateField ShowHeader="false">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit_icon.gif" />
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                        
                        </Columns>
                        
                    <HeaderStyle CssClass="HeaderStyle"  />
	                <RowStyle CssClass="RowStyle" />
	                <EmptyDataRowStyle CssClass="EditRowStyle" />
	                <AlternatingRowStyle CssClass="AltRowStyle" />
	                <PagerStyle CssClass="PagerStyle" />
                    </asp:GridView>
                        </td>
                    </tr>
                  </table>
      
    </ContentTemplate>
</asp:UpdatePanel>       
</asp:Content>
