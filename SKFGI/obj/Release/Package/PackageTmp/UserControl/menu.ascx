<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="menu.ascx.cs" Inherits="CollegeERP.UserControl.menu" %>
<ul id="quick">
    <li id="liSettings" runat="server"><a href="" title="Settings"><span class="icon">
        <img src="/Images/cog.png" alt="Settings" /></span><span>Settings</span></a>
        <ul>
            <li id="liCompanyConfig" runat="server"><a href="/Common/CollegeMaster.aspx">Company
                Configuration</a></li>
            <li id="liCreateFnYear" runat="server" class="last"><a href="/Accounts/CreatefinYr.aspx">Create Financial Year</a></li>
            <li id="liDbBackupRestore" runat="server"><a href="/Common/DbBackupRestore.aspx">Database Backup</a></li>
            <li id="liLeaveApplicationConfig" runat="server"><a href="/HR/LeaveApplicationConfig.aspx">Leave Application Config
            </a></li>
            <li id="liChangeCompany" runat="server"><a href="../Accounts/ChangeCompany.aspx">Change Company
            </a></li>
            <li id="liStateMaster" runat="server"><a href="../Common/StateMaster.aspx">State Master
            </a></li>
            <li id="liDistrictMaster" runat="server"><a href="../Common/DistrictMaster.aspx">District Master
            </a></li>
            <li id="liCityMaster" runat="server"><a href="../Common/CityMaster.aspx">City Master
            </a></li>
            <li id="liSchoolMaster" runat="server"><a href="../Common/SchoolMaster.aspx">School Master
            </a></li>
        </ul>
    </li>
    <li id="liStudent" runat="server"><a href="" title="Student"><span class="icon">
        <img src="/Images/Student.jpg" alt="Student" /></span><span>Student</span></a>
        <ul>
            <li id="liAllStudent" runat="server"><a href="/Student/allStudent.aspx">All Student</a></li>
            <li id="liDiplomaReg" runat="server"><a href="/Student/DiplomaRegistration.aspx">Diploma Registration</a></li>
            <li id="liBtechReg" runat="server"><a href="/Student/BTechRegistration.aspx">B.Tech
                Registration</a></li>
            <li id="liMBAReg" runat="server"><a href="/Student/MBARegistration.aspx">Management
                Registration</a></li>
            <li id="liMTechReg" runat="server"><a href="/Student/MTechRegistration.aspx">M.Tech
                Registration</a></li>
            <li id="liNonAICTE" runat="server"><a href="/Student/NonAICTERegistration.aspx">Non AICTE Registration</a></li>
            <li id="liNewStudentAdmissionReport" runat="server"><a href="/Student/NewAdmissionReport.aspx">New Student Admission Report</a></li>
            <li id="liFeesHeadMaster" runat="server"><a href="/Student/FeesHeadMaster.aspx">Fees
                Head Master</a></li>
            <li id="liFees" runat="server"><a href="/Student/Fees.aspx">Fees</a></li>
            <li id="liAllFees" runat="server"><a href="/Student/AllFees.aspx">All Fees</a></li>
            <li id="liHostelFees" runat="server"><a href="/Student/HostelFees.aspx">Hostel Fees</a></li>
            <li id="liApproveStudent" runat="server"><a href="/Student/ApproveStudent.aspx">Approve
                Student</a></li>
            <li id="liUserBaseApproveRejectReport" runat="server"><a href="/Student/UserBaseApprovedStudentList.aspx">Approved/Rejected Student List</a></li>
            <li id="li2" runat="server"><a href="/Student/ApproveHostel.aspx">Approved/Rejected
                Hostel List</a></li>
            <li id="li4" runat="server"><a href="/Student/ApproveHostelList.aspx">Approved/Rejected
                Hostel Report</a></li>
            <li id="liLibraryFine" runat="server"><a href="/Student/LibraryFine.aspx">Library Fine</a></li>
            <li id="liTeacherSubjectMapping" runat="server"><a href="/Student/TeacherSubjectMapping.aspx">Teacher Subject Mapping</a></li>
            <li id="liStudentAttendance" runat="server"><a href="/Student/Attendance.aspx">Student
                Attendance</a></li>
            <li id="liStudentAttendanceReport" runat="server"><a href="/Student/AttendanceReport.aspx">Student Attendance Report</a></li>
            <li id="liSemFeesGenaration" runat="server"><a href="/Student/SemFeesGeneration.aspx">Semester Fees Generation</a></li>
            <li id="liHostelFeesGeneration" runat="server"><a href="/Student/HostelFeesGeneration.aspx">Hostel Fees Generation </a></li>
            <li id="liStudentCreditBillEntry" runat="server"><a href="/Student/StudentCreditBillEntry.aspx">Student Credit Bill Entry</a></li>
            <li id="liStreamMaster" runat="server"><a href="/Common/StreamMaster.aspx">Stream Master</a></li>
            <li id="liStudentPromotion" runat="server"><a href="/Student/StudentPromotion.aspx">Student Promotion</a></li>
            <li id="liStudentDropOut" runat="server"><a href="/Student/StudentDropout.aspx">Student
                DropOut</a></li>
            <li id="liStudentBillEntry" runat="server"><a href="/Student/StudentBillEntry.aspx">Student Single Bill Entry</a></li>
            <li id="liStudentSectionMapping" runat="server"><a href="/Student/StudentSectionMapping.aspx">Student Section Mapping</a></li>
            <li id="liStudentSubjectMapping" runat="server" class="last"><a href="/Student/StudentSubjectMapping.aspx">Student Subject Mapping</a></li>
        </ul>
    </li>
    <li id="liPayroll" runat="server"><a href="" title="Payroll"><span class="icon">
        <img src="/Images/Payroll.jpg" alt="Payroll" /></span><span>Payroll</span></a>
        <ul>
            <li id="liStatutorySalaryConfig" runat="server"><a href="/Common/StatutorySalaryConfig.aspx ">Statutory Salary Fixation</a></li>
            <li id="liHoliday" runat="server"><a href="/Common/HolidayList.aspx">Yearly Holiday
                Fixation</a></li>
            <li id="liPTaxSlabFixation" runat="server"><a href="/Common/PTax.aspx">P.Tax Slab Fixation</a></li>
            <li id="liPayband" runat="server"><a href="/Common/PaybandMaster.aspx">Payband Master</a></li>
            <li id="liEmployeeSalarySetting" runat="server"><a href="/Payroll/EmployeeSalary.aspx">Employee Salary Setting</a></li>
            <li id="liEmployeeAdditionalHeadSetting" runat="server"><a href="/Payroll/EmployeeAdditionalHeadSetting.aspx">Salary Additional Head Setting</a></li>
            <li id="liEmployeeDeductionSetting" runat="server"><a href="/Payroll/EmployeeDeductionSetting.aspx">Employee Deduction Setting</a></li>
            <li id="liLoan" runat="server"><a href="/Payroll/LoanEntryDetails.aspx">Loan/Advance
                Setting</a></li>
            <li id="liMonthlySalaryGeneration" runat="server"><a href="/Payroll/MonthlySalaryGeneration.aspx">Monthly Salary Generation</a></li>
            <li id="liPaySlip" runat="server"><a href="/Payroll/PaySlip.aspx">Monthly Pay Slip</a></li>
            <li id="liAllEmployeesPaySlip" runat="server"><a href="/Payroll/AllEmployeesPaySlip.aspx">All Employees Pay Slip</a></li>
            <li id="liIndividualSalaryDetails" runat="server"><a href="/Payroll/IndividualSalaryDeatails.aspx">Individual Salary Details</a></li>
            <li id="liPFRegister" runat="server"><a href="/Payroll/MonthlyPFRegister.aspx">PF Register</a></li>
            <li id="liESIRegister" runat="server"><a href="/Payroll/MonthlyESIRegister.aspx">ESI
                Register</a></li>
            <li id="liPTaxRegister" runat="server"><a href="/Payroll/MonthlyPTaxRegister.aspx">P.Tax
                Register</a></li>
            <li id="liPTaxDetailsReport" runat="server"><a href="/Payroll/PTaxDetailsReport.aspx">P.Tax Details Report</a></li>
            <li id="liPTaxSummeryReport" runat="server"><a href="/Payroll/PTaxSummeryReport.aspx">P.Tax Summery Report</a></li>
            <li id="liSalaryHeadUpdation" runat="server"><a href="/Payroll/SalaryHeadUpdation.aspx">Salary Head Modification</a></li>
            <li id="liEmployeeIncrementList" runat="server"><a href="/Payroll/EmployeeIncrementList.aspx">Increment List</a></li>
            <li id="liIncrementReport" runat="server"><a href="/Payroll/EmployeeIncrementReport.aspx">Increment Report</a></li>
            <li id="liPTaxChallan" runat="server"><a href="/Payroll/PtaxChallan.aspx">P Tax Challan</a></li>
            <li id="liTDSChallan" runat="server"><a href="/Payroll/TdsChallan.aspx">TDS Challan</a></li>
            <li id="liIncomeTax" runat="server"><a href="#" class="childs">Income Tax</a>
                <ul>
                    <li id="liGenarateForm16" runat="server"><a href="/Payroll/Form16Generate.aspx">Generate
                        Form 16</a></li>
                    <li id="liViewForm16" runat="server"><a href="/Payroll/Form16.aspx">View Form 16</a></li>
                    <li id="liITEmpCntr" runat="server"><a href="/Payroll/ITaxEmployeeContribution.aspx">IT Employee Cntr</a></li>
                    <li id="liInvestmentHead" runat="server"><a href="/Payroll/ITaxInvestmentHeads.aspx">Investment Head</a></li>
                    <li id="liPrevEmplHead" runat="server"><a href="/Payroll/ITaxPrevEmplHeads.aspx">Prev
                        Empl Head</a></li>
                    <li id="liEmployeePrevEmplDetails" runat="server"><a href="/Payroll/ITaxEmployeePrevEmplDetails.aspx">Employee Prev Empl Details</a></li>
                    <li id="liSectionMaster" runat="server" class="last"><a href="/Payroll/ITaxSectionMaster.aspx">Section Master</a></li>
                </ul>
            </li>
        </ul>
    </li>
    <li id="liHR" runat="server"><a href="" title="HR"><span class="icon">
        <img src="/Images/HR.jpg" alt="HR" /></span><span>HR</span></a>
        <ul>
            <li id="liEmployeeInformation" runat="server"><a href="/Common/AddEditEmployee.aspx">Employee Information</a></li>
            <li id="liEmployeeDetail" runat="server"><a href="/HR/EmployeeDetail.aspx">Employee
                Detail</a></li>
            <li id="liEmpIndividualReport" runat="server"><a href="/HR/EmployeeIndividualReport.aspx">Employee Individual Report</a></li>
            <li id="liRoleAccessLevel" runat="server"><a href="/Common/RoleAccessLevel.aspx">Role
                Access Level</a></li>
            <li id="liImportAttendance" runat="server"><a href="/Common/EmployeeAttendanceNew.aspx">Import Employee Attendance</a></li>
            <li id="liClaim" runat="server"><a href="#" class="childs">Claim</a>
                <ul>
                    <li id="liClaimSetting" runat="server"><a href="/HR/ExpenseType.aspx">Expense Type Setting</a></li>
                    <li id="liClaimApply" runat="server"><a href="/HR/ApplyClaim.aspx">Claim Apply</a></li>
                    <li id="liClaimRequest" runat="server"><a href="/HR/ClaimList.aspx">Claim Requests</a></li>
                    <li class="last" id="liDirectorClaimApproval" runat="server"><a href="/HR/DirectorClaimApproval.aspx">Claim approved By management authority</a></li>
                </ul>
            </li>
            <li id="liLeave" runat="server"><a href="#" class="childs">Leave</a>
                <ul>
                    <li id="liLeaveSetting" runat="server"><a href="/HR/LeaveSetting.aspx">Permissible Leave
                        Setting</a></li>
                    <li id="liLeaveStockUpdate" runat="server"><a href="/HR/LeaveStockUpdate.aspx">Leave Stock Update</a></li>
                    <li id="liLeaveApply" runat="server"><a href="/HR/ApplyLeaves.aspx">Leave Apply</a></li>
                    <li id="liDirectLeaveEntry" runat="server"><a href="/HR/DirectLeaveEntry.aspx">Direct
                        Leave Entry</a></li>
                    <li id="liLeaveRequest" runat="server"><a href="/HR/LeaveList.aspx">Leave Requests</a></li>
                    <li id="liLeaveManagerChange" runat="server"><a href="/HR/LeaveManagerChange.aspx">Leave
                        Manager Change</a></li>
                    <li class="last" id="liDirectorLeaveApproval" runat="server"><a href="/HR/DirectorLeaveApproval.aspx">Director Leave Approval</a></li>
                    <li id="li5" runat="server"><a href="/HR/LeaveDeleteList.aspx">Leave Delete</a></li>
                    <li id="liLeaveReport" runat="server"><a href="/HR/LeaveReport.aspx">Leave Report</a></li>
                    <li id="liLeaveBalanceReport" runat="server"><a href="/HR/LeaveReportBalance.aspx">Leave
                        Balance Report</a></li>
                </ul>
            </li>
        </ul>
    </li>
    <li id="liAccounts" runat="server"><a href="" title="Accounts"><span class="icon">
        <img src="/Images/Ruppees.jpg" alt="Accounts" /></span><span>Accounts</span></a>
        <ul>
            <li id="liAccountsMaster" runat="server"><a href="#" class="childs">Masters</a>
                <ul>
                    <li id="liGroupType" runat="server"><a href="/Accounts/AccountGroupType.aspx">Accounts
                        Group Type</a></li>
                    <li id="liGroup" runat="server"><a href="/Accounts/AccountsGroup.aspx">Accounts Group</a></li>
                    <li id="liGeneralLedger" runat="server"><a href="/Accounts/GeneralLedger.aspx">General
                        Ledger</a></li>
                    <li id="liCostCentre" runat="server"><a href="/Accounts/CostCentre.aspx">Cost Centre</a></li>
                    <li id="liBankAccount" runat="server" class="last"><a href="/Accounts/BankAccount.aspx">Bank Account</a></li>
                </ul>
            </li>
            <li id="liCashBankVoucherEntry" runat="server"><a href="/Accounts/CashBankVoucher.aspx">Receipt/Payment Voucher</a></li>
            <li id="liJournalVoucher" runat="server"><a href="/Accounts/JournalVoucher.aspx">Journal
                Voucher</a></li>
            <li id="liStudentJournal" runat="server"><a href="/Accounts/StudentJournalTran.aspx">Student Journal</a></li>
            <li id="liContraVoucher" runat="server"><a href="/Accounts/ContraVoucher.aspx">Contra
                Voucher</a></li>
            <li id="liBankReconsiliation" runat="server"><a href="/Accounts/BankReconsiliation.aspx">Bank Reconsiliation </a></li>
            <li id="liPurchaseOrder" runat="server" visible="false"><a href="/Accounts/RawMaterialPurchaseOrder.aspx">Material Purchase Order </a></li>
            <li id="liTrialBalance" runat="server"><a href="/Accounts/TrialBalance.aspx">Trial Balance
            </a></li>
            <li id="liStudentFees" runat="server"><a href="/Accounts/StudentFeesCollection.aspx">Student Fees Collection</a></li>
            <li id="liStudentBillDetails" runat="server"><a href="/Accounts/StudentBillDetails.aspx">Student Bill Details</a></li>
            <li id="liStudentPaymentDetails" runat="server"><a href="/Accounts/StudentPaymentDetails.aspx">Student Payment Details</a></li>
            <li id="liStudentAdvanceRefund" runat="server"><a href="/Accounts/StudentAdvanceRefund.aspx">Student Advance Refund</a></li>
            <li id="liCautionMoneyRefund" runat="server"><a href="/Accounts/StudentCautionMoneyRefund.aspx">Student Caution Money Refund</a></li>
            <li id="li3" runat="server"><a href="/Accounts/StudentOpeningBalance.aspx">Student Opening
                Balance</a></li>
            <li id="liFundTransfer" runat="server"><a href="/Accounts/FundTransfer.aspx">Fund Transfer</a></li>
            <li id="liExpenseReimbursement" runat="server"><a href="/Accounts/ExpenseReimbursement.aspx">Expense Reimbursement</a></li>
            <li id="liBillPayment" runat="server"><a href="/Accounts/PurchaseBillPayment.aspx">Purchase
                Bill Payment</a></li>
            <li id="liChangeFinYr" runat="server"><a href="/Accounts/ChangeFinYr.aspx">Change Fin
                Year / Company</a></li>
            <li id="liDebitNote" runat="server"><a href="/Accounts/DebitNote.aspx">Debit Note</a></li>
            <li id="liPLTransfer" runat="server"><a href="/Accounts/PLTransfer.aspx">P/L Transfer</a></li>
            <li id="liReport" runat="server"><a href="#" class="childs">Reports</a>
                <ul>
                    <li id="liRPTGeneralLedger" runat="server"><a href="/Accounts/RPTGeneralLedger.aspx">General Ledger</a></li>
                    <li id="liRPTGeneralLedgerBalance" runat="server"><a href="/Accounts/RPTGeneralLedgerBalance.aspx">General Ledger Balance</a></li>
                    <li id="liRPTLedger" runat="server"><a href="/Accounts/RPTLedger.aspx">Ledger Report</a></li>
                    <li id="liRPTBrs" runat="server"><a href="/Accounts/RPTBrs.aspx">BRS Report</a></li>
                    <li id="liRPTJournalRegister" runat="server"><a href="/Accounts/RPTJournalRegister.aspx">Journal Report</a></li>
                    <li id="liRPTCashBankVoucher" runat="server"><a href="/Accounts/RPTCashBankVoucher.aspx">Receipt/Payment Report</a></li>
                    <li id="liRPTUserBaseCashBankVoucher" runat="server"><a href="/Accounts/RPTUserBaseCashBankVoucher.aspx">Day End Report</a></li>
                    <li id="liRPTContraVoucher" runat="server"><a href="/Accounts/RPTContraVoucher.aspx">Contra Report</a></li>
                    <li id="liStudentOutstandingReport" runat="server"><a href="/Accounts/StudentOutstandingReport.aspx">Student Ledger Report</a></li>
                    <li id="liStudentConsolidatedOutstandingReport" runat="server"><a href="/Accounts/StudentConsolidatedOutstandingReport.aspx">Student Consolidated Ledger Report
                    </a></li>
                    <li id="liStudentCautionMoneySummaryReport" runat="server"><a href="/Accounts/StudentCautionMoneySummaryReport.aspx">Caution Money Summary Report</a></li>
                    <li id="liStudentCautionMoneyIndividualReport" runat="server"><a href="/Accounts/StudentCautionMoneyIndividualReport.aspx">Caution Money Individual Report</a></li>
                    <li id="liRPTBillPayment" runat="server"><a href="/Accounts/RPTPurchaseBillPayment.aspx">Bill Payment Report</a></li>
                    <li id="liRPTTrailBalance" runat="server"><a href="/Accounts/RPTTrailBalance.aspx">Trial
                        Balance</a></li>
                    <li id="liBalanceSheet" runat="server"><a href="/Accounts/RPTProfitLoss.aspx">P/L &
                        Balance Sheet</a></li>
                    <li id="liRPTCostCenterWiseReport" runat="server"><a href="/Accounts/RPTCostCenterWiseReport.aspx">Cost Center Wise Report</a></li>
                    <li id="liRPTStudentFeesCollection" runat="server"><a href="../Accounts/RptStudentFeesCollection.aspx">Student Fees Collection Report</a></li>
                    <li id="li" runat="server"><a href="../Accounts/RTPReceiptPaymentAccount.aspx">Receipt Payment Account Report</a></li>
                </ul>
            </li>
        </ul>
    </li>
    <li id="liPO" runat="server"><a href="" title="Purchase Order"><span class="icon">
        <img src="/Images/PO.jpg" alt="Purchase Order" /></span><span>Purchase</span></a>
        <ul>
            <li id="liPOEntry" runat="server" class="last"><a href="/PurchaseOrder/PurchaseOrderEntry.aspx">Purchase Bill Entry</a></li>
        </ul>
    </li>
</ul>
