<%@ Page Language="C#" MasterPageFile="~/MasterAdmin.Master" AutoEventWireup="true"
    CodeBehind="RoleAccessLevel.aspx.cs" Inherits="CollegeERP.Common.RoleAccessLevel"
    Title="Role Access Level" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .header
        {
            background-image: url(../Images/headerbg.jpg);
            font-size: 10px;
            color: #333333;
            font-weight: bold;
            padding: 8px 0 8px 0;
            text-align: center;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="Script1" runat="server">
    </asp:ScriptManager>
    <div class="title">
        <h5>
            Role Access Level</h5>
    </div>
    <asp:UpdatePanel ID="UP1" runat="server">
        <ContentTemplate>
            <div style="width: 770px;">
                <table width="100%" align="center" class="table">
                    <tr>
                        <td align="left" width="10%" class="label">
                            Role
                        </td>
                        <td align="left" width="90%">
                            <asp:DropDownList ID="ddlRole" runat="server" AutoPostBack="true" Width="270px" CssClass="dropdownList"
                                DataValueField="RoleId" DataTextField="RoleDescription" OnSelectedIndexChanged="ddlRole_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
                <br />
                <br />
                <table width="100%" align="center">
                    <tr>
                        <td align="center" width="32%" class="header">
                            SETTINGS
                        </td>
                        <td width="2%">
                        </td>
                        <td align="center" width="32%" class="header">
                            PAYROLL
                        </td>
                        <td width="2%">
                        </td>
                        <td align="center" width="32%" class="header">
                            HR
                        </td>
                    </tr>
                    <tr>
                        <td align="center" width="32%" valign="top">
                            <asp:CheckBoxList ID="ChkLstSettings" runat="server" AutoPostBack="True" CssClass="label"
                                OnSelectedIndexChanged="CheckListBox_SelectedIndexChanged">
                                <asp:ListItem Value="100" Text="SETTINGS"></asp:ListItem>
                                <asp:ListItem Value="101" Text="COMPANY CONFIG"></asp:ListItem>
                                <asp:ListItem Value="102" Text="CREATE FIN YEAR"></asp:ListItem>
                                <asp:ListItem Value="103" Text="DATABASE BACKUP"></asp:ListItem>
                                <asp:ListItem Value="104" Text="LEAVE APPLICATION CONFIG"></asp:ListItem>
                                <asp:ListItem Value="105" Text="CHANGE COMPANY"></asp:ListItem>
                                 <asp:ListItem Value="106" Text="STATE MASTER"></asp:ListItem>
                                  <asp:ListItem Value="107" Text="DISTRICT MASTER"></asp:ListItem>
                                  <asp:ListItem Value="108" Text="CITY MASTER"></asp:ListItem>
                                  <asp:ListItem Value="109" Text="SCHOOL MASTER"></asp:ListItem>
                            </asp:CheckBoxList>
                        </td>
                        <td width="2%">
                        </td>
                        <td align="center" width="32%" valign="top">
                            <asp:CheckBoxList ID="ChkLstPayroll" runat="server" AutoPostBack="True" CssClass="label"
                                OnSelectedIndexChanged="CheckListBox_SelectedIndexChanged">
                                <asp:ListItem Value="300" Text="PAYROLL"></asp:ListItem>
                                <asp:ListItem Value="319" Text="STATUTORY SALARY CONFIG"></asp:ListItem>
                                <asp:ListItem Value="320" Text="YEARLY HOLIDAY CONFIG"></asp:ListItem>
                                <asp:ListItem Value="321" Text="P.TAX SLAB CONFIG"></asp:ListItem>                                
                                <asp:ListItem Value="322" Text="PAYBAND MASTER"></asp:ListItem>
                                
                                <asp:ListItem Value="301" Text="EMPLOYEE SALARY CONFIG"></asp:ListItem>
                                <asp:ListItem Value="302" Text="LOAN CONFIG"></asp:ListItem>
                                <asp:ListItem Value="303" Text="MONTHLY SALARY GENERATION"></asp:ListItem>
                                <asp:ListItem Value="304" Text="MONTHLY PAYSLIP GENERATION"></asp:ListItem>
                                <asp:ListItem Value="318" Text="INDIVIDUAL SALARY DETAILS"></asp:ListItem>
                                <asp:ListItem Value="311" Text="PF REGISTER"></asp:ListItem>
                                <asp:ListItem Value="312" Text="ESI REGISTER"></asp:ListItem>
                                <asp:ListItem Value="313" Text="P.TAX REGISTER"></asp:ListItem>
                                <asp:ListItem Value="331" Text="P.TAX DETAILS REPORT"></asp:ListItem>
                                <asp:ListItem Value="332" Text="P.TAX SUMMERY REPORT"></asp:ListItem>
                                <asp:ListItem Value="323" Text="EMPLOYEE DEDUCTION SETTING"></asp:ListItem>
                                <asp:ListItem Value="324" Text="ALL EMPLOYEES PAYSLIP"></asp:ListItem>
                                <asp:ListItem Value="325" Text="EMPLOYEE ADDITIONAL HEAD SETTING"></asp:ListItem>
                                
                                <asp:ListItem Value="326" Text="SALARY HEAD UPDATION"></asp:ListItem>
                                <asp:ListItem Value="327" Text="EMPLOYEE INCREMENT LIST"></asp:ListItem>
                                <asp:ListItem Value="328" Text="EMPLOYEE INCREMENT REPORT"></asp:ListItem>
                                <asp:ListItem Value="329" Text="PTAX CHALLAN"></asp:ListItem>
                                <asp:ListItem Value="330" Text="TDS CHALLAN"></asp:ListItem>
                            </asp:CheckBoxList>
                        </td>
                        <td width="2%">
                        </td>
                        <td align="center" width="32%" valign="top">
                            <asp:CheckBoxList ID="ChkLstHR" runat="server" AutoPostBack="True" CssClass="label"
                                OnSelectedIndexChanged="CheckListBox_SelectedIndexChanged">
                                <asp:ListItem Value="400" Text="HR"></asp:ListItem>
                                <asp:ListItem Value="401" Text="CLAIM"></asp:ListItem>
                                <asp:ListItem Value="402" Text="CLAIM TYPE SETTING"></asp:ListItem>
                                <asp:ListItem Value="403" Text="APPLY CLAIM"></asp:ListItem>
                                <asp:ListItem Value="404" Text="APPROVE CLAIM"></asp:ListItem>
                                <asp:ListItem Value="405" Text="DIRECTOR CLAIM APPROVAL"></asp:ListItem>
                                <asp:ListItem Value="406" Text="LEAVE"></asp:ListItem>
                                <asp:ListItem Value="407" Text="PERMISSIBLE LEAVE SETTING"></asp:ListItem>
                                <asp:ListItem Value="408" Text="APPLY LEAVE"></asp:ListItem>
                                <asp:ListItem Value="409" Text="APPROVE LEAVE"></asp:ListItem>
                                <asp:ListItem Value="410" Text="DIRECTOR LEAVE APPROVAL"></asp:ListItem>
                                <asp:ListItem Value="411" Text="EMPLOYEE INFORMATION"></asp:ListItem>
                                <asp:ListItem Value="412" Text="ROLE ACCESS LEVEL"></asp:ListItem>
                                <asp:ListItem Value="413" Text="IMPORT EMPLOYEE ATTENDANCE"></asp:ListItem>
                                <asp:ListItem Value="414" Text="LEAVE MANAGER CHANGE"></asp:ListItem>
                                 <asp:ListItem Value="415" Text="EMPLOYEE LEAVE REPORT"></asp:ListItem>
                                 <asp:ListItem Value="416" Text="EMPLOYEE LEAVE BALANCE REPORT"></asp:ListItem>
                                 <asp:ListItem Value="417" Text="DIRECT LEAVE ENTRY"></asp:ListItem>
                                 <asp:ListItem Value="418" Text="LEAVE DELETE"></asp:ListItem>
                                 <asp:ListItem Value="419" Text="EMPLOYEE DETAIL"></asp:ListItem>
                                 <asp:ListItem Value="420" Text="EMPLOYEE INDIVIDUAL REPORT"></asp:ListItem>
                                 <asp:ListItem Value="421" Text="LEAVE STOCK UPDATE"></asp:ListItem>
                            </asp:CheckBoxList>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td align="center" width="32%" class="header">
                            STUDENT
                        </td>
                        <td width="2%">
                        </td>
                        <td align="center" width="32%" class="header">
                            INCOME TAX
                        </td>
                        <td width="2%">
                        </td>
                        <td align="center" width="32%" class="header">
                            ACCOUNTS
                        </td>
                    </tr>
                    <tr>
                        <td align="center" width="32%" valign="top">
                            <asp:CheckBoxList ID="ChkLstStudent" runat="server" AutoPostBack="True" CssClass="label"
                                OnSelectedIndexChanged="CheckListBox_SelectedIndexChanged">
                                <asp:ListItem Value="200" Text="STUDENT"></asp:ListItem>
                                <asp:ListItem Value="201" Text="ALL STUDENT"></asp:ListItem>
                                <asp:ListItem Value="202" Text="BTECH REGISTRATION"></asp:ListItem>
                                <asp:ListItem Value="203" Text="MBA REGISTRATION"></asp:ListItem>
                                <asp:ListItem Value="212" Text="MTECH REGISTRATION"></asp:ListItem>
                                <asp:ListItem Value="204" Text="LIBRARY FINE"></asp:ListItem>
                                <asp:ListItem Value="205" Text="TEACHER SUBJECT MAPPING"></asp:ListItem>
                                <asp:ListItem Value="206" Text="STUDENT ATTENDANCE"></asp:ListItem>
                                <asp:ListItem Value="207" Text="STUDENT ATTENDANCE REPORT"></asp:ListItem>
                                <asp:ListItem Value="213" Text="FEES HEAD MASTER"></asp:ListItem>
                                <asp:ListItem Value="208" Text="FEES"></asp:ListItem>
                                <asp:ListItem Value="209" Text="ALL FEES"></asp:ListItem>
                                <asp:ListItem Value="210" Text="APPROVE STUDENT"></asp:ListItem>
                                <asp:ListItem Value="211" Text="SEMESTER FEES GENERATION"></asp:ListItem>
                                <asp:ListItem Value="214" Text="STREAM MASTER"></asp:ListItem>
                                <asp:ListItem Value="215" Text="STUDENT PROMOTION"></asp:ListItem>
                                <asp:ListItem Value="216" Text="NEW STUDENT ADMISSION REPORT"></asp:ListItem>
                                <asp:ListItem Value="217" Text="USER BASE APPROVED STUDENT LIST"></asp:ListItem>
                                <asp:ListItem Value="218" Text="HOSTEL FEES CONFIG"></asp:ListItem>
                                <asp:ListItem Value="219" Text="HOSTEL FEES GENERATION"></asp:ListItem>
                                <asp:ListItem Value="220" Text="STUDENT CREDIT BILL ENTRY"></asp:ListItem>
                                <asp:ListItem Value="221" Text="STUDENT DROP OUT"></asp:ListItem>
                                <asp:ListItem Value="222" Text="APPROVE HOSTEL"></asp:ListItem>
                                <asp:ListItem Value="223" Text="APPROVE HOSTEL LIST"></asp:ListItem>
                                <asp:ListItem Value="224" Text="STUDENT SINGLE BILL ENTRY"></asp:ListItem>
                                <asp:ListItem Value="225" Text="STUDENT SECTION MAPPING"></asp:ListItem>
                                <asp:ListItem Value="226" Text="STUDENT ELECTIVE SUBJECT MAPPING"></asp:ListItem>
                                <asp:ListItem Value="227" Text="ATTENDANCE UPDATE"></asp:ListItem>
                                <asp:ListItem Value="228" Text="LIBRARY_FINE_DELETE_BUTTON"></asp:ListItem>
                                <asp:ListItem Value="229" Text="STUDENT DIPLOMA"></asp:ListItem>
                                <asp:ListItem Value="230" Text="ATTENDANCE DELETE"></asp:ListItem>
                            </asp:CheckBoxList>
                        </td>
                        <td width="2%">
                        </td>
                        <td align="center" width="32%" valign="top">
                            <asp:CheckBoxList ID="ChkLstITax" runat="server" AutoPostBack="True" CssClass="label"
                                OnSelectedIndexChanged="CheckListBox_SelectedIndexChanged">
                                <asp:ListItem Value="305" Text="INCOME TAX"></asp:ListItem>
                                <asp:ListItem Value="306" Text="GENERATE FORM16"></asp:ListItem>
                                <asp:ListItem Value="316" Text="VIEW FORM16"></asp:ListItem>
                                <asp:ListItem Value="317" Text="ALL EMPLOYEES FORM16"></asp:ListItem>
                                
                                <asp:ListItem Value="307" Text="IT EMPLOYEE CONTRIBUTION"></asp:ListItem>
                                <asp:ListItem Value="314" Text="IT PREV EMPL DETAILS"></asp:ListItem>
                                <asp:ListItem Value="315" Text="ALL EMPLOYEES IT CONTRIBUTION"></asp:ListItem>
                                <asp:ListItem Value="308" Text="IT INVESTMENT HEAD"></asp:ListItem>
                                <asp:ListItem Value="309" Text="IT PREV EMP HEAD"></asp:ListItem>
                                <asp:ListItem Value="310" Text="IT SECTION MASTER"></asp:ListItem>
                                
                            </asp:CheckBoxList>
                        </td>
                        <td width="2%">
                        </td>
                        <td align="center" width="32%" valign="top">
                            <asp:CheckBoxList ID="ChkLstAccounts" runat="server" AutoPostBack="True" CssClass="label"
                                OnSelectedIndexChanged="CheckListBox_SelectedIndexChanged">
                                <asp:ListItem Value="500" Text="ACCOUNTS"></asp:ListItem>
                                <%--<asp:ListItem Value="501" Text="ACCOUNTS GROUP TYPE"></asp:ListItem>--%>
                                <asp:ListItem Value="502" Text="ACCOUNTS GROUP"></asp:ListItem>
                                <asp:ListItem Value="503" Text="GENERAL LEDGER"></asp:ListItem>
                                <asp:ListItem Value="504" Text="COST CENTER"></asp:ListItem>
                                <asp:ListItem Value="505" Text="BANK ACCOUNT"></asp:ListItem>
                                <asp:ListItem Value="506" Text="RECEIPT PAYMENT VOUCHER"></asp:ListItem>
                                <asp:ListItem Value="507" Text="JOURNAL VOUCHER"></asp:ListItem>
                                <asp:ListItem Value="508" Text="CONTRA VOUCHER"></asp:ListItem>
                                <asp:ListItem Value="509" Text="BANK RECONSILATION"></asp:ListItem>
                                <%--<asp:ListItem Value="510" Text="TRIAL BALANCE"></asp:ListItem>--%>
                                <asp:ListItem Value="511" Text="STUDENT FEES COLLECTION"></asp:ListItem>
                                <asp:ListItem Value="539" Text="STUDENT BILL DETAILS"></asp:ListItem>
                                <asp:ListItem Value="512" Text="STUDENT PAYMENT DETAILS"></asp:ListItem>
                                <asp:ListItem Value="528" Text="STUDENT ADVANCE REFUND"></asp:ListItem>
                                <asp:ListItem Value="529" Text="STUDENT CAUTION MONEY REFUND"></asp:ListItem>
                                <asp:ListItem Value="530" Text="STUDENT OPENING BALANCE"></asp:ListItem>
                                <asp:ListItem Value="524" Text="STUDENT OUTSTANDING REPORT"></asp:ListItem>
                                <asp:ListItem Value="513" Text="EXPENSE REIMBURSEMENT"></asp:ListItem>
                                <asp:ListItem Value="514" Text="PURCHASE BILL PAYMENT"></asp:ListItem>
                                <asp:ListItem Value="515" Text="CHANGE FIN YEAR/COMPANY"></asp:ListItem>
                                <%--<asp:ListItem Value="516" Text="REPORT GENERAL LEDGER"></asp:ListItem>
                                <asp:ListItem Value="517" Text="REPORT GENERAL LEDGER BALANCE"></asp:ListItem>--%>
                                <asp:ListItem Value="518" Text="REPORT LEDGER"></asp:ListItem>
                                <asp:ListItem Value="525" Text="REPORT BRS"></asp:ListItem>
                                <asp:ListItem Value="519" Text="REPORT JOURNAL VOUCHER"></asp:ListItem>
                                <asp:ListItem Value="520" Text="REPORT RECEIPT PAYMENT VOUCHER"></asp:ListItem>
                                <asp:ListItem Value="527" Text="REPORT USER BASE RECEIPT PAYMENT VOUCHER"></asp:ListItem>
                                <asp:ListItem Value="526" Text="REPORT CONTRA VOUCHER"></asp:ListItem>
                                <asp:ListItem Value="521" Text="REPORT BILL PAYMENT"></asp:ListItem>
                                <asp:ListItem Value="522" Text="REPORT TRIAL BALANCE"></asp:ListItem>
                                <asp:ListItem Value="523" Text="REPORT BALANCE SHEET"></asp:ListItem>
                                <asp:ListItem Value="531" Text="FUND TRANSFER"></asp:ListItem>
                                <asp:ListItem Value="532" Text="STUDENT JOURNAL"></asp:ListItem>
                                <asp:ListItem Value="533" Text="DEBIT NOTE"></asp:ListItem>
                                <asp:ListItem Value="534" Text="COST CENTER WISE REPORT"></asp:ListItem>
                                <asp:ListItem Value="535" Text="CAUTION MONEY SUMMARY REPORT"></asp:ListItem>
                                <asp:ListItem Value="536" Text="CAUTION MONEY INDIVIDUAL REPORT"></asp:ListItem>
                                <asp:ListItem Value="537" Text="STUDENT CONSOLIDATED OUTSTANDING REPORT"></asp:ListItem>
                                <asp:ListItem Value="538" Text="STUDENT FEES COLLECTION REPORT"></asp:ListItem>
                            </asp:CheckBoxList>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4"><br /></td>
                    </tr>
                    <tr>
                        <td align="center" width="32%" class="header">
                            PURCHASE ORDER
                        </td>
                        <td width="2%">
                        </td>
                        <td align="center" width="32%" class="header">
                            ACTIONS ALLOWED
                        </td>
                        <td width="2%">
                        </td>
                        <td align="center" width="32%">
                        </td>
                    </tr>
                    <tr>
                        <td align="center" width="32%" valign="top">
                            <asp:CheckBoxList ID="ChkLstPO" runat="server" AutoPostBack="True" CssClass="label"
                                OnSelectedIndexChanged="CheckListBox_SelectedIndexChanged">
                                <asp:ListItem Value="600" Text="Purchase Order"></asp:ListItem>   
                                <asp:ListItem Value="601" Text="Purchase Order Entry"></asp:ListItem>
                            </asp:CheckBoxList>
                        </td>
                        <td width="2%">
                        </td>
                        <td align="center" width="32%">
                            <asp:CheckBoxList ID="ChkLstAction" runat="server" AutoPostBack="True" CssClass="label"
                                OnSelectedIndexChanged="CheckListBox_SelectedIndexChanged">
                                <asp:ListItem Value="701" Text="Delete Voucher"></asp:ListItem>   
                            </asp:CheckBoxList>
                        </td>
                        <td width="2%">
                        </td>
                        <td align="center" width="32%">
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
