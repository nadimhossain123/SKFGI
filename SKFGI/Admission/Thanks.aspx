﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Thanks.aspx.cs" Inherits="CollegeERP.Admission.Thanks" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Thanks</title>
    <script type="text/javascript">
        window.history.forward(1); 
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">  
        </asp:ScriptManager>  
        <asp:Timer ID="Timer1" runat="server" Interval="4000" ontick="Timer1_Tick">  
        </asp:Timer>
        
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <center>
            <div style="background-color:#CEE5FF; width:45%; border:solid 1px #1399EB; padding:50px 70px 50px 70px; font-family:Book Antiqua; font-size:18px;">
                Thanks For Registering Your Details.
            </div>    
        </center>
    </form>
</body>
</html>
