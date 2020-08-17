<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AppointmentLetter.aspx.cs"
    Inherits="CollegeERP.Common.AppointmentLetter" Title="Appointment Letter" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Appointment Letter</title>
    <style type="text/css">
        .text
        {
            color: #000000;
            font-family: Lucida Grande, Verdana, Lucida Sans Regular, Lucida Sans Unicode, Arial, sans-serif;
            font-size: 11px;
            font-weight: normal;
            padding-left: 10px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <center>
        <div style="width: 940px;" id="divMain" runat="server">
            <h4>
                <u>APPOINTMENT-LETTER</u></h4>
            <br />
            <table width="100%" align="center">
                <tr>
                    <td align="left" width="50%" class="text">
                        <asp:Literal ID="ltrRefNo" runat="server" Mode="PassThrough"></asp:Literal>
                    </td>
                    <td align="right" width="50%" class="text">
                        <asp:Literal ID="ltrDate" runat="server" Mode="PassThrough"></asp:Literal>
                    </td>
                </tr>
            </table>
            <br />
            <table width="100%" align="center">
                <tr>
                    <td align="left" class="text">
                        <asp:Literal ID="ltrHeader" runat="server" Mode="PassThrough"></asp:Literal>
                    </td>
                </tr>
            </table>
            <br />
            <table width="100%" align="center">
                <tr>
                    <td align="left" class="text">
                        <asp:GridView ID="dgvSalary" runat="server" Width="60%" CellPadding="0" CellSpacing="0"
                            ShowHeader="false" AutoGenerateColumns="false" AllowPaging="false" GridLines="None" ShowFooter="true">
                            <Columns>
                                <asp:TemplateField ShowHeader="false" FooterText="<b>CTC</b>">
                                    <ItemTemplate>
                                        <%# DataBinder.Eval(Container.DataItem, "SalaryHeadDetails")%>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="text" HorizontalAlign="Left" />
                                    <FooterStyle CssClass="text" HorizontalAlign="Left" />
                                    
                                </asp:TemplateField>
                                <asp:TemplateField ShowHeader="false">
                                    <ItemTemplate>
                                        <%# DataBinder.Eval(Container.DataItem, "SalaryHeadAmount")%>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Literal ID="ltrCTC" runat="server" Mode="PassThrough"></asp:Literal>
                                    </FooterTemplate> 
                                    <ItemStyle CssClass="text" HorizontalAlign="Left" />
                                    <FooterStyle CssClass="text" HorizontalAlign="Left" />
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
            </table>
            <br />
            <table width="100%" align="center">
                <tr>
                    <td align="left" class="text">
                        <asp:Literal ID="ltrTerms" runat="server" Mode="PassThrough"></asp:Literal>
                    </td>
                </tr>
            </table>
            <br />
            <table width="100%" align="center">
                <tr>
                    <td align="left" class="text">
                        Yours faithfully,
                    </td>
                </tr>
                <tr>
                    <td>
                        <br />
                    </td>
                </tr>
                <tr>
                    <td align="left" class="text">
                        <b>SUPREME KNOWLEDGE FOUNDATION</b>
                    </td>
                </tr>
                <tr>
                    <td>
                        <br />
                        <br />
                        <br />
                    </td>
                </tr>
                <tr>
                    <td align="left" class="text">
                        <b>(Bijoy Guha Mallick)</b>
                    </td>
                </tr>
                <tr>
                    <td align="left" class="text">
                        <b>Chairman</b>
                    </td>
                </tr>
            </table>
        </div>
        
        <br />
        <asp:Button ID="btnDownload" runat="server" Text="Download as PDF" Visible="false" 
            onclick="btnDownload_Click" />
        
    </center>
    </form>
</body>
</html>
