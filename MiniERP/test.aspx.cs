using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DocumentObjectModel;
using BusinessAccessLayer;
using System.Configuration;
using MiniERP.Shared;

namespace MiniERP
{
    public partial class test : BasePage
    {
        List<ServiceDetailDOM> lstServiceDetail = null;
        CompanyWorkOrderBL companyWorkOrderBL = null;

        MetaData meta = null;
        List<MetaData> lstmeta = null;


        protected void Page_Load(object sender, EventArgs e)
        {
            //lstServiceDetail = new List<ServiceDetailDOM>();
            //companyWorkOrderBL = new CompanyWorkOrderBL();
            // = companyWorkOrderBL.ReadCompanyWorkOrderServiceDetail(48);
            

            //lstmeta = lstMeatData();

        }

        protected void abc(object sender, EventArgs e)
        {

        }


    }
}