using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DocumentObjectModel;
using BusinessAccessLayer;
using MiniERP.Shared;

namespace MiniERP.Admin
{
    public partial class SearchItemStock : BasePage
    {
        #region Private Global Variables

        Contractor contractor = null;
        Supplier supplier = null;
        List<Contractor> lstContractor = null;
        SearchBAL searchBL = new SearchBAL();
        StoreBL storeBL = new StoreBL();
        List<Supplier> lstSupplier = null;

        #region Search Item Stock Section       
            

        #endregion

        #region Item Search Section

            string ItemName = "";
            string Specification = "";
            int ItemUnitId = 0;
            int ItemCategoryId = 0;
            string Brand = "";
            string ModelCode = "";
            List<Item> lstItem = new List<Item>();
            ItemBL itemBL = new ItemBL();
            string Id = String.Empty;
            string contractorId = string.Empty;
            string supplierId = string.Empty;

        #endregion    

       
        #region Protected Section

        MetaDataBL metadataBL = new MetaDataBL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string PageName = Request.QueryString["PageName"];
                if (PageName == "ManageItemStock")
                {
                    pnlSearchItemStock.Visible = true;
                    pnlSearchItem.Visible = false;
                    pnlSearchContractor.Visible = false;
                    BindDropDown(ddlUnitMeasurementS, "Name", "Id", metadataBL.ReadMetaDataUnitMeasurement());
                    BindDropDown(ddlStore, "StoreName", "StoreId", storeBL.ReadStore(null));
                }
                else if (PageName == "ManageItem")
                {
                    pnlSearchItemStock.Visible = false;
                    pnlSearchItem.Visible = true;
                    pnlSearchContractor.Visible = false;
                    BindDropDown(ddlUnitMeasurement, "Name", "Id", metadataBL.ReadMetaDataUnitMeasurement());
                    BindDropDown(ddlCategory, "Range", "ItemCategoryId", itemBL.ReadRanges(null));
                    
                }
                else if (PageName == "ManageContractor")
                {
                    lblHeaderSearch.Text = "Search Contractor";
                    pnlSearchItemStock.Visible = false;
                    pnlSearchItem.Visible = false;
                    pnlSearchContractor.Visible = true;
                }
                else if (PageName == "ManageSupplier")
                {
                    lblHeaderSearch.Text = "Search Supplier";
                    pnlSearchItemStock.Visible = false;
                    pnlSearchItem.Visible = false;
                    pnlSearchContractor.Visible = true;
                    lblCompanyName.Text = "Supplier Name";
                    lblCpnMobile.Text = "Supplier Mobile No.";
                    lblCpnPhone.Text = "Supplier Phone No.";
                    lblServiceTaxNo.Visible = false;
                    txtServiceTaxNo.Visible = false;
                }
            }            
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string PageName = Request.QueryString["PageName"];
            if (PageName == "ManageItem")
            {                
                ItemName = txtItemName.Text.Trim();
                Specification = txtSpecification.Text.Trim();
                ItemUnitId=Convert.ToInt32(ddlUnitMeasurement.SelectedValue);
                
                ItemCategoryId = Convert.ToInt32(ddlCategory.SelectedValue);
                
                Brand = txBrand.Text.Trim();
                ModelCode = txtModelCode.Text.Trim();
                if ( ItemUnitId==0 && ItemCategoryId == 0 )
                {
                    SearchItemByItemModel(ItemName, Specification, null, null, Brand, ModelCode);
                }
                else if (ItemUnitId != 0 && ItemCategoryId == 0)
                {
                    SearchItemByItemModel(ItemName, Specification, ItemUnitId, null, Brand, ModelCode);
                }
                else if (ItemUnitId == 0 && ItemCategoryId != 0)
                {
                    SearchItemByItemModel(ItemName, Specification, null, ItemCategoryId, Brand, ModelCode);
                }
                else
                {
                    SearchItemByItemModel(ItemName, Specification, ItemUnitId, ItemCategoryId, Brand, ModelCode);
                }                               
            }
            else if (PageName == "ManageItemStock")
            {
                int? storeId =Convert.ToInt32(ddlStore.SelectedValue);
                ItemName = txtItemNameS.Text;
                Specification = txtSpecificationS.Text;
                ItemUnitId = Convert.ToInt32(ddlUnitMeasurementS.SelectedValue);
                if (ItemUnitId == 0)
                {
                    SearchItemStockByItemModel(storeId,ItemName, Specification, null);
                }
                else
                {
                    SearchItemStockByItemModel(storeId,ItemName, Specification, ItemUnitId);
                }
            }
            else if (PageName == "ManageContractor")
            {
                // lstContractor = new List<Contractor>();
                contractor = new Contractor();
                if (txtCompanyName.Text.Trim() != String.Empty)
                {
                    contractor.Name = txtCompanyName.Text.Trim();
                }
                

                if (txtCpnEmailId.Text.Trim()!=String.Empty)
                {
                    contractor.Email = txtCpnEmailId.Text.Trim();
                }
                

                if (txtCity.Text.Trim()!=String.Empty)
                {
                    contractor.City = txtCity.Text.Trim();
                }
                

                if (txtState.Text.Trim()!=String.Empty)
                {
                    contractor.State = txtState.Text.Trim();
                }
                

                if (txtCpnPhone.Text.Trim()!=String.Empty)
                {
                    contractor.Phone = txtCpnPhone.Text.Trim();
                }
                

                if (txtCpnMobile.Text.Trim()!=String.Empty)
                {
                    contractor.Mobile = txtCpnMobile.Text.Trim();
                }
                

                if (txtServiceTaxNo.Text.Trim()!=String.Empty)
                {
                    contractor.Information.ServiceTaxNumber = txtServiceTaxNo.Text.Trim();
                }
                

                if (txtPAN.Text.Trim()!=String.Empty)
                {
                    contractor.Information.PanNumber = txtPAN.Text.Trim();
                }
                

                if(txtESI.Text.Trim()!=String.Empty)
                {
                    contractor.Information.EsiNumber = txtESI.Text.Trim();
                }
               

                if (txtTAN.Text.Trim()!=String.Empty)
                {
                    contractor.Information.TanNumber = txtTAN.Text.Trim();
                }
                

                if (txtFax.Text.Trim()!=String.Empty)
                {
                    contractor.Information.FaxNumber = txtFax.Text.Trim();
                }
                

                if (txtPF.Text.Trim()!=String.Empty)
                {
                    contractor.Information.PfNumber = txtPF.Text.Trim();
                }
               

                if (txtWebSite.Text.Trim()!=String.Empty)
                {
                    contractor.Website = txtWebSite.Text.Trim(); 
                }


                SearchContractorByIds(contractor);
                
            }

