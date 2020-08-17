using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CollegeERP.UserControl
{
    public partial class menu : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Settings
                liSettings.Visible = HttpContext.Current.User.IsInRole(Entity.Common.Utility.SETTINGS);
                liCompanyConfig.Visible = HttpContext.Current.User.IsInRole(Entity.Common.Utility.COMPANY_CONFIG);
                liCreateFnYear.Visible = HttpContext.Current.User.IsInRole(Entity.Common.Utility.CREATE_FINYR);
                liDbBackupRestore.Visible = HttpContext.Current.User.IsInRole(Entity.Common.Utility.DATABASE_BACKUP_RESTORE);
                liLeaveApplicationConfig.Visible = HttpContext.Current.User.IsInRole(Entity.Common.Utility.LEAVE_APPLICATION_CONFIG);
                liChangeCompany.Visible = HttpContext.Current.User.IsInRole(Entity.Common.Utility.CHANGE_COMPANY);
                liStateMaster.Visible = HttpContext.Current.User.IsInRole(Entity.Common.Utility.STATE_MASTER);
                liDistrictMaster.Visible = HttpContext.Current.User.IsInRole(Entity.Common.Utility.DISTRICT_MASTER);
                liCityMaster.Visible = HttpContext.Current.User.IsInRole(Entity.Common.Utility.CITY_MASTER);
                liSchoolMaster.Visible = HttpContext.Current.User.IsInRole(Entity.Common.Utility.SCHOOL_MASTER);

                //Student
                liStudent.Visible = HttpContext.Current.User.IsInRole(Entity.Common.Utility.STUDENT);
                liAllStudent.Visible = HttpContext.Current.User.IsInRole(Entity.Common.Utility.ALL_STUDENT);
                liBtechReg.Visible = HttpContext.Current.User.IsInRole(Entity.Common.Utility.STUDENT_BTECH);
                liMBAReg.Visible = HttpContext.Current.User.IsInRole(Entity.Common.Utility.STUDENT_MBA);
                liMTechReg.Visible = HttpContext.Current.User.IsInRole(Entity.Common.Utility.STUDENT_MTECH);
                liNewStudentAdmissionReport.Visible = HttpContext.Current.User.IsInRole(Entity.Common.Utility.NEW_STUDENT_ADMISSION_REPORT);
                liLibraryFine.Visible = HttpContext.Current.User.IsInRole(Entity.Common.Utility.LIBRARY_FINE);
                liTeacherSubjectMapping.Visible = HttpContext.Current.User.IsInRole(Entity.Common.Utility.TEACHER_SUBJECT_MAPPING);
                liStudentAttendance.Visible = HttpContext.Current.User.IsInRole(Entity.Common.Utility.STUDENT_ATTENDANCE);
                liStudentAttendanceReport.Visible = HttpContext.Current.User.IsInRole(Entity.Common.Utility.STUDENT_ATTENDANCE_REPORT);
                liFeesHeadMaster.Visible = HttpContext.Current.User.IsInRole(Entity.Common.Utility.FEES_HEAD_MASTER);
                liFees.Visible = HttpContext.Current.User.IsInRole(Entity.Common.Utility.FEES);
                liAllFees.Visible = HttpContext.Current.User.IsInRole(Entity.Common.Utility.ALL_FEES);
                liApproveStudent.Visible = HttpContext.Current.User.IsInRole(Entity.Common.Utility.APPROVE_STUDENT);
                liUserBaseApproveRejectReport.Visible = HttpContext.Current.User.IsInRole(Entity.Common.Utility.USER_BASE_APPROVED_STUDENT_LIST);
                liSemFeesGenaration.Visible = HttpContext.Current.User.IsInRole(Entity.Common.Utility.SEM_FEES_GENERATION);
                liStreamMaster.Visible = HttpContext.Current.User.IsInRole(Entity.Common.Utility.STREAM_MASTER);
                liStudentPromotion.Visible = HttpContext.Current.User.IsInRole(Entity.Common.Utility.STUDENT_PROMOTION);
                liHostelFees.Visible = HttpContext.Current.User.IsInRole(Entity.Common.Utility.HOSTEL_FEES_CONFIGURATION);
                liHostelFeesGeneration.Visible = HttpContext.Current.User.IsInRole(Entity.Common.Utility.HOSTEL_BILL_GENERATION);
                liStudentCreditBillEntry.Visible = HttpContext.Current.User.IsInRole(Entity.Common.Utility.STUDENT_CREDIT_BILL_ENTRY);
                liStudentSectionMapping.Visible = HttpContext.Current.User.IsInRole(Entity.Common.Utility.STUDENT_SECTION_MAPPING);
                liStudentSubjectMapping.Visible = HttpContext.Current.User.IsInRole(Entity.Common.Utility.STUDENT_ELECTIVE_SUBJECT_MAPPING);
                liDiplomaReg.Visible = HttpContext.Current.User.IsInRole(Entity.Common.Utility.STUDENT_DIPLOMA);

                //PayRoll
                liPayroll.Visible = HttpContext.Current.User.IsInRole(Entity.Common.Utility.PAYROLL);
                liStatutorySalaryConfig.Visible = HttpContext.Current.User.IsInRole(Entity.Common.Utility.STATUTORY_SALARY_CONFIG);
                liHoliday.Visible = HttpContext.Current.User.IsInRole(Entity.Common.Utility.HOLIDAY_CONFIG);
                liPTaxSlabFixation.Visible = HttpContext.Current.User.IsInRole(Entity.Common.Utility.PTAX_CONFIG);
                liPayband.Visible = HttpContext.Current.User.IsInRole(Entity.Common.Utility.PAYBAND_MASTER);

                liEmployeeSalarySetting.Visible = HttpContext.Current.User.IsInRole(Entity.Common.Utility.EMPLOYEE_SALARY_SETTING);
                liEmployeeAdditionalHeadSetting.Visible = HttpContext.Current.User.IsInRole(Entity.Common.Utility.EMPLOYEE_ADDITIONAL_HEAD_SETTING);
                liEmployeeDeductionSetting.Visible = HttpContext.Current.User.IsInRole(Entity.Common.Utility.EMPLOYEE_DEDUCTION_SETTING);
                liLoan.Visible = HttpContext.Current.User.IsInRole(Entity.Common.Utility.LOAN_SETTING);
                liMonthlySalaryGeneration.Visible = HttpContext.Current.User.IsInRole(Entity.Common.Utility.MONTHLY_SALARY_GENERATION);
                liPaySlip.Visible = HttpContext.Current.User.IsInRole(Entity.Common.Utility.MONTHLY_PAYSLIP_GENERATION);
                liPTaxDetailsReport.Visible = HttpContext.Current.User.IsInRole(Entity.Common.Utility.PTAX_DETAILS_REPORT);
                liPTaxSummeryReport.Visible = HttpContext.Current.User.IsInRole(Entity.Common.Utility.PTAX_SUMMERY_REPORT);
                liAllEmployeesPaySlip.Visible = HttpContext.Current.User.IsInRole(Entity.Common.Utility.ALL_EMPLOYEES_PAYSLIP);
                liIndividualSalaryDetails.Visible = HttpContext.Current.User.IsInRole(Entity.Common.Utility.INDIVIDUAL_SALARY_DETAILS);

                liIncomeTax.Visible = HttpContext.Current.User.IsInRole(Entity.Common.Utility.ITAX);
                liGenarateForm16.Visible = HttpContext.Current.User.IsInRole(Entity.Common.Utility.GENERATE_FORM16);
                liViewForm16.Visible = HttpContext.Current.User.IsInRole(Entity.Common.Utility.VIEW_FORM16);

                liITEmpCntr.Visible = HttpContext.Current.User.IsInRole(Entity.Common.Utility.ITAX_EMPLOYEE_CNTR);
                liInvestmentHead.Visible = HttpContext.Current.User.IsInRole(Entity.Common.Utility.ITAX_INVESTMENT_HEAD);
                liPrevEmplHead.Visible = HttpContext.Current.User.IsInRole(Entity.Common.Utility.ITAX_PREV_EMPLHEAD);
                liSectionMaster.Visible = HttpContext.Current.User.IsInRole(Entity.Common.Utility.ITAX_SECTION_MASTER);

                liPFRegister.Visible = HttpContext.Current.User.IsInRole(Entity.Common.Utility.PF_REGISTER);
                liESIRegister.Visible = HttpContext.Current.User.IsInRole(Entity.Common.Utility.ESI_REGISTER);
                liPTaxRegister.Visible = HttpContext.Current.User.IsInRole(Entity.Common.Utility.PTAX_REGISTER);
                liEmployeePrevEmplDetails.Visible = HttpContext.Current.User.IsInRole(Entity.Common.Utility.ITAX_EMPLOYEE_PREVEMPL_DETAILS);

                //HR
                liHR.Visible = HttpContext.Current.User.IsInRole(Entity.Common.Utility.HR);
                liClaim.Visible = HttpContext.Current.User.IsInRole(Entity.Common.Utility.CLAIM);
                liClaimSetting.Visible = HttpContext.Current.User.IsInRole(Entity.Common.Utility.CLAIM_SETTING);
                liClaimApply.Visible = HttpContext.Current.User.IsInRole(Entity.Common.Utility.CLAIM_APPLY);
                liClaimRequest.Visible = HttpContext.Current.User.IsInRole(Entity.Common.Utility.CLAIM_APPROVE);
                liDirectorClaimApproval.Visible = HttpContext.Current.User.IsInRole(Entity.Common.Utility.CLAIM_APPROVE_BY_DIRECTOR);

                liLeave.Visible = HttpContext.Current.User.IsInRole(Entity.Common.Utility.LEAVE);
                liLeaveApply.Visible = HttpContext.Current.User.IsInRole(Entity.Common.Utility.LEAVE_APPLY);
                liLeaveRequest.Visible = HttpContext.Current.User.IsInRole(Entity.Common.Utility.LEAVE_APPROVE);
                liLeaveSetting.Visible = HttpContext.Current.User.IsInRole(Entity.Common.Utility.LEAVE_SETTING);
                liDirectorLeaveApproval.Visible = HttpContext.Current.User.IsInRole(Entity.Common.Utility.LEAVE_APPROVE_BY_DIRECTOR);

                liEmployeeInformation.Visible = HttpContext.Current.User.IsInRole(Entity.Common.Utility.EMPLOYEE_SETTING);
                liRoleAccessLevel.Visible = HttpContext.Current.User.IsInRole(Entity.Common.Utility.ROLE_ACCESS_LEVEL);
                liImportAttendance.Visible = HttpContext.Current.User.IsInRole(Entity.Common.Utility.IMPORT_EMPLOYEE_ATTENDANCE);
                liLeaveStockUpdate.Visible = HttpContext.Current.User.IsInRole(Entity.Common.Utility.LEAVE_STOCK_UPDATE);

                //ACCOUNTS
                liAccounts.Visible=HttpContext.Current.User.IsInRole(Entity.Common.Utility.ACCOUNTS);
                //liGroupType.Visible = HttpContext.Current.User.IsInRole(Entity.Common.Utility.ACCOUNT_GROUPTYPE);
                liGroupType.Visible = false;

                liGroup.Visible = HttpContext.Current.User.IsInRole(Entity.Common.Utility.ACCOUNT_GROUP);
                liGeneralLedger.Visible = HttpContext.Current.User.IsInRole(Entity.Common.Utility.GENERAL_LEDGER);
                liCostCentre.Visible = HttpContext.Current.User.IsInRole(Entity.Common.Utility.COST_CENTER);
                liBankAccount.Visible = HttpContext.Current.User.IsInRole(Entity.Common.Utility.BANK_ACCOUNT);

                liCashBankVoucherEntry.Visible = HttpContext.Current.User.IsInRole(Entity.Common.Utility.RECEIPT_PAYMENT_VOUCHER);
                liJournalVoucher.Visible = HttpContext.Current.User.IsInRole(Entity.Common.Utility.JOURNAL_VOUCHER);
                liContraVoucher.Visible = HttpContext.Current.User.IsInRole(Entity.Common.Utility.CONTRA_VOUCHER);
                liBankReconsiliation.Visible = HttpContext.Current.User.IsInRole(Entity.Common.Utility.BANK_RECONSILATION);
                //liTrialBalance.Visible = HttpContext.Current.User.IsInRole(Entity.Common.Utility.TRIAL_BALANCE);
                liTrialBalance.Visible = false;
                liStudentFees.Visible = HttpContext.Current.User.IsInRole(Entity.Common.Utility.STUDENT_FEES_COLLECTION);
                liStudentPaymentDetails.Visible = HttpContext.Current.User.IsInRole(Entity.Common.Utility.STUDENT_PAYMENT_DETAILS);
                liStudentOutstandingReport.Visible = HttpContext.Current.User.IsInRole(Entity.Common.Utility.STUDENT_OUTSTANDING_REPORT);
                liExpenseReimbursement.Visible = HttpContext.Current.User.IsInRole(Entity.Common.Utility.EXPENSE_REIMBURSEMENT);
                liBillPayment.Visible = HttpContext.Current.User.IsInRole(Entity.Common.Utility.PURCHASE_BILL_PAYMENT);
                liChangeFinYr.Visible = HttpContext.Current.User.IsInRole(Entity.Common.Utility.CHANGE_FINYR);
                //liRPTGeneralLedger.Visible = HttpContext.Current.User.IsInRole(Entity.Common.Utility.REPORT_GENERAL_LEDGER);
                //liRPTGeneralLedgerBalance.Visible = HttpContext.Current.User.IsInRole(Entity.Common.Utility.REPORT_GENERAL_LEDGER_BALANCE);
                liStudentBillDetails.Visible = HttpContext.Current.User.IsInRole(Entity.Common.Utility.STUDENT_BILL_DETAILS);
                liRPTGeneralLedger.Visible = false;
                liRPTGeneralLedgerBalance.Visible = false;
                liFundTransfer.Visible = false;

                liRPTLedger.Visible = HttpContext.Current.User.IsInRole(Entity.Common.Utility.REPORT_LEDGER);
                liRPTBrs.Visible = HttpContext.Current.User.IsInRole(Entity.Common.Utility.REPORT_BRS);
                liRPTJournalRegister.Visible = HttpContext.Current.User.IsInRole(Entity.Common.Utility.REPORT_JOURNAL_VOUCHER);
                liRPTCashBankVoucher.Visible = HttpContext.Current.User.IsInRole(Entity.Common.Utility.REPORT_RECEIPT_PAYMENT_VOUCHER);
                liRPTUserBaseCashBankVoucher.Visible = HttpContext.Current.User.IsInRole(Entity.Common.Utility.REPORT_USER_BASE_RECEIPT_PAYMENT_VOUCHER);
                liRPTContraVoucher.Visible = HttpContext.Current.User.IsInRole(Entity.Common.Utility.REPORT_CONTRA_VOUCHER);
                liRPTBillPayment.Visible = HttpContext.Current.User.IsInRole(Entity.Common.Utility.REPORT_BILL_PAYMENT);
                liRPTTrailBalance.Visible = HttpContext.Current.User.IsInRole(Entity.Common.Utility.REPORT_TRIAL_BALANCE);
                liBalanceSheet.Visible = HttpContext.Current.User.IsInRole(Entity.Common.Utility.REPORT_BALANCE_SHEET);
                liStudentAdvanceRefund.Visible = HttpContext.Current.User.IsInRole(Entity.Common.Utility.STUDENT_ADVANCE_REFUND);
                liRPTCostCenterWiseReport.Visible = HttpContext.Current.User.IsInRole(Entity.Common.Utility.COST_CENTER_WISE_REPORT);
                liStudentCautionMoneySummaryReport.Visible = HttpContext.Current.User.IsInRole(Entity.Common.Utility.CAUTION_MONEY_SUMMARY_REPORT);
                liStudentCautionMoneyIndividualReport.Visible = HttpContext.Current.User.IsInRole(Entity.Common.Utility.CAUTION_MONEY_INDIVIDUAL_REPORT);
                liStudentConsolidatedOutstandingReport.Visible = HttpContext.Current.User.IsInRole(Entity.Common.Utility.STUDENT_CONSOLIDATED_OUTSTANDING_REPORT);
                liRPTStudentFeesCollection.Visible = HttpContext.Current.User.IsInRole(Entity.Common.Utility.STUDENT_FEES_COLLECTION_REPORT);

                //PURCHASE ORDER
                liPO.Visible = HttpContext.Current.User.IsInRole(Entity.Common.Utility.PURCHASE_ORDER);
                liPOEntry.Visible = HttpContext.Current.User.IsInRole(Entity.Common.Utility.PURCHASE_ORDER_ENTRY);
            }
        }
    }
}