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
    public partial class PendingContractorPaymentRpt : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            MetaDataBL metaDataBL = new MetaDataBL();
           
           
            if (!IsPostBack)
            {
                List<MetaData> lst = new List<MetaData>();
                lst = metaDataBL.ReadMetadataApprovalStatus();
                ddlStatusType.DataSource = lst.Where(x=>x.Id==(int)StatusType.Approved || x.Id==(int)StatusType.Pending);
                ddlStatusType.DataValueField = "Id";
                ddlStatusType.DataTextField = "Name";
                ddlStatusType.DataBind();
                ddlStatusType.Items.Insert(0, new ListItem("--Select--","0"));
                ddlStatusType.SelectedValue = "0";
                //ddlStatusType.Items.Insert(1, new ListItem("All", "1,3,4"));
                //string IDVNid = Request.QueryString["QuotationType"];
                //ReportParameter[] repParam = new ReportParameter[5];
                //repParam[0] = new ReportParameter("in_ContractorName");
                //repParam[1] = new ReportParameter("in_ToDate");
                //repParam[2] = new ReportParameter("in_FromDate");
                //repParam[3] = new ReportParameter("in_QuotationNo");
                //repParam[4] = new ReportParameter("in_Status_Type");
                //rptPendingContractorPayment.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
                //rptPendingContractorPayment.ServerReport.ReportServerCredentials = new ReportCredentials(ConfigurationManager.AppSettings["ReportServerUserName"].ToString(), ConfigurationManager.AppSettings["ReportServerPassword"].ToString(), ConfigurationManager.AppSettings["ReportServerDomainName"].ToString());

                //rptPendingContractorPayment.ServerReport.ReportServerUrl = new Uri(ConfigurationManager.AppSettings["ReportServrPath"].ToString());
                //rptPendingContractorPayment.ServerReport.ReportPath = "/MiniERPReports/ContractorPaymentPending";
                //rptPendingContractorPayment.ServerReport.SetParameters(repParam);
                //rptPendingContractorPayment.ServerReport.Refresh();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            //string IDVNid = Request.QueryString["QuotationType"];
            ReportParameter[] repParam = new ReportParameter[5];
            repParam[0] = new ReportParameter("in_ContractorName", txtContractorName.Text == "" ? null : txtContractorName.Text);
            repParam[1] = new ReportParameter("in_ToDate",txtToDate.Text==""?null:txtToDate.Text);
            repParam[2] = new ReportParameter("in_FromDate",txtFromDate.Text==""?null:txtFromDate.Text);
            repParam[3] = new ReportParameter("in_QuotationNo",txtQuotationNo.Text==""?null:txtContractorName.Text);
            repParam[4] = new ReportParameter("in_Status_Type",ddlStatusType.SelectedItem.Value); 
            rptPendingContractorPayment.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
            rptPendingContractorPayment.ServerReport.ReportServerCredentials = new ReportCredentials(ConfigurationManager.AppSettings["ReportServerUserName"].ToString(), ConfigurationManager.AppSettings["ReportServerPassword"].ToString(), ConfigurationManager.AppSettings["ReportServerDomainName"].ToString());
            rptPendingContractorPayment.ServerReport.ReportServerUrl = new Uri(ConfigurationManager.AppSettings["ReportServrPath"].ToString());
            rptPendingContractorPayment.ServerReport.ReportPath = ConfigurationManager.AppSettings["ReportFolderName"].ToString() + "ContractorPaymentPending";
            rptPendingContractorPayment.ServerReport.SetParameters(repParam);
            rptPendingContractorPayment.ServerReport.Refresh();
        }
    }
}