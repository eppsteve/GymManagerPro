﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GymManagerPro.RibbonUI
{
    public partial class AddNewContract : Form
    {
        private int id;                 // member's id
        bool has_loaded = false;        // set to true when the form has been loaded using shown event. This is used to avoid errors with the initialization of textboxes values

        public AddNewContract()
        {
            InitializeComponent();
        }

        public AddNewContract(int id)
        {
            InitializeComponent();
            this.id = id;
        }

        private void AddNewContract_Load(object sender, EventArgs e)
        {
            // get all plans and fill the combobox with the data
            cbProgrammes.DataSource = new BindingSource(DataLayer.Plan.GetAllPlans(), null);    // bind dictionary to combobox
            cbProgrammes.DisplayMember = "Value";                                               // name of the plan
            cbProgrammes.ValueMember = "Key";                                                   // id of the plan
        }

        private void btnFinish_Click(object sender, EventArgs e)
        {
            if (cbProgrammes.Text != "")
            {
                
                // create a new membership and fill with data
                DataLayer.Membership membership = new DataLayer.Membership();
                membership.MemberId = this.id;                                      // member's id
                membership.Plan = (int) cbProgrammes.SelectedValue;                 // id of the selected plan
                membership.start = datePickerStart.Value;                           // when the membership starts
                membership.end = datePickerEnd.Value;                               // when the membership expires

               
                if (DataLayer.Memberships.NewMembership(membership) >0 )
                {
                    MessageBox.Show("Membership added successfully!", "Gym Manager Pro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Failed to add new membership. Please try again", "Gym Manager Pro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                this.Close();
            }
            else
            {
                MessageBox.Show("Please Select a Programme!", "Gym Manager Pro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void datePickerStart_ValueChanged(object sender, EventArgs e)
        {
            // set the expiration date of the membership based on the start date
            datePickerEnd.Value = datePickerStart.Value;                                                            // get membership's start date
            int plan_id = Int32.Parse( cbProgrammes.SelectedValue.ToString());                                      // get id of the selected plan
            datePickerEnd.Value = datePickerEnd.Value.AddMonths(DataLayer.Plan.GetPlanDuration(plan_id));           // calculate membership duration
            datePickerEnd.Value = datePickerEnd.Value.AddDays(-1);                                                  // subtract one day
        }

        private void cbProgrammes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (has_loaded)
            {
                int plan_id = Int32.Parse(cbProgrammes.SelectedValue.ToString());                                           // get id of the selected plan

                txtMembershipFee.Text = DataLayer.Plan.GetPlanPrice(plan_id).ToString();                                    // get membership's cost
                datePickerEnd.Value = datePickerStart.Value;                                                                // get membership's start date
                datePickerEnd.Value = datePickerEnd.Value.AddMonths(DataLayer.Plan.GetPlanDuration(plan_id));               // calculate membership duration
                datePickerEnd.Value = datePickerEnd.Value.AddDays(-1);                                                      // subtract one day
            }
        }

        private void txtInitiationFee_TextChanged(object sender, EventArgs e)
        {
            int plan_id = Int32.Parse(cbProgrammes.SelectedValue.ToString());                                           // get id of the selected plan
            decimal programmefee = DataLayer.Plan.GetPlanPrice(plan_id);                                                // get selected plan's price
            try
            {
                if (txtInitiationFee.Text.Trim().Length == 0)                                                           // if initationfee textbox is empty
                    txtInitiationFee.Text = "0";
                else
                {
                    decimal totalfee = (decimal)Decimal.Parse(txtInitiationFee.Text.ToString()) + programmefee;         // calculate the total fee by adding the initation fee to the plan's fee
                    txtTotalFees.Text = totalfee.ToString();                                                            // display total fee
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void AddNewContract_Shown(object sender, EventArgs e)
        {
            this.has_loaded = true;
        }
    }
}
