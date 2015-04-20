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
using BusinessAccessLayer.Invoice;
using BusinessAccessLayer.Quality;

namespace MiniERP.Quality
{
    public partial class ViewIssueMaterial : BasePage
    {
        #region Global Varriables
        Int32 DaysCount = Convert.ToInt32(ConfigurationManager.AppSettings["No_of_Days"]);
        string NoUnit = string.Empty;
        string UnitDemand = string.Empty;
        decimal TotalUnitIssued;
        ContractorBL contractorBL = new ContractorBL();
        CompanyWorkOrderBL CompWorkOrderBL = new CompanyWorkOrderBL();
        IssueMaterialBL issueMaterialBL = new IssueMaterialBL();
        IssueMaterialDOM issueMaterial = null;
        List<IssueMaterialDOM> lstissueMaterial = null;
        List<ItemTransaction> lstItemTransaction = null;

        #endregion

        #region Protected Methods

        protected void Page_Load(object sender, EventArgs e)
        {
            PageDefaults();
            if (!IsPostBack)
            {
                ToDate = DateTime.Now;
                FromDate = DateTime.Now.AddDays(-DaysCount);
                BindDropDown(ddlContName, "Name", "ContractorId", contractorBL.ReadContractor(null));
                BindDropDown(ddlContractNo, "ContractNumber", "CompanyWorkOrderId", CompWorkOrderBL.ReadCompOrder(null));
                CalExtfromDate.EndDate = DateTime.Now;
                CalExtToDate.EndDate = DateTime.Now;
                BindGrid(null, 0, ToDate, FromDate, string.Empty, string.Empty, string.Empty);
                txtFromDate.Text = FromDate.ToString("dd/MM/yyyy");
                txtToDate.Text = ToDate.ToString("dd/MM/yyyy");
            }

        }

        private void PageDefaults()
        {
            Page_Name = (Request.Url.LocalPath).Split('/').Last().Split('.').First();
        }

        protected void LinkSearch_Click(object sender, EventArgs e)
        {
            ResetViewState();
            if (rbtn.SelectedValue == "1")
            {
                IssueMaterialNo = txtIssueNo.Text;
                BindGrid(null, ContractorId, ToDate, FromDate, ContractNo, IssueMaterialNo, ContractorQuotNo);
            }
            else if (rbtn.SelectedValue == "2")
            {
                ContractorQuotNo = txtIssueNo.Text;
                BindGrid(null, ContractorId, ToDate, FromDate, ContractNo, IssueMaterialNo, ContractorQuotNo);
            }

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindEmptyGrid(gvIssueMaterialNo);
            ResetViewState();
            if (ddlContractNo.SelectedItem.Text != "--Select--")
                ContractNo = Convert.ToString(ddlContractNo.SelectedItem.Text);
            ContractorId = Convert.ToInt32(ddlContName.SelectedValue);

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
            if (ToDate < FromDate)
            {
                ShowMessageWithUpdatePanel("To Date should be Greater Than From Date");
            }
            BindGrid(null, ContractorId, ToDate, FromDate, ContractNo, IssueMaterialNo, ContractorQuotNo);
            //ResetControles();
        }

        protected void gvIssueMaterialNo_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvIssueMaterialNo.PageIndex = e.NewPageIndex;
            BindGrid(null, ContractorId, ToDate, FromDate, ContractNo, IssueMaterialNo, ContractorQuotNo);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="itemTransactions"></param>
        private void BindMainGrid(List<ItemTransaction> itemTransactions)
        {

            var finalList = itemTransactions.AsEnumerable()
            .Select(x => new
            {
                x.Item.ItemId,
                x.Item.ItemName,
                x.Item.ModelSpecification.ModelSpecificationId,
                x.Item.ModelSpecification.ModelSpecificationName,
                x.NumberOfUnit,
                x.ItemRequired,
                x.UnitLeft,
                x.Item.ModelSpecification.Category.ItemCategoryId,
                x.Item.ModelSpecification.Category.ItemCategoryName,
                x.Item.ModelSpecification.UnitMeasurement.Name,
                x.Item.ModelSpecification.UnitMeasurement.Id,
                x.DeliverySchedule.ActivityDescription


            })
            .GroupBy(a => new
            {
                a.ItemId,
                a.ItemName,
                a.ModelSpecificationId,
                a.ModelSpecificationName,
                a.NumberOfUnit,
                a.ItemCategoryId,
                a.ItemCategoryName,
                a.Id,
                a.Name,
                a.ActivityDescription

            })
    .Select(b => new
    {
        ItemId = b.Key.ItemId,
        ItemName = b.Key.ItemName,
        ItemSpecificationId = b.Key.ModelSpecificationId,
        ItemSpecification = b.Key.ModelSpecificationName,
        ItemCategoryId = b.Key.ItemCategoryId,
        ItemCategoryName = b.Key.ItemCategoryName,
        UnitMeasurement = b.Key.Name,
        IssuedQuantity = b.Sum(z => z.ItemRequired),
        AvailableQuantity = b.Key.NumberOfUnit - b.Sum(z => z.ItemRequired),
        QuantityDemand = b.Key.NumberOfUnit,
        ActivityDescription = b.Key.ActivityDescription
    }).ToList();

            // Bind Main Grid
            gvMainGrid.DataSource = finalList;
            gvMainGrid.DataBind();


        }
        protected void gvIssueMaterialNo_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            String Message;

