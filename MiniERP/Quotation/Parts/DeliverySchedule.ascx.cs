using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DocumentObjectModel;
using BusinessAccessLayer;
using MiniERP.Shared;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Globalization;
using System.Threading;

namespace MiniERP.Parts
{
    public partial class DeliverySchedule : System.Web.UI.UserControl
    {
        #region Private Global Variables

        bool flag;
        bool track = false;
        Decimal quantity = 0;
        Label lblItem = null;
        Label lblItemQuantity = null;

        Decimal itemQuantity = 0;
        int id = 0;
        string pageName = String.Empty;
        BasePage base_Page = new BasePage();

        DeliveryScheduleDOM deliverySchedule = null;

        List<DeliveryScheduleDOM> lst_delivery_schedule = null;

        #endregion

        #region Main Page Method

        public void LoadControl()
        {
            if (base_Page.ActiveTab == 1)
            {
                //Thread.Sleep(2000);
                DefaultLoad();
            }
        }

        #endregion

        #region Protected Methods

        //protected override void OnInit(EventArgs e)
        //{
        //    if (base_Page.ActiveTab == 1)
        //    {
        //        //Thread.Sleep(2000);
        //        BindData();
        //        ClearAll();
        //        ControlsView();
        //        BindGrid();
        //        SetRegularExpressions();
        //    }
        //}

        //protected override void OnLoad(EventArgs e)
        //{
        //    base.OnLoad(e);
        //    if (!IsPostBack)
        //    {
        //        if (base_Page.ActiveTab == 1)
        //        {
        //            //Thread.Sleep(2000);
        //            BindData();
        //            ClearAll();
        //            ControlsView();
        //            BindGrid();
        //            SetRegularExpressions();
        //        }
        //    }
        //}

