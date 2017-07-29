using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using GymManagerPro.Presenter;
using DevComponents.DotNetBar.Controls;

namespace GymManagerPro.View
{
    public partial class frmMain : Form, IMember, ITrainer
    {
        DataTable dataset;
        bool form_loaded;
        int trainer_id;
        int member_id = 0;
        DataLayer.Plan plan;

        public MemberPresenter Presenter { get; set; }
        public TrainerPresenter TPresenter { get; set; }

        #region MemberPresenter properties
        /// <summary>
        /// FindMember Properties
        /// </summary>
        string IMember.FFirstName
        {
            get { return txtFindFirstName.Text; }
            set { txtFindFirstName.Text = value; }
        }
        string IMember.FLastName
        {
            get { return txtFindLastName.Text; }
            set { txtFindLastName.Text = value; }
        }
        public int SelectedPlanIndex
        {
            get { return cbFindPlan.SelectedIndex; }
            set { cbFindPlan.SelectedIndex = value; }
        }
        public int SelectedPersonalTrainerIndex
        {
            get { return cbFindPersonalTrainer.SelectedIndex; }
            set { cbFindPersonalTrainer.SelectedIndex = value; }
        }
        public int SelectedSearchByIndex
        {
            get { return cbFindSearchBy.SelectedIndex; }
            set { cbFindSearchBy.SelectedIndex = value; }
        }
        public string SearchBy
        {
            get { return cbFindSearchBy.SelectedItem.ToString(); }
        }
        public string Keyword
        {
            get { return txtFindSearch.Text; }
            set { txtFindSearch.Text = value; }
        }
        object IMember.MembersGridDataSource
        {
            get { return membersDataGridViewX.DataSource; }
            set { membersDataGridViewX.DataSource = value; }
        }
        SaveFileDialog IMember.ExportFileDialog
        {
            get { return saveFileDialog1; }
        }
        public DataGridViewColumnCollection MembersGridColumns
        {
            get { return membersDataGridViewX.Columns; }
        }
        public DataGridViewRowCollection MembersGridRows
        {
            get { return membersDataGridViewX.Rows; }
        }

