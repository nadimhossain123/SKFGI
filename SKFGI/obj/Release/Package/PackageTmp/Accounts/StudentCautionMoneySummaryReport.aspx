<%@ Page Language="C#" MasterPageFile="~/MasterAdmin.Master" AutoEventWireup="true"
    CodeBehind="StudentCautionMoneySummaryReport.aspx.cs" Inherits="CollegeERP.Accounts.StudentCautionMoneySummaryReport"
    Title="Caution Money Summary Report" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="title">
        <h5>
            Caution Money Summary Report</h5>
    </div>
    <br />
    <br />
    <div style="width: 1000px;">
        <table width="100%" align="center" class="table">
            <tr>
                <td align="left" width="8%" class="label">
                    Course:
                </td>
                <td align="left" width="16%">
                    <asp:DropDownList ID="ddlCourse" runat="server" AutoPostBack="true" CssClass="dropdownList"
                        Width="140px" DataValueField="CourseId" DataTextField="CourseName" OnSelectedIndexChanged="ddlCourse_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <td align="left" width="8%" class="label">
                    Stream:
                </td>
                <td align="left" width="16%">
                    <asp:DropDownList ID="ddlStream" runat="server" CssClass="dropdownList" Width="140px"
                        DataValueField="StreamId" DataTextField="stream_name">
                    </asp:DropDownList>
                </td>
                <td align="left" width="8%" class="label">
                    Batch:
                </td>
                <td align="left" width="16%">
                    <asp:DropDownList ID="ddlBatch" runat="server" CssClass="dropdownList" Width="140px"
                        DataValueField="id" DataTextField="batch_name">
                    </asp:DropDownList>
                </td>
                <td align="left">
                    <asp:Button ID="btnSearch" runat="server" CssClass="button" Text="Search" OnClick="btnSearch_Click" />
                </td>
            </tr>
        </table>
        <br />
        <table width="100%" align="center" class="table">
            <tr>
                <td align="center">
                    <asp:GridView ID="dgvReport" runat="server" Width="100%" AutoGenerateColumns="false"
                        GridLines="None" AllowPaging="false" ShowFooter="true" CellPadding="0" CellSpacing="0">
                        <Columns>
                            <asp:TemplateField HeaderText="SL" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="15px">
                                <ItemTemplate>
                                    <%# (Container.DataItemIndex) + 1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="name" HeaderText="Student Name" />
                            <asp:BoundField DataField="student_code" HeaderText="Code" />
                            <asp:BoundField DataField="batch_name" HeaderText="Batch" ItemStyle-HorizontalAlign="Center"
                                ItemStyle-Width="40px" />
                            <%--<asp:BoundField DataField="CourseName" HeaderText="Course" />--%>
                            <asp:BoundField DataField="stream_name" HeaderText="Stream" ItemStyle-HorizontalAlign="Center"
                                ItemStyle-Width="45px" FooterText="<b>Total</b>" />
                            <asp:TemplateField HeaderText="Received Amount" ItemStyle-HorizontalAlign="Right"
                                FooterStyle-HorizontalAlign="Right" ItemStyle-Width="125px">
                                <ItemTemplate>
                                    <%# Eval("ReceivedAmount","{0:n}") %>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Literal ID="ltrTotalReceivedAmount" runat="server" Mode="PassThrough"></asp:Literal>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Adjust Amount" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right"
                                ItemStyle-Width="120px">
                                <ItemTemplate>
                                    <%# Eval("AdjustedAmount","{0:n}") %>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Literal ID="ltrTotalAdjustedAmount" runat="server" Mode="PassThrough"></asp:Literal>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Refund Amount" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right"
                                ItemStyle-Width="120px">
                                <ItemTemplate>
                                    <%# Eval("RefundedAmount","{0:n}") %>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Literal ID="ltrTotalRefundedAmount" runat="server" Mode="PassThrough"></asp:Literal>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Balance Amount" ItemStyle-HorizontalAlign="Right"
                                FooterStyle-HorizontalAlign="Right" ItemStyle-Width="120px">
                                <ItemTemplate>
                                    <%# Eval("BalanceAmount","{0:n}") %>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Literal ID="ltrTotalBalanceAmount" runat="server" Mode="PassThrough"></asp:Literal>
                                </FooterTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <HeaderStyle CssClass="HeaderStyle" />
                        <RowStyle CssClass="RowStyle" />
                        <AlternatingRowStyle CssClass="AltRowStyle" />
                        <FooterStyle CssClass="RowStyle" />
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Button ID="btnPrint" runat="server" CssClass="button" Text="Print" OnClick="btnPrint_Click" />&nbsp;
                    <asp:Button ID="btnDownload" runat="server" CssClass="button" Text="Download" OnClick="btnDownload_Click" />
                </td>
            </tr>
        </table>
    </div>
    <br />
    <br />
    <br />
</asp:Content>
