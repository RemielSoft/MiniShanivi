using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccessLayer;
using DocumentObjectModel;
using BusinessAccessLayer;
using MiniERP.Shared;
using System.Text;
using System.Drawing;
using System.IO;
using System.Data;
using System.Configuration;
using System.Net;
using BusinessAccessLayer.Quality;
using System.Globalization;

namespace MiniERP.StockMaterial
{
    public partial class ReceiveMaterial : BasePage
    {

        #region Private Global Variables

        int i = 0;
        int j = 0;
        int k = 0;
        int count = 0;
        Boolean flag = false;
        int cnt = 0;
        string strInvalid = string.Empty;
        decimal dec = 0;
        decimal BigNo = 0;
        string strnull = string.Empty;
        string str = string.Empty;
        MetaData metaData = new MetaData();
        SupplierRecieveMaterialBAL supplierRecieveMaterialBL = null;
        List<QuotationDOM> lstQuotation = null;
        List<SupplierRecieveMatarial> lstSupplierRecieveMatarial = null;
        QuotationDOM quotationDOM = null;
        SupplierRecieveMatarial supplierRecieveMatarial = null;
        //lstItemTransaction = null;
        ItemTransaction itemTransaction = null;
        QuotationBL quotationBL = new QuotationBL();
        HiddenField hdfItemId = null;
        HiddenField hdnfStore = null;
        HiddenField hdnfBrand = null;
        HiddenField hdfSupplierPOMappingId = null;
        Label lblItem = null;
        HiddenField hdfSpecificationId = null;
        HiddenField hdfUnitMeasurementId = null;
        Label lblSpecification = null;
        Label lblItemCategory = null;
        Label lblBrand = null;
        Label lblStore = null;
        Label lblNOF = null;
        Label lblItemRecieve = null;
        Label lblItemLeft = null;
        Label lblMeasurement = null;
        CheckBox chkSelect = null;
        decimal itemQuantity = 0;
        decimal itemLeft = 0;

        Label lblIndex = null;

        String pageName = String.Empty;

        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            pageName = (Request.Url.LocalPath).Split('/').Last().Split('.').First();
            if (!IsPostBack)
            {
                lstQuotation = new List<QuotationDOM>();
                lstQuotation = quotationBL.ReadSupplierQuotationView(0, DateTime.MinValue, DateTime.MinValue, null);
                var lst = lstQuotation.Select(a => new { a.SupplierName, a.SupplierId }).Distinct().ToList().OrderBy(a => a.SupplierName);

                BindDropDown(ddlSupplierReceiveMaterial, "SupplierName", "SupplierId", lst);




                //GetPagePostBack();
                CalendarExtender1.EndDate = DateTime.Now;
                //txtRMDate.Text = DateTime.Now.ToString("dd'/'MM'/'yyyy");
                if (Request.QueryString["SupplierReceiveMaterialId"] != null)
                {
                    SupplierRecieveMaterialId = 0;
                    quotationBL = new QuotationBL();
                    lstSupplierRecieveMatarial = new List<SupplierRecieveMatarial>();
                    lstItemTransaction = new List<ItemTransaction>();
                    supplierRecieveMaterialBL = new SupplierRecieveMaterialBAL();
                    lstItemTransactionAdd = new List<ItemTransaction>();
                    lstQuotation = new List<QuotationDOM>();
                    SupplierRecieveMaterialId = Convert.ToInt32(Request.QueryString["SupplierReceiveMaterialId"]);
                    lstSupplierRecieveMatarial = supplierRecieveMaterialBL.ReadSupplierReceiveMaterial(SupplierRecieveMaterialId, null);
                    GetDocumentData(lstSupplierRecieveMatarial);
                    //lstItemTransaction1 = supplierRecieveMaterialBL.ReadSupplierReceiveMaterial(SupplierRecieveMaterialId);
                    //lstItemTransaction = supplierRecieveMaterialBL.ReadSupplierReceiveMaterialMapping(SupplierRecieveMaterialId); 
                    lstItemTransactionAdd = supplierRecieveMaterialBL.ReadSupplierReceiveMaterialMapping(SupplierRecieveMaterialId);
                    lstQuotation = quotationBL.ReadSupplierQuotation(null, lstSupplierRecieveMatarial[0].Quotation.SupplierQuotationNumber.Trim());
                    BindSupplier(lstQuotation);
                    BindUpdateText(lstSupplierRecieveMatarial);
                    BindAddGridSupplier();
                    pnlSearch.Visible = false;
                    pnlRecieveMetarial.Visible = true;
                    Enabled(true);
                    // CalculateItemLeft();
                    //  BindAddGridSupplier();
                    //gvSupplier.DataSource = lstItemTransaction;
                    //gvSupplier.DataBind();

                }
                else if (!IsPostBack && Request.QueryString["SupplierReceiveMaterialId"] == null)
                {
                    //LinkSearch_Click(null,null);
                    pnlRecieveMetarial.Visible = false;
                    pnlSearch.Visible = true;
                    Enabled(false);
                    Session["SupplierRecieveMaterialId"] = SupplierRecieveMaterialId;
                    //txtOrderNumber.Focus();
                    //txtOrderNumber.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('" + LinkSearch.ClientID + "').click();return false;}} else {return true}; ");
                }
            }
        }


        private void GetPagePostBack()
        {
            if (Session["CNT"] == null)
                Session["CNT"] = i + 1;
            else
                Session["CNT"] = Convert.ToInt32(Session["CNT"]) + 1;
        }

        protected void LinkSearch_Click(object sender, EventArgs e)
        {
            //quotationBL = new QuotationBL();
            quotationDOM = new QuotationDOM();
            lstQuotation = new List<QuotationDOM>();
            quotationDOM.StatusType = new MetaData();

            QuotationNumber = txtOrderNumber.Text.Trim();
            //Session["lstQuotation"] = null;
            lstQuotation = quotationBL.ReadSupplierQuotation(null, QuotationNumber);
            if (string.IsNullOrEmpty(txtOrderNumber.Text.ToString()))
            {
                Alert("Please enter Valid Supplier purchase order number", LinkSearch);
            }
            else if (lstQuotation.Count > 0 && (lstQuotation[0].StatusType.Id == Convert.ToInt32(StatusType.Generated)))
            {


                ltrl_err_msg.Text = string.Empty;
                txtRMDate.Text = DateTime.Now.ToString("dd'/'MM'/'yyyy");

                BindAddGridSupplier();

                SetAllDefaultData(lstQuotation);
                pnlRecieveMetarial.Visible = true;
                Enabled(false);


                //txtOrderNumber.Text = string.Empty;

            }
            else if (lstQuotation.Count > 0 && lstQuotation[0].StatusType.Id <= 2)
            {
                Alert("Suppplier Purchase Order No. is not Generated!", LinkSearch);

            }
            else
            {
                Alert("Invalid Suppplier Purchase Order Number!", LinkSearch);
                pnlRecieveMetarial.Visible = false;

            }


        }

