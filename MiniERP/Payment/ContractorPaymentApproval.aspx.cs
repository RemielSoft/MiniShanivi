﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MiniERP.Payment
{
    public partial class ContractorPaymentApproval : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CSPayment.PageType = "Contractor";
        }
    }
}