using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccessLayer
{
    public class DBConstants
    {
        public const String READ_AUTHORITY = "procReadAuthorityLevelMetaData";

        #region Master Constants
        //ContractorQuotation
        //public const String CREATE_CONTRACTORQUOTATION = "ProcCreateContractorQuotation";
        //public const String UPDATE_CONTRACTORQUOTATION = "ProcUpdateContractor_Quotation";
        //public const String DELETE_CONTRACTORQUOTATION = "ProcDeleteContractorQuotation";
        //public const String READ_CONTRACTORQUOTATION = "ProcReadContractor_Quotation";



        //ContractorQuotationMapping

        //public const String CREATE_CONTRACTORQUOTATIONMAPPING = "ProcCreateContractor_Quotation_Mapping";
        //public const String DELETE_CONTRACTORQUOTATIONMAPPING = "ProcDeleteContractor_Quotation_Mapping";
        //public const String READ_CONTRACTORQUOTATIONMAPPING = "ProcReadContractor_Quotation_Mapping";
        //public const String UPDATE_CONTRACTORQUOTATIONMAPPING = "";



        //Group constants "MASTER"
        public const String CREATE_GROUP_MASTER = "procMasterCreateGroup";
        public const String UPDATE_GROUP_MASTER = "procMasterUpdateGroup";
        public const String DELETE_GROUP_MASTER = "procMasterDeleteGroup";
        public const String VALIDATE_GROUP_MASTER = "ProcMasterValidateGroup";
        public const String READ_GROUP_MASTER = "procMasterReadGroup";


        // Brand Constants "Master"

        public const String CREATE_BRAND_MASTER = "procCreateBrands";
        public const String UPDATE_BRAND_MASTER = "procUpdateBrands";
        public const String DELETE_BRAND_MASTER = "procDeleteBrands";
        public const String READ_BRAND_MASTER = "procReadBrands";
        public const String READ_ITEM_BRAND_MAPPING = "procReadManageItemBrandMapping";
        public const String CREATE_ITEM_BRAND_MAPPING = "procCreateManageItemBrandMapping";
        public const String DELETE_ITEM_BRAND_MAPPING = "procDeleteManageItemBrandMapping";
        public const String VALIDATE_BRAND_MASTER = "ProcMasterValidateBrand";


        // STORE Constants "Master"

        public const String CREATE_STORE_MASTER = "procCreateStore";
        public const String UPDATE_STORE_MASTER = "procUpdateStores";
        public const String DELETE_STORE_MASTER = "procDeleteStore";
        public const String READ_STORE_MASTER = "procReadStores";
        public const String READ_STORE_BASED_ON_STOCK_MASTER = "procReadStoreBasedOnStock";

        //Department constants "MASTER"
        public const String CREATE_DEPARTMENT_MASTER = "procMasterCreateDepartment";
        public const String UPDATE_DEPARTMENT_MASTER = "procMasterUpdateDepartment";
        public const String DELETE_DEPARTMENT_MASTER = "procMasterDeleteDepartment";
        public const String READ_DEPARTMENT_MASTER = "procMasterReadDepartment";
        public const String VALIDATE_DEPARTMENT_MASTER = "ProcMasterValidateDepartment";

        public const String READ_USER_BY_LOGIN_ID = "ProcReadUserByLoginId";


        // User Constant "MASTER"
        public const String CREATE_USER_MASTER = "procMasterCreateUser";
        public const String UPDATE_USER_MASTER = "procMasterUpdateUser";
        public const String DELETE_USER_MASTER = "procMasterDeleteUser";
        public const String READ_USER_MASTER = "procMasterReadUser";
        public const String READ_USER_GROUP = "ProcReadUserGroup";
        public const String CREATE_USERGROUP = "procCreateUserGroup";
        public const String READ_LOGINID = "procReadLoginId";
        public const String VALIDATE_USER_MASTER = "procMasterValidateUser";
        public const String VALIDATION_USER = "ProcValidationForUser";
        public const String UPDATE_USER_MASTER_CHANGE_PASSWORD = "procMasterUpdateUserChangPassword";

        // Project Constatnt  "MASTER"
        public const String CREATE_PROJECT_MASTER = "procMasterCreateProject";
        public const String UPDATE_PROJECT_MASTER = "procMasterUpdateProject";
        public const String DELETE_PROJECT_MASTER = "procMasterDeleteProject";
        public const String READ_PROJECT_MASTER = "procMasterReadProject";
        public const String VALIDATE_PROJECT_MASTER = "ProcMasterValidateProject";

        // ItemCategory Constatnt "MASTER"
        public const String CREATE_ITEMCATEGORY_MASTER = "procMasterCreateItemCategory";
        public const String UPDATE_ITEMCATEGORY_MASTER = "procMasterUpdateItemCategory";
        public const String DELETE_ITEMCATEGORY_MASTER = "procMasterDeleteItemCategory";
        public const String READ_ITEMCATEGORY_MASTER = "procMasterReadItemCategory";
        public const String VALIDATE_ITEM_CATEGORY_MASTER = "ProcMasterValidateItemCategory";

        //Contractor Constants "MASTER"
        public const String CREATE_CONTRACTOR_MASTER = "procMasterCreateContractor";
        public const String READ_CONTRACTOR_MASTER = "procMasterReadContractor";
        public const String UPDATE_CONTRACTOR_MASTER = "procMasterUpdateContractor";
        public const String DELETE_CONTRACTOR_MASTER = "procMasterDeleteContractor";
        public const String VALIDATE_CONTRACTOR_MASTER = "ProcMasterValidateContractor";
        public const String SEARCH_CONTRACTOR_MASTER = "procSearchContractor";
        public const String SEARCH_CONTRACTOR_BYID = "procSearchContractorId";


        //Supplier Constants "MASTER"
        public const String CREATE_SUPPLIER_MASTER = "procMasterCreateSupplier";
        public const String READ_SUPPLIER_MASTER = "procMasterReadSupplier";
        public const String UPDATE_SUPPLIER_MASTER = "procMasterUpdateSupplier";
        public const String DELETE_SUPPLIER_MASTER = "procMasterDeleteSupplier";
        public const String VALIDATE_SUPPLIER_MASTER = "ProcMasterValidateSupplier";
        public const String SEARCH_SUPPLIER_MASTER = "procSearchSupplier";
        public const String SEARCH_SUPPLIER_BYID = "procSearchSupplierId";

        //Item Constatnt "MASTER"
        public const String CREATE_ITEM_MASTER = "procMasterCreateItem";
        public const String READ_ITEM_MASTER = "procMasterReadItem";
        public const String READ_ITEM_BY_SPECIFICATION_ID_MASTER = "procMasterReadItemBySpcificationMappingId";
        public const String READ_ITEM_BY_CATEGORYID = "procReadItemByCategoryId";
        public const String UPDATE_ITEM_MASTER = "procMasterUpdateItem";
        public const String DELETE_ITEM_MASTER = "procMasterDeleteItem";
        public const String VALIDATE_ITEM_MASTER = "ProcMasterValidateItem";
        public const String READ_UNITMEASUREMENT = "procReadUnitMeasurement";
        public const String SEARCH_ITEM = "procSearchItem";
        public const String SEARCH_ITEM_TEST = "procTEST";
        public const String SEARCH_ITEM_BY_SPECIFICATION_ID = "procSearchItemBySpecificationId";
        public const String CREATE_ITEM_SPECIFICATIONS_MAPPING_MASTER = "procMasterCreateItemSpecificationMapping";
        public const String UPDATE_ITEM_SPECIFICATIONS_MAPPING_MASTER = "procMasterUpdateItemSpecificationMapping";
        //public const String READ_ITEM_MODEL = "procReadItemModels";
        public const String DELETE_ITEM_SPECIFICATIONS_BY_ID_MASTER = "procMasterDeleteItemSpecificationById";
        public const String DELETE_ITEM_SPECIFICATIONS_MASTER = "procMasterDeleteItemSpecification";
        public const String READ_MAXIMUM_SPECIFICATION_ID = "procReadMaxSpecificationId";
        public const String READ_SPECIFICATION_MASTER = "procMasterReadSpecifications";

        public const String VALIDATE_SPECIFICATION_MASTER = "ProcMasterValidateSpecification";
        public const String DELETE_SPECIFICATION_MASTER = "procMasterDeleteSpecifications";
        public const String READ_RANGES = "procReadRanges";


        public const String FIND_ITEMID_BY_SPECIFICATIONID = "procFindItemIdBySpecificaionId";
        public const String COUNT_SPECIFICATIONIDS_BY_ITEMID = "procCountSpecificaionIdsByItemId";

        //ItemStock Constants "MASTER"
        public const String CREATE_ITEMSTOCK_MASTER = "procMasterCreateItemStock";
        public const String READ_ITEMSTOCK_MASTER = "procMasterReadItemStock";
        public const String UPDATE_ITEMSTOCK_MASTER = "procMasterUpdateItemStock";
        public const String DELETE_ITEMSTOCK_MASTER = "procMasterDeleteItemStock";
        public const String VALIDATE_ITEMSTOCK_MASTER = "ProcMasterValidateSupplier";
        public const String SEARCH_ITEMSTOCK = "procSearchItemStock";
        public const String READ_BRANDS = "procReadBrands";
        public const String READ_SPECIFICATION_MASTER_FOR_UNITMEASUREMET = "procMasterReadUnitMeasurementBySpecifications";
        // public const String SEARCH_ITEMSTOCK_BYSPECICATIONID = "procSearchItemStockBySpecificationId";
        public const String SEARCH_ITEMSTOCK_BYSTOCKID = "procSearchItemStockByStockId";



        //Item Model Constants "MASTER"
        public const String CREATE_ITEMMODEL = "procCreateItemModel";
        public const String READ_ITEMMODEL = "procReadItemModel";
        public const String READ_MAKE_UNIT_MEASUREMENT = "procReadMake_UnitMeasurement";
        public const String UPDATE_ITEMMODEL = "procUpdateItemModel";
        public const String DELETE_ITEMMODEL = "procDeleteItemModel";
        public const String VALIDATE_ITEMMODEL = "ProcValidateItemModel";

        //TermsAndCondition Constants "MASTER"
        public const String CREATE_TERMS_AND_CONDITION_MASTER = "procMasterCreateTermsAndConditions";
        public const String UPDATE_TERMS_AND_CONDITION_MASTER = "procMasterUpdateTermsAndConditions";
        public const String DELETE_TERMS_AND_CONDITION_MASTER = "procMasterDeleteTermsAndConditions";
        public const String READ_TERMS_AND_CONDITION_MASTER = "procMasterReadTermsAndConditions";
        public const String VALIDATE_TERMS_AND_CONDITION_MASTER = "ProcMasterValidateTermsAndConditions";

        //Tax Constants
        public const String CREATE_TAX_MASTER = "procMasterCreateTax";
        public const String READ_TAX_MASTER = "procMasterReadTax";
        public const String UPDATE_TAX_MASTER = "procMasterUpdateTax";
        public const String DELETE_TAX_MASTER = "procMasterDeleteTax";
        public const String READ_DISCOUNT_MODE = "procReadDiscountMode";
        public const String READ_TAX_BY_DISCOUNT_Mode_ID = "procReadTaxByDiscountId";

        #endregion

        #region Quotation Constants

        //ContractorPurchaseOrder CONSTANTS
        public const String CREATE_CONTRACTOR_PURCHASE_ORDER = "ProcCreateContractorPurchaseOrder";
        public const String READ_CONTRACTOR_QUOTATION = "procReadContractorQuotation";
        public const String READ_CONTRACTOR_ORDER = "procReadContractorOrder";
        //public const String READ_CONTRACTOR_QUOTATION_BY_QUOTATION_ID = "procReadContractorQuotationByQuotationId";
        public const String READ_QUOTATION_STATUS_METADATA = "procReadQuotationStatusMetaData";
        public const String READ_CONTRACTOR_QUOTATION_BY_STATUS_TYPE = "procReadContractorQuotationStatusWise";
        public const String READ_CONTRACTOR_QUOTATION_BY_Id_NUMBER = "procReadContractorQuotationByQuotationId";
        public const String UPDATE_CONTRACTOR_QUOTATION_STATUS = "procUpdateContractorQuotaionStatus";
        public const String DELETE_CONTRACTOR_QUOTATION = "ProcDeleteContractorQuotation";
        public const String READ_TAX_TYPE = "procReadTaxType";


        //Contractor PURCHASE ORDER MAPPING CONSTANTS
        public const String CREATE_CONTRACTOR_PURCHASE_ORDER_MAPPING = "procCreateContractor_Purchase_Order_Mapping";
        public const String READ_CONTRACTOR_QUOTATION_MAPPING = "ProcReadContractorQuotationMapping";
        public const String DELETE_CONTRACTOR_QUOTATION_MAPPING = "ProcDeleteContractorQuotationMapping";
        public const String RESET_QUOTATION_MAPPING = "procResetQuotationMapping";

        //SUPPLIER PURCHASE ORDER CONSTANTS
        public const String CREATE_SUPPLIER_PURCHASE_ORDER = "procCreateSupplierPurchaseOrder";
        public const String READ_SUPPLIER_QUOTATION = "procReadSupplierQuotation";
        public const String READ_SUPPLIER_QUOTATION_BY_Id_NUMBER = "procReadSupplierQuatationByQuatationId";
        public const String READ_SUPPLIER_QUOTATION_BY_STATUS_TYPE = "procReadSupplierQuotationStatusWise";
        public const String READ_SUPPLIER_ORDER = "procReadSupplierOrder";
        public const String UPDATE_SUPPLIER_QUOTATION_STATUS = "procUpdateSupplierQuotaionStatus";
        public const String DELETE_SUPPLIER_QUOTATION = "ProcDeleteSupplierQuotation";


        //SUPPLIER PURCHASE ORDER MAPPING CONSTANTS
        public const String CREATE_SUPPLIER_PURCHASE_ORDER_MAPPING = "procCreateSupplier_Purchase_Order_Mapping";
        public const String READ_SUPPLIER_QUOTATION_MAPPING = "ProcReadSupplierQuotationMapping";
        public const String DELETE_SUPPLIER_QUOTATION_MAPPING = "ProcDeleteSupplierQuotationMapping";

        //Documment Upload CONSTANTS
        public const String CREATE_AND_READ_DOCUMENT_STACK_ID = "procCreateDocument_Satck";
        public const String CREATE_DOCUMENT_MAPPING = "procCreateDocument_Mapping";
        public const String READ_DOCUMENT_MAPPING = "procReadDocument_Mapping";
        public const String RESET_DOCUMENT_MAPPING = "procResetDocument_Mapping";


        // Delivery Schedule CONSTANTS
        public const String CREATE_DELIVERY_SCHEDULE = "procCreateDeliverySchedule";
        public const String READ_DELIVERY_SCHEDULE = "procReadDeliverySchedule";
        public const String DELETE_DELIVERY_SCHEDULE = "procDeleteDeliverySchedule";
        public const String RESET_DELIVERY_SCHEDULE = "procResetDeliverySchedule";

        // Payment Term CONSTANTS
        public const String CREATE_PAYMENT_TERM = "procCreatePaymentTerm";
        public const String READ_PAYMENT_TERM = "procReadPaymentTerm";
        public const String DELETE_PAYMENT_TERM = "procDeletePaymentTerm";
        public const String READ_PAYMENT_TERM_META = "procReadPaymentTermMetaData";
        public const String RESET_PAYMENT_TERM = "procResetPaymentTerm";


        // Term And Condition CONSTANTS
        public const String CREATE_TERM_AND_CONDITION = "procCreateTermAndCondition";
        public const String READ_TERM_AND_CONDITION = "procReadTermAndCondition";
        public const String DELETE_TERM_AND_CONDITION = "procDeleteTermAndCondition";
        public const String RESET_TERM_AND_CONDITION = "procResetTermConditions";

        #endregion

        #region Company Work Order

        public const String CREATE_COMPANY_WORK_ORDER = "procCreateCompanyWorkOrder";
        public const String CREATE_COMPANY_WORK_ORDER_MAPPING = "procCreateCompanyWorkOrderMapping";
        public const String CREATE_COMPANY_WORK_ORDER_BANK_GUARANTEE = "procCreateCompanyWorkOrderBankGuarantee";
        public const String CREATE_COMPANY_WORK_ORDER_SERVICE_DETAIL = "procCreateCompanyWorkOrderServiceDetail";

        public const String READ_COMPANY_WORK_ORDER = "procReadCompanyWorkOrder";
        public const String READ_COMPANY_WORK_ORDER_BY_DATE = "procReadCompanyWorkOrderByDate";
        public const String READ_COMPANY_WORK_ORDER_MAPPING = "procReadCompanyWorkOrderMapping";
        public const String READ_COMPANY_WORK_ORDER_BANK_GUARANTEE = "procReadCompanyWorkOrderBankGuarantee";
        public const String READ_COMPANY_WORK_ORDER_SERVICE_DETAIL = "procReadCompanyWorkOrderServiceDetail";

        public const String DELETE_COMPANY_WORK_ORDER = "procDeleteCompanyWorkOrder";
        public const String DELETE_COMPANY_WORK_ORDER_MAPPING = "procDeleteWorkOrderMapping";
        public const String DELETE_COMPANY_WORK_ORDER_BANK_GUARANTEE = "procDeleteCompanyBankGuarantee";//
        public const String DELETE_COMPANY_WORK_ORDER_SERVICE_DETAIL = "procDeleteCompanyWorkOrderServiceDetail";

        public const String UPDATE_COMPANY_WORK_ORDER = "procUpdateCompanyWorkOrder";//
        public const String UPDATE_COMPANY_WORK_ORDER_STATUS = "procUpdateCompanyWorkOrderStatus";

        public const String VALIDATE_COMPANY_WORK_ORDER = "procValidateCompanyWorkWorder";

        #endregion

        #region Invoice Constants

        //Contractor Invoice Approval
        public const String READ_APPROVAL_STATUS = "procReadApprovalStatus";
        public const String READ_INVOICE_TYPE = "procReadInvoiceType";
        #endregion

        #region Quality Constants

        // Issue Demand Voucher
        public const String CREATE_ISSUE_DEMAND_VOUCHER = "procCreateMaterialIssueDemandVoucher";
        public const String CREATE_ISSUE_DEMAND_VOUCHER_MAPPING = "procCreateMaterialIssueDemandVoucherMapping";
        public const String SEARCH_ISSUE_DEMAND_VOUCHER = "procReadIssueDemandVoucher";
        public const String DELETE_ISSUE_DEMAND_VOUCHER = "procDeleteIssueDemandVoucher";
        public const String VALIDATE_ISSUE_DEMAND_VOUCHER = "ProcValidationIssueDemandVoucher";
        public const String READ_ISSUE_DEMAND_VOUCHER_MAPPING = "procReadIssueDemandMapping";
        public const String UPDATE_ISSUE_DEMAND_VOUCHER_STATUS = "procUpdateIssueDemandVoucherStatus";
        public const String READ_MATERIAL_ISSUE_DEMAND_VOUCHER = "procReadMaterialIssueDemandVoucher";
        public const String RESET_ISSUE_DEMAND_VOUCHER_MAPPING = "procResetIssueDemandVoucherMapping";
        //public const String READ_MATERIAL_ISSUE_DEMAND_VOUCHER_MAPPING = "procReadMaterialIssueDemandVoucherMapping";

        //Supplier Recieve Material
        public const String CREATE_SUPPLIER_RECIEVE_MATERIAL = "procCreateSupplierRecieveMaterial";
        public const String CREATE_SUPPLIER_RECIEVE_MATERIAL_MAPPING = "procCreateRecieveMaterialMapping";
        public const String SEARCH_SUPPLIER_RECIEVE_MATERIAL = "procSearchSupplierReceiveMaterial";
        public const String READ_SUPPLIER_RECIEVE_MATERIAL = "procReadSupplierReceiveMaterial";
        public const String READ_SUPPLIER_RECIEVE_MATERIAL_MAPPING = "procReadSupplierReceiveMaterialMapping";
        public const String UPDATE_SUPPLIER_RECIEVE_MATERIAL_STATUS = "procUpdateSupplierReceiveMaterialStatus";
        public const String RESET_SUPPLIER_RECIEVE_MATERIAL_MAPPING = "procResetSupplierRecieveMaterialMapping";
        public const String DELETE_SUPPLIER_RECEIVE_MATERIAL = "procDeleteSupplierReceiveMaterial";
        public const String UPDATE_STOCK_RECEIVE_ISSUE_MATERIAL = "procUpdateStockReceiveMaterial";

        //Receive Material CWO
        public const String CREATE_RECIEVE_MATERIAL_CWO = "procCreateCompanyWorkOrderReceiveMaterial";
        public const String CREATE_RECIEVE_MATERIAL_CWO_MAPPING = "procCreateCompanyWorkOderReceiveMaterialMapping";
        public const String DELETE_RECEIVE_MATERIAL_CWO = "procDeleteReceiveMaterialCWO";
        public const String UPDATE_RECEIVE_MATERIAL_CWO_STATUS = "procUpdateReceiveMaterialCWO";
        public const String READ_RECEIVE_MATERIAL_CWO = "procReadContractReceiveMaterial";
        public const String READ_RECEIVE_MATERIAL_CWO_MAPPING = "procReadContractReceiveMaterialMapping";
        public const String READ_RECEIVE_MATERIAL_CWO_BYID = "procReadCompanyWorkOrederReceiveMaterial";
        public const String RESET_SUPPLIER_RECIEVE_MATERIAL_CWO_MAPPING = "procResetSupplierRecieveMaterialCWOMapping";

        //Issue Material 
        public const String CREATE_ISSUE_MATERIAL = "procCreatedIssueMaterial";
        public const String CREATE_ISSUE_MATERIAL_MAPPING = "procCreateIssueMaterialMapping";
        public const String RESET_ISSUE_MATERIAL_MAPPING = "procResetIssueMaterialMapping";
        public const String READ_ISSUE_MATERIAL = "procReadIssueMaterial";
        public const String READ_ISSUE_MATERIAL_MAPPING = "procReadIssueMaterialMapping";
        public const String UPDATE_ISSUE_MATERIAL_STATUS = "procUpdateIssueMaterialStatus";
        public const String DELETE_ISSUE_MATERIAL = "procDeleteIssueMaterial";
        public const String READ_STOCK_STATUS = "procReadStockStatus";
        public const String READ_STOCK_STATUS_TEMP = "procReadStockStatus_Temp";
        public const String GENERATE_ISSUE_MATERIAL_NUMBER = "GenerateIssueMaterialNumber";
        public const String READ_ISSUE_MATERIALBY_DEMAND_VOUCHER = "procGetIssueMaterialByIssueDemandVoucher";
        public const String READ_CONTRACTOR_NAMEBY_PREFIX = "procReadContractorNameByPrefix";
        public const String READ_SUPPLIER_NUMBER_PREFIX = "procReadSupplierNameByPrefix";
        //Return Material
        public const string CREATE_RETURN_MATERIAL = "procCreateReturnMaterial";
        public const string CREATE_RETURN_MATERIAL_MAPPING = "procCreateReturnMaterialMapping";
        public const String RESET_RETURN_MATERIAL_MAPPING = "procResetReturnMaterialMapping";
        public const string READ_SUPPLIER_RETURN_MATERIAL_MAPPING = "procReadReturnMaterialMapping";
        public const string READ_SUPPLIER_RETURN_MATERIAL = "procReadReturnMaterial";
        public const String DELETE_SUPPLIER_RETURN_MATERIAL = "procDeleteReturnMaterial";
        public const String UPDATE_SUPPLIER_RETURN_MATERIAL_STATUS = "procUpdateReturnMaterialStatus";



        //Return Material Contractor

        public const string CREATE_RETURN_MATERIAL_CONTRACTOE = "procCreateReturnMaterialContractor";
        public const string CREATE_RETURN_MATERIAL_CONTRACTOR_MAPPING = "procCreateReturnMaterialContractorMapping";
        public const string READ_RETURN_MATERIAL_CONTRACTOR = "procReadReturnMaterialContractor";
        public const string READ_RETURN_MATERIAL_CONTRACTOR_MAPPING = "procReadReturnMaterialContractorMapping";
        public const String UPDATE_RETURN_MATERIAL_CONTRACTOR_STATUS = "procUpdateReturnMaterialContractorStatus";
        public const String DELETE_RETURN_MATERIAL_CONTRACTOR = "procDeleteReturnMaterialContractor";
        public const String READ_ISSUE_RETURN_MATERIAL_CONTRACTOR_MAPPING = "procReadIssueReturnMaterialContractorMapping_New";

        #endregion

        #region Invoice Constants

        // Contractor Invoice
        public const String CREATE_CONTRACTOR_INVOICE = "procCreateContractorInvoice";
        public const String CREATE_CONTRACTOR_INVOICE_MAPPING = "procCreateContractorInvoiceMapping";
        public const String CREATE_CONTRACTOR_INVOICE_MAPPING_ADVANCE = "procCreateContractorInvoiceMappingAdvance";
        public const String READ_CONTRACTOR_INVOICE = "procReadContractorInvoice";
        public const String READ_CONTRACTOR_INVOICE_BY_STATUS_WISE = "procReadContractorInvoiceStatusWise";
        public const String DELETE_CONTRACTOR_INVOICE = "procDeleteContractorInvoice";
        public const String DELETE_CONTRACTOR_INVOICE_MAPPING = "procDeleteContractorInvoiceMapping";
        public const String UPDATE_CONTRACTOR_INVOICE_STATUS = "procUpdateContractorInvoiceStatus";
        public const String UPDATE_CONTRACTOR_INVOICE_STATUS_TYPE = "procUpdateContractorInvoiceStatusType";
        public const String READ_QUOTATION_DEMAND_VOUCHER = "ProcReadQuotationDemandVoucher";
        public const String READ_INVOICE_BY_INVOICE_NUMBER = "procReadInvoiceByInvoiceNumber";
        public const String READ_CONTRACTOR_WORK_ORDER_MAPPING = "procReadContractorWorkOrderMapping";
        public const String READCONTRACTOR_INVOICE_MAPPING = "procReadContractorInvoiceMapping";
        public const String READ_INVOICE_MAPPING_BY_INVOICE_ID = "procReadInvoiceMappingByInvoiceId";
        public const String READ_INVOICE_MAPPING_BY_INVOICE_NUMBER = "procReadInvoiceMappingByInvoiceNumber";
        public const String RESET_CONTRACTOR_QUOTATION_INVOICE_MAPPING = "procResetContractorQuotationInvoiceMapping";
        public const String READ_CONTRACTOR_QUTATION_BY_NUMBER = "procReadContractorQuotationByQuotationNo";
        public const String READ_CONTRACTOR_INVOICE_VIEW = "procReadContractorInvoiceView";


        // Supplier Invoice
        public const String CREATE_SUPPLIER_INVOICE = "procCreateSupplierInvoice";
        public const String CREATE_SUPPLIER_INVOICE_MAPPING = "procCreateSupplierInvoiceMapping";
        public const String CREATE_SUPPLIER_INVOICE_MAPPING_ADVANCE = "procCreateSupplierInvoiceMappingAdvance";
        public const String RESET_SUPPLIER_INVOICE_MAPPING = "procResetSupplierInvoiceMapping";
        public const String READ_SUPPLIERPO_RECEIVE_MATERIAL_MAPPING = "procReadSupplierPOReceiveMaterialMapping";
        public const String READ_SUPPLIER_INVOICE = "procReadSupplierInvoice";
        public const String DELETE_SUPPLIER_INVOICE = "procDeleteSupplierInvoice";
        public const String DELETE_SUPPLIER_INVOICE_MAPPING = "procDeleteSupplierInvoiceMapping";
        public const String UPDATE_SUPPLIER_INVOICE_STATUS = "procUpdateSupplierInvoiceStatus";
        public const String READ_SUPPLIER_INVOICE_MAPPING = "procReadSupplierInvoiceMappingRpt";
        public const String READ_SUPPLIER_INVOICE_BY_STATUS_WISE = "procReadSupplierInvoiceStatusWise";
        public const String READ_SUPPLIER_QUOTATION_BY_PONUMBER = "procReadSupplierQuatationByPONumber";
        public const String UPDATE_SUPPLIER_INVOICE_STATUS_TYPE = "procUpdateSupplierInvoiceStatusType";
        public const String READ_SUPPLIER_INVOICE_VIEW = "procReadSupplierInvoiceView";
        public const String READ_ALL_PAYMENT = "proc_readallPayment";

        //Material ConsumptionNotes Constants
        public const String READ_ISSUE_MATERIAL_CONSUMPTION = "procReadIssueMaterialConsumption";
        public const String READ_ISSUE_MATERIAL_MAPPING_CONSUMPTION = "procReadIssueMaterialMappingConsumption";
        public const String READ_MATERIAL_CONSUMPTION_MAPPING = "procReadMaterialConsumptionMapping";
        public const String READ_MATERIAL_CONSUMPTION_NOTES_EDIT_MAPPING = "procReadMaterialConsumptionEditMapping";
        public const String READ_MATERIAL_CONSUMPTION_NOTES = "procReadMaterialConsumptionNotes";
        public const String CREATE_MATERIAL_CONSUMPTION_NOTES_MAPPING = "procCreateMaterialConsumptionNoteMapping";
        public const String CREATE_MATERIAL_CONSUMPTION_NOTES = "procCreateMaterialConsumptionNote";
        public const String RESET_MATERIAL_CONSUMPTION_NOTES_MAPPING = "procResetMaterialConsumptionMapping";
        public const String DELETE_MATERIAL_CONSUMPTION_NOTES = "procDeleteMaterialConsumptionNotes";
        public const String READ_MATERIAL_RECONCILATION_REPORT = "procReadMaterialReconciliationReport";
        public const String UPDATE_MATERIAL_RECONCILIATION_STATUS = "procUpdateMaterialReconciliationStatus";
        public const String DELETE_MATERIAL_CONSUMPTION_NOTE_MAPPING = "procDeleteMaterialConsumptionMapingMapping";



        #endregion

        #region Payment

        public const String CREATE_PAYMENT = "procCreatePayment";
        public const String READ_PAYMENT = "procReadPayment";
        public const String READ_PAYMENT_BY_DATE_AND_ID = "procReadPaymentByDateAndId";//
        public const String READ_PAYMENT_BY_DATE = "procReadPaymentByDate";
        public const String READ_PAYMENT_BY_STATUS_ID = "procReadPaymentByStatusId";
        public const String DELETE_PAYMENT = "procDeletePayment";//
        public const String UPDATE_PAYMENT_STATUS = "procUpdatePaymentStatus";
        public const String UPDATE_PAYMENT = "procUpdatePayment";

        #endregion

        #region MetaData Constants

        public const String READ_PAYMENT_MODE_TYPE_METADATA = "procReadPaymentModeTypeMetaData";
        public const String READ_PAYMENT_STATUS = "procReadpaymentStatusMetaData";

        #endregion
    }
}
