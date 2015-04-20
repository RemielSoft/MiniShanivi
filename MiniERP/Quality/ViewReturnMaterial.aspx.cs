using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using MiniERP.Shared;
using DocumentObjectModel;
using BusinessAccessLayer;
using BusinessAccessLayer.Quality;

namespace MiniERP.Quality
{
    public partial class ViewReturnMaterial : BasePage
    {
        #region Global Varriable(s)
        Int32 DaysCount = Convert.ToInt32(ConfigurationManager.AppSettings["No_of_Days"]);
        SupplierBL supplierBL = new SupplierBL();
        List<ReturnMaterialDOM> lstReturnMaterialDOM = null;
        ReturnMaterialDOM returnMaterialDOM = null;
        ReturnMaterialBL returnMaterialBL = new ReturnMaterialBL();
        string Message;
        
        #endregion

        #region Protected Method(s)
        protected void Page_Load(object sender, EventArgs e)
        {
            PageDefaults();
            if (!IsPostBack)
            {                
                this.FromDate = DateTime.Now.AddDays(-DaysCount);
                this.ToDate = DateTime.Now;

                txtFromDate.Text = this.FromDate.ToString("dd'/'MM'/'yyyy");
                txtToDate.Text = this.ToDate.ToString("dd'/'MM'/'yyyy");                
                BindDropDown(ddlSupplierName, "Name", "SupplierId", supplierBL.ReadSupplier(null));                
                BindGridReturnMaterial(null, null, null, null, null, null, FromDate, ToDate);
                CalExtFromDate.EndDate = DateTime.Now;
                CalExtToDate.EndDate = DateTime.Now;
            }
        }

        private void PageDefaults()
        {
            Page_Name = (Request.Url.LocalPath).Split('/').Last().Split('.').First();

        }
        
        protected void btnSearch_Click(object sender, EventArgs e)
        {

            //FromDate = Convert.ToDateTime(txtFromDate.Text);

            string FDate = txtFromDate.Text;
            FromDate = DateTime.ParseExact(FDate, "dd/MM/yyyy", null);
            string TDate = txtToDate.Text;
            ToDate = DateTime.ParseExact(TDate, "dd/MM/yyyy", null);
            //ToDate = Convert.ToDateTime(txtToDate.Text);
            if (!string.IsNullOrEmpty(txtFromDate.Text))
            {
                FromDate = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            }
            if (!string.IsNullOrEmpty(txtFromDate.Text))
            {
                ToDate = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

            }
            if (!string.IsNullOrEmpty(txtFromDate.Text) && !string.IsNullOrEmpty(txtToDate.Text))
            {
                FromDate = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                ToDate = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            }

            // Compare that to date must be greater than from date
            if (ToDate < FromDate && (ToDate != DateTime.MinValue && FromDate != DateTime.MinValue))
            {
                Alert("To Date should be Greater Than From Date", btnSearch);
            }
            if (Convert.ToInt32(ddlSupplierName.SelectedValue) == 0)
            {
                BindGridReturnMaterial(null, null, null, null,null, null, FromDate, ToDate);
            }
            else
            {
                BindGridReturnMaterial(null, null, null, null, Convert.ToInt32(ddlSupplierName.SelectedValue), null, FromDate, ToDate);
            }
            

            //ResetControl();
        }

        protected void lnkSearch_Click(object sender, EventArgs e)
        {
            if (rbtn.SelectedValue == "1")
            {
                BindGridReturnMaterial(null, txtSearch.Text.ToString(), null, null, null, null, FromDate, ToDate);
            }
            else if (rbtn.SelectedValue == "2")
            {
                BindGridReturnMaterial(null, null, null, txtSearch.Text.ToString(), null, null, FromDate, ToDate);
            }
            else if(rbtn.SelectedValue=="3")
            {
                BindGridReturnMaterial(null, null, null, null, null, txtSearch.Text.ToString(), FromDate, ToDate);
            }
            //ResetControl();
        }
        protected void lnkbtnClear_Click(object sender, EventArgs e)
        {
            txtFromDate.Text = string.Empty;
        }

        protected void lnkbtn_Click(object sender, EventArgs e)
        {
            txtToDate.Text = string.Empty;
        }
        protected void gvReturnMaterial_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string str = Convert.ToString(e.CommandArgument);
            string[] strid = str.Split(',');
            int id = Convert.ToInt32(strid[0].ToString());

