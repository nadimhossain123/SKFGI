<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Form16Print.aspx.cs"
 Inherits="CollegeERP.Payroll.Form16Print" %>

<%@ Import Namespace="Entity" %>
<%@ Import Namespace="BusinessLayer" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="System.Data.SqlClient" %>
<%@ Import Namespace="CollegeERP" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>FORM 16</title>
    <style type="text/css">
        #PRINT
        {
            width: 1000px; /*height:1000px;*/
        }
        #HEAD
        {
            width: 700px;
            height: 75px;
        }
        #BODY1
        {
            width: 800px;
        }
        #BODY2
        {
            width: 800px; /*height:600px;*/
        }
        #BODY3
        {
            width: 800px;
        }
        #BODY4
        {
            width: 800px;
        }
        .DTL
        {
            font-size: 12px;
            font-family: Arial;
        }
        .Heading
        {
            font-size: 12px;
            font-family: Arial;
        }
        .td
        {
            font-size: 12px;
            font-family: Arial;
        }
        </style>
</head>
<body>

<% 
    DataTable dt = new DataTable();
    DataTable dt1 = new DataTable();
    DataTable dtEmp = new DataTable();
    DataTable dtEmpDetail = new DataTable();
    DataTable dtDesig = new DataTable();
    DataTable dtOffc = new DataTable();
    DataTable dtComp = new DataTable();
    DataAccess.Common.Report ObjReport = new DataAccess.Common.Report();

    dt = ObjReport.LoadDataTable("select EmployeeName,Designation,CompanyName,CompanyPanNo,CompanyTanNo,PANNo,Period,AssessmentYear,Section17_1,Section17_2,Section17_3,PTaxAmt,TaxOnTotalIncome,TaxPaid,PreviousIncome,ActRentReceived,ActRentPaid,BasicDa,BasicDaMetro,MedicalReInbursement,ConveyenceAllowence,RetirementLeave,EntAllowance,HBLoanInterest,TDS from Form16ReportMaster where EmployeeId=" + Convert.ToInt32(Request.QueryString["EmployeeId"]) + "");
    dtEmp = ObjReport.LoadDataTable("select EmpCode,CorrespondanceAddresscity,Gender from Employee where EmployeeId=" + Convert.ToInt32(Request.QueryString["EmployeeId"]) + "");
    dtEmpDetail = ObjReport.LoadDataTable("select isnull(MemberName,'') as MemberName from Employeefamily Where EmployeeFamily_Employeeid=" + Convert.ToInt32(Request.QueryString["EmployeeId"]) + " and MemberRelation='father'");
    dtDesig = ObjReport.LoadDataTable("Select isnull(DesignationName,'' ) as DesignationName from Designation Where DesignationId=(select EmployeeOfficial_DesignationId from employeeofficial where EmployeeOfficial_EmployeeId=" + Convert.ToInt32(Request.QueryString["EmployeeId"]) + ")");
    dtOffc = ObjReport.LoadDataTable("select IsNull(PANNo,'') As PANNo From Employeeofficial Where EmployeeOfficial_EmployeeId=" + Convert.ToInt32(Request.QueryString["EmployeeId"]) + "");
    dtComp = ObjReport.LoadDataTable("select isnull(CompanyTanNo,'') as CompanyTanNo,isnull(CompanyPANNo,'') as CompanyPANNo from company Where CompanyId=3");
    %>
    <input id="hdnValue" type="hidden" runat="server" />
   <div id="BODY1">
        <table width="100%" border="1" cellpadding="0" cellspacing="0">
            <tr>
                <td width="33%" style="font-family: Arial; font-size: medium; font-weight: bold"
                    align="center" colspan="4">
                    FORM 16</td>
            </tr>
            <tr>
                <td width="33%" style="font-family: Arial; font-size: smaller"
                    align="center" colspan="4">
                    [See rule 31(1)(a)]</td>
            </tr>
            <tr>
                <td width="33%" style="font-family: Arial; font-size: smaller; font-weight: bold"
                    align="center" colspan="4">
                    PART A</td>
          </tr>
            <tr>
                <td width="33%" style="font-family: Arial; font-size: small"; align="center" colspan="4">
                   <b> Certificate under section 203 of the Income-tax Act, 1961 for Tax deducted at source on Salary </b></td>
            </tr>
            <tr>
                
                    <td  style="font-family: Arial; font-size: small" align="left" colspan="4">
                        <table width="100%">
                        <tr>
                        <td style="width:33%; font-family: Arial; font-size: small">
                            <b>Certificate No.</b>
                        </td>
                        <td style="width:33%; font-family: Arial; font-size: small">
                            <b>Last updated on </b>
                        </td>
                        </tr>
                        </table>
                    </td>
            </tr>
            <tr>
                <td width="33%" style="font-family: Arial; font-size: small" align="center">
                   <b> Name and address of the Employer</b></td>
            
                <td width="33%" style="font-family: Arial; font-size: small" align="center" colspan="3">
                    <b>Name and Designation of the Employee</b></td>
            </tr>
            <tr>
               <td width="33%" class="DTL" align="center">
                    <%=dt.Rows[0]["CompanyName"]%></td>
               
               <td width="33%" class="DTL" align="center" colspan="3">
                    <%=dt.Rows[0]["EmployeeName"]%> <br />
                    <%=dt.Rows[0]["Designation"]%>
                    </td>
            </tr>
            <tr>
               <td width="33%" class="DTL" align="center">
               <table width="100%">
                 <tr>
                    <td class="DTL" align="center"><b>PAN of the Deductor</b></td>
                    <td class="DTL" align="center"><b> TAN of the Deductor </b> </td>
                 </tr>
                 
               </table>
                    </td>
               
               <td width="15%" class="DTL" align="center">
               <b> PAN of the Employee</b>
                  </td>
                       <td width="23%" class="DTL" align="center" colspan="2">
                           </td>
            </tr>
            <tr>
               <td width="33%" class="DTL" align="center">
               <table width="100%">
                <tr>
                 <td class="DTL" align="center"><%=dtComp.Rows[0]["CompanyPanNo"]%></td>
                 <td class="DTL" align="center"><%=dtComp.Rows[0]["CompanyTanNo"]%></td>
                </tr>
               </table>
                    </td>
               
               <td width="15%" class="DTL" align="center">
               <%=dt.Rows[0]["PANNo"]%>
                    </td>
                       <td width="23%" class="DTL" align="center" colspan="2">
                      Employee Code( <%=dtEmp.Rows[0]["EmpCode"]%> )
                           </td>
            </tr>
             <tr>
               <td width="33%" class="DTL" align="center">
                    <b>CIT (TDS)</b></td>
               
               <td width="15%" class="DTL" align="center">
                    <b> Assessment Year </b></td>
                       <td width="23%" class="DTL" align="center" colspan="2">
                             <b> Period with the employer </b> </td>
            </tr>
             <tr>
               <td width="33%" class="DTL" align="center" rowspan="2">
               <table>
                <tr>
                    <td class="DTL" align="center">
                        <b>Address:</b>
                    </td>
                    <td class="DTL" align="left">
                        The Commissioner of Income Tax (TDS) 7th Floor,
                         Middleton Row Kolkata - 700071
                    </td>
                </tr>
               </table>
               
                    </td>
               
               <td width="15%" class="DTL" align="center" rowspan="2">
                    <%=dt.Rows[0]["AssessmentYear"].ToString() + " - " + (Convert.ToInt32(dt.Rows[0]["AssessmentYear"].ToString()) + 1).ToString()%>
                    </td>
                       <td width="12%" class="DTL" align="center">                            
                            <b>From</b></td>
                           <td width="12%" class="DTL" align="center">
                            <b>To</b></td>
            </tr>
            <tr>
                       <td width="12%" class="DTL" align="center">                            
                            01/04/<%=dt.Rows[0]["Period"]%>
                           </td>
                           <td width="12%" class="DTL" align="center">
                            
                              31/03/<%=Convert.ToInt32(dt.Rows[0]["Period"]) + 1%></td>
            </tr>
            </table>
    </div>
     <div id="BODY2">
       <%-- <table width="100%" border="1" cellpadding="0" cellspacing="0">
            
            
            
            <tr>
                <td width="33%" style="font-family: Arial; font-size: small"; align="center" colspan="4">
                    Summary of tax deducted at source</td>
            </tr>
            <tr>
                <td width="15%" style="font-family: Arial; font-size: small" align="center">
                    Quarter</td>
            
                <td width="33%" style="font-family: Arial; font-size: small" align="center" >
                    Receipt Numbers of Original statements of TDS under sub-section(3) of section 
                    200</td>
                    <td width="15%" style="font-family: Arial; font-size: small" align="center" >
                        Amount of tax deducted in respect
                        <br />
                        of the employee</td>
                    <td width="15%" style="font-family: Arial; font-size: small" align="center">
                        Amount of tax deposited/remitted in respect of the employee</td>
            </tr>
            <tr>
                <td width="15%" style="font-family: Arial; font-size: small" align="center">
                     &nbsp;</td>
            
                <td width="33%" style="font-family: Arial; font-size: small" align="center" >
                     &nbsp;</td>
                    <td width="15%" style="font-family: Arial; font-size: small" align="center" >
                        &nbsp;</td>
                    <td width="15%" style="font-family: Arial; font-size: small" align="center">
                         &nbsp;</td>
            </tr>
            
            </table>--%>
    </div>
    <div id="BODY3">
        <table width="100%" border="1" cellpadding="0" cellspacing="0">
            <tr>
                <td width="33%" style="font-family: Arial; font-size: medium; font-weight: bold"
                    align="center" colspan="5">
                    PART B (Annexure)</td>
            </tr>
            <tr>
                <td width="33%" style="font-family: Arial; font-size: smaller;font-weight: bold"
                    align="left" colspan="5">
                   Details of Salary Paid and any other income and tax deducted</td>
            </tr>
            <tr>
                <td width="33%" style="font-family: Arial; font-size: small" colspan="2" >
                    </td>
                    <td width="12%" style="font-family: Arial; font-size: small;font-weight: bold" align="center">
                        <b><del>&#2352;</del></b></td>
                    <td width="12%" style="font-family: Arial; font-size: small;font-weight: bold" align="center">
                        <b><del>&#2352;</del></b></td>
                    <td width="12%" style="font-family: Arial; font-size: small;font-weight: bold" align="center" >
                        <b><del>&#2352;</del></b></td>
          </tr>
           <tr>
                <td width="33%" style="font-family: Arial; font-size: small" align="left" colspan="2">
               <p style="font-size:16px; font-weight:bold "> 1.  Gross Salary</p>
                    </td>
                    <td width="12%" style="font-family: Arial; font-size: small" align="right">
                        </td>
                    <td width="12%" style="font-family: Arial; font-size: small" align="right" >
                        </td>
                    <td width="12%" style="font-family: Arial; font-size: small" align="right" >
                        </td>
          </tr>
           <tr>
                <td width="33%" style="font-family: Arial; font-size: small" align="left" colspan="2">
                    &nbsp;&nbsp;&nbsp;&nbsp;(a) Salary as per provisions contained in sec. 17(1)</td>
                    <td width="12%" style="font-family: Arial; font-size: small" align="right">
                        <%=dt.Rows[0]["Section17_1"]%>
                        </td>
                    <td width="12%" style="font-family: Arial; font-size: small" align="right" >
                     
                        </td>
                    <td width="12%" style="font-family: Arial; font-size: small" align="right" >
                     
                        </td>
          </tr>
          <tr>
                <td width="33%" style="font-family: Arial; font-size: small" align="left" colspan="2">
                    &nbsp;&nbsp;&nbsp;&nbsp;(b) Income from privious Employer / Any Other Income</td>
                    <td width="12%" style="font-family: Arial; font-size: small" align="right">
                        <%=dt.Rows[0]["PreviousIncome"]%>
                        </td>
                    <td width="12%" style="font-family: Arial; font-size: small" align="right" >
                     
                        </td>
                    <td width="12%" style="font-family: Arial; font-size: small" align="right" >
                     
                        </td>
          </tr>
           <tr>
                <td width="33%" style="font-family: Arial; font-size: small" align="left" colspan="2">
                &nbsp;&nbsp;&nbsp;&nbsp;(c) Value of perquisites u/s 17(2) (as per Form No. &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 12BB, wherever applicable)
                    </td>
                    <td width="12%" style="font-family: Arial; font-size: small" align="right">
                    <%=dt.Rows[0]["Section17_2"]%>
                        </td>
                    <td width="12%" style="font-family: Arial; font-size: small" align="right" >
                        </td>
                    <td width="12%" style="font-family: Arial; font-size: small" align="right" >
                        </td>
          </tr>
           <tr>
                <td width="33%" style="font-family: Arial; font-size: small" align="left" colspan="2">
                &nbsp;&nbsp;&nbsp;&nbsp;(d) Profits in lieu of salary under section 17(3)(as per &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Form No. 12BB, wherever 
                    applicable</td>
                    <td width="12%" style="font-family: Arial; font-size: small" align="right">
                    <%=dt.Rows[0]["Section17_3"]%>
                        </td>
                    <td width="12%" style="font-family: Arial; font-size: small" align="right" >
                        </td>
                    <td width="12%" style="font-family: Arial; font-size: small" align="right" >
                        </td>
          </tr>
           <tr>
                <td width="33%" style="font-family: Arial; font-size: small" align="left" colspan="2">
                &nbsp;&nbsp;&nbsp;&nbsp;(A) Total
                    </td>
                    <td width="12%" style="font-family: Arial; font-size: small" align="right">
                        </td>
                    <td width="12%" style="font-family: Arial; font-size: small" align="right" >
                        </td>
                    <td width="12%" style="font-family: Arial; font-size: small" align="right" >
                        <%=Convert.ToDecimal(dt.Rows[0]["Section17_1"]) + Convert.ToDecimal(dt.Rows[0]["Section17_2"]) + Convert.ToDecimal(dt.Rows[0]["Section17_3"]) + Convert.ToDecimal(dt.Rows[0]["PreviousIncome"])  %>
                        </td>
          </tr>
          <tr>
            <td colspan="5">
            &nbsp;
            </td>
          </tr>
          <tr>
                <td width="33%" style="font-family: Arial; font-size: small" align="left" colspan="2">
                <p style="font-size:16px; font-weight:bold ">2. Less: Allowance to the extent exempt U/s 10</p>
                    </td>
                    <td width="12%" style="font-family: Arial; font-size: small" align="right">
                        </td>
                    <td width="12%" style="font-family: Arial; font-size: small" align="right" >
                        </td>
                    <td width="12%" style="font-family: Arial; font-size: small" align="right" >
                        </td>
          </tr>
          <tr>
                <td width="20%" class="DTL" align="center" >
                   <b> Allowance </b></td>
                       <td width="12%" class="DTL" align="center" >
                           <b><del>&#2352;</del></b></td>
                            <td width="12%" style="font-family: Arial; font-size: small" align="right">
                        </td>
                    <td width="12%" style="font-family: Arial; font-size: small" align="right" >
                        </td>
                         <td width="12%" style="font-family: Arial; font-size: small" align="right" >
                        </td>
                   
                    
          </tr>
          <tr>
                <td width="20%" class="DTL" align="left">
                    Actual Rent Received</td>
                       <td width="12%" class="DTL" align="center" >
                       <%=dt.Rows[0]["ActRentReceived"]%>
                       </td>
                       <td width="12%" style="font-family: Arial; font-size: small" align="right">
                       </td>
                       <td width="12%" style="font-family: Arial; font-size: small" align="right" >
                       </td>
                       <td width="12%" style="font-family: Arial; font-size: small" align="right" >
                       </td>                 
          </tr>
          <tr>
                <td width="20%" class="DTL" align="left" >
                    Actual Rent Paid</td>
                       <td width="12%" class="DTL" align="center" >
                       <%=dt.Rows[0]["ActRentPaid"]%>
                       <% if (Convert.ToDecimal(dt.Rows[0]["ActRentReceived"]) < Convert.ToDecimal(dt.Rows[0]["ActRentPaid"])) 
                          {
                              hdnValue.Value = dt.Rows[0]["ActRentReceived"].ToString();
                          }
                          else
                          {
                          hdnValue.Value=dt.Rows[0]["ActRentPaid"].ToString();
                          }
                           %>
                           </td>
                       <td width="12%" style="font-family: Arial; font-size: small" align="right">
                       </td>
                    <td width="12%" style="font-family: Arial; font-size: small" align="right" >
                       </td>
                       <td width="12%" style="font-family: Arial; font-size: small" align="right" >
                       </td>                 
          </tr>
          <tr>
                <td width="20%" class="DTL" align="left" >
                    Rent Paid Excess 10% of Salary</td>
                       <td width="12%" class="DTL" align="center" >
                      <%=Convert.ToDecimal(dt.Rows[0]["ActRentPaid"]) - Convert.ToDecimal(dt.Rows[0]["BasicDa"])%>
                       <% decimal basicda = 0;
                          basicda = Convert.ToDecimal(dt.Rows[0]["ActRentPaid"]) - Convert.ToDecimal(dt.Rows[0]["BasicDa"]);
                           if (Convert.ToDecimal(dt.Rows[0]["ActRentPaid"]) - Convert.ToDecimal(dt.Rows[0]["BasicDa"]) < Convert.ToDecimal(hdnValue.Value)) 
                          {
                              hdnValue.Value = basicda.ToString();
                          }
                          else
                          {
                              hdnValue.Value = hdnValue.Value;
                          }
                           %>
                           </td>
                       <td width="12%" style="font-family: Arial; font-size: small" align="right">
                       </td>
                    <td width="12%" style="font-family: Arial; font-size: small" align="right" >
                       </td>
                       <td width="12%" style="font-family: Arial; font-size: small" align="right" >
                       </td>                 
          </tr>
          <tr>
                <td width="20%" class="DTL" align="left" >
                    40 % & 50% of Salary (Basic + D.A)</td>
                       <td width="12%" class="DTL" align="center" >
                        <%=dt.Rows[0]["BasicDaMetro"]%>
                         <% 

                            if (Convert.ToDecimal(dt.Rows[0]["BasicDaMetro"]) < Convert.ToDecimal(hdnValue.Value)) 
                          {
                              hdnValue.Value = dt.Rows[0]["BasicDaMetro"].ToString();
                          }
                          else
                          {
                              hdnValue.Value = hdnValue.Value;
                          }
                           %>
                           </td>
                       <td width="12%" style="font-family: Arial; font-size: small" align="right">
                      <%= Convert.ToDecimal( hdnValue.Value) %>
                       </td>
                    <td width="12%" style="font-family: Arial; font-size: small" align="right" >
                       </td>
                       <td width="12%" style="font-family: Arial; font-size: small" align="right" >
                     
    
   
                       </td>                 
          </tr>
          <tr>
                <td width="20%" class="DTL" align="left" >
                    Medical Reimbursment [U/s 17(2)]</td>
                       <td width="12%" class="DTL" align="center" >
                       <%=dt.Rows[0]["MedicalReInbursement"]%>
                           </td>
                       <td width="12%" style="font-family: Arial; font-size: small" align="right">
                       </td>
                    <td width="12%" style="font-family: Arial; font-size: small" align="right" >
                       </td>
                       <td width="12%" style="font-family: Arial; font-size: small" align="right" >
                       </td>                 
          </tr>
            <tr>
                <td width="20%" class="DTL" align="left" >
                    Conveyance Allowance</td>
                       <td width="12%" class="DTL" align="center" >
                        <%=dt.Rows[0]["ConveyenceAllowence"]%>
                           </td>
                       <td width="12%" style="font-family: Arial; font-size: small" align="right">
                       </td>
                    <td width="12%" style="font-family: Arial; font-size: small" align="right" >
                       </td>
                       <td width="12%" style="font-family: Arial; font-size: small" align="right" >
                       </td>                 
          </tr>
           <tr>
                <td width="20%" class="DTL" align="left" >
                    Retirement Leave Salary Amount U/s 10(10AA)</td>
                       <td width="12%" class="DTL" align="center" >
                       <%=dt.Rows[0]["RetirementLeave"]%>
                           </td>
                       <td width="12%" style="font-family: Arial; font-size: small" align="right">
                       </td>
                    <td width="12%" style="font-family: Arial; font-size: small" align="right" >
                       </td>
                       <td width="12%" style="font-family: Arial; font-size: small" align="right" >
                                                 <%=Convert.ToDecimal(dt.Rows[0]["ActRentReceived"]) + Convert.ToDecimal(dt.Rows[0]["ActRentPaid"]) + Convert.ToDecimal(Convert.ToDecimal(dt.Rows[0]["ActRentPaid"]) - Convert.ToDecimal(dt.Rows[0]["BasicDa"])) + Convert.ToDecimal(dt.Rows[0]["BasicDaMetro"]) + Convert.ToDecimal(dt.Rows[0]["MedicalReInbursement"]) + Convert.ToDecimal(dt.Rows[0]["ConveyenceAllowence"]) + Convert.ToDecimal(dt.Rows[0]["RetirementLeave"])%>
  
                       </td>                 
          </tr>          
         <%-- <%
              dt1 = ObjReport.LoadDataTable("select ITaxHeadName,ITaxHeadAmt from Form16ReportDetails where ITaxSection ='10' and EmployeeId=" + Convert.ToInt32(Request.QueryString["EmployeeId"]) + "");
              
               %>
          
          <%if (dt1.Rows.Count > 0)
            {
                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    
                
                 %>
          <tr>
                <td width="20%" class="DTL" align="center" >
                    <%=dt1.Rows[0]["ITaxHeadName"]%>
                    </td>
                       <td width="12%" class="DTL" align="center" >
                            <%=dt1.Rows[0]["ITaxHeadAmt"]%></td>
                            <td width="12%" style="font-family: Arial; font-size: small" align="right">
                        </td>
                    <td width="12%" style="font-family: Arial; font-size: small" align="right" >
                        &nbsp;</td>
                         <td width="12%" style="font-family: Arial; font-size: small" align="right" >
                             &nbsp;</td>
                   
                    
          </tr>
          <%} %>
          <%} %>(Close On 05-07-2013)--%>
            <tr>
                <td width="33%" style="font-family: Arial; font-size: small" align="left" colspan="2">
                <p style="font-size:16px; font-weight:bold ">3. Balance (1-2)</p>
                    </td>
                    <td width="12%" style="font-family: Arial; font-size: small" align="right">
                        </td>
                    <td width="12%" style="font-family: Arial; font-size: small" align="right" >
                        </td>
                    <td width="12%" style="font-family: Arial; font-size: small" align="right" >
                         <%= Convert.ToDecimal(Convert.ToDecimal(dt.Rows[0]["Section17_1"]) + Convert.ToDecimal(dt.Rows[0]["Section17_2"]) + Convert.ToDecimal(dt.Rows[0]["Section17_3"]) + Convert.ToDecimal(dt.Rows[0]["PreviousIncome"])) - Convert.ToDecimal(Convert.ToDecimal(dt.Rows[0]["ActRentReceived"]) + Convert.ToDecimal(dt.Rows[0]["ActRentPaid"]) + Convert.ToDecimal(Convert.ToDecimal(dt.Rows[0]["ActRentPaid"]) - Convert.ToDecimal(dt.Rows[0]["BasicDa"])) + Convert.ToDecimal(dt.Rows[0]["BasicDaMetro"]) + Convert.ToDecimal(dt.Rows[0]["MedicalReInbursement"]) + Convert.ToDecimal(dt.Rows[0]["ConveyenceAllowence"]) + Convert.ToDecimal(dt.Rows[0]["RetirementLeave"]))%>

                        </td>
          </tr>
          <tr>
                <td width="33%" style="font-family: Arial; font-size: small" align="left" colspan="2">
               <p style="font-size:16px; font-weight:bold ">4. Deductions</p>
                    </td>
                    <td width="12%" style="font-family: Arial; font-size: small" align="right">
                        </td>
                    <td width="12%" style="font-family: Arial; font-size: small" align="right" >
                        </td>
                    <td width="12%" style="font-family: Arial; font-size: small" align="right" >
                        </td>
          </tr>
           <tr>
                <td width="33%" style="font-family: Arial; font-size: small" align="left" colspan="2">
                    &nbsp;&nbsp;&nbsp;&nbsp;(a) Entertainment allowance</td>
                    <td width="12%" style="font-family: Arial; font-size: small" align="right">
                    <%=dt.Rows[0]["EntAllowance"]%>
                    
                        </td>
                    <td width="12%" style="font-family: Arial; font-size: small" align="right" >
                        </td>
                    <td width="12%" style="font-family: Arial; font-size: small" align="right" >
                        </td>
          </tr>
           
           <tr>
                <td width="33%" style="font-family: Arial; font-size: small" align="left" colspan="2">
                    &nbsp;&nbsp;&nbsp;&nbsp;(b) Tax on employment</td>
                    <td width="12%" style="font-family: Arial; font-size: small" align="right">
                        <%=dt.Rows[0]["PTaxAmt"] %>
                        </td>
                    <td width="12%" style="font-family: Arial; font-size: small" align="right" >
                        </td>
                    <td width="12%" style="font-family: Arial; font-size: small" align="right" >
                        </td>
          </tr>
           <tr>
                <td width="33%" style="font-family: Arial; font-size: small" align="left" colspan="2">
                    &nbsp;&nbsp;&nbsp;&nbsp;(c) H.B.Loan Interest paid</td>
                    <td width="12%" style="font-family: Arial; font-size: small" align="right">
                        
                        <%=dt.Rows[0]["HBLoanInterest"]%>
                        </td>
                    <td width="12%" style="font-family: Arial; font-size: small" align="right" >
                        </td>
                    <td width="12%" style="font-family: Arial; font-size: small" align="right" >
                        </td>
          </tr>
           <tr>
                <td width="33%" style="font-family: Arial; font-size: small" align="left" colspan="2">
                    &nbsp;&nbsp;&nbsp;&nbsp;(d) Convayance </td>
                    <td width="12%" style="font-family: Arial; font-size: small" align="right">
                        <%=dt.Rows[0]["ConveyenceAllowence"]%>
                        </td>
                    <td width="12%" style="font-family: Arial; font-size: small" align="right" >
                        </td>
                    <td width="12%" style="font-family: Arial; font-size: small" align="right" >
                     <%=Convert.ToDecimal(dt.Rows[0]["EntAllowance"]) + Convert.ToDecimal(dt.Rows[0]["PTaxAmt"]) + Convert.ToDecimal(dt.Rows[0]["HBLoanInterest"]) + Convert.ToDecimal(dt.Rows[0]["ConveyenceAllowence"]) %>

                        </td>
          </tr>
         <%-- <tr>
                <td width="33%" style="font-family: Arial; font-size: small" align="left" colspan="2">
               5. Aggregate of 4(a) and (b)
                    </td>
                    <td width="12%" style="font-family: Arial; font-size: small" align="right">
                        </td>
                    <td width="12%" style="font-family: Arial; font-size: small" align="right" >
                        </td>
                    <td width="12%" style="font-family: Arial; font-size: small" align="right" >
                        </td>
          </tr>(Change On 05-07-2013)--%>
          <tr>
                <td width="33%" style="font-family: Arial; font-size: small" align="left" colspan="2">
               <p style="font-size:16px; font-weight:bold ">6. Income chargebale under the head 'Salaries' (3-5)</p>
                    </td>
                    <td width="12%" style="font-family: Arial; font-size: small" align="right">
                        </td>
                    <td width="12%" style="font-family: Arial; font-size: small" align="right" >
                        </td>
                    <td width="12%" style="font-family: Arial; font-size: small" align="right" >
                     <%= Convert.ToDecimal(Convert.ToDecimal(Convert.ToDecimal(dt.Rows[0]["Section17_1"]) + Convert.ToDecimal(dt.Rows[0]["Section17_2"]) + Convert.ToDecimal(dt.Rows[0]["Section17_3"]) + Convert.ToDecimal(dt.Rows[0]["PreviousIncome"])) - Convert.ToDecimal(Convert.ToDecimal(dt.Rows[0]["ActRentReceived"]) + Convert.ToDecimal(dt.Rows[0]["ActRentPaid"]) + Convert.ToDecimal(Convert.ToDecimal(dt.Rows[0]["ActRentPaid"]) - Convert.ToDecimal(dt.Rows[0]["BasicDa"])) + Convert.ToDecimal(dt.Rows[0]["BasicDaMetro"]) + Convert.ToDecimal(dt.Rows[0]["MedicalReInbursement"]) + Convert.ToDecimal(dt.Rows[0]["ConveyenceAllowence"]) + Convert.ToDecimal(dt.Rows[0]["RetirementLeave"]))) - Convert.ToDecimal(Convert.ToDecimal(dt.Rows[0]["EntAllowance"]) + Convert.ToDecimal(dt.Rows[0]["PTaxAmt"]) + Convert.ToDecimal(dt.Rows[0]["HBLoanInterest"]) + Convert.ToDecimal(dt.Rows[0]["ConveyenceAllowence"]))%>
                        </td>
          </tr>
          <%-- <tr>
                <td width="33%" style="font-family: Arial; font-size: small" align="left" colspan="2">
               7. Add: Any other income reported by the employee
                    </td>
                    <td width="12%" style="font-family: Arial; font-size: small" align="right">
                        </td>
                    <td width="12%" style="font-family: Arial; font-size: small" align="right" >
                        </td>
                    <td width="12%" style="font-family: Arial; font-size: small" align="right" >
                        </td>
          </tr>--%>
          <tr>
            <td colspan="5">
            &nbsp;
            </td>
          </tr>
          <tr>
                <td width="33%" style="font-family: Arial; font-size: small" align="left" colspan="2">
               <p style="font-size:16px; font-weight:bold ">7. Add: Any other income reported by the employee</p>
                    </td>
                    <td width="12%" style="font-family: Arial; font-size: small" align="right">
                        </td>
                    <td width="12%" style="font-family: Arial; font-size: small" align="right" >
                        </td>
                    <td width="12%" style="font-family: Arial; font-size: small" align="right" >
                        </td>
          </tr>
            <input id="hdnTaxVal" type="hidden" runat="server" />
         <%
                 
              //Any Other Income 
             hdnDedVal.Value = "0";
             hdnTaxVal.Value = "0";
             decimal ValDed=0; 
             dt1 = ObjReport.LoadDataTable("select ITaxHeadName,ITaxHeadAmt from Form16ReportDetailsAdd where  EmployeeId=" + Convert.ToInt32(Request.QueryString["EmployeeId"]) + "");
              
               %>
          <%if (dt1.Rows.Count > 0)
            {
                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    
                
                 %>
          <tr>
                <td width="20%" class="DTL" align="center" >
                    <%=dt1.Rows[i]["ITaxHeadName"]%>
                    </td>
                       <td width="12%" class="DTL" align="center" >
                            <%=dt1.Rows[i]["ITaxHeadAmt"]%>
                            <%
                                ValDed = ValDed + Convert.ToDecimal(dt1.Rows[i]["ITaxHeadAmt"]);
                            %>
                            </td>
                            <td width="12%" style="font-family: Arial; font-size: small" align="right">
                        </td>
                    <td width="12%" style="font-family: Arial; font-size: small" align="right" >
                        &nbsp;</td>
                         <td width="12%" style="font-family: Arial; font-size: small" align="right" >
                             &nbsp;</td>
                   
                    
          </tr>
          <%} hdnTaxVal.Value = Convert.ToString(ValDed); %>
          <%} %>
          
           <tr>
            <td colspan="5">
            &nbsp;
            </td>
          </tr>
          <tr>
                <td width="33%" style="font-family: Arial; font-size: small" align="left" colspan="2">
              <p style="font-size:16px; font-weight:bold "> 8. Gross Total income (6+7)</p>
                    </td>
                    <td width="12%" style="font-family: Arial; font-size: small" align="right">
                        </td>
                    <td width="12%" style="font-family: Arial; font-size: small" align="right" >
                        </td>
                    <td width="12%" style="font-family: Arial; font-size: small" align="right" >
                     <%= Convert.ToDecimal(Convert.ToDecimal(Convert.ToDecimal(dt.Rows[0]["Section17_1"]) + Convert.ToDecimal(dt.Rows[0]["Section17_2"]) + Convert.ToDecimal(dt.Rows[0]["Section17_3"]) + Convert.ToDecimal(dt.Rows[0]["PreviousIncome"])) - Convert.ToDecimal(Convert.ToDecimal(dt.Rows[0]["ActRentReceived"]) + Convert.ToDecimal(dt.Rows[0]["ActRentPaid"]) + Convert.ToDecimal(Convert.ToDecimal(dt.Rows[0]["ActRentPaid"]) - Convert.ToDecimal(dt.Rows[0]["BasicDa"])) + Convert.ToDecimal(dt.Rows[0]["BasicDaMetro"]) + Convert.ToDecimal(dt.Rows[0]["MedicalReInbursement"]) + Convert.ToDecimal(dt.Rows[0]["ConveyenceAllowence"]) + Convert.ToDecimal(dt.Rows[0]["RetirementLeave"]))) - Convert.ToDecimal(Convert.ToDecimal(dt.Rows[0]["EntAllowance"]) + Convert.ToDecimal(dt.Rows[0]["PTaxAmt"]) + Convert.ToDecimal(dt.Rows[0]["HBLoanInterest"]) + Convert.ToDecimal(dt.Rows[0]["ConveyenceAllowence"])) + Convert.ToDecimal(hdnTaxVal.Value)%>

                        </td>
          </tr>
           <tr>
                <td width="33%" style="font-family: Arial; font-size: small" align="left" colspan="2">
              <p style="font-size:16px; font-weight:bold "> 9. Deductions under Chapter VI A </p>
                    </td>
                    <td width="12%" style="font-family: Arial; font-size: small" align="right">
                        </td>
                    <td width="12%" style="font-family: Arial; font-size: small" align="right" >
                        </td>
                    <td width="12%" style="font-family: Arial; font-size: small" align="right" >
                        </td>
          </tr>
          <tr>
                <td width="33%" style="font-family: Arial; font-size: small" align="left" colspan="2">
                &nbsp;&nbsp;&nbsp;
                <p style="font-size:16px; font-weight:bold ">(A) sections 80C, 80CCC and 80CCD</p>
                    </td>
                    <td width="12%" style="font-family: Arial; font-size: small" align="right">
                        </td>
                    <td width="12%" style="font-family: Arial; font-size: small" align="right" >
                        </td>
                    <td width="12%" style="font-family: Arial; font-size: small" align="right" >
                        </td>
          </tr>
           <tr>
            <td colspan="5">
            &nbsp;
            </td>
          </tr>
          <tr>
              <input id="hdnDedVal" type="hidden" runat="server" />
                <td width="33%" style="font-family: Arial; font-size: small" align="left" colspan="2">
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                 <b>(a) Section 80 C</b>
                    </td>
                    <td width="12%" style="font-family: Arial; font-size: small" align="right">
                       <b> Gross Amount</b></td>
                    <td width="12%" style="font-family: Arial; font-size: small" align="right" >
                       <b> Qualifying Amount</b></td>
                    <td width="12%" style="font-family: Arial; font-size: small" align="right" >
                       <b> Deductible Amount</b></td>
          </tr>
          
           <%
                 
              //Any 80C Deduction
               decimal ValDed3 = Convert.ToDecimal(hdnDedVal.Value);
               dt1 = ObjReport.LoadDataTable("select ITaxHeadName,ITaxHeadAmt from Form16ReportDetails where ITaxSection ='80C' and EmployeeId=" + Convert.ToInt32(Request.QueryString["EmployeeId"]) + "");
              
               %>
          <%if (dt1.Rows.Count > 0)
            {
                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    
                
                 %>
          <tr>
                <td width="20%" class="DTL" align="center" colspan="2">
                    <%=dt1.Rows[0]["ITaxHeadName"]%>
                    </td>
                       
                    <td width="12%" style="font-family: Arial; font-size: small" align="right" >
                       <%=dt1.Rows[0]["ITaxHeadAmt"]%>
                       <%
                    //string val=dt1.Rows[0]["ITaxHeadAmt"].ToString();
                    
                    ValDed3 += Convert.ToDecimal(dt1.Rows[0]["ITaxHeadAmt"].ToString());
                       %>
                       </td>
                         <td width="12%" style="font-family: Arial; font-size: small" align="right" >
                             &nbsp;</td>
                             <td width="12%" style="font-family: Arial; font-size: small" align="right" >
                             &nbsp;</td>
                   
                    
          </tr>
          <%} hdnDedVal.Value = Convert.ToString(ValDed3); %>
          <%} %>
          
           
            <tr>
                <td width="33%" style="font-family: Arial; font-size: small" align="left" colspan="2">
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                 <b>(b) Section 80CCC</b>
                    </td>
                    <td width="12%" style="font-family: Arial; font-size: small" align="right">
                       <b> Gross Amount</b></td>
                    <td width="12%" style="font-family: Arial; font-size: small" align="right" >
                       <b> Qualifying Amount</b></td>
                    <td width="12%" style="font-family: Arial; font-size: small" align="right" >
                       <b> Deductible Amount</b></td>
          </tr>
           <%
                 
              //Any 80CCC Deduction
               decimal ValDed1 = Convert.ToDecimal(hdnDedVal.Value);
               dt1 = ObjReport.LoadDataTable("select ITaxHeadName,ITaxHeadAmt from Form16ReportDetails where ITaxSection ='80CCC' and EmployeeId=" + Convert.ToInt32(Request.QueryString["EmployeeId"]) + "");
              
               %>
          <%if (dt1.Rows.Count > 0)
            {
                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    
                
                 %>
          <tr>
                <td width="20%" class="DTL" align="center" colspan="2">
                    <%=dt1.Rows[0]["ITaxHeadName"]%>
                    </td>
                      
                    <td width="12%" style="font-family: Arial; font-size: small" align="right" >
                       <%=dt1.Rows[0]["ITaxHeadAmt"]%>
                       
                        <%
                    //string val=dt1.Rows[0]["ITaxHeadAmt"].ToString();
                    
                    ValDed1 += Convert.ToDecimal(dt1.Rows[0]["ITaxHeadAmt"].ToString());
                       %>
                       </td>
                         <td width="12%" style="font-family: Arial; font-size: small" align="right" >
                             &nbsp;</td>
                   <td width="12%" style="font-family: Arial; font-size: small" align="right" >
                             &nbsp;</td>
                    
          </tr>
          <%} hdnDedVal.Value = Convert.ToString(ValDed1); %>
          <%} %>
          
           <tr>
                <td width="33%" style="font-family: Arial; font-size: small" align="left" colspan="2">
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <b> (c) Section 80 CCD </b>
                    </td>
                    <td width="12%" style="font-family: Arial; font-size: small" align="right">
                     <b>   Gross Amount</b></td>
                    <td width="12%" style="font-family: Arial; font-size: small" align="right" >
                      <b>  Qualifying Amount</b></td>
                    <td width="12%" style="font-family: Arial; font-size: small" align="right" >
                      <b>  Deductible Amount </b></td>
          </tr>
           <%
                 
              //Any 80CCD Deduction
               decimal ValDed2 = Convert.ToDecimal(hdnDedVal.Value);
               dt1 = ObjReport.LoadDataTable("select ITaxHeadName,ITaxHeadAmt from Form16ReportDetails where ITaxSection ='80CCD' and EmployeeId=" + Convert.ToInt32(Request.QueryString["EmployeeId"]) + "");
              
               %>
          <%if (dt1.Rows.Count > 0)
            {
                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    
                
                 %>
          <tr>
                <td width="20%" class="DTL" align="center" colspan="2">
                    <%=dt1.Rows[0]["ITaxHeadName"]%>
                    </td>
                      
                    <td width="12%" style="font-family: Arial; font-size: small" align="right" >
                       <%=dt1.Rows[0]["ITaxHeadAmt"]%>
                       
                        <%
                    //string val=dt1.Rows[0]["ITaxHeadAmt"].ToString();
                    
                    ValDed2 += Convert.ToDecimal(dt1.Rows[0]["ITaxHeadAmt"].ToString());
                       %>
                       
                       </td>
                         <td width="12%" style="font-family: Arial; font-size: small" align="right" >
                             &nbsp;</td>
                             <td width="12%" style="font-family: Arial; font-size: small" align="right" >
                             &nbsp;</td>
                   
                    
          </tr>
          <%} hdnDedVal.Value = Convert.ToString(ValDed2); %>
          <%} %>
          <tr>
            <td colspan="5">
            &nbsp;
            </td>
          </tr>
         <%-- <tr>
                <td width="33%" style="font-family: Arial; font-size: small; font-style:italic" align="left" colspan="2">
                &nbsp;&nbsp;&nbsp;&nbsp;                
                 Note: 1. Aggregate amount deductible under&nbsp;section 80 C shall not exceed one lakh rupees.
                    </td>
                    <td width="12%" style="font-family: Arial; font-size: small" align="right">
                        &nbsp;</td>
                    <td width="12%" style="font-family: Arial; font-size: small" align="right" >
                        &nbsp;</td>
                    <td width="12%" style="font-family: Arial; font-size: small" align="right" >
                        <%=ValDed %></td>
          </tr>
          <tr>
                <td width="33%" style="font-family: Arial; font-size: small; font-style:italic" align="left" colspan="2">
                &nbsp;&nbsp;&nbsp;&nbsp;                
                  2. Aggregate amount deductible under the three sections, i.e. 80C,80CCC,80CCD shall not exceed one lakh rupees.
                    </td>
                    <td width="12%" style="font-family: Arial; font-size: small" align="right">
                        &nbsp;</td>
                    <td width="12%" style="font-family: Arial; font-size: small" align="right" >
                        &nbsp;</td>
                    <td width="12%" style="font-family: Arial; font-size: small" align="right" >
                        &nbsp;</td>
          </tr>--%>
          <tr>
                <td width="33%" style="font-family: Arial; font-size: small" align="left" colspan="2">
               
                <b>(B) other sections (e.g. 80E,80G etc.) under Chapter VI-A</b>
                    </td>
                   <td width="12%" style="font-family: Arial; font-size: small" align="right">
                       <b> Gross Amount </b></td>
                    <td width="12%" style="font-family: Arial; font-size: small" align="right" >
                       <b> Qualifying Amount </b></td>
                    <td width="12%" style="font-family: Arial; font-size: small" align="right" >
                       <b> Deductible Amount </b></td>
          </tr>
          
          <%
                 
              //Any 80CCD Deduction
               decimal ValDed4 = Convert.ToDecimal(hdnDedVal.Value);
               dt1 = ObjReport.LoadDataTable("select ITaxHeadName,ITaxHeadAmt from Form16ReportDetails where ITaxSection in('80E','80G') and EmployeeId=" + Convert.ToInt32(Request.QueryString["EmployeeId"]) + "");
              
               %>
          <%if (dt1.Rows.Count > 0)
            {
                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    
                
                 %>
          <tr>
                <td width="20%" class="DTL" align="center" colspan="2">
                    <%=dt1.Rows[0]["ITaxHeadName"]%>
                    </td>
                      
                    <td width="12%" style="font-family: Arial; font-size: small" align="right" >
                       <%=dt1.Rows[0]["ITaxHeadAmt"]%>
                       
                        <%
                    //string val=dt1.Rows[0]["ITaxHeadAmt"].ToString();
                    
                    ValDed4 += Convert.ToDecimal(dt1.Rows[0]["ITaxHeadAmt"].ToString());
                       %>
                       
                       </td>
                         <td width="12%" style="font-family: Arial; font-size: small" align="right" >
                             &nbsp;</td>
                             <td width="12%" style="font-family: Arial; font-size: small" align="right" >
                             &nbsp;</td>
                   
                    
          </tr>
          <%} hdnDedVal.Value = Convert.ToString(ValDed4); %>
          <%} %>
          
           <tr>
            <td colspan="5">
            &nbsp;
            </td>            
          </tr>
           <tr>
                <td width="33%" style="font-family: Arial; font-size: small" align="left" colspan="2">
                &nbsp;
                    </td>
                    <td width="12%" style="font-family: Arial; font-size: small" align="right">
                        &nbsp;</td>
                    <td width="12%" style="font-family: Arial; font-size: small" align="right" >
                        &nbsp;</td>
                    <td width="12%" style="font-family: Arial; font-size: small" align="right" >
                        &nbsp;</td>
          </tr>
          <tr>
            <td colspan="5">
            &nbsp;
            </td>            
          </tr>
          <tr>
                <td width="33%" style="font-family: Arial; font-size: small" align="left" colspan="2">
                           
                 <p style="font-size:16px; font-weight:bold "> 10. Aggregate of deductible amount under Chapter VI A</p>
                    </td>
                    <td width="12%" style="font-family: Arial; font-size: small" align="right">
                        &nbsp;</td>
                    <td width="12%" style="font-family: Arial; font-size: small" align="right" >
                        &nbsp;</td>
                    <td width="12%" style="font-family: Arial; font-size: small" align="right" >
                       <%=hdnDedVal.Value  %></td>
          </tr>
           <tr>
            <td colspan="5">
            &nbsp;
            </td>            
          </tr>
          <tr>
                <td width="33%" style="font-family: Arial; font-size: small" align="left" colspan="2">
                      
                 <p style="font-size:16px; font-weight:bold "> 11. Total Income (8-10)</p>
                    </td>
                    <td width="12%" style="font-family: Arial; font-size: small" align="right">
                        &nbsp;</td>
                    <td width="12%" style="font-family: Arial; font-size: small" align="right" >
                        &nbsp;</td>
                    <td width="12%" style="font-family: Arial; font-size: small" align="right" >
                    <input id="hdnTotalIncome" type="hidden" runat="server" /> 
                        <%= Convert.ToDecimal(Convert.ToDecimal(Convert.ToDecimal(dt.Rows[0]["Section17_1"]) + Convert.ToDecimal(dt.Rows[0]["Section17_2"]) + Convert.ToDecimal(dt.Rows[0]["Section17_3"]) + Convert.ToDecimal(dt.Rows[0]["PreviousIncome"])) - Convert.ToDecimal(Convert.ToDecimal(dt.Rows[0]["ActRentReceived"]) + Convert.ToDecimal(dt.Rows[0]["ActRentPaid"]) + Convert.ToDecimal(Convert.ToDecimal(dt.Rows[0]["ActRentPaid"]) - Convert.ToDecimal(dt.Rows[0]["BasicDa"])) + Convert.ToDecimal(dt.Rows[0]["BasicDaMetro"]) + Convert.ToDecimal(dt.Rows[0]["MedicalReInbursement"]) + Convert.ToDecimal(dt.Rows[0]["ConveyenceAllowence"]) + Convert.ToDecimal(dt.Rows[0]["RetirementLeave"]))) - Convert.ToDecimal(Convert.ToDecimal(dt.Rows[0]["EntAllowance"]) + Convert.ToDecimal(dt.Rows[0]["PTaxAmt"]) + Convert.ToDecimal(dt.Rows[0]["HBLoanInterest"]) + Convert.ToDecimal(dt.Rows[0]["ConveyenceAllowence"])) + Convert.ToDecimal(hdnTaxVal.Value) - Convert.ToDecimal(hdnDedVal.Value) %>
                        <% hdnTotalIncome.Value=Convert.ToString( Convert.ToDecimal(Convert.ToDecimal(Convert.ToDecimal(dt.Rows[0]["Section17_1"]) + Convert.ToDecimal(dt.Rows[0]["Section17_2"]) + Convert.ToDecimal(dt.Rows[0]["Section17_3"]) + Convert.ToDecimal(dt.Rows[0]["PreviousIncome"])) - Convert.ToDecimal(Convert.ToDecimal(dt.Rows[0]["ActRentReceived"]) + Convert.ToDecimal(dt.Rows[0]["ActRentPaid"]) + Convert.ToDecimal(Convert.ToDecimal(dt.Rows[0]["ActRentPaid"]) - Convert.ToDecimal(dt.Rows[0]["BasicDa"])) + Convert.ToDecimal(dt.Rows[0]["BasicDaMetro"]) + Convert.ToDecimal(dt.Rows[0]["MedicalReInbursement"]) + Convert.ToDecimal(dt.Rows[0]["ConveyenceAllowence"]) + Convert.ToDecimal(dt.Rows[0]["RetirementLeave"]))) - Convert.ToDecimal(Convert.ToDecimal(dt.Rows[0]["EntAllowance"]) + Convert.ToDecimal(dt.Rows[0]["PTaxAmt"]) + Convert.ToDecimal(dt.Rows[0]["HBLoanInterest"]) + Convert.ToDecimal(dt.Rows[0]["ConveyenceAllowence"])) + Convert.ToDecimal(hdnTaxVal.Value) - Convert.ToDecimal(hdnDedVal.Value)); %>
                        &nbsp;</td>
          </tr>
          <tr>
                <td width="33%" style="font-family: Arial; font-size: small" align="left" colspan="2">
                    <input id="hdnIncomeTax" type="hidden" runat="server" />           
                 <p style="font-size:16px; font-weight:bold "> 12. Tax on total income</p>
                    </td>
                    <td width="12%" style="font-family: Arial; font-size: small" align="right">
                        &nbsp;</td>
                    <td width="12%" style="font-family: Arial; font-size: small" align="right" >
                        &nbsp;</td>
                    <td width="12%" style="font-family: Arial; font-size: small" align="right" >
                       <% 
                           DataTable dtTaxConfig = new DataTable();
                           dtTaxConfig = ObjReport.LoadDataTable("select isnull(itaxconfigper,0) as itaxconfigper from itaxconfiguration where " + Convert.ToDecimal(hdnTotalIncome.Value) + ">=itaxconfigfromamt and " + Convert.ToDecimal(hdnTotalIncome.Value) + "<=itaxconfigtoamt and iTaxConfigKey='" + dtEmp.Rows[0]["Gender"] + "'");
                           
                       %>
                       <% 
                           if (dtTaxConfig.Rows.Count > 0)
                           {%>
                           <%=Convert.ToDecimal(hdnTotalIncome.Value) * Convert.ToDecimal(dtTaxConfig.Rows[0]["itaxconfigper"]) / 100%><%
                           hdnIncomeTax.Value = Convert.ToString(Convert.ToDecimal(hdnTotalIncome.Value) * Convert.ToDecimal(dtTaxConfig.Rows[0]["itaxconfigper"]) / 100);
                           }
                           else { hdnIncomeTax.Value = Convert.ToString("0"); }                                                                                                                
                           %>
                        <%
                           
                             %>
                        
                        
                        
                        </td>
          </tr>
          <tr>
                <td width="33%" style="font-family: Arial; font-size: small" align="left" colspan="2">
                               
                 <p style="font-size:16px; font-weight:bold "> 13. Education cess @ 3% (on tax computed at S.No.12)</p>
                    </td>
                    <td width="12%" style="font-family: Arial; font-size: small" align="right">
                        &nbsp;</td>
                    <td width="12%" style="font-family: Arial; font-size: small" align="right" >
                        &nbsp;</td>
                    <td width="12%" style="font-family: Arial; font-size: small" align="right" >
                        <%=Convert.ToDecimal( hdnIncomeTax.Value ) * 3 /100%></td>
          </tr>
          <tr>
                <td width="33%" style="font-family: Arial; font-size: small" align="left" colspan="2">
                          
                 <p style="font-size:16px; font-weight:bold "> 14. Tax Payable (12+13)</p>
                    </td>
                    <td width="12%" style="font-family: Arial; font-size: small" align="right">
                        &nbsp;</td>
                    <td width="12%" style="font-family: Arial; font-size: small" align="right" >
                        &nbsp;</td>
                    <td width="12%" style="font-family: Arial; font-size: small" align="right" >
                         <%=Convert.ToDecimal(hdnIncomeTax.Value) + Convert.ToDecimal(hdnIncomeTax.Value) * 3 / 100%></td>
          </tr>
          <tr>
                <td width="33%" style="font-family: Arial; font-size: small" align="left" colspan="2">
                                
                 <p style="font-size:16px; font-weight:bold "> 15. Less: Relief under section 89 (attach details)</p>
                    </td>
                    <td width="12%" style="font-family: Arial; font-size: small" align="right">
                        &nbsp;</td>
                    <td width="12%" style="font-family: Arial; font-size: small" align="right" >
                        &nbsp;</td>
                    <td width="12%" style="font-family: Arial; font-size: small" align="right" >
                        <%=dt.Rows[0]["PreviousIncome"]%></td>
          </tr>
          <tr>
                <td width="33%" style="font-family: Arial; font-size: small" align="left" colspan="2">
                                
                  <p style="font-size:16px; font-weight:bold "> 16. Tax Payable (14-15)</p>
                    </td>
                    <td width="12%" style="font-family: Arial; font-size: small" align="right">
                        &nbsp;</td>
                    <td width="12%" style="font-family: Arial; font-size: small" align="right" >
                        &nbsp;</td>
                    <td width="12%" style="font-family: Arial; font-size: small" align="right" >
                       <%=Convert.ToDecimal( hdnIncomeTax.Value) + Convert.ToDecimal(hdnIncomeTax.Value) * 3 / 100 - Convert.ToDecimal(dt.Rows[0]["PreviousIncome"])%></td>
          </tr>
          <tr>
                <td width="33%" style="font-family: Arial; font-size: small" align="left" colspan="2">
                                
                  <p style="font-size:16px; font-weight:bold "> 17. Tax deducted at source u/s 192(1)</p>
                    </td>
                    <td width="12%" style="font-family: Arial; font-size: small" align="right">
                        &nbsp;</td>
                    <td width="12%" style="font-family: Arial; font-size: small" align="right" >
                        &nbsp;</td>
                    <td width="12%" style="font-family: Arial; font-size: small" align="right" >
                    <%=dt.Rows[0]["TDS"]%>
                    </td>
          </tr>
            <tr>
                <td width="33%" style="font-family: Arial; font-size: small" align="left" colspan="2">
                                
                  <p style="font-size:16px; font-weight:bold "> 19.Total Tax paid</p>
                    </td>
                    <td width="12%" style="font-family: Arial; font-size: small" align="right">
                        &nbsp;</td>
                    <td width="12%" style="font-family: Arial; font-size: small" align="right" >
                        &nbsp;</td>
                    <td width="12%" style="font-family: Arial; font-size: small" align="right" >
                     <%=dt.Rows[0]["TDS"]%>
                    </td>
          </tr>
            <tr>
                <td width="33%" style="font-family: Arial; font-size: small" align="left" colspan="2">
                                
                  <p style="font-size:16px; font-weight:bold "> 20. Tax  Payable</p>
                    </td>
                    <td width="12%" style="font-family: Arial; font-size: small" align="right">
                        &nbsp;</td>
                    <td width="12%" style="font-family: Arial; font-size: small" align="right" >
                        &nbsp;</td>
                    <td width="12%" style="font-family: Arial; font-size: small" align="right" >
                     <%=Convert.ToDecimal( hdnIncomeTax.Value) + Convert.ToDecimal(hdnIncomeTax.Value) * 3 / 100 - Convert.ToDecimal(dt.Rows[0]["PreviousIncome"])- Convert.ToDecimal( dt.Rows[0]["TDS"])%>
                    </td>
          </tr>
          <tr>
            <td colspan="5" class="DTL" align="center">
              <p style="font-size:16px; font-weight:bold ">Verification</p>
            </td>            
          </tr>
          <tr>
            <td colspan="5" class="DTL" align="left">
           <p style="word-spacing:4px" align="justify">
           <% 
               DataTable dtInWords = new DataTable();
               dtInWords = ObjReport.LoadDataTable("select dbo.udf_Num_ToWords(" + Convert.ToDecimal(dt.Rows[0]["TDS"]) + ") as AmtInWords");
                %>
           
            I,  KRISHNA CHANDRA MONDAL, son / daughter of <b>SITANGSHU SEKHAR MONDAL</b> working in the capacity of <b>TREASURER </b> (designation) do hereby certify that a sum of Rs.<%=dt.Rows[0]["TDS"]%> [Rs. <b><%=dtInWords.Rows[0]["AmtInWords"]%> (in Words)]</b> has been deducted and deposited to the credit
             of the Central Government. I further certify that the information given above is true, complete and correct and is based on the books of account, documents, TDS statements, TDS deposited and other available records.</p>
            </td>            
          </tr>
          <tr>
                <td width="12%" class="DTL" align="center" >
                  <b> Place </b></td>
                       <td width="20%" class="DTL" align="center" >
                       CHINSURAH
                           </td>
                            <td style="font-family: Arial; font-size: small; width: 24%;" 
                    align="left" colspan="3" rowspan="2">
                        </td>
                   
                    
          </tr>
           <tr>
                <td width="12%" class="DTL" align="center" >
                  <b> Date </b></td>
                       <td width="20%" class="DTL" align="center" >
                       <%=DateTime.Today.ToString("dd/MM/yyyy") %>
                           </td>
                   
                    
          </tr>
          <tr>
                <td width="12%" class="DTL" align="center" >
                    &nbsp;</td>
                       <td width="20%" class="DTL" align="center" >
                           </td>
                            <td style="font-family: Arial; font-size: small; width: 24%;" 
                    align="center" colspan="3">
                               <b> Signature of person responsible for deduction of tax </b></td>
                   
                    
          </tr>
         <tr>
                <td width="12%" class="DTL" align="center" >
                  <b> Designation : </b> </td>
                       <td width="20%" class="DTL" align="center" >
                           TREASURER</td>
                            <td style="font-family: Arial; font-size: small; width: 24%;" 
                    align="center" colspan="3">
                                KRISHNA CHANDRA MONDAL</td>                  
                    
          </tr>
          <tr>
            <td colspan="5"  >
             <b> Pan No: </b> &nbsp;&nbsp; AFCPM4341K
            </td>            
          </tr>
          <%--<tr>
            <td colspan="5">
            &nbsp;
            </td>            
          </tr>
          <tr>
                <td width="12%" class="DTL" align="center" >
                    &nbsp;</td>
                       <td width="33%" class="DTL" align="left" colspan="4">
                           Notes: (Need not be printed. Only for info.)</td>
                                       
                    
          </tr>
          <tr>
                <td width="12%" class="DTL" align="center" >
                    &nbsp;</td>
                       <td width="33%" class="DTL" align="left" colspan="4">
                          1. If an assessee is employed under more than one employer during the year, each of the employers shall issue Part A of the certificate in Form 16 pertaining to the period for which such assessee was employed with each of the employers. Part B may be issued by each of the employers or the last employer at the option of the assessee.</td>
                                
          </tr>
           <tr>
                <td width="12%" class="DTL" align="center" >
                    &nbsp;</td>
                       <td width="33%" class="DTL" align="left" colspan="4">
                         2. Government deductors to enclose Annexure -A if tax is paid without production of an income-tax challan and Annexure-B if tax is paid accompanied by an income-tax challan.</td>
                                
          </tr>
           <tr>
                <td width="12%" class="DTL" align="center" >
                    &nbsp;</td>
                       <td width="33%" class="DTL" align="left" colspan="4">
                          3. Non-Government deductors to enclose Annexure-B.</td>
                                
          </tr>
           <tr>
                <td width="12%" class="DTL" align="center" >
                    &nbsp;</td>
                       <td width="33%" class="DTL" align="left" colspan="4">
                          4. The deductor shall furnish the address of the Commissioner of Income-tax(TDS) having jurisdiction as regards TDS </td>
          </tr>
           <tr>
                <td width="12%" class="DTL" align="center" >
                    &nbsp;</td>
                       <td width="33%" class="DTL" align="left" colspan="4">
                          5. This Form shall be applicable only in respect of tax deducted on or after 1st day of April, 2010.</td>
                                
          </tr>--%>
            
            </table>
    </div>
    <input type="button" value="Print" onclick="this.style.display='none'; window.print(); this.style.display='block';" />
</body>
</html>
