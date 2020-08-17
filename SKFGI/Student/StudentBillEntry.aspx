<%@ Page Language="C#" MasterPageFile="~/MasterAdmin.Master" AutoEventWireup="true"
    CodeBehind="StudentBillEntry.aspx.cs" Inherits="CollegeERP.Student.StudentBillEntry"
    Title="Student Single Bill Entry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../UserControl/Message.ascx" TagName="Message" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">
    
        function Validation()
        {
            if (document.getElementById('<%=ddlSemester.ClientID%>').selectedIndex == 0)
                return ShowMsg("Please Select Semester");
            else if (parseFloat(document.getElementById('<%=txtTotalAmt.ClientID%>').value) == 0)
                return ShowMsg("Zero Amount is Not Allowed");
            else if (parseFloat(document.getElementById('<%=txtBillDate.ClientID%>').value) == '')
                return ShowMsg("Please Enter Bill Date");
            else
               return confirm("Are You Sure?");
        }
        
        function ShowMsg(str)
        {
            alert(str);
            return false;
        }
        
        function TotalAmount()
        {
          var amt=0;
          var gv = document.getElementById('<%=dgvFeesHead.ClientID%>');
          var rCount = gv.rows.length;
          var rowIdx=1;
          
          for (rowIdx; rowIdx <= rCount-1; rowIdx ++)
         {
            var rowElement = gv.rows[rowIdx];
            var txtBox=rowElement.cells[1].getElementsByTagName("input")[0];
                amt = amt + ((txtBox.value == '') ? 0 : parseFloat(txtBox.value)) ;
         }
         document.getElementById('<%=txtTotalAmt.ClientID%>').value= amt.toFixed(2);   
    }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ToolkitScriptManager ID="toolScript1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="title">
        <h5>
            Student Single Bill Entry</h5>
    </div>
    <asp:UpdatePanel ID="UP1" runat="server">
        <ContentTemplate>
            <div style="width: 720px;">
                
                <table width="100%" align="center" class="table">
                    <tr>
                        <td colspan="2">
                            <uc3:Message ID="Message" runat="server" />
                            <br />
                        </td>
                    </tr>
                     <tr>
                        <td colspan="2">
                            <table width="100%" align="center" class="table">
                <tr>
                    <td width="15%" align="left" class="label">
                      Batch :
                    </td>
                    <td align="left" class="style2">
                        <asp:DropDownList ID="ddlBatch" runat="server" Width="192px" CssClass="dropdownList"
                                    AutoPostBack="True" OnSelectedIndexChanged="ddlBatch_SelectedIndexChanged" DataValueField="id" DataTextField="batch_name">
                                </asp:DropDownList>
                    </td>
                    <td width="15%" align="left" class="label">
                        Course :
                    </td>
                    <td align="left" width="15%">
                    <asp:DropDownList ID="ddlCourse" runat="server" AutoPostBack="true" CssClass="dropdownList"
                        Width="130px" DataValueField="CourseId" DataTextField="CourseName" OnSelectedIndexChanged="ddlCourse_SelectedIndexChanged">
                    </asp:DropDownList>
                    </td>
                    <td width="15%" align="left" class="label">
                        Stream :
                    </td>
                    <td align="left" width="15%">
                    <asp:DropDownList ID="ddlStream" runat="server" CssClass="dropdownList" Width="130px"
                        DataTextField="stream_name" DataValueField="StreamId" AutoPostBack="true" 
                            onselectedindexchanged="ddlStream_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                    </tr>
                    </table>
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td align="left" colspan="2">
                            <asp:ComboBox ID="ddlStudent" runat="server" CssClass="WindowsStyle" DropDownStyle="DropDown"
                                AutoCompleteMode="SuggestAppend" CaseSensitive="false" Width="560px" DataValueField="id"
                                DataTextField="StudentName" Visible="false" AutoPostBack="true">
                            </asp:ComboBox>
                            <asp:Label ID = "lblDropout" runat="server" Font-Bold="true" ForeColor="Red"></asp:Label>
                            <%--********************************--%>
                            <asp:GridView ID="gvStudent" runat="server" AutoGenerateColumns="false" Width="100%"
                                AllowPaging="false" DataKeyNames="id" >
                                <Columns>
                                    <asp:TemplateField HeaderText="Select">
                                        <ItemTemplate >
                                             <asp:CheckBox ID="ChkSelect" runat="server"  />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="id" HeaderText="Student Id" />
                                    <asp:BoundField DataField="StudentName" HeaderText="Student Name" />
                                   <%-- <asp:BoundField DataField="EmployeeType" HeaderText="Employee Type" />
                                    <asp:BoundField DataField="PayableBasic" HeaderText="Payable Basic" HeaderStyle-HorizontalAlign="Right" />
                                    <asp:BoundField DataField="Percentage" HeaderText="Percentage(%)" HeaderStyle-HorizontalAlign="Right" DataFormatString="{0:F2}" />
                                    <asp:BoundField DataField="Amount" HeaderText="Payable Amount" HeaderStyle-HorizontalAlign="Right" DataFormatString="{0:F2}" />--%>
                                  
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
                    <tr>
                        <td colspan="2">
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td align="left" colspan="2">
                            <asp:DropDownList ID="ddlSemester" runat="server" CssClass="dropdownList" Width="250px">
                                <asp:ListItem Value="0" Text="---SELECT---"></asp:ListItem>
                                <asp:ListItem Value="1" Text="SEM 1"></asp:ListItem>
                                <asp:ListItem Value="2" Text="SEM 2"></asp:ListItem>
                                <asp:ListItem Value="3" Text="SEM 3"></asp:ListItem>
                                <asp:ListItem Value="4" Text="SEM 4"></asp:ListItem>
                                <asp:ListItem Value="5" Text="SEM 5"></asp:ListItem>
                                <asp:ListItem Value="6" Text="SEM 6"></asp:ListItem>
                                <asp:ListItem Value="7" Text="SEM 7"></asp:ListItem>
                                <asp:ListItem Value="8" Text="SEM 8"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                     <tr>
                        <td colspan="2">
                            <br />
                        </td>
                    </tr>
                    <tr>
                    
                        <td colspan="2">
                        
                        <table style="width:100%">
                        <tr>
                        <td align="left" style="width:15%">
                            Bill Date :<span class="req">*</span>
                        </td>
                        <td align="left" style="width:85%">
                             <asp:TextBox ID="txtBillDate" runat="server" CssClass="textbox_required" 
                                Width="110px" ></asp:TextBox>
                                <asp:CalendarExtender ID="CalendarExtender1" runat="server" CssClass="cal_Theme1"
                                PopupPosition="BottomRight" Format="dd MMM yyyy" 
                                TargetControlID="txtBillDate" Enabled="True">
                            </asp:CalendarExtender>
                        </td>
                        </tr>
                        </table>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" colspan="2">
                            <asp:GridView ID="dgvFeesHead" runat="server" Width="100%" AutoGenerateColumns="false"
                                AllowPaging="false" GridLines="None" DataKeyNames="id">
                                <Columns>
                                    <asp:BoundField DataField="fees" HeaderText="Fees Head" />
                                    <asp:TemplateField HeaderText="Amount" ItemStyle-Width="90px">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtAmount" runat="server" CssClass="textbox_green" onkeypress="return AmountOnly('txtAmount',this);"
                                                onkeyup="TotalAmount()" Text="0.00"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <HeaderStyle CssClass="HeaderStyle" />
                                <RowStyle CssClass="RowStyle" />
                                <AlternatingRowStyle CssClass="AltRowStyle" />
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr class="RowStyle">
                        <td align="left" class="label">
                            Total
                        </td>
                        <td align="center" width="90px">
                            <asp:TextBox ID="txtTotalAmt" runat="server" CssClass="textbox_yellow" Enabled="false"
                                Text="0.00"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td align="right" colspan="2">
                            <asp:Button ID="btnGenerate" runat="server" CssClass="button" Text="Generate Bill"
                                OnClientClick="return Validation()" OnClick="btnGenerate_Click" />
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
