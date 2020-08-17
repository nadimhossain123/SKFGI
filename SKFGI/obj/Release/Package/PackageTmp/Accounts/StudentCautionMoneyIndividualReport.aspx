<%@ Page Language="C#" MasterPageFile="~/MasterAdmin.Master" AutoEventWireup="true"
    CodeBehind="StudentCautionMoneyIndividualReport.aspx.cs" Inherits="CollegeERP.Accounts.StudentCautionMoneyIndividualReport"
    Title="Caution Money Individual Report" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../UserControl/Message.ascx" TagName="Message" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ToolkitScriptManager ID="ToolScript1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="title">
        <h5>
            Caution Money Individual Report</h5>
    </div>
    <uc3:Message ID="Message" runat="server" />
    <br />
    <table width="85%" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td width="8%" align="left" class="label">
                Batch:
            </td>
            <td align="left" width="17%">
                <asp:DropDownList ID="ddlBatch" runat="server" Width="150px" CssClass="dropdownList"
                    AutoPostBack="True" OnSelectedIndexChanged="ddlBatch_SelectedIndexChanged" DataValueField="id"
                    DataTextField="batch_name">
                </asp:DropDownList>
            </td>
            <td width="8%" align="left" class="label">
                Course:
            </td>
            <td align="left" width="17%">
                <asp:DropDownList ID="ddlCourse" runat="server" AutoPostBack="true" CssClass="dropdownList"
                    Width="150px" DataValueField="CourseId" DataTextField="CourseName" OnSelectedIndexChanged="ddlCourse_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            <td width="8%" align="left" class="label">
                Stream:
            </td>
            <td align="left">
                <asp:DropDownList ID="ddlStream" runat="server" CssClass="dropdownList" Width="150px"
                    DataTextField="stream_name" DataValueField="StreamId" AutoPostBack="true" OnSelectedIndexChanged="ddlStream_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
        </tr>
    </table>
    <table width="85%" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td width="8%" align="left" class="label">
                Student:
            </td>
            <td width="33%" align="left" style="padding-bottom: 5px;">
                <asp:ComboBox ID="ddlStudent" runat="server" CssClass="WindowsStyle" AutoPostBack="false"
                    DropDownStyle="DropDown" AutoCompleteMode="SuggestAppend" CaseSensitive="false"
                    Width="300px" DataValueField="id" DataTextField="StudentName">
                </asp:ComboBox>
            </td>
            <td width="5%" align="left" class="label">
                Head:
            </td>
            <td width="17%" align="left">
                <asp:DropDownList ID="ddlFeesHead" runat="server" CssClass="dropdownList" Width="150px"
                    DataValueField="id" DataTextField="fees">
                </asp:DropDownList>
            </td>
            <td width="4%" align="left" class="label">
                From:
            </td>
            <td width="12%" align="left">
                <asp:TextBox ID="txtFromDate" runat="server" Width="100px" CssClass="textbox" MaxLength="12"
                    onkeydown="return false;"></asp:TextBox>
                <asp:CalendarExtender ID="CalendarExtender1" runat="server" CssClass="cal_Theme1"
                    PopupPosition="BottomRight" Format="dd MMM yyyy" TargetControlID="txtFromDate"
                    OnClientDateSelectionChanged="" Enabled="True">
                </asp:CalendarExtender>
            </td>
            <td width="4%" align="left" class="label">
                To:
            </td>
            <td width="12%" align="left">
                <asp:TextBox ID="txtToDate" runat="server" Width="100px" CssClass="textbox" MaxLength="12"
                    onkeydown="return false;"></asp:TextBox>
                <asp:CalendarExtender ID="CalendarExtender2" runat="server" CssClass="cal_Theme1"
                    PopupPosition="BottomRight" Format="dd MMM yyyy" TargetControlID="txtToDate"
                    OnClientDateSelectionChanged="" Enabled="True">
                </asp:CalendarExtender>
            </td>
            <td align="left">
                <asp:Button ID="btnSearch" runat="server" CssClass="button" Text="Search" OnClick="btnSearch_Click" />
            </td>
        </tr>
    </table>
    <br />
    <br />
    <br />
    <table width="85%" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td align="left" style="font-family: Calibri; font-size: 14px;">
                <asp:Literal ID="ltrStudentInfo" runat="server" Mode="PassThrough"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td align="center">
                <asp:GridView ID="dgvReport" runat="server" Width="100%" AutoGenerateColumns="false"
                    ShowFooter="true" GridLines="None" AllowPaging="false" CellPadding="0" CellSpacing="0">
                    <Columns>
                        <asp:BoundField DataField="DocumentNo" HeaderText="Document No" FooterText="<b>*** Closing Balance ***</b>" />
                        <asp:BoundField DataField="DocumentDate" HeaderText="Date" />
                        <asp:BoundField DataField="DocumentType" HeaderText="Type" />
                        <asp:BoundField DataField="FeesType" HeaderText="Description" />
                        <asp:TemplateField HeaderText="Rec. Amount" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right"
                            ItemStyle-Width="100px">
                            <ItemTemplate>
                                <%# Eval("ReceivedAmount", "{0:n}")%>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Literal ID="ltrTotalReceivedAmount" runat="server" Mode="PassThrough"></asp:Literal>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Pay/Adj Amount" ItemStyle-HorizontalAlign="Right"
                            FooterStyle-HorizontalAlign="Right" ItemStyle-Width="100px">
                            <ItemTemplate>
                                <%# Eval("RefundAmount", "{0:n}")%>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Literal ID="ltrTotalRefundAmount" runat="server" Mode="PassThrough"></asp:Literal>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Balance Amount" ItemStyle-HorizontalAlign="Right"
                            FooterStyle-HorizontalAlign="Right" ItemStyle-Width="100px">
                            <ItemTemplate>
                                <%# Eval("BalanceAmount", "{0:n}")%>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Literal ID="ltrTotalBalanceAmount" runat="server" Mode="PassThrough"></asp:Literal>
                            </FooterTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataTemplate>
                        <table style="height: 10px; width: 100%;" cellpadding="0" cellspacing="0">
                            <tr align="left" class="HeaderStyle">
                                <th scope="col">
                                    No Record Found
                                </th>
                            </tr>
                            <tr class="RowStyle">
                                <td>
                                    No Record Found
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
                <asp:Button ID="btnPrint" runat="server" CssClass="button" Text="Print" OnClick="btnPrint_Click" />&nbsp;
                <asp:Button ID="btnDownload" runat="server" CssClass="button" Text="Download" OnClick="btnDownload_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