        protected void chbxSelectAll_Click(object sender, EventArgs e)
        {

            foreach (GridViewRow row in gvMainGrid.Rows)
            {
                chkSelect = (CheckBox)row.FindControl("chkSelect");
                if (chkSelect.Enabled == true)
                {
                    chkSelect.Checked = ((CheckBox)sender).Checked;
                }
            }


        }

        protected void chkSelect_Click(object sender, EventArgs e)
        {
            flag = false;
            CheckBox chkSelectAll = (CheckBox)gvMainGrid.HeaderRow.FindControl("chbxSelectAll");
            foreach (GridViewRow row in gvMainGrid.Rows)
            {
                chkSelect = (CheckBox)row.FindControl("chkSelect");


                //if (chkSelect.Checked == false || chkSelect.Enabled == false)
                //{
                //    btnAdd1.Enabled = false;
                //}
                //else
                //{
                //    btnAdd1.Enabled = true;
                //}
                if (!chkSelect.Checked)
                    flag = true;
            }
            if (flag == true)
            {
                chkSelectAll.Checked = false;
            }
            else
            {
                chkSelectAll.Checked = true;
            }
        }
        protected void btnExportToExcel_Click(object sender, EventArgs e)
        {
            Response.Clear();
            Response.ClearHeaders();
            Response.AddHeader("content-disposition", "attachment;filename=SupplierReceiveMaterial.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.xls";
            StringBuilder sb = new StringBuilder();
            StringWriter stringWriter = new StringWriter(sb);
            HtmlTextWriter htm = new HtmlTextWriter(stringWriter);
            gvSupplier.AllowPaging = false;

            gvSupplier.HeaderRow.Style.Add("background-color", "#FFFFFF");
            gvSupplier.FooterRow.HorizontalAlign = HorizontalAlign.Right;
            //gvSupplier.HeaderRow.Cells[0].Visible = false;

            gvSupplier.RenderControl(htm);
            Response.Write(stringWriter);
            Response.End();
            gvSupplier.Visible = false;

            Response.End();
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
               server control at run time. */
        }
        protected void btnAdd1_Click(object sender, EventArgs e)
        {

            if (lstItemTransactionAdd == null)
            {
                lstItemTransactionAdd = new List<ItemTransaction>();

            }

            flag = false;
            foreach (GridViewRow row in gvMainGrid.Rows)
            {
                lblItem = (Label)row.FindControl("lblItem");
                lblSpecification = (Label)row.FindControl("lblSpecification");
                lblItemCategory = (Label)row.FindControl("lblItemCategory");
                // lblBrand = (Label)row.FindControl("lblMake");
                //lblStore = (Label)row.FindControl("lblStore");
                lblNOF = (Label)row.FindControl("lblItemQuantity");
                lblItemRecieve = (Label)row.FindControl("lblItemRecieve");
                lblItemLeft = (Label)row.FindControl("lblItemLeft");
                lblMeasurement = (Label)row.FindControl("lblMeasurement");
                chkSelect = (CheckBox)row.FindControl("chkSelect");
                hdfSupplierPOMappingId = (HiddenField)row.FindControl("hdfSupplierPOMappingId");
                hdfItemId = (HiddenField)row.FindControl("hdfItemId");
                hdfSpecificationId = (HiddenField)row.FindControl("hdfSpecificationId");
                hdfUnitMeasurementId = (HiddenField)row.FindControl("hdfUnitMeasurementId");
                //hdnfStore = (HiddenField)row.FindControl("hdnfStore");
                //hdnfBrand = (HiddenField)row.FindControl("hdnfBrand");
                if (chkSelect.Checked == true && hdfSupplierPOMappingId != null)
                {
                    flag = true;
                    if (chkSelect.Checked.Equals(true))
                    {
                        itemTransaction = new ItemTransaction();
                        itemTransaction.MetaProperty = new MetaData();
                        itemTransaction.DeliverySchedule = new DeliveryScheduleDOM();
                        itemTransaction.Item = new Item();
                        itemTransaction.Item.ModelSpecification = new ModelSpecification();
                        itemTransaction.Item.ModelSpecification.Category = new ItemCategory();
                        itemTransaction.Item.ModelSpecification.UnitMeasurement = new MetaData();
                        itemTransaction.DeliverySchedule.Id = Convert.ToInt32(hdfSupplierPOMappingId.Value);

                        itemTransaction.Item.ItemId = Convert.ToInt32(hdfItemId.Value);
                        itemTransaction.Item.ItemName = lblItem.Text.ToString();
                        itemTransaction.Item.ModelSpecification.ModelSpecificationId = Convert.ToInt32(hdfSpecificationId.Value);
                        itemTransaction.Item.ModelSpecification.ModelSpecificationName = lblSpecification.Text.ToString();
                        itemTransaction.Item.ModelSpecification.Category.ItemCategoryName = lblItemCategory.Text.ToString();

                        //if (!string.IsNullOrEmpty(hdnfBrand.Value))
                        //{
                        //    itemTransaction.Item.ModelSpecification.Brand.BrandName = lblBrand.Text.ToString();
                        //    itemTransaction.Item.ModelSpecification.Brand.BrandId = Convert.ToInt32(hdnfBrand.Value);
                        //    itemTransaction.Item.ModelSpecification.Store.StoreId = Convert.ToInt32(hdnfStore.Value);
                        //    itemTransaction.Item.ModelSpecification.Store.StoreName = lblStore.Text.ToString();
                        //}

                        //  itemTransaction.Item.ModelSpecification.UnitMeasurement.Id = Convert.ToInt32(hdfUnitMeasurementId.Value);
                        itemTransaction.Item.ModelSpecification.UnitMeasurement.Name = lblMeasurement.Text.ToString();
                        itemTransaction.NumberOfUnit = Convert.ToDecimal(lblNOF.Text.ToString());
                        itemTransaction.UnitLeft = Convert.ToDecimal(lblItemLeft.Text.ToString());
                        itemTransaction.ItemRequired = Convert.ToDecimal(lblItemLeft.Text.ToString());

                        itemTransaction.CreatedBy = LoggedInUser.UserLoginId;

                        lstItemTransactionAdd.Add(itemTransaction);
                        chkSelect.Checked = false;
                        //chkSelect.Enabled = false;
                    }
                }
            }
            if (lstItemTransactionAdd.Count > 0 && flag)
            {
                BindAddGridSupplier();
                Enabled(true);
                txtOrderNumber.Text = string.Empty;
                // txtContractorQuotationNumber.Text = string.Empty;
            }
            else
                if (lblItemLeft.Text == "0")
                {
                    Enabled(false);
                    Alert("There is no left item in all the items.", btnAdd1);
                }
                else
                {
                    Enabled(false);
                    Alert("Please Select At Least One Item", btnAdd1);
                }
            foreach (TableCell item in gvMainGrid.HeaderRow.Cells)
            {
                CheckBox chbxSelectAll = (CheckBox)item.FindControl("chbxSelectAll");
                //CheckBox chkSelect = (CheckBox)item.FindControl("chkSelect");
                if (chbxSelectAll.Checked == true)
                {
                    chbxSelectAll.Checked = false;

                }


            }


        }

        protected void gvSupplierAdd_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //lstItemTransaction = new List<ItemTransaction>();
            int index = Convert.ToInt32(e.CommandArgument);
            String s = string.Empty;
            s = lstItemTransactionAdd[index].Item.ItemName;
            if (e.CommandName == "cmdDelete")
            {

                lstItemTransactionAdd.RemoveAt(index);
                //foreach (GridViewRow row in gvSupplier.Rows)
                //{
                //    chkSelect = (CheckBox)row.FindControl("chkSelect");
                //    Label lblItem = (Label)row.FindControl("lblItem");
                //    if (lblItem.Text.Trim() == s)
                //    {

                //        chkSelect.Enabled = true;
                //        chkSelect.Checked = false;
                //        flag = true;
                //        break;
                //    }

                //}
                if (lstItemTransactionAdd.Count == 0)
                {
                    lstItemTransactionAdd = null;
                    BindGridSupplier();
                    btnSaveDraft.Visible = false;
                    btnReset.Visible = false;

                }
                BindAddGridSupplier();
            }
        }

