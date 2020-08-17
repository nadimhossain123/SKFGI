<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PopUpChangeStudentDetail.aspx.cs" Inherits="CollegeERP.Common.PopUpChangeStudentDetail" Title="Change Details" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register src="../UserControl/Message.ascx" tagname="Message" tagprefix="uc3" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Change Detail</title>
      <link href="../Styles/style.css" rel="Stylesheet" type="text/css" />
    <link href="../Styles/Control.css" rel="Stylesheet" type="text/css" />
    <link href="../Styles/blue.css" rel="Stylesheet" type="text/css" />
    <link href="../Styles/reset.css" rel="Stylesheet" type="text/css" />
        <style type="text/css">
        
        
        .Img
        {
        	cursor:pointer;
        	width:38px;
        	height:26px;
        }
        
        /*------------------POPUPS------------------------*/#fade
        {
            display: none;
            background: #fff;
            position: fixed;
            left: 0;
            top: 0;
            z-index: 10;
            width: 100%;
            height: 100%;
            opacity: .60;
            z-index: 9999;
        }
        .popup_block
        {
            display: none;
            background: #fff;
            padding: 10px;
            border: 5px solid #ddd;
            float: left;
            font-size: 1.2em;
            position: fixed;
            top: 50%;
            left: 50%;
            z-index: 99999;
            -webkit-box-shadow: 0px 0px 20px #000;
            -moz-box-shadow: 0px 0px 20px #000;
            box-shadow: 0px 0px 20px #000;
            -webkit-border-radius: 10px;
            -moz-border-radius: 10px;
            border-radius: 10px;
        }
        img.btn_close
        {
            float: right;
            margin: -40px -40px 0 0;
        }
        .popup p
        {
            padding: 5px 10px;
            margin: 5px 0;
        }
        /*--Making IE6 Understand Fixed Positioning--*/*html #fade
        {
            position: absolute;
        }
        *html .popup_block
        {
            position: absolute;
        }
            .style1
            {
                width: 15%;
            }
    </style>

    <script type="text/javascript">
      function RefreshParent()
        {
            //            window.close();
            opener.location.reload();
        }
         function Validation()
        {
            if (document.getElementById('<%=ddlStream.ClientID%>').selectedIndex == 0)
            {
            alert("Please Select a Stream");
            return false;
            }
         } 
           function ValidationFees()
        {
            if (document.getElementById('<%=ddlFeeStructure.ClientID%>').selectedIndex == 0)
            {
            alert("Please Select a Fee Structure");
            return false;
            }
         }   
    
    function openpopup(poplocation, querystring, popheight, popwidth,poptop, popleft)
     {                
        var popposition='left = 200, top=50, width=950,align=center, height=500,menubar=no, scrollbars=yes, resizable=no';                
               
        var NewWindow = window.open(poplocation,'',popposition);                
        if (NewWindow.focus!=null){                        
        NewWindow.focus();                
        }        
     }
     
     function openRolepopup(poplocation, querystring, popheight, popwidth,poptop, popleft)
     {                
        var popposition='left = 200, top=15, width=850,align=center, height=640,menubar=no, scrollbars=yes, resizable=no';                
               
        var NewWindow = window.open(poplocation,'',popposition);                
        if (NewWindow.focus!=null){                        
        NewWindow.focus();                
        }        
     }
    </script>
</head>
<body onunload="RefreshParent()" >
    <form id="form1" runat="server">
    
     <div style="padding:8px 0 8px 0; background-color:#FADC76;" id="divtitle" runat="server">
            </div>
            <br />
            <uc3:Message ID="Message" runat="server" />
            <br />
            <div style="width:450px;">
                <%--<asp:UpdatePanel ID="UP1" runat="server">
                    <ContentTemplate>      --%>                  
                        <table width="100%" align="center" style="padding:4px; background-color:#fff;">
                                <tr>
                                <td width="15%" align="left" class="label">Batch</td>
                                <td colspan="2" align="left" class="style1">
                                    <asp:DropDownList ID="ddlBatch" runat="server" CssClass="dropdownList" Width="120px" DataValueField="id" DataTextField="batch_name"></asp:DropDownList>
                                    
                                    <%--<img id="btnAddCategory" runat="server" src="~/Images/newLeft.gif"   />--%>
                                </td>
                                <td width="15%" align="right" class="label">
                                    
                                </td>
                               

                                </tr>
                               <tr>
                                <td width="15%" align="left" class="label">Stream</td>
                                <td colspan="2" align="left" class="style1">
                                    <asp:DropDownList ID="ddlStream" runat="server" CssClass="dropdownList" Width="120px" DataValueField="id" DataTextField="stream_name"></asp:DropDownList>
                                    
                                    <%--<img id="btnAddCategory" runat="server" src="~/Images/newLeft.gif"   />--%>
                                </td>
                                <td width="15%" align="right" class="label">
                                    <asp:Button ID="btnSave" runat="server" Text="Update" CssClass="button" 
                                                OnClientClick="javascript:return Validation()"  
                                        onclick="btnSave_Click"/>
                                </td>
                               

                                </tr>
                                <tr>
                                    <td colspan="3"  style="width:45% ">
                                    </td>
                                </tr>
                                <tr>
                                    <td width="15%" align="left" class="label" visible="false"><%--Fee Structure--%></td>
                                <td colspan="2" align="left" class="style1">
                                    <asp:DropDownList ID="ddlFeeStructure" runat="server" CssClass="dropdownList" Width="120px" DataValueField="id" DataTextField="fees_name" Visible="false"></asp:DropDownList>
                                    
                                    <%--<img id="btnAddCategory" runat="server" src="~/Images/newLeft.gif"   />--%>
                                </td>
                                <td width="15%" align="right" class="label">
                                    <asp:Button ID="btnSaveFees" runat="server" Text="Update Fees" CssClass="button" 
                                                OnClientClick="javascript:return ValidationFees()"  
                                        onclick="btnSaveFees_Click" Visible="false"/>
                                </td>
                                </tr>
                                <tr>
                                    <td colspan="3"  style="width:45% ">
                                    </td>
                                </tr>
                              <%--  <tr align="center">
                                    <td colspan="3"  style="width:45% ">
                                    <asp:Button ID="btnSaveAll" runat="server" Text="Save" CssClass="button" 
                                                OnClientClick="javascript:return ValidationFees()"  
                                        onclick="btnSaveAll_Click"/>
                                    </td>
                                </tr>--%>
                                </table>
    </div>
    </form>
</body>
</html>
