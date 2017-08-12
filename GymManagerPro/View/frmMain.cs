using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using GymManagerPro.Presenter;
using DevComponents.DotNetBar.Controls;

namespace GymManagerPro.View
{
    public partial class frmMain : Form, IMember, ITrainer, IPlan, IWizard
    {
        bool form_loaded;

        public MemberPresenter Presenter { get; set; }
        public TrainerPresenter TPresenter { get; set; }
        public PlanPresenter PPresenter { get; set; }
        public WizardPresenter WPresenter { get; set; }
        
        #region Presenter properties
        /// <summary>
        /// FindMember Properties
        /// </summary>
        string IMember.FFirstName { get => txtFindFirstName.Text; set => txtFindFirstName.Text = value; }
        string IMember.FLastName { get => txtFindLastName.Text; set => txtFindLastName.Text = value; }
        public int SelectedPlanIndex { get => cbFindPlan.SelectedIndex; set => cbFindPlan.SelectedIndex = value; }
        public int SelectedPersonalTrainerIndex{ get => cbFindPersonalTrainer.SelectedIndex; set => cbFindPersonalTrainer.SelectedIndex = value;}
        public int SelectedSearchByIndex { get => cbFindSearchBy.SelectedIndex; set => cbFindSearchBy.SelectedIndex = value;}
        public string SearchBy => cbFindSearchBy.SelectedItem.ToString(); 
        public string Keyword { get => txtFindSearch.Text; set => txtFindSearch.Text = value; }
        SaveFileDialog IMember.ExportFileDialog => saveFileDialog1; 
        DataGridViewX IMember.MembersGrid { get =>  membersDataGridViewX; set => membersDataGridViewX = value; }
        ComboBox IMember.cbFindPlan { get => cbFindPlan; set => cbFindPlan = value; }
        ComboBox IMember.cbFindPersonalTrainer { get => cbFindPersonalTrainer; set => cbFindPersonalTrainer = value; }

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
                lblFName.Text = value;
            }
        }
        string IMember.LastName
        {
            get { return txtLastName.Text; }
            set
            {
                txtLastName.Text = value;
                lblLName.Text = value;
            }
        }
        int IMember.CardNumber
        {
            get { return Int32.Parse(txtCardNumber.Text); }
            set
            {
                txtCardNumber.Text = value.ToString();
                txtCardNumber2.Text = value.ToString();
            }
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
            get { return txtPostalCode.TextLength == 0 ? 0 : Int32.Parse(txtPostalCode.Text); }
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
        string IMember.Notes
        {
            get { return txtNotes.Text; }
            set { txtNotes.Text = value; }
        }
        object IMember.MembershipsGridDataSource
        {
            get { return dataGridViewMemberships.DataSource; }
            set { dataGridViewMemberships.DataSource = value; }
        }
        DataGridViewRowCollection IMember.MDGVRows => dataGridViewMemberships.Rows; 
        public int PersonalTrainer
        {
            get
            {
                if (cbPersonalTrainer.SelectedValue != null)
                    return Int32.Parse(cbPersonalTrainer.SelectedValue.ToString());
                else
                    return 0;
            }
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
        public string MembershipsNotifications { get => lblNotifications.Text; set => lblNotifications.Text = value; }
        ComboBoxEx IMember.cbPersonalTrainer { get => cbPersonalTrainer; set => cbPersonalTrainer = value; }
        DataGridViewX IMember.MeasurementsGrid { get => dgvMeasurements; set => dgvMeasurements = value; }
        public ContextMenuStrip MeasurementsContextMenu { get => contextMenuMeasurements; set => ContextMenuStrip = value; }

        /// <summary>
        /// Trainer Properties
        /// </summary>
        public int TrainerID { get => Int32.Parse(txtTrainerId.Text); set => txtTrainerId.Text = value.ToString(); }
        string ITrainer.FirstName { get => txtTrainerFName.Text; set => txtTrainerFName.Text = value; }
        string ITrainer.LastName { get => txtTrainerLName.Text; set => txtTrainerLName.Text = value; }    
        string ITrainer.HomePhone { get=>txtTrainerHomePhone.Text; set => txtTrainerHomePhone.Text = value; }
        string ITrainer.CellPhone {get => txtTrainerCellPhone.Text; set => txtTrainerCellPhone.Text = value; }
        string ITrainer.Email { get => txtTrainerEmail.Text; set => txtTrainerEmail.Text = value; }
        string ITrainer.Notes { get => txtTrainerNotes.Text; set => txtTrainerNotes.Text = value; }
        string ITrainer.Street { get => txtTrainerStreet.Text; set => txtTrainerStreet.Text = value; }
        string ITrainer.City { get => txtTrainerCity.Text; set => txtTrainerCity.Text = value; }
        string ITrainer.Suburb { get => txtTrainerSuburb.Text; set => txtTrainerSuburb.Text = value; }
        bool ITrainer.Male_IsSelected { get => rbTrainerMale.Checked; set => rbTrainerMale.Checked = value; }
        bool ITrainer.Female_IsSelected { get => rbTrainerFemale.Checked; set => rbTrainerFemale.Checked = value; }
        DateTime ITrainer.DateOfBirth { get => dtpTrainerDOB.Value; set => dtpTrainerDOB.Value = value; }
        decimal ITrainer.Salary
        {
            get { return Decimal.Parse(txtTrainerSalary.Text.ToString()); }
            set { txtTrainerSalary.Text = value.ToString(); }
        }
        public ListBox lbTrainers { get => listBoxTrainers; set => listBoxTrainers = value; }
        public DataGridViewX dgLinkedMembers { get => amTrainersDataGridViewX; set => amTrainersDataGridViewX = value; }
        bool ITrainer.IsPanelVisible { get => panelTrainers.Visible; set => panelTrainers.Visible = value; }

        /// <summary>
        /// Plan Properties
        /// </summary>       
        public int Duration { get =>Int32.Parse(txtPlanDuration.Text); set => txtPlanDuration.Text = value.ToString(); }  
        public decimal Price { get => Decimal.Parse(txtPlanPrice.Text); set => txtPlanPrice.Text = value.ToString(); }
        public string PlanName { get => txtPlanName.Text; set => txtPlanName.Text = value; }
        public bool IsPlansPanelVisible { get => panelPlans.Visible; set => panelPlans.Visible = value; }
        public ListBox lbPlans { get => listBoxPlans; set => listBoxPlans = value; }
        string IPlan.Notes { get => txtPlanNotes.Text; set => txtPlanNotes.Text = value; }

        /// <summary>
        /// New Member Wizard Properties
        /// </summary>
        string IWizard.FirstName { get => txtWizardFirstName.Text; set => txtWizardFirstName.Text = value; } 
        string IWizard.LastName { get => txtWizardLastName.Text; set => txtWizardLastName.Text = value; }
        int IWizard.CardNumber { get => Int32.Parse(txtWizardCardNumber.Text); set => txtWizardCardNumber.Text = value.ToString(); }
        string IWizard.HomePhone { get => txtWizardHomePhone.Text; set => txtWizardHomePhone.Text = value; }
        string IWizard.CellPhone { get => txtWizardCellPhone.Text; set => txtWizardCellPhone.Text = value; }
        string IWizard.Email { get => txtWizardEmail.Text; set => txtWizardEmail.Text = value; }
        string IWizard.Notes { get => txtWizardNotes.Text; set => txtWizardNotes.Text = value; }
        string IWizard.Street { get => txtWizardStreet.Text; set => txtWizardStreet.Text = value; }
        string IWizard.Suburb { get => txtWizardSuburb.Text; set => txtWizardSuburb.Text = value; }
        string IWizard.City { get => txtWizardCity.Text; set => txtWizardCity.Text = value; }
        string IWizard.Occupation { get => txtWizardOccupation.Text; set => txtWizardOccupation.Text = value; }
        int IWizard.PostalCode { get =>Int32.Parse(txtWizardPostalCode.Text); set => txtWizardPostalCode.Text = value.ToString(); }
        bool IWizard.IsMaleChecked { get => rbWizardMale.Checked; set => rbWizardMale.Checked = value; }
        bool IWizard.IsFemaleChecked { get => rbWizardFemale.Checked; set => rbWizardFemale.Checked = value; }
        public string ImageLocation { get => picWizard.ImageLocation; set => picWizard.ImageLocation = value; }
        public DateTime MembershipStart => dtpWizardStartPlan.Value;
        public DateTime MembershipEnd => dtpWizardEndPlan.Value;
        public int PlanId => (int)cbWizardPlans.SelectedValue;
        public string SelectedPlanName => cbWizardPlans.Text;
        bool IWizard.IsPanelVisible { get => panelNewMemberWizard.Visible; set => panelNewMemberWizard.Visible = value; }
        public decimal InitializationFee { get => Decimal.Parse(txtWizardInitiationFee.Text); set => txtWizardInitiationFee.Text = value.ToString(); }
        public decimal TotalFee { get => Decimal.Parse(txtWizardTotalFees.Text); set => txtWizardTotalFees.Text = value.ToString(); }
        ComboBox IWizard.cbWizardPlans { get => cbWizardPlans; set => cbWizardPlans = value; }
        DevComponents.DotNetBar.Wizard IWizard.NewMemberWizard { get => wizard1; set => wizard1 = value; }
        #endregion

        public frmMain()
        {
            InitializeComponent();
            Presenter = new MemberPresenter(this);
            TPresenter = new TrainerPresenter(this);
            PPresenter = new PlanPresenter(this);
            WPresenter = new WizardPresenter(this);
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

        private void DoNotAllowTrainerEdit()
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
            btnTrainersSave.Enabled = false;
            rbTrainerMale.Enabled = false;
            rbTrainerFemale.Enabled = false;

            //change button text and icon
            btnTrainersEdit.Text = "Edit";
            ComponentResourceManager resources = new ComponentResourceManager(typeof(frmMain));
            btnTrainersEdit.Icon = ((Icon)(resources.GetObject("btnTrainersEdit.Icon")));
        }

        private void AllowTrainerEdit()
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
            btnTrainersSave.Enabled = true;
            rbTrainerMale.Enabled = true;
            rbTrainerFemale.Enabled = true;

            btnTrainersEdit.Text = "Cancel";
            btnTrainersEdit.Icon = null;
            btnTrainersEdit.Tooltip = "Cancel editing";
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
            rbFemale.Enabled = false;
            rbMale.Enabled = false;

            // change button text and icon
            btnMembersEdit.Text = "Edit";
            ComponentResourceManager resources = new ComponentResourceManager(typeof(frmMain));
            btnMembersEdit.Icon = ((System.Drawing.Icon)(resources.GetObject("btnMembersEdit.Icon")));
            // disable save button
            btnMembersSave.Enabled = false;
        }

        private void AllowMemberEdit()
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
            rbMale.Enabled = true;
            rbFemale.Enabled = true;

            btnMembersEdit.Text = "Cancel";
            btnMembersEdit.Icon = null;
            btnMembersEdit.Tooltip = "Cancel editing";
            btnMembersSave.Enabled = true;
        }

        // reloads data to AllMembers datagridview to refresh
        private void RefreshAllMembersDataGrid()
        {
            // get all members and bind them to the members datagridview to reload
            BindingSource bSource = new BindingSource();
            var dataset = DataLayer.Members.GetAllMembers();
            bSource.DataSource = dataset;
            membersDataGridViewX.DataSource = bSource;

            //set comboboxes to default value
            cbFindPersonalTrainer.SelectedIndex = 0;
            cbFindPlan.SelectedIndex = 0;
        }


        // --------------------------------- EVENTS ------------------------------------ //

        private void frmMain_Load(object sender, EventArgs e)
        {
            // load check ins
            SetUpAttedance();

            // set up autocomplete members search box in ribbon
            SetUpSearch();

            SwitchToPanel(panelAllMembers);
            ribbonTabFind.Select();       //switch to Find ribbon tab
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
                PPresenter.ChangeSelectedPlan();
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
            if (!String.IsNullOrWhiteSpace(txtMembersSearch.Text))
            {
                SelectedMember = DataLayer.Members.QuickSearch(txtMembersSearch.Text);
                Presenter.LoadMember();
                SwitchToPanel(panelMemberManager);
            }            
        }

        private void btnCheckIn_Click(object sender, EventArgs e)
        {
            Presenter.CheckInMember();
            SetUpAttedance();
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
                if (Presenter.SaveMember())                
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
                    AllowMemberEdit();
                }
                else if (btnMembersEdit.Text == "Cancel")
                {
                    DoNotAllowMemberEdit();
                    Presenter.LoadMember();
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
            AddNewMembership newContract = new AddNewMembership(SelectedMember, dataGridViewMemberships);
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
            WPresenter.AddInitializationFee();
        }

        private void wizard1_FinishButtonClick(object sender, CancelEventArgs e)
        {
            if (WPresenter.AddNewMember())
            {
                RefreshAllMembersDataGrid();
                SwitchToPanel(panelAllMembers);
            }
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
            if (String.IsNullOrWhiteSpace(txtWizardLastName.Text) && String.IsNullOrWhiteSpace(txtWizardCardNumber.Text))
            {
                MessageBox.Show("The Last Name and Card Number cannot be empty!", "Gym Manager Pro", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                wizard1.NavigateBack();
            }
        }

        private void wizardPage2_CancelButtonClick(object sender, CancelEventArgs e)
        {
            SwitchToPanel(panelAllMembers);
            WPresenter.ClearTextBoxes();
        }

        private void wizard1_CancelButtonClick(object sender, CancelEventArgs e)
        {
            SwitchToPanel(panelAllMembers);
            WPresenter.ClearTextBoxes();
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
            SwitchToPanel(panelNewMemberWizard);
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
            Presenter.SetUpNotifications();
        }

        private void btnPlansNew_Click(object sender, EventArgs e)
        {
            PPresenter.NewPlan();
        }

        private void btnPlansEdit_Click(object sender, EventArgs e)
        {
            if (form_loaded)
            {
                PPresenter.EditPlan();
            }
        }

        private void btnPlansDelete_Click(object sender, EventArgs e)
        {
            PPresenter.DeletePlan();
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
            if (panelTrainers.Visible)
            {
                if (btnTrainersEdit.Text == "Edit")
                {
                    AllowTrainerEdit();
                }
                else if (btnTrainersEdit.Text == "Cancel")
                {
                    DoNotAllowTrainerEdit();
                    TPresenter.LoadTrainer();
                }
            }
            else
            {
                MessageBox.Show("Please select a trainer first!");
                SwitchToPanel(panelTrainers);
            }
        }

        private void btnTrainersSave_Click(object sender, EventArgs e)
        {
            TPresenter.SaveTrainer();
            DoNotAllowTrainerEdit();
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
            AllowTrainerEdit();
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

        private void btnMembersNewMeasurement_Click(object sender, EventArgs e)
        {
            Presenter.NewMeasurement();
        }

        private void dgvMeasurements_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int id = (int)dgvMeasurements.Rows[e.RowIndex].Cells[0].Value;
            new frmMeasurement(SelectedMember, id).Show();
        }

        private void dgvMeasurements_MouseClick(object sender, MouseEventArgs e)
        {
            Presenter.MeasurementsGrid_Click(e);
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Presenter.EditMeasurement();          
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Presenter.NewMeasurement();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Presenter.DeleteMeasurement();
        }

        private void btnBackup_Click(object sender, EventArgs e)
        {
            Util.Common.BackUpDatabase();
        }

        private void btnRestoreDatabase_Click(object sender, EventArgs e)
        {
            Util.Common.RestoreDatabase();
        }
    }
}