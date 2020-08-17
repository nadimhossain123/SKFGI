<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmpIndividualPrint.aspx.cs"
 Inherits="CollegeERP.HR.EmpIndividualPrint" %>

<%@ Import Namespace="Entity" %>
<%@ Import Namespace="BusinessLayer" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="System.Data.SqlClient" %>
<%@ Import Namespace="CollegeERP" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Employee Individual Report</title>
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
        #BODY5
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
    DataTable dtEmp = new DataTable();
    DataTable dtOff = new DataTable();
    //DataTable dtEmp = new DataTable();
    DataTable dtQuali = new DataTable();
    DataTable dtWork = new DataTable();
    DataTable dtFmly = new DataTable();
    DataTable dtComp = new DataTable();
    DataAccess.Common.Report ObjReport = new DataAccess.Common.Report();

    dtEmp = ObjReport.LoadDataTable("select FirstName + ' ' + MiddleName + ' ' + LastName 'EmpName',EmpCode,convert(varchar(10),DateOfBirth,103) DOB,Gender,MaritalStatus,BloodGroup,Nationality,Cast,Religion,CorrespondanceAddress " +
    ",CorrespondanceAddressCity,CorrespondanceAddressState,CorrespondanceAddressPin,PermanentAddress" +
    ",PermanentAddressCity,PermanentAddressState,PermanentAddressPin,Country,ContactNo1,ContactNo2" +
    ",ContactEmail1,ContactEmail2,PassportNo,PassportPlaceOfIssue,Convert(char(10),PassportIssueDate,103) PassportIssueDate" +
    ",Convert(Char(10),PassportExpiryDate,103) PassportExpiryDate,PayMode,BankName,BankBranchAddress,BankAcNo,BankIFSCode,IsActive" +
    ",Case When IsPermanent='true' Then 'Permanent' Else 'Temporary' End 'JobType' " +
    ",(Select FirstName + ' ' + MiddleName + ' ' + LastName From Employee Where Employeeid=E.LeaveManagerId) 'LeaveManager'" +
	",(Select FirstName + ' ' + MiddleName + ' ' + LastName From Employee Where Employeeid=E.ClaimApproverId) 'ClaimApprover'" +
    "From Employee E where EmployeeId=" + Convert.ToInt32(Request.QueryString["EmployeeId"]) + "");
    //dtEmp = ObjReport.LoadDataTable("select EmpCode,CorrespondanceAddresscity,Gender from Employee where EmployeeId=" + Convert.ToInt32(Request.QueryString["EmployeeId"]) + "");
    dtOff = ObjReport.LoadDataTable("select Convert(char(10),EO.DOJ,103) DOJ,Convert(Char(10),EO.ConfDate,103) 'Confdate'," +
	"Convert(Char(10),EO.EffectiveDate,103) 'Effective Date',EO.PTax,EO.EvaluationType,Convert(Char(10),EO.LastEvaluationDate,103) 'Last Evaluation Date'," +
	"EO.HasPF,Convert(Char(10),EO.PFEffectiveDate,103) 'PF Effective Date',EO.PFNo,EO.HasESI,Convert(Char(10),EO.ESIEffectiveDate,103) 'ESI Effective Date'," +
	"EO.ESINo,EO.HasTDS,Convert(Char(10),EO.TDSEffectiveDate,103) 'TDS Effective Date',EO.PANNo,EO.HasHealthCard," +
	"EO.MediclaimNo,Convert(Char(10),EO.DOR,103) DOR,Convert(Char(10),EO.DOL,103) DOL,EO.NoticePeriod," +
	"EO.ReasonForLeaving,EO.EmployeeType,EO.WorkingDays,EO.FileNo," +
    "(Select DesignationName From Designation Where DesignationId=EO.EmployeeOfficial_DesignationId) 'Designation'," +
	"(Select DepartmentName From Department Where Departmentid=EO.EmployeeOfficial_DepartmentId) 'Department', " +
    "(Select GradePay From Payband Where PayBandId=EO.PayBandId) 'GradePay' " +
    "from EmployeeOfficial EO Where EO.EmployeeOfficial_EmployeeId=" + Convert.ToInt32(Request.QueryString["EmployeeId"]) + " ");
    dtQuali = ObjReport.LoadDataTable("Select  QualificationName,QualificationBoard,QualificationPassingYear,QualificationPercOfMarks,QualificationStream" +
    " from employeequalification where EmployeeQualification_EmployeeId=" + Convert.ToInt32(Request.QueryString["EmployeeId"]) + " ");
    dtWork = ObjReport.LoadDataTable("select CompanyName,WorkPeriod,WorkDesignation,WorkResponsibilities,WorkSalary " +
    "From EmployeeWork Where EmployeeWork_EmployeeId=" + Convert.ToInt32(Request.QueryString["EmployeeId"]) + "");
    dtFmly = ObjReport.LoadDataTable("select MemberName,MemberOccupation,MemberRelation,MemberGender,MemberContactNo " +
    "From EmployeeFamily Where EmployeeFamily_EmployeeId=" + Convert.ToInt32(Request.QueryString["EmployeeId"]) + "");
    %>
    <input id="hdnValue" type="hidden" runat="server" />
   <div id="BODY1">
        <table width="100%" border="1" cellpadding="0" cellspacing="0">
           <tr>
                    <td colspan="4" style="font-family: Arial; font-size:large" align="center">
                    Employee Individual Report
                    </td>
            </tr>
            
            <tr>
                    <td width="25%" style="font-family: Arial; font-size: small" align="left">
                    <b>Employee Name :</b></td>            
                    <td width="25%" style="font-family: Arial; font-size: small" align="left" >
                    <%=dtEmp.Rows[0]["EmpName"]%>
                    </td>
                    <td width="25%" style="font-family: Arial; font-size: small" align="left" >
                    Employee Code
                    </td>
                    <td width="25%" style="font-family: Arial; font-size: small" align="left" >
                    <%=dtEmp.Rows[0]["EmpCode"]%>
                    </td>
            </tr>
            <tr>
                    <td width="25%" style="font-family: Arial; font-size: small" align="left">
                    Date Of Birth :</td>            
                    <td width="25%" style="font-family: Arial; font-size: small" align="left" >
                    <%=dtEmp.Rows[0]["DOB"]%>
                    </td>
                    <td width="25%" style="font-family: Arial; font-size: small" align="left" >
                    Gender :
                    </td>
                    <td width="25%" style="font-family: Arial; font-size: small" align="left" >
                    <%=dtEmp.Rows[0]["Gender"]%>
                    </td> 
            </tr>
            <tr>
                    <td width="25%" style="font-family: Arial; font-size: small" align="left">
                    Marital Status :</td>            
                    <td width="25%" style="font-family: Arial; font-size: small" align="left" >
                    <%=dtEmp.Rows[0]["MaritalStatus"]%>
                    </td>
                    <td width="25%" style="font-family: Arial; font-size: small" align="left" >
                    Blood Group :
                    </td>
                    <td width="25%" style="font-family: Arial; font-size: small" align="left" >
                    <%=dtEmp.Rows[0]["BloodGroup"]%>
                    </td> 
            </tr>
             <tr>
                    <td width="25%" style="font-family: Arial; font-size: small" align="left">
                   Nationality :</td>            
                    <td width="25%" style="font-family: Arial; font-size: small" align="left" >
                    <%=dtEmp.Rows[0]["Nationality"]%>
                    </td>
                    <td width="25%" style="font-family: Arial; font-size: small" align="left" >
                   Cast :
                    </td>
                    <td width="25%" style="font-family: Arial; font-size: small" align="left" >
                    <%=dtEmp.Rows[0]["Cast"]%>
                    </td> 
            </tr>
             <tr>
                    <td width="25%" style="font-family: Arial; font-size: small" align="left">
                   Religion :</td>            
                    <td width="25%" style="font-family: Arial; font-size: small" align="left" >
                    <%=dtEmp.Rows[0]["Religion"]%>
                    </td>
                    <td width="25%" style="font-family: Arial; font-size: small" align="left" >
                   Correspondance Address :
                    </td>
                    <td width="25%" style="font-family: Arial; font-size: small" align="left" >
                    <%=dtEmp.Rows[0]["CorrespondanceAddress"]%>
                    </td> 
            </tr>
             <tr>
                    <td width="25%" style="font-family: Arial; font-size: small" align="left">
                   Cor. Address City :</td>            
                    <td width="25%" style="font-family: Arial; font-size: small" align="left" >
                    <%=dtEmp.Rows[0]["CorrespondanceAddressCity"]%>
                    </td>
                    <td width="25%" style="font-family: Arial; font-size: small" align="left" >
                   Cors. Address State :
                    </td>
                    <td width="25%" style="font-family: Arial; font-size: small" align="left" >
                    <%=dtEmp.Rows[0]["CorrespondanceAddressState"]%>
                    </td> 
            </tr>
             <tr>
                    <td width="25%" style="font-family: Arial; font-size: small" align="left">
                   Cors. Address Pin :</td>            
                    <td width="25%" style="font-family: Arial; font-size: small" align="left" >
                    <%=dtEmp.Rows[0]["CorrespondanceAddressPin"]%>
                    </td>
                    <td width="25%" style="font-family: Arial; font-size: small" align="left" >
                    PermanentAddress :
                    </td>
                    <td width="25%" style="font-family: Arial; font-size: small" align="left" >
                    <%=dtEmp.Rows[0]["PermanentAddress"]%>
                    </td> 
            </tr>
             <tr>
                    <td width="25%" style="font-family: Arial; font-size: small" align="left">
                    Permanent Address City :</td>            
                    <td width="25%" style="font-family: Arial; font-size: small" align="left" >
                    <%=dtEmp.Rows[0]["PermanentAddressCity"]%>
                    </td>
                    <td width="25%" style="font-family: Arial; font-size: small" align="left" >
                    Permanent Address State :
                    </td>
                    <td width="25%" style="font-family: Arial; font-size: small" align="left" >
                    <%=dtEmp.Rows[0]["PermanentAddressState"]%>
                    </td> 
            </tr>
            <tr>
                    <td width="25%" style="font-family: Arial; font-size: small" align="left">
                    Permanent Address Pin :</td>            
                    <td width="25%" style="font-family: Arial; font-size: small" align="left" >
                    <%=dtEmp.Rows[0]["PermanentAddressPin"]%>
                    </td>
                    <td width="25%" style="font-family: Arial; font-size: small" align="left" >
                    Country :
                    </td>
                    <td width="25%" style="font-family: Arial; font-size: small" align="left" >
                    <%=dtEmp.Rows[0]["Country"]%>
                    </td> 
            </tr>
            <tr>
                    <td width="25%" style="font-family: Arial; font-size: small" align="left">
                    Contact No1 :</td>            
                    <td width="25%" style="font-family: Arial; font-size: small" align="left" >
                    <%=dtEmp.Rows[0]["ContactNo1"]%>
                    </td>
                    <td width="25%" style="font-family: Arial; font-size: small" align="left" >
                    Contact No2 :
                    </td>
                    <td width="25%" style="font-family: Arial; font-size: small" align="left" >
                    <%=dtEmp.Rows[0]["ContactNo2"]%>
                    </td> 
            </tr>
            <tr>
                    <td width="25%" style="font-family: Arial; font-size: small" align="left">
                    Contact Email1 :</td>            
                    <td width="25%" style="font-family: Arial; font-size: small" align="left" >
                    <%=dtEmp.Rows[0]["ContactEmail1"]%>
                    </td>
                    <td width="25%" style="font-family: Arial; font-size: small" align="left" >
                    Contact Email2 :
                    </td>
                    <td width="25%" style="font-family: Arial; font-size: small" align="left" >
                    <%=dtEmp.Rows[0]["ContactEmail2"]%>
                    </td> 
            </tr>
             <tr>
                    <td width="25%" style="font-family: Arial; font-size: small" align="left">
                    Passport No :</td>            
                    <td width="25%" style="font-family: Arial; font-size: small" align="left" >
                    <%=dtEmp.Rows[0]["PassportNo"]%>
                    </td>
                    <td width="25%" style="font-family: Arial; font-size: small" align="left" >
                    Passport Place Of Issue :
                    </td>
                    <td width="25%" style="font-family: Arial; font-size: small" align="left" >
                    <%=dtEmp.Rows[0]["PassportPlaceOfIssue"]%>
                    </td> 
            </tr> 
              <tr>
                    <td width="25%" style="font-family: Arial; font-size: small" align="left">
                    Passport Issue Date :</td>            
                    <td width="25%" style="font-family: Arial; font-size: small" align="left" >
                    <%=dtEmp.Rows[0]["PassportIssueDate"]%>
                    </td>
                    <td width="25%" style="font-family: Arial; font-size: small" align="left" >
                    Passport Expiry Date :
                    </td>
                    <td width="25%" style="font-family: Arial; font-size: small" align="left" >
                    <%=dtEmp.Rows[0]["PassportExpiryDate"]%>
                    </td> 
            </tr>
              <tr>
                    <td width="25%" style="font-family: Arial; font-size: small" align="left">
                    Pay Mode :</td>            
                    <td width="25%" style="font-family: Arial; font-size: small" align="left" >
                    <%=dtEmp.Rows[0]["PayMode"]%>
                    </td>
                    <td width="25%" style="font-family: Arial; font-size: small" align="left" >
                    Bank Name :
                    </td>
                    <td width="25%" style="font-family: Arial; font-size: small" align="left" >
                    <%=dtEmp.Rows[0]["BankName"]%>
                    </td> 
            </tr>
             <tr>
                    <td width="25%" style="font-family: Arial; font-size: small" align="left">
                    Bank Branch Address :</td>            
                    <td width="25%" style="font-family: Arial; font-size: small" align="left" >
                    <%=dtEmp.Rows[0]["BankBranchAddress"]%>
                    </td>
                    <td width="25%" style="font-family: Arial; font-size: small" align="left" >
                    BankAcNo :
                    </td>
                    <td width="25%" style="font-family: Arial; font-size: small" align="left" >
                    <%=dtEmp.Rows[0]["BankAcNo"]%>
                    </td> 
            </tr>
              <tr>
                    <td width="25%" style="font-family: Arial; font-size: small" align="left">
                    BankIFSCode :</td>            
                    <td width="25%" style="font-family: Arial; font-size: small" align="left" >
                    <%=dtEmp.Rows[0]["BankIFSCode"]%>
                    </td>
                    <td width="25%" style="font-family: Arial; font-size: small" align="left" >
                    Job Type :
                    </td>
                    <td width="25%" style="font-family: Arial; font-size: small" align="left" >
                    <%=dtEmp.Rows[0]["JobType"]%>
                    </td> 
            </tr>
              <tr>
                    <td width="25%" style="font-family: Arial; font-size: small" align="left">
                    Leave Manager :</td>            
                    <td width="25%" style="font-family: Arial; font-size: small" align="left" >
                    <%=dtEmp.Rows[0]["LeaveManager"]%>
                    </td>
                    <td width="25%" style="font-family: Arial; font-size: small" align="left" >
                    Claim Approver :
                    </td>
                    <td width="25%" style="font-family: Arial; font-size: small" align="left" >
                    <%=dtEmp.Rows[0]["ClaimApprover"]%>
                    </td> 
            </tr>
            </table>
    </div>
     <div id="BODY2">
         <table width="100%" border="1" cellpadding="0" cellspacing="0">
            <tr>
                    <td colspan="4" style="font-family: Arial; font-size:large" align="left">
                    Official Detail
                    </td>
            </tr>
            <tr>
                    <td width="25%" style="font-family: Arial; font-size: small" align="left">
                    Department :</td>            
                    <td width="25%" style="font-family: Arial; font-size: small" align="left" >
                    <%=dtOff.Rows[0]["Department"]%>
                    </td>
                    <td width="25%" style="font-family: Arial; font-size: small" align="left" >
                    Designation :
                    </td>
                    <td width="25%" style="font-family: Arial; font-size: small" align="left" >
                    <%=dtOff.Rows[0]["Designation"]%>
                    </td> 
            </tr>
            <tr>
                    <td width="25%" style="font-family: Arial; font-size: small" align="left">
                    Joining Date :</td>            
                    <td width="25%" style="font-family: Arial; font-size: small" align="left" >
                    <%=dtOff.Rows[0]["DOJ"]%>
                    </td>
                    <td width="25%" style="font-family: Arial; font-size: small" align="left" >
                    Conformation Date :
                    </td>
                    <td width="25%" style="font-family: Arial; font-size: small" align="left" >
                    <%=dtOff.Rows[0]["Confdate"]%>
                    </td> 
            </tr>
            <tr>
                    <td width="25%" style="font-family: Arial; font-size: small" align="left">
                    Effective Date :</td>            
                    <td width="25%" style="font-family: Arial; font-size: small" align="left" >
                    <%=dtOff.Rows[0]["Effective Date"]%>
                    </td>
                    <td width="25%" style="font-family: Arial; font-size: small" align="left" >
                    P Tax :
                    </td>
                    <td width="25%" style="font-family: Arial; font-size: small" align="left" >
                    <%=dtOff.Rows[0]["PTax"]%>
                    </td> 
            </tr>
           <tr>
                    <td width="25%" style="font-family: Arial; font-size: small" align="left">
                    Evaluation Type :</td>            
                    <td width="25%" style="font-family: Arial; font-size: small" align="left" >
                    <%=dtOff.Rows[0]["EvaluationType"]%>
                    </td>
                    <td width="25%" style="font-family: Arial; font-size: small" align="left" >
                    Last Evaluation Date :
                    </td>
                    <td width="25%" style="font-family: Arial; font-size: small" align="left" >
                    <%=dtOff.Rows[0]["Last Evaluation Date"]%>
                    </td> 
            </tr>
            <tr>
                    <td width="25%" style="font-family: Arial; font-size: small" align="left">
                    Has PF :</td>            
                    <td width="25%" style="font-family: Arial; font-size: small" align="left" >
                    <%=dtOff.Rows[0]["HasPF"]%>
                    </td>
                    <td width="25%" style="font-family: Arial; font-size: small" align="left" >
                    PF Effective Date :
                    </td>
                    <td width="25%" style="font-family: Arial; font-size: small" align="left" >
                    <%=dtOff.Rows[0]["PF Effective Date"]%>
                    </td> 
            </tr>
              <tr>
                    <td width="25%" style="font-family: Arial; font-size: small" align="left">
                  PF No :</td>            
                    <td width="25%" style="font-family: Arial; font-size: small" align="left" >
                    <%=dtOff.Rows[0]["PFNo"]%>
                    </td>
                    <td width="25%" style="font-family: Arial; font-size: small" align="left" >
                    HasESI :
                    </td>
                    <td width="25%" style="font-family: Arial; font-size: small" align="left" >
                    <%=dtOff.Rows[0]["HasESI"]%>
                    </td> 
            </tr>
              <tr>
                    <td width="25%" style="font-family: Arial; font-size: small" align="left">
                    ESI Effective Date :</td>            
                    <td width="25%" style="font-family: Arial; font-size: small" align="left" >
                    <%=dtOff.Rows[0]["ESI Effective Date"]%>
                    </td>
                    <td width="25%" style="font-family: Arial; font-size: small" align="left" >
                    ESI No :
                    </td>
                    <td width="25%" style="font-family: Arial; font-size: small" align="left" >
                    <%=dtOff.Rows[0]["ESINo"]%>
                    </td> 
            </tr>
           <tr>
                    <td width="25%" style="font-family: Arial; font-size: small" align="left">
                    Has TDS :</td>            
                    <td width="25%" style="font-family: Arial; font-size: small" align="left" >
                    <%=dtOff.Rows[0]["HasTDS"]%>
                    </td>
                    <td width="25%" style="font-family: Arial; font-size: small" align="left" >
                    TDS Effective Date :
                    </td>
                    <td width="25%" style="font-family: Arial; font-size: small" align="left" >
                    <%=dtOff.Rows[0]["TDS Effective Date"]%>
                    </td> 
            </tr>
           <tr>
                    <td width="25%" style="font-family: Arial; font-size: small" align="left">
                    PAN No :</td>            
                    <td width="25%" style="font-family: Arial; font-size: small" align="left" >
                    <%=dtOff.Rows[0]["PANNo"]%>
                    </td>
                    <td width="25%" style="font-family: Arial; font-size: small" align="left" >
                    Has Health Card :
                    </td>
                    <td width="25%" style="font-family: Arial; font-size: small" align="left" >
                    <%=dtOff.Rows[0]["HasHealthCard"]%>
                    </td> 
            </tr>
            <tr>
                    <td width="25%" style="font-family: Arial; font-size: small" align="left">
                    Mediclaim No :</td>            
                    <td width="25%" style="font-family: Arial; font-size: small" align="left" >
                    <%=dtOff.Rows[0]["MediclaimNo"]%>
                    </td>
                    <td width="25%" style="font-family: Arial; font-size: small" align="left" >
                    DOR :
                    </td>
                    <td width="25%" style="font-family: Arial; font-size: small" align="left" >
                    <%=dtOff.Rows[0]["DOR"]%>
                    </td> 
            </tr>
            <tr>
                    <td width="25%" style="font-family: Arial; font-size: small" align="left">
                    DOL :</td>            
                    <td width="25%" style="font-family: Arial; font-size: small" align="left" >
                    <%=dtOff.Rows[0]["DOL"]%>
                    </td>
                    <td width="25%" style="font-family: Arial; font-size: small" align="left" >
                    Notice Period :
                    </td>
                    <td width="25%" style="font-family: Arial; font-size: small" align="left" >
                    <%=dtOff.Rows[0]["NoticePeriod"]%>
                    </td> 
            </tr>
            <tr>
                    <td width="25%" style="font-family: Arial; font-size: small" align="left">
                    Reason For Leaving :</td>            
                    <td width="25%" style="font-family: Arial; font-size: small" align="left" >
                    <%=dtOff.Rows[0]["ReasonForLeaving"]%>
                    </td>
                    <td width="25%" style="font-family: Arial; font-size: small" align="left" >
                    Employee Type :
                    </td>
                    <td width="25%" style="font-family: Arial; font-size: small" align="left" >
                    <%=dtOff.Rows[0]["EmployeeType"]%>
                    </td> 
            </tr>
            <tr>
                    <td width="25%" style="font-family: Arial; font-size: small" align="left">
                    Working Days :</td>            
                    <td width="25%" style="font-family: Arial; font-size: small" align="left" >
                    <%=dtOff.Rows[0]["WorkingDays"]%>
                    </td>
                    <td width="25%" style="font-family: Arial; font-size: small" align="left" >
                    File No :
                    </td>
                    <td width="25%" style="font-family: Arial; font-size: small" align="left" >
                    <%=dtOff.Rows[0]["FileNo"]%>
                    </td> 
            </tr>
              <tr>
                    <td width="25%" style="font-family: Arial; font-size: small" align="left">
                    Grade Pay :</td>            
                    <td width="25%" style="font-family: Arial; font-size: small" align="left" >
                    <%=dtOff.Rows[0]["GradePay"]%>
                    </td>
                    <td width="25%" style="font-family: Arial; font-size: small" align="left" >
                   
                    </td>
                    <td width="25%" style="font-family: Arial; font-size: small" align="left" >
                    
                    </td> 
            </tr>
          
          
            
            </table>
    </div>
    <div id="BODY3">
        <table width="100%" border="1" cellpadding="0" cellspacing="0">
        <%
        
            if (dtQuali.Rows.Count > 0)
            {      
        %>
            <tr>
                    <td colspan="4" style="font-family: Arial; font-size:large" align="left">
                    Qualification Detail
                    </td>
            </tr>
            <tr>
                    <td width="20%" style="font-family: Arial; font-size: small" align="left">
                    <b>Qualification Name</b></td>            
                    <td width="20%" style="font-family: Arial; font-size: small" align="left" >
                    <b>Board / Univ.</b>
                    </td>
                    <td width="20%" style="font-family: Arial; font-size: small" align="left" >
                    <b>Passing Year</b>
                    </td>
                    <td width="20%" style="font-family: Arial; font-size: small" align="left" >
                    <b>Perc Of Mark</b>
                    </td>
                    <td width="20%" style="font-family: Arial; font-size: small" align="left" >
                    <b>Qualification</b>
                    </td> 
            </tr>
         <%
            for (int i = 0; i < dtQuali.Rows.Count ; i++)
            {           
             
         %> 
         
          <tr>
                    <td width="20%" style="font-family: Arial; font-size: small" align="left">
                    <%=dtQuali.Rows[i]["QualificationName"]%></td>            
                    <td width="20%" style="font-family: Arial; font-size: small" align="left" >
                    <%=dtQuali.Rows[i]["QualificationBoard"]%>
                    </td>
                    <td width="20%" style="font-family: Arial; font-size: small" align="left" >
                    <%=dtQuali.Rows[i]["QualificationPassingYear"]%>
                    </td>
                    <td width="20%" style="font-family: Arial; font-size: small" align="left" >
                    <%=dtQuali.Rows[i]["QualificationPercOfMarks"]%>
                    </td>
                    <td width="20%" style="font-family: Arial; font-size: small" align="left" >
                    <%=dtQuali.Rows[i]["QualificationStream"]%>
                    </td>           
          </tr>
           
         <%
            }
            }    
         %> 
          
            
        </table>
    </div>
    <div id="BODY4">
        <table width="100%" border="1" cellpadding="0" cellspacing="0">
        <%
            
            if (dtWork.Rows.Count > 0)
            {      
        %>
            <tr>
                    <td colspan="4" style="font-family: Arial; font-size:large" align="left">
                    Work Experience Detail
                    </td>
            </tr>
            <tr>
                    <td width="20%" style="font-family: Arial; font-size: small" align="left">
                    <b>Company Name</b></td>            
                    <td width="20%" style="font-family: Arial; font-size: small" align="left" >
                    <b>Work Period</b>
                    </td>
                    <td width="20%" style="font-family: Arial; font-size: small" align="left" >
                    <b>Work Designation</b>
                    </td>
                    <td width="20%" style="font-family: Arial; font-size: small" align="left" >
                    <b>Work Responsibilities</b>
                    </td>
                    <td width="20%" style="font-family: Arial; font-size: small" align="left" >
                    <b>Work Salary</b>
                    </td> 
            </tr>
         <%
             for (int j = 0; j < dtWork.Rows.Count ; j++)
            {           
             
         %> 
         
          <tr>
                    <td width="20%" style="font-family: Arial; font-size: small" align="left">
                    <%=dtWork.Rows[j]["CompanyName"]%></td>            
                    <td width="20%" style="font-family: Arial; font-size: small" align="left" >
                    <%=dtWork.Rows[j]["WorkPeriod"]%>
                    </td>
                    <td width="20%" style="font-family: Arial; font-size: small" align="left" >
                    <%=dtWork.Rows[j]["WorkDesignation"]%>
                    </td>
                    <td width="20%" style="font-family: Arial; font-size: small" align="left" >
                    <%=dtWork.Rows[j]["WorkResponsibilities"]%>
                    </td>
                    <td width="20%" style="font-family: Arial; font-size: small" align="left" >
                    <%=dtWork.Rows[j]["WorkSalary"]%>
                    </td>           
          </tr>
           
         <%
            }
            }    
         %> 
          
            
        </table>
    </div>
     <div id="BODY5">
        <table width="100%" border="1" cellpadding="0" cellspacing="0">
        <%
            
            if (dtFmly.Rows.Count > 0)
            {      
        %>
            <tr>
                    <td colspan="4" style="font-family: Arial; font-size:large" align="left">
                    Family Detail
                    </td>
            </tr>
            <tr>
                    <td width="20%" style="font-family: Arial; font-size: small" align="left">
                    <b>MemberName</b></td>            
                    <td width="20%" style="font-family: Arial; font-size: small" align="left" >
                    <b>Member Occupation</b>
                    </td>
                    <td width="20%" style="font-family: Arial; font-size: small" align="left" >
                    <b>Member Relation</b>
                    </td>
                    <td width="20%" style="font-family: Arial; font-size: small" align="left" >
                    <b>Member Gender</b>
                    </td>
                    <td width="20%" style="font-family: Arial; font-size: small" align="left" >
                    <b>Member ContactNo</b>
                    </td> 
            </tr>
         <%
             for (int j = 0; j < dtFmly.Rows.Count; j++)
            {           
             
         %> 
         
          <tr>
                    <td width="20%" style="font-family: Arial; font-size: small" align="left">
                    <%=dtFmly.Rows[j]["MemberName"]%></td>            
                    <td width="20%" style="font-family: Arial; font-size: small" align="left" >
                    <%=dtFmly.Rows[j]["MemberOccupation"]%>
                    </td>
                    <td width="20%" style="font-family: Arial; font-size: small" align="left" >
                    <%=dtFmly.Rows[j]["MemberRelation"]%>
                    </td>
                    <td width="20%" style="font-family: Arial; font-size: small" align="left" >
                    <%=dtFmly.Rows[j]["MemberGender"]%>
                    </td>
                    <td width="20%" style="font-family: Arial; font-size: small" align="left" >
                    <%=dtFmly.Rows[j]["MemberContactNo"]%>
                    </td>           
          </tr>
           
         <%
            }
            }    
         %> 
          
            
        </table>
    </div>
    <input type="button" value="Print" onclick="this.style.display='none'; window.print(); this.style.display='block';" />
</body>
</html>
