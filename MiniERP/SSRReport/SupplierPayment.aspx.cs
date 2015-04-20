using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using MiniERP.Shared;
using Microsoft.Reporting.WebForms;


namespace MiniERP.SSRReport
{
    public partial class SupplierPayment : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string PaymentId = Request.QueryString["paymentId"];
                ReportParameter[] repParam = new ReportParameter[1];
                repParam[0] = new ReportParameter("in_Payment_Id", PaymentId);
                rptSupplierPayment.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
                rptSupplierPayment.ServerReport.ReportServerUrl = new Uri(ConfigurationManager.AppSettings["ReportServrPath"].ToString());
                rptSupplierPayment.ServerReport.ReportPath = ConfigurationManager.AppSettings["ReportFolderName"].ToString() + "SupplierPayment";
                rptSupplierPayment.ServerReport.SetParameters(repParam);
                rptSupplierPayment.ServerReport.Refresh();
            }     
        }
    }
}