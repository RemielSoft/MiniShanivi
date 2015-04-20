using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DocumentObjectModel;
using BusinessAccessLayer;
using MiniERP.Shared;

namespace MiniERP.Quotation
{
    public partial class SupplierQuotation : BasePage
    {

        #region Protected Section

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                tbc_quotation_ActiveTabChanged(null, null);
            }
        }


        protected void tbc_quotation_ActiveTabChanged(object sender, EventArgs e)
        {
            //For Quotation
            if (tbc_quotation.ActiveTabIndex == 0)
            {
                ActiveTab = 0;
                QuotationControl.LoadControl();
            }

            //For Delivery Schedule
            else if (tbc_quotation.ActiveTabIndex == 1)
            {
                ActiveTab = 1;
                DeliveryControl.LoadControl();
            }

            //For Payment Terms
            else if (tbc_quotation.ActiveTabIndex == 2)
            {
                ActiveTab = 2;
                PaymentControl.LoadControl();
            }

            //For Term and Conditions
            else if (tbc_quotation.ActiveTabIndex == 3)
            {
                ActiveTab = 3;
                TermConditionControl.LoadControl();
            }

        }


        #endregion



    }
}