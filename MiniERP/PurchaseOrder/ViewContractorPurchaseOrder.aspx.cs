using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MiniERP.Shared;
using DocumentObjectModel;
using BusinessAccessLayer;
using System.Configuration;

namespace MiniERP.PurchaseOrder
{
    public partial class ViewContractorPurchaseOrder : System.Web.UI.Page
    {
         
        protected void Page_Load(object sender, EventArgs e)
        {
           // Response.Redirect("../Home.aspx?Page=Contractor");
            ViewContractororder.PageType = "Contractor";
            
        }
    }
}