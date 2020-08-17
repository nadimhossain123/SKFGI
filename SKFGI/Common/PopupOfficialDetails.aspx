<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PopupOfficialDetails.aspx.cs"
    Inherits="CollegeERP.Common.PopupOfficialDetails" Title="Official Details" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="../UserControl/Message.ascx" TagName="Message" TagPrefix="uc3" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Official Details</title>
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
<body style="background-color: #fff;">
    <script src="../Scripts/ssjscript.js" type="text/javascript"></script>
    <script type="text/javascript">
        function Validation() {
            if (isValidDate(document.getElementById('<%=txtDOJ.ClientID%>').value) == false) {
                alert("Enter Date Of Joining in DD/MM/YYYY format");
                return false;
            }
            else if (document.getElementById('<%=txtConfDt.ClientID%>').value != '' && isValidDate(document.getElementById('<%=txtConfDt.ClientID%>').value) == false) {
                alert("Enter Conf Date in DD/MM/YYYY format");
                return false;
            }
            else if (document.getElementById('<%=txtLastEvaluationDate.ClientID%>').value != '' && isValidDate(document.getElementById('<%=txtLastEvaluationDate.ClientID%>').value) == false) {
                alert("Enter Last Evaluation Date in DD/MM/YYYY format");
                return false;
            }
            else if (document.getElementById('<%=txtEffectiveDate.ClientID%>').value != '' && isValidDate(document.getElementById('<%=txtEffectiveDate.ClientID%>').value) == false) {
                alert("Enter Effective Date in DD/MM/YYYY format");
                return false;
            }
            else if (document.getElementById('<%=txtPFEffDt.ClientID%>').value != '' && isValidDate(document.getElementById('<%=txtPFEffDt.ClientID%>').value) == false) {
                alert("Enter PF Effective Date in DD/MM/YYYY format");
                return false;
            }
            else if (document.getElementById('<%=txtESIEffDt.ClientID%>').value != '' && isValidDate(document.getElementById('<%=txtESIEffDt.ClientID%>').value) == false) {
                alert("Enter ESI Effective Date in DD/MM/YYYY format");
                return false;
            }
            else if (document.getElementById('<%=txtTDSEffDt.ClientID%>').value != '' && isValidDate(document.getElementById('<%=txtTDSEffDt.ClientID%>').value) == false) {
                alert("Enter TDS Effective Date in DD/MM/YYYY format");
                return false;
            }
            else if (document.getElementById('<%=ddlPayBand.ClientID%>').selectedIndex == 0) {
                alert("Please Select Payband");
                return false;
            }
            else if (document.getElementById('<%=txtDateOfResign.ClientID%>').value != '' && isValidDate(document.getElementById('<%=txtDateOfResign.ClientID%>').value) == false) {
                alert("Enter Resign Date in DD/MM/YYYY format");
                return false;
            }
            else if (document.getElementById('<%=txtDateLeaving.ClientID%>').value != '' && isValidDate(document.getElementById('<%=txtDateLeaving.ClientID%>').value) == false) {
                alert("Enter Leaving Date in DD/MM/YYYY format");
                return false;
            }
            else { return true; }
        }
        function isValidDate(s) {
            // format D(D)/M(M)/(YY)YY
            var dateFormat = /^\d{1,4}[\.|\/|-]\d{1,2}[\.|\/|-]\d{1,4}$/;

            if (dateFormat.test(s)) {
                // remove any leading zeros from date values
                s = s.replace(/0*(\d*)/gi, "$1");
                var dateArray = s.split(/[\.|\/|-]/);

                // correct month value
                dateArray[1] = dateArray[1] - 1;

                // correct year value
                if (dateArray[2].length < 4) {
                    // correct year value
                    dateArray[2] = (parseInt(dateArray[2]) < 50) ? 2000 + parseInt(dateArray[2]) : 1900 + parseInt(dateArray[2]);
                }

                var testDate = new Date(dateArray[2], dateArray[1], dateArray[0]);
                if (testDate.getDate() != dateArray[0] || testDate.getMonth() != dateArray[1] || testDate.getFullYear() != dateArray[2]) {
                    return false;
                } else {
                    return true;
                }
            } else {
                return false;
            }
        }

        function RefreshParent() {
            window.close();
            opener.location.reload();
        }

        function openpopup(poplocation, querystring, popheight, popwidth, poptop, popleft) {
            var popposition = 'left = 300, top=90, width=680,align=center, height=370,menubar=no, scrollbars=yes, resizable=no';

            var NewWindow = window.open(poplocation, '', popposition);
            if (NewWindow.focus != null) {
                NewWindow.focus();
            }
            return false;
        }
   
    </script>
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ToolScript1" runat="server">
    </asp:ToolkitScriptManager>
    <center>
        <div style="padding: 8px 0 8px 0; background-color: #FADC76;" id="divtitle" runat="server">
        </div>
        <br />
        <uc3:Message ID="Message" runat="server" />
        <br />
        <div style="width: 900px;">
            <%--<asp:UpdatePanel ID="UP1" runat="server">
                    <ContentTemplate>      --%>
            <table width="100%" align="center" style="padding: 4px; background-color: #fff;">
                <tr>
                    <td align="left" class="label">
                        Category
                    </td>
                    <td colspan="2" align="left">
                        <asp:DropDownList ID="ddlCategory" runat="server" CssClass="dropdownList" Width="120px"
                            DataValueField="CategoryId" DataTextField="CategoryName">
                        </asp:DropDownList>
                        <a rel="divAddCategory" class="poplight" onclick="popUp('600','divAddCategory')">
                            <img src="../Images/newLeft.gif" />
                        </a>
                        <%--<img id="btnAddCategory" runat="server" src="~/Images/newLeft.gif"   />--%>
                    </td>
                    <td align="left" class="label">
                        Department
                    </td>
                    <td  align="left" colspan="2">
                        <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="dropdownList" Width="120px"
                            DataValueField="DepartmentId" DataTextField="DepartmentName">
                        </asp:DropDownList>
                        <a rel="divAddDepartment" class="poplight" onclick="popUp('600','divAddDepartment')">
                            <img src="../Images/newLeft.gif" />
                        </a>
                        <%--<img id="btnDepartmentAdd" runat="server" src="~/Images/newLeft.gif"   />--%>
                    </td>
                </tr>
                <tr>
                    <td align="left" class="label">
                        Designation
                    </td>
                    <td colspan="2" align="left">
                        <asp:DropDownList ID="ddlDesignation" runat="server" CssClass="dropdownList" Width="120px"
                            DataValueField="DesignationId" DataTextField="DesignationName">
                        </asp:DropDownList>
                        <a rel="divAddDesignation" class="poplight" onclick="popUp('600','divAddDesignation')">
                            <img src="../Images/newLeft.gif" />
                        </a>
                        <%--<img id="btnAddDesignation" runat="server" src="~/Images/newLeft.gif"   />--%>
                    </td>
                    <td align="left" class="label">
                        Branch/Posted At
                    </td>
                    <td align="left" colspan="2">
                        <asp:DropDownList ID="ddlBranch" runat="server" CssClass="dropdownList" Width="120px"
                            DataValueField="BranchId" DataTextField="BranchName">
                        </asp:DropDownList>
                        <a rel="divAddBranch" class="poplight" onclick="popUp('600','divAddBranch')">
                            <img src="../Images/newLeft.gif" />
                        </a>
                        <%--<img id="btnAddBranch" runat="server" src="~/Images/newLeft.gif"   />--%>
                    </td>
                </tr>
                <tr>
                    <td align="left" class="label">
                        Grade
                    </td>
                    <td colspan="2" align="left">
                        <asp:DropDownList ID="ddlGrade" runat="server" CssClass="dropdownList" Width="120px"
                            DataValueField="GradeId" DataTextField="GradeName">
                        </asp:DropDownList>
                        <a rel="divAddGrade" class="poplight" onclick="popUp('600','divAddGrade')">
                            <img src="../Images/newLeft.gif" />
                        </a>
                        <%--<img id="btnAddGrade" runat="server" src="~/Images/newLeft.gif"   />--%>
                    </td>
                    <td align="left" class="label">
                        File No
                    </td>
                    <td align="left" colspan="2">
                        <asp:TextBox ID="txtFileNo" runat="server" CssClass="textbox" Width="120px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="left" class="label">
                        DOJ<span class="req">*</span>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtDOJ" runat="server" CssClass="textbox_required" Width="110px"></asp:TextBox>
                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" CssClass="cal_Theme1"
                            PopupPosition="BottomRight" Format="dd/MM/yyyy" TargetControlID="txtDOJ" OnClientDateSelectionChanged=""
                            Enabled="True">
                        </asp:CalendarExtender>
                    </td>
                    <td align="left" class="label">
                        Conf. Date
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtConfDt" runat="server" CssClass="textbox" Width="110px"></asp:TextBox>
                        <asp:CalendarExtender ID="CalendarExtender2" runat="server" CssClass="cal_Theme1"
                            PopupPosition="BottomRight" Format="dd/MM/yyyy" TargetControlID="txtConfDt" OnClientDateSelectionChanged=""
                            Enabled="True">
                        </asp:CalendarExtender>
                    </td>
                    <td align="left" class="label">
                        P.Tax(Y/N)
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ddlPTax" runat="server" CssClass="dropdownList">
                            <asp:ListItem Value="Y" Text="Y"></asp:ListItem>
                            <asp:ListItem Value="N" Text="N"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td align="left" class="label">
                        Last Evaluation Date
                    </td>
                    <td>
                        <asp:TextBox ID="txtLastEvaluationDate" runat="server" CssClass="textbox" Width="110px"></asp:TextBox>
                        <asp:CalendarExtender ID="CalendarExtender3" runat="server" CssClass="cal_Theme1"
                            PopupPosition="BottomRight" Format="dd/MM/yyyy" TargetControlID="txtLastEvaluationDate"
                            OnClientDateSelectionChanged="" Enabled="True">
                        </asp:CalendarExtender>
                    </td>
                    <td align="left" class="label">
                        Evaluation Type
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ddlEvaluationType" runat="server" CssClass="dropdownList">
                            <asp:ListItem Value="Increment" Text="Increment"></asp:ListItem>
                            <asp:ListItem Value="Decrement" Text="Decrement"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td align="left" class="label">
                        Next Evaluation Date
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtEffectiveDate" runat="server" CssClass="textbox" Width="110px"></asp:TextBox>
                        <asp:CalendarExtender ID="CalendarExtender4" runat="server" CssClass="cal_Theme1"
                            PopupPosition="BottomRight" Format="dd/MM/yyyy" TargetControlID="txtEffectiveDate"
                            OnClientDateSelectionChanged="" Enabled="True">
                        </asp:CalendarExtender>
                    </td>
                </tr>
                <tr>
                    <td align="left" class="label">
                        PF(Y/N)
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ddlPF" runat="server" CssClass="dropdownList" AutoPostBack="true"
                            OnSelectedIndexChanged="ddlPF_SelectedIndexChanged">
                            <asp:ListItem Value="Y" Text="Y"></asp:ListItem>
                            <asp:ListItem Value="N" Text="N"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td align="left" class="label">
                        Effective Date
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtPFEffDt" runat="server" CssClass="textbox" Width="110px"></asp:TextBox>
                        <asp:CalendarExtender ID="CalendarExtender5" runat="server" CssClass="cal_Theme1"
                            PopupPosition="BottomRight" Format="dd/MM/yyyy" TargetControlID="txtPFEffDt"
                            OnClientDateSelectionChanged="" Enabled="True">
                        </asp:CalendarExtender>
                    </td>
                    <td align="left" class="label">
                        P.F.No
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtPFNo" runat="server" CssClass="textbox" Width="110px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="left" class="label">
                        ESI(Y/N)
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ddlESI" runat="server" CssClass="dropdownList" AutoPostBack="true"
                            OnSelectedIndexChanged="ddlESI_SelectedIndexChanged">
                            <asp:ListItem Value="Y" Text="Y"></asp:ListItem>
                            <asp:ListItem Value="N" Text="N"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td align="left" class="label">
                        Effective Date
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtESIEffDt" runat="server" CssClass="textbox" Width="110px"></asp:TextBox>
                        <asp:CalendarExtender ID="CalendarExtender6" runat="server" CssClass="cal_Theme1"
                            PopupPosition="BottomRight" Format="dd/MM/yyyy" TargetControlID="txtESIEffDt"
                            OnClientDateSelectionChanged="" Enabled="True">
                        </asp:CalendarExtender>
                    </td>
                    <td align="left" class="label">
                        UAN No
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtUNANo" runat="server" CssClass="textbox" Width="110px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="left" class="label">
                        TDS(Y/N)
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ddlTDS" runat="server" CssClass="dropdownList" AutoPostBack="true"
                            OnSelectedIndexChanged="ddlTDS_SelectedIndexChanged">
                            <asp:ListItem Value="Y" Text="Y"></asp:ListItem>
                            <asp:ListItem Value="N" Text="N"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td align="left" class="label">
                        Effective Date
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtTDSEffDt" runat="server" CssClass="textbox" Width="110px"></asp:TextBox>
                        <asp:CalendarExtender ID="CalendarExtender7" runat="server" CssClass="cal_Theme1"
                            PopupPosition="BottomRight" Format="dd/MM/yyyy" TargetControlID="txtTDSEffDt"
                            OnClientDateSelectionChanged="" Enabled="True">
                        </asp:CalendarExtender>
                    </td>
                    <td align="left" class="label">
                        ESI No
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtESINo" runat="server" CssClass="textbox" Width="110px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="left" class="label">
                        Health Card(Y/N)
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ddlHealthCard" runat="server" CssClass="dropdownList" AutoPostBack="true"
                            OnSelectedIndexChanged="ddlHealthCard_SelectedIndexChanged">
                            <asp:ListItem Value="Y" Text="Y"></asp:ListItem>
                            <asp:ListItem Value="N" Text="N"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td align="left" class="label">
                        Mediclaim No
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtMediclaimNo" runat="server" CssClass="textbox" Width="110px"></asp:TextBox>
                    </td>
                    <td align="left" class="label">
                        PAN
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtPAN" runat="server" CssClass="textbox" Width="110px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="left" class="label">
                        Date Of Resign
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtDateOfResign" runat="server" CssClass="textbox" Width="110px"></asp:TextBox>
                        <asp:CalendarExtender ID="CalendarExtender8" runat="server" CssClass="cal_Theme1"
                            PopupPosition="BottomRight" Format="dd/MM/yyyy" TargetControlID="txtDateOfResign"
                            OnClientDateSelectionChanged="" Enabled="True">
                        </asp:CalendarExtender>
                    </td>
                    <td align="left" class="label">
                        Notice Period
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtNoticePeriod" runat="server" CssClass="textbox" Width="110px"></asp:TextBox>
                        <asp:FilteredTextBoxExtender ID="ftb1" runat="server" FilterType="Numbers" TargetControlID="txtNoticePeriod">
                        </asp:FilteredTextBoxExtender>
                    </td>
                    <td align="left" class="label">
                        Aadhaar</td>
                    <td align="left">
                        <asp:TextBox ID="txtAadhaar" runat="server" CssClass="textbox" Width="110px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="left" class="label">
                        Date Of Leaving
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtDateLeaving" runat="server" CssClass="textbox" Width="110px"></asp:TextBox>
                        <asp:CalendarExtender ID="CalendarExtender9" runat="server" CssClass="cal_Theme1"
                            PopupPosition="BottomRight" Format="dd/MM/yyyy" TargetControlID="txtDateLeaving"
                            OnClientDateSelectionChanged="" Enabled="True">
                        </asp:CalendarExtender>
                    </td>
                    <td align="left" class="label">
                        Reason Of Leaving
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtReasonLeving" runat="server" CssClass="textbox" Width="110px"></asp:TextBox>
                    </td>
                    <td align="left" class="label">
                        Employee Type
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ddlEmployeeType" runat="server" CssClass="dropdownList">
                            <asp:ListItem Value="Teaching" Text="Teaching Staff"></asp:ListItem>
                            <asp:ListItem Value="NonTeaching" Text="Non-Teaching Staff"></asp:ListItem>
                            <asp:ListItem Value="Contract" Text="Contractual Basis"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td align="left" class="label">
                        &nbsp;</td>
                    <td align="left">
                        &nbsp;</td>
                    <td align="left" class="label">
                        PayBand<span class="req">*</span>
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ddlPayBand" runat="server" CssClass="dropdownList" Width="110px"
                            DataTextField="PayBandName" DataValueField="PayBandId">
                        </asp:DropDownList>
                    </td>
                    <td align="left" class="label">
                        Working Days
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ddlWorkingDays" runat="server" CssClass="dropdownList">
                            <asp:ListItem Value="5" Text="5"></asp:ListItem>
                            <asp:ListItem Value="6" Text="6"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td align="right" colspan="6">
                        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="button" OnClientClick="javascript:return Validation()"
                            OnClick="btnSave_Click" />
                    </td>
                </tr>
            </table>
            <%--</ContentTemplate>
                </asp:UpdatePanel>--%>
        </div>
    </center>
    <div id="divAddCategory" class="popup_block">
        <iframe width="610" height="300" src="CategoryMaster.aspx"></iframe>
    </div>
    <div id="divAddBranch" class="popup_block">
        <iframe width="610" height="300" src="BranchMaster.aspx"></iframe>
    </div>
    <div id="divAddDepartment" class="popup_block">
        <iframe width="610" height="300" src="DepartmentMaster.aspx"></iframe>
    </div>
    <div id="divAddDesignation" class="popup_block">
        <iframe width="610" height="300" src="DesignationMaster.aspx"></iframe>
    </div>
    <div id="divAddGrade" class="popup_block">
        <iframe width="610" height="300" src="GradeMaster.aspx"></iframe>
    </div>
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

    function RefreshParent() {
        $('#fade , .popup_block').fadeOut(function () {
            $('#fade, a.close').remove();
        });
        window.location.reload();
    }
</script>
</html>
