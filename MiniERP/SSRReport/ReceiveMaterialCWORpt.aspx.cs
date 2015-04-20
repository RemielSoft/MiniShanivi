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
    public partial class ReceiveMaterialCWORpt : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string RMCWOId = Request.QueryString["id"];
                ReportParameter[] repParam = new ReportParameter[1];
                repParam[0] = new ReportParameter("in_CRM_Id", RMCWOId);
                rptReceiveMaterialCWO.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
                rptReceiveMaterialCWO.ServerReport.ReportServerUrl = new Uri(ConfigurationManager.AppSettings["ReportServrPath"].ToString());
             //   rptReceiveMaterialCWO.ServerReport.ReportServerCredentials = new ReportCredentials(ConfigurationManager.AppSettings["ReportServerUserName"].ToString(), ConfigurationManager.AppSettings["ReportServerPassword"].ToString(), ConfigurationManager.AppSettings["ReportServerDomainName"].ToString());
                rptReceiveMaterialCWO.ServerReport.ReportPath = ConfigurationManager.AppSettings["ReportFolderName"].ToString() + "ReceiveMaterialCWO";
                rptReceiveMaterialCWO.ServerReport.SetParameters(repParam);
                rptReceiveMaterialCWO.ServerReport.Refresh();
            }   
        }
    }
}