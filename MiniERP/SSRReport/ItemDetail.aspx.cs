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
    public partial class ItemDetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //ReportParameter[] repParam = new ReportParameter[1];
                //repParam[0] = new ReportParameter("in_Contractor_WO");
                //rptItemDetails.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
                //rptItemDetails.ServerReport.ReportServerUrl = new Uri(ConfigurationManager.AppSettings["ReportServrPath"].ToString());
                //rptItemDetails.ServerReport.ReportPath = ConfigurationManager.AppSettings["ReportFolderName"].ToString() + "ItemDetailsAgainstWO";
                //rptItemDetails.ServerReport.ReportServerCredentials = new ReportCredentials(ConfigurationManager.AppSettings["ReportServerUserName"].ToString(), ConfigurationManager.AppSettings["ReportServerPassword"].ToString(), ConfigurationManager.AppSettings["ReportServerDomainName"].ToString());
                //rptItemDetails.ServerReport.SetParameters(repParam);
                //rptItemDetails.ServerReport.Refresh();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            ReportParameter[] repParam = new ReportParameter[3];
            repParam[0] = new ReportParameter("in_Contractor_WO", txtWorkOrderNo.Text == "" ? null : txtWorkOrderNo.Text);
            repParam[1] = new ReportParameter("in_From_Date", txtFromDate.Text == "" ? null : txtFromDate.Text);
            repParam[2] = new ReportParameter("in_To_Date", txtToDate.Text == "" ? null : txtToDate.Text);
            rptItemDetails.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
            rptItemDetails.ServerReport.ReportServerUrl = new Uri(ConfigurationManager.AppSettings["ReportServrPath"].ToString());
            rptItemDetails.ServerReport.ReportPath = ConfigurationManager.AppSettings["ReportFolderName"].ToString() + "ItemDetailsAgainstWO";
            rptItemDetails.ServerReport.ReportServerCredentials = new ReportCredentials(ConfigurationManager.AppSettings["ReportServerUserName"].ToString(), ConfigurationManager.AppSettings["ReportServerPassword"].ToString(), ConfigurationManager.AppSettings["ReportServerDomainName"].ToString());
            rptItemDetails.ServerReport.SetParameters(repParam);
            rptItemDetails.ServerReport.Refresh();
        }
    }
}