            else if (PageName == "ManageSupplier")
            {
                
                supplier = new Supplier();
                if (txtCompanyName.Text.Trim() != String.Empty)
                {
                    supplier.Name = txtCompanyName.Text.Trim();
                }


                if (txtCpnEmailId.Text.Trim() != String.Empty)
                {
                    supplier.Email = txtCpnEmailId.Text.Trim();
                }


                if (txtCity.Text.Trim() != String.Empty)
                {
                    supplier.City = txtCity.Text.Trim();
                }


                if (txtState.Text.Trim() != String.Empty)
                {
                    supplier.State = txtState.Text.Trim();
                }


                if (txtCpnPhone.Text.Trim() != String.Empty)
                {
                    supplier.Phone = txtCpnPhone.Text.Trim();
                }


                if (txtCpnMobile.Text.Trim() != String.Empty)
                {
                    supplier.Mobile = txtCpnMobile.Text.Trim();
                }


                //if (txtServiceTaxNo.Text.Trim() != String.Empty)
                //{
                //    contractor.Information.ServiceTaxNumber = txtServiceTaxNo.Text.Trim();
                //}


                if (txtPAN.Text.Trim() != String.Empty)
                {
                    supplier.Information.PanNumber = txtPAN.Text.Trim();
                }


                if (txtESI.Text.Trim() != String.Empty)
                {
                    supplier.Information.EsiNumber = txtESI.Text.Trim();
                }


                if (txtTAN.Text.Trim() != String.Empty)
                {
                    supplier.Information.TanNumber = txtTAN.Text.Trim();
                }


                if (txtFax.Text.Trim() != String.Empty)
                {
                    supplier.Information.FaxNumber = txtFax.Text.Trim();
                }


                if (txtPF.Text.Trim() != String.Empty)
                {
                    supplier.Information.PfNumber = txtPF.Text.Trim();
                }


                if (txtWebSite.Text.Trim() != String.Empty)
                {
                    supplier.Website = txtWebSite.Text.Trim();
                }


                SearchSupplierByIds(supplier);

            }
        }


        #endregion

        #region Private Section

        private void SearchSupplierByIds(Supplier supplier)
        {
            lstSupplier = searchBL.SearchSupplier(supplier);
            for (int i = 0; i < lstSupplier.Count; i++)
            {
                if (i == 0)
                {
                    supplierId = supplierId + lstSupplier[i].SupplierId;
                }
                else
                {
                    supplierId = supplierId + "," + lstSupplier[i].SupplierId;
                }

            }
            ClosePopUpWithRefereshParent();
            if (string.IsNullOrEmpty(supplierId))
            {
                supplierId = "0";
            }
            Session["supplierId"] = supplierId;
            Session["Flag"] = "4";
        }

        

        private void SearchContractorByIds(Contractor contractor)
        {
            lstContractor = searchBL.SearchContractor(contractor);
            for (int i = 0; i < lstContractor.Count; i++)
            {
                if (i==0)
                {
                    contractorId = contractorId + lstContractor[i].ContractorId; 
                }
                else
                {
                    contractorId = contractorId + "," + lstContractor[i].ContractorId;
                }
               
            }
            ClosePopUpWithRefereshParent();
            if (string.IsNullOrEmpty(contractorId))
            {
                contractorId = "0";
            }
            Session["ContractorId"] = contractorId;
            Session["Flag"] = "3";
        }
        private void SearchItemStockByItemModel(int? storeId,String itemname, String itemspec, int? itemunitid)
        {
            List<ItemStock> lstItemStock = new List<ItemStock>();
            lstItemStock = searchBL.ItemStock(storeId,itemname, itemspec, itemunitid);
            for (int i = 0; i < lstItemStock.Count; i++)
            {
                if (i == 0)
                {
                    Id = Id + lstItemStock[i].ItemStockId;
                }
                else
                {
                    Id = Id + "," + lstItemStock[i].ItemStockId;
                }
            }
            ClosePopUpWithRefereshParent();
            if (string.IsNullOrEmpty(Id))
            {
                Id = "0";
            }
            Session["ItemStockIds"] = Id;
            Session["Flag"] = "1";
        }

        #endregion

       #endregion

       #region Search Item

        #region Protected Section

        #endregion

        #region Private Section

        private void SearchItemByItemModel(String ItemName, String Specification, int? ItemUnitId, int? ItemCategoryId, String Brand, String ModelCode)
        {
            List<Item> lstItem = new List<Item>();
            lstItem = searchBL.SearchItem(ItemName, Specification, ItemUnitId, ItemCategoryId, Brand,ModelCode);
            // Session["ListItem"] = lstItem;            Commented By Jai
            
            for (int i = 0; i < lstItem.Count; i++)
            {
                if (i == 0)
                {
                    Id = Id + lstItem[i].ModelSpecification.ModelSpecificationId;
                }
                else
                {
                    Id = Id + "," + lstItem[i].ModelSpecification.ModelSpecificationId;
                }
            }
            
            ClosePopUpWithRefereshParent();
            if (string.IsNullOrEmpty(Id))
            {
                Id = "0";
            }
            Session["SpecificationIds"] = Id;
            Session["Flag"] = "2";                     
            
        }

        private void BindRangesItem()
        {
            ItemCategory itemCategory = new ItemCategory();
            List<ItemCategory> lstItemCategory = new List<ItemCategory>();
            lstItemCategory = itemBL.ReadRanges(null);
            ddlCategory.DataSource = lstItemCategory;
            ddlCategory.DataValueField = "ItemCategoryId";
            ddlCategory.DataTextField = "Range";
            ddlCategory.DataBind();

            ddlCategory.Items.Insert(0, new ListItem("0","--Select--"));
            ddlCategory.SelectedValue = "0";
        }

        private List<ItemCategory> GetItemCategoryForItem()
        {
            List<ItemCategory> listitemcategory = new List<ItemCategory>();
            ItemCategoryBL itemcategoryBL = new ItemCategoryBL();
            listitemcategory = itemcategoryBL.ReadItemCategory();
            return listitemcategory;
        }

        #endregion

       #endregion

        #region

       
        #endregion
    }
}