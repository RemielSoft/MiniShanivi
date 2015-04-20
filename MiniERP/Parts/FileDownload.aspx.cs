using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;

namespace MiniERP.Quotation.Parts
{
    public partial class FileDownload : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && Request.QueryString["id"] != null)
            {
                FileDownLoad();
            }
        }

        private void FileDownLoad()
        {
            try
            {
                string strURL = (String)Session["FilePath"];
                WebClient req = new WebClient();
                HttpResponse response = HttpContext.Current.Response;
                response.Clear();
                response.ClearContent();
                response.ClearHeaders();
                response.Buffer = true;
                response.AddHeader("Content-Disposition", "attachment;filename=" + Session["OriginalFileName"]);
                byte[] data = req.DownloadData(strURL);
                response.BinaryWrite(data);
                response.End();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}