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
    public partial class IssueMaterialReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string IDVNid = Request.QueryString["IssueMaterialId"];
                ReportParameter[] repParam = new ReportParameter[1];
                repParam[0] = new ReportParameter("in_IssueMaterial_Id", IDVNid);
                rptIssueMaterial.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
                rptIssueMaterial.ServerReport.ReportServerUrl = new Uri(ConfigurationManager.AppSettings["ReportServrPath"].ToString());
                rptIssueMaterial.ServerReport.ReportPath = ConfigurationManager.AppSettings["ReportFolderName"].ToString() + "IssueMaterial";
               // rptIssueMaterial.ServerReport.ReportServerCredentials = new ReportCredentials(ConfigurationManager.AppSettings["ReportServerUserName"].ToString(), ConfigurationManager.AppSettings["ReportServerPassword"].ToString(), ConfigurationManager.AppSettings["ReportServerDomainName"].ToString());
                rptIssueMaterial.ServerReport.SetParameters(repParam);
                rptIssueMaterial.ServerReport.Refresh();
            }
        }
    }
}