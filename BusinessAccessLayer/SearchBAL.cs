using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using DataAccessLayer;
using DocumentObjectModel;
using System.Transactions;
using Microsoft.Practices.EnterpriseLibrary.Logging;

namespace BusinessAccessLayer
{
    public class SearchBAL:BaseBL
    {
        #region Search Item Stock

        #region private global variable(s)

        private Database myDataBase;
        private SearchDAL searchDAL = null;
     
        #endregion

        #region Constructor(s)

        /// <summary>
        /// Initializes a new instance of the <see cref="MetaDataBAL"/> class.
        /// </summary>
        public SearchBAL()
        {
            myDataBase = DatabaseFactory.CreateDatabase(C_ConnectionString);
            searchDAL = new SearchDAL(myDataBase);
        }

        #endregion

        #region Search ItemStock CRUD

        public List<ItemStock> ItemStock(int? storeId, String itemname, String itemspec, int? itemunitid)
        {
            List<ItemStock> lstItemStock = new List<ItemStock>();
            try
            {
                lstItemStock = searchDAL.SearchItemStock(storeId,itemname, itemspec, itemunitid);
            }
            catch (Exception ex)
            {
                Logger.Write(ex.Message);
            }
            return lstItemStock;
        }
        public List<ItemStock> SearchItemStockById(String ItemStockIds)
        {
            List<ItemStock> lstItemStock = new List<ItemStock>(); 
            try
            {
                lstItemStock = searchDAL.SearchItemStockById(ItemStockIds);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lstItemStock;
        }

        #endregion

        #endregion

        #region Search Item

        #region Search Item CRUD

        public List<Item> SearchItem(String ItemName, String Specification, int? ItemUnitId, int? ItemCategoryId, String Brand,String ModelCode)
        {
            List<Item> lstItem = new List<Item>();
            try
            {
                lstItem = searchDAL.SearchItem(ItemName, Specification, ItemUnitId, ItemCategoryId, Brand, ModelCode);
            }
            catch (Exception ex)
            {
                Logger.Write(ex.Message);
            }
            return lstItem;
        }
        public List<Item> SearchItemBySpecificationId(String SpecificationIds)
        {
            List<Item> lstItem = new List<Item>();
            try
            {
                lstItem = searchDAL.SearchItemBySpecificationId(SpecificationIds);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lstItem;
        }
        public int ReadMaxSpecificationId()
        {
            int id = 0;
            try
            {
               id = searchDAL.ReadMaxSpecificationId();
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return id;
        }
        #endregion

        #endregion

        #region Search Contractor

        public List<Contractor> SearchContractor(Contractor contractor)
        {
            List<Contractor> lstContractor = new List<Contractor>();
            try
            {
                lstContractor = searchDAL.SearchContractor(contractor);
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return lstContractor;
        }
        public List<Contractor> SearchContractorById(String ContractorIds)
        {
            List<Contractor> lstContractor = new List<Contractor>();
            try
            {
                lstContractor = searchDAL.SearchContractorById(ContractorIds);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lstContractor;
        }
        #endregion

        #region Search Supplier
        public List<Supplier> SearchSupplier(Supplier supplier)
        {
            List<Supplier> lstSupplier = new List<Supplier>();
            try
            {
                lstSupplier = searchDAL.SearchSupplier(supplier);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lstSupplier;
        }
        public List<Supplier> SearchSupplierById(String SupplierIds)
        {
            List<Supplier> lstSupplier = new List<Supplier>();
            try
            {
                lstSupplier = searchDAL.SearchSupplierById(SupplierIds);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lstSupplier;
        }
        #endregion


        public List<DocumentObjectModel.ItemStock> SearchItemTEST(int itemStockId)
        {
            throw new NotImplementedException();
        }
    }
}
