using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DocumentObjectModel;
using BusinessAccessLayer;
using MiniERP.Shared;

namespace MiniERP.PurchaseOrder
{
    public partial class ContractorPurchaseOrder : BasePage
    {
        #region Private Global Variable(s)

        #endregion

        #region Protected Methods
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindContractorName();
                BindProjectName();
            }
        }

        protected void btnGeneratePurchaseOrder_Click(object sender, EventArgs e)
        {
            Response.Redirect("~\\pdf\\ContractorWork.pdf");
        }

        #endregion

        #region Private Methods

        private void BindContractorName()
        {
            ContractorBL contractorBL = new ContractorBL();
            List<Contractor> lstContractor = new List<Contractor>();
            lstContractor = contractorBL.ReadContractor(null);
            ddlContractorName.DataSource = lstContractor;
            ddlContractorName.DataValueField = "ContractorId";
            ddlContractorName.DataTextField = "Name";
            ddlContractorName.DataBind();

            ddlContractorName.Items.Insert(0, "--Select--");
        }

        private void BindProjectName()
        {
            ProjectBAL projectBAL = new ProjectBAL();
            List<Project> lstProject = new List<Project>();
            lstProject = projectBAL.ReadProject();
            ddlProject.DataSource = lstProject;
            ddlProject.DataValueField = "ProjectId";
            ddlProject.DataTextField = "Name";
            ddlProject.DataBind();

            ddlProject.Items.Insert(0, "--Select--");
        }


        #endregion

        #region Private Properties

        #endregion
    }
}