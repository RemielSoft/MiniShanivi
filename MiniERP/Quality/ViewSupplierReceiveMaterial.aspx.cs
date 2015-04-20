using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using MiniERP.Shared;
using BusinessAccessLayer.Quality;
using DocumentObjectModel;
using BusinessAccessLayer;
using System.IO;

namespace MiniERP.Quality
{
    public partial class ViewSupplierReceiveMaterial : BasePage
    {
        #region Global Variable(s)
        Int32 dayscount = Convert.ToInt32(ConfigurationManager.AppSettings["No_of_Days"]);
        SupplierRecieveMaterialBAL SupplierReceiveMatBal = new SupplierRecieveMaterialBAL();
        QuotationDOM quotation;
        SupplierRecieveMatarial supplierRecieveMatarial;
        List<QuotationDOM> lstQuotation = null;
        List<SupplierRecieveMatarial> lstSupplierRecieveMatarial = null;
        List<ItemTransaction> lstItemtransaction = null;
        #endregion

        #region Protected Method(s)
        protected void Page_Load(object sender, EventArgs e)
        {
            PageDefaults();
            
            if (!IsPostBack)
            {
               ToDate = DateTime.Now;
                FromDate = DateTime.Now.AddDays(-dayscount);
                CalExtFromDate.EndDate = DateTime.Now;
                CalExtToDate.EndDate = DateTime.Now;
                BindGrid(String.Empty, String.Empty, String.Empty, ToDate, FromDate);
                txtFromDate.Text = FromDate.ToString("dd/MM/yyyy");
                txtToDate.Text = ToDate.ToString("dd/MM/yyyy"); ;
                
            }
        }

        private void PageDefaults()
        {
            Page_Name = (Request.Url.LocalPath).Split('/').Last().Split('.').First();
        }

