using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using System.Configuration;
using MiniERP.Shared;


namespace MiniERP.SSRReport
{
    public partial class ReturnMaterialContractorRpt : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string RMCId = Request.QueryString["RMCNId"];
                
                ReportParameter[] repParam = new ReportParameter[1];
                repParam[0] = new ReportParameter("in_Return_Material_Contractor_Id", RMCId);
                rptReturnMaterialContractorrpt.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
                rptReturnMaterialContractorrpt.ServerReport.ReportServerUrl = new Uri(ConfigurationManager.AppSettings["ReportServrPath"].ToString());
                rptReturnMaterialContractorrpt.ServerReport.ReportPath = ConfigurationManager.AppSettings["ReportFolderName"].ToString() + "ReturnMaterialContractor";
              //  rptReturnMaterialContractorrpt.ServerReport.ReportServerCredentials = new ReportCredentials(ConfigurationManager.AppSettings["ReportServerUserName"].ToString(), ConfigurationManager.AppSettings["ReportServerPassword"].ToString(), ConfigurationManager.AppSettings["ReportServerDomainName"].ToString());
                rptReturnMaterialContractorrpt.ServerReport.SetParameters(repParam);
                rptReturnMaterialContractorrpt.ServerReport.Refresh();
            }

        }
    }
}