            string str = Convert.ToString(e.CommandArgument);
            string[] strid = str.Split(',');
            int Id = Convert.ToInt32(strid[0].ToString());
            if (e.CommandName == "lnkIMNoDetails")
            {
                lstItemTransaction = new List<ItemTransaction>();
                lstItemTransaction = issueMaterialBL.ReadIssueMaterialMapping(Id);
                BindEmptyGrid(gvItemInfo);
                if (lstItemTransaction.Count > 0)
                {
                    BindMainGrid(lstItemTransaction);
                    gvItemInfo.DataSource = lstItemTransaction;
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
                Response.Redirect("~/Quality/IssuingMaterial.aspx?IssueMaterialId=" + Id);
            }

            if (e.CommandName == "cmdPrint")
            {
                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                LinkButton lbtn = (LinkButton)row.FindControl("LinkPrint");

                //String IssueId = strid[1].ToString();
                //Response.Redirect("~/SSRReport/IssuingMaterial.aspx?IssueMaterialId=" + Id);
                OpenPopupWithUpdatePanelForReport(lbtn, "../SSRReport/IssueMaterialReport.aspx?IssueMaterialId=" + Id + "", "IssueMaterial");
            }
            if (e.CommandName == "cmdDelete")
            {
                Message = issueMaterialBL.DeleteIssueMaterial(Id, LoggedInUser.UserLoginId, DateTime.Now);
                if (Message == "")
                {
                    ShowMessageWithUpdatePanel("Issue Material is Deleted Successfully");
                }
                else
                {
                    ShowMessageWithUpdatePanel(Message);
                }
                BindGrid(null, ContractorId, ToDate, FromDate, ContractNo, IssueMaterialNo, ContractorQuotNo);
            }
            if (e.CommandName == "cmdGenerate")
            {
                issueMaterial = new IssueMaterialDOM();
                issueMaterial.IssueMaterialId = Id;
                issueMaterial.DemandVoucher = new IssueDemandVoucherDOM();
                issueMaterial.DemandVoucher.Quotation = new QuotationDOM();
                issueMaterial.DemandVoucher.Quotation.StatusType = new MetaData();
                //quotation.StatusType = new MetaData();
                issueMaterial.DemandVoucher.Quotation.StatusType.Id = Convert.ToInt32(StatusType.Generated);
                issueMaterial.DemandVoucher.Quotation.GeneratedBy = LoggedInUser.UserLoginId;
                int outId = issueMaterialBL.UpdateIssueMaterialStatus(issueMaterial);
                if (outId > 0)
                {
                    BindGrid(null, ContractorId, ToDate, FromDate, ContractNo, IssueMaterialNo, ContractorQuotNo);

                }
            }

        }

