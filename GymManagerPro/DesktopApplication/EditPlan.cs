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
    //This class is used for editing an existing plan or adding a new one
    public partial class EditPlan : Form
    {
        DataLayer.Plan plan;
        bool edit_mode = true; //true if we edit an already existed plan, false if we add a new one

        public EditPlan()
        {
            InitializeComponent();
            edit_mode = false;
            this.Text = "Add a new Plan";
        }

        public EditPlan(DataLayer.Plan myplan)
        {
            InitializeComponent();

            plan = new DataLayer.Plan();
            this.plan = myplan;

            txtName.Text = plan.Name;
            txtDuration.Text = plan.Duration.ToString();
            txtPrice.Text = plan.Price.ToString();
            this.Text = "Edit Plan '" + plan.Name + "'";
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (edit_mode)
            {
                // update plan
                plan.Name = txtName.Text;
                plan.Duration = Int32.Parse(txtDuration.Text);
                plan.Price = Decimal.Parse(txtPrice.Text);

                if (DataLayer.Plan.UpdatePlan(plan) > 0)
                {
                    MessageBox.Show("Plan updated!", "Gym Manager Pro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Failed to update. Please try again", "Gym Manager Pro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (!edit_mode)
            {
                // create a new plan
                plan = new DataLayer.Plan();
                plan.Name = txtName.Text;
                plan.Duration = Int32.Parse(txtDuration.Text);
                plan.Price = Decimal.Parse(txtPrice.Text);

               // insert new plan into database              
               if ( DataLayer.Plan.CreateNewPlan(plan) >0 )
               {
                   MessageBox.Show("Plan created!", "Gym Manager Pro", MessageBoxButtons.OK, MessageBoxIcon.Information);
               }
               else
               {
                   MessageBox.Show("Failed create a new plan. Please try again", "Gym Manager Pro", MessageBoxButtons.OK, MessageBoxIcon.Error);
               }  
            }
            this.Close();
        }

        private void EditPlan_Shown(object sender, EventArgs e)
        {

        }
    }
}
