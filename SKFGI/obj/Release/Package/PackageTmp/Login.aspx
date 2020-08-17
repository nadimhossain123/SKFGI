﻿<%@ Page Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs"
    Inherits="CollegeERP.Login" Title="Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">
       // window.onload=CheckBrowser();
        function CheckBrowser()
        {
            var is_chrome = navigator.userAgent.toLowerCase().indexOf('chrome');
            if (is_chrome == -1)
                window.location.href='GoogleCrome.htm';
        }
        function Validation() {
            if (document.getElementById('<%=txtUserName.ClientID%>').value == '') {
                alert("Please Enter User ID");
                return false;
            }
            else if (document.getElementById('<%=txtPassword.ClientID%>').value == '') {
                alert("Please Enter Password");
                return false;
            }
            else { return true; }
        }        
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <img src="Images/AboutUs.JPG" />
    <div align="center">
        <div id="login-box">
            <h2>
                Login</h2>
            Supreme Knowledge Foundation Group of Institutions (SKFGI)
            <br />
            <asp:RadioButtonList ID="rbtnLoginAs" runat="server" RepeatDirection="Horizontal">
                <asp:ListItem Value="Employee" Text="Employee"></asp:ListItem>
                <asp:ListItem Value="Student" Text="Student Reg"></asp:ListItem>
                <asp:ListItem Value="SuperAdmin" Text="Super Admin"></asp:ListItem>
            </asp:RadioButtonList>
            
            <div id="login-box-name" style="margin-top: 1px;">
                Username</div>
            <div id="login-box-field" style="margin-top: 1px;">
                <asp:TextBox ID="txtUserName" runat="server" CssClass="form-login" Width="170px"></asp:TextBox>
            </div>
            <div id="login-box-name">
                Password</div>
            <div id="login-box-field">
                <asp:TextBox ID="txtPassword" runat="server" CssClass="form-login" Width="170px"
                    TextMode="Password"></asp:TextBox>
            </div>
            <br />
            <br />
            <br />
            <asp:ImageButton ID="btnLogIn" runat="server" ImageUrl="~/Images/login-btn.png" OnClientClick="javascript:return Validation()"
                OnClick="btnLogIn_Click" />
            <br />
            <br />
        </div>
    </div>
</asp:Content>
