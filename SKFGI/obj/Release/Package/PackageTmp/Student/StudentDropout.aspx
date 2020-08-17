<%@ Page Language="C#" MasterPageFile="~/MasterAdmin.Master" AutoEventWireup="true" CodeBehind="StudentDropout.aspx.cs" Inherits="CollegeERP.Student.WebForm1" Title="Student Drop Out" EnableEventValidation="false" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../UserControl/Message.ascx" TagName="Message" TagPrefix="uc3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
  <script language="javascript" type="text/javascript">
        function Validation()
        {
            if (document.getElementById('<%=txtDropOutDate.ClientID%>').value == '' )
            {
                alert("Please Select Drop Out Date");
                return false;
            }
            else if (document.getElementById('<%=txtReason.ClientID%>').value == '')
            {
                alert('Please Enter Reason');
                return false;
            }
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  <asp:ToolkitScriptManager ID="toolScript1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="title">
        <h5>
            Student Drop Out</h5>
    </div>
    <asp:UpdatePanel ID="UP1" runat="server">
        <ContentTemplate>
       <%--  <div style="width: 740px;">--%>
            <uc3:Message ID="Message" runat="server" />
       <%-- </div>--%>
                <div style="width: 780px; clear:both">
                <table width="100%" align="center" class="table">
                    <tr>
                        <td align="left" width="20%" class="label">
                            Student Name
                        </td>
                        <td align="left" colspan="3" valign="top">
                            <asp:ComboBox ID="ddlStudent" runat="server" CssClass="WindowsStyle" AutoPostBack="true"
                                DropDownStyle="DropDown" AutoCompleteMode="SuggestAppend" CaseSensitive="false"
                                Width="340px" DataValueField="id" DataTextField="StudentName" OnSelectedIndexChanged="ddlStudent_SelectedIndexChanged">
                            </asp:ComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td align="left" width="20%" class="label">
                            Drop Out Date<span class="req">*</span></td>
                        <td align="left" colspan="3" valign="top">
                         <asp:TextBox ID="txtDropOutDate" runat="server" CssClass="textbox_required" Width="110px"></asp:TextBox>
                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" CssClass="cal_Theme1"
                                PopupPosition="BottomRight" Format="dd MMM yyyy" TargetControlID="txtDropOutDate"
                                OnClientDateSelectionChanged="" Enabled="True">
                            </asp:CalendarExtender>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td align="left" width="20%" class="label">
                            Reason<span class="req">*</span>
                        </td>
                        <td align="left" colspan="3">
                            <asp:TextBox ID="txtReason" runat="server" CssClass="textbox_required" Width="510px"
                                Height="35px" TextMode="MultiLine" Style="resize: none"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" width="20%" class="label">
                            &nbsp;</td>
                        <td align="left" width="30%">
                            &nbsp;</td>
                        <td align="left" width="20%" class="label">
                        </td>
                        <td align="left" width="30%">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td align="center" colspan="4">
                            <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="button" OnClientClick="javascript:return Validation()"
                                OnClick="btnSave_Click" />&nbsp;
                            <asp:Button ID="btnCancel" runat="server" CssClass="button" Text="Reset" OnClick="btnCancel_Click" />&nbsp;
                            </td>
                    </tr>
                </table>
            </div>
              </ContentTemplate>
    </asp:UpdatePanel>
            <br />
            <h6 align="left" style="color: #00356A;">
                Student Drop Out Report</h6>
           
               <div align="center"  style="width:780px">
                <table style="width:780px">
                    <tr>
                <td align="left" width="20%" class="label">
                    <%--From Date--%>
                    Batch
                    <%--<span class="req">*</span>--%>
                </td>
                <td align="left" width="30%">
                 <asp:DropDownList ID="ddlBatch" runat="server" Width="192px" CssClass="dropdownList"
                                    AutoPostBack="false" >
                                </asp:DropDownList>
                    <%--<asp:TextBox ID="txtFromDate" runat="server" CssClass="textbox_required" Width="110px"></asp:TextBox>
                    <asp:CalendarExtender ID="CalendarExtender2" runat="server" CssClass="cal_Theme1"
                        PopupPosition="BottomRight" Format="dd MMM yyyy" TargetControlID="txtFromDate"
                        OnClientDateSelectionChanged="" Enabled="True">
                    </asp:CalendarExtender>--%>
                    
                </td>
                <td align="left" width="20%" class="label">
                   <%-- To Date<span class="req">*</span>--%>
                </td>
                <td align="left" width="30%">
                   <%-- <asp:TextBox ID="txtTodate" runat="server" CssClass="textbox_required" Width="110px"></asp:TextBox>
                    <asp:CalendarExtender ID="CalendarExtender3" runat="server" CssClass="cal_Theme1"
                        PopupPosition="BottomRight" Format="dd MMM yyyy" TargetControlID="txtTodate" OnClientDateSelectionChanged=""
                        Enabled="True">
                    </asp:CalendarExtender>--%>
                    <asp:Button ID="btnShow" runat="server" CssClass="button" Text="Get Drop Out Report" OnClientClick=""
                         OnClick="btnShow_Click" />
                </td>
            </tr>
            <tr>
                <td colspan="3">
                </td>
                <td align="left">
                    
                </td>
            </tr>
              <tr>
                <td colspan="4" align="center">
                    <asp:GridView ID="dgvReport" runat="server" AutoGenerateColumns="false" Width="100%"
                        AllowPaging="false" DataKeyNames="student_code" GridLines="None" OnRowCommand="dgvReport_RowCommand">
                        <Columns>
                          
                            <asp:BoundField DataField="student_code" HeaderText="Student Code" />
                            <asp:BoundField DataField="name" HeaderText="Student Name" />
                            <asp:BoundField DataField="sem" HeaderText="Cerrent Sem" />
                            <asp:BoundField DataField="DODate" HeaderText="Drop Out Date" />
                            <asp:BoundField DataField="Reason" HeaderText="Reason" />
                            <asp:TemplateField HeaderText="Active">
                                <ItemTemplate>
                                    <asp:Button ID="btnDelete" runat="server" CssClass="button" Text="Active" CommandName="DeleteDropout" CommandArgument='<%#Eval("Id") %>'
                                        OnClientClick="return confirm('Are you sure?')" />
                                </ItemTemplate>
                            </asp:TemplateField>
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
                        <EmptyDataRowStyle CssClass="EditRowStyle" />
                        <AlternatingRowStyle CssClass="AltRowStyle" />
                        <PagerStyle CssClass="PagerStyle" />
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td colspan="4" align="right">
                 <asp:Button ID="btnPrint" runat="server" CssClass="button" Text="Print" OnClick="btnPrint_Click" />&nbsp;
                    <asp:Button ID="btnDownload" runat="server" CssClass="button" Text="Download Drop Out Report"
                        OnClick="btnDownload_Click" />
                </td>
            </tr>
    </table>
    </div>
            
    <br />
 

</asp:Content>