        protected void btnSaveDraft_Click(object sender, EventArgs e)
        {
            ltrl_err_msg.Text = String.Empty;
            //quotationDOM = new QuotationDOM();
            supplierRecieveMatarial = new SupplierRecieveMatarial();
            supplierRecieveMatarial = GetRecieveMetarialDetail();
            if (!string.IsNullOrEmpty(strInvalid))
            {
                ltrl_err_msg.Text = strInvalid;
            }
            else
            {
                if (SupplierRecieveMaterialId > 0)
                {
                    metaData = CreateSupplierRecieveMaterial(supplierRecieveMatarial, SupplierRecieveMaterialId);
                }
                else
                {
                    metaData = CreateSupplierRecieveMaterial(supplierRecieveMatarial, null);
                }
                //metaData = CreateSupplierRecieveMaterial(supplierRecieveMatarial);
                if (metaData.Id > 0)
                {

                    CreateDocumentMapping();

                    //lstQuotation = null;
                    if (SupplierRecieveMaterialId > 0)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect",
"alert( 'Supplier Recieve Material No: " + metaData.Name + " updated successfully'); window.location='" +
Request.ApplicationPath + "Quality/ViewSupplierReceiveMaterial.aspx';", true);
                        //Alert("Supplier Recieve Material No: " + metaData.Name + " Updated Successfully", btnSaveDraft, "ViewSupplierReceiveMaterial.aspx");
                        pnlSearch.Visible = false;
                    }
                    else
                    {
                        Alert("Supplier Recieve Material No: " + metaData.Name + " Created Successfully", btnSaveDraft);
                        pnlSearch.Visible = true;
                    }
                    ltrl_err_msg.Text = string.Empty;
                    lstItemTransaction = null;
                    lstItemTransactionAdd = null;
                    //txtContractorQuotationNumber.Focus();
                    BindAddGridSupplier();
                    pnlRecieveMetarial.Visible = false;
                }
                else
                {
                    Alert(GlobalConstants.C_DUPLICATE_MESSAGE, btnSaveDraft);
                }
            }
        }

        protected void gvSupplier_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    Label lblQuantity = (Label)e.Row.FindControl("lblItemQuantity");
            //    itemQuantity = Convert.ToDecimal(lblQuantity.Text);
            //    Label lblItemRecieve = (Label)e.Row.FindControl("lblItemRecieve");
            //    itemLeft += Convert.ToDecimal(lblItemRecieve.Text);

            //}

            //foreach (GridViewRow row in gvSupplier.Rows)
            //{
            //    chkSelect = (CheckBox)row.FindControl("chkSelect");

            //    lblItemLeft = (Label)row.FindControl("lblItemLeft");
            //    hdfSupplierPOMappingId = (HiddenField)row.FindControl("hdfSupplierPOMappingId");
            //    if (SupplierRecieveMaterialId != 0 && lstItemTransactionAdd != null)
            //    {
            //        foreach (ItemTransaction item in lstItemTransactionAdd)
            //        {
            //            if (Convert.ToDecimal(lblItemLeft.Text.ToString()) == 0 || Convert.ToInt32(hdfSupplierPOMappingId.Value) == item.DeliverySchedule.Id)
            //            {
            //                chkSelect.Checked = false;
            //                chkSelect.Enabled = false;
            //            }

            //        }

            //    }
            //    else if (Convert.ToDecimal(lblItemLeft.Text.ToString()) == 0)
            //    {
            //        chkSelect.Checked = true;
            //        chkSelect.Enabled = false;
            //    }

            //}
            //if (e.Row.RowType == DataControlRowType.Footer)
            //{
            //    Label lblItemQuantityValue = (Label)e.Row.FindControl("lblItemQuantityValue");
            //    Label lblItemLeftValue = (Label)e.Row.FindControl("lblItemLeftValue");
            //    lblItemQuantityValue.Text = itemQuantity.ToString();
            //    lblItemLeftValue.Text = (itemQuantity - itemLeft).ToString();
            //}
        }

        protected void gvSupplierAdd_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList ddlStore = (DropDownList)e.Row.FindControl("ddlStore");
                DropDownList ddlBrand = (DropDownList)e.Row.FindControl("ddlBrand");

                BrandBL brandBL = new BrandBL();
                BindDropDown(ddlBrand, "BrandName", "BrandId", brandBL.ReadBrands(null));

                StoreBL storeBL = new StoreBL();
                BindDropDown(ddlStore, "StoreName", "StoreId", storeBL.ReadStore(null));




                //txtRequired.MaxLength = 10;
                if (SupplierRecieveMaterialId != 0)
                {
                    if (lstItemTransactionAdd != null)
                    {
                        ddlStore.SelectedValue = lstItemTransactionAdd[e.Row.RowIndex].Item.ModelSpecification.Store.StoreId.ToString();
                        ddlBrand.SelectedValue = lstItemTransactionAdd[e.Row.RowIndex].Item.ModelSpecification.Brand.BrandId.ToString();
                        foreach (ItemTransaction Item in lstItemTransactionAdd)
                        {
                            foreach (ItemTransaction Items in lstItemTransaction)
                            {

                                if (Item.DeliverySchedule.Id == Items.DeliverySchedule.Id)
                                {


                                    Item.UnitIssued = Items.UnitIssued;
                                    Item.UnitLeft = Items.UnitLeft;
                                    Item.CreatedBy = LoggedInUser.UserLoginId;
                                }
                            }
                        }
                    }
                }
                else
                {
                    HiddenField hdnfBrand = (HiddenField)e.Row.FindControl("hdnfBrand");
                    ddlBrand.SelectedValue = hdnfBrand.Value;
                    HiddenField hdnfStore = (HiddenField)e.Row.FindControl("hdnfStore");
                    ddlStore.SelectedValue = hdnfStore.Value;
                }
            }


        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            Reset();
            EmptyDocumentList();
        }

        // popup search Suppiler Oder Number
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            int SupplierId = 0;
            SupplierId = Convert.ToInt32(ddlSupplierReceiveMaterial.SelectedValue);
            BindGrid(SupplierId, DateTime.MinValue, DateTime.MinValue, null, null);
            ModalPopupExtender2.Show();
        }

        protected void rbtSelect_OncheckChanged(object sender, System.EventArgs e)
        {
            foreach (GridViewRow oldRow in gvViewOrder.Rows)
            {
                ((RadioButton)oldRow.FindControl("rbtSelect")).Checked = false;
            }
            RadioButton rb = (RadioButton)sender;
            GridViewRow row = (GridViewRow)rb.NamingContainer;
            ((RadioButton)row.FindControl("rbtSelect")).Checked = true;
            Label SupplierWON = (Label)row.FindControl("lbtnQuotation");
            txtOrderNumber.Text = SupplierWON.Text.ToString();
        }


        public void BindGrid(Int32 contractorId, DateTime toDate, DateTime fromDate, String contractNo, String WorkOrderNo)
        {

            lstQuotation = new List<QuotationDOM>();

            lstQuotation = quotationBL.ReadSupplierQuotationView(contractorId, toDate, fromDate, WorkOrderNo);



            if (lstQuotation.Count > 0)
            {
                // var lst = lstQuotation.Where(e => e.StatusType.Name.Equals(Convert.ToInt32(StatusType.Generated)));
                var lst = lstQuotation.Where(e => e.StatusType.Name.Equals(Convert.ToString(StatusType.Generated)));
                gvViewOrder.DataSource = lst;
                gvViewOrder.DataBind();
            }
            else
            {
                BindEmptyGrid(gvViewOrder);
            }
        }

        private void BindEmptyGrid(GridView gv)
        {
            gv.DataSource = new List<object>();
            gv.DataBind();
        }



        #region Private Section
        private void BindMainGrid(List<ItemTransaction> lstItemTransaction)
        {

            // Manage Using Linq
            //  var finalList = new List<ItemTransaction>();
            var finalList = lstItemTransaction
                    .Select(x => new
                    {
                        x.Item.ItemId,
                        x.Item.ItemName,
                        x.Item.ModelSpecification.ModelSpecificationId,
                        x.Item.ModelSpecification.ModelSpecificationName,
                        x.NumberOfUnit,
                        x.UnitIssued,
                        x.UnitLeft,
                        x.Item.ModelSpecification.Category.ItemCategoryId,
                        x.Item.ModelSpecification.Category.ItemCategoryName,
                        x.Item.ModelSpecification.UnitMeasurement.Name,
                        x.Item.ModelSpecification.UnitMeasurement.Id,
                        SupplierPOId = x.DeliverySchedule.Id

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
                        a.SupplierPOId

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
                IssuedQuantity = b.Sum(z => z.UnitIssued),
                AvailableQuantity = b.Key.NumberOfUnit - b.Sum(z => z.UnitIssued),
                QuantityDemand = b.Key.NumberOfUnit,
                SupplierPurchaseOrderId = b.Key.SupplierPOId
            }).ToList();


            // lstItemTransaction = quotationBL.ReadSupplierQuotationMapping(QuotationId);
            if (finalList.Count > 0)
            {
                IssuedItems = new List<ValidateIssueItem>();
                foreach (var item in finalList)
                {
                    ValidateIssueItem issuedItem = new ValidateIssueItem();
                    issuedItem.ItemId = item.ItemId;
                    issuedItem.ItemName = item.ItemName;
                    issuedItem.ItemSpecificationId = item.ItemSpecificationId;
                    issuedItem.ItemSpecification = item.ItemSpecification;
                    issuedItem.AvailableQuantity = item.AvailableQuantity;
                    IssuedItems.Add(issuedItem);
                }
                gvMainGrid.DataSource = finalList;
                gvMainGrid.DataBind();
            }
            else
            {
                GridViewEmptyText(gvMainGrid);

            }
        }


        private void BindGridSupplier()
        {
            //lstItemTransaction = new List<ItemTransaction>();
            lstItemTransaction = quotationBL.ReadSupplierQuotationMapping(QuotationId);
            if (lstItemTransaction.Count > 0)
            {
                gvSupplier.DataSource = lstItemTransaction;
                gvSupplier.DataBind();
            }
            else
            {
                GridViewEmptyText(gvSupplier);

            }
        }

        private void CalculateItemLeft()
        {
            for (int i = 0; i < lstItemTransactionAdd.Count; i++)
            {
                lstItemTransactionAdd[i].UnitLeft += lstItemTransactionAdd[i].ItemRequired;
                for (int j = 0; j < lstItemTransaction.Count; j++)
                {
                    if (lstItemTransactionAdd[i].Item.ModelSpecification.ModelSpecificationId == lstItemTransaction[j].Item.ModelSpecification.ModelSpecificationId)
                    {
                        lstItemTransaction[j].UnitLeft += lstItemTransactionAdd[i].ItemRequired;
                        lstItemTransaction[j].UnitIssued -= lstItemTransactionAdd[i].ItemRequired;
                    }
                }
            }
        }
        private void BindSupplier(List<QuotationDOM> lst)
        {
            quotationBL = new QuotationBL();
            //lstQuotation = new List<QuotationDOM>();
            lstItemTransaction = new List<ItemTransaction>();
            if (lst.Count > 0)
            {
                lstItemTransaction = quotationBL.ReadSupplierQuotationMapping(lst[0].SupplierQuotationId);

                // Here Bind MAin Grid Also
                BindMainGrid(lstItemTransaction);
                gvSupplier.DataSource = lstItemTransaction;
                gvSupplier.DataBind();
            }
        }

        private void BindAddGridSupplier()
        {
            gvSupplierAdd.DataSource = lstItemTransactionAdd;
            gvSupplierAdd.DataBind();
        }

        private void BindText(List<QuotationDOM> lst)
        {
            QuotationId = lst[0].SupplierQuotationId;
            lblPurchaseOrderNumber.Text = lst[0].SupplierQuotationNumber;
            lblOrderDate.Text = (lst[0].OrderDate).ToString("dd'/'MM'/'yyyy");
            hdfSupplierId.Value = lst[0].SupplierQuotationId.ToString();

            //lblContractorQuotationNumber.Text = lst[0].ContractQuotationNumber.ToString();
            //lblContractorQuotationDate.Text = lst[0].OrderDate.ToString("dd-MMM-yyyy");
            //lblContractorName.Text = lst[0].ContractorName.ToString();
            //lblContractNumber.Text = lst[0].ContractNumber.ToString();
            //hdfContractorId.Value = lst[0].ContractorId.ToString();
        }

        private void SetAllDefaultData(List<QuotationDOM> lst)
        {
            //List<QuotationDOM> lstQuotation = new List<QuotationDOM>();
            BindText(lst);
            // BindGridSupplier();
            BindSupplier(lst);
        }

        private void Reset()
        {
            i = 0;
            txtChallanNumber.Text = string.Empty;
            ltrl_err_msg.Text = string.Empty;
            txtRMDate.Text = DateTime.Now.ToString("dd'/'MM'/'yyyy");
            for (i = 0; i < lstItemTransactionAdd.Count; i++)
            {
                lstItemTransactionAdd[i].ItemRequired = 0;
            }
            //document_BL.ResetDocumentMapping(Convert.ToInt32(DocumentStackId));
            BindAddGridSupplier();
        }

        private void BindUpdateText(List<SupplierRecieveMatarial> lst)
        {
            lblPurchaseOrderNumber.Text = lst[0].Quotation.SupplierQuotationNumber.ToString();
            lblOrderDate.Text = lst[0].Quotation.OrderDate.ToString("dd'/'MM'/'yyyy");
            txtChallanNumber.Text = lst[0].DeliveryChallanNumber;
            txtRMDate.Text = Convert.ToDateTime(lst[0].RecieveMaterialDate).ToString("dd'/'MM'/'yyyy");
            hdfSupplierId.Value = lst[0].Quotation.SupplierId.ToString();

        }
        private void Enabled(Boolean Condition)
        {
            btnSaveDraft.Visible = Condition;
            btnReset.Visible = Condition;
        }

        private Decimal TryToParse(string Value)
        {
            dec = 0;
            Decimal.TryParse(Value, out dec);
            return dec;
        }

        private MetaData CreateSupplierRecieveMaterial(SupplierRecieveMatarial supplierRecieveMatarial, Int32? SRMID)
        {
            if (lstItemTransactionAdd != null)
            {

                supplierRecieveMaterialBL = new SupplierRecieveMaterialBAL();
                metaData = supplierRecieveMaterialBL.CreateSupplierRecieveMaterial(supplierRecieveMatarial, SRMID);
            }
            return metaData;
        }
        private SupplierRecieveMatarial GetRecieveMetarialDetail()
        {

            j = 0;

            supplierRecieveMatarial = new SupplierRecieveMatarial();
            supplierRecieveMatarial.Quotation = new QuotationDOM();
            supplierRecieveMatarial.Quotation.StatusType = new MetaData();
            supplierRecieveMaterialBL = new SupplierRecieveMaterialBAL();
            if (SupplierRecieveMaterialId > 0)
            {
                supplierRecieveMatarial.SupplierRecieveMatarialId = SupplierRecieveMaterialId;
            }

            supplierRecieveMatarial.Quotation.SupplierQuotationNumber = lblPurchaseOrderNumber.Text;
            //supplierRecieveMatarial.Quotation.SupplierId = Convert.ToInt32(hdfSupplierId.Value);
            supplierRecieveMatarial.Quotation.OrderDate = DateTime.ParseExact(lblOrderDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            supplierRecieveMatarial.DeliveryChallanNumber = txtChallanNumber.Text.Trim();
            // supplierRecieveMatarial.RecieveMaterialDate = Convert.ToDateTime(txtRMDate.Text.Trim());
            supplierRecieveMatarial.RecieveMaterialDate = DateTime.ParseExact(txtRMDate.Text.Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture);// Convert.ToDateTime(txtRMDate.Text.Trim());
            supplierRecieveMatarial.UploadFile = new Document();
            supplierRecieveMatarial.UploadFile.DocumentId = DocumentStackId;
            supplierRecieveMatarial.Quotation.StatusType.Id = Convert.ToInt32(StatusType.Pending);
            supplierRecieveMatarial.CreatedBy = LoggedInUser.UserLoginId;

            decimal recievedValue = decimal.MinValue;
            List<ValidateIssueItem> recievedItems = new List<ValidateIssueItem>();
            for (i = 0; i < gvSupplierAdd.Rows.Count; i++)
            {

                ValidateIssueItem recievedItem = new ValidateIssueItem();
                BigNo = 0;
                dec = 0;
                if (SupplierRecieveMaterialId > 0)
                {


                    BigNo = lstItemTransactionAdd[i].UnitLeft;
                }
                else
                {
                    BigNo = (lstItemTransactionAdd[i].UnitLeft + lstItemTransactionAdd[i].ItemRequired);
                }

                TextBox box = (TextBox)gvSupplierAdd.Rows[i].Cells[10].FindControl("txtRecieveQuantity");
                DropDownList ddlStore = (DropDownList)gvSupplierAdd.Rows[i].Cells[5].FindControl("ddlStore");
                DropDownList ddlBrand = (DropDownList)gvSupplierAdd.Rows[i].Cells[6].FindControl("ddlBrand");
                Label lblIndex = (Label)gvSupplierAdd.Rows[i].Cells[0].FindControl("lblIndex");
                if (ddlStore != null && ddlBrand != null)
                {
                    if (ddlBrand.SelectedIndex > 0)
                    {
                        lstItemTransactionAdd[i].Item.ModelSpecification.Brand.BrandId = Convert.ToInt32(ddlBrand.SelectedValue);
                        lstItemTransactionAdd[i].Item.ModelSpecification.Brand.BrandName = ddlBrand.SelectedItem.Text;


                    }
                    else
                    {
                        strInvalid = strInvalid + "Brand should be selected at S.No: " + lblIndex.Text.Trim() + "</br>";
                    }
                    if (ddlStore.SelectedIndex > 0)
                    {
                        lstItemTransactionAdd[i].Item.ModelSpecification.Store.StoreId = Convert.ToInt32(ddlStore.SelectedValue);
                        lstItemTransactionAdd[i].Item.ModelSpecification.Store.StoreName = ddlStore.SelectedItem.Text;

                    }
                    else
                    {
                        strInvalid = strInvalid + "Store should be selected at S.No: " + lblIndex.Text.Trim() + "</br>";
                    }
                    // supplierRecieveMatarial.Quotation.ItemTransaction.Add(itemTransaction);

                }



                lblIndex = (Label)gvSupplierAdd.Rows[i].Cells[0].FindControl("lblIndex");
                dec = TryToParse(box.Text);

                // fill REceived Item 
                recievedItem.ItemId = lstItemTransactionAdd[i].Item.ItemId;
                recievedItem.ItemName = lstItemTransactionAdd[i].Item.ItemName;
                recievedItem.ItemSpecificationId = lstItemTransactionAdd[i].Item.ModelSpecification.ModelSpecificationId;
                recievedItem.ItemSpecification = lstItemTransactionAdd[i].Item.ModelSpecification.ModelSpecificationName;
                recievedItem.RecievedQuantity = dec;
                //lstItemTransaction1[i].ItemRequired = Convert.ToDecimal(box.Text);
                if (dec > 0)
                {
                    cnt = 0;
                    cnt = NumberDecimalPlaces(dec);
                    if (SupplierRecieveMaterialId > 0)
                    {

                        if (!(dec <= lstItemTransactionAdd[i].ItemRequired))
                        {
                            if ((cnt > 2 || dec > BigNo))
                            {
                                if (j > 0)
                                {
                                    strInvalid = strInvalid + "Recieve Quantity can't be 0 and greater than Item Left at S.No: " + lblIndex.Text.Trim() + "</br>";
                                    j++;
                                }
                                else
                                {
                                    strInvalid = strInvalid + "Recieve Quantity can't be 0 and greater than Item Left at S.No:" + lblIndex.Text.Trim() + "</br>";
                                    j++;
                                }
                                // flag = true;
                            }
                        }
                    }
                    else if (cnt > 2 || dec > lstItemTransactionAdd[i].UnitLeft)
                    {
                        if (j > 0)
                        {
                            strInvalid = strInvalid + " Recieve Quantity can't be 0 and greater than Item Left at S.No:" + lblIndex.Text.Trim() + "</br>";
                            j++;
                        }
                        else
                        {
                            strInvalid = strInvalid + "Recieve Quantity can't be 0 and greater than Item Left at S.No:" + lblIndex.Text.Trim() + "</br>";
                            j++;
                        }
                        // flag = true;
                    }
                }
                else
                {
                    if (j > 0)
                    {
                        strInvalid = strInvalid + "Recieve Quantity can't be 0 and greater than Item Left at S.No: " + lblIndex.Text.Trim() + "</br>";
                        j++;
                    }
                    else
                    {
                        strInvalid = strInvalid + "Recieve Quantity can't be 0 and greater than Item Left at S.No:" + lblIndex.Text.Trim() + "</br>";
                        j++;
                    }
                    //flag = true;
                }
                if (string.IsNullOrEmpty(strInvalid))
                {
                    lstItemTransactionAdd[i].ItemRequired = Convert.ToDecimal(box.Text);
                }
                recievedItems.Add(recievedItem);
            }

            var rsRecievedItems = recievedItems.AsEnumerable()
                   .Select(x => new
                   {
                       x.ItemId,
                       x.ItemName,
                       x.ItemSpecification,
                       x.ItemSpecificationId,
                       x.RecievedQuantity
                   })
                   .GroupBy(a => new
                   {
                       a.ItemId,
                       a.ItemName,
                       a.ItemSpecification,
                       a.ItemSpecificationId,
                       a.RecievedQuantity


                   })
                   .Select(b => new
                   {
                       ItemId = b.Key.ItemId,
                       ItemName = b.Key.ItemName,
                       ItemSpecificationId = b.Key.ItemSpecificationId,
                       ItemSpecification = b.Key.ItemSpecification,
                       RecievedQuantity = b.Sum(z => z.RecievedQuantity)
                   }).ToList();


            foreach (var rcvdItem in rsRecievedItems)
            {
                decimal UnitLeft = IssuedItems.Where(x => x.ItemId == rcvdItem.ItemId && x.ItemSpecificationId == rcvdItem.ItemSpecificationId).Select(z => z.AvailableQuantity).FirstOrDefault();

                if (!(SupplierRecieveMaterialId > 0))
                {
                    if (UnitLeft < rcvdItem.RecievedQuantity || rcvdItem.RecievedQuantity == Decimal.Zero)
                    {
                        strInvalid += " Recieve Quantity can't be 0 and greater than Item Left  for " + rcvdItem.ItemName + "-" + rcvdItem.ItemSpecification + "</br>";
                    }
                }
                else
                {
                    if (rcvdItem.RecievedQuantity == Decimal.Zero)
                    {
                        strInvalid += " Recieve Quantity can't be 0  for " + rcvdItem.ItemName + "-" + rcvdItem.ItemSpecification + "</br>";
                    }
                }
            }
            //if (lstItemTransaction1[i].ItemRequired == 0)
            //{
            //    supplierRecieveMatarial.Quotation.ItemTransaction = lstItemTransaction1;
            //}
            if (!string.IsNullOrEmpty(strInvalid))
            {
                // strInvalid = "Unit Required allows only numeric value up to 2 decimal places & less than or equal to unit left at Index: " + strInvalid;
                //strInvalid = "Recieve Quantity can't be 0 and greater than Item Left";
            }
            else
            {
                supplierRecieveMatarial.Quotation.ItemTransaction = lstItemTransactionAdd;
            }
            return supplierRecieveMatarial;

            //supplierRecieveMatarial.Quotation.ItemTransaction = lstItemTransaction1;
            //return supplierRecieveMatarial;
        }

        private int NumberDecimalPlaces(Decimal dec)
        {
            string testdec = Convert.ToString(dec);
            int s = (testdec.IndexOf(".") + 1); // the first numbers plus decimal point 
            if (s > 0)
            {
                return ((testdec.Length) - s);   //total length minus beginning numbers and decimal = number of decimal points 
            }
            else
            {
                return 0;
            }
        }

        #endregion

        #region public Properties

        public Int32 QuotationId
        {
            get
            {
                try
                {
                    return (Int32)ViewState["QuotationId"];
                }
                catch
                {

                    return 0;
                }
            }
            set
            {
                ViewState["QuotationId"] = value;
            }
        }


        public List<ValidateIssueItem> IssuedItems
        {
            get
            {
                try
                {
                    if (ViewState["IssuedItems"] != null)
                    {
                        return (List<ValidateIssueItem>)ViewState["IssuedItems"];
                    }
                    else
                    {
                        return new List<ValidateIssueItem>();
                    }

                }
                catch
                {

                    return null;
                }
            }
            set
            {
                ViewState["IssuedItems"] = value;
            }
        }


        public int SupplierRecieveMaterialId
        {
            get
            {
                try
                {
                    return (Int32)ViewState["SupplierRecieveMaterialId"];
                }
                catch
                {

                    return 0;
                }
            }
            set
            {
                ViewState["SupplierRecieveMaterialId"] = value;
            }
        }
        public String QuotationNumber
        {
            get
            {
                try
                {
                    return (String)ViewState["QuotationNumber"];
                }
                catch
                {

                    return String.Empty;
                }
            }
            set
            {
                ViewState["QuotationNumber"] = value;
            }
        }


        public List<ItemTransaction> lstItemTransactionAdd
        {
            get
            {
                return (List<ItemTransaction>)ViewState["lstItemTransactionAdd"];
            }
            set
            {
                ViewState["lstItemTransactionAdd"] = value;
            }
        }
        public List<ItemTransaction> lstItemTransaction
        {
            get
            {
                return (List<ItemTransaction>)ViewState["lstItemTransaction"];
            }
            set
            {
                ViewState["lstItemTransaction"] = value;
            }
        }

        #endregion

        #region Upload Document Code

        #region Private Global Variable(s)

        DocumentBL document_BL = new DocumentBL();

        Document document = null;
        Int32 Year = 0;
        Int32 Index = 0;

        String Head_Folder_Path = String.Empty;
        String Sub_Folder_Path = String.Empty;
        String File_Extension = String.Empty;
        String File_Path = String.Empty;

        DataSet page_Data = null;

        List<Document> lst_document = null;
        #endregion

        #region Protected Methods

        protected void btn_upload_Click(object sender, EventArgs e)
        {
            //For Copy Quotation, It is TRUE
            LoadData(false);
        }

        private void LoadData(bool forCopy)
        {
            LoadDocument(forCopy);
            DirectoryHandle(FileUpload_Control);
            BindDocument();
        }

        private void LoadDocument(bool forCopy)
        {
            ManageSession(forCopy);

            if (DocumentStackId == 0)
            {
                CreateAndReadDocumentStackId();
            }

            BindDocument();
        }

        protected void gv_documents_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "FileDelete")
            {
                Index = Convert.ToInt32(e.CommandArgument);
                lst_document = new List<Document>();
                lst_document = DocumentsList;
                lst_document.RemoveAt(Index);
                DocumentsList = lst_document;
                BindDocument();
            }
            else if (e.CommandName == "OpenFile")
            {
                Index = Convert.ToInt32(e.CommandArgument);
                Index = Convert.ToInt32(e.CommandArgument);
                lst_document = new List<Document>();
                lst_document = DocumentsList;


                Response.Redirect(@"\" + lst_document[Index].Path + @"\" + lst_document[Index].Replaced_Name);

                //File_Path = lst_document[Index].Path + @"\" + lst_document[Index].Replaced_Name;
                //WebClient req = new WebClient();
                //HttpResponse response = HttpContext.Current.Response;
                //response.Clear();
                //response.ClearContent();
                //response.ClearHeaders();
                //response.Buffer = true;
                //response.AddHeader("Content-Disposition", "attachment;filename=" + lst_document[Index].Replaced_Name.Replace(lst_document[Index].Replaced_Name, lst_document[Index].Original_Name));
                ////response.AddHeader("Content-Disposition", "attachment;filename=" + lst_document[Index].Replaced_Name);
                //byte[] data = req.DownloadData(Server.MapPath(File_Path));
                //response.BinaryWrite(data);
                //response.End();

                //string strURL = "~/Images/1.jpg";

                //string filename = "1.jpg";
                //WebClient req = new WebClient();
                //HttpResponse response = HttpContext.Current.Response;
                //response.Clear();
                //response.ClearContent();
                //response.ClearHeaders();
                //response.Buffer = true;
                //response.AddHeader("Content-Disposition", "attachment;filename=" + filename.Replace(filename, "123.jpg"));


                //byte[] data = req.DownloadData(Server.MapPath(strURL));
                //response.BinaryWrite(data);
                //response.End();

                // Response.Redirect("../Upload_Documents/2013/SupplierReceiveMaterial/1286_1.docx",);
            }
        }

        #endregion


        #region Private Methods

        private void ManageSession(bool forCopy)
        {
            RequestPageName = pageName;
            if (forCopy)
            {
                DocumentStackId = 0;
            }
            else if (Page_Name == null || Page_Name != RequestPageName)
            {
                Page_Name = RequestPageName;
                DocumentStackId = 0;
                DocumentSerial = 0;
                DocumentsList = null;
            }
            else
            {
                //GO AHEAD
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private Int32 CreateAndReadDocumentStackId()
        {
            document = new Document();
            document.CreatedBy = LoggedInUser.UserLoginId;
            DocumentStackId = document_BL.CreateAndReadDocumnetStackId(document);
            return DocumentStackId;
        }

        private void DirectoryHandle(FileUpload fileupload)
        {
            if (fileupload.HasFile)
            {
                if (fileupload.FileContent.Length > 10485760)
                {
                    Alert("You can upload up to 10 megabytes (MB) in size at a time", FileUpload_Control);
                }

                else
                {
                    Year = DateTime.Now.Year;

                    //Get list of pages
                    page_Data = new DataSet();
                    page_Data.ReadXml(Server.MapPath(ConfigurationManager.AppSettings["PageDictionary_Path"].ToString()));

                    Head_Folder_Path = Server.MapPath(@"\" + ConfigurationManager.AppSettings["Upload_Document_Path"].ToString() + Year.ToString());
                    //Check to existance of Main Folder
                    if (!Directory.Exists(Head_Folder_Path))
                    {
                        Directory.CreateDirectory(Head_Folder_Path);
                    }
                    //For Check to existance of Sub-Folders and if not, then create
                    foreach (DataRow dr in page_Data.Tables[0].Rows)
                    {
                        Sub_Folder_Path = Head_Folder_Path + @"\" + dr["Page"].ToString();
                        if (!Directory.Exists(Sub_Folder_Path))
                        {
                            Directory.CreateDirectory(Sub_Folder_Path);
                        }
                    }
                    //If folder exist then Upload Document in respective folder
                    Sub_Folder_Path = Head_Folder_Path + @"\" + RequestPageName;

                    if (Directory.Exists(Sub_Folder_Path))
                    {
                        if (DocumentStackId != 0)
                        {
                            document = new Document();
                            lst_document = new List<Document>();
                            flag = false;

                            document.Original_Name = fileupload.FileName.Split('\\').Last();
                            if (DocumentsList != null)
                            {
                                foreach (Document item in DocumentsList)
                                {
                                    if (item.Original_Name == document.Original_Name)
                                    {
                                        flag = true;
                                        break;
                                    }
                                }
                            }
                            if (flag == true)
                            {
                                Alert(GlobalConstants.M_File_Exist, FileUpload_Control);
                            }
                            else
                            {
                                DocumentSerial = DocumentSerial + 1;

                                File_Extension = System.IO.Path.GetExtension(fileupload.FileName.Split('\\').Last());
                                document.Replaced_Name = Convert.ToString(DocumentStackId) + "_" + Convert.ToString(DocumentSerial) + File_Extension;

                                File_Path = Sub_Folder_Path + @"\" + document.Replaced_Name;
                                //File_Path = Sub_Folder_Path + @"\" + document.Original_Name;
                                //Upload file in respective path
                                FileUpload_Control.SaveAs(File_Path);

                                document.DocumentId = DocumentStackId;

                                document.Path = ConfigurationManager.AppSettings["Upload_Document_Path"].ToString() + Year.ToString() + @"\" + RequestPageName;
                                document.LastIndex = DocumentSerial;


                                if (DocumentsList == null)
                                {
                                    lst_document.Add(document);
                                }
                                else
                                {
                                    lst_document = DocumentsList;
                                    lst_document.Add(document);
                                }

                                //Add Doc's info in list
                                DocumentsList = lst_document;
                            }
                        }
                    }
                }
            }
            else
            {
                ShowMessage("Please Select File.");
            }
        }

        public void BindDocument()
        {
            if (DocumentsList != null)
            {
                gv_documents.DataSource = DocumentsList;
            }
            else
            {
                gv_documents.DataSource = null;
            }
            gv_documents.DataBind();
        }

        private void CreateDocumentMapping()
        {
            document_BL = new DocumentBL();
            lst_document = new List<Document>();
            lst_document = DocumentsList;

            if (lst_document != null)
            {
                //For Reset IsDeleted All documents realted to Document_Id
                document_BL.ResetDocumentMapping(Convert.ToInt32(DocumentStackId));

                //For Insert/Update the Documents
                foreach (Document doc in lst_document)
                {
                    document = new Document();
                    document.DocumentId = Convert.ToInt32(DocumentStackId);
                    document.Original_Name = doc.Original_Name;
                    document.Replaced_Name = doc.Replaced_Name;
                    document.Path = doc.Path;
                    //DocumentSerial is the last updated document
                    document.LastIndex = DocumentSerial;
                    document.CreatedBy = LoggedInUser.UserLoginId;
                    document.Id = doc.Id;
                    document_BL.CreateDocumentMapping(document);
                }
            }
        }

        private void GetDocumentData(List<SupplierRecieveMatarial> lst_quotation)
        {
            document_BL = new DocumentBL();
            lst_document = new List<Document>();
            lst_document = document_BL.ReadDocumnetMapping(lst_quotation[0].UploadFile.DocumentId);
            if (lst_document.Count >= 1)
            {
                DocumentsList = lst_document;
                DocumentStackId = lst_document[0].DocumentId;

                DocumentSerial = lst_document[0].LastIndex;
                Page_Name = pageName;
                BindDocument();
            }
        }

        public void EmptyDocumentList()
        {
            DocumentStackId = 0;
            DocumentSerial = 0;
            DocumentsList = null;
            BindDocument();
        }
        #endregion

        #region Public Properties

        public String RequestPageName
        {
            get
            {
                return (String)ViewState["Page"];
            }
            set
            {
                ViewState["Page"] = value;
            }
        }
        #endregion

        protected void gvSupplier_PreRender(object sender, EventArgs e)
        {

            if (SupplierRecieveMaterialId == 0)
            {
                bool show = false;
                foreach (var item in lstItemTransaction)
                {
                    if (!string.IsNullOrEmpty(item.Item.ModelSpecification.Brand.BrandName))
                    {
                        show = true;
                        break;
                    }
                }
                if (!show)
                {
                    gvSupplier.Columns[4].Visible = false;
                    gvSupplier.Columns[5].Visible = false;
                }
                else
                {
                    gvSupplier.Columns[4].Visible = true;
                    gvSupplier.Columns[5].Visible = true;
                }
            }
            // MergeRows(gvSupplier);

        }

        protected void gvMainGrid_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            foreach (GridViewRow row in gvMainGrid.Rows)
            {
                chkSelect = (CheckBox)row.FindControl("chkSelect");

                lblItemLeft = (Label)row.FindControl("lblItemLeft");
                // hdfSupplierPOMappingId = (HiddenField)row.FindControl("hdfSupplierPOMappingId");
                if (SupplierRecieveMaterialId != 0 && lstItemTransactionAdd != null)
                {
                    foreach (ItemTransaction item in lstItemTransactionAdd)
                    {
                        if (Convert.ToDecimal(lblItemLeft.Text.ToString()) == 0)
                        {
                            chkSelect.Checked = false;
                            chkSelect.Enabled = false;
                        }

                    }

                }
                else if (Convert.ToDecimal(lblItemLeft.Text.ToString()) == 0)
                {
                    chkSelect.Checked = false;
                    chkSelect.Enabled = false;
                }

            }
        }

        protected void ddlBrand_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddl = (DropDownList)sender;
            GridViewRow row = (GridViewRow)ddl.Parent.Parent;
            int idx = row.RowIndex;
            lstItemTransactionAdd[idx].Item.ModelSpecification.Brand.BrandId = Convert.ToInt32(ddl.SelectedValue);
        }

        protected void ddlStore_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddl = (DropDownList)sender;
            GridViewRow row = (GridViewRow)ddl.Parent.Parent;
            int idx = row.RowIndex;
            lstItemTransactionAdd[idx].Item.ModelSpecification.Store.StoreId = Convert.ToInt32(ddl.SelectedValue);
        }

        protected void txtRecieveQuantity_TextChanged(object sender, EventArgs e)
        {
            TextBox ddl = (TextBox)sender;
            GridViewRow row = (GridViewRow)ddl.Parent.Parent;
            int idx = row.RowIndex;
            lstItemTransactionAdd[idx].ItemRequired = Convert.ToDecimal(ddl.Text);
        }


        //public void MergeRows(GridView gridView)
        //{
        //    for (int rowIndex = gridView.Rows.Count - 2; rowIndex >= 0; rowIndex--)
        //    {
        //        GridViewRow row = gridView.Rows[rowIndex];
        //        GridViewRow previousRow = gridView.Rows[rowIndex + 1];

        //        for (int i = 0; i < row.Cells.Count; i++)
        //        {
        //            if (((Label)row.Cells[i].FindControl("lblItemQuantity")).Text == ((Label)previousRow.Cells[i].FindControl("lblItemQuantity")).Text)
        //            {
        //                row.Cells[i].RowSpan = previousRow.Cells[i].RowSpan < 2 ? 2 :
        //                                       previousRow.Cells[i].RowSpan + 1;
        //                previousRow.Cells[i].Visible = false;
        //            }
        //        }
        //    }
        //}





        #endregion



    }
}