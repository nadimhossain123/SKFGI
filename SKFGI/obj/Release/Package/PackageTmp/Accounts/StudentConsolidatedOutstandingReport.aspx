<%@ Page Title="Student Consolidated Ledger Report" Language="C#" MasterPageFile="~/MasterAdmin.Master"
    AutoEventWireup="true" CodeBehind="StudentConsolidatedOutstandingReport.aspx.cs"
    Inherits="CollegeERP.Accounts.StudentConsolidatedOutstandingReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<asp:ToolkitScriptManager ID="toolScript1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="title">
        <h5>
            Student Consolidated Ledger Report</h5>
    </div>
    <br />
    <div style="width: 720px;">
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                
                <td width="20%" class="label">
                    From Date:
                </td>
                <td width="30%">
                    <asp:TextBox ID="txtFromDate" runat="server" CssClass="textbox"
                        Width="140px">
                    </asp:TextBox>
                    <asp:CalendarExtender ID="CalendarExtender1" runat="server" CssClass="cal_Theme1"
                        PopupPosition="BottomRight" Format="dd MMM yyyy" TargetControlID="txtFromDate"
                        OnClientDateSelectionChanged="" Enabled="True">
                    </asp:CalendarExtender>
                </td>
                <td width="20%" class="label">
                    To Date:
                </td>
                <td width="30%">
                    <asp:TextBox ID="txtToDate" runat="server" CssClass="textbox"
                        Width="140px">
                    </asp:TextBox>
                     <asp:CalendarExtender ID="CalendarExtender2" runat="server" CssClass="cal_Theme1"
                        PopupPosition="BottomRight" Format="dd MMM yyyy" TargetControlID="txtToDate"
                        OnClientDateSelectionChanged="" Enabled="True">
                    </asp:CalendarExtender>
                </td>
            </tr>
            <tr>
                <td width="20%" class="label">
                    Course:
                </td>
                <td width="30%">
                    <asp:DropDownList ID="ddlCourse" runat="server" AutoPostBack="true" CssClass="dropdownList"
                        Width="140px" DataValueField="CourseId" DataTextField="CourseName" OnSelectedIndexChanged="ddlCourse_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <td width="20%" class="label">
                    Batch:
                </td>
                <td width="30%">
                    <asp:DropDownList ID="ddlBatch" runat="server" CssClass="dropdownList" Width="140px"
                        DataValueField="id" DataTextField="batch_name">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="label">
                    Stream:
                </td>
                <td>
                    <asp:DropDownList ID="ddlStream" runat="server" CssClass="dropdownList" Width="140px"
                        DataValueField="StreamId" DataTextField="stream_name">
                    </asp:DropDownList>
                </td>
                <td class="label">
                    Semester:
                </td>
                <td>
                    <asp:DropDownList ID="ddlSemNo" runat="server" CssClass="dropdownList" Width="140px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="label">
                    Fees Head:
                </td>
                <td>
                    <asp:DropDownList ID="ddlFeesHead" runat="server" CssClass="dropdownList" Width="140px"
                        DataValueField="id" DataTextField="fees">
                    </asp:DropDownList>
                </td>
                <td class="label">
                    Show Zero Due Bal.
                </td>
                <td>
                    <asp:CheckBox ID="chkShowZeroDue" runat="server" Checked="true"/>
                </td>
            </tr>
            <tr>
                <td >                    
                </td>
                <td>                    
                </td>
                <td>
                </td>
                <td>
                    <asp:Button ID="btnSearch" runat="server" CssClass="button" Text="Show Report" OnClick="btnSearch_Click" />
                </td>
            </tr>
        </table>
        <br />
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td>
                    <asp:GridView ID="dgvBill" runat="server" Width="100%" ShowFooter="true" GridLines="None"
                        AutoGenerateColumns="false" AllowPaging="false" CellPadding="0" CellSpacing="0" OnRowDataBound="dgvBill_RowDataBound">
                        <Columns>
                            <asp:BoundField DataField="Sl" HeaderText="Sl" />
                            <asp:BoundField DataField="name" HeaderText="Student Name" />
                            <asp:BoundField DataField="student_code" HeaderText="Code" FooterText="<b>Total</b>" />
                            <asp:TemplateField HeaderText="Bill Amount">
                                <ItemTemplate>
                                    <asp:Label ID="lblBillAmt" runat="server" Text='<%#Bind("BillAmount","{0:n}") %>'></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Literal ID="ltrTotBillAmt" runat="server" Mode="PassThrough"></asp:Literal>
                                </FooterTemplate>
                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                <FooterStyle HorizontalAlign="Right"></FooterStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Paid">
                                <ItemTemplate>
                                    <asp:Label ID="lblPaidAmt" runat="server" Text='<%#Bind("PaidAmount","{0:n}") %>'></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Literal ID="ltrTotPaidAmt" runat="server" Mode="PassThrough"></asp:Literal>
                                </FooterTemplate>
                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                <FooterStyle HorizontalAlign="Right"></FooterStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Due">
                                <ItemTemplate>
                                    <asp:Label ID="lblDueAmt" runat="server" Text='<%#Bind("DueAmount","{0:n}") %>'></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Literal ID="ltrTotDueAmt" runat="server" Mode="PassThrough"></asp:Literal>
                                </FooterTemplate>
                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                <FooterStyle HorizontalAlign="Right"></FooterStyle>
                            </asp:TemplateField>
                            <asp:BoundField DataField="studentmobile" HeaderText="Student Phone No" />
                            <asp:BoundField DataField="parentmobile" HeaderText="Parent Phone No" />
                        </Columns>
                        <EmptyDataTemplate>
                            <table width="100%" cellpadding="0" cellspacing="0">
                                <tr class="HeaderStyle">
                                    <th>
                                        No Records Found
                                    </th>
                                </tr>
                                <tr class="RowStyle">
                                    <td>
                                        No Records Found
                                    </td>
                            </table>
                        </EmptyDataTemplate>
                        <HeaderStyle CssClass="HeaderStyle" />
                        <RowStyle CssClass="RowStyle" />
                        <AlternatingRowStyle CssClass="AltRowStyle" />
                        <FooterStyle CssClass="RowStyle" />
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td>
                    <br />
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Button ID="btnDownload" runat="server" CssClass="button" Text="Download" OnClick="btnDownload_Click" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
