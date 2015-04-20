using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using Microsoft.Reporting.WebForms;
using DocumentObjectModel;
using BusinessAccessLayer;

namespace MiniERP.SSRReport
{
    public partial class PendingSupplierPaymentRpt : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            MetaDataBL metaDataBL = new MetaDataBL();
            if (!IsPostBack)
            {
                List<MetaData> lst = new List<MetaData>();
                lst = metaDataBL.ReadMetadataApprovalStatus();
                ddlStatusType.DataSource = lst.Where(x => x.Id == (int)StatusType.Approved || x.Id == (int)StatusType.Pending);
                ddlStatusType.DataValueField = "Id";
                ddlStatusType.DataTextField = "Name";
                ddlStatusType.DataBind();
                ddlStatusType.Items.Insert(0, new ListItem("--Select--", "0"));
                ddlStatusType.SelectedValue = "0";
                //string IDVNid = Request.QueryString["QuotationType"];
                //ReportParameter[] repParam = new ReportParameter[5];
                //repParam[0] = new ReportParameter("in_SupplierName");
                //repParam[1] = new ReportParameter("in_ToDate");
                //repParam[2] = new ReportParameter("in_FromDate");
                //repParam[3] = new ReportParameter("in_QuotationNo");
                //repParam[4] = new ReportParameter("in_Status_Type");
                //rptPendingSupplierPayment.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
                //rptPendingSupplierPayment.ServerReport.ReportServerCredentials = new ReportCredentials(ConfigurationManager.AppSettings["ReportServerUserName"].ToString(), ConfigurationManager.AppSettings["ReportServerPassword"].ToString(), ConfigurationManager.AppSettings["ReportServerDomainName"].ToString());

                //rptPendingSupplierPayment.ServerReport.ReportServerUrl = new Uri(ConfigurationManager.AppSettings["ReportServrPath"].ToString());
                //rptPendingSupplierPayment.ServerReport.ReportPath = "/MiniERPReports/SupplierPaymentPending";
                //rptPendingSupplierPayment.ServerReport.SetParameters(repParam);
                //rptPendingSupplierPayment.ServerReport.Refresh();
            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            //string IDVNid = Request.QueryString["QuotationType"];
            ReportParameter[] repParam = new ReportParameter[5];
            repParam[0] = new ReportParameter("in_SupplierName", txtContractorName.Text == "" ? null : txtContractorName.Text);
            repParam[1] = new ReportParameter("in_ToDate", txtToDate.Text == "" ? null : txtToDate.Text);
            repParam[2] = new ReportParameter("in_FromDate", txtFromDate.Text == "" ? null : txtFromDate.Text);
            repParam[3] = new ReportParameter("in_QuotationNo", txtQuotationNo.Text == "" ? null : txtContractorName.Text);
            repParam[4] = new ReportParameter("in_Status_Type", ddlStatusType.SelectedItem.Value);
            rptPendingSupplierPayment.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
            rptPendingSupplierPayment.ServerReport.ReportServerCredentials = new ReportCredentials(ConfigurationManager.AppSettings["ReportServerUserName"].ToString(), ConfigurationManager.AppSettings["ReportServerPassword"].ToString(), ConfigurationManager.AppSettings["ReportServerDomainName"].ToString());
            rptPendingSupplierPayment.ServerReport.ReportServerUrl = new Uri(ConfigurationManager.AppSettings["ReportServrPath"].ToString());
            rptPendingSupplierPayment.ServerReport.ReportPath = ConfigurationManager.AppSettings["ReportFolderName"].ToString() + "SupplierPaymentPending";
            rptPendingSupplierPayment.ServerReport.SetParameters(repParam);
            rptPendingSupplierPayment.ServerReport.Refresh();
        }
    }
}