        /// <summary>
        /// MemberManager Properties
        /// </summary>
        public int SelectedMember
        {
            get { return int.Parse(membersDataGridViewX.SelectedRows[0].Cells["id"].Value.ToString()); }
            set
            {   //select the row which has the specified member id
                membersDataGridViewX.Rows.OfType<DataGridViewRow>()
                    .Where(x => (int)x.Cells["Id"].Value == value)
                    .ToArray<DataGridViewRow>()[0].Selected = true;
            }
        }
        int IMember.MemberId
        {
            get { return Int32.Parse(txtMemberId.Text); }
            set { txtMemberId.Text = value.ToString(); }
        }
        string IMember.FirstName
        {
            get { return txtFirstName.Text; }
            set
            {
                txtFirstName.Text = value;
                lblName.Text = value + " " + LastName;
            }
        }
        string IMember.LastName
        {
            get { return txtLastName.Text; }
            set
            {
                txtLastName.Text = value;
                lblName.Text = FirstName + " " + value;
            }
        }
        int IMember.CardNumber
        {
            get { return Int32.Parse(txtCardNumber.Text); }
            set { txtCardNumber.Text = value.ToString(); }
        }
        public bool Male_IsSelected
        {
            get { return rbMale.Checked; }
            set { rbMale.Checked = value; }
        }
        public bool Female_IsSelected
        {
            get { return rbFemale.Checked; }
            set { rbFemale.Checked = value; }
        }
        public DateTime DateOfBirth
        {
            get { return txtDateOfBirth.Value; }
            set { txtDateOfBirth.Value = value; }
        }
        public string Street
        {
            get { return txtStreet.Text; }
            set { txtStreet.Text = value; }
        }
        public string Suburb
        {
            get { return txtSuburb.Text; }
            set { txtSuburb.Text = value; }
        }
        public string City
        {
            get { return txtCity.Text; }
            set { txtCity.Text = value; }
        }
        public int PostalCode
        {
            get { return Int32.Parse(txtPostalCode.Text); }
            set { txtPostalCode.Text = value.ToString(); }
        }
        string IMember.HomePhone
        {
            get { return txtHomePhone.Text; }
            set { txtHomePhone.Text = value; }
        }
        string IMember.CellPhone
        {
            get { return txtCellPhone.Text; }
            set { txtCellPhone.Text = value; }
        }
        string IMember.Email
        {
            get { return txtEmail.Text; }
            set { txtEmail.Text = value; }
        }
        public string Occupation
        {
            get { return txtOccupation.Text; }
            set { txtOccupation.Text = value; }
        }
        public string Notes
        {
            get { return txtNotes.Text; }
            set { txtNotes.Text = value; }
        }
        object IMember.MembershipsGridDataSource
        {
            get { return dataGridViewMemberships.DataSource; }
            set { dataGridViewMemberships.DataSource = value; }
        }
        DataGridViewRowCollection IMember.MDGVRows
        {
            get { return dataGridViewMemberships.Rows; }
        }
        public int PersonalTrainer
        {
            get { return Int32.Parse(cbPersonalTrainer.SelectedValue.ToString()); }
            set { cbPersonalTrainer.SelectedValue = value; }
        }
        public Image MemberImage
        {
            get { return pictureBoxMemberManager.Image; }
            set { pictureBoxMemberManager.Image = value; }
        }
        public string MemberImageLocation
        {
            get { return pictureBoxMemberManager.ImageLocation; }
            set { pictureBoxMemberManager.ImageLocation = value; }
        }
        public bool IsMemberPanelVisible
        {
            get { return panelMemberManager.Visible; }
            set { panelMemberManager.Visible = value; }
        }
        public bool IsAllMembersPanelVisible
        {
            get { return panelAllMembers.Visible; }
            set { panelAllMembers.Visible = value; }
        }
        public int SelectedRowsCount
        {
            get { return membersDataGridViewX.SelectedRows.Count; }
        }
        public int SelectedMembership
        {
            get { return (int)dataGridViewMemberships.SelectedRows[0].Cells["Membership Id"].Value; }
        }

        /// <summary>
        /// Trainer Properties
        /// </summary>
        public int TrainerID
        {
            get { return Int32.Parse(txtTrainerId.Text); }
            set { txtTrainerId.Text = value.ToString(); }
        }
        string ITrainer.FirstName
        {
            get { return txtTrainerFName.Text; }
            set { txtTrainerFName.Text = value; }
        }
        string ITrainer.LastName
        {
            get { return txtTrainerLName.Text; }
            set { txtTrainerFName.Text = value; }
        }        
        string ITrainer.HomePhone
        {
            get { return txtHomePhone.Text; }
            set { txtHomePhone.Text = value; }
        }
        string ITrainer.CellPhone
        {
            get { return txtCellPhone.Text; }
            set { txtCellPhone.Text = value; }
        }
        string ITrainer.Email
        {
            get { return txtEmail.Text; }
            set { txtEmail.Text = value; }
        }
        public decimal Salary
        {
            get { return Decimal.Parse(txtTrainerSalary.Text.ToString()); }
            set { txtTrainerSalary.Text = value.ToString(); }
        }
        public ListBox lbTrainers
        {
            get { return listBoxTrainers; }
            set { listBoxTrainers = value; }
        }
        public DataGridViewX dgLinkedMembers
        {
            get { return amTrainersDataGridViewX; }
            set { amTrainersDataGridViewX = value;}
        }
        public bool IsPanelVisible
        {
            get { return panelTrainers.Visible; }
            set { panelTrainers.Visible = value; }
        }
        #endregion

        public frmMain()
        {
            InitializeComponent();
            Presenter = new MemberPresenter(this);
            TPresenter = new TrainerPresenter(this);
        }



