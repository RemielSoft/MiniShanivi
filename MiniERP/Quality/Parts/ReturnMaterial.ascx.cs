


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
using BusinessAccessLayer.Quality;
using System.Data;
using System.Configuration;
using System.Net;
using System.IO;
using System.Reflection;

namespace MiniERP.Quality.Parts
{

    public partial class ReturnMaterial : System.Web.UI.UserControl
    {
        #region Global Variables Declation

        QuotationBL quotationBL = null;
        BrandBL brandBL = new BrandBL();
        List<SupplierRecieveMatarial> lstSupplierRecieveMatarial = null;
        //List<ItemTransaction> lstPreItemTransaction = null;
        //List<ReturnMaterialDOM> lstReturnMaterialDOM = null;
        SupplierRecieveMaterialBAL supplierRecieveMaterialBAL = null;
        ReturnMaterialDOM returnMaterialDOM = null;
        ReturnMaterialBL returnMaterialBL = null;
        MetaData metaData = null;
        SupplierRecieveMaterialBAL SupplierReceiveMatBal = new SupplierRecieveMaterialBAL();

        BasePage basePage = new BasePage();

        ItemTransaction itemTransaction = null;

        Boolean flag = false;
        HiddenField hdfItemId = null;
        HiddenField hdfSpecificationId = null;
        HiddenField hdfSupplierPOMappingId = null;
        HiddenField hdfUnitMeasurementId = null;
        Label lblItem = null;
        Label lblSpecification = null;
        Label lblItemCategory = null;
        Label lblBrand = null;
        Label lblStore = null;
        Label lblItemQuantity = null;
        Label lblItemRecieve = null;
        Label lblItemLeft = null;
        Label lblMeasurement = null;
        CheckBox chkSelectAll = null;
        CheckBox chkSelect = null;
        Label lblIndex = null;
        HiddenField hdnStore = null;
        HiddenField hdnBrand = null;

        String pageName = String.Empty;
        int index = 0;
        Int32 Status = 0;
        int i = 0;
        int j = 0;
        string strInvalid = string.Empty;
        decimal dec = 0;
        int cnt = 0;
        String s = string.Empty;
        decimal BigNo = 0;
        int count = 0;
        #endregion

