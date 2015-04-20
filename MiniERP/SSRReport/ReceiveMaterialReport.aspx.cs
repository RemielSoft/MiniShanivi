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
    public partial class ReceiveMaterialReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string SRMId = Request.QueryString["SupplierReceiveMaterialId"];
                string SRMNo = Request.QueryString["SuppReceiveNo"];
                //string CPOtype = (Convert.ToInt16(QuotationType.Contractor)).ToString();
                ReportParameter[] repParam = new ReportParameter[2];
                repParam[0] = new ReportParameter("in_supplier_receive_materialId", SRMId);
                repParam[1] = new ReportParameter("in_Supplier_Recieve_Material_Number", SRMNo);
                //repParam[2] = new ReportParameter("in_Contractor_Purchase_Order_Number", CPONo);
                rptReceiveMaterial.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
                rptReceiveMaterial.ServerReport.ReportServerUrl = new Uri(ConfigurationManager.AppSettings["ReportServrPath"].ToString());
                rptReceiveMaterial.ServerReport.ReportPath = ConfigurationManager.AppSettings["ReportFolderName"].ToString() + "ReceiveMaterial";
              //  rptReceiveMaterial.ServerReport.ReportServerCredentials = new ReportCredentials(ConfigurationManager.AppSettings["ReportServerUserName"].ToString(), ConfigurationManager.AppSettings["ReportServerPassword"].ToString(), ConfigurationManager.AppSettings["ReportServerDomainName"].ToString());
                rptReceiveMaterial.ServerReport.SetParameters(repParam);
                rptReceiveMaterial.ServerReport.Refresh();
            }
        }
    }
}