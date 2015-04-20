using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using System.Configuration;

namespace MiniERP.SSRReport
{
    public partial class AdvanceSupplierInvoiceReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string InvoiceId = Request.QueryString["InvoiceId"];
                ReportParameter[] repParam = new ReportParameter[1];
                repParam[0] = new ReportParameter("in_Supplier_Invoice_Id", InvoiceId);
                rptSupplierInvoice.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
                rptSupplierInvoice.ServerReport.ReportServerUrl = new Uri(ConfigurationManager.AppSettings["ReportServrPath"].ToString());
                rptSupplierInvoice.ServerReport.ReportPath = ConfigurationManager.AppSettings["ReportFolderName"].ToString() + "AdvanceSupplierInvoice";
                rptSupplierInvoice.ServerReport.SetParameters(repParam);
                rptSupplierInvoice.ServerReport.Refresh();
            }
        }
    }
}