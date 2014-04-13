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
    public partial class Plans : Form
    {
        DataLayer.Plan plan;
        //int planId; // id of the selected plan

        public Plans()
        {
            InitializeComponent();
        }

        private void Plans_Load(object sender, EventArgs e)
        {
            listBox1.DataSource = DataLayer.Plan.GetAllPlans().ToList();
            listBox1.ValueMember = "Key";
            listBox1.DisplayMember = "Value"; 
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                try
                {
                    int planId = Convert.ToInt32(listBox1.SelectedValue.ToString());
                    plan = DataLayer.Plan.GetPlan(planId);
                    txtName.Text = plan.Name;
                    txtDuration.Text = plan.Duration.ToString();
                    txtPrice.Text = plan.Price.ToString();
                }
                catch { }
            }
        }

        private void btnEditPlan_Click(object sender, EventArgs e)
        {
            EditPlan editplan = new EditPlan(plan);
            editplan.ShowDialog();
        }

        private void Plans_FormClosed(object sender, FormClosedEventArgs e)
        {
            
        }

        private void btnNewPlan_Click(object sender, EventArgs e)
        {
            EditPlan ep = new EditPlan();
            ep.ShowDialog();
        }

        private void btnDeletePlan_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Warning! Deleting the selected plan will also expire (delete) all the associated memberships! Are you sure you want to continue?", "Gym Manager Pro", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            if (dialogResult == DialogResult.Yes)
            {
                DataLayer.Plan.DeletePlan(plan.Id);
                // reload listbox data
                listBox1.DataSource = DataLayer.Plan.GetAllPlans();
                listBox1.ValueMember = "Id";
                listBox1.DisplayMember = "Name"; 
            }
        }
    }
}
