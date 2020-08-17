<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAdmin.Master" AutoEventWireup="true"
    CodeBehind="Fees.aspx.cs" Inherits="CollegeERP.Student.Fees" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../UserControl/Message.ascx" TagName="Message" TagPrefix="uc3" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>        
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
            opacity: .50;
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

    <script language="javascript" type="text/javascript" src="../Scripts/ssjscript.js"></script>

    <script language="javascript" type="text/javascript">
        function Validation() {
            if (!checkforvaliddata(eval('document.forms[0].' + '<%=ddlBatch.ClientID%>'), "Addmission In", 0)) return false;
            if (!checkforvaliddata(eval('document.forms[0].' + '<%=ddlCourse1.ClientID%>'), "Select Course", 0)) return false;
            if (!checkforvaliddata(eval('document.forms[0].' + '<%=ddlStream1.ClientID%>'), "Select Stream", 0)) return false;
            if (!checkforvaliddata(eval('document.forms[0].' + '<%=txtFeesName.ClientID%>'), "Fees Name", 1)) return false;
            return confirm('Are You Sure?');
        }
        function validateBatchInsert() {
            if (!checkforvaliddata(eval('document.forms[0].' + '<%=txtBatchName.ClientID%>'), "Batch", 1)) return false;
            if (!checkforvaliddata(eval('document.forms[0].' + '<%=txtStartDate.ClientID%>'), "Start Date", 22)) return false;
            if (!checkforvaliddata(eval('document.forms[0].' + '<%=txtEndDate.ClientID%>'), "End Date", 22)) return false;
        }    
        
        function validateFeesHeadInsert()
        {
            if (!checkforvaliddata(eval('document.forms[0].' + '<%=txtFeesHead.ClientID%>'), "Head Name", 1)) return false;
            if (!checkforvaliddata(eval('document.forms[0].' + '<%=ddlAssestLedger.ClientID%>'), "Assets Ledger", 0)) return false;
            if (!checkforvaliddata(eval('document.forms[0].' + '<%=ddlIncomeLedger.ClientID%>'), "Income Ledger", 0)) return false;
        }
    </script>

    <asp:ToolkitScriptManager ID="ToolScript1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="title">
        <h5>
            Fees
        </h5>
    </div>
    <asp:UpdatePanel ID="Up1" runat="server">
        <ContentTemplate>
            <div>
                <div style="width: 740px;">
                    <uc3:Message ID="Message" runat="server" />
                </div>
                <div style="width: 740px;">
                    <table width="100%" align="center" class="table">
                        <tr>
                            <td align="left" width="20%" class="label">
                                Batch
                            </td>
                            <td style="width: 195px">
                                <asp:DropDownList ID="ddlBatch" runat="server" Width="192px" CssClass="dropdownList"
                                    AutoPostBack="True" OnSelectedIndexChanged="ddlBatch_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                            <td align="left" width="20%" class="label" valign="top">
                                <%--  <a href="#?w=350" rel="divAddBatch" class="poplight">Add New</a>--%>
                                <a rel="divAddBatch" class="poplight" onclick="popUp('350','divAddBatch')">
                                    <img src="../Images/newLeft.gif" style="cursor: pointer; opacity: 0.6;" />
                                </a>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" width="20%" class="label">
                                Select Course
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlCourse1" runat="server" Width="192px" CssClass="dropdownList"
                                    AutoPostBack="True" OnSelectedIndexChanged="ddlCourse1_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                            <td align="left" width="20%" class="label" valign="top">
                                <%-- <a href="#?w=350" rel="divAddStream" class="poplight">Add New</a>--%>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr id="trCourse" runat="server">
                            <td align="left" width="20%" class="label">
                                Select Stream
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlStream1" runat="server" Width="192px" CssClass="dropdownList"
                                    AutoPostBack="True" OnSelectedIndexChanged="ddlStream1_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                            <td align="left" width="20%" class="label" valign="top">
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" width="20%" class="label">
                                Fees Name
                            </td>
                            <td align="left" width="30%">
                                <asp:TextBox ID="txtFeesName" runat="server" CssClass="textbox_required" Width="90%"></asp:TextBox>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" width="20%" class="label">
                            </td>
                            <td align="left" width="30%">
                                <asp:Button ID="btnSearch" runat="server" Text=" Search" CssClass="button" OnClick="btnSearch_Click"
                                    Visible="false" />
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                        </tr>
                    </table>
                </div>
                <h6 align="left" style="color: #00356A;">
                    Fees Detsils</h6>
                <div style="width: 740px;">
                    <table width="100%" align="center" class="table">
                        <tr>
                            <td align="left">
                                <a rel="divAddFeesHead" class="poplight" onclick="popUp('350','divAddFeesHead')">New
                                    Fees Head </a>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <%--   <asp:Panel ID="panelfeesDetails" runat="server">
                                </asp:Panel>--%>
                                <asp:PlaceHolder ID="panelfeesDetails1" runat="server"></asp:PlaceHolder>
                                <br />
                            </td>
                        </tr>
                    </table>
                </div>
                <div style="width: 740px;">
                    <table width="100%" align="center" class="table">
                        <tr>
                            <td align="right">
                                <asp:Button ID="btnStramSave" runat="server" Text="Save" CssClass="button" OnClick="btnStramSave_Click"
                                    OnClientClick="return Validation();" />
                                &nbsp;
                                <asp:Button ID="btnCancel" runat="server" CssClass="button" Text="Reset" OnClick="btnCancel_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <asp:HiddenField ID="hidCourse_id" runat="server" Value="0" />
        </ContentTemplate>
    </asp:UpdatePanel>
    <div id="divAddBatch" class="popup_block">
        <div align="center">
            <table cellpadding="4" cellspacing="0" width="300px" style="font-family: Tahoma;
                font-size: 12px; color: #747879;">
                <tr>
                    <td colspan="2">
                        <div class="title" style="margin: 0 0 10px 0;">
                            <h4>
                                Add Addmission In
                            </h4>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                        Batch
                    </td>
                    <td>
                        <asp:TextBox ID="txtBatchName" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        Start Date
                    </td>
                    <td>
                        <asp:TextBox ID="txtStartDate" runat="server" CssClass="textbox_required" Width="80%"></asp:TextBox>
                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" CssClass="cal_Theme1"
                            PopupPosition="BottomRight" Format="dd/MM/yyyy" TargetControlID="txtStartDate"
                            OnClientDateSelectionChanged="" Enabled="True">
                        </asp:CalendarExtender>
                    </td>
                </tr>
                <tr>
                    <td>
                        End Date
                    </td>
                    <td>
                        <asp:TextBox ID="txtEndDate" runat="server" CssClass="textbox_required" Width="80%"></asp:TextBox>
                        <asp:CalendarExtender ID="CalendarExtender2" runat="server" CssClass="cal_Theme1"
                            PopupPosition="BottomRight" Format="dd/MM/yyyy" TargetControlID="txtEndDate"
                            OnClientDateSelectionChanged="" Enabled="True">
                        </asp:CalendarExtender>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                    <td align="center">
                        <asp:Button ID="btnSave" runat="server" CssClass="button" Text="Save" OnClick="btnSave_Click"
                            OnClientClick="return validateBatchInsert();" />
                        <input type="reset" value="Reset" class="button" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <%----------------------------------------------------------%>
    <div id="divAddFeesHead" class="popup_block">
        <div align="center">
            <table cellpadding="4" cellspacing="0" width="350px" style="font-family: Tahoma;
                font-size: 12px; color: #747879;">
                <tr>
                    <td colspan="2">
                        <div class="title" style="margin: 0 0 10px 0;">
                            <h4>
                                Add Fees Head
                            </h4>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                        Head Name
                    </td>
                    <td>
                        <asp:TextBox ID="txtFeesHead" runat="server" CssClass="textbox_required" Width="80%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        Head Type
                    </td>
                    <td>
                        <asp:RadioButtonList ID="rbtnListHeadType" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Value="SEM" Text="Semester" Selected="True"></asp:ListItem>
                            <asp:ListItem Value="HOS" Text="Hostel"></asp:ListItem>
                            <asp:ListItem Value="OTH" Text="Other"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td>
                        Assets Ledger
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlAssestLedger" runat="server" CssClass="dropdownList" Width="80%" DataValueField="LedgerID" DataTextField="LedgerName">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        Income Ledger
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlIncomeLedger" runat="server" CssClass="dropdownList" Width="80%" DataValueField="LedgerID" DataTextField="LedgerName">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>Refundable</td>
                    <td>
                        <asp:CheckBox ID="ChkIsRefundable" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>One Time Applicable</td>
                    <td>
                        <asp:CheckBox ID="ChkIsOneTimeApplicable" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                    <td align="center">
                        <asp:Button ID="btnFeesHeadSave" runat="server" CssClass="button" Text="Save" OnClientClick="return validateFeesHeadInsert();"
                            OnClick="btnFeesHeadSave_Click" />
                        <input type="reset" value="Reset" class="button" />
                    </td>
                </tr>
            </table>
        </div>
    </div>

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
            $('#' + popID).fadeIn().css({ 'width': Number(popWidth) }).prepend('<a href="#" class="close"><img src="../images/close_pop.png" class="btn_close" title="Close Window" alt="Close" onclick="popUpClose();" /></a>');
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
        function popUpClose() {
            $('#fade , .popup_block').fadeOut(function () {
                $('#fade, a.close').remove();
            });
        }
    </script>

</asp:Content>
