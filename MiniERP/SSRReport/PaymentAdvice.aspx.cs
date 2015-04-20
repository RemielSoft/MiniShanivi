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
    public partial class PaymentAdvice : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string paymentId = Request.QueryString["paymentId"];
                ReportParameter[] repParam = new ReportParameter[1];
                repParam[0] = new ReportParameter("in_Payment_Id", paymentId);
                PAdvice.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
                PAdvice.ServerReport.ReportServerUrl = new Uri(ConfigurationManager.AppSettings["ReportServrPath"].ToString());
             //   PAdvice.ServerReport.ReportServerCredentials = new ReportCredentials(ConfigurationManager.AppSettings["ReportServerUserName"].ToString(), ConfigurationManager.AppSettings["ReportServerPassword"].ToString(), ConfigurationManager.AppSettings["ReportServerDomainName"].ToString());
                PAdvice.ServerReport.ReportPath = ConfigurationManager.AppSettings["ReportFolderName"].ToString() + "PaymentAdv";
                PAdvice.ServerReport.SetParameters(repParam);
                PAdvice.ServerReport.Refresh();
            }
        }
    }
}