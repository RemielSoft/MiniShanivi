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
    public partial class ReturnMaterialRpt : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string ReturnMaterialNo = Request.QueryString["ReturnMaterialNo"];
                ReportParameter[] repParam = new ReportParameter[1];
                repParam[0] = new ReportParameter("in_Return_Material_Number", ReturnMaterialNo);
                rptReturnMaterial.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
                rptReturnMaterial.ServerReport.ReportServerUrl = new Uri(ConfigurationManager.AppSettings["ReportServrPath"].ToString());
                rptReturnMaterial.ServerReport.ReportPath = ConfigurationManager.AppSettings["ReportFolderName"].ToString() + "ReturnMaterialNote";
                rptReturnMaterial.ServerReport.SetParameters(repParam);
                rptReturnMaterial.ServerReport.Refresh();
            }   
        }
    }
}