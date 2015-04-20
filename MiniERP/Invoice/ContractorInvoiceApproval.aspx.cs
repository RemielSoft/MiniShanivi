using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DocumentObjectModel;
using BusinessAccessLayer;
using BusinessAccessLayer.Invoice;
using MiniERP.Shared;


namespace MiniERP.Invoice
{
    public partial class ContractorInvoiceApproval : BasePage
    {
        //#region Private Global Variables

        //Int32 id = 0;
        
        //List<InvoiceDom> lstInvoice = null;
        //ContractorInvoiceBL contractorInvoiceBL = null;
        //List<ItemTransaction> lstItemtransaction = null;
        //QuotationBL quotationBL = new QuotationBL();
        //#endregion
        
        //#region Protected Section
        //protected void Page_Load(object sender, EventArgs e)
        //{
        //    if (!IsPostBack)
        //    {
        //        BindDropDownData(ddlStatus, "Name", "Id", quotationBL.ReadQuotationStatusMetaData());
        //        BindInvoice(Convert.ToInt32(StatusType.Pending));
        //    }

        //}
        //protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    id = Convert.ToInt32(ddlStatus.SelectedValue);
        //    ViewState["StatusId"] = id;
        //    BindInvoice(id);
        //}
        //protected void gvInvoice_PageIndexChanging(object sender, GridViewPageEventArgs e)
        //{
        //    gvInvoice.PageIndex = e.NewPageIndex;
        //    BindInvoice(Convert.ToInt32(ViewState["StatusId"]));
        //}

        //protected void gvInvoice_RowCommand(object sender, GridViewCommandEventArgs e)
        //{
        //    id = Convert.ToInt32(e.CommandArgument);
        //    if (e.CommandName=="lnkQuotation")
        //    {
        //        BindEmptyGrid(gvInvoiceItems);
        //        contractorInvoiceBL = new ContractorInvoiceBL();
        //        lstItemtransaction = contractorInvoiceBL.ReadInvoiceMapping(id);
        //        gvInvoiceItems.DataSource = lstItemtransaction;
        //        gvInvoiceItems.DataBind();
        //        ModalPopupExtender2.Show();
        //    }          
        //}
        //#endregion

        //#region Private Section

        //private void BindInvoice(Int32 status)
        //{
        //    contractorInvoiceBL = new ContractorInvoiceBL();
        //    lstInvoice = contractorInvoiceBL.ReadContractorInvoiceStatusWise(status);
        //    gvInvoice.DataSource = lstInvoice;
        //    gvInvoice.DataBind();
        //}
        //private void BindEmptyGrid(GridView Grid)
        //{
        //    Grid.DataSource =new  List<object>();
        //    Grid.DataBind();
        //}
        //#endregion

        
    }
}