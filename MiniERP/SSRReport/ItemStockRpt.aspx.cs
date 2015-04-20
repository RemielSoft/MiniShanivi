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
    public partial class ItemStockRpt : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            //string IDVNid = Request.QueryString["QuotationType"];
            ReportParameter[] repParam = new ReportParameter[1];
            repParam[0] = new ReportParameter("Item_Name", txtItemName.Text == "" ? null : txtItemName.Text);
            rptItemStock.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
         //   rptItemStock.ServerReport.ReportServerCredentials = new ReportCredentials(ConfigurationManager.AppSettings["ReportServerUserName"].ToString(), ConfigurationManager.AppSettings["ReportServerPassword"].ToString(), ConfigurationManager.AppSettings["ReportServerDomainName"].ToString());
            rptItemStock.ServerReport.ReportServerUrl = new Uri(ConfigurationManager.AppSettings["ReportServrPath"].ToString());
            rptItemStock.ServerReport.ReportPath = ConfigurationManager.AppSettings["ReportFolderName"].ToString() + "ItemStockRpt";
            rptItemStock.ServerReport.SetParameters(repParam);
            rptItemStock.ServerReport.Refresh();
        }
    }
}