using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using DocumentObjectModel;
using System.Configuration;
using MiniERP.Shared;

namespace MiniERP.SSRReport
{
    public partial class ContractorPayment : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string paymentId = Request.QueryString["paymentId"];
                ReportParameter[] repParam = new ReportParameter[1];
                repParam[0] = new ReportParameter("in_Payment_Id", paymentId);
                CPReportViewer.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
                CPReportViewer.ServerReport.ReportServerUrl = new Uri(ConfigurationManager.AppSettings["ReportServrPath"].ToString());
              //  CPReportViewer.ServerReport.ReportServerCredentials = new ReportCredentials(ConfigurationManager.AppSettings["ReportServerUserName"].ToString(), ConfigurationManager.AppSettings["ReportServerPassword"].ToString(), ConfigurationManager.AppSettings["ReportServerDomainName"].ToString());
                CPReportViewer.ServerReport.ReportPath = ConfigurationManager.AppSettings["ReportFolderName"].ToString() + "ContractorPayment";
                CPReportViewer.ServerReport.SetParameters(repParam);
                CPReportViewer.ServerReport.Refresh();
            }
        }
    }
}