        // populates richTextBox Attedance with checkins data
        public void SetUpAttedance()
        {
            //get data from db
            richTextBoxAttedance.Text = DataLayer.Members.GetAttedance().ToString();
            // format textbox
            string active = "Active - Entrance allowed";
            string inactive = "Inactive - Entrance denied";
            Util.Common.HighlightText(richTextBoxAttedance, active, Color.Green);
            Util.Common.HighlightText(richTextBoxAttedance, inactive, Color.Red);
        }

        // sets up auto-complete search boxes
        private void SetUpSearch()
        {
            txtMembersSearch.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtMembersSearch.AutoCompleteSource = AutoCompleteSource.CustomSource;
            AutoCompleteStringCollection coll = new AutoCompleteStringCollection();
            coll.AddRange(DataLayer.Members.AutoCompleteSearch().ToArray());
            txtMembersSearch.AutoCompleteCustomSource = coll;

            txtAttedanceSearch.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtAttedanceSearch.AutoCompleteSource = AutoCompleteSource.CustomSource;
            AutoCompleteStringCollection coll2 = new AutoCompleteStringCollection();
            coll2.AddRange(DataLayer.Members.AutoCompleteMemberIdSearch().ToArray());
            txtAttedanceSearch.AutoCompleteCustomSource = coll2;
        }

        private void SetUpComboboxes()
        {
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

            // fill personal trainer combobox with trainers in member manager
            trainers.Remove(0);                                                                 // remove 'All' entry
            cbPersonalTrainer.DataSource = new BindingSource(trainers, null);                       // bind dictionary to combobox
            cbPersonalTrainer.DisplayMember = "Value";                                              // name of the trainer
            cbPersonalTrainer.ValueMember = "Key";

            // fill combobox with all plans, in new member wizard
            plans.Remove(0);                                                    //remove 'All' option
            plans.Add(0, "None");                                               // add 'None' option 
            cbWizardPlans.DataSource = new BindingSource(plans, null);
            cbWizardPlans.DisplayMember = "Value";
            cbWizardPlans.ValueMember = "Key";
        }

        // displays notifications in member manager
        private void SetUpNotifications()
        {
            lblNotifications.Text = "";                                         // clear
            foreach (DataGridViewRow row in dataGridViewMemberships.Rows)
            {
                DateTime exp_date = (DateTime)row.Cells["End Date"].Value;      // get end date
                String membership = row.Cells["Name"].Value.ToString();         // get membership name
                double days = (exp_date - DateTime.Today).TotalDays;            // calculate the difference between today and end date
                if (days > 0)
                    // membership is active
                    lblNotifications.Text += membership + " expires in " + (int)days + " days" + Environment.NewLine;
                else
                    // membership has expired
                    lblNotifications.Text += membership + " has expired!" + Environment.NewLine;
            }
        }

        // reloads data to AllMembers datagridview to refresh
        private void RefreshAllMembersDataGrid()
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

        // shows the specified panel
        private void SwitchToPanel(Panel panel)
        {
            // hide all panels
            panelPlans.Visible = false;
            panelAllMembers.Visible = false;
            panelMemberManager.Visible = false;
            panelTrainers.Visible = false;
            panelAttedance.Visible = false;
            panelNewMemberWizard.Visible = false;
            panelReports.Visible = false;

            panel.Visible = true;
        }