        protected void gvIssueMaterialNo_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton lnkEdit = (LinkButton)e.Row.FindControl("lnkEdit");
                LinkButton lnkDelete = (LinkButton)e.Row.FindControl("LinkDelete");
                LinkButton lnkGenerate = (LinkButton)e.Row.FindControl("LinkGenQuot");
                LinkButton lnkPrint = (LinkButton)e.Row.FindControl("LinkPrint");
                HiddenField hiddenfld = (HiddenField)e.Row.FindControl("hdfStatusId");
                int Status = Convert.ToInt32((hiddenfld).Value);
                if (Convert.ToInt32(StatusType.Generated) == Status)
                {
                    lnkDelete.Visible = false;
                    lnkEdit.Visible = false;
                    lnkGenerate.Visible = false;
                }
                else if (Convert.ToInt32(StatusType.Generated) != Status)
                {
                    lnkPrint.Visible = true;
                    lnkGenerate.Visible = true;
                    lnkDelete.Visible = true;
                    lnkEdit.Visible = true;
                }
            }
        }

        protected void lnkFromDate_Click(object sender, EventArgs e)
        {
            txtFromDate.Text = String.Empty;
        }

        protected void lnkToDate_Click(object sender, EventArgs e)
        {
            txtToDate.Text = String.Empty;
        }

        #endregion

        #region Private Methods
        private void BindGrid(Int32? IssueMaterialId, Int32 ContractorId, DateTime ToDate, DateTime FromDate, String ContractNo, String IssueMaterialNo, String ContractorQuotNo)
        {
            lstissueMaterial = new List<IssueMaterialDOM>();
            lstissueMaterial = issueMaterialBL.ReadIssueMaterial(IssueMaterialId, ContractorId, ToDate, FromDate, ContractNo, IssueMaterialNo, ContractorQuotNo);
            if (lstissueMaterial.Count > 0)
            {
                gvIssueMaterialNo.DataSource = lstissueMaterial;
                gvIssueMaterialNo.DataBind();
            }
            else
            {
                BindEmptyGrid(gvIssueMaterialNo);
            }
        }
        private void BindEmptyGrid(GridView gv)
        {
            gv.DataSource = new List<object>();
            gv.DataBind();
        }
        private void ResetControles()
        {
            ddlContName.SelectedValue = "0";
            ddlContractNo.SelectedValue = "0";
            txtFromDate.Text = String.Empty;
            txtToDate.Text = String.Empty;
            txtIssueNo.Text = String.Empty;
        }
        private void ResetViewState()
        {
            IssueMaterialNo = String.Empty;
            ContractorQuotNo = String.Empty;
            ContractNo = String.Empty;
            ToDate = DateTime.MinValue;
            FromDate = DateTime.MinValue;
            ContractorId = 0;
        }
        #endregion

        #region Private Properties

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
        public String ContractNo
        {
            get
            {
                try
                {
                    return (String)ViewState["ContractNo"];
                }
                catch
                {
                    return String.Empty;
                }
            }
            set { ViewState["ContractNo"] = value; }
        }
        public String IssueMaterialNo
        {
            get
            {
                try
                {
                    return (String)ViewState["IssueMaterialNo"];
                }
                catch
                {
                    return String.Empty;
                }
            }
            set { ViewState["IssueMaterialNo"] = value; }
        }
        public String ContractorQuotNo
        {
            get
            {
                try
                {
                    return (String)ViewState["ContractorQuotNo"];
                }
                catch
                {
                    return String.Empty;
                }
            }
            set { ViewState["ContractorQuotNo"] = value; }
        }
        public Int32 ContractorId
        {
            get
            {
                try
                {
                    return (Int32)ViewState["ContractorId"];
                }
                catch
                {
                    return 0;
                }
            }
            set { ViewState["ContractorId"] = value; }
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvItemInfo_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowIndex < 1)
                {
                    NoUnit = DataBinder.Eval(e.Row.DataItem, "NumberOfUnit").ToString();
                    UnitDemand = DataBinder.Eval(e.Row.DataItem, "UnitDemanded").ToString();
                }
                TotalUnitIssued += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "ItemRequired"));

            }
            //if (e.Row.RowType == DataControlRowType.Footer)
            //{
            //    Label lblNoOfUnit = (Label)e.Row.FindControl("lblNOF");
            //    Label lblUnitDemanded = (Label)e.Row.FindControl("lblItemReq");
            //    Label lblItemIssueTotal = (Label)e.Row.FindControl("lblItemIssuetotal");
            //    lblNoOfUnit.Text = NoUnit;
            //    lblUnitDemanded.Text = UnitDemand;
            //    lblItemIssueTotal.Text = TotalUnitIssued.ToString();
            //}
        }

    }
}