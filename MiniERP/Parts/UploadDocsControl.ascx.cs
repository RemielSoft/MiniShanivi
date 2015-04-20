using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DocumentObjectModel;
using BusinessAccessLayer;
using MiniERP.Shared;
using System.IO;
using System.Data;
using System.Configuration;
using System.Net;

namespace MiniERP.Parts
{
    public partial class UploadDocsControl : System.Web.UI.UserControl
    {
        #region Private Global Variable(s)

        DocumentBL documentBL = new DocumentBL();
        BasePage base_Page = new BasePage();

        Document doc = null;
        Int32 Year = 0;
        Int32 Index = 0;
        Document document = null;

        String Head_Folder_Path = String.Empty;
        String Sub_Folder_Path = String.Empty;
        String File_Extension = String.Empty;
        String File_Path = String.Empty;

        DataSet page_Data = null;

        List<Document> lst_documents = null;
        // DirectoryInfo dirInfo = null;
        #endregion

        #region Public Method

        public void CreateDocumentMapping()
        {
            documentBL = new DocumentBL();
            lst_documents = new List<Document>();
            lst_documents = base_Page.DocumentsList;

            if (lst_documents != null)
            {
                //For Reset IsDeleted All documents realted to Document_Id
                documentBL.ResetDocumentMapping(Convert.ToInt32(base_Page.DocumentStackId));

                //For Insert/Update the Documents
                foreach (Document docs in lst_documents)
                {
                    document = new Document();
                    document.DocumentId = Convert.ToInt32(base_Page.DocumentStackId);
                    document.Original_Name = docs.Original_Name;
                    document.Replaced_Name = docs.Replaced_Name;
                    document.Path = docs.Path;
                    //base_Page.DocumentSerial is the last updated document
                    document.LastIndex = base_Page.DocumentSerial;
                    document.CreatedBy = base_Page.LoggedInUser.UserLoginId;
                    document.Id = docs.Id;
                    documentBL.CreateDocumentMapping(document);
                }
            }
        }

        public void GetDocumentData(Int32 DocumentId)
        {
            documentBL = new DocumentBL();
            lst_documents = new List<Document>();
            if (DocumentId != Int32.MinValue)
            {
                lst_documents = documentBL.ReadDocumnetMapping(DocumentId);
                if (lst_documents.Count >= 1)
                {
                    base_Page.DocumentsList = lst_documents;
                    base_Page.DocumentStackId = lst_documents[0].DocumentId;
                    base_Page.DocumentSerial = lst_documents[0].LastIndex;
                    //base_Page.Page_Name = RequestPageName;
                    BindDocument();
                }
                else
                {
                    base_Page.DocumentsList = null;
                    BindDocument();
                }
            }
            else
            {
                base_Page.DocumentsList = null;
                BindDocument();
            }
        }

        public void EmptyDocumentList()
        {
            base_Page.DocumentStackId = 0;
            base_Page.DocumentSerial = 0;
            base_Page.DocumentsList = null;
        }

        #endregion