        // sets the textboxes as editables for editing trainer details
        private void EditTrainer()
        {
            if (panelTrainers.Visible)
            {
                if (btnTrainersEdit.Text == "Edit")
                {
                    txtTrainerFName.ReadOnly = false;
                    txtTrainerLName.ReadOnly = false;
                    txtTrainerCellPhone.ReadOnly = false;
                    txtTrainerCity.ReadOnly = false;
                    txtTrainerEmail.ReadOnly = false;
                    txtTrainerHomePhone.ReadOnly = false;
                    txtTrainerNotes.ReadOnly = false;
                    txtTrainerSalary.ReadOnly = false;
                    txtTrainerStreet.ReadOnly = false;
                    txtTrainerSuburb.ReadOnly = false;
                    dtpTrainerDOB.Enabled = true;

                    btnTrainersEdit.Text = "Cancel";
                    btnTrainersEdit.Icon = null;
                    btnTrainersEdit.Tooltip = "Cancel editing";
                }
                else if (btnTrainersEdit.Text == "Cancel")
                {
                    txtTrainerFName.ReadOnly = true;
                    txtTrainerLName.ReadOnly = true;
                    txtTrainerCellPhone.ReadOnly = true;
                    txtTrainerCity.ReadOnly = true;
                    txtTrainerEmail.ReadOnly = true;
                    txtTrainerHomePhone.ReadOnly = true;
                    txtTrainerNotes.ReadOnly = true;
                    txtTrainerSalary.ReadOnly = true;
                    txtTrainerStreet.ReadOnly = true;
                    txtTrainerSuburb.ReadOnly = true;
                    dtpTrainerDOB.Enabled = false;

                    //change button text and icon
                    btnTrainersEdit.Text = "Edit";
                    ComponentResourceManager resources = new ComponentResourceManager(typeof(frmMain));
                    btnTrainersEdit.Icon = ((System.Drawing.Icon)(resources.GetObject("btnTrainersEdit.Icon")));
                }
            }
            else
            {
                MessageBox.Show("Please select a trainer first!");
                SwitchToPanel(panelTrainers);
            }
        }

        // sets controls in member manager as read only
        private void DoNotAllowMemberEdit()
        {
            txtLastName.ReadOnly = true;
            txtFirstName.ReadOnly = true;
            txtHomePhone.ReadOnly = true;
            txtStreet.ReadOnly = true;
            txtSuburb.ReadOnly = true;
            txtCity.ReadOnly = true;
            txtCellPhone.ReadOnly = true;
            txtOccupation.ReadOnly = true;
            txtNotes.ReadOnly = true;
            txtEmail.ReadOnly = true;
            txtPostalCode.ReadOnly = true;
            txtDateOfBirth.Enabled = false;
            txtCardNumber.Enabled = false;
            cbPersonalTrainer.Enabled = false;

            // change button text and icon
            btnMembersEdit.Text = "Edit";
            ComponentResourceManager resources = new ComponentResourceManager(typeof(frmMain));
            btnMembersEdit.Icon = ((System.Drawing.Icon)(resources.GetObject("btnMembersEdit.Icon")));
        }


        
        // --------------------------------- EVENTS ------------------------------------ //

        private void frmMain_Load(object sender, EventArgs e)
        {
            //load trainers
            TPresenter.LoadTrainers();

            // get all plans and bind them to listbox, in Plans panel
            listBoxPlans.DataSource = DataLayer.Plan.GetAllPlans().ToList();
            listBoxPlans.ValueMember = "Key";
            listBoxPlans.DisplayMember = "Value";

            // load check ins
            SetUpAttedance();

            // set up autocomplete members search box in ribbon
            SetUpSearch();

            SetUpComboboxes();

            SwitchToPanel(panelAllMembers);
            ribbonTabFind.Select();                                             //switch to Find ribbon tab
        }

        private void membersDataGridViewX_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Presenter.LoadMember();
        }

        private void membersDataGridViewX_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Presenter.LoadMember();

