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
    public partial class AdvanceContractorInvoiceReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string InvoiceId = Request.QueryString["InvoiceId"];
                ReportParameter[] repParam = new ReportParameter[1];
                repParam[0] = new ReportParameter("in_Contractor_Invoice_Id", InvoiceId);
                rptContractorInvoice.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
                rptContractorInvoice.ServerReport.ReportServerUrl = new Uri(ConfigurationManager.AppSettings["ReportServrPath"].ToString());
                rptContractorInvoice.ServerReport.ReportPath = ConfigurationManager.AppSettings["ReportFolderName"].ToString() + "AdvanceContractorInvoice";
                rptContractorInvoice.ServerReport.SetParameters(repParam);
                rptContractorInvoice.ServerReport.Refresh();
            }
        }
    }
}