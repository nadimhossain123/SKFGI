<%@ Page Language="C#" MasterPageFile="~/MasterAdmin.Master" AutoEventWireup="true"
    CodeBehind="StudentOpeningBalance.aspx.cs" Inherits="CollegeERP.Accounts.StudentOpeningBalance"
    Title="Student Opening Balance" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../UserControl/Message.ascx" TagName="Message" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">
    
        function Validation()
        {
            if (document.getElementById('<%=ddlSemester.ClientID%>').selectedIndex == 0)
                return ShowMsg("Please Select Semester");
                
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
            var dropDownList=rowElement.cells[2].getElementsByTagName("select")[0];
            var DrCr=dropDownList.options[dropDownList.selectedIndex].value;
            
            if (DrCr == 'DR')
                amt = amt + ((txtBox.value == '') ? 0 : parseFloat(txtBox.value)) ;
            else if (DrCr == 'CR')
                amt = amt - ((txtBox.value == '') ? 0 : parseFloat(txtBox.value)) ;    
         }
         document.getElementById('<%=txtTotalAmt.ClientID%>').value= amt.toFixed(2);  
       } 
       
        function moveEnter(rowIndex)
        {
            
            if ((event.which && event.which == 13) || (event.keyCode && event.keyCode == 13))
            {            
                var gv = document.getElementById('<%=dgvFeesHead.ClientID%>');
                var rCount = gv.rows.length;
            
                if (rowIndex < rCount)
                {
                    var rowElement = gv.rows[parseInt(rowIndex + 1)];
                    var txtBox=rowElement.cells[1].getElementsByTagName("input")[0].focus();                
                }
                event.preventDefault();         
            }
            TotalAmount();
        }
        
       
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ToolkitScriptManager ID="toolScript1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="title">
        <h5>
            Student Opening Balance</h5>
    </div>
    <asp:UpdatePanel ID="UP1" runat="server">
        <ContentTemplate>
            <div style="width: 560px;">
                
                <table width="100%" align="center" class="table">
                    <tr>
                        <td colspan="3">
                            <uc3:Message ID="Message" runat="server" />
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td align="left" colspan="3">
                            <asp:DropDownList ID="ddlBatch" runat="server" Width="300px" CssClass="dropdownList"
                                AutoPostBack="True" OnSelectedIndexChanged="ddlBatch_SelectedIndexChanged" DataValueField="id"
                                DataTextField="batch_name">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" colspan="3">
                            <asp:DropDownList ID="ddlCourse" runat="server" AutoPostBack="true" CssClass="dropdownList"
                                Width="300px" DataValueField="CourseId" DataTextField="CourseName" OnSelectedIndexChanged="ddlCourse_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" colspan="3">
                            <asp:DropDownList ID="ddlStream" runat="server" CssClass="dropdownList" Width="300px"
                                DataTextField="stream_name" DataValueField="StreamId" AutoPostBack="true" OnSelectedIndexChanged="ddlStream_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" colspan="3">
                            <asp:ComboBox ID="ddlStudent" runat="server" CssClass="WindowsStyle" DropDownStyle="DropDown"
                                AutoCompleteMode="SuggestAppend" CaseSensitive="false" Width="500px" DataValueField="id"
                                DataTextField="StudentName">
                            </asp:ComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td align="left" colspan="2">
                            <asp:DropDownList ID="ddlSemester" runat="server" CssClass="dropdownList" Width="200px">
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
                        <td>
                            <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="button" 
                                onclick="btnSearch_Click"/>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" colspan="3">
                            <asp:GridView ID="dgvFeesHead" runat="server" Width="100%" AutoGenerateColumns="false"
                                AllowPaging="false" GridLines="None" DataKeyNames="id" OnRowDataBound="dgvFeesHead_RowDataBound">
                                <Columns>
                                    <asp:BoundField DataField="fees" HeaderText="Fees Head" />
                                    <asp:TemplateField HeaderText="Amount" ItemStyle-Width="90px">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtAmount" runat="server" CssClass="textbox_green" onkeypress="return AmountOnly('txtAmount',this);"
                                                tabstop="true" Text="0.00" onblur="TotalAmount();"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle Width="90px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="DR/CR" ItemStyle-Width="50px">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="ddlDrCr" runat="server" CssClass="dropdownList" Width="50px">
                                                <asp:ListItem Value="DR" Text="DR"></asp:ListItem>
                                                <asp:ListItem Value="CR" Text="CR"></asp:ListItem>
                                            </asp:DropDownList>
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
                        <td width="50px"></td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td align="right" colspan="3">
                            <asp:Button ID="btnCancel" runat="server" CssClass="button" Text="Reset" OnClick="btnCancel_Click" />
                            &nbsp;&nbsp;
                            <asp:Button ID="btnSave" runat="server" CssClass="button" Text="Save" OnClientClick="return Validation()"
                                OnClick="btnSave_Click" />
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
