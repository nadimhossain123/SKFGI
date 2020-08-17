<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmployeeTermsAndCondition.aspx.cs"
    Inherits="CollegeERP.Common.EmployeeTermsAndCondition" Title="Employee Terms & Condition" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Employee Terms & Condition</title>
    <link href="../Styles/style.css" rel="Stylesheet" type="text/css" />
    <link href="../Styles/Control.css" rel="Stylesheet" type="text/css" />
    <link href="../Styles/blue.css" rel="Stylesheet" type="text/css" />
    <link href="../Styles/reset.css" rel="Stylesheet" type="text/css" />
    <style type="text/css">
        .Img
        {
            cursor: pointer;
            width: 38px;
            height: 26px;
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
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="Tool1" runat="server">
    </asp:ToolkitScriptManager>
    <asp:UpdatePanel ID="UP1" runat="server">
        <ContentTemplate>
            <center>
                <div style="width: 800px;">
                    <br />
                    <table width="100%" align="center" class="table">
                        <tr>
                            <td align="left">
                                <a rel="divAddTerms" class="poplight" onclick="popUp('600','divAddTerms')">
                                    <img src="../Images/newLeft.gif" />
                                </a>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <asp:GridView ID="dgvTerms" runat="server" AutoGenerateColumns="false" AllowPaging="false"
                                    Width="100%" DataKeyNames="TermsId" OnRowDataBound="dgvTerms_RowDataBound" OnRowEditing="dgvTerms_RowEditing"
                                    GridLines="None">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:HiddenField ID="HiddenId" runat="server" Value='<%#Bind("EmployeeTermsId") %>' />
                                                <asp:ImageButton ID="btnApplicable" runat="server" CommandName="Edit" Width="30px"
                                                    Height="30px" Style="cursor: pointer;" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="TermsName" HeaderText="Terms & Condition" />
                                    </Columns>
                                    <HeaderStyle CssClass="HeaderStyle" />
                                    <RowStyle CssClass="RowStyle" />
                                    <EmptyDataRowStyle CssClass="EditRowStyle" />
                                    <AlternatingRowStyle CssClass="AltRowStyle" />
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                </div>
                <div id="divAddTerms" class="popup_block">
                    <iframe width="610" height="300" src="PopUpTermsAndCondition.aspx"></iframe>
                </div>
            </center>
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>

<script src="../Scripts/jquery-1.6.4.min.js" type="text/javascript"></script>

<script type="text/javascript">
//        $(document).ready(function () {
//            $('a.close, #fade').live('click', function () { //When clicking on the close or fade layer...
//                $('#fade , .popup_block').fadeOut(function () {
//                    $('#fade, a.close').remove();
//                }); //fade them both out
//                return false;
//            });
//        });

        function popUp(width, popUpID) {
            var popID = popUpID;
            var popWidth = width;
            //Fade in the Popup and add close button
            $('#' + popID).fadeIn().css({ 'width': Number(popWidth) }).prepend('<a href="#" class="close"><img src="../images/close_pop.png" class="btn_close" title="Close Window" alt="Close" onClick="RefreshParent()" /></a>');
            //Define margin for center alignment (vertical + horizontal) - we add 80 to the height/width to accomodate for the padding + border width defined in the css
            var popMargTop = ($('#' + popID).height() + 80) / 2;
            var popMargLeft = ($('#' + popID).width() + 80) / 2;
            //Apply Margin to Popup
            $('#' + popID).css({
                'margin-top': -popMargTop,
                'margin-left': -popMargLeft
            });
            //Fade in Background
            $('body').append('<div id="fade"></div>'); //Add the fade layer to bottom of the body tag.
            $('#fade').css({ 'filter': 'alpha(opacity=80)' }).fadeIn(); //Fade in the fade layer 
            return false;
        }

        function RefreshParent()
        {
                $('#fade , .popup_block').fadeOut(function () {
                    $('#fade, a.close').remove();
                });
            window.location.reload();
        }
</script>

</html>
