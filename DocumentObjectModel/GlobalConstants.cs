using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DocumentObjectModel
{
    public class GlobalConstants
    {
        public const String C_USER_SESSION = "UserSession";

        public const String S_Quotation = "Quotation";
        public const String S_Document_Stack_Id = "DocumentStackId";
        public const String S_Document_Serial = "DocumentSerial";
        public const String S_Documents = "Documents";
        public const String S_Page_Name = "PageName";
        public const String S_ItemTransaction_List = "ItemTransactionList";
        public const String S_Active_Tab = "ActiveTab";
        public const String S_DeliverySchedule = "DeliverySchedule";
        public const String S_PaymentTerm = "PaymentTerm";
        public const String S_TermCondition = "TermCondition";

        public const String L_Duplicate_Activity = "* Record Already Exist.";
        public const String L_Duplicate_Item = "* Item with Spceification Alreday Exist,Kindly Choose Another.";
        public const String L_Item_Exceed = "* Item Quantity of Item with Spceification can not be more than Actual Number of Item Ordered.";
        public const String L_Duplicate_Payment_Type = "* Payment Type Already Added.";
        public const String L_Multiple_Payment_Type = "* Only Single Payment Type Can Be Used.";
        public const String L_Duplicate_TermCondition = "* Terms & Conditions Alreday Added For Use.";
        public const String L_Duplicate_Master_TermCondition = "* Alreday Defined Terms & Conditions Can Not Be Added Manually.";

        public const String L_Quotation_Approval = "";
        public const String L_Quotation = "";

        public const String M_File_Exist = "Document Already Exist, Kindly choose another";
        public const String M_Date_Difference = "Valid Till Date(Closing Date) can not be less than Order Date";

        public const String M_Quotaion_Not_Exist = "Quotation Does Not Exist.";
        public const String M_Tax_without_Activity_Desc = "* Please Add the Activity Description for tax applicable.";
        public const String M_Tax_without_Item = "* Please Add the Item for tax applicable.";
        public const String M_Discount_More_Than_Amount = "* Discount can not be more than Amount.";
        public const String M_Discount_More_Than_Cost = "* Discount can not be more than Cost.";
        public const String M_Days_Payment = "Please Enter Number of Days.";
        public const String M_Advance_Payment = "Please Enter Percentage Value.";

        public const String P_Contractor_Quotation = "ContractorQuotation";
        public const String P_Supplier_Quotation = "SupplierQuotation";

        public const String P_Contractor_Quotation_Approval = "ContractorQuotationApproval";
        public const String P_View_Contractor_Quotation = "ViewContractorQuotation";
        public const String P_Supplier_Quotation_Approval = "SupplierQuotationApproval";
        public const String P_View_Supplier_Quotation = "ViewSupplierQuotation";
        public const String P_View_Contractor_Purchase_Order = "ViewContractorPurchaseOrder";
        public const String P_View_Supplier_Purchase_Order = "ViewSupplierPurchaseOrder";

        public const String P_Contractor_Invoice_Approval = "ContractorInvoiceApproval";
        public const String P_View_Contractor_Invoice = "ViewContractorInvoice";
        public const String P_Supplier_Invoice_Approval = "SupplierInvoiceApproval";
        public const String P_View_Supplier_Invoice = "ViewSupplierInvoice";

        public const String P_Contractor_Payment_Approval = "ContractorPaymentApproval";
        public const String P_View_Contractor_Payment = "ViewContractorPayment";
        public const String P_Supplier_Payment_Approval = "SupplierPaymentApproval";
        public const String P_View_Supplier_Payment = "ViewSupplierPayment";

        public const String P_Company_Work_Order = "CompanyWorkOrder.aspx";
        public const String P_Company_Work_Order_Approval = "CompanyWorkOrderApproval";
        public const String P_View_Company_WorkOrder = "ViewCompanyWorkOrder";


        public const String P_View_Supplier_Receive_Material = "ViewSupplierReceiveMaterial";
        public const String P_View_Demand_Issue_Voucher = "ViewDemandIssueVoucher";
        public const String P_View_Issue_Material = "ViewIssueMaterial";
        public const String P_View_Supplier_Receive_Material_CWO = "ViewReceiveMaterialCompanyWorkOrder";

        public const String P_View_Return_Material = "ViewReturnMaterial";


        public const string C_ERROR_MESSAGE_GROUP_USEDIN_User = "This Group Is Used In Manage User";
        public const string EMPTY_TEXT = "No Record(s) Found";
        public const string C_UPDATE_GROUP_MESSAGE = "Group Details Updated Successfully.";
        public const string C_MODE_INSERT = "INSERT";
        public const string C_MODE_UPDATE = "UPDATE";
        public const string C_DUPLICATE_MESSAGE = "Record Already Exist.";
        public const string C_CREATE_GROUP_MESSAGE = "Group Details Created Successfully.";
        public const string C_GROUP_DELETE_MESSAGE = "Group Details Deleted Successfully.";
        public const string C_DEPARTMENT_CREATE_MESSAGE = "Department Details  Created Successfully.";
        public const string C_DEPARTMENT_UPDATE_MESSAGE = "Department Details Updated Successfully.";
        public const string C_DEPARTMENT_DELETE_MESSAGE = "Department Details Deleted Successfully.";
        public const string C_ERROR_MESSAGE_USER_USEDIN_DEPARTMENT = "You Can't Delete This User As User(s) Are Mapped To Department.";
        public const string C_CREATE_USER_MESSAGE = "User Details Created Successfully.";
        public const string C_USER_DELETE_MESSAGE = "User Details Deleted Successfully.";
        public const string C_UPDATE_USER_MESSAGE = "User Details Updated Successfully.";
        public const string C_UPDATE_USER_PASSWORD = "New Password Updated Successfully.";

        // Contractor Section
        public const string C_CREATE_CONTRACTOR_MESSAGE = "Contractor Details Created Successfully.";
        public const string C_UPDATE_CONTRACTOR_MESSAGE = "Contractor Details Updated Successfully.";
        public const string C_ERROR_MESSAGE_CONTRACTOR_USEDIN_CONTRACTOR_INVOICE = "You Can't Delete This Contractor as Contractor_Invoice(s) Are Mapped To This Contractor.";

        // Supplier Section
        public const string C_CREATE_SUPPLIER_MESSAGE = "Supplier Details Created Successfully.";
        public const string C_UPDATE_SUPPLIER_MESSAGE = "Supplier Details Updated Successfully.";
        public const string C_ERROR_MESSAGE_SUPPLIER_USEDIN_Contractor_Invoice = "You Can't Delete This Supplier As Contractor_Invoice(s) Are Mapped To This Contractor.";

        // ItemStock Section
        public const string C_CREATE_ItemStock_MESSAGE = "Item Stock  Created Successfully.";
        public const string C_UPDATE_ItemStock_MESSAGE = "Item Stock Updated Successfully.";
        public const string C_ERROR_MESSAGE_ItemStock_USEDIN_Contractor_Invoice = "You Can't Delete This Supplier As Contractor_Invoice(s) Are Mapped To This Contractor.";

        //Item Section
        public const string C_CREATE_ITEM_MESSAGE = "Item Created Successfully.";
        public const string C_UPDATE_ITEM_MESSAGE = "Item Updated Successfully.";
        public const string C_DELETE_ITEM_MESSAGE = "Item Deleted Successfully.";

        public const string C_CREATE_BRAND_MESSAGE = "Brand Created Successfully.";
        public const string C_UPDATE_BRAND_MESSAGE = "Brand Updated Successfully.";
        public const string C_DELETE_BRAND_MESSAGE = "Brand Deleted Successfully.";


        // Store
        public const string C_CREATE_STORE_MESSAGE = "Store Created Successfully.";
        public const string C_UPDATE_STORE_MESSAGE = "Store Updated Successfully.";
        public const string C_DELETE_STORE_MESSAGE = "Store Deleted Successfully.";

        //Project Section
        public const string C_CREATE_PROJECT_MESSAGE = "Project Details Created Successfully.";
        public const string C_UPDATE_PROJECT_MESSAGE = "Project Details Updated Successfully.";
        public const string C_DELETE_PROJECT_MESSAGE = "Project Details Deleted Successfully.";

        //ItemCategory Section
        public const string C_CREATE_ITEMCATEGORY_MESSAGE = "Item Category Created Successfully.";
        public const string C_UPDATE_ITEMCATEGORY_MESSAGE = "Item Category Updated Successfully.";
        public const string C_DELETE_ITEMCATEGORY_MESSAGE = "Item Category Deleted Successfully.";

        //ItemModel Section
        public const string C_CREATE_ITEMMODEL_MESSAGE = "ItemModel Created Successfully.";
        public const string C_UPDATE_ITEMMODEL_MESSAGE = "ItemModel Updated Successfully.";
        public const string C_DELETE_ITEMMODEL_MESSAGE = "ItemModel Deleted Successfully.";

        //TermsAndCondition Section
        public const string C_CREATE_TERMS_AND_CONDITION_MESSAGE = "Terms & Condition Is Created Successfully.";
        public const string C_DELETE_TERMS_AND_CONDITION_MESSAGE = "Terms & Condition Is Deleted Successfully.";
        public const string C_UPDATE_TERMS_AND_CONDITION_MESSAGE = "Terms & Condition Is Updated Successfully.";

        //Tax Section
        public const string C_CREATE_TAX_MESSAGE = "Tax Master Is Created Successfully.";
        public const string C_UPDATE_TAX_MESSAGE = "Tax Master Is Updated Successfully.";
        public const string C_TAX_DELETE_MESSAGE = "Tax Deleted Successfully.";

        // Validation for Information Dom
        public const string C_ERROR_MESSAGE_PAN_ALPHANUMERIC = "PAN No Should Be In Correct Format";
        public const string C_ERROR_MESSAGE_TAN_ALPHANUMERIC = "TIN No Should Be Alphanumeric";
        public const string C_ERROR_MESSAGE_ESI_ALPHANUMERIC = "ESI No Should Be Alphanumeric";
        public const string C_ERROR_MESSAGE_PF_ALPHANUMERIC = "PF No Should Be Alphanumeric";
        public const string C_ERROR_MESSAGE_FAX_NUMERIC = "FAX No Should Be Numeric";
        public const string C_ERROR_MESSAGE_CONTACT_NAME_ALPHANUMERIC = "Contact Person Name Should Be Alphanumeric";

        //Validation Error Message Constants
        public const string C_ERROR_MESSAGE_ALPHANUMERIC = "Only Alphanumerics Will Allowed";
        public const string C_ERROR_MESSAGE_ADDRESS = "Enter the Text";
        public const string C_ERROR_MESSAGE_EMAILID = "Invalid Email Entry";
        public const string C_ERROR_MESSAGE_NUMERIC = "Only Numeric Value Is Allowed";
        public const string C_ERROR_MESSAGE_DATE_FORMAT = "Invalid Date Format";
        public const string C_ERROR_MESSAGE_MOBILE_NUMBER = "Mobile Number Should Be In Minimum 10 Digits";
        //public const string C_ERROR_MESSAGE_ADDRESS = @"In address these char (^<>\\{}"") are not allowed ";
        public const string C_ERROR_MESSAGE_ALPHABETIC = "Only Alphabetic Characters Are Allowed";
        public const string C_ERROR_MESSAGE_PIN_CODE = "Pin Code Should Be In Minimum 6 Digits";
        public const string C_ERROR_MESSAGE_CITY = "City Should Be In Characters";
        public const string C_ERROR_MESSAGE_STATE = "State Should Be In Characters";
        public const string C_ERROR_MESSAGE_PHONE_NUMBER = "Phone Number Should Be In Minimum 10 Digits";
        //public const string C_ERROR_MESSAGE_DESCRIPTION = @"In description these char (^<>\\{}"") are not allowed ";


    }

}