        protected void lnkbtnSearch_Click(object sender, EventArgs e)
        {
            ResetViewState();
            BindEmptyGrid(gvRSM);
            ChallanNo = txtDelChallanNo.Text;
            BindGrid(PurchaseOrderNumber, ChallanNo, String.Empty, ToDate, FromDate);
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            ResetViewState();
            BindEmptyGrid(gvRSM);
           
            PurchaseOrderNumber = txtPurchaseOrderNumber.Text;
            if (!string.IsNullOrEmpty(txtFromDate.Text))
            {
                FromDate = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            }
            if (!string.IsNullOrEmpty(txtToDate.Text))
            {
                ToDate = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            }
            if (!string.IsNullOrEmpty(txtFromDate.Text) && !string.IsNullOrEmpty(txtToDate.Text))
            {
                FromDate = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                ToDate = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            }
            BindGrid(PurchaseOrderNumber, ChallanNo, String.Empty, ToDate, FromDate);
           // ResetControl();
        }
        protected void gvRSM_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton lnkEdit = (LinkButton)e.Row.FindControl("lnkEdit");
                LinkButton lnkGenerate = (LinkButton)e.Row.FindControl("lnkGenerate");
                LinkButton lnkPrint = (LinkButton)e.Row.FindControl("lnkPrint");
                LinkButton lnkDelete = (LinkButton)e.Row.FindControl("lnkDelete");
                HiddenField hiddenfld = (HiddenField)e.Row.FindControl("hdfStatusId");
                int Status = Convert.ToInt32((hiddenfld).Value);
                if (Convert.ToInt32(StatusType.Generated) == Status)
                {
                    lnkEdit.Visible = false;
                    lnkGenerate.Visible = false;
                    lnkDelete.Visible = false;
                }
                else if (Convert.ToInt32(StatusType.Generated) != Status)
                {
                    lnkPrint.Visible = true;
                    lnkGenerate.Visible = true;
                    lnkEdit.Visible = true;
                }
            }
        }
        protected void gvRSM_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            String str = Convert.ToString(e.CommandArgument);
            String[] strid = str.Split(',');
            int SRMId = Convert.ToInt32(strid[0].ToString());

            lstItemtransaction = new List<ItemTransaction>();
            if (e.CommandName == "lnkSRMDetails")
            {
                lstItemtransaction = SupplierReceiveMatBal.ReadSupplierReceiveMaterialMapping(SRMId);
                BindEmptyGrid(gvItemInfo);
                if (lstItemtransaction.Count > 0)
                {
                    gvItemInfo.DataSource = lstItemtransaction;
                    gvItemInfo.DataBind();

                    //For Document
                    GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                    HiddenField hdfc = (HiddenField)row.FindControl("hdf_documnent_Id");
                    if (hdfc != null)
                        updcFile.GetDocumentData(Convert.ToInt32(hdfc.Value));
                    else
                        updcFile.GetDocumentData(Int32.MinValue);
                    //End............
                }
                ModalPopupExtender2.Show();
            }
            if (e.CommandName == "cmdEdit")
            {
                Response.Redirect("~/Quality/SupplierReceiveMaterial.aspx?SupplierReceiveMaterialId=" + SRMId);
            }
            if (e.CommandName == "cmdPrint")
            {
                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                LinkButton lbtn = (LinkButton)row.FindControl("lnkPrint");
                String SRMNo = strid[1].ToString();
                // Response.Redirect("~/SSRReport/ReceiveMaterialReport.aspx?SupplierReceiveMaterialId=" + SRMId + "&SuppReceiveNo=" + SRMNo + "");
                OpenPopupWithUpdatePanelForReport(lbtn, "../SSRReport/ReceiveMaterialReport.aspx?SupplierReceiveMaterialId=" + SRMId + "&SuppReceiveNo=" + SRMNo + "", "ReceiveMaterial");
            }
            if (e.CommandName == "cmdDelete")
            {
                String Message = SupplierReceiveMatBal.DeleteSupplierReceiveMaterial(SRMId, LoggedInUser.UserLoginId, DateTime.Now);
                if (Message == "")
                {
                    ShowMessageWithUpdatePanel("Receive Material is Deleted Successfully");
                }
                else
                {
                    ShowMessageWithUpdatePanel(Message);
                }
                BindGrid(String.Empty, String.Empty, String.Empty, ToDate, FromDate);
            }
            if (e.CommandName == "cmdGenerate")
            {

                supplierRecieveMatarial = new SupplierRecieveMatarial();
                supplierRecieveMatarial.SupplierRecieveMatarialId = SRMId;
                supplierRecieveMatarial.Quotation = new QuotationDOM();
                supplierRecieveMatarial.Quotation.StatusType = new MetaData();
                supplierRecieveMatarial.Quotation.StatusType.Id = Convert.ToInt32(StatusType.Generated);
                supplierRecieveMatarial.Quotation.GeneratedBy = LoggedInUser.UserLoginId;
                int outId = SupplierReceiveMatBal.UpdateSupplierReceiveMaterialStatus(supplierRecieveMatarial);
                if (outId > 0)
                {
                    BindGrid(String.Empty, String.Empty, String.Empty, ToDate, FromDate);
                    ShowMessageWithUpdatePanel("Recieve Material Is Generated Successfully");
                }
            }
        }
        protected void gvRSM_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvRSM.PageIndex = e.NewPageIndex;
            BindGrid(PurchaseOrderNumber, ChallanNo, String.Empty, ToDate, FromDate);
        }
        protected void LnkBtn_Click(object sender, EventArgs e)
        {
            txtFromDate.Text = String.Empty;
        }
        protected void lnkBtnClear_Click(object sender, EventArgs e)
        {
            txtToDate.Text = String.Empty;
        }
        #endregion

        #region Private Method(s)
        public void BindGrid(String SupplierPONumber, String ChallanNo, String SupplierReceiveMaterialNo, DateTime ToDate, DateTime FromDate)
        {
            //lstQuotation = new List<QuotationDOM>();
            lstSupplierRecieveMatarial = new List<SupplierRecieveMatarial>();
            lstSupplierRecieveMatarial = SupplierReceiveMatBal.SearchReceiveMaterial(SupplierPONumber, ChallanNo, SupplierReceiveMaterialNo, ToDate, FromDate,null);
            if (lstSupplierRecieveMatarial.Count > 0)
            {
                gvRSM.DataSource = lstSupplierRecieveMatarial;
                gvRSM.DataBind();


            }
            else
            {
                BindEmptyGrid(gvRSM);
            }
           // ResetControl();
        }
        private void ResetControl()
        {
            txtDelChallanNo.Text = String.Empty;
            txtFromDate.Text = String.Empty;
            txtPurchaseOrderNumber.Text = String.Empty;
            txtToDate.Text = String.Empty;
        }
        private void ResetViewState()
        {
            ToDate = DateTime.MinValue;
            FromDate = DateTime.MinValue;
            ChallanNo = String.Empty;
            SupplierReceiveMaterialNo = String.Empty;
        }
        private void BindEmptyGrid(GridView gv)
        {
            gv.DataSource = new List<object>();
            gv.DataBind();
        }
        #endregion

        #region Public Propertie(s)
        public DateTime FromDate
        {
            get
            {
                try
                {
                    return Convert.ToDateTime(ViewState["FromDate"]);
                }
                catch
                {
                    return DateTime.MinValue;
                }
            }
            set { ViewState["FromDate"] = value; }
        }
        public DateTime ToDate
        {
            get
            {
                try
                {
                    return Convert.ToDateTime(ViewState["ToDate"]);
                }
                catch
                {
                    return DateTime.MinValue;
                }
            }
            set { ViewState["ToDate"] = value; }
        }
        public String ChallanNo
        {
            get
            {
                try
                {
                    return Convert.ToString(ViewState["ChallanNo"]);
                }
                catch
                {
                    return String.Empty;
                }
            }
            set { ViewState["ChallanNo"] = value; }
        }
        public String SupplierReceiveMaterialNo
        {
            get
            {
                try
                {
                    return Convert.ToString(ViewState["SupplierReceiveMaterialNo"]);
                }
                catch
                {
                    return String.Empty;
                }
            }
            set { ViewState["SupplierReceiveMaterialNo"] = value; }
        }

        public String PurchaseOrderNumber
        {
            get
            {
                try
                {
                    return Convert.ToString(ViewState["PurchaseOrderNumber"]);
                }
                catch
                {
                    return String.Empty;
                }
            }
            set { ViewState["PurchaseOrderNumber"] = value; }
        }
        #endregion


    }
}