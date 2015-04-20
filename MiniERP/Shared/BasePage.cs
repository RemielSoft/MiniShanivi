using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using DocumentObjectModel;
using BusinessAccessLayer;
using System.Text;

namespace MiniERP.Shared
{
    public class BasePage : Page
    {
        private string content;

        /// <summary>
        /// Gets the logged in user.
        /// </summary>
        /// <value>The logged in user.</value>
        public Users LoggedInUser
        {
            get
            {
                if (HttpContext.Current.Session[GlobalConstants.C_USER_SESSION] == null ||
                     CurrentUserName.ToUpper() != ((Users)HttpContext.Current.Session[GlobalConstants.C_USER_SESSION]).UserLoginId.ToUpper()
                     )
                {
                    String currentUser = this.CurrentUserName;
                    Users user = new Users();
                    try
                    {
                        user = ReadUserByLoginID(currentUser);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    HttpContext.Current.Session[GlobalConstants.C_USER_SESSION] = user;
                }
                return (Users)HttpContext.Current.Session[GlobalConstants.C_USER_SESSION];
            }
        }


        //public DateTime FormToDB(string sdate)
        //{
        //    CultureInfo objcul = (CultureInfo)CultureInfo.CurrentCulture.Clone();
        //    objcul.DateTimeFormat.ShortDatePattern = C_FORMAT_DATE;
        //    System.Threading.Thread.CurrentThread.CurrentCulture = objcul;
        //    DateTimeFormatInfo objdate = new DateTimeFormatInfo();
        //    objdate.ShortDatePattern = C_FORMAT_DATE;
        //    DateTime validatedt = Convert.ToDateTime(sdate, objdate);
        //    return validatedt;

        //}
        /// <summary>
        /// Gets the name of the current user.
        /// </summary>
        /// <value>The name of the current user.</value>
        private String CurrentUserName
        {
            get
            {
                String currentUser = Page.User.Identity.Name;
                if (currentUser.IndexOf('\\') > -1)
                {
                    currentUser = currentUser.Substring(currentUser.IndexOf('\\') + 1, currentUser.Length - currentUser.IndexOf('\\') - 1);
                }
                return currentUser;
            }
        }

        /// <summary>
        /// Gets or sets the user detail.
        /// </summary>
        /// <value>The employee detail.</value>
        //public Employee EmployeeDetail
        //{
        //    get { return (Employee)Session[GlobalConstants.C_EMPLOYEE_DETAIL]; }
        //    set
        //    {
        //        Session[GlobalConstants.C_EMPLOYEE_DETAIL] = value;
        //    }
        //}


        ///// <summary>
        ///// Dowloads the file.
        ///// </summary>
        ///// <param name="fileContent">Content of the file.</param>
        ///// <param name="fileName">Name of the file.</param>
        ///// <param name="fileExtension">The file extension.</param>
        //public void DowloadFile(byte[] fileContent, String fileName, String fileExtension)
        //{
        //    switch (fileExtension)
        //    {
        //        case "doc":
        //        case "docx":

        //            Response.ContentType = "application/msword";
        //            break;
        //        case "xls":
        //        case "Xlsx":
        //            Response.ContentType = "application/vnd.ms-excel";
        //            break;
        //        case "ppt":
        //        case "pptx":
        //            Response.ContentType = "application/vnd.ms-powerpoint";
        //            break;
        //        case "pdf":
        //            Response.ContentType = "application/pdf";
        //            break;
        //        case "gif":
        //            Response.ContentType = "image/gif";
        //            break;
        //        case "jpg":
        //        case "jpeg":
        //            Response.ContentType = "image/jpeg";
        //            break;
        //        case "ico":
        //            Response.ContentType = "image/vnd.microsoft.icon";
        //            break;
        //        case "zip":
        //            Response.ContentType = "application/zip";
        //            break;
        //    }

        //    Response.AddHeader("Content-Disposition", "Attachment;filename=" + fileName);

        //    Response.BinaryWrite(fileContent);

        //}

        //public QuotationDOM Quotation
        //{
        //    get
        //    {
        //        return (QuotationDOM)HttpContext.Current.Session[GlobalConstants.S_Quotation];
        //    }
        //    set
        //    {
        //        HttpContext.Current.Session[GlobalConstants.S_Quotation] = value;
        //    }
        //}

        /// <summary>
        /// Gets or sets the Document Stack Id[Manveer]
        /// </summary>
        public int DocumentStackId
        {
            get
            {
                try
                {
                    return (int)HttpContext.Current.Session[GlobalConstants.S_Document_Stack_Id];
                }
                catch
                {
                    return 0;
                }

            }
            set
            {
                HttpContext.Current.Session[GlobalConstants.S_Document_Stack_Id] = value;
            }
        }

        /// <summary>
        /// Gets or sets the Document Serial[Manveer]
        /// </summary>
        public int DocumentSerial
        {
            get
            {
                try
                {
                    return (int)HttpContext.Current.Session[GlobalConstants.S_Document_Serial];
                }
                catch
                {
                    return 0;
                }

            }
            set
            {
                HttpContext.Current.Session[GlobalConstants.S_Document_Serial] = value;
            }
        }

        /// <summary>
        /// Gets or sets the Documents List[Manveer]
        /// </summary>
        public List<Document> DocumentsList
        {
            get
            {
                return (List<Document>)HttpContext.Current.Session[GlobalConstants.S_Documents];
            }
            set
            {
                HttpContext.Current.Session[GlobalConstants.S_Documents] = value;
            }
        }

        /// <summary>
        /// Gets or Sets the ItemTransactionList[Manveer]
        /// </summary>
        public List<ItemTransaction> ItemTransactionList
        {
            get
            {
                return (List<ItemTransaction>)HttpContext.Current.Session[GlobalConstants.S_ItemTransaction_List];
            }
            set
            {
                HttpContext.Current.Session[GlobalConstants.S_ItemTransaction_List] = value;
            }
        }

        /// <summary>
        /// Gets or Sets DeliveryScheduleList [Manveer]
        /// </summary>
        public List<DeliveryScheduleDOM> DeliveryScheduleList
        {
            get
            {
                return (List<DeliveryScheduleDOM>)HttpContext.Current.Session[GlobalConstants.S_DeliverySchedule];
            }
            set
            {
                HttpContext.Current.Session[GlobalConstants.S_DeliverySchedule] = value;
            }
        }

        /// <summary>
        /// Gets or Sets PaymentTermsList [Manveer]
        /// </summary>
        public List<PaymentTerm> PaymentTermsList
        {
            get
            {
                return (List<PaymentTerm>)HttpContext.Current.Session[GlobalConstants.S_PaymentTerm];
            }
            set
            {
                HttpContext.Current.Session[GlobalConstants.S_PaymentTerm] = value;
            }
        }

        /// <summary>
        /// Gets or sets TermConditionList [Manveer]
        /// </summary>
        public List<TermAndCondition> TermConditionList
        {
            get
            {
                return (List<TermAndCondition>)HttpContext.Current.Session[GlobalConstants.S_TermCondition];
            }
            set
            {
                HttpContext.Current.Session[GlobalConstants.S_TermCondition] = value;
            }
        }
        /// <summary>
        /// Gets or sets the requested page name [Manveer]
        /// </summary>
        public String Page_Name
        {
            get
            {
                try
                {
                    return (String)HttpContext.Current.Session[GlobalConstants.S_Page_Name];
                }
                catch
                {
                    return null;
                }
            }
            set
            {
                HttpContext.Current.Session[GlobalConstants.S_Page_Name] = value;
            }
        }

        /// <summary>
        /// Track the Active Tab [Manveer]
        /// </summary>
        public short ActiveTab
        {
            get
            {
                try
                {
                    return (short)HttpContext.Current.Session[GlobalConstants.S_Active_Tab];
                }
                catch
                {
                    return -1;
                }
            }
            set
            {
                HttpContext.Current.Session[GlobalConstants.S_Active_Tab] = value;
            }
        }

        public Int32 QuotationStatusID
        {
            get
            {
                try
                {
                    return (Int32)Session["Status"];
                }
                catch 
                {

                    return 1;
                }
            }
            set
            {
                Session["Status"] = value;
            }
        }

        /// <summary>
        /// Shows the message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void ShowMessage(string message)
        {
            String cleanMessage = message.Replace("'", "\\'");
            string script = "<script type=\"text/javascript\">alert('" + cleanMessage + "');</script>";
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", script);
        }

        /// <summary>
        /// Message Box for Controls[Manveer,14.03.2013]
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="ctl"></param>
        public void Alert(string msg, Control ctl)
        {
            ScriptManager.RegisterStartupScript(ctl, Type.GetType("System.String"), "myscript", "alert('" + msg + "');", true);
        }

        /// <summary>
        /// Message Box for particular Page.
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="ctl"></param>
        /// <param name="pageName"></param>
        public void Alert(string msg, Control ctl, string pageName)
        {
            ScriptManager.RegisterStartupScript(ctl, Type.GetType("System.String"), "myscript", "alert('" + msg + "');window.location ='" + pageName + "'", true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="ctl"></param>
        /// <param name="res"></param>
        bool track = false;
        public bool Confirm(string msg, Control ctl)
        {
            if (!track)
            {
                ScriptManager.RegisterStartupScript(ctl, Type.GetType("System.String"), "myscript", "var res = confirm('" + msg + "')", true);
                track = true;
            }
            else
            {
                track = false;
            }
            return track;
        }

        public void ShowMessageWithUpdatePanel(String message)
        {
            String cleanMessage = message.Replace("'", "\\'");
            String script = "alert('" + cleanMessage + "');";
            ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "alert", script, true);

        }
        public void OpenPopup(string url, string title)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "openwin", "window.open('" + url + "','" + title + "','resizable=0,location=0,status=0,scrollbars=1,menubar=0,toolbar=0,top=10,left=40,width=640,height=380');", true);
        }

