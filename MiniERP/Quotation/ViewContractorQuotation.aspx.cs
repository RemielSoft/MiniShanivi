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
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace MiniERP.Quotation
{
    public partial class ViewContractorQuotation : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            quotation.PageType = "Contractor"; 
        }
    }
}