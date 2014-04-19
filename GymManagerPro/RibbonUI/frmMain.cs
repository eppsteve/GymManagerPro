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
        int member_id = 0;

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

        // sets up auto-complete search box
        private void SetUpSearch()
        {
            txtMembersSearch.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtMembersSearch.AutoCompleteSource = AutoCompleteSource.CustomSource;
            AutoCompleteStringCollection coll = new AutoCompleteStringCollection();
            coll.AddRange(DataLayer.Members.AutoCompleteSearch().ToArray());
            txtMembersSearch.AutoCompleteCustomSource = coll;
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

            // get all plans and bind them to listbox, in Plans panel
            listBoxPlans.DataSource = DataLayer.Plan.GetAllPlans().ToList();
            listBoxPlans.ValueMember = "Key";
            listBoxPlans.DisplayMember = "Value"; 

            // get check ins
            SetUpAttedance();

            // set up autocomplete members search box in ribbon
            SetUpSearch();

            // fill combobox with all plans, in ribbon find tab
            SortedDictionary<int, string> plans = new SortedDictionary<int, string>(DataLayer.Plan.GetAllPlans());           // get all the plans and put them to a sorted dictionary
            plans.Add(0, "All");                                                                                            // add 'All' entry to dictionary
            cbFindPlan.DataSource = new BindingSource(plans, null);                                                             // bind dictionary to combobox
            cbFindPlan.DisplayMember = "Value";                                                                                 // name of the plan
            cbFindPlan.ValueMember = "Key";                                                                                 // id of the plan
            
            // fill personal trainer combobox with all trainers, in ribbon find tab
            SortedDictionary<int, string> trainers = new SortedDictionary<int, string>(DataLayer.Trainers.GetAllTrainers());  // get id and name of all trainers and put them to a sorted dictionary
            trainers.Add(0, "All");                                                                 // add 'All' entry
            cbFindPersonalTrainer.DataSource = new BindingSource(trainers, null);                       // bind dictionary to combobox
            cbFindPersonalTrainer.DisplayMember = "Value";                                              // name of the trainer
            cbFindPersonalTrainer.ValueMember = "Key";                                                  // id of the trainer



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
            member_id = int.Parse(((DataGridView)sender).Rows[e.RowIndex].Cells[0].Value.ToString());

            // load member
            LoadMember(member_id);

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

        private void txtFindLastName_KeyDown(object sender, KeyEventArgs e)
        {
            // filter datagridview data by last name
            DataView dv = new DataView(dataset);
            dv.RowFilter = string.Format("LastName LIKE '%{0}%'", txtFindLastName.Text);
            membersDataGridViewX.DataSource = dv;
        }

        private void txtFindFirstName_KeyDown(object sender, KeyEventArgs e)
        {
            // filter datagridview data by first name
            DataView dv = new DataView(dataset);
            dv.RowFilter = string.Format("FirstName LIKE '%{0}%'", txtFindFirstName.Text);
            membersDataGridViewX.DataSource = dv;
        }

        private void cbFindPlan_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (form_loaded)
            {
                // retrieve the members who have the selected plan and bind them to datagridview
                int plan_id = Int32.Parse(cbFindPlan.SelectedValue.ToString());                 // get id of the selected plan

                if (plan_id != 0)                   // if the selected plan is not 'All'
                {
                    BindingSource bSource = new BindingSource();
                    dataset = DataLayer.Members.GetMembersByPlan(plan_id);
                    bSource.DataSource = dataset;
                    membersDataGridViewX.DataSource = bSource;
                }
                else     // if the selected plan is 'ALL'
                {
                    // get all members and bind them to the datagridview
                    BindingSource bSource = new BindingSource();
                    dataset = DataLayer.Members.GetAllMembers();
                    bSource.DataSource = dataset;
                    membersDataGridViewX.DataSource = bSource;
                }

                // set personal trainer filter combobox to default value
                cbFindPersonalTrainer.SelectedIndex = 0;

                // show find members panel
                panelAttedance.Visible = false;
                panelAllMembers.Visible = true;
                panelMemberManager.Visible = false;
                panelTrainers.Visible = false;
                panelPlans.Visible = false;
            }
        }

        private void cbFindPersonalTrainer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (form_loaded)
            {
                // retrieve the members who are assigned to the the selected trainer and bind them to datagridview
                int trainer_id = Int32.Parse(cbFindPersonalTrainer.SelectedValue.ToString());                         // get id of the selected trainer

                if (trainer_id != 0)                                                           // if the selected trainer is not set to 'All'
                {
                    BindingSource bSource = new BindingSource();
                    dataset = DataLayer.Members.GetMembersByPersonalTrainer(trainer_id);
                    bSource.DataSource = dataset;
                    membersDataGridViewX.DataSource = bSource;
                }
                else
                {
                    // get all members and bind them to the datagridview
                    BindingSource bSource = new BindingSource();
                    dataset = DataLayer.Members.GetAllMembers();
                    bSource.DataSource = dataset;
                    membersDataGridViewX.DataSource = bSource;
                }

                // set plan filter combobox to default value
                cbFindPlan.SelectedIndex = 0;

                // show find members panel
                panelAttedance.Visible = false;
                panelAllMembers.Visible = true;
                panelMemberManager.Visible = false;
                panelTrainers.Visible = false;
                panelPlans.Visible = false;
            }
        }

        private void btnFindRefresh_Click(object sender, EventArgs e)
        {
            // get all members and bind them to the members datagridview to reload
            BindingSource bSource = new BindingSource();
            dataset = DataLayer.Members.GetAllMembers();
            bSource.DataSource = dataset;
            membersDataGridViewX.DataSource = bSource;

            //set comboboxes to default value
            cbFindPersonalTrainer.SelectedIndex = 0;
            cbFindPlan.SelectedIndex = 0;
        }

        private void btnMembersNext_Click(object sender, EventArgs e)
        {
            // Retrieves and displays the next member (if any)
            if (DataLayer.Members.MemberHasNext(member_id))
            {
                member_id = DataLayer.Members.GetNextMember(member_id);
                LoadMember(member_id);
            }
            else
            {
                MessageBox.Show("There are no more members!", "Gym Manager Pro", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnMembersPrev_Click(object sender, EventArgs e)
        {
            if (DataLayer.Members.MemberHasPrevious(member_id))
            {
                member_id = DataLayer.Members.GetPrevMember(member_id);
                LoadMember(member_id);
            }
            else
            {
                MessageBox.Show("There are no more members!", "Gym Manager Pro", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnMembersSearch_Click(object sender, EventArgs e)
        {
            // Displays the selected member from the search box
            LoadMember(DataLayer.Members.QuickSearch(txtMembersSearch.Text));
        }

        private void btnCheckIn_Click(object sender, EventArgs e)
        {
            if (member_id != 0)             // if a member has been selected
            {
                if (DataLayer.Members.MemberCheckin(member_id) > 0)
                {
                    MessageBox.Show(lblName.Text + " just Checked-in!", "Gym Manager Pro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Console.Beep();
                }
                else
                {
                    MessageBox.Show("Couldnot check-in. Please try again", "Gym Manager Pro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please select a user first!", "Gym Manager Pro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnAttedanceRefresh_Click(object sender, EventArgs e)
        {
            // get check ins again to refresh
            SetUpAttedance();
        }

        private void btnMembersDelete_Click(object sender, EventArgs e)
        {
            if (panelMemberManager.Visible)
            {
                // Deletes the current member
                DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete this member? All related data will be lost!!!", "Gym Manager Pro", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dialogResult == DialogResult.Yes)
                {
                    DataLayer.Members.DeleteMember(member_id);
                    MessageBox.Show("Member deleted!", "Gym Manager Pro", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // display the previous member
                    member_id = DataLayer.Members.GetPrevMember(member_id);
                    LoadMember(member_id);
                }
            }
        }

        private void btnMembersSave_Click(object sender, EventArgs e)
        {
            // create a new member and save its details to the db
            DataLayer.Members members = new DataLayer.Members();
            DataLayer.Member member = new DataLayer.Member();

            if (!String.IsNullOrEmpty(txtLastName.Text))
            {
                // fill the properties of member object based on textboxes text
                member.MemberID = this.member_id;
                member.CardNumber = Int32.Parse(txtCardNumber.Text);
                member.LName = txtLastName.Text;
                member.FName = txtFirstName.Text;
                if (rbMale.Checked)
                {
                    member.Sex = "male";
                }
                else if (rbFemale.Checked)
                {
                    member.Sex = "female";
                }
                member.DateOfBirth = txtDateOfBirth.Value;
                member.Street = txtStreet.Text;
                member.Suburb = txtSuburb.Text;
                member.City = txtCity.Text;
                if (txtPostalCode.Text.Length > 0)
                {
                    member.PostalCode = Int32.Parse(txtPostalCode.Text);
                }
                else
                {
                    member.PostalCode = 0;
                }
                member.HomePhone = txtHomePhone.Text;
                member.CellPhone = txtCellPhone.Text;
                member.Email = txtEmail.Text;
                member.Occupation = txtOccupation.Text;
                member.Notes = txtNotes.Text;

                // holds the member's picture
                //byte[] imageBt = null;
                if (pictureBox1.ImageLocation != null)
                {
                    FileStream fstream = new FileStream(pictureBox1.ImageLocation, FileMode.Open, FileAccess.Read);
                    BinaryReader br = new BinaryReader(fstream);
                    member.Image = br.ReadBytes((int)fstream.Length);
                }
                else
                {
                    byte[] empty_array = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 };
                    member.Image = empty_array;
                }


                if (DataLayer.Members.UpdateMember(member) > 0)
                {
                    MessageBox.Show("Member Updated successfully!", "Gym Manager Pro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Failed to Update Member!", "Gym Manager Pro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                // set textboxes to readonly
                txtLastName.ReadOnly = true;
                txtFirstName.ReadOnly = true;
                txtHomePhone.ReadOnly = true;
                txtStreet.ReadOnly = true;
                txtSuburb.ReadOnly = true;
                txtCity.ReadOnly = true;
                txtCellPhone.ReadOnly = true;
                txtOccupation.ReadOnly = true;
                txtEmail.ReadOnly = true;

                btnMembersEdit.Text = "Edit";
            }
            else
            {
                MessageBox.Show("Last Name cannot be empty!", "Gym Manager Pro", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnMembersEdit_Click(object sender, EventArgs e)
        {
            txtLastName.ReadOnly = false;
            txtFirstName.ReadOnly = false;
            txtHomePhone.ReadOnly = false;
            txtStreet.ReadOnly = false;
            txtSuburb.ReadOnly = false;
            txtCity.ReadOnly = false;
            txtCellPhone.ReadOnly = false;
            txtOccupation.ReadOnly = false;
            txtEmail.ReadOnly = false;

            btnMembersEdit.Text = "Cancel";
        }

    }
}
