using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace GymManagerPro.RibbonUI
{
    public partial class frmMain : Form
    {
        DataTable dataset;
        bool form_loaded;
        int trainer_id;

        DataLayer.Plan plan;

        public frmMain()
        {
            InitializeComponent();
        }

        // loads data for the specified member
        public void LoadMember(int id)
        {
            DataLayer.Members members = new DataLayer.Members();
            DataLayer.Member member = new DataLayer.Member();

            try
            {
                // retrieves member data from db
                member = members.GetMember(id);

                // populate controls with the data  
                txtCardNumber.Text = member.CardNumber.ToString();
                txtCardNumber2.Text = member.CardNumber.ToString();
                txtLastName.Text = member.LName;
                txtFirstName.Text = member.FName;

                if (member.Sex == "male")
                {
                    rbMale.Checked = true;
                }
                else if (member.Sex == "female")
                {
                    rbFemale.Checked = true;
                }

                txtDateOfBirth.Value = member.DateOfBirth;
                txtStreet.Text = member.Street;
                txtSuburb.Text = member.Suburb;
                txtCity.Text = member.City;
                txtPostalCode.Text = member.PostalCode.ToString();
                txtHomePhone.Text = member.HomePhone;
                txtCellPhone.Text = member.CellPhone;
                txtEmail.Text = member.Email;
                txtOccupation.Text = member.Occupation;
                txtNotes.Text = member.Notes;
                //txtPersonalTrainer.Text = member.PersonalTrainer;
                lblName.Text = member.FName + " " + member.LName;
                txtMemberId.Text = id.ToString();

                //display the member's picture
                pictureBox1.Image = null; // clears the picturebox
                byte[] img = member.Image;
                if (member.Image != null)
                {
                    try
                    {
                        MemoryStream mstream = new MemoryStream(img);
                        pictureBox1.Image = Image.FromStream(mstream);
                    }
                    catch { }
                }

                //load membership data
                DataTable table = DataLayer.Memberships.GetMembershipByMemberId(id);
                //dataGridView1.DataSource = table;
                //resetTextBoxes();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        // populates listbox with the names of all the trainers
        public void LoadAllTrainerNames()
        {
            listBoxTrainers.DataSource = new BindingSource(DataLayer.Trainers.GetAllTrainers(), null);
            listBoxTrainers.DisplayMember = "Value";
            listBoxTrainers.ValueMember = "Key";
        }

        // loads data for the specified trainer
        public void LoadTrainer(int id)
        {
            DataLayer.Trainer trainer = new DataLayer.Trainer();
            try
            {
                // display trainer details
                trainer = DataLayer.Trainers.GetTrainer(id);
                SetUpTrainerTextBoxes(trainer);

                // display associated members
                DataTable membersTable = DataLayer.Trainers.GetAssociatedMembers(id);
                amTrainersDataGridViewX.DataSource = membersTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        // populates textboxes with trainer's data
        public void SetUpTrainerTextBoxes(DataLayer.Trainer trainer)
        {
            txtTrainerFName.Text = trainer.FName;
            txtTrainerLName.Text = trainer.LName;
            txtTrainerCellPhone.Text = trainer.CellPhone;
            txtTrainerCity.Text = trainer.City;
            dtpTrainerDOB.Value = trainer.DateOfBirth.Date;
            txtTrainerEmail.Text = trainer.Email;
            txtTrainerHomePhone.Text = trainer.HomePhone;
            //txtId.Text = id.ToString();
            txtTrainerNotes.Text = trainer.Notes;
            if (trainer.Sex == "Male")
                rbTrainerMale.Checked = true;
            else if (trainer.Sex == "Female")
                rbTrainerFemale.Checked = true;
            //txtPostalCode.Text = trainer.PostalCode.ToString();
            txtTrainerSalary.Text = trainer.Salary.ToString();
            txtTrainerSuburb.Text = trainer.Suburb;
            //label15.Text = txtTrainerFName.Text + " " + txtTrainerLName.Text + " is set as a Personal Trainer for the following members.";

            //display the trainer's picture
            //pictureBox1.Image = null; // clears the picturebox
            //byte[] img = trainer.Image;
            //if (trainer.Image != null)
            //{
            //    try
            //    {
            //        MemoryStream mstream = new MemoryStream(img);
            //        pictureBox1.Image = Image.FromStream(mstream);
            //    }
            //    catch { }
            //}
        }

        // populates richTextBox Attedance with checkins data
        public void SetUpAttedance()
        {
            //get data from db
            richTextBoxAttedance.Text = DataLayer.Members.GetAttedance().ToString();
            // format textbox
            string active = "Active - Entrance allowed";
            string inactive = "Inactive - Entrance denied";
            Utility.HighlightText(richTextBoxAttedance, active, Color.Green);
            Utility.HighlightText(richTextBoxAttedance, inactive, Color.Red);
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            // get all members and bind them to the members datagridview
            BindingSource bSource = new BindingSource();
            dataset = DataLayer.Members.GetAllMembers();
            bSource.DataSource = dataset;
            membersDataGridViewX.DataSource = bSource;

            //load trainers
            LoadAllTrainerNames();

            // get all plans and bind them to listbox
            listBoxPlans.DataSource = DataLayer.Plan.GetAllPlans().ToList();
            listBoxPlans.ValueMember = "Key";
            listBoxPlans.DisplayMember = "Value"; 

            // get check ins
            SetUpAttedance();

            //hide all panels
            panelMemberManager.Visible = false;
            panelAllMembers.Visible = false;
            panelTrainers.Visible = false;
            panelTrainers.Visible = false;
            panelPlans.Visible = false;
            panelAttedance.Visible = false;

            //switch to Find ribbon tab
            ribbonTabFind.Select();
        }


        private void membersDataGridViewX_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // get member's id from selected row
            int id = int.Parse(((DataGridView)sender).Rows[e.RowIndex].Cells[0].Value.ToString());

            // load member
            LoadMember(id);

            // show member manager panel and hide all other panels
            panelAllMembers.Visible = false;
            panelMemberManager.Visible = true;

            //switch to ribbon tab Members
            ribbonTabMembers.Select();
        }

        private void listBoxTrainers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (form_loaded)
            {
                if (listBoxTrainers.SelectedIndex != -1)
                {
                    //get trainer's id and load trainer's data
                    trainer_id = Convert.ToInt32(listBoxTrainers.SelectedValue.ToString());
                    LoadTrainer(trainer_id);
                }
            }
        }

        private void frmMain_Shown(object sender, EventArgs e)
        {
            form_loaded = true;
        }

        private void listBoxPlans_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (form_loaded)
            {
                if (listBoxPlans.SelectedIndex != -1)
                {
                    // get selected plan
                    int planId = Convert.ToInt32(listBoxPlans.SelectedValue.ToString());
                    plan = DataLayer.Plan.GetPlan(planId);
                    //populate textboxes with plan's data
                    txtPlanName.Text = plan.Name;
                    txtPlanDuration.Text = plan.Duration.ToString();
                    txtPlanPrice.Text = plan.Price.ToString();
                }
            }
        }

        private void btnViewAllMembers_Click(object sender, EventArgs e)
        {
            panelAllMembers.Visible = true;
            panelMemberManager.Visible = false;
            panelTrainers.Visible = false;
            panelPlans.Visible = false;
            panelAttedance.Visible = false;
        }

        private void btnViewCheckins_Click(object sender, EventArgs e)
        {
            panelAttedance.Visible = true;
            panelAllMembers.Visible = false;
            panelMemberManager.Visible = false;
            panelTrainers.Visible = false;
            panelPlans.Visible = false;
        }

        private void btnViewTrainers_Click(object sender, EventArgs e)
        {
            panelTrainers.Visible = true;
            panelPlans.Visible = false;
            panelAllMembers.Visible = false;
            panelMemberManager.Visible = false;
            panelAttedance.Visible = false;
        }

        private void btnViewPlans_Click(object sender, EventArgs e)
        {
            panelPlans.Visible = true;
            panelAllMembers.Visible = false;
            panelMemberManager.Visible = false;
            panelTrainers.Visible = false;
            panelAttedance.Visible = false;
        }

        private void btnTrainersAddMember_Click(object sender, EventArgs e)
        {
            if (form_loaded)
            {
                // show trainers panel
                panelPlans.Visible = false;
                panelAllMembers.Visible = false;
                panelMemberManager.Visible = false;
                panelTrainers.Visible = true;
                panelAttedance.Visible = false;

                if (listBoxTrainers.SelectedIndex != -1)
                {
                    TrainerAssignmentDialog tad = new TrainerAssignmentDialog(trainer_id);
                    tad.Show();
                }
                else
                {
                    MessageBox.Show("Please select a trainer first!", "Gym Manager Pro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void btnTrainersRefresh_Click(object sender, EventArgs e)
        {
            // load again to refresh
            LoadAllTrainerNames();
        }

    }
}
