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
    public partial class FindMembers : Form
    {
        bool form_loaded = false;        // set to true when the form has been loaded using shown event. This is used to avoid errors with the initialization of combobox values

        public FindMembers()
        {
            InitializeComponent();
        }

        private void FindMembers_Load(object sender, EventArgs e)
        {
            // get all members and bind them to the datagridview
            BindingSource bSource = new BindingSource();
            bSource.DataSource = DataLayer.Members.GetAllMembers();
            membersDataGridView.DataSource = bSource;

            // fill plan combobox with all plans
            SortedDictionary<int, string> plans = new SortedDictionary<int,string>(DataLayer.Plan.GetAllPlans());           // get all the plans and put them to a sorted dictionary
            plans.Add(0, "All");                                                                                            // add 'All' entry to dictionary
            cbPlan.DataSource = new BindingSource(plans, null);                                                             // bind dictionary to combobox
            cbPlan.DisplayMember = "Value";                                                                                 // name of the plan
            cbPlan.ValueMember = "Key";                                                                                     // id of the plan
            
            // fill personal trainer combobox with all trainers
            SortedDictionary<int, string> trainers = new SortedDictionary<int,string>(DataLayer.Trainers.GetAllTrainers());  // get id and name of all trainers and put them to a sorted dictionary
            trainers.Add(0, "All");                                                                 // add 'All' entry
            cbPersonalTrainer.DataSource = new BindingSource(trainers, null);                       // bind dictionary to combobox
            cbPersonalTrainer.DisplayMember = "Value";                                              // name of the trainer
            cbPersonalTrainer.ValueMember = "Key";                                                  // id of the trainer
        }

        private void membersDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            
            int val = int.Parse(((DataGridView)sender).Rows[e.RowIndex].Cells[0].Value.ToString());         // get the value of the first cell, which is the member's id
            
            // open member's data in member manager
            MemberManager mm = new MemberManager(val);                                                     
            mm.MdiParent = this.MdiParent;
            mm.Show();
        }



        private void btnShowDetails_Click(object sender, EventArgs e)
        {
            // if a row is selected
            if (membersDataGridView.SelectedCells.Count > 0)
            {
                // get the selected member id
                int val = int.Parse(membersDataGridView.SelectedRows[0].Cells[0].Value.ToString());
                
                // open selected member in member manager
                MemberManager manager = new MemberManager(val);
                manager.MdiParent = this.MdiParent;
                manager.Show();
            }
        }

        private void btnCheckin_Click(object sender, EventArgs e)
        {
            //get id of the selected member
            int id = int.Parse(membersDataGridView.SelectedRows[0].Cells[0].Value.ToString());

            if (DataLayer.Members.MemberCheckin(id) > 0)
            {
                MessageBox.Show("User just Checked-in!", "Gym Manager Pro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Console.Beep();
            }
            else
            {
                MessageBox.Show("Couldnot check-in. Please try again.", "Gym Manager Pro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cbPlan_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (form_loaded)
            {
                // retrieve the members who have the selected plan and bind them to datagridview
                int plan_id = Int32.Parse(cbPlan.SelectedValue.ToString());                         // get id of the selected plan

                if (plan_id != 0)                                                           // if the selected plan is not 'All'
                {
                    BindingSource bSource = new BindingSource();
                    bSource.DataSource = DataLayer.Members.GetMembersByPlan(plan_id);
                    membersDataGridView.DataSource = bSource;
                }
                else 
                {
                    // get all members and bind them to the datagridview
                    BindingSource bSource = new BindingSource();
                    bSource.DataSource = DataLayer.Members.GetAllMembers();
                    membersDataGridView.DataSource = bSource;
                }
            }
        }

        private void FindMembers_Shown(object sender, EventArgs e)
        {
            form_loaded = true;
        }

        private void cbPersonalTrainer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (form_loaded)
            {
                // retrieve the members who are assigned to the the selected trainer and bind them to datagridview
                int trainer_id = Int32.Parse(cbPersonalTrainer.SelectedValue.ToString());                         // get id of the selected trainer

                if (trainer_id != 0)                                                           // if the selected trainer is not set to 'All'
                {
                    BindingSource bSource = new BindingSource();
                    bSource.DataSource = DataLayer.Members.GetMembersByPersonalTrainer(trainer_id);
                    membersDataGridView.DataSource = bSource;
                }
                else
                {
                    // get all members and bind them to the datagridview
                    BindingSource bSource = new BindingSource();
                    bSource.DataSource = DataLayer.Members.GetAllMembers();
                    membersDataGridView.DataSource = bSource;
                }
            }
        }
    }
}
