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

namespace MiniERP.Quotation.Parts
{
    public partial class DeliveryScheduleContractor : System.Web.UI.UserControl
    {

        #region Private Variables

        int id = 0;

        int activityId = 0, itemId = 0, specificationid = 0;
        bool track = false;
        bool flag = false;
        DateTime scheduleDate, nowDate;
        Decimal quantity = 0;
        String activityDescription = String.Empty;

        BasePage base_Page = new BasePage();
        LinkButton imgBtn = null;
        Label lblQuantityLeft = null;
        //Label lblActivityDescription = null;
        //HiddenField hdf = null;
        TextBox txtQuantity = null;
        TextBox txtDate = null;
        LinkButton lbtnAdd = null;


        DeliveryScheduleDOM deliverySchedule = null;

        #endregion

        #region Public Methods

        public void LoadControl()
        {
            if (base_Page.ActiveTab == 1)
            {
                MyItemTransactionList = new List<ItemTransaction>();
                MyItemTransactionList = base_Page.ItemTransactionList;
                DefaultLoad();
            }
        }

        #endregion

        #region Protected Methods

        protected void Page_Load(object sender, EventArgs e)
        {
            if (base_Page.ActiveTab == 0)
            {
                MyItemTransactionList = new List<ItemTransaction>();
                MyItemTransactionList = base_Page.ItemTransactionList;
                DefaultLoad();
            }
        }

        protected void gv_Delivery_Schedule_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                txtDate = (TextBox)e.Row.FindControl("txt_shedule_date");
                txtDate.Text = DateTime.Now.Date.ToString("dd'/'MM'/'yyyy");

                txtQuantity = (TextBox)e.Row.FindControl("txt_Schedule_Quantity");
                imgBtn = (LinkButton)e.Row.FindControl("img_btn_calander_order");
                lbtnAdd = (LinkButton)e.Row.FindControl("lnkAdd");

