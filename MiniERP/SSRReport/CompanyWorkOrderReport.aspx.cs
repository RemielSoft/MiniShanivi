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
    public partial class CompanyWorkOrderReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string cwoId = Request.QueryString["workOrderId"];//
                string statusType = Request.QueryString["statusType"];
                ReportParameter[] repParam = new ReportParameter[2];
                repParam[0] = new ReportParameter("Company_Work_Order_Id", cwoId);
                repParam[1] = new ReportParameter("in_Status_Type_Id", statusType);
                //repParam[2] = new ReportParameter("in_Supplier_Purchase_Order_Number", CPONo);
                rptViewerCWO.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
                rptViewerCWO.ServerReport.ReportServerUrl = new Uri(ConfigurationManager.AppSettings["ReportServrPath"].ToString());
                //rptSPO.ServerReport.ReportServerUrl = new Uri("http://192.168.0.124/ReportServer");
                rptViewerCWO.ServerReport.ReportPath = ConfigurationManager.AppSettings["ReportFolderName"].ToString() + "CompanyWorkOrder";
             //   rptViewerCWO.ServerReport.ReportServerCredentials = new ReportCredentials(ConfigurationManager.AppSettings["ReportServerUserName"].ToString(), ConfigurationManager.AppSettings["ReportServerPassword"].ToString(), ConfigurationManager.AppSettings["ReportServerDomainName"].ToString());
                rptViewerCWO.ServerReport.SetParameters(repParam);
                rptViewerCWO.ServerReport.Refresh();
            }
        }
    }
}