        #region Protected Methods

        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!IsPostBack)
            //{
            //    EmptyDocumentList();
            //}
            if (base_Page.Page_Name == GlobalConstants.P_Contractor_Quotation_Approval
                || base_Page.Page_Name == GlobalConstants.P_View_Contractor_Quotation
                || base_Page.Page_Name == GlobalConstants.P_Supplier_Quotation_Approval
                || base_Page.Page_Name == GlobalConstants.P_View_Supplier_Quotation
                || base_Page.Page_Name == GlobalConstants.P_View_Contractor_Purchase_Order
                || base_Page.Page_Name == GlobalConstants.P_View_Supplier_Purchase_Order
                || base_Page.Page_Name == GlobalConstants.P_Contractor_Invoice_Approval
                || base_Page.Page_Name == GlobalConstants.P_View_Contractor_Invoice
                || base_Page.Page_Name == GlobalConstants.P_Supplier_Invoice_Approval
                || base_Page.Page_Name == GlobalConstants.P_View_Supplier_Invoice
                || base_Page.Page_Name == GlobalConstants.P_Contractor_Payment_Approval
                || base_Page.Page_Name == GlobalConstants.P_View_Contractor_Payment
                || base_Page.Page_Name == GlobalConstants.P_Supplier_Payment_Approval
                || base_Page.Page_Name == GlobalConstants.P_View_Supplier_Payment
                || base_Page.Page_Name == GlobalConstants.P_Company_Work_Order_Approval
                || base_Page.Page_Name == GlobalConstants.P_View_Company_WorkOrder
                || base_Page.Page_Name == GlobalConstants.P_View_Supplier_Receive_Material
                || base_Page.Page_Name == GlobalConstants.P_View_Demand_Issue_Voucher
                || base_Page.Page_Name == GlobalConstants.P_View_Issue_Material
                || base_Page.Page_Name == GlobalConstants.P_View_Return_Material
                || base_Page.Page_Name==GlobalConstants.P_View_Supplier_Receive_Material_CWO)
            {
                btn_upload_doc.Visible = false;
                FileUpload_Control.Visible = false;
                gv_documents.Columns[1].Visible = false;
            }
            BindDocument();
        }

        protected void btn_upload_Click(object sender, EventArgs e)
        {
            if (FileUpload_Control.HasFile)
            {
                LoadDocument();
                DirectoryHandle(FileUpload_Control);
                BindDocument();
            }

        }

        protected void gv_documents_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "FileDelete")
            {
                Index = Convert.ToInt32(e.CommandArgument);
                lst_documents = new List<Document>();
                lst_documents = base_Page.DocumentsList;
                lst_documents.RemoveAt(Index);
                base_Page.DocumentsList = lst_documents;
                BindDocument();
            }
            else if (e.CommandName == "OpenFile")
            {
                Index = Convert.ToInt32(e.CommandArgument);
                lst_documents = new List<Document>();
                lst_documents = base_Page.DocumentsList;


                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                LinkButton lbtn = (LinkButton)row.FindControl("lbtn_file");

                string fileName = lst_documents[Index].Replaced_Name;
                string strURL = lst_documents[Index].Path + @"\" + lst_documents[Index].Replaced_Name;

                Session["FilePath"] = Server.MapPath(strURL);
                Session["OriginalFileName"] = lst_documents[Index].Original_Name;
                Session["ReplacedFileName"] = lst_documents[Index].Replaced_Name;
                base_Page.OpenPopupWithUpdatePanelForFileDownload(lbtn, "../Parts/FileDownload.aspx?id=" + "File", "DownloadFile");


                // Response.Redirect(@"\" + lst_documents[Index].Path + @"\" + lst_documents[Index].Replaced_Name);
                //File_Path = lst_documents[Index].Path + @"\" + lst_documents[Index].Replaced_Name;
                //WebClient req = new WebClient();
                //HttpResponse response = HttpContext.Current.Response;
                //response.Clear();
                //response.ClearContent();
                //response.ClearHeaders();
                //response.Buffer = true;
                //response.AddHeader("Content-Disposition", "attachment;filename=" + lst_documents[Index].Replaced_Name.Replace(lst_documents[Index].Replaced_Name, lst_documents[Index].Original_Name));
                //byte[] data = req.DownloadData(Server.MapPath(File_Path));
                //response.BinaryWrite(data);
                //response.End();
            }
        }

        protected void Page_LoadComplete(object sender, EventArgs e)
        {
            BindDocument();
        }

        #endregion

        #region Private Methods

        private void ManageSession()
        {
            RequestPageName = (Request.Url.LocalPath).Split('/').Last().Split('.').First();
            if (base_Page.Page_Name == null || base_Page.Page_Name != RequestPageName)
            {
                base_Page.Page_Name = RequestPageName;
                base_Page.DocumentStackId = 0;
                base_Page.DocumentSerial = 0;
                base_Page.DocumentsList = null;
            }
            else
            {
                //GO AHEAD
            }
        }

        private void LoadDocument()
        {
            ManageSession();
            if (base_Page.DocumentStackId == 0)
            {
                CreateAndReadDocumentStackId();
            }
            BindDocument();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private Int32 CreateAndReadDocumentStackId()
        {
            doc = new Document();
            doc.CreatedBy = base_Page.LoggedInUser.UserLoginId;
            base_Page.DocumentStackId = documentBL.CreateAndReadDocumnetStackId(doc);
            return base_Page.DocumentStackId;
        }

        /// <summary>
        /// 
        /// </summary>
        private void DirectoryHandle(FileUpload fileupload)
        {
            Year = DateTime.Now.Year;

            //Get list of pages
            page_Data = new DataSet();
            page_Data.ReadXml(Server.MapPath(ConfigurationManager.AppSettings["PageDictionary_Path"].ToString()));

            Head_Folder_Path = Server.MapPath(@"\" + ConfigurationManager.AppSettings["Upload_Document_Path"].ToString() + Year.ToString());


            //Check to existance of Main Folder
            if (!Directory.Exists(Head_Folder_Path))
            {
                Directory.CreateDirectory(Head_Folder_Path);
            }

            //For Check to existance of Sub-Folders and if not, then create
            foreach (DataRow dr in page_Data.Tables[0].Rows)
            {
                Sub_Folder_Path = Head_Folder_Path + @"\" + dr["Page"].ToString();
                if (!Directory.Exists(Sub_Folder_Path))
                {
                    Directory.CreateDirectory(Sub_Folder_Path);
                }
            }


            //If folder exist then Upload Document in respective folder
            Sub_Folder_Path = Head_Folder_Path + @"\" + RequestPageName;

            if (Directory.Exists(Sub_Folder_Path))
            {
                if (base_Page.DocumentStackId != 0)
                {
                    doc = new Document();
                    lst_documents = new List<Document>();

                    base_Page.DocumentSerial = base_Page.DocumentSerial + 1;

                    File_Extension = System.IO.Path.GetExtension(fileupload.FileName.Split('\\').Last());
                    doc.Replaced_Name = Convert.ToString(base_Page.DocumentStackId) + "_" + Convert.ToString(base_Page.DocumentSerial) + File_Extension;

                    File_Path = Sub_Folder_Path + @"\" + doc.Replaced_Name;

                    //Upload file in respective path
                    FileUpload_Control.SaveAs(File_Path);

                    doc.DocumentId = base_Page.DocumentStackId;
                    doc.Original_Name = fileupload.FileName.Split('\\').Last();
                    doc.Path = ConfigurationManager.AppSettings["Upload_Document_Path"].ToString() + Year.ToString() + @"\" + RequestPageName;
                    doc.LastIndex = base_Page.DocumentSerial;

                    if (base_Page.DocumentsList == null)
                    {
                        lst_documents.Add(doc);
                    }
                    else
                    {
                        lst_documents = base_Page.DocumentsList;
                        lst_documents.Add(doc);
                    }

                    //Add Doc's info in list
                    base_Page.DocumentsList = lst_documents;
                }
            }

        }

        public void BindDocument()
        {
            if (base_Page.DocumentsList != null)
            {
                gv_documents.DataSource = base_Page.DocumentsList;
            }
            else
            {
                gv_documents.DataSource = null;
            }
            gv_documents.DataBind();
        }
        #endregion

        #region Public Properties

        public String RequestPageName
        {
            get
            {
                return (String)ViewState["Page"];
            }
            set
            {
                ViewState["Page"] = value;
            }
        }
        #endregion


    }
}