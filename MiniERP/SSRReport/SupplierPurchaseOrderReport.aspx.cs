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
    public partial class SupplierPurchaseOrder : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string CPOid = Request.QueryString["id"];
                string CPONo = Request.QueryString["quotationNo"];
                string CPOtype = (Convert.ToInt16(QuotationType.Supplier)).ToString();
                ReportParameter[] repParam = new ReportParameter[3];
                repParam[0] = new ReportParameter("in_Quotation_Id", CPOid);
                 repParam[1] = new ReportParameter("in_Quotation_Type", CPOtype);
                repParam[2] = new ReportParameter("in_Supplier_Purchase_Order_Number", CPONo);
                rptSPO.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
                rptSPO.ServerReport.ReportServerUrl = new Uri(ConfigurationManager.AppSettings["ReportServrPath"].ToString());
               rptSPO.ServerReport.ReportServerCredentials = new ReportCredentials(ConfigurationManager.AppSettings["ReportServerUserName"].ToString(), ConfigurationManager.AppSettings["ReportServerPassword"].ToString(), ConfigurationManager.AppSettings["ReportServerDomainName"].ToString());
                rptSPO.ServerReport.ReportPath = ConfigurationManager.AppSettings["ReportFolderName"].ToString() + "SupplierQuot";
                rptSPO.ServerReport.SetParameters(repParam);
                rptSPO.ServerReport.Refresh();
            }

        }
    }
}