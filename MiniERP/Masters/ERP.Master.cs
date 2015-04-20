using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DocumentObjectModel;
using BusinessAccessLayer;
using System.Web.Security;

namespace MiniERP.Masters
{
    public partial class ERP : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
            if (!IsPostBack)
            {
                LoginName1.FormatString = LoggedInUser.FullName;
                if (LoggedInUser.Role().Equals(AuthorityLevelType.Admin.ToString()))
                {
                    //Quotation
                    liQuotation.Visible = true;

                    //Contractor Quotation
                    liContractorQuotation.Visible = true;
                    liCQuotationApproval.Visible = true;
                    liViewContractorQuotation.Visible = true;

                    //Supplier Quotation
                    liSupplierQuotation.Visible = true;
                    liSQuotationApproval.Visible = true;
                    liViewSupplierQuotation.Visible = true;

                    //Purchase
                    liPurchase.Visible = true;

                    //Contractor Work Order
                    liViewContractorWorkOrder.Visible = true;

                    //Supplier Purchase Order
                    liViewSupplierPurchaseOrder.Visible = true;

                    //Contrator Invoice
                    liContractorInvoice.Visible = true;
                    liCInvoiceApproval.Visible = true;
                    liViewContratorInvoice.Visible = true; ;
                    
                    //Supplier Invoice
                    liSupplierInvoice.Visible = true;
                    liSInvoiceApproval.Visible = true;
                    liViewSupplierInvoice.Visible = true;
                    
                    liMaterialConsumptionNote.Visible = true;
                    liMaterialReconcilationReport.Visible = true;

                    //Contractor Payment
                    liContractorPayment.Visible = true;
                    liCPaymentApproval.Visible = true;
                    liViewContractorPayment.Visible = true;
                    
                    //Supplier Payment
                    liSupplierPayment.Visible = true;
                    liSPaymentApproval.Visible = true;
                    liViewSupplierPayment.Visible = true;
                    
                    //Company work order
                    liCompany.Visible = true;
                    liCompanyWorkOrderApproval.Visible = true;
                                        
                    //Quality
                    liQuality.Visible = true;
                    
                    //Reports
                    liERPReports.Visible = true;
                    
                    //Administrator
                    liAdministrator.Visible = true;
                }
                else if (LoggedInUser.Role().Equals(AuthorityLevelType.Purchase.ToString()))
                {
                    //Quotation
                    liQuotation.Visible = true;

                    //Contractor Quotation
                    liContractorQuotation.Visible = false;
                    liCQuotationApproval.Visible = false;
                    liViewContractorQuotation.Visible = false;
                    
                    //Supplier Quotation
                    liSupplierQuotation.Visible = true;
                    liSQuotationApproval.Visible = false;
                    liViewSupplierQuotation.Visible = true;

                    //Purchase
                    liPurchase.Visible = true;

                    //Contractor Work Order
                    liViewContractorWorkOrder.Visible = false;

                    //Supplier Purchase Order
                    liViewSupplierPurchaseOrder.Visible = true;

                    //Contrator Invoice
                    liContractorInvoice.Visible = false;
                    liCInvoiceApproval.Visible = false;
                    liViewContratorInvoice.Visible = false;
                    
                    //Supplier Invoice
                    liSupplierInvoice.Visible = true;
                    liSInvoiceApproval.Visible = false;
                    liViewSupplierInvoice.Visible = true;

                    liMaterialConsumptionNote.Visible = true;
                    liMaterialReconcilationReport.Visible = true;

                    //Contractor Payment
                    liContractorPayment.Visible = false;
                    liCPaymentApproval.Visible = false;
                    liViewContractorPayment.Visible = false;

                    //Supplier Payment
                    liSupplierPayment.Visible = false;
                    liSPaymentApproval.Visible = false;
                    liViewSupplierPayment.Visible = true;

                    //Company work order
                    liCompany.Visible = false;
                    liCompanyWorkOrderApproval.Visible = false;
                    
                    //Quality
                    liQuality.Visible = true;

                    //Reports
                    liERPReports.Visible = true;
                    liReportPendingContractorPayment.Visible = false;
                    liReportPendindSupplierPayment.Visible = false;
                    liReportItemstockRpt.Visible = true;
                    liItemDetailsReport.Visible = true;
                    

                    //Administrator
                    liAdministrator.Visible = true;
                    liManageContractor.Visible = false;
                    liManageDepartment.Visible = false;
                    liManageSupplier.Visible = true;
                    liManageTermsAndCondition.Visible = true;
                    liManageUser.Visible = false;
                    liManageServiceDetails.Visible = true;
                    liManageItem.Visible = true;
                    liManageItemCategory.Visible = false;
                    liManageItemStock.Visible = true;
                    liManageGroup.Visible = false;
                    licheckPaymentStatus.Visible = false;
                }
                else if (LoggedInUser.Role().Equals(AuthorityLevelType.Project.ToString()))
                {
                    //Quotation
                    liQuotation.Visible = true;

                    //Contractor Quotation
                    liContractorQuotation.Visible = true;
                    liCQuotationApproval.Visible = false;
                    liViewContractorQuotation.Visible = true;

                    //Supplier Quotation
                    liSupplierQuotation.Visible = false;
                    liSQuotationApproval.Visible = false;
                    liViewSupplierQuotation.Visible = false;

                    //Purchase
                    liPurchase.Visible = true;

                    //Contractor Work Order
                    liViewContractorWorkOrder.Visible = true;

                    //Supplier Purchase Order
                    liViewSupplierPurchaseOrder.Visible = false;

                    //Contrator Invoice
                    liContractorInvoice.Visible = true;
                    liCInvoiceApproval.Visible = false;
                    liViewContratorInvoice.Visible = true; ;

                    //Supplier Invoice
                    liSupplierInvoice.Visible = false;
                    liSInvoiceApproval.Visible = false;
                    liViewSupplierInvoice.Visible = false;

                    liMaterialConsumptionNote.Visible = true;
                    liMaterialReconcilationReport.Visible = true;

                    //Contractor Payment
                    liContractorPayment.Visible = false;
                    liCPaymentApproval.Visible = false;
                    liViewContractorPayment.Visible = true;

                    //Supplier Payment
                    liSupplierPayment.Visible = false;
                    liSPaymentApproval.Visible = false;
                    liViewSupplierPayment.Visible = false;

                    //Company work order
                    liCompany.Visible = true;
                    liCompanyWorkOrderApproval.Visible = false;
                    
                    //Quality
                    liQuality.Visible = true;
                    liQualityViewSupplierReceiveMaterial.Visible = true;
                    liQualityViewDemandIssueVoucher.Visible = true;
                    liQualityViewReturnMaterial.Visible = true;
                    liQualityViewIssueMaterial.Visible = true;
                    liQualityViewReceiveMaterialCompanyWorkOrder.Visible =true;
                    liQualityDemandIssueVoucher.Visible = true;
                    liQualityReturnMaterial.Visible = false;
                    liQualitySupplierReceiveMaterialSRM.Visible = false;
                    liQualityIssuingMaterial.Visible = false;
                    liQualityReceiveMaterialCompanyWorkOrder.Visible = true;
                        

                    

                    //Reports
                    liERPReports.Visible = true;
                    liReportPendingContractorPayment.Visible = false;
                    liReportPendindSupplierPayment.Visible = false;
                    liReportItemstockRpt.Visible = true;
                    liItemDetailsReport.Visible = false;

                    //Administrator
                    liAdministrator.Visible = true;
                    liManageContractor.Visible = true;
                    liManageDepartment.Visible = false;
                    liManageSupplier.Visible = false;
                    liManageTermsAndCondition.Visible = true;
                    liManageUser.Visible = false;
                    liManageServiceDetails.Visible = true;
                    liManageItem.Visible = false;
                    liManageItemCategory.Visible = false;
                    liManageItemStock.Visible = false;
                    liManageGroup.Visible = false;
                    licheckPaymentStatus.Visible = false;
                }
                else if (LoggedInUser.Role().Equals(AuthorityLevelType.Finance.ToString()))
                {
                    //Quotation
                    liQuotation.Visible = true;

                    // Contractor Quotation
                    liContractorQuotation.Visible = true;
                    liCQuotationApproval.Visible = false;
                    liViewContractorQuotation.Visible = true;

                    //Supplier Quotation
                    liSupplierQuotation.Visible = true;
                    liSQuotationApproval.Visible = false;
                    liViewSupplierQuotation.Visible = true;

                    //Purchase
                    liPurchase.Visible = true;

                    //Contractor Work Order
                    liViewContractorWorkOrder.Visible = true;

                    //Supplier Purchase Order
                    liViewSupplierPurchaseOrder.Visible = true;

                    //Contrator Invoice
                    liContractorInvoice.Visible = true;
                    liCInvoiceApproval.Visible = false;
                    liViewContratorInvoice.Visible = true;

                    //Supplier Invoice
                    liSupplierInvoice.Visible = true;
                    liSInvoiceApproval.Visible = false;
                    liViewSupplierInvoice.Visible = true;

                    liMaterialConsumptionNote.Visible = true;
                    liMaterialReconcilationReport.Visible = true;

                    //Contractor Payment
                    liContractorPayment.Visible = true;
                    liCPaymentApproval.Visible = false;
                    liViewContractorPayment.Visible = true;

                    //Supplier Payment
                    liSupplierPayment.Visible = true;
                    liSPaymentApproval.Visible = false;
                    liViewSupplierPayment.Visible = true;

                    //Company work order
                    liCompany.Visible = false;
                    liCompanyWorkOrderApproval.Visible = false;
                    
                    //Quality
                    liQuality.Visible = false;

                    //Reports
                    liERPReports.Visible = true;
                    liReportPendingContractorPayment.Visible = true;
                    liReportPendindSupplierPayment.Visible = true;
                    liReportItemstockRpt.Visible = true;
                    liItemDetailsReport.Visible = true;

                    //Administrator
                    liAdministrator.Visible = true;
                    liManageContractor.Visible = false;
                    liManageDepartment.Visible = false;
                    liManageSupplier.Visible = true;
                    liManageTermsAndCondition.Visible = false;
                    liManageUser.Visible = false;
                    liManageServiceDetails.Visible = true;
                    liManageItem.Visible = false;
                    liManageItemCategory.Visible = false;
                    liManageItemStock.Visible = false;
                    liManageGroup.Visible = false;
                    licheckPaymentStatus.Visible = false;
                }
                else if (LoggedInUser.Role().Equals(AuthorityLevelType.Executive.ToString()))
                {
                    //Quotation
                    liQuotation.Visible = true;

                    //Contractor Quotation
                    liContractorQuotation.Visible = true;
                    liCQuotationApproval.Visible = false;
                    liViewContractorQuotation.Visible = true;

                    //Supplier Quotation
                    liSupplierQuotation.Visible = true;
                    liSQuotationApproval.Visible = false;
                    liViewSupplierQuotation.Visible = true;

                    //Purchase
                    liPurchase.Visible = true;

                    //Contractor Work Order
                    liViewContractorWorkOrder.Visible = true;

                    //Supplier Purchase Order
                    liViewSupplierPurchaseOrder.Visible = true;

                    //Contrator Invoice
                    liContractorInvoice.Visible = true;
                    liCInvoiceApproval.Visible = false;
                    liViewContratorInvoice.Visible = true; ;

                    //Supplier Invoice
                    liSupplierInvoice.Visible = true;
                    liSInvoiceApproval.Visible = false;
                    liViewSupplierInvoice.Visible = true;

                    liMaterialConsumptionNote.Visible = true;
                    liMaterialReconcilationReport.Visible = true;

                    //Contractor Payment
                    liContractorPayment.Visible = true;
                    liCPaymentApproval.Visible = false;
                    liViewContractorPayment.Visible = true;

                    //Supplier Payment
                    liSupplierPayment.Visible = true;
                    liSPaymentApproval.Visible = false;
                    liViewSupplierPayment.Visible = true;

                    //Company work order
                    liCompany.Visible = true;
                    liCompanyWorkOrderApproval.Visible = false;

                    //Quality
                    liQuality.Visible = true;
                    liQualityViewSupplierReceiveMaterial.Visible = true;
                    liQualityViewDemandIssueVoucher.Visible = true;
                    liQualityViewReturnMaterial.Visible = true;
                    liQualityViewIssueMaterial.Visible = true;
                    liQualityViewReceiveMaterialCompanyWorkOrder.Visible = true;
                    liQualityDemandIssueVoucher.Visible = true;
                    liQualityReturnMaterial.Visible = true;
                    liQualitySupplierReceiveMaterialSRM.Visible = true;
                    liQualityIssuingMaterial.Visible = true;
                    liQualityReceiveMaterialCompanyWorkOrder.Visible = true;

                    //Reports
                    liERPReports.Visible = true;

                    //Administrator
                    liAdministrator.Visible = true;

                    liManageContractor.Visible = true;
                    liManageDepartment.Visible = false;
                    liManageSupplier.Visible = true;
                    liManageTermsAndCondition.Visible = true;
                    liManageUser.Visible = false;
                    liManageServiceDetails.Visible = true;
                    liManageItem.Visible = true;
                    liManageItemCategory.Visible = false;
                    liManageItemStock.Visible = false;
                    liManageGroup.Visible = false;
                    licheckPaymentStatus.Visible = false;
                }
                else if (LoggedInUser.Role().Equals(AuthorityLevelType.Store.ToString()))
                {
                    //Quotation
                    liQuotation.Visible = false;

                    //Purchase
                    liPurchase.Visible = false;

                    //Payment
                    liPayment.Visible = false;

                    //Invoice
                    liInvoice.Visible = false;

                    //Company work order
                    liCompany.Visible = false;
                    
                    //Quality
                    liQuality.Visible = false;

                    //Reports
                    liERPReports.Visible = false;

                    //Administrator
                    liAdministrator.Visible = true;

                    liManageContractor.Visible = false;
                    liManageDepartment.Visible = false;
                    liManageSupplier.Visible = false;
                    liManageTermsAndCondition.Visible = false;
                    liManageUser.Visible = false;
                    liManageServiceDetails.Visible = false;
                    liManageItem.Visible = true;
                    liManageItemCategory.Visible = false;
                    liManageItemStock.Visible = true;
                    liManageGroup.Visible = false;
                    licheckPaymentStatus.Visible = false;
                }
                else
                {
                    //Quotation
                    liQuotation.Visible = true;

                    // Contractor Quotation
                    liContractorQuotation.Visible = false;
                    liCQuotationApproval.Visible = false;
                    liViewContractorQuotation.Visible = false;

                    //Supplier Quotation
                    liSupplierQuotation.Visible = false;
                    liSQuotationApproval.Visible = false;
                    liViewSupplierQuotation.Visible = false;

                    //Purchase
                    liPurchase.Visible = false;

                    //Contractor Work Order
                    liViewContractorWorkOrder.Visible = false;

                    //Supplier Purchase Order
                    liViewSupplierPurchaseOrder.Visible = false;

                    //Contrator Invoice
                    liContractorInvoice.Visible = false;
                    liCInvoiceApproval.Visible = false;
                    liViewContratorInvoice.Visible = false;

                    //Supplier Invoice
                    liSupplierInvoice.Visible = false;
                    liSInvoiceApproval.Visible = false;
                    liViewSupplierInvoice.Visible = false;

                    liMaterialConsumptionNote.Visible = false;
                    liMaterialReconcilationReport.Visible = false;

                    //Contractor Payment
                    liContractorPayment.Visible = false;
                    liCPaymentApproval.Visible = false;
                    liViewContractorPayment.Visible = false;

                    //Supplier Payment
                    liSupplierPayment.Visible = false;
                    liSPaymentApproval.Visible = false;
                    liViewSupplierPayment.Visible = false;

                    //Company work order
                    liCompany.Visible = false;
                    liCompanyWorkOrderApproval.Visible = false;
                    
                    //Quality
                    liQuality.Visible = false;

                    //Reports
                    liERPReports.Visible = false;

                    //Administrator
                    liAdministrator.Visible = false;
                    licheckPaymentStatus.Visible = false;
                }
            }
        }

        #region Private Section
        public Users LoggedInUser
        {
            get
            {
                if (HttpContext.Current.Session[GlobalConstants.C_USER_SESSION] == null ||
                     CurrentUserName.ToUpper() != ((Users)HttpContext.Current.Session[GlobalConstants.C_USER_SESSION]).UserLoginId.ToUpper()
                     )
                {
                    String currentUser = this.CurrentUserName;
                    Users user = new Users();
                    try
                    {
                        user = ReadUserByLoginID(currentUser);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    HttpContext.Current.Session[GlobalConstants.C_USER_SESSION] = user;
                }
                return (Users)HttpContext.Current.Session[GlobalConstants.C_USER_SESSION];
            }
        }

        private String CurrentUserName
        {
            get
            {
                String currentUser = Page.User.Identity.Name;
                if (currentUser.IndexOf('\\') > -1)
                {
                    currentUser = currentUser.Substring(currentUser.IndexOf('\\') + 1, currentUser.Length - currentUser.IndexOf('\\') - 1);
                }
                return currentUser;
            }
        }

        private Users ReadUserByLoginID(String loginId)
        {
            UserBL userBL = new UserBL();
            Users user = userBL.ReadUserByLoginID(loginId);
            return user;
        }

        #endregion

        protected void LoginStatus1_LoggedOut(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
            this.Page.Response.Clear();
            Session.Abandon();
        }

       
    }
}