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
    public partial class RateCardReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //string ItemId = Request.QueryString["IssueMaterialId"];
                ReportParameter[] repParam = new ReportParameter[4];
                repParam[0] = new ReportParameter("in_ItemName");
                repParam[1] = new ReportParameter("in_Specification");
                repParam[2] = new ReportParameter("in_ToDate");
                repParam[3] = new ReportParameter("in_FromDate");
                rptRateCardReport.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
                rptRateCardReport.ServerReport.ReportServerUrl = new Uri(ConfigurationManager.AppSettings["ReportServrPath"].ToString());
                rptRateCardReport.ServerReport.ReportPath = ConfigurationManager.AppSettings["ReportFolderName"].ToString() + "RateCardReport";
                rptRateCardReport.ServerReport.SetParameters(repParam);
                rptRateCardReport.ServerReport.Refresh();
            }
        }
    }
}