        //protected override void OnDataBinding(EventArgs e)
        //{
        //    EnsureChildControls();
        //    base.OnDataBinding(e);
        //    if (base_Page.ActiveTab == 1)
        //    {
        //        //Thread.Sleep(2000);
        //        BindData();
        //        ClearAll();
        //        ControlsView();
        //        BindGrid();
        //        SetRegularExpressions();
        //    }
        //}

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && base_Page.ActiveTab == 0)
            {
                DefaultLoad();
            }
        }

        protected void ddl_Item_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (ItemTransaction item in base_Page.ItemTransactionList)
            {
                if (item.DeliverySchedule.SpecificationId == Convert.ToInt32(ddl_Item.SelectedValue))
                {
                    lbl_unit_of_measurement.Text = item.DeliverySchedule.SpecificationUnit;
                    hdf_number_of_unit.Value = item.NumberOfUnit.ToString();
                    break;
                }
                else
                    lbl_unit_of_measurement.Text = String.Empty;
            }
        }

        protected void btn_add_Click(object sender, EventArgs e)
        {
            if (btn_add.Text == "Add")
            {
                deliverySchedule = GetControlsData();
                if (base_Page.DeliveryScheduleList == null)
                {
                    base_Page.DeliveryScheduleList = new List<DeliveryScheduleDOM>();
                    if (!IsContractor)
                    {
                        if (CheckDuplicate(deliverySchedule))
                        {
                            lbl_duplicate_activity.Text = GlobalConstants.L_Item_Exceed;
                        }
                        else
                            base_Page.DeliveryScheduleList.Add(deliverySchedule);
                    }

                    else
                        base_Page.DeliveryScheduleList.Add(deliverySchedule);
                }
                else
                {
                    if (CheckDuplicate(deliverySchedule))
                    {
                        if (IsContractor)
                            lbl_duplicate_activity.Text = GlobalConstants.L_Duplicate_Activity;
                        else
                            lbl_duplicate_activity.Text = GlobalConstants.L_Item_Exceed;
                    }
                    else
                    {
                        base_Page.DeliveryScheduleList.Add(deliverySchedule);
                    }
                }
            }
            else if (btn_add.Text == "Update")
            {
                if (this.LastIndex >= 0)
                {
                    deliverySchedule = GetControlsData();
                    if (CheckDuplicate(deliverySchedule))
                    {
                        if (IsContractor)
                            lbl_duplicate_activity.Text = GlobalConstants.L_Duplicate_Activity;
                        else
                            lbl_duplicate_activity.Text = GlobalConstants.L_Item_Exceed;

                    }
                    else
                    {
                        base_Page.DeliveryScheduleList = UpdateData(deliverySchedule);
                        btn_add.Text = "Add";
                        LastIndex = -1;
                    }
                }
            }
            BindGrid();
            if (track == false)
            {
                ClearAll();
            }
        }

        protected void btn_cancel_Click(object sender, EventArgs e)
        {
            btn_add.Text = "Add";
            ClearAll();
        }

        protected void gv_Delivery_Schedule_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName == "Edit")
            {
                ddl_Activity_Desc_DS.SelectedIndex = ddl_Activity_Desc_DS.Items.IndexOf(ddl_Activity_Desc_DS.Items.FindByText(base_Page.DeliveryScheduleList[index].ActivityDescription));

                ddl_Item.SelectedValue = base_Page.DeliveryScheduleList[index].SpecificationId.ToString();
                txt_Item_Quantity.Text = base_Page.DeliveryScheduleList[index].ItemQuantity.ToString();
                hdf_number_of_unit.Value = base_Page.DeliveryScheduleList[index].ActualNumberOfUnit.ToString();

                txt_Delivery_Date_DS.Text = base_Page.DeliveryScheduleList[index].DeliveryDate.ToString("MM'/'dd'/'yyyy");

                btn_add.Text = "Update";
                this.LastIndex = index;

                ddl_Item_SelectedIndexChanged(null, null);
            }
            else if (e.CommandName == "Delete")
            {
                base_Page.DeliveryScheduleList.RemoveAt(index);
                if (base_Page.DeliveryScheduleList.Count == 0)
                {
                    ClearAll();
                    btn_add.Text = "Add";
                    base_Page.DeliveryScheduleList = null;
                }
                BindGrid();
            }
        }

        protected void gv_Delivery_Schedule_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void gv_Delivery_Schedule_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }

        protected void gv_Delivery_Schedule_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gv_Delivery_Schedule.PageIndex = e.NewPageIndex;
            BindGrid();
        }

        #endregion


        #region Private Methods

        private void DefaultLoad()
        {
            BindData();
            ClearAll();
            ControlsView();
            BindGrid();
            SetRegularExpressions();
        }

        private void SetRegularExpressions()
        {
            rev_Item_Quantity.ValidationExpression = ValidationExpression.C_DECIMAL;
        }

        private void BindData()
        {
            // cal_ext_delivery.StartDate = DateTime.Now;
            if (IsContractor)
            {
                BindActivityDescription();
                UpdateDeliverySchedule();
                rw_item.Visible = false;
            }
            else if (base_Page.Page_Name == GlobalConstants.P_Supplier_Quotation)
            {
                BindItem();
                UpdateDeliverySchedule();
                UpdateItemQuantity();
                rw_activity.Visible = false;
            }
        }

        private void BindActivityDescription()
        {

            if (base_Page.ItemTransactionList != null)
            {
                lst_delivery_schedule = new List<DeliveryScheduleDOM>();

                foreach (ItemTransaction item in base_Page.ItemTransactionList)
                {
                    deliverySchedule = new DeliveryScheduleDOM();
                    deliverySchedule.ActivityDescription = item.DeliverySchedule.ActivityDescription;
                    lst_delivery_schedule.Add(deliverySchedule);
                }
                base_Page.BindDropDown(ddl_Activity_Desc_DS, "ActivityDescription", "ActivityDescription", lst_delivery_schedule);
            }
        }

        private void BindItem()
        {
            if (base_Page.ItemTransactionList != null)
            {
                lst_delivery_schedule = new List<DeliveryScheduleDOM>();

                foreach (ItemTransaction item in base_Page.ItemTransactionList)
                {
                    deliverySchedule = new DeliveryScheduleDOM();
                    deliverySchedule.ItemDescription = item.DeliverySchedule.ItemDescription;
                    deliverySchedule.SpecificationId = item.DeliverySchedule.SpecificationId;
                    lst_delivery_schedule.Add(deliverySchedule);
                }
                base_Page.BindDropDown(ddl_Item, "ItemDescription", "SpecificationId", lst_delivery_schedule);
            }
        }

        private DeliveryScheduleDOM GetControlsData()
        {
            deliverySchedule = new DeliveryScheduleDOM();
            if (ddl_Activity_Desc_DS.SelectedIndex != 0)
            {
                deliverySchedule.ActivityDescription = ddl_Activity_Desc_DS.SelectedItem.ToString();
            }
            if (Convert.ToInt32(ddl_Item.SelectedValue) != 0)
            {
                deliverySchedule.ItemDescription = ddl_Item.SelectedItem.ToString();
                deliverySchedule.SpecificationId = Convert.ToInt32(ddl_Item.SelectedValue);
                deliverySchedule.ItemQuantity = Convert.ToDecimal(txt_Item_Quantity.Text.Trim());
                deliverySchedule.ActualNumberOfUnit = Convert.ToDecimal(hdf_number_of_unit.Value);
            }
            deliverySchedule.DeliveryDate = DateTime.ParseExact(txt_Delivery_Date_DS.Text, "MM/dd/yyyy", CultureInfo.InvariantCulture);

            return deliverySchedule;

        }

        private List<DeliveryScheduleDOM> UpdateData(DeliveryScheduleDOM deliverySchedule)
        {
            base_Page.DeliveryScheduleList[this.LastIndex].ActivityDescription = deliverySchedule.ActivityDescription;
            base_Page.DeliveryScheduleList[this.LastIndex].ItemDescription = deliverySchedule.ItemDescription;
            base_Page.DeliveryScheduleList[this.LastIndex].SpecificationId = deliverySchedule.SpecificationId;
            base_Page.DeliveryScheduleList[this.LastIndex].ItemQuantity = deliverySchedule.ItemQuantity;
            base_Page.DeliveryScheduleList[this.LastIndex].DeliveryDate = deliverySchedule.DeliveryDate;

            return base_Page.DeliveryScheduleList;
        }

        private void UpdateDeliverySchedule()
        {
            if (base_Page.DeliveryScheduleList != null)
            {
                if (base_Page.ItemTransactionList != null)
                {
                    for (int i = 0; i < base_Page.DeliveryScheduleList.Count; i++)
                    {
                        foreach (ItemTransaction item in base_Page.ItemTransactionList)
                        {
                            if (IsContractor)
                            {
                                if (base_Page.DeliveryScheduleList[i].ActivityDescription == item.DeliverySchedule.ActivityDescription)
                                {
                                    track = true;
                                    break;
                                }
                                else
                                {
                                    track = false;
                                }
                            }
                            else
                            {
                                if (base_Page.DeliveryScheduleList[i].SpecificationId == item.DeliverySchedule.SpecificationId)
                                {
                                    track = true;
                                    break;
                                }
                                else
                                {
                                    track = false;
                                }
                            }
                        }
                        if (track == false)
                        {
                            base_Page.DeliveryScheduleList.RemoveAt(i);
                        }
                    }
                }
                else
                {
                    base_Page.DeliveryScheduleList = null;
                }
                BindGrid();
            }
        }

        private void UpdateItemQuantity()
        {
            if (base_Page.DeliveryScheduleList != null)
            {
                if (base_Page.ItemTransactionList != null)
                {
                    for (int i = 0; i < base_Page.DeliveryScheduleList.Count; i++)
                    {
                        for (int j = 0; j < base_Page.ItemTransactionList.Count; j++)
                        {
                            if (!IsContractor)
                            {
                                if (base_Page.DeliveryScheduleList[i].ItemDescription == base_Page.ItemTransactionList[j].DeliverySchedule.ItemDescription)
                                {
                                    base_Page.DeliveryScheduleList[i].ActualNumberOfUnit = base_Page.ItemTransactionList[j].NumberOfUnit;
                                }
                            }
                        }
                    }
                }
            }
        }

        private Boolean CheckDuplicate(DeliveryScheduleDOM delivery_schedule)
        {
            id = 0;
            if (!IsContractor && base_Page.DeliveryScheduleList.Count == 0)
            {
                foreach (ItemTransaction transaction in base_Page.ItemTransactionList)
                {
                    if (delivery_schedule.ItemQuantity > transaction.NumberOfUnit)
                        track = true;
                }
            }
            foreach (DeliveryScheduleDOM item in base_Page.DeliveryScheduleList)
            {
                id = id + 1;
                if (IsContractor)
                {
                    if (delivery_schedule.ActivityDescription.ToLower() == item.ActivityDescription.ToLower())
                    {
                        if (LastIndex != -1 && id == LastIndex + 1)
                        {
                            track = false;
                            break;
                        }
                        else
                        {
                            track = true;
                            break;
                        }
                    }
                    else
                    {
                        track = false;
                    }
                }
                else
                {
                    //Can not be More Than Number of Unit At Speciication Id
                    if (delivery_schedule.SpecificationId == item.SpecificationId)
                    {
                        foreach (GridViewRow row in gv_Delivery_Schedule.Rows)
                        {
                            lblItem = (Label)row.FindControl("lblItem");
                            lblItemQuantity = (Label)row.FindControl("lblItemQuantity");
                            if (lblItem.Text == item.ItemDescription && row.RowIndex != LastIndex)
                            {
                                quantity = quantity + Convert.ToDecimal(lblItemQuantity.Text);
                            }
                        }
                        foreach (ItemTransaction transaction in base_Page.ItemTransactionList)
                        {
                            if (item.ItemDescription == transaction.DeliverySchedule.ItemDescription)
                            {
                                itemQuantity = transaction.NumberOfUnit;
                            }
                        }
                        if ((quantity + delivery_schedule.ItemQuantity) > itemQuantity)
                        {
                            track = true;
                            break;
                        }
                        else
                        {
                            track = false;
                            break;
                        }
                    }
                    else if (delivery_schedule.SpecificationId != item.SpecificationId)
                    {
                        foreach (ItemTransaction transaction in base_Page.ItemTransactionList)
                        {
                            if (delivery_schedule.ItemDescription == transaction.DeliverySchedule.ItemDescription)
                            {
                                if (delivery_schedule.ItemQuantity > transaction.NumberOfUnit)
                                    track = true;
                                break;
                            }
                        }
                    }
                    else
                    {
                        track = false;
                    }
                }
                if (track == true)
                {
                    break;
                }

            }
            return track;
        }

        private void BindGrid()
        {
            if (base_Page.DeliveryScheduleList != null)
                gv_Delivery_Schedule.DataSource = base_Page.DeliveryScheduleList;
            else
                gv_Delivery_Schedule.DataSource = null;
            gv_Delivery_Schedule.DataBind();
            if (IsContractor)
            {
                gv_Delivery_Schedule.Columns[1].Visible = false;
                gv_Delivery_Schedule.Columns[2].Visible = false;
                gv_Delivery_Schedule.Columns[3].Visible = false;

            }
            else
            {
                gv_Delivery_Schedule.Columns[0].Visible = false;
            }
        }

        private void ClearAll()
        {
            lbl_duplicate_activity.Text = String.Empty;
            ddl_Activity_Desc_DS.SelectedIndex = 0;
            ddl_Item.SelectedIndex = 0;
            txt_Item_Quantity.Text = String.Empty;
            txt_Delivery_Date_DS.Text = String.Empty;
            lbl_unit_of_measurement.Text = String.Empty;
        }

        private void ControlsView()
        {
            if (base_Page.QuotationStatusID != Convert.ToInt32(StatusType.Pending))
            {
                EnableDisableControls(false);
            }
            else
            {
                EnableDisableControls(true);
            }
        }

        private void EnableDisableControls(bool condition)
        {
            tbl_delivery.Visible = condition;
            dv_button.Visible = condition;

            gv_Delivery_Schedule.Columns[5].Visible = condition;
        }

        #endregion


        #region Private Property

        private int LastIndex
        {
            get
            {
                try
                {
                    return (int)ViewState["Index"];
                }
                catch
                {
                    return -1;
                }
            }
            set
            {
                ViewState["Index"] = value;
            }
        }

        private bool IsContractor
        {
            get
            {
                if (base_Page.Page_Name == GlobalConstants.P_Contractor_Quotation)
                {
                    flag = true;
                    return flag;
                }
                else
                {
                    flag = false;
                    return flag;
                }
            }
            set
            {
                flag = value;
            }
        }
        #endregion


    }
}