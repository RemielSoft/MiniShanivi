using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using Microsoft.Reporting.WebForms;

namespace MiniERP.SSRReport
{
    public partial class MaterialConsumptionNote : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string MCNId = Request.QueryString["MatCNId"];
                ReportParameter[] repParam = new ReportParameter[6];
                repParam[0] = new ReportParameter("in_Material_Consumption_Id",MCNId);
                repParam[1] = new ReportParameter("in_Contractor_Id");
                repParam[2] = new ReportParameter("in_ToDate");
                repParam[3] = new ReportParameter("in_FromDate");
                repParam[4] = new ReportParameter("in_Contract_No");
                repParam[5] = new ReportParameter("in_Material_Consumption_No");
                rptMCN.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
                rptMCN.ServerReport.ReportServerCredentials = new ReportCredentials(ConfigurationManager.AppSettings["ReportServerUserName"].ToString(), ConfigurationManager.AppSettings["ReportServerPassword"].ToString(), ConfigurationManager.AppSettings["ReportServerDomainName"].ToString());
                rptMCN.ServerReport.ReportServerUrl = new Uri(ConfigurationManager.AppSettings["ReportServrPath"].ToString());
                rptMCN.ServerReport.ReportPath = ConfigurationManager.AppSettings["ReportFolderName"].ToString() + "MaterialConsumption";
                rptMCN.ServerReport.SetParameters(repParam);
                rptMCN.ServerReport.Refresh();
            }
        }
    }
}