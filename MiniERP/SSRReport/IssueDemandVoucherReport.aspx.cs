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
    public partial class IssueDemandVoucherReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string IDVNid = Request.QueryString["id"];
                String IDVNo=Request.QueryString["IDVNo"];
                ReportParameter[] repParam = new ReportParameter[2];
                repParam[0] = new ReportParameter("in_Material_Issue_Demand_Voucher_Id", IDVNid);
                repParam[1] = new ReportParameter("in_Issue_Demand_Voucher_Number", IDVNo);
                rptIssueDemandVoucher.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
                rptIssueDemandVoucher.ServerReport.ReportServerUrl = new Uri(ConfigurationManager.AppSettings["ReportServrPath"].ToString());
              //  rptIssueDemandVoucher.ServerReport.ReportServerCredentials = new ReportCredentials(ConfigurationManager.AppSettings["ReportServerUserName"].ToString(), ConfigurationManager.AppSettings["ReportServerPassword"].ToString(), ConfigurationManager.AppSettings["ReportServerDomainName"].ToString());
                rptIssueDemandVoucher.ServerReport.ReportPath = ConfigurationManager.AppSettings["ReportFolderName"].ToString() + "IssueDemandVoucher";
                rptIssueDemandVoucher.ServerReport.SetParameters(repParam);
                rptIssueDemandVoucher.ServerReport.Refresh();
            }
        }
    }
}