        public void OpenPopupWithUpdatePanel(Control control, string url, string title)
        {
            ScriptManager.RegisterStartupScript(control, this.GetType(), "openwin", "window.open('" + url + "','" + title + "','resizable=0,location=0,status=0,scrollbars=1,menubar=0,toolbar=0,top=10,left=40,width=640,height=380');", true);
        }

        public void OpenPopupWithUpdatePanelForReport(Control control, string url, string title)
        {
            ScriptManager.RegisterStartupScript(control, this.GetType(), "openwin", "window.open('" + url + "','" + title + "','resizable=0,location=0,status=0,scrollbars=1,menubar=0,toolbar=0,top=10,left=100,width=850,height=650');", true);
        }

        public void OpenPopupWithUpdatePanelForFileDownload(Control control, string url, string title)
        {
            ScriptManager.RegisterStartupScript(control, this.GetType(), "openwin", "window.open('" + url + "','" + title + "','resizable=0,location=0,status=0,scrollbars=1,menubar=0,toolbar=0,top=10,left=40,width=450,height=50');", true);
        }

        public void CloseWinWithUpdatePanel(Control control)
        {
            ScriptManager.RegisterStartupScript(control, this.GetType(), "closewin", "self.close();", true);
        }

        public void CloseWin()
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "closewin", "self.close();", true);
        }

        public void ClosePopUpwindow()
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "closewin", "this.focus(); self.opener = this; self.close();", true);

        }
        public void ClosePopUpWithRefereshParent()
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "close", "<script language=javascript>window.opener.location.reload(true);self.close();</script>");
        }
        /// <summary>
        /// Shows the message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="pageName">Name of the page.</param>
        public void ShowMessage(string message, string pageName)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Success", string.Format("alert('{0}');window.location =' {1}'", message, pageName), true);

        }
        public void RedirectToParentpage()
        {
            //if (EnableSharepoint())
            //{

            //    Response.Redirect("/TrainingNeeds/Pages/LineManager.aspx");
            //}
            //else
            //{
            //    Response.Redirect("/TrainingNeedIdentification/ApproveRP.aspx");
            //}

        }
        /// <summary> 
        /// Gets the extension of the uploaded file 
        /// </summary> 
        /// <param name="Filename"></param> 
        /// <returns></returns> 
        /// <remarks></remarks> 
        public string GetFileExtension(string Filename)
        {
            string[] arr = null;
            string extension = null;
            arr = Filename.Split('.');
            extension = arr[arr.Length - 1];

            return extension;
        }

        /// <summary>
        /// Download file
        /// </summary>
        public void DowloadFile(byte[] fileContent, String fileName, String fileExtension)
        {
            if (fileExtension != string.Empty)
            {
                switch (fileExtension)
                {
                    case "doc":
                    case "docx":
                        Response.ContentType = "application/msword";
                        break;
                    case "xls":
                    case "Xlsx":
                        Response.ContentType = "application/vnd.ms-excel";
                        break;
                    case "ppt":
                    case "pptx":
                        Response.ContentType = "application/vnd.ms-powerpoint";
                        break;
                    case "pdf":
                        Response.ContentType = "application/pdf";
                        break;
                    case "gif":
                        Response.ContentType = "image/gif";
                        break;
                    case "jpg":
                    case "jpeg":
                        Response.ContentType = "image/jpeg";
                        break;
                    case "ico":
                        Response.ContentType = "image/vnd.microsoft.icon";
                        break;
                    case "tiff":
                    case "tif":
                        Response.ContentType = "image/tiff";
                        break;
                    case "zip":
                        Response.ContentType = "application/zip";
                        break;
                }
            }
            else
            {
                Response.ContentType = "application/msword";

            }
            Response.AddHeader("Content-Disposition", "Attachment;filename=" + fileName + "." + fileExtension);

            //Response.BinaryWrite(fileContent);
            Response.BinaryWrite(fileContent);

        }

        /// <summary>
        /// Binds the drop down.
        /// </summary>
        /// <param name="ddl">The DDL.</param>
        /// <param name="dataTextField">The data text field.</param>
        /// <param name="dataValueField">The data value field.</param>
        /// <param name="dataSource">The data source.</param>
        public void BindDropDown(DropDownList ddl, string dataTextField, string dataValueField, object dataSource)
        {
            ddl.Items.Clear();

            ddl.DataSource = dataSource;
            ddl.DataTextField = dataTextField;
            ddl.DataValueField = dataValueField;
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("--Select--", "0"));
            ddl.SelectedValue = "0";
        }

        public void BindDropDownData(DropDownList ddl, string dataTextField, string dataValueField, object dataSource)
        {

            ddl.DataSource = dataSource;
            ddl.DataTextField = dataTextField;
            ddl.DataValueField = dataValueField;
            ddl.DataBind();

        }

        public void BindEmptyDropDown(DropDownList ddl)
        {
            ddl.DataSource = new List<Object>();
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("--Select--", "0"));
            //ddl.SelectedValue = "0";
        }

        public bool ValidateMail()
        {
            if (ConfigurationManager.AppSettings["MailHost"] == null || ConfigurationManager.AppSettings["MailPort"] == null || ConfigurationManager.AppSettings["MailUserID"] == null || ConfigurationManager.AppSettings["MailPWD"] == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }


        /// <summary>
        /// Empties the text.
        /// </summary>
        /// <param name="grd">The GRD.</param>
        public void GridViewEmptyText(GridView grd)
        {
            grd.EmptyDataText = GlobalConstants.EMPTY_TEXT;
        }

        //public bool IsCurrentModuleAdmin(string group)
        //{
        //    var groupName = LoggedInEmployee.Groups.Where<Group>(g => g.Name == group).Select(s => s).ToList<Group>();
        //    if (groupName.Count > 0)
        //    {
        //        if (!string.IsNullOrEmpty(groupName[0].Name))
        //            return true;
        //        else
        //            return false;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}

        public string ArrayToString(Array list)
        {
            string name = string.Empty;
            if (list != null)
            {
                foreach (string str in list)
                {
                    name = name + ", " + str;
                }
            }
            if (name.Length > 0)
                return name.Substring(1, name.Length - 1);
            else
                return string.Empty;
        }



        public String RemoveWhiteSpace(String str)
        {
            return System.Text.RegularExpressions.Regex.Replace(str, @"\s+", " ");
        }

        public bool ValidateDate(DateTime date)
        {
            TimeSpan ts = new TimeSpan();

            ts = date.Subtract(DateTime.Now);
            if (ts.Days > -1)
            {
                return true;

            }
            else
            {
                return false;
            }
        }


        #region Private Helper Methods

        /// <summary>
        /// Gets the employee by login id.
        /// </summary>
        /// <param name="loginId">The login id.</param>
        /// <returns></returns>
        private Users ReadUserByLoginID(String loginId)
        {
            UserBL userBL = new UserBL();
            Users user = userBL.ReadUserByLoginID(loginId);
            return user;
        }

        public void Popup(string url)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "openwin", "window.showModalDialog('" + url + "', window.self, dialogwidth:800px; dialogheight:380px; resizable=no; scrollbars=yes;)");
        }


        #endregion

    }
}