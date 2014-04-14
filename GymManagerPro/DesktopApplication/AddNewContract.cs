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
        private int id;         // member's id

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
            cbProgrammes.DataSource = new BindingSource(DataLayer.Plan.GetAllPlans(), null);
            cbProgrammes.DisplayMember = "Value";
            cbProgrammes.ValueMember = "Key";
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
            datePickerEnd.Value = datePickerEnd.Value.AddMonths(DataLayer.Plan.GetPlanDuration(cbProgrammes.Text)); // calculate membership duration
            datePickerEnd.Value = datePickerEnd.Value.AddDays(-1);                                                  // subtract one day
        }

        private void cbProgrammes_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtMembershipFee.Text = DataLayer.Members.GetProgrammePrice(cbProgrammes.Text).ToString();                  // get membership's cost
            datePickerEnd.Value = datePickerStart.Value;                                                                // get membership's start date
            datePickerEnd.Value = datePickerEnd.Value.AddMonths(DataLayer.Plan.GetPlanDuration(cbProgrammes.Text));     // calculate membership duration
            datePickerEnd.Value = datePickerEnd.Value.AddDays(-1);                                                      // subtract one day
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
