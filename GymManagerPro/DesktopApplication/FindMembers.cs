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
            BindingSource bSource = new BindingSource();
            bSource.DataSource = DataLayer.Members.GetAllMembers();
            membersDataGridView.DataSource = bSource;
        }

        private void membersDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //gets the value of the first cell
            int val = int.Parse(((DataGridView)sender).Rows[e.RowIndex].Cells[0].Value.ToString());
            //MessageBox.Show(val.ToString());
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