            // switch to member manager and ribbon tab Members
            SwitchToPanel(panelMemberManager);
            ribbonTabMembers.Select();
        }

        private void listBoxTrainers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (form_loaded)
                TPresenter.ChangeSelectedTrainer();
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
                    if (plan.Notes != null)
                        txtPlanNotes.Text = plan.Notes.ToString();
                    else
                        txtPlanNotes.Text = null;
                }
            }
        }

        private void btnViewAllMembers_Click(object sender, EventArgs e)
        {
            SwitchToPanel(panelAllMembers);
        }

        private void btnViewCheckins_Click(object sender, EventArgs e)
        {
            SwitchToPanel(panelAttedance);
        }

        private void btnViewTrainers_Click(object sender, EventArgs e)
        {
            SwitchToPanel(panelTrainers);
        }

        private void btnViewPlans_Click(object sender, EventArgs e)
        {
            SwitchToPanel(panelPlans);
        }

        private void btnTrainersAddMember_Click(object sender, EventArgs e)
        {
            if (form_loaded)
            {
                TPresenter.LinkMember();
            }
        }

        private void btnTrainersRefresh_Click(object sender, EventArgs e)
        {
            TPresenter.LoadTrainers();
        }

        private void cbFindPlan_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (form_loaded)
            {
                SwitchToPanel(panelAllMembers);
                Presenter.FilterByPlan();
            }
        }

        private void cbFindPersonalTrainer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (form_loaded)
            {
                SwitchToPanel(panelAllMembers);
                Presenter.FilterByPersonalTrainer();
            }
        }

        private void btnFindRefresh_Click(object sender, EventArgs e)
        {
            Presenter.Refresh();
        }

        private void btnMembersNext_Click(object sender, EventArgs e)
        {
            SwitchToPanel(panelMemberManager);
            Presenter.NextMember();
        }

        private void btnMembersPrev_Click(object sender, EventArgs e)
        {
            SwitchToPanel(panelMemberManager);
            Presenter.PrevMember();
        }

        private void btnMembersSearch_Click(object sender, EventArgs e)
        {
            // loads specified member
            SelectedMember = DataLayer.Members.QuickSearch(txtMembersSearch.Text);
            Presenter.LoadMember();
            SwitchToPanel(panelMemberManager);
        }

        private void btnCheckIn_Click(object sender, EventArgs e)
        {
            Presenter.CheckInMember();
        }

        private void btnAttedanceRefresh_Click(object sender, EventArgs e)
        {
            // get check ins again to refresh
            SetUpAttedance();
        }

        private void btnMembersDelete_Click(object sender, EventArgs e)
        {
            Presenter.DeleteMember();
        }

        private void btnMembersSave_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtLastName.Text))
            {
                Presenter.SaveMember();                
                DoNotAllowMemberEdit(); // set textboxes to readonly
            }
            else
            {
                MessageBox.Show("Last Name cannot be empty!", "Gym Manager Pro", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnMembersEdit_Click(object sender, EventArgs e)
        {
            if (panelAllMembers.Visible)
                SwitchToPanel(panelMemberManager);

            if (panelMemberManager.Visible)
            {
                if (btnMembersEdit.Text == "Edit")
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
                    txtNotes.ReadOnly = false;
                    txtPostalCode.ReadOnly = false;
                    txtDateOfBirth.Enabled = true;
                    txtCardNumber.Enabled = true;
                    cbPersonalTrainer.Enabled = true;

                    btnMembersEdit.Text = "Cancel";
                    btnMembersEdit.Icon = null;
                    btnMembersEdit.Tooltip = "Cancel editing";
                }
                else if (btnMembersEdit.Text == "Cancel")
                {
                    DoNotAllowMemberEdit();
                }
            }
            else
            {
                MessageBox.Show("Pleaase select a member first!");
                SwitchToPanel(panelAllMembers);
            }
        }

        private void buttonXNewMembership_Click(object sender, EventArgs e)
        {
            AddNewMembership newContract = new AddNewMembership(member_id, dataGridViewMemberships);
            newContract.Show();
        }

        private void buttonXDeleteMembership_Click(object sender, EventArgs e)
        {
            Presenter.DeleteMembership();
        }

        private void btnAttedanceCheckin_Click(object sender, EventArgs e)
        {
            if (txtAttedanceSearch.Text.Length > 0)
            {
                if ( DataLayer.Members.CheckIfIdExists(Int32.Parse(txtAttedanceSearch.Text)) >0 )
                {
                    if (DataLayer.Members.MemberCheckin(Int32.Parse(txtAttedanceSearch.Text)) > 0)
                    {
                        MessageBox.Show("Member just Checked-In!", "Gym Manager Pro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Failed to check-in. Please try again", "Gym Manager Pro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("This user does not exist.");
                }
            }
            else
            {
                MessageBox.Show("Please enter a member's card number first!", "Gym Manager Pro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnMembersNew_Click(object sender, EventArgs e)
        {
            SwitchToPanel(panelNewMemberWizard);
        }

        private void cbWizardPlans_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (form_loaded)
            {
                int plan_id = Int32.Parse(cbWizardPlans.SelectedValue.ToString());                                           // get id of the selected plan

                txtWizardMembershipFee.Text = DataLayer.Plan.GetPlanPrice(plan_id).ToString();                                    // get membership's cost
                dtpWizardEndPlan.Value = dtpWizardStartPlan.Value;                                                                // get membership's start date
                dtpWizardEndPlan.Value = dtpWizardEndPlan.Value.AddMonths(DataLayer.Plan.GetPlanDuration(plan_id));               // calculate membership duration
                dtpWizardEndPlan.Value = dtpWizardEndPlan.Value.AddDays(-1);                                                      // subtract one day
            }
        }

        private void txtWizardInitiationFee_TextChanged(object sender, EventArgs e)
        {
            int plan_id = Int32.Parse(cbWizardPlans.SelectedValue.ToString());                                           // get id of the selected plan
            decimal programmefee = DataLayer.Plan.GetPlanPrice(plan_id);                                                // get selected plan's price
            try
            {
                if (txtWizardInitiationFee.Text.Trim().Length == 0)                                                           // if initationfee textbox is empty
                    txtWizardInitiationFee.Text = "0";
                else
                {
                    decimal totalfee = (decimal)Decimal.Parse(txtWizardInitiationFee.Text.ToString()) + programmefee;         // calculate the total fee by adding the initation fee to the plan's fee
                    txtWizardTotalFees.Text = totalfee.ToString();                                                            // display total fee
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void wizard1_FinishButtonClick(object sender, CancelEventArgs e)
        {
            //////////////// CREATE A NEW MEMBER //////////////

            // create a new member
            DataLayer.Member member = new DataLayer.Member();

            // fill member properties from textboxes
            member.CardNumber = Int32.Parse(txtWizardCardNumber.Text);
            member.LName = txtWizardLastName.Text;
            member.FName = txtWizardFirstName.Text;
            if (rbWizardMale.Checked)
            {
                member.Sex = "male";
            }
            else if (rbWizardFemale.Checked)
            {
                member.Sex = "female";
            }
            member.DateOfBirth = dtpWizardDOB.Value;
            member.Street = txtWizardStreet.Text;
            member.Suburb = txtWizardSuburb.Text;
            member.City = txtWizardCity.Text;
            if (txtPostalCode.Text.Length > 0)
            {
                member.PostalCode = Int32.Parse(txtWizardPostalCode.Text);
            }
            member.HomePhone = txtWizardHomePhone.Text;
            member.CellPhone = txtWizardCellPhone.Text;
            member.Email = txtWizardEmail.Text;
            member.Occupation = txtWizardOccupation.Text;
            member.Notes = txtWizardNotes.Text;

            // holds the member's picture
            //byte[] imageBt = null;
            if (picWizard.ImageLocation != null)
            {
                FileStream fstream = new FileStream(picWizard.ImageLocation, FileMode.Open, FileAccess.Read);
                BinaryReader br = new BinaryReader(fstream);
                member.Image = br.ReadBytes((int)fstream.Length);
            }
            else
            {
                byte[] empty_array = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 };
                member.Image = empty_array;
            }


            //add member to db
            if (DataLayer.Members.AddNewMember(member) > 0)
            {
                //a new member has been added without any membership
                //MessageBox.Show("A New Member has been added. You can add a membership at any time from Member Manager", "Gym Manager Pro", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Failed to add new member. Please try again", "Gym Manager Pro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }



            ////////////// CREATE A NEW MEMBERSHIP FOR THE SAME MEMBER //////////////

            if (cbWizardPlans.Text != "None")
            {
                // create a new membership and fill with data
                DataLayer.Membership membership = new DataLayer.Membership();

                //to find member's id we get the last inserted id and increment by 1 because we haven't inserted the new member yet
                membership.MemberId = DataLayer.Members.GetLastInsertedMember();        // member's id
                //membership.MemberId++;
                membership.Plan = (int)cbWizardPlans.SelectedValue;                 // id of the selected plan
                membership.start = dtpWizardStartPlan.Value;                           // when the membership starts
                membership.end = dtpWizardEndPlan.Value;                               // when the membership expires

                if (DataLayer.Memberships.NewMembership(membership) > 0)
                {
                    MessageBox.Show("A New Member has been added successfully!", "Gym Manager Pro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Failed to add new membership. Please add manually from Member Manager", "Gym Manager Pro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }


            RefreshAllMembersDataGrid();            // refresh AllMembers datagridview
            panelNewMemberWizard.Visible = false;
            panelAllMembers.Visible = true;         // show all members datagrid view

        }

        private void dtpWizardStartPlan_ValueChanged(object sender, EventArgs e)
        {
            // set the expiration date of the membership based on the start date
            dtpWizardEndPlan.Value = dtpWizardStartPlan.Value;                                                            // get membership's start date
            int plan_id = Int32.Parse(cbWizardPlans.SelectedValue.ToString());                                      // get id of the selected plan
            dtpWizardEndPlan.Value = dtpWizardEndPlan.Value.AddMonths(DataLayer.Plan.GetPlanDuration(plan_id));           // calculate membership duration
            dtpWizardEndPlan.Value = dtpWizardEndPlan.Value.AddDays(-1);
        }

        private void wizardPage2_NextButtonClick(object sender, CancelEventArgs e)
        {
            if (String.IsNullOrEmpty(txtWizardLastName.Text) && String.IsNullOrEmpty(txtWizardCardNumber.Text))
            {
                MessageBox.Show("The Last Name and Card Number cannot be empty!", "Gym Manager Pro", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                wizard1.NavigateBack();
            }
        }

        private void wizardPage2_CancelButtonClick(object sender, CancelEventArgs e)
        {
            panelNewMemberWizard.Visible = false;
            wizard1.NavigateBack();
        }

        private void wizard1_CancelButtonClick(object sender, CancelEventArgs e)
        {
            panelNewMemberWizard.Visible = false;
        }

        private void wizardPage3_CancelButtonClick(object sender, CancelEventArgs e)
        {
            panelNewMemberWizard.Visible = false;
            wizard1.NavigateBack();
        }

        private void buttonItem13_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonItem1_Click(object sender, EventArgs e)
        {
            panelAllMembers.Visible = false;
            panelMemberManager.Visible = false;
            panelTrainers.Visible = false;
            panelPlans.Visible = false;
            panelAttedance.Visible = false;
            panelNewMemberWizard.Visible = true;
        }

        private void btnAttedanceSaveTxt_Click(object sender, EventArgs e)
        {
            SwitchToPanel(panelAttedance);

            // save file as a txt document
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Text Files | *.txt";
            sfd.DefaultExt = "txt";
            sfd.Title = "Save as text file";
            if (sfd.ShowDialog() == DialogResult.OK)
                System.IO.File.WriteAllText(sfd.FileName, richTextBoxAttedance.Text);
        }

        private void dataGridViewMemberships_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            //set up membership expire notifications in member manager
            SetUpNotifications();
        }

        private void btnPlansNew_Click(object sender, EventArgs e)
        {
            EditPlan ep = new EditPlan(listBoxPlans);
            ep.ShowDialog();
            SwitchToPanel(panelPlans);
        }

        private void btnPlansEdit_Click(object sender, EventArgs e)
        {
            if (form_loaded)
            {
                if (panelPlans.Visible)
                {
                    if (listBoxPlans.SelectedIndex != -1)
                    {
                        // get selected plan
                        int planId = Convert.ToInt32(listBoxPlans.SelectedValue.ToString());
                        EditPlan ep = new EditPlan(plan, listBoxPlans);
                        ep.ShowDialog();
                    }
                }
                else
                {
                    MessageBox.Show("Please select a plan first");
                    SwitchToPanel(panelPlans);
                }
            }
        }

        private void btnPlansDelete_Click(object sender, EventArgs e)
        {
            if (panelPlans.Visible)
            {
                DialogResult dialogResult = MessageBox.Show("Warning! Deleting the selected plan will also expire (delete) all the associated memberships! Are you sure you want to continue?", "Gym Manager Pro", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                if (dialogResult == DialogResult.Yes)
                {
                    DataLayer.Plan.DeletePlan(plan.Id);

                    // refresh the listbox
                    listBoxPlans.DataSource = DataLayer.Plan.GetAllPlans().ToList();
                    listBoxPlans.ValueMember = "Key";
                    listBoxPlans.DisplayMember = "Value";
                }
            }
            else
            {
                MessageBox.Show("Please select a plan first!");
                SwitchToPanel(panelPlans);
            }
        }

        private void btnTrainersRemoveMember_Click(object sender, EventArgs e)
        {
            TPresenter.UnlinkMember();
        }

        private void txtWizardLastName_TextChanged(object sender, EventArgs e)
        {
            lblWizardName.Text = txtWizardFirstName.Text +" "+ txtWizardLastName.Text;
        }

        private void txtWizardFirstName_TextChanged(object sender, EventArgs e)
        {
            lblWizardName.Text = txtWizardFirstName.Text + " " + txtWizardLastName.Text;
        }

        private void btnTrainersEdit_Click(object sender, EventArgs e)
        {
            EditTrainer();
        }

        private void btnTrainersSave_Click(object sender, EventArgs e)
        {
            TPresenter.SaveTrainer();
        }

        private void btnTrainersDelete_Click(object sender, EventArgs e)
        {
            TPresenter.DeleteMember();
        }

        private void btnTrainersAdd_Click(object sender, EventArgs e)
        {
            // switch to trainers panel
            if (!panelTrainers.Visible)
                SwitchToPanel(panelTrainers);

            TPresenter.AddTrainer();
            EditTrainer();  // set textboxes editable
        }

        private void menuAbout_Click(object sender, EventArgs e)
        {
            new AboutBox().Show();
        }

        private void btnFindSearch_Click(object sender, EventArgs e)
        {
            if (!panelAllMembers.Visible)
                SwitchToPanel(panelAllMembers);

            Presenter.SearchAction();
        }

        private void menuNewMember_Click(object sender, EventArgs e)
        {
            SwitchToPanel(panelNewMemberWizard);
        }

        private void pictureBoxMemberManager_Click(object sender, EventArgs e)
        {
            //loads an image for the member
            OpenFileDialog fd = new OpenFileDialog();
            fd.Filter = "JPG Files(*.jpg)|*.jpg|PNG Files(*.png)|*.png|All Files(*.*)|*.*";
            if (fd.ShowDialog() == DialogResult.OK)
            {
                string picLoc = fd.FileName.ToString();
                pictureBoxMemberManager.ImageLocation = picLoc;
            }
        }

        private void btnReportsOverview_Click(object sender, EventArgs e)
        {
            SwitchToPanel(panelReports);
        }

        private void btnFindExport_Click(object sender, EventArgs e)
        {
            Presenter.Export();
        }

        private void txtFindLastName_InputTextChanged(object sender)
        {
            if (!panelAllMembers.Visible)
                SwitchToPanel(panelAllMembers);

            Presenter.FilterByLastName();
        }

        private void txtFindFirstName_InputTextChanged(object sender)
        {
            if (!panelAllMembers.Visible)
                SwitchToPanel(panelAllMembers);

            Presenter.FilterByFirstName();
        }
    }

}


