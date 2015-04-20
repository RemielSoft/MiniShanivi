using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MiniERP.Invoice
{
    public partial class ViewContractorInvoice : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            invoice.PageType = "Contractor";
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            //
        }
    }
}