            if (e.CommandName == "lnkEdit")
            {
                Response.Redirect("~/Quality/ReturnMaterial.aspx?ReturnMaterialId=" + id);
            }
            else if (e.CommandName == "lnkPrint")
            {
                string ReturnMaterialNo = strid[1].ToString();
                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                LinkButton lbtn = (LinkButton)row.FindControl("LinkPrint");
                OpenPopupWithUpdatePanelForReport(lbtn, "../SSRReport/ReturnMaterialRpt.aspx?ReturnMaterialNo=" + ReturnMaterialNo + "", "ReturnMaterialNote");

            }
            else if (e.CommandName == "lnkDelete")
            {

                Message = returnMaterialBL.DeleteReturnMaterial(id, LoggedInUser.UserLoginId);

                if (Message == "")
                {
                    Alert("Return Material is Deleted Successfully", gvReturnMaterial);
                }
                else
                {
                    Alert(Message, gvReturnMaterial);
                }
                BindGridReturnMaterial(null, null, null, null, null, null, FromDate, ToDate);
            }
            else if (e.CommandName == "lnkGenerate")
            {
                returnMaterialDOM = new ReturnMaterialDOM();
                returnMaterialDOM.RecieveMatarial = new SupplierRecieveMatarial();
                returnMaterialDOM.RecieveMatarial.Quotation = new QuotationDOM();
                //Invoice.IssueMaterial.DemandVoucher.Quotation = new QuotationDOM();
                returnMaterialDOM.RetutrnMaterialId= id;
                returnMaterialDOM.RecieveMatarial.Quotation.StatusType = new MetaData();
                //quotation.StatusType = new MetaData();
                returnMaterialDOM.RecieveMatarial.Quotation.StatusType.Id = Convert.ToInt32(StatusType.Generated);
                returnMaterialDOM.ModifiedBy =LoggedInUser.UserLoginId;
                int outId = returnMaterialBL.UpdateSupplierReturnMaterialStatus(returnMaterialDOM);
                if (outId > 0)
                {
                    BindGridReturnMaterial(null, null, null, null, null, null, FromDate, ToDate);
                    Alert("Return Material Is Generated Successfully", gvReturnMaterial);
                }
            }
            else if (e.CommandName == "lnkReturnMaterialDetails")
            {
                String ReturnMaterialNo = strid[1].ToString();
                List<ItemTransaction> lstItemTransaction = new List<ItemTransaction>();
                lstItemTransaction = returnMaterialBL.ReadSupplierReturnMaterialMapping(null, ReturnMaterialNo);
                BindEmptyGrid(gvItemInfo);
                if (lstItemTransaction.Count > 0)
                {
                    gvItemInfo.DataSource = lstItemTransaction;
                    gvItemInfo.DataBind();

                   // For Document
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
        }
        protected void gvReturnMaterial_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                LinkButton lnkEdit = (LinkButton)e.Row.FindControl("lnkEdit");
                LinkButton lnkDelete = (LinkButton)e.Row.FindControl("LinkDelete");
                LinkButton lnkGenerate = (LinkButton)e.Row.FindControl("LinkGenQuot");
                LinkButton lnkPrint = (LinkButton)e.Row.FindControl("LinkPrint");
                HiddenField hiddenfld = (HiddenField)e.Row.FindControl("hdfStatusTypeId");

                int Status = Convert.ToInt32((hiddenfld).Value);
                //if (Convert.ToInt32(StatusType.Approved) == Status)
                //{
                //    lnkEdit.Visible = false;
                //    lnkDelete.Visible = false;
                //}
                if (Convert.ToInt32(StatusType.Generated) == Status)
                {
                    lnkDelete.Visible = false;
                    lnkEdit.Visible = false;
                    lnkGenerate.Visible = false;
                }
                else if (Convert.ToInt32(StatusType.Pending) == Status)
                {
                    lnkGenerate.Visible = true;
                    lnkDelete.Visible = true;
                    lnkEdit.Visible = true;
                    lnkGenerate.Visible = true;
                }
                else if (Convert.ToInt32(StatusType.Rejected) == Status)
                {
                    lnkDelete.Visible = false;
                    lnkEdit.Visible = false;
                    lnkGenerate.Visible = false;
                }
                HiddenField hdf_doc_id = (HiddenField)e.Row.FindControl("hdf_documnent_Id");

                ReturnMaterialDOM ob = (ReturnMaterialDOM)e.Row.DataItem;
                hdf_doc_id.Value = ob.RecieveMatarial.Quotation.UploadDocumentId.ToString();
            }
        }
        #endregion

        #region Private Method(s)

        public void BindEmptyGrid(GridView gv)
        {
            gv.DataSource = new List<object>();
            gv.DataBind();
        }
        private void BindGridReturnMaterial(Int32? ReturnMaterialId, String ReturnMaterialNo, String ReceiveMaterialNo, String SupplierPONo, Int32? SupplieId,String DeliveryChallanNo, DateTime FromDate, DateTime EndDate)
        {
            returnMaterialBL = new ReturnMaterialBL();
            lstReturnMaterialDOM = new List<ReturnMaterialDOM>();
            lstReturnMaterialDOM = returnMaterialBL.ReadSupplierReturnMaterial(ReturnMaterialId, ReturnMaterialNo, ReceiveMaterialNo, SupplierPONo, SupplieId, DeliveryChallanNo, FromDate, EndDate);
            gvReturnMaterial.DataSource = lstReturnMaterialDOM;
            gvReturnMaterial.DataBind();
        }
        public void ResetControl()
        {
            txtFromDate.Text = string.Empty;
            txtToDate.Text = string.Empty;
            txtSearch.Text = string.Empty;
            ddlSupplierName.SelectedValue = "0";
        }
        #endregion

        #region Public Property(s)
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

        

        
        //public Int32 SupplierId
        //{
        //    get
        //    {
        //        try
        //        {
        //            return Convert.ToInt32(ViewState["SupplierId"]);
        //        }
        //        catch
        //        {
        //            return 0;
        //        }
        //    }
        //    set { ViewState["SupplierId"] = value; }
        //}
        #endregion
    }
}