        #region Protected Method
        protected void Page_Load(object sender, EventArgs e)
        {
            pageName = (Request.Url.LocalPath).Split('/').Last().Split('.').First();
            SupplierRecieveMaterialId = 0;
            CalendarExtender1.EndDate = DateTime.Now;
            txtRMDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");

            if (!IsPostBack)
            {

                //lstSupplierRecieveMatarial = new List<SupplierRecieveMatarial>();
                //lstSupplierRecieveMatarial = SupplierReceiveMatBal.SearchReceiveMaterial(null, null, null, DateTime.MinValue, DateTime.MinValue);


                //var lst = lstQuotation.Select(a => new { a.ContractorName, a.ContractorId }).Distinct().ToList().OrderBy(a => a.ContractorName);


                //basePage.BindDropDown(ddlDemandVoucher, "ContractorName", "ContractorId", lst);

                if (Request.QueryString["ReturnMaterialId"] != null)
                {
                    ReturnMaterialId = Convert.ToInt32(Request.QueryString["ReturnMaterialId"]);
                    if (ReturnMaterialId > 0)
                    {
                        BindUpdate();
                    }
                }
                else
                {
                    EmptyDocumentList();
                    ReturnMaterialId = 0;
                    pnlRecieveMetarial.Visible = false;
                    pnlSearch.Visible = true;
                    Enabled(false);
                    txtReceiveMaterialNumber.Focus();


                    txtReceiveMaterialNumber.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('" + lnkSerach.ClientID + "').click();return false;}} else {return true}; ");
                }

            }
        }

        protected void lnkSerach_Click(object sender, EventArgs e)
        {
            string recMaterial = txtReceiveMaterialNumber.Text.Trim();
            if (recMaterial == string.Empty)
            {
                basePage.Alert("Please Enter Receive Material Number", lnkSerach);
            }
            else
            {
                lstReturnMaterialDOM = new List<ReturnMaterialDOM>();
                lstItemTransaction = new List<ItemTransaction>();
                lstSupplierRecieveMatarial = new List<SupplierRecieveMatarial>();
                supplierRecieveMaterialBAL = new SupplierRecieveMaterialBAL();
                returnMaterialBL = new ReturnMaterialBL();
                lstReturnMaterialDOM = returnMaterialBL.ReadSupplierReturnMaterial(null, null, txtReceiveMaterialNumber.Text.Trim(), null, null, null, System.DateTime.MinValue, System.DateTime.MinValue);
                if (lstReturnMaterialDOM.Count > 0)
                {
                    ReturnMaterialId = lstReturnMaterialDOM[0].RetutrnMaterialId;
                    Status = lstReturnMaterialDOM[0].RecieveMatarial.Quotation.StatusType.Id;
                    Enabled(true);
                }
                lstItemTransaction = returnMaterialBL.ReadSupplierReturnMaterialMapping(txtReceiveMaterialNumber.Text.Trim(), null);
                lstSupplierRecieveMatarial = supplierRecieveMaterialBAL.SearchReceiveMaterial(string.Empty, string.Empty, txtReceiveMaterialNumber.Text.Trim().ToString(), System.DateTime.MinValue, System.DateTime.MinValue, null);
                if (Status == Convert.ToInt32(StatusType.Generated))
                {
                    basePage.Alert("Return Material for this Receive Material is already generated", lnkSerach);
                }
                else if (lstSupplierRecieveMatarial.Count > 0 && (lstSupplierRecieveMatarial[0].Quotation.StatusType.Id == Convert.ToInt32(StatusType.Generated)))
                {
                    txtRMDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
                    SetAllDefaultData(lstSupplierRecieveMatarial);
                    pnlRecieveMetarial.Visible = true;
                    //Enabled(false);               
                }
                else if (string.IsNullOrEmpty(txtReceiveMaterialNumber.Text.ToString()))
                {
                    basePage.Alert("Please enter a valid Receive Material Number", lnkSerach);
                }
                else if (lstSupplierRecieveMatarial.Count > 0 && (lstSupplierRecieveMatarial[0].Quotation.StatusType.Id != Convert.ToInt32(StatusType.Generated)))
                {
                    basePage.Alert("Receive Material Number is not generated!", lnkSerach);
                }
                else
                {
                    basePage.Alert("Invalid Receive Material Number!", lnkSerach);
                }
            }
        }

        protected void chkSelectAll_Click(object sender, EventArgs e)
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
            CheckBox chkSelectAll = (CheckBox)gvMainGrid.HeaderRow.FindControl("chkSelectAll");
            foreach (GridViewRow row in gvMainGrid.Rows)
            {
                chkSelect = (CheckBox)row.FindControl("chkSelect");
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

        protected void btnAddSupplierItem_Click(object sender, EventArgs e)
        {
            if (lstItemTransaction == null)
            {
                lstItemTransaction = new List<ItemTransaction>();

            }
            #region Commented Code By Vishal
            //foreach (GridViewRow row in gvSupplier.Rows)
            //{
            //    lblItem = (Label)row.FindControl("lblItem");

            //    lblSpecification = (Label)row.FindControl("lblSpecification");
            //    lblItemCategory = (Label)row.FindControl("lblItemCategory");
            //    lblBrand = (Label)row.FindControl("lblMake");
            //    lblStore = (Label)row.FindControl("lblStore");
            //    lblItemQuantity = (Label)row.FindControl("lblItemQuantity");
            //    lblItemRecieve = (Label)row.FindControl("lblItemRecieve");
            //    lblItemLeft = (Label)row.FindControl("lblItemLeft");
            //    lblMeasurement = (Label)row.FindControl("lblMeasurement");
            //    hdfUnitMeasurementId = (HiddenField)row.FindControl("hdfUnitMeasurementId");
            //    chkSelect = (CheckBox)row.FindControl("chkSelect");
            //    hdfSupplierPOMappingId = (HiddenField)row.FindControl("hdfSupplierPOMappingId");
            //    hdfItemId = (HiddenField)row.FindControl("hdfItemId");
            //    hdfSpecificationId = (HiddenField)row.FindControl("hdfSpecificationId");
            //    hdnStore = (HiddenField)row.FindControl("hdnStore");
            //    hdnBrand = (HiddenField)row.FindControl("hdnBrand");

            //    if (chkSelect.Checked == true && hdfSupplierPOMappingId != null)
            //    {
            //        if (chkSelect.Checked.Equals(true))
            //        {
            //            itemTransaction = new ItemTransaction();
            //            itemTransaction.MetaProperty = new MetaData();
            //            itemTransaction.DeliverySchedule = new DeliveryScheduleDOM();
            //            itemTransaction.Item = new Item();
            //            itemTransaction.Item.ModelSpecification = new ModelSpecification();
            //            itemTransaction.Item.ModelSpecification.Category = new ItemCategory();
            //            itemTransaction.Item.ModelSpecification.UnitMeasurement = new MetaData();
            //            itemTransaction.DeliverySchedule.Id = Convert.ToInt32(hdfSupplierPOMappingId.Value);
            //            itemTransaction.Item.ItemId = Convert.ToInt32(hdfItemId.Value);
            //            itemTransaction.Item.ItemName = lblItem.Text.ToString();
            //            itemTransaction.Item.ModelSpecification.ModelSpecificationId = Convert.ToInt32(hdfSpecificationId.Value);
            //            itemTransaction.Item.ModelSpecification.ModelSpecificationName = lblSpecification.Text.ToString();
            //            itemTransaction.Item.ModelSpecification.Category.ItemCategoryName = lblItemCategory.Text.ToString();
            //            itemTransaction.Item.ModelSpecification.Brand.BrandName = lblBrand.Text.ToString();
            //            itemTransaction.Item.ModelSpecification.Store.StoreName = lblStore.Text.ToString();
            //            itemTransaction.Item.ModelSpecification.Store.StoreId = Convert.ToInt32(hdnStore.Value);
            //            itemTransaction.Item.ModelSpecification.Brand.BrandId = Convert.ToInt32(hdnBrand.Value);
            //            itemTransaction.Item.ModelSpecification.UnitMeasurement.Id = Convert.ToInt32(hdfUnitMeasurementId.Value);
            //            itemTransaction.Item.ModelSpecification.UnitMeasurement.Name = lblMeasurement.Text.ToString();
            //            itemTransaction.NumberOfUnit = Convert.ToDecimal(lblItemQuantity.Text.ToString());
            //            itemTransaction.QuantityReceived = Convert.ToDecimal(lblItemRecieve.Text.ToString());
            //            itemTransaction.QuantityReturned = Convert.ToDecimal(lblItemLeft.Text.ToString());
            //            itemTransaction.UnitLeft = Convert.ToDecimal(lblItemLeft.Text.ToString());

            //            itemTransaction.CreatedBy = basePage.LoggedInUser.UserLoginId;

            //            lstItemTransaction.Add(itemTransaction);
            //            chkSelect.Checked = false;
            //            chkSelect.Enabled = false;
            //        }
            //    }
            //}
            #endregion

            foreach (GridViewRow row in gvMainGrid.Rows)
            {
                lblItem = (Label)row.FindControl("lblItem");

                lblSpecification = (Label)row.FindControl("lblSpecification");
                lblItemCategory = (Label)row.FindControl("lblItemCategory");
                //lblBrand = (Label)row.FindControl("lblMake");
                //lblStore = (Label)row.FindControl("lblStore");
                lblItemQuantity = (Label)row.FindControl("lblItemQuantity");
                lblItemRecieve = (Label)row.FindControl("lblItemRecieve");
                lblItemLeft = (Label)row.FindControl("lblItemLeft");
                lblMeasurement = (Label)row.FindControl("lblMeasurement");
                //hdfUnitMeasurementId = (HiddenField)row.FindControl("hdfUnitMeasurementId");
                chkSelect = (CheckBox)row.FindControl("chkSelect");
                //hdfSupplierPOMappingId = (HiddenField)row.FindControl("hdfSupplierPOMappingId");
                hdfItemId = (HiddenField)row.FindControl("hdfItemId");
                hdfSpecificationId = (HiddenField)row.FindControl("hdfSpecificationId");
                //hdnStore = (HiddenField)row.FindControl("hdnStore");
                //hdnBrand = (HiddenField)row.FindControl("hdnBrand");

                if (chkSelect.Checked == true || hdfSupplierPOMappingId != null)
                {
                    if (chkSelect.Checked.Equals(true))
                    {
                        itemTransaction = new ItemTransaction();
                        itemTransaction.MetaProperty = new MetaData();
                        itemTransaction.DeliverySchedule = new DeliveryScheduleDOM();
                        itemTransaction.Item = new Item();
                        itemTransaction.Item.ModelSpecification = new ModelSpecification();
                        itemTransaction.Item.ModelSpecification.Category = new ItemCategory();
                        itemTransaction.Item.ModelSpecification.UnitMeasurement = new MetaData();
                        // itemTransaction.DeliverySchedule.Id = Convert.ToInt32(hdfSupplierPOMappingId.Value);
                        itemTransaction.Item.ItemId = Convert.ToInt32(hdfItemId.Value);
                        itemTransaction.Item.ItemName = lblItem.Text.ToString();
                        itemTransaction.Item.ModelSpecification.ModelSpecificationId = Convert.ToInt32(hdfSpecificationId.Value);
                        itemTransaction.Item.ModelSpecification.ModelSpecificationName = lblSpecification.Text.ToString();
                        itemTransaction.Item.ModelSpecification.Category.ItemCategoryName = lblItemCategory.Text.ToString();
                        //itemTransaction.Item.ModelSpecification.Brand.BrandName = lblBrand.Text.ToString();
                        //itemTransaction.Item.ModelSpecification.Store.StoreName = lblStore.Text.ToString();
                        //itemTransaction.Item.ModelSpecification.Store.StoreId = Convert.ToInt32(hdnStore.Value);
                        //itemTransaction.Item.ModelSpecification.Brand.BrandId = Convert.ToInt32(hdnBrand.Value);
                        //itemTransaction.Item.ModelSpecification.UnitMeasurement.Id = Convert.ToInt32(hdfUnitMeasurementId.Value);
                        itemTransaction.Item.ModelSpecification.UnitMeasurement.Name = lblMeasurement.Text.ToString();
                        itemTransaction.NumberOfUnit = Convert.ToDecimal(lblItemQuantity.Text.ToString());
                        itemTransaction.QuantityReceived = Convert.ToDecimal(lblItemRecieve.Text.ToString());
                        itemTransaction.QuantityReturned = Convert.ToDecimal(lblItemLeft.Text.ToString());
                        itemTransaction.UnitLeft = Convert.ToDecimal(lblItemLeft.Text.ToString());

                        itemTransaction.CreatedBy = basePage.LoggedInUser.UserLoginId;

                        lstItemTransaction.Add(itemTransaction);
                        chkSelect.Checked = false;
                        chkSelect.Enabled = false;
                    }
                }
            }

            if (lstItemTransaction.Count != 0)
            {

                BindItemReturnAddGrid();
                Enabled(true);
                txtReceiveMaterialNumber.Text = string.Empty;
                // txtContractorQuotationNumber.Text = string.Empty;
            }
            else
            {
                Enabled(false);
                basePage.Alert("Please Check At Least One Activity Description", btnAddSupplierItem);
            }
            foreach (TableCell item in gvMainGrid.HeaderRow.Cells)
            {
                CheckBox chbxSelectAll = (CheckBox)item.FindControl("chkSelectAll");
                if (chbxSelectAll.Checked == true)
                {
                    chbxSelectAll.Checked = false;
                }
            }
        }

        protected void gvSupplierAdd_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            String s = string.Empty;
            s = lstItemTransaction[index].Item.ItemName;
            if (e.CommandName == "cmdDelete")
            {

                lstItemTransaction.RemoveAt(index);
                foreach (GridViewRow row in gvSupplier.Rows)
                {
                    chkSelect = (CheckBox)row.FindControl("chkSelect");
                    Label lblItem = (Label)row.FindControl("lblItem");
                    if (lblItem.Text.Trim() == s)
                    {

                        chkSelect.Enabled = true;
                        chkSelect.Checked = false;
                        flag = true;
                        break;
                    }

                }
                if (lstItemTransaction.Count == 0)
                {
                    lstItemTransaction = null;
                }
                BindItemReturnAddGrid();
            }
        }
        protected void btnSaveDraft_Click(object sender, EventArgs e)
        {
            ltrl_err_msg.Text = string.Empty;
            metaData = new MetaData();
            returnMaterialDOM = new ReturnMaterialDOM();
            returnMaterialDOM = GetReturnMaterialDetails();
            if (!string.IsNullOrEmpty(strInvalid))
            {
                ltrl_err_msg.Text = strInvalid;
            }
            else
            {
                if (ReturnMaterialId > 0)
                {
                    metaData = CreateReturnMaterial(returnMaterialDOM, ReturnMaterialId);
                }
                else
                {
                    metaData = CreateReturnMaterial(returnMaterialDOM, null);
                }
                if (metaData.Id > 0)
                {
                    CreateDocumentMapping();
                    if (ReturnMaterialId > 0)
                    {
                        basePage.Alert("Issue Material No: " + metaData.Name + " Updated Successfully", btnSaveDraft, "ViewReturnMaterial.aspx");
                    }
                    else
                    {
                        basePage.Alert("Issue Material No: " + metaData.Name + " Created Successfully", btnSaveDraft, "ReturnMaterial.aspx");
                    }
                    ltrl_err_msg.Text = string.Empty;
                    lstItemTransaction = null;
                    BindItemReturnAddGrid();
                }
            }
        }
        protected void gvSupplier_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //foreach (GridViewRow row in gvSupplier.Rows)
            //{
            //    hdfItemId = (HiddenField)row.FindControl("hdfItemId");
            //    lblItemLeft = (Label)row.FindControl("lblItemLeft");
            //    hdfSpecificationId = (HiddenField)row.FindControl("hdfSpecificationId");
            //    chkSelect = (CheckBox)row.FindControl("chkSelect");
            //    if (lstItemTransaction.Count > 0)
            //    {
            //        foreach (ItemTransaction item in lstItemTransaction)
            //        {
            //            if (Convert.ToDecimal(lblItemLeft.Text.ToString()) == 0 || (Convert.ToInt32(hdfItemId.Value) == item.Item.ItemId && Convert.ToInt32(hdfSpecificationId.Value) == item.Item.ModelSpecification.ModelSpecificationId))
            //            {
            //                chkSelect.Checked = false;
            //                chkSelect.Enabled = false;
            //            }
            //        }
            //    }
            //    else if (Convert.ToDecimal(lblItemLeft.Text.ToString()) == 0)
            //    {
            //        chkSelect.Checked = false;
            //        chkSelect.Enabled = false;
            //    }
            //}
        }
        protected void gvSupplierAdd_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList ddlBrandlist = (DropDownList)e.Row.FindControl("ddlBrandList");
                // Here we have to bind drop down for the brand
                var lstBrand = brandBL.ReadBrands(null);

                if (lstBrand != null && lstBrand.Count > 0)
                {

                    var brands = lstBrand.ConvertAll(x => new MetaData { Id = x.BrandId, Name = x.BrandName }).ToList();
                    basePage.BindDropDown(ddlBrandlist, "Name", "Id", brands);
                    // ddlBrandlist.SelectedValue = lstItemTransaction[count].Item.ModelSpecification.Brand.BrandId.ToString();
                }



                if (ReturnMaterialId != 0)
                {
                    if (lstItemTransaction != null)
                    {
                        ddlBrandlist.SelectedValue = lstItemTransaction[e.Row.RowIndex].Item.ModelSpecification.Brand.BrandId.ToString();

                        foreach (ItemTransaction Item in lstItemTransaction)
                        {
                            foreach (ItemTransaction Items in lstPreItemTransaction)
                            {
                                if (Item.Item.ItemId == Items.Item.ItemId && Item.Item.ModelSpecification.ModelSpecificationId == Items.Item.ModelSpecification.ModelSpecificationId)
                                {
                                    Item.UnitIssued = Items.UnitIssued;
                                    Item.UnitLeft = Items.UnitLeft;
                                    Item.CreatedBy = basePage.LoggedInUser.UserLoginId;
                                }
                            }
                        }
                    }
                }
            }


        }
        #endregion

        #region Private Method
        private void BindUpdate()
        {
            lstReturnMaterialDOM = new List<ReturnMaterialDOM>();
            lstItemTransaction = new List<ItemTransaction>();
            lstSupplierRecieveMatarial = new List<SupplierRecieveMatarial>();
            supplierRecieveMaterialBAL = new SupplierRecieveMaterialBAL();
            returnMaterialBL = new ReturnMaterialBL();
            lstReturnMaterialDOM = returnMaterialBL.ReadSupplierReturnMaterial(ReturnMaterialId, null, null, null, null, null, System.DateTime.MinValue, System.DateTime.MinValue);
            if (lstReturnMaterialDOM.Count > 0)
            {
                ReturnMaterialId = lstReturnMaterialDOM[0].RetutrnMaterialId;
                Status = lstReturnMaterialDOM[0].RecieveMatarial.Quotation.StatusType.Id;
                Enabled(true);
            }
            lstItemTransaction = returnMaterialBL.ReadSupplierReturnMaterialMapping(lstReturnMaterialDOM[0].RecieveMatarial.SupplierRecieveMaterialNumber, null);
            lstSupplierRecieveMatarial = supplierRecieveMaterialBAL.SearchReceiveMaterial(string.Empty, string.Empty, lstReturnMaterialDOM[0].RecieveMatarial.SupplierRecieveMaterialNumber, System.DateTime.MinValue, System.DateTime.MinValue, null);

            if (lstSupplierRecieveMatarial.Count > 0 && (lstSupplierRecieveMatarial[0].Quotation.StatusType.Id == Convert.ToInt32(StatusType.Generated)))
            {
                txtRMDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
                SetAllDefaultData(lstSupplierRecieveMatarial);
                pnlRecieveMetarial.Visible = true;
                //Enabled(false);               
            }
        }
        private void SetAllDefaultData(List<SupplierRecieveMatarial> lst)
        {
            if (ReturnMaterialId > 0)
            {
                BindUpdatedText(lstReturnMaterialDOM);
            }
            else
            {
                BindText(lst);
            }

            BindItemReturnGrid(lst);
            if (lstItemTransaction != null)
            {
                BindItemReturnAddGrid();
            }
        }
        private MetaData CreateReturnMaterial(ReturnMaterialDOM returnMaterialDOM, Int32? ReturnMaterialId)
        {
            if (lstItemTransaction != null)
            {
                metaData = new MetaData();
                //issueMaterialBL = new IssueMaterialBL();
                returnMaterialBL = new ReturnMaterialBL();
                metaData = returnMaterialBL.CreateReturnMaterial(returnMaterialDOM, ReturnMaterialId);
            }
            return metaData;
        }
        private ReturnMaterialDOM GetReturnMaterialDetails()
        {

            strInvalid = string.Empty;
            flag = false;
            j = 0;
            returnMaterialDOM = new ReturnMaterialDOM();
            returnMaterialBL = new ReturnMaterialBL();
            returnMaterialDOM.RecieveMatarial = new SupplierRecieveMatarial();
            returnMaterialDOM.RecieveMatarial.Quotation = new QuotationDOM();
            returnMaterialDOM.RecieveMatarial.Quotation.StatusType = new MetaData();
            if (ReturnMaterialId > 0)
            {
                returnMaterialDOM.RetutrnMaterialId = ReturnMaterialId;
            }
            returnMaterialDOM.RecieveMatarial.Quotation.UploadDocumentId = basePage.DocumentStackId;
            //returnMaterialDOM.ReturnMaterialNumber = lblR.Text;
            returnMaterialDOM.RecieveMatarial.Quotation.SupplierId = Convert.ToInt32(hdnSupplierId.Value);
            returnMaterialDOM.RecieveMatarial.Quotation.SupplierQuotationNumber = lblPurchaseOrderNumber.Text;
            returnMaterialDOM.RecieveMatarial.SupplierRecieveMaterialNumber = lblReceiveMaterialNumber.Text;
            returnMaterialDOM.RecieveMatarial.Quotation.SupplierName = lblSupplierName.Text;
            returnMaterialDOM.RecieveMatarial.DeliveryChallanNumber = lblDeliveryChallanNumber.Text;
            returnMaterialDOM.RecieveMatarial.Quotation.StatusType.Id = Convert.ToInt32(StatusType.Pending);
            returnMaterialDOM.RecieveMatarial.Quotation.OrderDate = Convert.ToDateTime(lblPurchaseOrderDate.Text);

            returnMaterialDOM.ReturnMaterialDate = Convert.ToDateTime(txtRMDate.Text.Trim());

            returnMaterialDOM.CreatedBy = basePage.LoggedInUser.UserLoginId;
            for (i = 0; i < gvSupplierAdd.Rows.Count; i++)
            {
                BigNo = 0;
                dec = 0;
                BigNo = (lstItemTransaction[i].UnitLeft + lstItemTransaction[i].QuantityReturned);
                TextBox box = (TextBox)gvSupplierAdd.Rows[i].Cells[10].FindControl("txtReturnQuantity");
                TextBox txtRemark = (TextBox)gvSupplierAdd.Rows[i].Cells[11].FindControl("txtRemark");
                DropDownList ddlBrand = (DropDownList)gvSupplierAdd.Rows[i].Cells[5].FindControl("ddlBrandList");
                lblIndex = (Label)gvSupplierAdd.Rows[i].Cells[0].FindControl("lblIndex");
                //hdnStore = (HiddenField)gvSupplierAdd.Rows[i].Cells[4].FindControl("hdnStore");
                // hdnBrand = (HiddenField)gvSupplierAdd.Rows[i].Cells[5].FindControl("hdnBrand");
                dec = TryToParse(box.Text);
                // lstItemTransaction[i].Item.ModelSpecification.Store.StoreId = Convert.ToInt32(hdnStore.Value);
                if (ddlBrand != null && ddlBrand.SelectedIndex > 0)
                {
                    lstItemTransaction[i].Item.ModelSpecification.Brand.BrandId = Convert.ToInt32(ddlBrand.SelectedValue);
                }
                else
                {
                    strInvalid = "Select Brand for item at Index: " + strInvalid + "</br>";
                }
                if (dec > 0)
                {
                    cnt = 0;
                    cnt = NumberDecimalPlaces(dec);
                    if (ReturnMaterialId > 0)
                    {
                        if (cnt > 2 || dec > lstItemTransaction[i].UnitLeft)
                        {
                            if (j > 0)
                            {
                                strInvalid = strInvalid + ",  " + " " + lblIndex.Text.Trim();
                                j++;
                            }
                            else
                            {
                                strInvalid = strInvalid + lblIndex.Text.Trim();
                                j++;
                            }
                            flag = true;
                        }
                    }
                    else if (cnt > 2 || dec > lstItemTransaction[i].UnitLeft)
                    {
                        if (j > 0)
                        {
                            strInvalid = strInvalid + ",  " + " " + lblIndex.Text.Trim();
                            j++;
                        }
                        else
                        {
                            strInvalid = strInvalid + lblIndex.Text.Trim();
                            j++;
                        }
                        flag = true;
                    }
                }
                else
                {
                    if (j > 0)
                    {
                        strInvalid = strInvalid + ",   " + " " + lblIndex.Text.Trim();
                        j++;
                    }
                    else
                    {
                        strInvalid = strInvalid + lblIndex.Text.Trim();
                        j++;
                    }
                    flag = true;
                }
                if (string.IsNullOrEmpty(strInvalid))
                {
                    lstItemTransaction[i].QuantityReturned = Convert.ToDecimal(box.Text);
                    lstItemTransaction[i].Remark = (txtRemark.Text);
                }
            }
            if (!string.IsNullOrEmpty(strInvalid))
            {
                strInvalid = "Unit Required allows only numeric value up to 2 decimal places & less than or equal to unit left at Index: " + strInvalid + "</br>";
            }
            else
            {
                returnMaterialDOM.RecieveMatarial.Quotation.ItemTransaction = lstItemTransaction;
            }
            return returnMaterialDOM;
        }

        private void BindItemReturnGrid(List<SupplierRecieveMatarial> lst)
        {
            quotationBL = new QuotationBL();
            lstPreItemTransaction = new List<ItemTransaction>();
            if (lst.Count > 0)
            {
                lstPreItemTransaction = quotationBL.ReadSupplierQuotationMapping(lst[0].Quotation.SupplierQuotationId);

                if (lstPreItemTransaction.Count > 0)
                {
                    var finalList = lstPreItemTransaction.AsEnumerable()
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
                        x.Item.ModelSpecification.UnitMeasurement.Id


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
                        a.Name

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
                QuantityDemand = b.Key.NumberOfUnit
            }).ToList();


                    gvMainGrid.DataSource = finalList;
                    gvMainGrid.DataBind();
                }


                gvSupplier.DataSource = lstPreItemTransaction;
                gvSupplier.DataBind();
            }
        }

        private void BindItemReturnAddGrid()
        {
            gvSupplierAdd.DataSource = lstItemTransaction;
            gvSupplierAdd.DataBind();
        }

        private Decimal TryToParse(string Value)
        {
            dec = 0;
            Decimal.TryParse(Value, out dec);
            return dec;
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
        private void BindText(List<SupplierRecieveMatarial> lst)
        {

            lblPurchaseOrderNumber.Text = lst[0].Quotation.SupplierQuotationNumber;
            lblPurchaseOrderDate.Text = lst[0].Quotation.OrderDate.ToString("dd-MMM-yyyy");
            lblReceiveMaterialNumber.Text = lst[0].SupplierRecieveMaterialNumber;
            lblReceiveMaterialDate.Text = lst[0].RecieveMaterialDate.ToShortDateString();
            hdnSupplierId.Value = lst[0].Quotation.SupplierId.ToString();
            lblSupplierName.Text = lst[0].Quotation.SupplierName.ToString();
            //if (ReturnMaterialId > 0)
            //{
            //    GetDocumentData(lst[0].DemandVoucher.Quotation.UploadDocumentId);
            //}
            lblDeliveryChallanNumber.Text = lst[0].DeliveryChallanNumber.ToString();

        }
        private void BindUpdatedText(List<ReturnMaterialDOM> lst)
        {

            lblPurchaseOrderNumber.Text = lst[0].RecieveMatarial.Quotation.SupplierQuotationNumber;
            lblPurchaseOrderDate.Text = lst[0].RecieveMatarial.Quotation.OrderDate.ToString("dd-MMM-yyyy");
            lblReceiveMaterialNumber.Text = lst[0].RecieveMatarial.SupplierRecieveMaterialNumber;
            lblReceiveMaterialDate.Text = lst[0].RecieveMatarial.RecieveMaterialDate.ToShortDateString();
            hdnSupplierId.Value = lst[0].RecieveMatarial.Quotation.SupplierId.ToString();
            lblSupplierName.Text = lst[0].RecieveMatarial.Quotation.SupplierName.ToString();

            GetDocumentData(lst[0].RecieveMatarial.Quotation.UploadDocumentId);

            lblDeliveryChallanNumber.Text = lst[0].RecieveMatarial.DeliveryChallanNumber.ToString();

        }

        private void Enabled(Boolean Condition)
        {
            btnSaveDraft.Visible = Condition;
            btnReset.Visible = Condition;
        }
        #endregion

        #region Public Property
        private int SupplierRecieveMaterialId
        {
            get
            {
                return (Int32)ViewState["SupplierRecieveMaterialId"];
            }
            set
            {
                ViewState["SupplierRecieveMaterialId"] = value;
            }
        }
        private int ReturnMaterialId
        {
            get
            {
                return (Int32)ViewState["ReturnMaterialId"];
            }
            set
            {
                ViewState["ReturnMaterialId"] = value;
            }
        }
        private String QuotationNumber
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

        private Int32 QuotationId
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

        public List<ReturnMaterialDOM> lstReturnMaterialDOM
        {
            get
            {
                return (List<ReturnMaterialDOM>)ViewState["lstReturnMaterialDOM"];
            }
            set
            {
                ViewState["lstReturnMaterialDOM"] = value;
            }
        }
        private List<ItemTransaction> lstItemTransaction
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
        private List<ItemTransaction> lstPreItemTransaction
        {
            get
            {
                return (List<ItemTransaction>)ViewState["lstPreItemTransaction"];
            }
            set
            {
                ViewState["lstPreItemTransaction"] = value;
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

            if (basePage.DocumentStackId == 0)
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
                lst_document = basePage.DocumentsList;
                lst_document.RemoveAt(Index);
                basePage.DocumentsList = lst_document;
                BindDocument();
            }
            else if (e.CommandName == "OpenFile")
            {
                Index = Convert.ToInt32(e.CommandArgument);
                Index = Convert.ToInt32(e.CommandArgument);
                lst_document = new List<Document>();
                lst_document = basePage.DocumentsList;


                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                LinkButton lbtn = (LinkButton)row.FindControl("lbtn_file");

                string fileName = lst_document[Index].Replaced_Name;
                string strURL = lst_document[Index].Path + @"\" + lst_document[Index].Replaced_Name;

                Session["FilePath"] = Server.MapPath(strURL);
                Session["OriginalFileName"] = lst_document[Index].Original_Name;
                Session["ReplacedFileName"] = lst_document[Index].Replaced_Name;
                basePage.OpenPopupWithUpdatePanelForFileDownload(lbtn, "../Parts/FileDownload.aspx?id=" + "File", "DownloadFile");



            }
        }

        #endregion

        #region Private Methods

        private void ManageSession(bool forCopy)
        {
            RequestPageName = pageName;
            if (forCopy)
            {
                basePage.DocumentStackId = 0;
            }
            else if (basePage.Page_Name == null || basePage.Page_Name != RequestPageName)
            {
                basePage.Page_Name = RequestPageName;
                basePage.DocumentStackId = 0;
                basePage.DocumentSerial = 0;
                basePage.DocumentsList = null;
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
            document.CreatedBy = basePage.LoggedInUser.UserLoginId;
            basePage.DocumentStackId = document_BL.CreateAndReadDocumnetStackId(document);
            return basePage.DocumentStackId;
        }

        private void DirectoryHandle(FileUpload fileupload)
        {
            if (fileupload.HasFile)
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
                    if (basePage.DocumentStackId != 0)
                    {
                        document = new Document();
                        lst_document = new List<Document>();
                        flag = false;

                        document.Original_Name = fileupload.FileName.Split('\\').Last();
                        if (basePage.DocumentsList != null)
                        {
                            foreach (Document item in basePage.DocumentsList)
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
                            basePage.Alert(GlobalConstants.M_File_Exist, FileUpload_Control);
                        }
                        else
                        {
                            basePage.DocumentSerial = basePage.DocumentSerial + 1;

                            File_Extension = System.IO.Path.GetExtension(fileupload.FileName.Split('\\').Last());
                            document.Replaced_Name = Convert.ToString(basePage.DocumentStackId) + "_" + Convert.ToString(basePage.DocumentSerial) + File_Extension;

                            File_Path = Sub_Folder_Path + @"\" + document.Replaced_Name;
                            //File_Path = Sub_Folder_Path + @"\" + document.Original_Name;
                            //Upload file in respective path
                            FileUpload_Control.SaveAs(File_Path);

                            document.DocumentId = basePage.DocumentStackId;

                            document.Path = ConfigurationManager.AppSettings["Upload_Document_Path"].ToString() + Year.ToString() + @"\" + RequestPageName;
                            document.LastIndex = basePage.DocumentSerial;


                            if (basePage.DocumentsList == null)
                            {
                                lst_document.Add(document);
                            }
                            else
                            {
                                lst_document = basePage.DocumentsList;
                                lst_document.Add(document);
                            }

                            //Add Doc's info in list
                            basePage.DocumentsList = lst_document;
                        }
                    }
                }
            }
        }

        public void BindDocument()
        {
            if (basePage.DocumentsList != null)
            {
                gv_documents.DataSource = basePage.DocumentsList;
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
            lst_document = basePage.DocumentsList;

            if (lst_document != null)
            {
                //For Reset IsDeleted All documents realted to Document_Id
                document_BL.ResetDocumentMapping(Convert.ToInt32(basePage.DocumentStackId));

                //For Insert/Update the Documents
                foreach (Document doc in lst_document)
                {
                    document = new Document();
                    document.DocumentId = Convert.ToInt32(basePage.DocumentStackId);
                    document.Original_Name = doc.Original_Name;
                    document.Replaced_Name = doc.Replaced_Name;
                    document.Path = doc.Path;
                    //DocumentSerial is the last updated document
                    document.LastIndex = basePage.DocumentSerial;
                    document.CreatedBy = basePage.LoggedInUser.UserLoginId;
                    document.Id = doc.Id;
                    document_BL.CreateDocumentMapping(document);
                }
            }
        }

        private void GetDocumentData(Int32 Document_Id)
        {
            document_BL = new DocumentBL();
            lst_document = new List<Document>();
            lst_document = document_BL.ReadDocumnetMapping(Document_Id);
            if (lst_document.Count >= 1)
            {
                basePage.DocumentsList = lst_document;
                basePage.DocumentStackId = lst_document[0].DocumentId;
                basePage.DocumentSerial = lst_document[0].LastIndex;
                basePage.Page_Name = pageName;
                BindDocument();
            }
        }

        public void EmptyDocumentList()
        {
            basePage.DocumentStackId = 0;
            basePage.DocumentSerial = 0;
            basePage.DocumentsList = null;
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

        // popup search SPONRecive
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string supplierName = txtName.Text;
            DateTime fromDate;
            DateTime toDate;
            if (txtFromDate.Text == "")
            {
                fromDate = DateTime.MinValue;
            }
            else
            {
                fromDate = Convert.ToDateTime(txtFromDate.Text);
            }
            if (txtToDate.Text == "")
            {
                toDate = DateTime.MinValue;
            }
            else
            {
                toDate = Convert.ToDateTime(txtToDate.Text);
            }

            BindGrid(null, null, null, toDate, fromDate, supplierName);
            ClearText();
            ModalPopupExtender2.Show();
        }

        protected void rbtSelect_OncheckChanged(object sender, System.EventArgs e)
        {
            foreach (GridViewRow oldRow in gvRSM.Rows)
            {
                ((RadioButton)oldRow.FindControl("rbtSelect")).Checked = false;
            }
            RadioButton rb = (RadioButton)sender;
            GridViewRow row = (GridViewRow)rb.NamingContainer;
            ((RadioButton)row.FindControl("rbtSelect")).Checked = true;
            Label ReceiveMaterialNumber = (Label)row.FindControl("lnkSRMNo");
            txtReceiveMaterialNumber.Text = ReceiveMaterialNumber.Text.ToString();
        }

        private void BindGrid(String SupplierPONumber, String ChallanNo, String SupplierReceiveMaterialNo, DateTime ToDate, DateTime FromDate, string name)
        {
            //lstQuotation = new List<QuotationDOM>();
            lstSupplierRecieveMatarial = new List<SupplierRecieveMatarial>();
            lstSupplierRecieveMatarial = SupplierReceiveMatBal.SearchReceiveMaterial(SupplierPONumber, ChallanNo, SupplierReceiveMaterialNo, ToDate, FromDate, name);
            if (lstSupplierRecieveMatarial.Count > 0)
            {

                gvRSM.DataSource = lstSupplierRecieveMatarial;
                gvRSM.DataBind();
            }
            else
            {
                BindEmptyGrid(gvRSM);
            }
        }



        private void BindEmptyGrid(GridView gv)
        {
            gv.DataSource = new List<object>();
            gv.DataBind();
        }

        private void ClearText()
        {
            txtName.Text = string.Empty;
            txtFromDate.Text = string.Empty;
            txtToDate.Text = string.Empty;
        }

        #endregion

        protected void gvMainGrid_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            foreach (GridViewRow row in gvMainGrid.Rows)
            {
                hdfItemId = (HiddenField)row.FindControl("hdfItemId");
                lblItemLeft = (Label)row.FindControl("lblItemLeft");
                hdfSpecificationId = (HiddenField)row.FindControl("hdfSpecificationId");
                chkSelect = (CheckBox)row.FindControl("chkSelect");
                if (lstItemTransaction.Count > 0)
                {


                    foreach (ItemTransaction item in lstItemTransaction)
                    {
                        if (Convert.ToDecimal(lblItemLeft.Text.ToString()) <= 0 || (Convert.ToInt32(hdfItemId.Value) == item.Item.ItemId && Convert.ToInt32(hdfSpecificationId.Value) == item.Item.ModelSpecification.ModelSpecificationId))
                        {
                            chkSelect.Checked = false;
                            chkSelect.Enabled = false;
                        }
                    }
                }
                else if (Convert.ToDecimal(lblItemLeft.Text.ToString()) <= 0)
                {
                    chkSelect.Checked = false;
                    chkSelect.Enabled = false;
                }
            }
        }
    }
}