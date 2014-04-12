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
    public partial class TrainerAssignmentDialog : Form
    {
        int trainerID;

        public TrainerAssignmentDialog()
        {
            InitializeComponent();
        }

        public TrainerAssignmentDialog(int id)
        {
            InitializeComponent();
            trainerID = id;
        }

        private void TrainerAssignmentDialog_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = DataLayer.Members.GetAllMembers();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            int memberid = (int)dataGridView1.SelectedCells[0].Value;
            if (DataLayer.Trainers.AssignTrainerToMember(this.trainerID, memberid) > 0)
            {
                MessageBox.Show("Member has been added successfully!", "Gym Manager Pro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Failed to add member. Please try again", "Gym Manager Pro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }
    }
}
