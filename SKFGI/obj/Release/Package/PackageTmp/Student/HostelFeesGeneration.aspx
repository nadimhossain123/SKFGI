<%@ Page Language="C#" MasterPageFile="~/MasterAdmin.Master" AutoEventWireup="true"
    CodeBehind="HostelFeesGeneration.aspx.cs" Inherits="CollegeERP.Student.HostelFeesGeneration"
    Title="Hostel Fees Generation" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../UserControl/Message.ascx" TagName="Message" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">
        function SearchValidation()
        {
            if (document.getElementById('<%=ddlBatch.ClientID%>').selectedIndex == 0)
                return ShowMsg("Select Batch");
            else if (document.getElementById('<%=ddlYear.ClientID%>').selectedIndex == 0)
                return ShowMsg("Select Year");
            else if (document.getElementById('<%=ddlQuarter.ClientID%>').selectedIndex == 0)
                 return ShowMsg("Select Quarter"); 
            else if (document.getElementById('<%=txtBillDate.ClientID%>').value == '' )
                 return ShowMsg("Please Select Bill Date");             
                                
            else 
                 return true;
            //ValidationGV();
        }
        
        
        function ShowMsg(str)
        {
            alert(str);
            return false;
        }
        function ValidationGV()
        {
        var flag=0;
        var gv=document.getElementById('<%=dgvFeesHead.ClientID%>');
        var arr=gv.getElementsByTagName("input");
        for (var i=0; i<arr.length; i++)
         {
          if (arr[i].type=='text' && arr[i].value=='')
          {
               return false; 
          }
         }
        }
        function Validation()
        {
            var gv = document.getElementById('<%=dgvStudent.ClientID%>');
            var rowCount = gv.rows.length - 1;
            
            if (rowCount == 0) 
            {
                alert("No Student to Update");
                return false;
            }
            else if (Checkbox_Validation() == false) 
            {
                alert("Please Select Atleast One Student");
                return false;
            }
              else if (ValidationGV() == false) 
            {
                alert("Please Enter Amount");
                return false;
            }   
            else 
               return confirm('Do you want to proceed?');
        }
        
        function Checkbox_Validation() {
            var flag = 0;
            var gv = document.getElementById('<%=dgvStudent.ClientID%>');
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
        
        function CheckAll(checkbox) {

            var gv = document.getElementById('<%=dgvStudent.ClientID%>');
            var arr = gv.getElementsByTagName("input");
            for (var i = 0; i < arr.length; i++) {
                if (arr[i].type == 'checkbox') {
                    if (checkbox.checked == true) {
                        arr[i].checked = true;
                        arr[i].parentNode.parentNode.className='SelectedRowStyle';
                    }
                    else {
                        arr[i].checked = false;
                        arr[i].parentNode.parentNode.className='RowStyle';
                    }
                }
            }

        }
        
        function ChangeCSS(Obj)
        {
            var row = Obj.parentNode.parentNode;
            if(Obj.checked)
              row.className='SelectedRowStyle';
            else
              row.className='RowStyle';     
    
        }
        function moveEnter(rowIndex)
        {
        if ((event.which && event.which == 13) || (event.keyCode && event.keyCode == 13))
        {            
            var gv = document.getElementById('<%=dgvFeesHead.ClientID%>');
            var rCount = gv.rows.length;
            
            if (rowIndex <= rCount)
            {
                var rowElement = gv.rows[parseInt(rowIndex + 1)];
                var txtBox=rowElement.cells[1].getElementsByTagName("input")[0].focus();                
            }
          event.preventDefault();         
        }       
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   <%-- <asp:ScriptManager ID="Sc1" runat="server">
    </asp:ScriptManager>--%>
    <asp:ToolkitScriptManager ID="toolScript1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="title">
        <h5>
            Hostel Fees Generation</h5>
    </div>
    <asp:UpdateProgress ID="UProgress1" runat="server" AssociatedUpdatePanelID="UP1">
        <ProgressTemplate>
            <div class="overlay">
                <img style="top: 50%; position: relative;" src="../Images/ajax-loader.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="UP1" runat="server">
        <ContentTemplate>
            <div style="width: 780px">
                <uc3:Message ID="Message" runat="server" />
            </div>
            <div style="width: 780px">
                <table width="100%" align="center" class="table">
                    <tr>
                        <td align="left" class="label" width="13%">
                            Batch
                        </td>
                        <td align="left" class="label" width="13%">
                            Year
                        </td>
                        <td align="left" class="label" width="13%">
                            Quarter
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" width="13%">
                            <asp:DropDownList ID="ddlBatch" runat="server" CssClass="dropdownList" Width="140px"
                                DataValueField="id" DataTextField="batch_name" AutoPostBack="true" 
                                onselectedindexchanged="ddlBatch_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td align="left" width="13%">
                            <asp:DropDownList ID="ddlYear" runat="server" CssClass="dropdownList" Width="140px">
                            </asp:DropDownList>
                        </td>
                        <td align="left" width="13%">
                            <asp:DropDownList ID="ddlQuarter" runat="server" CssClass="dropdownList" Width="140px">
                                <asp:ListItem Value="0" Text="---SELECT---"></asp:ListItem>
                                <asp:ListItem Value="1" Text="1ST"></asp:ListItem>
                                <asp:ListItem Value="2" Text="2ND"></asp:ListItem>
                                <asp:ListItem Value="3" Text="3RD"></asp:ListItem>
                                <asp:ListItem Value="4" Text="4TH"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td align="center">
                            <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="button" 
                                OnClientClick="return SearchValidation()" onclick="btnSearch_Click" />&nbsp;
                            <asp:Button ID="btnCancel" runat="server" CssClass="button" Text="Reset" 
                                onclick="btnCancel_Click" />
                            &nbsp;
                            <asp:Button ID="btnGenerate" runat="server" CssClass="button" Text="Generate" 
                                OnClientClick="return Validation()" onclick="btnGenerate_Click" />
                        </td>
                    </tr>
                </table>
                <table width="100%" align="center" class="table">
                    <tr>
                        <td align="center" class="label" style="width:60%" >
                            BillDate<span class="req">*</span>
                        </td>
                        <td colspan="2" align="left" >
                            <asp:TextBox ID="txtBillDate" runat="server" CssClass="textbox_required" Width="110px"></asp:TextBox>
                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" CssClass="cal_Theme1"
                                PopupPosition="BottomRight" Format="dd MMM yyyy" TargetControlID="txtBillDate"
                                OnClientDateSelectionChanged="" Enabled="True">
                            </asp:CalendarExtender>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" align="center">
                        <asp:GridView ID="dgvFeesHead" runat="server" Width="50%" AutoGenerateColumns="false"
                                GridLines="None" AllowPaging="false" DataKeyNames="id" 
                                onrowdatabound="dgvFeesHead_RowDataBound">
                                <Columns>
                                    <asp:BoundField DataField="fees" HeaderText="Fees Head" />
                                    <asp:TemplateField HeaderText="Amount" ItemStyle-Width="90px">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtAmount" runat="server" CssClass="textbox_green" Text='<%#Bind("amount") %>'
                                                onkeypress="return AmountOnly('txtAmount',this);" style="text-align:right; padding-right:6px;"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <HeaderStyle CssClass="HeaderStyle" />
                                <RowStyle CssClass="RowStyle" />
                                <AlternatingRowStyle CssClass="AltRowStyle" />
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" colspan="3">
                            <asp:CheckBox ID="ChkSelect" runat="server" Text="All" onclick="javascript:CheckAll(this);" />
                        </td>
                    </tr>
                    <tr>
                        <td align="center" colspan="3">
                            <asp:GridView ID="dgvStudent" runat="server" Width="100%" AutoGenerateColumns="false"
                                GridLines="None" AllowPaging="false" DataKeyNames="id">
                                <Columns>
                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="ChkSelect" runat="server" onclick="ChangeCSS(this)" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:ImageField DataImageUrlField="Photo" HeaderText="Photo" ControlStyle-Height="50px"
                                        ControlStyle-Width="50px">
                                    </asp:ImageField>
                                    <asp:BoundField DataField="student_code" HeaderText="Roll No" />
                                    <asp:BoundField DataField="name" HeaderText="Name" />
                                    <asp:BoundField DataField="stream_name" HeaderText="Stream" />
                                </Columns>
                                <EmptyDataTemplate>
                                    <table style="height: 10px; width: 100%;">
                                        <tr align="left" class="HeaderStyle">
                                            <th scope="col">
                                                No Student Found
                                            </th>
                                        </tr>
                                        <tr class="RowStyle">
                                            <td>
                                                Sorry! No Student Found.
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
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
