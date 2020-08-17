<%@ Page Title="Approve/Reject Student" Language="C#" MasterPageFile="~/MasterAdmin.Master" AutoEventWireup="True"
    CodeBehind="ApproveStudent.aspx.cs" Inherits="CollegeERP.Student.ApproveStudent" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../UserControl/Message.ascx" TagName="Message" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script language="javascript" type="text/javascript" src="../Scripts/ssjscript.js"></script>

    <script language="javascript" type="text/javascript">
        function openpopup(poplocation, querystring, popheight, popwidth, poptop, popleft) {
            if (popheight == '') {
                popheight = 500
            }
            var popposition = 'left = 300, top=60, width=' + popwidth + ',align=center, height='+ popheight +',menubar=no, scrollbars=yes, resizable=no';

            var NewWindow = window.open(poplocation + querystring, '', popposition);
            if (NewWindow.focus != null) {
                NewWindow.focus();
            }
            return false;
        }
           function openIDpopup(poplocation, querystring, popheight, popwidth,poptop, popleft)
             {                
                var popposition='left = 200, top=15, width=850,align=center, height=640,menubar=no, scrollbars=yes, resizable=no';                
                       
                var NewWindow = window.open(poplocation,'',popposition);                
                if (NewWindow.focus!=null){                        
                NewWindow.focus();                
                }        
             }
        
        function openStudentFormPopUp()
        {
            var URL=document.getElementById('<%=HidURL.ClientID%>').value;
            if (URL != '#')
            {
                 var popposition = 'left = 0, top=0, width=' + screen.width + ',align=center, height=' + screen.height + ',menubar=no, scrollbars=yes, resizable=no';
                var NewWindow = window.open(URL, '', popposition);
                if (NewWindow.focus != null) {
                NewWindow.focus();
                }
                return false;
            }
                
        }
        
        function validation() {
            if (document.getElementById('<%=ddlBatch.ClientID%>').selectedIndex == 0)
                return ShowMsg("Please Select Batch");
            else if (document.getElementById('<%=ddlCourse1.ClientID%>').selectedIndex == 0)
                return ShowMsg("Please Select Course");
            else if (document.getElementById('<%=ddlStudent.ClientID%>').selectedIndex == 0)
                return ShowMsg("Please Select Student");
            else if (document.getElementById('<%=ddlFees.ClientID%>').selectedIndex == 0)
                return ShowMsg("Please Select Fees Structure");            
            else if (document.getElementById('<%=ChkHostelFacility.ClientID%>').checked == true && document.getElementById('<%=ddlHostelFees.ClientID%>').selectedIndex == 0)
                return ShowMsg("Please Select Hostel Fees Structure When Hostel is Applicable");
            else
                return confirm("Are You Sure");    
        }
        
        function ShowMsg(str)
        {
            alert(str);
            return false;
        }
        
        function RejectValidation()
        {
            if (!checkforvaliddata(eval('document.forms[0].' + '<%=ddlStudent.ClientID%>'), "Select Student", 0)) return false;
            return confirm('Are You Sure?'); 
        }
        
        function HostelEnableDisable(Obj)
        {
            var ddl=document.getElementById('<%=ddlHostelFees.ClientID%>');
            ddl.selectedIndex=0;
            if (!Obj.checked)
                ddl.disabled=true;
            else
                ddl.disabled=false;    
        }
    </script>

    <asp:ToolkitScriptManager ID="ToolScript1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="title">
        <h5>
            Approve Student
        </h5>
    </div>
    <asp:UpdatePanel ID="Up1" runat="server">
        <ContentTemplate>
            <div>
                <div style="width: 740px;">
                    <uc3:Message ID="Message" runat="server" />
                </div>
                <div style="width: 740px;">
                    <asp:HiddenField ID="HidURL" runat="server" />
                    <table width="100%" align="center" class="table">
                        <tr>
                            <td align="left" width="20%" class="label">
                                Addmission In
                            </td>
                            <td style="width: 195px">
                                <asp:DropDownList ID="ddlBatch" runat="server" Width="192px" CssClass="dropdownList"
                                    AutoPostBack="True" OnSelectedIndexChanged="ddlBatch_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                            <td align="left" width="20%" class="label" valign="top">
                                <%--  <a href="#?w=350" rel="divAddBatch" class="poplight">Add New</a>--%>
                                <%-- <a rel="divAddBatch" class="poplight" onclick="popUp('350','divAddBatch')">
                                    <img src="../Images/newLeft.gif"   />
                                </a>--%>
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
                                <a rel="divBookingStatus" class="poplight" onclick="popUp('350','divBookingStatus')">
                                    Seat Booking Status</a>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" width="20%" class="label">
                                Select Student
                            </td>
                            <td style="width: 195px">
                                <asp:DropDownList ID="ddlStudent" runat="server" Width="192px" CssClass="dropdownList"
                                    AutoPostBack="True" OnSelectedIndexChanged="ddlStudent_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                            <td id="tdStudentDetails" runat="server">
                                <%-- <asp:HyperLink ID="studentDetails" runat="server">View Details</asp:HyperLink>--%>
                                <a id="studentDetails11" runat="server" onclick="return openStudentFormPopUp()">View
                                    Details</a>
                            </td>
                        </tr>
                        <tr id="trCourse" runat="server">
                            <td align="left" width="20%" class="label">
                                Select Stream
                            </td>
                            <td colspan="3" align="left">
                                <asp:RadioButtonList ID="chkStream" runat="server" RepeatDirection="Horizontal" DataValueField="StreamId" DataTextField="stream_name"></asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr id="trBooean" runat="server">
                            <td align="left" width="20%" class="label">
                            </td>
                            <td style="width: 195px">
                                <div>
                                    <asp:CheckBox ID="ChkHostelFacility" runat="server" Text="Hostel Facility" TextAlign="Right" onclick="HostelEnableDisable(this);" />
                                </div>
                                <div>
                                    <asp:CheckBox ID="ChkLateral" runat="server" Text="Lateral Entry" TextAlign="Right" />
                                </div>
                                <div>
                                    <asp:CheckBox ID="ChkTFW" runat="server" Text="TFW" TextAlign="Right" />
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" width="20%" class="label">
                                Select Fees Structure
                            </td>
                            <td style="width: 195px">
                                <asp:DropDownList ID="ddlFees" runat="server" Width="192px" CssClass="dropdownList">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" width="20%" class="label">
                                <%--Hostel Fees Structure--%>
                            </td>
                            <td style="width: 195px;">
                                <asp:DropDownList ID="ddlHostelFees" runat="server" Width="192px" CssClass="dropdownList" DataValueField="id" DataTextField="fees_name" Visible="False"></asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                </div>
                <div style="width: 740px;">
                    <table width="100%" align="center" class="table">
                        <tr>
                            <td align="right">
                                <asp:Button ID="btnStramSave" runat="server" Text="Approve" CssClass="button" OnClientClick="return validation();"
                                    OnClick="btnStramSave_Click" />
                                &nbsp;
                                <asp:Button ID="btnReject" runat="server" CssClass="button" Text="Reject" OnClientClick="return RejectValidation();"
                                    OnClick="btnReject_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
                <h6 align="left" style="color: #00356A;">
                    All Active Student List</h6>
                <div style="width: 740px;">
                    <table width="100%" align="center" class="table">
                        <tr>
                            <td align="left" width="20%" class="label">
                                Select Course
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="ddlCourse" runat="server" AutoPostBack="true" CssClass="dropdownList"
                                    Width="192px" DataValueField="CourseId" DataTextField="CourseName" OnSelectedIndexChanged="ddlCourse_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" width="20%" class="label">
                            Student
                            </td>
                            <td align="left">
                             <asp:ComboBox ID="ddlActiveStudent" runat="server" CssClass="WindowsStyle" AutoPostBack="true"
                                DropDownStyle="DropDown" AutoCompleteMode="SuggestAppend" CaseSensitive="false"
                                Width="340px" DataValueField="id" DataTextField="StudentName" >
                            </asp:ComboBox>
                            
                            &nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnShow" runat="server" CssClass="button" Text="Search" OnClientClick=""
                         OnClick="btnShow_Click" />
                            
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="2">
                                <asp:GridView ID="dgvAllStudent" runat="server" AutoGenerateColumns="false" Width="100%"
                                    AllowPaging="true" PageSize="100" DataKeyNames="id,CourseId" 
                                    OnRowDataBound="dgvAllStudent_RowDataBound" 
                                    onpageindexchanging="dgvAllStudent_PageIndexChanging">
                                    <Columns>
                                         <asp:TemplateField HeaderText="Photo">
                                            <ItemTemplate>
                                                <asp:Image ID="ImgIDCard" runat="server" ImageUrl='<%#Bind("Photo") %>' Width="50px"
                                                    Height="50px" ToolTip="Click For ID Card" />
                                                
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="batch_Name" HeaderText=" Batch Name " />
                                        <asp:BoundField DataField="stream_name" HeaderText=" Stream Name " />
                                        <asp:BoundField DataField="name" HeaderText=" Name " />
                                        <asp:BoundField DataField="student_code" HeaderText=" Code " />
                                        <asp:BoundField DataField="fees_name" HeaderText=" Fees " />
                                        <asp:TemplateField ShowHeader="false">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="btnPrintForm" runat="server" ImageUrl="~/Images/print.gif" Width="17px" Height="17px" ToolTip="Print Application Form" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ShowHeader="false">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="btnPrint" runat="server" ImageUrl="~/Images/print-icon.JPG"
                                                    CommandName="Edit" Width="25px" Height="25px" ToolTip="Print Fees Structure" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <HeaderStyle CssClass="HeaderStyle" />
                                    <RowStyle CssClass="RowStyle" />
                                    <AlternatingRowStyle CssClass="AltRowStyle" />
                                    <PagerStyle CssClass="PagerStyle" />
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <asp:HiddenField ID="hidCourse_id" runat="server" />
            <asp:HiddenField ID="hidCurrent_ID" runat="server" />
            <%----------------------------------------------------------%>
            <div id="divBookingStatus" class="popup_block">
                <div align="center">
                    <table cellpadding="4" cellspacing="0" width="300px" style="font-family: Tahoma;
                        font-size: 12px; color: #747879;">
                        <tr>
                            <td>
                                <div class="title" style="margin: 0 0 10px 0;">
                                    <h4>
                                        <asp:Literal ID="ltrHeader" runat="server" Mode="PassThrough"></asp:Literal>
                                    </h4>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" valign="top">
                                <asp:GridView ID="dgvSeat" runat="server" GridLines="None" Width="97%" AutoGenerateColumns="false">
                                    <Columns>
                                        <asp:BoundField DataField="stream_name" HeaderText="Stream" />
                                        <asp:BoundField DataField="Capacity" HeaderText="Capacity" />
                                        <asp:BoundField DataField="TotalApprove" HeaderText="Approve" />
                                        <asp:BoundField DataField="TotalActive" HeaderText="Active" />
                                    </Columns>
                                    <HeaderStyle CssClass="HeaderStyle" />
                                    <RowStyle CssClass="RowStyle" />
                                    <EmptyDataRowStyle CssClass="EditRowStyle" />
                                    <AlternatingRowStyle CssClass="AltRowStyle" />
                                    <PagerStyle CssClass="PagerStyle" />
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <script src="../Scripts/jquery-1.6.4.min.js" type="text/javascript"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            $('a.close, #fade').live('click', function () { //When clicking on the close or fade layer...
                $('#fade , .popup_block').fadeOut(function () {
                    $('#fade, a.close').remove();
                }); //fade them both out
                return false;
            });
        });

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
