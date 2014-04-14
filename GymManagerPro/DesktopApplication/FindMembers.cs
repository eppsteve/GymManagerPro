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

        private void btnAddMember_Click(object sender, EventArgs e)
        {
            // show AddNewMember window
            AddNewMember addmember = new AddNewMember();
            addmember.Show();
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
    }
}
