using System;
using System.Linq;
using System.Windows.Forms;

namespace GymManagerPro.View
{
    //This class is used for editing an existing plan or adding a new one
    public partial class EditPlan : Form
    {
        DataLayer.Plan plan;
        bool edit_mode = true;          // true if we edit an already existed plan, false if we add a new one
        ListBox mylistbox = null;       // we pass as an argument the listbox that contains all the lists in order to be automatically refreshed

        public EditPlan()
        {
            InitializeComponent();
        }

        //constructor for creating a new plan
        public EditPlan(ListBox lb)
        {
            InitializeComponent();
            edit_mode = false;
            this.Text = "Add a new Plan";

            mylistbox = lb as ListBox;
        }

        //constructor for editing an existing plan
        public EditPlan(DataLayer.Plan myplan, ListBox lb)
        {
            InitializeComponent();

            plan = new DataLayer.Plan();
            this.plan = myplan;

            txtName.Text = plan.Name;
            txtDuration.Text = plan.Duration.ToString();
            txtPrice.Text = plan.Price.ToString();
            if (plan.Notes != null)
                txtNotes.Text = plan.Notes.ToString();
            this.Text = "Edit Plan '" + plan.Name + "'";

            mylistbox = lb as ListBox;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            // name, price and duration cannot be empty
            if (txtName.Text.Trim() != "" && txtPrice.Text.Trim() != "" && txtDuration.Text.Trim() != "")
            {
                if (edit_mode)
                {
                    // update plan
                    plan.Name = txtName.Text;
                    plan.Duration = Int32.Parse(txtDuration.Text);
                    plan.Price = Decimal.Parse(txtPrice.Text);
                    plan.Notes = txtNotes.Text;

                    if (DataLayer.Plan.UpdatePlan(plan) > 0)
                    {
                        MessageBox.Show("Plan updated!", "Gym Manager Pro", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // refresh the listbox
                        this.mylistbox.DataSource = DataLayer.Plan.GetAllPlans().ToList();
                        this.mylistbox.ValueMember = "Key";
                        this.mylistbox.DisplayMember = "Value";
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
                    plan.Notes = txtNotes.Text;

                    // insert new plan into database              
                    if (DataLayer.Plan.CreateNewPlan(plan) > 0)
                    {
                        MessageBox.Show("Plan created!", "Gym Manager Pro", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // refresh the listbox
                        this.mylistbox.DataSource = DataLayer.Plan.GetAllPlans().ToList();
                        this.mylistbox.ValueMember = "Key";
                        this.mylistbox.DisplayMember = "Value";
                    }
                    else
                    {
                        MessageBox.Show("Failed create a new plan. Please try again", "Gym Manager Pro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                this.Close();
            }
            else
            {
                MessageBox.Show("Name, price and duration cannot be empty!", "Gym Manager Pro", MessageBoxButtons.OK);
            }
        }

        private void EditPlan_Shown(object sender, EventArgs e)
        {

        }
    }
}
