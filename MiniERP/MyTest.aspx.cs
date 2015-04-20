using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.IO;

namespace MiniERP
{
    public partial class MyTest : System.Web.UI.Page
    {
        String filePath = String.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                // Get the HttpFileCollection 
                HttpFileCollection hfc = Request.Files;
                for (int i = 0; i < hfc.Count; i++)
                {
                    HttpPostedFile hpf = hfc[i];
                    if (hpf.ContentLength > 0)
                    {
                        hpf.SaveAs(Server.MapPath("Images" + "\\" +
                          Path.GetFileName(hpf.FileName)));
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);

            }

        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(FileUpld.FileName))
            {
                filePath = FileUpld.FileName;
                FileUpld.SaveAs(Server.MapPath("PaymentDocs\\"+Path.GetFileName(filePath)));
                
                //Response.Write(Server.MapPath("PaymentDocs\\" + Path.GetFileName(filePath)));
                FileUpld.Dispose();
            }
        }

        protected void FileUpld_DataBinding(object sender, EventArgs e)
        {

        }
    }
}