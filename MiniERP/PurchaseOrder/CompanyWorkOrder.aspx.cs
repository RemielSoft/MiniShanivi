using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MiniERP.PurchaseOrder
{
    public partial class CompanyWorkOrder : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            TextStartDate1.Text = DateTime.Now.AddYears(-2).ToString("dd-MMM-yyyy");
            TextEndDate1.Text = DateTime.Now.ToString("dd-MMM-yyyy");
            TextWorkOrderDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
            
            
            
        }
    }
}