                if (txtQuantity.Text == "0" || txtQuantity.Text == "0.00")
                {
                    txtQuantity.Enabled = false;
                    imgBtn.Enabled = false;
                    lbtnAdd.Enabled = false;
                }
            }
        }

        protected void gv_Delivery_Schedule_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "add")
            {
                int index = int.Parse(e.CommandArgument.ToString());

                GridViewRow row = gv_Delivery_Schedule.Rows[index];

                SetValue(row);
            }
        }

        protected void gv_Final_Delivery_Schedule_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "del")
            {
                id = Convert.ToInt32(e.CommandArgument.ToString());
                base_Page.DeliveryScheduleList.RemoveAt(id);
                if (base_Page.DeliveryScheduleList.Count == 0)
                {
                    base_Page.DeliveryScheduleList = null;
                }
                DefaultLoad();
            }
        }

        protected void gv_Final_Delivery_Schedule_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void gv_Final_Delivery_Schedule_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }

        protected void gv_Final_Delivery_Schedule_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        #endregion

        #region Private Methods

        private List<ItemTransaction> MyItemTransactionList
        {
            get
            {
                return (List<ItemTransaction>)ViewState["MyItemTransactionList"];
            }
            set
            {
                ViewState["MyItemTransactionList"] = value;
            }
        }

        private void HideDeliveryScheduleColumn()
        {
            if (!IsContractor)
            {
                gv_Delivery_Schedule.Columns[1].Visible = false;
            }
        }

        private void HideFinalGridColumn()
        {
            if (!IsContractor)
            {
                gv_Final_Delivery_Schedule.Columns[1].Visible = false;
            }
        }

        private void DefaultLoad()
        {
            ControlsView();
            BindData();
            BindFinalGrid();
            if (base_Page.DeliveryScheduleList == null)
            {
                divFinal.Visible = false;
            }
            else
            {
                divFinal.Visible = true;
            }
        }

        private void ControlsView()
        {
            if ((base_Page.QuotationStatusID == Convert.ToInt32(StatusType.Pending)) || (base_Page.QuotationStatusID == Convert.ToInt32(StatusType.InComplete)))
            {
                EnableDisableControls(true);
            }
            else
            {
                EnableDisableControls(false);
            }
        }

        private void EnableDisableControls(bool condition)
        {
            gv_Delivery_Schedule.Visible = condition;

            gv_Delivery_Schedule.Columns[9].Visible = condition;
            gv_Final_Delivery_Schedule.Columns[7].Visible = condition;
        }

        private void BindData()
        {
            if (MyItemTransactionList != null)
            {
                UpdateData();
                UpdateItemQuantity();
                BindGrid();
                HideDeliveryScheduleColumn();
            }
            else
            {
                BindGrid();
            }
        }

        private void BindGrid()
        {
            if (MyItemTransactionList != null)
                gv_Delivery_Schedule.DataSource = MyItemTransactionList;
            else
                gv_Delivery_Schedule.DataSource = null;
            gv_Delivery_Schedule.DataBind();
        }


        private void UpdateData()
        {
            if (base_Page.DeliveryScheduleList != null)
                for (int i = 0; i < base_Page.DeliveryScheduleList.Count; i++)
                {
                    track = true;
                    foreach (ItemTransaction item in MyItemTransactionList)
                    {
                        if (IsContractor)
                        {
                            if (item.DeliverySchedule.ActivityDescriptionId == base_Page.DeliveryScheduleList[i].ItemDescriptionId
                               && item.Item.ItemId == base_Page.DeliveryScheduleList[i].ItemId
                               && item.Item.ModelSpecification.ModelSpecificationId == base_Page.DeliveryScheduleList[i].SpecificationId)
                            {
                                track = false;
                            }
                        }
                        else
                        {
                            if (item.Item.ItemId == base_Page.DeliveryScheduleList[i].ItemId
                               && item.Item.ModelSpecification.ModelSpecificationId == base_Page.DeliveryScheduleList[i].SpecificationId)
                            {
                                track = false;
                            }
                        }
                    }
                    if (track)
                        base_Page.DeliveryScheduleList.RemoveAt(i);
                }
        }

        private void BindFinalGrid()
        {
            if (base_Page.DeliveryScheduleList != null)
                gv_Final_Delivery_Schedule.DataSource = base_Page.DeliveryScheduleList;
            else
                gv_Final_Delivery_Schedule.DataSource = null;
            gv_Final_Delivery_Schedule.DataBind();
            HideFinalGridColumn();
        }


        private void UpdateItemQuantity()
        {
            for (int i = 0; i < MyItemTransactionList.Count; i++)
            {
                quantity = MyItemTransactionList[i].NumberOfUnit;
                MyItemTransactionList[i].UnitLeft = quantity;
                MyItemTransactionList[i].NumberOfUnit = quantity;
            }

            if (base_Page.DeliveryScheduleList != null)
                for (int i = 0; i < MyItemTransactionList.Count; i++)
                {
                    track = true;
                    for (int j = 0; j < base_Page.DeliveryScheduleList.Count; j++)
                    {
                        if (IsContractor)
                        {
                            if (MyItemTransactionList[i].DeliverySchedule.ActivityDescriptionId == base_Page.DeliveryScheduleList[j].ItemDescriptionId
                                && MyItemTransactionList[i].Item.ItemId == base_Page.DeliveryScheduleList[j].ItemId
                                && MyItemTransactionList[i].Item.ModelSpecification.ModelSpecificationId == base_Page.DeliveryScheduleList[j].SpecificationId)
                            {
                                quantity = 0;
                                track = false;

                                for (int k = 0; k < base_Page.DeliveryScheduleList.Count; k++)
                                {
                                    if (MyItemTransactionList[i].DeliverySchedule.ActivityDescriptionId == base_Page.DeliveryScheduleList[k].ItemDescriptionId
                                        && MyItemTransactionList[i].Item.ItemId == base_Page.DeliveryScheduleList[k].ItemId
                                        && MyItemTransactionList[i].Item.ModelSpecification.ModelSpecificationId == base_Page.DeliveryScheduleList[k].SpecificationId)
                                    {
                                        quantity = quantity + base_Page.DeliveryScheduleList[k].ItemQuantity;
                                    }
                                }

                                if ((MyItemTransactionList[i].NumberOfUnit - quantity) < 0)
                                {
                                    for (int a = 0; a < base_Page.DeliveryScheduleList.Count; a++)
                                    {
                                        if (MyItemTransactionList[i].DeliverySchedule.ActivityDescriptionId == base_Page.DeliveryScheduleList[a].ItemDescriptionId
                                             && MyItemTransactionList[i].Item.ItemId == base_Page.DeliveryScheduleList[a].ItemId
                                              && MyItemTransactionList[i].Item.ModelSpecification.ModelSpecificationId == base_Page.DeliveryScheduleList[a].SpecificationId)
                                        {
                                            base_Page.DeliveryScheduleList.RemoveAt(a);
                                        }
                                    }
                                }
                                else
                                    MyItemTransactionList[i].UnitLeft = MyItemTransactionList[i].NumberOfUnit - quantity;
                            }
                        }
                        else
                        {
                            if (MyItemTransactionList[i].Item.ItemId == base_Page.DeliveryScheduleList[j].ItemId
                                && MyItemTransactionList[i].Item.ModelSpecification.ModelSpecificationId == base_Page.DeliveryScheduleList[j].SpecificationId)
                            {
                                track = false;
                                quantity = 0;

                                for (int l = 0; l < base_Page.DeliveryScheduleList.Count; l++)
                                {
                                    if (MyItemTransactionList[i].Item.ItemId == base_Page.DeliveryScheduleList[l].ItemId
                                        && MyItemTransactionList[i].Item.ModelSpecification.ModelSpecificationId == base_Page.DeliveryScheduleList[l].SpecificationId)
                                    {
                                        quantity = quantity + base_Page.DeliveryScheduleList[l].ItemQuantity;
                                    }
                                }

                                if ((MyItemTransactionList[i].NumberOfUnit - quantity) < 0)
                                {
                                    for (int b = 0; b < base_Page.DeliveryScheduleList.Count; b++)
                                    {
                                        if (MyItemTransactionList[i].Item.ItemId == base_Page.DeliveryScheduleList[b].ItemId
                                              && MyItemTransactionList[i].Item.ModelSpecification.ModelSpecificationId == base_Page.DeliveryScheduleList[b].SpecificationId)
                                        {
                                            base_Page.DeliveryScheduleList.RemoveAt(b);
                                        }
                                    }
                                }
                                else
                                    MyItemTransactionList[i].UnitLeft = MyItemTransactionList[i].NumberOfUnit - quantity;
                            }
                        }
                    }
                }
        }

        private void SetValue(GridViewRow row)
        {
            if (IsContractor)
            {
                HiddenField hdfActivity = (HiddenField)row.FindControl("hdfActivityId");
                activityId = Convert.ToInt32(hdfActivity.Value);
            }
            HiddenField hdfItem = (HiddenField)row.FindControl("hdfItemId");
            itemId = Convert.ToInt32(hdfItem.Value);
            HiddenField hdfSpecification = (HiddenField)row.FindControl("hdfSpecificationId");
            specificationid = Convert.ToInt32(hdfSpecification.Value);

            lblQuantityLeft = (Label)row.FindControl("lblQuantityLeft");
            txtQuantity = (TextBox)row.FindControl("txt_Schedule_Quantity");
            txtDate = (TextBox)row.FindControl("txt_shedule_date");
            lbtnAdd = (LinkButton)row.FindControl("lnkAdd");

            //For Date Comprasion
            scheduleDate = DateTime.ParseExact(txtDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            nowDate = DateTime.ParseExact(DateTime.Now.Date.ToString("dd'/'MM'/'yyyy"), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            //End

            SetDeliverySchedule(row, activityId, itemId, specificationid);
        }

        private void SetDeliverySchedule(GridViewRow row, Int32 activityId, Int32 itemId, Int32 specificationid)
        {
            track = false;
            track = Decimal.TryParse(txtQuantity.Text.Trim(), out quantity);
            if (track)
            {
                if (quantity == 0)
                {
                    txtQuantity.Focus();
                    base_Page.Alert("Schedule Quantity can not be Zero(0)", lbtnAdd);
                }
                else if (quantity > Convert.ToDecimal(lblQuantityLeft.Text))
                {
                    txtQuantity.Focus();
                    base_Page.Alert("Schedule Quantity can not be more than Quantity Left.", lbtnAdd);
                }
                else if (scheduleDate < nowDate)
                {
                    txtDate.Focus();
                    base_Page.Alert("Schedule Date can not be less than Present Date.", lbtnAdd);
                }
                else
                {
                    GetData(activityId, itemId, specificationid);
                }
            }
            else
            {
                flag = true;
                txtQuantity.Focus();
                base_Page.Alert("Only valid Numeric Values are allowed in Schedule Quantity.", lbtnAdd);
            }
        }

        private void GetData(Int32 activityId, Int32 itemId, Int32 specificationid)
        {
            deliverySchedule = GetControlsData(activityId, itemId, specificationid);

            if (base_Page.DeliveryScheduleList == null)
            {
                base_Page.DeliveryScheduleList = new List<DeliveryScheduleDOM>();
                base_Page.DeliveryScheduleList.Add(deliverySchedule);
            }
            else
            {
                base_Page.DeliveryScheduleList.Add(deliverySchedule);
            }

            DefaultLoad();
        }

        private DeliveryScheduleDOM GetControlsData(Int32 activityId, Int32 itemId, Int32 specificationid)
        {
            deliverySchedule = new DeliveryScheduleDOM();

            foreach (ItemTransaction item in MyItemTransactionList)
            {
                if (IsContractor)
                {
                    if (item.DeliverySchedule.ActivityDescriptionId == activityId
                        && item.Item.ItemId == itemId
                        && item.Item.ModelSpecification.ModelSpecificationId == specificationid)
                    {
                        deliverySchedule.ItemDescription = item.DeliverySchedule.ActivityDescription;
                        deliverySchedule.ItemDescriptionId = item.DeliverySchedule.ActivityDescriptionId;

                        deliverySchedule.Item = item.Item.ItemName;
                        deliverySchedule.ItemId = item.Item.ItemId;

                        deliverySchedule.Specification = item.Item.ModelSpecification.ModelSpecificationName;
                        deliverySchedule.SpecificationId = item.Item.ModelSpecification.ModelSpecificationId;

                        deliverySchedule.ItemQuantity = quantity;

                        deliverySchedule.SpecificationUnit = item.Item.ModelSpecification.UnitMeasurement.Name;
                        deliverySchedule.DeliveryDate = scheduleDate;
                    }
                }
                else
                {
                    if (item.Item.ItemId == itemId
                        && item.Item.ModelSpecification.ModelSpecificationId == specificationid)
                    {
                        deliverySchedule.Item = item.Item.ItemName;
                        deliverySchedule.ItemId = item.Item.ItemId;

                        deliverySchedule.Specification = item.Item.ModelSpecification.ModelSpecificationName;
                        deliverySchedule.SpecificationId = item.Item.ModelSpecification.ModelSpecificationId;

                        deliverySchedule.ItemQuantity = quantity;
                        deliverySchedule.SpecificationUnit = item.Item.ModelSpecification.UnitMeasurement.Name;
                        deliverySchedule.DeliveryDate = scheduleDate;
                    }
                }
            }
            return deliverySchedule;
        }

        #endregion

        #region Private Property

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