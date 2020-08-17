﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StudentIDCard.aspx.cs"
    Inherits="CollegeERP.Student.StudentIDCard" Title="ID Card" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ID Card</title>
    <link href="../Styles/Control.css" rel="Stylesheet" type="text/css" />
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
        <div style="padding: 1px 3px 10px 3px; width: 435px; border: solid 1px #000;">
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td width="20%" align="center" valign="middle">
                        <img src="../Images/logo-small.jpg" height="60px" />
                    </td>
                    <td width="80%" align="center" valign="middle">
                        <h4 style="color:#4B4686;">
                            SUPREME KNOWLEDGE FOUNDATION<br />
                            GROUP OF INSTITUTIONS</h4>
                    </td>
                </tr>
                <tr>
                    <td align="center" valign="top" colspan="2" class="label">
                        1, Khan Road, P.O.: Mankundu, Hooghly 712139, W.B., India<br />
                        Ph.: 033-26831141 E-mail: mankundu@skf.edu.in Website: www.skf.edu.in
                    </td>
                </tr>
                <tr>
                    <td align="center" valign="top" colspan="2">
                        <h4>
                            IDENTITY CARD</h4>
                    </td>
                </tr>
            </table>
            <table width="100%" cellpadding="2">
                <tr>
                    <td width="25%" align="center" valign="top" rowspan="6">
                        <asp:Image ID="ImgID" runat="server" Width="90px" Height="84px" />
                    </td>
                    <td class="text" align="left">
                        <asp:Literal ID="ltrStudentName" runat="server" Mode="PassThrough"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td class="text" align="left">
                        <asp:Literal ID="ltrStudentCode" runat="server" Mode="PassThrough"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td class="text" align="left">
                        <asp:Literal ID="ltrStream" runat="server" Mode="PassThrough"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td class="text" align="left">
                        <asp:Literal ID="ltrDOB" runat="server" Mode="PassThrough"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td class="text" align="left">
                        <asp:Literal ID="ltrBloodGroup" runat="server" Mode="PassThrough"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td class="text" align="left">
                        <asp:Literal ID="ltrValid" runat="server" Mode="PassThrough"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="right">
                        <img src="../Images/StudentCampusDirector.JPG" width="110px" height="40px" />
                    </td>
                </tr>
            </table>
        </div>
        <br />
        <br />
        <br />
        <div style="padding: 10px 10px 10px 10px; width: 435px; border: solid 1px #000;">
            <hr />
            <table width="100%" align="center">
                <tr>
                    <td colspan="2" align="center" class="label">
                        (Full Signature of the Student)
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <br />
                    </td>
                </tr>
                <tr>
                    <td align="left" colspan="2" class="text" valign="top">
                        <asp:Literal ID="ltrAddress" runat="server" Mode="PassThrough"></asp:Literal>
                    </td>
                    
                </tr>
                <tr>
                    <td width="50%" align="left" class="text" valign="top">
                        <asp:Literal ID="ltrS_Mobile" runat="server" Mode="PassThrough"></asp:Literal>
                    </td>
                    <td width="50%" align="left" class="text" valign="top">
                        <asp:Literal ID="ltrP_Mobile" runat="server" Mode="PassThrough"></asp:Literal>
                    </td>
                </tr>
               
            </table>
            <hr />
            <span class="label">This Card is non-transferable. If found elsewhere, please return
                to:<br />
                Campus Director, Supreme Knowledge Foundation Group of Institutions,<br />
                1, Khan Road, Mankundu, Hooghly 712139, West Bengal, India </span>
        </div>
    </center>
    </form>
</body>
</html>
