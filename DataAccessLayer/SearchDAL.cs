using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using DocumentObjectModel;
using System.Data.Common;
using System.Data;

namespace DataAccessLayer
{
    public class SearchDAL : BaseDAL
    {


        #region private global variable(s)

        private Database MyDataBase;
        ItemStock itemStock = new ItemStock();
        Item items = new Item();
        Contractor contractor = new Contractor();
        Supplier supplier = new Supplier();

        #endregion

        #region Constructor(s)


        /// <summary>
        /// Initializes a new instance of the <see cref="MetaDataDAL"/> class.
        /// </summary>
        /// <param name="dataBase">The data base.</param>
        public SearchDAL(Database dataBase)
        {
            MyDataBase = dataBase;
        }

        #endregion

        #region Search Item Stock

        #region Search ItemStock CRUD

        public List<ItemStock> SearchItemStock(int? storeId,String itemname, String itemspec, int? itemunitid)
        {
            List<ItemStock> lstItemStock = new List<ItemStock>();

            DbCommand dbCommand = MyDataBase.GetStoredProcCommand(DBConstants.SEARCH_ITEMSTOCK);
            MyDataBase.AddInParameter(dbCommand, "@StoreId", DbType.String, storeId);
            MyDataBase.AddInParameter(dbCommand, "@ItemName", DbType.String, itemname);
            MyDataBase.AddInParameter(dbCommand, "@ItemSpecification", DbType.String, itemspec);
            MyDataBase.AddInParameter(dbCommand, "@ItemUnitId", DbType.Int32, itemunitid);
            using (IDataReader reader = MyDataBase.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    itemStock = GenerateitemStockDetailFromDataReader(reader);
                    lstItemStock.Add(itemStock);
                }
            }
            return lstItemStock;
        }

