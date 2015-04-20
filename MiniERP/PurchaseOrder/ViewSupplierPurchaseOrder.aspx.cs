using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MiniERP.PurchaseOrder
{
    public partial class ViewSupplierPurchaseOrder : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //TextFromDate1.Text = DateTime.Now.AddYears(-2).ToString("dd-MMM-yyyy");
            //TextToDate1.Text = DateTime.Now.ToString("dd-MMM-yyyy");
            ViewContractororder.PageType = "Supplier";
        }
    }
}