<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PopUpLibraryFineVoucher.aspx.cs" Inherits="CollegeERP.Student.PopUpLibraryFineVoucher" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Voucher Print</title>
    <style type="text/css">
        .text
        {
            font:12px Verdana, Verdana, Arial, Helvetica, sans-serif;
            color:#333333;
            
        }
       
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <center>
             <br />
             <div style="width:700px;" class="text">
                    <div><b>Supreme Knowledge Foundation Group of Institutions (SKFGI)</b></div>
                    <div>[Organised by: Supreme Knowledge Foundation (SKF)]</div>
                    <div><b><u>Library Fine Voucher</u></b></div>
                    <div><br /></div>
                    
                    <table width="100%" align="center" cellspacing="0" cellpadding="5">
                            <tr>
                                <td align="left" width="20%"><b>Voucher No:</b></td>
                                <td align="left" width="30%">
                                    <asp:Literal ID="ltrVoucherNo" runat="server" Mode="PassThrough"></asp:Literal>
                                </td>
                                <td align="left" width="20%"><b>Date:</b></td>
                                <td align="left" width="30%">
                                    <asp:Literal ID="ltrDate" runat="server" Mode="PassThrough"></asp:Literal>
                                </td>
                            </tr>
                            <tr><td colspan="4"><br /></td></tr>
                            <tr>
                                <td align="left" width="20%"><b>Student Name:</b></td>
                                <td align="left" width="30%">
                                    <asp:Literal ID="ltrStudentName" runat="server" Mode="PassThrough"></asp:Literal>
                                </td>
                                <td align="left" width="20%"><b>Student Code:</b></td>
                                <td align="left" width="30%">
                                    <asp:Literal ID="ltrStudentCode" runat="server" Mode="PassThrough"></asp:Literal>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" width="20%"><b>Course:</b></td>
                                <td align="left" width="30%">
                                    <asp:Literal ID="ltrCourse" runat="server" Mode="PassThrough"></asp:Literal>
                                </td>
                                <td align="left" width="20%"><b>Batch:</b></td>
                                <td align="left" width="30%">
                                    <asp:Literal ID="ltrBatch" runat="server" Mode="PassThrough"></asp:Literal>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" width="20%"><b>Amount:</b></td>
                                <td align="left" width="30%">
                                    <asp:Literal ID="ltrAmount" runat="server" Mode="PassThrough" ></asp:Literal>
                                </td>
                                <td align="left" width="20%"><b>Semester:</b></td>
                                <td align="left" width="30%">
                                    <asp:Literal ID="ltrSemNo" runat="server" Mode="PassThrough" ></asp:Literal>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" width="20%"><b>Reason:</b></td>
                                <td align="left" colspan="3">
                                    <asp:Literal ID="ltrReason" runat="server" Mode="PassThrough"></asp:Literal>
                                </td>
                                
                            </tr>
                            <tr>
                            <td colspan="4" align="right" style="padding-top:45px;">
                                Signature of Authorised Signatory with Stamp & Date
                            </td>
                            </tr>
                            
                    </table>
                    <br />
             </div>
        </center>            
    </form>
</body>
</html>
