using DocumentObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessAccessLayer.Invoice;
using System.Text;
using System.IO;

namespace MiniERP.Invoice
{
    public partial class SupplierBillAmount : System.Web.UI.Page
    {
        List<InvoiceDom> lstInvoiceDom ;
        SupplierInvoiceBL supplierInvoiceBL =new SupplierInvoiceBL();

        protected void Page_Load(object sender, EventArgs e)
        {
           
            LinkBtnExport.Visible = false;
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            DateTime fromDate ; 
            DateTime toDate ;
            if (txtFrmDate.Text == "")
            {
                fromDate = DateTime.MinValue;
            }
            else
            {
                fromDate = Convert.ToDateTime(txtFrmDate.Text);
            }
            if (txttoDate.Text == "")
            {
                toDate = DateTime.MinValue;
            }
            else
            {
                toDate = Convert.ToDateTime(txttoDate.Text);
            }
           
            BindgvBilledAmount(fromDate,toDate);
        }
        private void BindgvBilledAmount(DateTime fromDate, DateTime toDate)
        {
            List<InvoiceDom> lstInvoiceDom = new List<InvoiceDom>();

            lstInvoiceDom = supplierInvoiceBL.ReadSupplierBillAmount(fromDate, toDate);
            gvBilledAmount.DataSource = lstInvoiceDom;
            gvBilledAmount.DataBind();
            Clear();
            LinkBtnExport.Visible = true;
        }

        protected void LinkBtnExport_Click(object sender, EventArgs e)
        {
            Response.Clear();
            Response.ClearHeaders();
            Response.AddHeader("content-disposition", "attachment;filename=SupplierBilledSheet.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.xls";

            StringBuilder sb = new StringBuilder();
            StringWriter stringWrite = new StringWriter(sb);
            HtmlTextWriter htm = new HtmlTextWriter(stringWrite);
            gvBilledAmount.AllowPaging = false;
            gvBilledAmount.HeaderRow.Style.Add("background-color", "#FFFFFF");
            gvBilledAmount.FooterRow.HorizontalAlign = HorizontalAlign.Right;       
            //gvSample is Gridview server control
            gvBilledAmount.RenderControl(htm);
            Response.Write(stringWrite);
            Response.End();
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
               server control at run time. */
        }
        public void Clear()
        {
            txtFrmDate.Text = string.Empty;
            txttoDate.Text = string.Empty;
        }
    }
}