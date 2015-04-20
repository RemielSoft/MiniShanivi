using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MiniERP.PurchaseOrder
{
    public partial class SupplierPurchaseOrder : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            BindDropDown();
        }

        private void BindDropDown()
        {

        }

        protected void btnGeneratePurchaseOrder_Click(object sender, EventArgs e)
        {
           Response.Redirect(@"~\pdf\PurchaseOrder.pdf");
            
            //Response.Redirect(redirectURL, "_blank", "menubar=0,scrollbars=1,width=780,height=900,top=10");
        }
    }
}