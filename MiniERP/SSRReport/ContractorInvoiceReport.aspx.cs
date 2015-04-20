using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using System.Configuration;
using System.Net;

namespace MiniERP.SSRReport
{
    public partial class ContractorInvoiceReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                string InvoiceId = Request.QueryString["InvoiceId"];
                ReportParameter[] repParam = new ReportParameter[1];
                repParam[0] = new ReportParameter("in_Contractor_Invoice_Id", InvoiceId);
                rptContractorInvoice.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote; 
                rptContractorInvoice.ServerReport.ReportServerCredentials = new ReportCredentials(ConfigurationManager.AppSettings["ReportServerUserName"].ToString(), ConfigurationManager.AppSettings["ReportServerPassword"].ToString(), ConfigurationManager.AppSettings["ReportServerDomainName"].ToString());
                rptContractorInvoice.ServerReport.ReportServerUrl = new Uri(ConfigurationManager.AppSettings["ReportServrPath"].ToString());
                rptContractorInvoice.ServerReport.ReportPath = ConfigurationManager.AppSettings["ReportFolderName"].ToString() + "ContractorInvoice";
                rptContractorInvoice.ServerReport.SetParameters(repParam);
                rptContractorInvoice.ServerReport.Refresh();
            }



        }
    }


    public class ReportCredentials : IReportServerCredentials
    {
        private string _UserName;
        private string _PassWord;
        private string _DomainName;

        public ReportCredentials(string UserName, string PassWord, string DomainName)
        {
            _UserName = UserName;
            _PassWord = PassWord;
            _DomainName = DomainName;
        }

        public System.Security.Principal.WindowsIdentity ImpersonationUser
        {
            get { return null; }
        }

        public ICredentials NetworkCredentials
        {
            get { return new NetworkCredential(_UserName, _PassWord, _DomainName); }
        }

        public bool GetFormsCredentials(out Cookie authCookie, out string user,
         out string password, out string authority)
        {
            authCookie = null;
            user = password = authority = null;
            return false;
        }
    }
}