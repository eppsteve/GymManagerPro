using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GymManagerPro
{
    public partial class AddNewContract : Form
    {
        private int id; 

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
            // bind dictionary to combobox
            cbProgrammes.DataSource = new BindingSource(DataLayer.Members.GetAllPlans(), null);
            cbProgrammes.DisplayMember = "Value";
            cbProgrammes.ValueMember = "Key";
        }

        private void btnFinish_Click(object sender, EventArgs e)
        {
            if (cbProgrammes.Text != "")
            {
                
                // create a new membership
                DataLayer.Membership membership = new DataLayer.Membership();
                membership.MemberId = this.id;
                membership.Plan = (int) cbProgrammes.SelectedValue; // get id of the selected plan
                membership.start = datePickerStart.Value;
                membership.end = datePickerEnd.Value;

                //if (DataLayer.Members.AddNewMembership(id, plan_id, datePickerStart.Value, datePickerEnd.Value) > 0)
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
            datePickerEnd.Value = datePickerStart.Value;
            datePickerEnd.Value = datePickerEnd.Value.AddMonths(DataLayer.Members.GetProgrammeDuration(cbProgrammes.Text));
            datePickerEnd.Value = datePickerEnd.Value.AddDays(-1);
        }

        private void cbProgrammes_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtMembershipFee.Text = DataLayer.Members.GetProgrammePrice(cbProgrammes.Text).ToString();
            datePickerEnd.Value = datePickerStart.Value;
            datePickerEnd.Value = datePickerEnd.Value.AddMonths(DataLayer.Members.GetProgrammeDuration(cbProgrammes.Text));
            datePickerEnd.Value = datePickerEnd.Value.AddDays(-1);
        }

        private void txtInitiationFee_TextChanged(object sender, EventArgs e)
        {
            decimal programmefee = DataLayer.Members.GetProgrammePrice(cbProgrammes.Text);
            try
            {
                if (txtInitiationFee.Text.Trim().Length == 0)
                    txtInitiationFee.Text = "0";
                else
                {
                    decimal totalfee = (decimal)Decimal.Parse(txtInitiationFee.Text.ToString()) + programmefee;
                    txtTotalFees.Text = totalfee.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
