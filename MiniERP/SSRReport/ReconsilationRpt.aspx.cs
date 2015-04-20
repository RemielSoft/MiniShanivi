using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace MiniERP.SSRReport
{
    public partial class ReconsilationRpt : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            //string IDVNid = Request.QueryString["QuotationType"];
            ReportParameter[] repParam = new ReportParameter[1];
            repParam[0] = new ReportParameter("WorkOrderNo", txtWorkOrderNo.Text == "" ? null : txtWorkOrderNo.Text);
            rptWorkOrderNo.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
         //   rptWorkOrderNo.ServerReport.ReportServerCredentials = new ReportCredentials(ConfigurationManager.AppSettings["ReportServerUserName"].ToString(), ConfigurationManager.AppSettings["ReportServerPassword"].ToString(), ConfigurationManager.AppSettings["ReportServerDomainName"].ToString());
            rptWorkOrderNo.ServerReport.ReportServerUrl = new Uri(ConfigurationManager.AppSettings["ReportServrPath"].ToString());
            rptWorkOrderNo.ServerReport.ReportPath = ConfigurationManager.AppSettings["ReportFolderName"].ToString() + "ReconsilationReport";
            rptWorkOrderNo.ServerReport.SetParameters(repParam);
            rptWorkOrderNo.ServerReport.Refresh();
        }
    }
}