        //Sarch by Id
        public List<ItemStock> SearchItemStockById(String ItemStockIds)
        {
            List<ItemStock> lstItemStock = new List<ItemStock>();

            DbCommand dbCommand = MyDataBase.GetStoredProcCommand(DBConstants.SEARCH_ITEMSTOCK_BYSTOCKID);
            MyDataBase.AddInParameter(dbCommand, "@itamStockId", DbType.String, ItemStockIds);

            using (IDataReader reader = MyDataBase.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    itemStock = GenerateitemStockDetailFromDataReader(reader);
                    lstItemStock.Add(itemStock);
                }
            }
            return lstItemStock;
        }


        #endregion

        #region Private Section

        private ItemStock GenerateitemStockDetailFromDataReader(IDataReader reader)
        {
            ItemStock itemStock = new ItemStock();
            itemStock.ItemStockId = GetIntegerFromDataReader(reader, "Item_Stock_Id");
            itemStock.ItemId = GetIntegerFromDataReader(reader, "Item_Id");
            itemStock.ItemName = GetStringFromDataReader(reader, "Item_Name");
            itemStock.ItemUnit = GetStringFromDataReader(reader, "Item_Unit");
            itemStock.ItemSpecificationName = GetStringFromDataReader(reader, "Specification");
            itemStock.QuantityOnhand = GetIntegerFromDataReader(reader, "Quantity_Onhand");
            itemStock.MinimumLevel = GetIntegerFromDataReader(reader, "Minimum_Level");
            itemStock.MaximumLevel = GetIntegerFromDataReader(reader, "Maximum_Level");
            itemStock.ReorderLevel = GetIntegerFromDataReader(reader, "Reorder_Level");
            itemStock.LeadTime = GetIntegerFromDataReader(reader, "Lead_Time");
            itemStock.CreatedBy = GetStringFromDataReader(reader, "Created_By");
            itemStock.CreatedDate = GetDateFromReader(reader, "Created_Date");
            itemStock.ModifiedBy = GetStringFromDataReader(reader, "Modified_By");
            itemStock.ModifiedDate = GetDateFromReader(reader, "Modified_Date");
            itemStock.Brand.BrandId = GetIntegerFromDataReader(reader, "Brand_Id");
            itemStock.Brand.BrandName = GetStringFromDataReader(reader, "Brand_Name");
            itemStock.Store.StoreId = GetIntegerFromDataReader(reader, "Store_Id");
            itemStock.Store.StoreName = GetStringFromDataReader(reader, "Store_Name");

            return itemStock;
        }

        #endregion

        #endregion

        #region Search Item

        #region Search Item CRUD

        public List<Item> SearchItem(String ItemName, String Specification, int? ItemUnitId, int? ItemCategoryId, String Brand, String ModelCode)
        {
            List<Item> lstItem = new List<Item>();
            DbCommand dbCommand = MyDataBase.GetStoredProcCommand(DBConstants.SEARCH_ITEM);
            MyDataBase.AddInParameter(dbCommand, "@Item_Name", DbType.String, ItemName);
            MyDataBase.AddInParameter(dbCommand, "@Specification", DbType.String, Specification);
            MyDataBase.AddInParameter(dbCommand, "@Category_Id", DbType.Int32, ItemCategoryId);
            MyDataBase.AddInParameter(dbCommand, "@Unit_Measurement_Id", DbType.Int32, ItemUnitId);
            MyDataBase.AddInParameter(dbCommand, "@Make", DbType.String, Brand);
            MyDataBase.AddInParameter(dbCommand, "@Model_Code", DbType.String, ModelCode);
            using (IDataReader reader = MyDataBase.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    items = GenerateItemDetailWithSpecificationFromDataReader(reader);
                    lstItem.Add(items);
                }
            }
            return lstItem;
        }
        public List<Item> SearchItemBySpecificationId(String SpecificationIds)
        {
            List<Item> lstItem = new List<Item>();
            DbCommand dbCommand = MyDataBase.GetStoredProcCommand(DBConstants.SEARCH_ITEM_BY_SPECIFICATION_ID);
            MyDataBase.AddInParameter(dbCommand, "@Item_Model_Mapping_Id", DbType.String, SpecificationIds);


            using (IDataReader reader = MyDataBase.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    items = GenerateItemDetailWithSpecificationFromDataReader(reader);
                    lstItem.Add(items);
                }
            }
            return lstItem;
        }
        public int ReadMaxSpecificationId()
        {
            int id = 0;
            DbCommand dbCommand = MyDataBase.GetStoredProcCommand(DBConstants.READ_MAXIMUM_SPECIFICATION_ID);
            using (IDataReader reader = MyDataBase.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    id = GetIntegerFromDataReader(reader, "Item_Model_Mapping_Id");
                }
            }
            return id;
        }
        #endregion

        #region Private Section

        private Item GenerateItemDetailWithSpecificationFromDataReader(IDataReader reader)
        {
            Item item = new Item();
            item.ItemId = GetIntegerFromDataReader(reader, "Item_Id");
            item.ItemName = GetStringFromDataReader(reader, "Item_Name");
            item.ItemDescription = GetStringFromDataReader(reader, "Item_Description");
            item.ModelSpecification = new ModelSpecification();
            item.ModelSpecification.ModelCode = GetStringFromDataReader(reader, "Model_Code");
            item.ModelSpecification.ModelSpecificationId = GetIntegerFromDataReader(reader, "Item_Model_Mapping_Id");
            item.ModelSpecification.ModelSpecificationName = GetStringFromDataReader(reader, "Specification");
            item.ModelSpecification.UnitMeasurement = new MetaData();
            item.ModelSpecification.UnitMeasurement.Id = GetIntegerFromDataReader(reader, "Unit_Measurement_Id");
            item.ModelSpecification.UnitMeasurement.Name = GetStringFromDataReader(reader, "Name");
            item.ModelSpecification.Brand.BrandName = GetStringFromDataReader(reader, "Make");
            item.ModelSpecification.Category = new ItemCategory();
            item.ModelSpecification.Category.ItemCategoryId = GetIntegerFromDataReader(reader, "Category_Id");
            item.ModelSpecification.Category.ItemCategoryName = GetStringFromDataReader(reader, "Category");
          //  item.ModelSpecification.Brand.BrandId = GetIntegerFromDataReader(reader, "Brand_Id");
            //if (item.ModelSpecification.Brand.BrandId!=0)
            //{
           
            //}
            //else
            //{
            //    item.ModelSpecification.Brand.BrandName = "No Brand";
               
            //}
            item.ModelSpecification.CategoryUsageValue = GetIntegerFromDataReader(reader, "Usage_Value");
            return item;
        }

        #endregion

        #endregion

        #region Search Contractor

        public List<Contractor> SearchContractor(Contractor contractor)
        {
            List<Contractor> lstContractor = new List<Contractor>();


            DbCommand dbCommand = MyDataBase.GetStoredProcCommand(DBConstants.SEARCH_CONTRACTOR_MASTER);
            //MyDataBase.AddInParameter(dbCommand, "@CName", DbType.String, contractor.Name);
            MyDataBase.AddInParameter(dbCommand, "@CName", DbType.String, contractor.Name);
            MyDataBase.AddInParameter(dbCommand, "@EmailId", DbType.String, contractor.Email);
            MyDataBase.AddInParameter(dbCommand, "@city", DbType.String, contractor.City);
            MyDataBase.AddInParameter(dbCommand, "@state", DbType.String, contractor.State);
            MyDataBase.AddInParameter(dbCommand, "@phone", DbType.String, contractor.Phone);
            MyDataBase.AddInParameter(dbCommand, "@mobile", DbType.String, contractor.Mobile);

            MyDataBase.AddInParameter(dbCommand, "@servicetax", DbType.String, contractor.Information.ServiceTaxNumber);
            MyDataBase.AddInParameter(dbCommand, "@pan", DbType.String, contractor.Information.PanNumber);
            MyDataBase.AddInParameter(dbCommand, "@esi", DbType.String, contractor.Information.EsiNumber);
            MyDataBase.AddInParameter(dbCommand, "@tan", DbType.String, contractor.Information.TanNumber);
            MyDataBase.AddInParameter(dbCommand, "@fax", DbType.String, contractor.Information.FaxNumber);
            MyDataBase.AddInParameter(dbCommand, "@pf", DbType.String, contractor.Information.PfNumber);

            MyDataBase.AddInParameter(dbCommand, "@website", DbType.String, contractor.Website);

            using (IDataReader reader = MyDataBase.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    contractor = GenerateContractorFromDataReader(reader);
                    lstContractor.Add(contractor);
                }
            }
            return lstContractor;
        }

        public List<Contractor> SearchContractorById(String contractorIds)
        {
            List<Contractor> lstContractor = new List<Contractor>();

            DbCommand dbCommand = MyDataBase.GetStoredProcCommand(DBConstants.SEARCH_CONTRACTOR_BYID);
            MyDataBase.AddInParameter(dbCommand, "@contractorId", DbType.String, contractorIds);

            using (IDataReader reader = MyDataBase.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    contractor = GenerateContractorFromDataReader(reader);
                    lstContractor.Add(contractor);
                }
            }
            return lstContractor;
        }

        #region Private Section

        private Contractor GenerateContractorFromDataReader(IDataReader reader)
        {
            Contractor contractor = new Contractor();
            contractor.ContractorId = GetIntegerFromDataReader(reader, "Contractor_Id");
            contractor.Name = GetStringFromDataReader(reader, "Contractor_Name");
            contractor.Address = GetStringFromDataReader(reader, "Address");
            contractor.Email = GetStringFromDataReader(reader, "Email");
            contractor.City = GetStringFromDataReader(reader, "City");
            contractor.State = GetStringFromDataReader(reader, "State");
            contractor.Phone = GetStringFromDataReader(reader, "Phone");
            contractor.Mobile = GetStringFromDataReader(reader, "Mobile");
            //contractor.Email = GetStringFromDataReader(reader, "Email");
            contractor.Website = GetStringFromDataReader(reader, "Website");

            contractor.Information.PanNumber = GetStringFromDataReader(reader, "PAN_No");
            contractor.Information.TanNumber = GetStringFromDataReader(reader, "TAN_No");
            contractor.Information.ServiceTaxNumber = GetStringFromDataReader(reader, "Service_Tax_No");
            contractor.Information.EsiNumber = GetStringFromDataReader(reader, "ESI_No");
            contractor.Information.PfNumber = GetStringFromDataReader(reader, "PF_No");
            contractor.Information.FaxNumber = GetStringFromDataReader(reader, "Fax_No");


            //contractor.CreatedBy = GetStringFromDataReader(reader, "Created_By");
            //contractor.CreatedDate = GetDateFromReader(reader, "Created_Date");
            //contractor.ModifiedBy = GetStringFromDataReader(reader, "Modified_By");
            //contractor.ModifiedDate = GetDateFromReader(reader, "Modified_Date");

            return contractor;
        }

        #endregion

        #endregion

        #region Search Supplier


        public List<Supplier> SearchSupplier(Supplier supplier)
        {
            List<Supplier> lstSupplier = new List<Supplier>();


            DbCommand dbCommand = MyDataBase.GetStoredProcCommand(DBConstants.SEARCH_SUPPLIER_MASTER);
            //MyDataBase.AddInParameter(dbCommand, "@CName", DbType.String, contractor.Name);
            MyDataBase.AddInParameter(dbCommand, "@SName", DbType.String, supplier.Name);
            MyDataBase.AddInParameter(dbCommand, "@EmailId", DbType.String, supplier.Email);
            MyDataBase.AddInParameter(dbCommand, "@city", DbType.String, supplier.City);
            MyDataBase.AddInParameter(dbCommand, "@state", DbType.String, supplier.State);
            MyDataBase.AddInParameter(dbCommand, "@phone", DbType.String, supplier.Phone);
            MyDataBase.AddInParameter(dbCommand, "@mobile", DbType.String, supplier.Mobile);

            //MyDataBase.AddInParameter(dbCommand, "@servicetax", DbType.String, supplier.Information.ServiceTaxNumber);
            MyDataBase.AddInParameter(dbCommand, "@pan", DbType.String, supplier.Information.PanNumber);
            MyDataBase.AddInParameter(dbCommand, "@esi", DbType.String, supplier.Information.EsiNumber);
            MyDataBase.AddInParameter(dbCommand, "@tan", DbType.String, supplier.Information.TanNumber);
            MyDataBase.AddInParameter(dbCommand, "@fax", DbType.String, supplier.Information.FaxNumber);
            MyDataBase.AddInParameter(dbCommand, "@pf", DbType.String, supplier.Information.PfNumber);

            MyDataBase.AddInParameter(dbCommand, "@website", DbType.String, supplier.Website);

            using (IDataReader reader = MyDataBase.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    supplier = GenerateSupplierFromDataReader(reader);
                    lstSupplier.Add(supplier);
                }
            }
            return lstSupplier;
        }
        public List<Supplier> SearchSupplierById(String supplierIds)
        {
            List<Supplier> lstSupplier = new List<Supplier>();

            DbCommand dbCommand = MyDataBase.GetStoredProcCommand(DBConstants.SEARCH_SUPPLIER_BYID);
            MyDataBase.AddInParameter(dbCommand, "@supplierId", DbType.String, supplierIds);

            using (IDataReader reader = MyDataBase.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    supplier = GenerateSupplierFromDataReader(reader);
                    lstSupplier.Add(supplier);
                }
            }
            return lstSupplier;
        }

        #region Private Section
        private Supplier GenerateSupplierFromDataReader(IDataReader reader)
        {
            Supplier supplier = new Supplier();
            supplier.SupplierId = GetIntegerFromDataReader(reader, "Supplier_Id");
            supplier.Name = GetStringFromDataReader(reader, "Supplier_Name");
            supplier.Address = GetStringFromDataReader(reader, "Supplier_Address");
            supplier.Email = GetStringFromDataReader(reader, "Email");
            supplier.City = GetStringFromDataReader(reader, "City");
            supplier.State = GetStringFromDataReader(reader, "State");
            supplier.Phone = GetStringFromDataReader(reader, "Phone");
            supplier.Mobile = GetStringFromDataReader(reader, "Mobile");
            supplier.Website = GetStringFromDataReader(reader, "Website");

            supplier.Information.PanNumber = GetStringFromDataReader(reader, "PAN_No");
            supplier.Information.TanNumber = GetStringFromDataReader(reader, "TAN_No");
            //supplier.Information.ServiceTaxNumber = GetStringFromDataReader(reader, "Service_Tax_No");
            supplier.Information.EsiNumber = GetStringFromDataReader(reader, "ESI_No");
            supplier.Information.PfNumber = GetStringFromDataReader(reader, "PF_No");
            supplier.Information.FaxNumber = GetStringFromDataReader(reader, "Fax_No");

            return supplier;
        }
        #endregion
        #endregion
    }
}
