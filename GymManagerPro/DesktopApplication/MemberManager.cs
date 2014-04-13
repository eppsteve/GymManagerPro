using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlServerCe;

namespace GymManagerPro
{
    public partial class MemberManager : Form
    {
        SqlCeConnection con = DataLayer.DB.GetSqlCeConnection();
        
        private int id;     // holds the id value of the current member
        private int membershipId;  // the selected membership id
        
        public MemberManager()
        {
            InitializeComponent();
        }

        public MemberManager(int id)
        {
            InitializeComponent();
            this.id = id;
        }

        private void MemberManager1_Load(object sender, EventArgs e)
        {
            // load member data
            LoadMember(id);

            programmeComboBox.DataSource = DataLayer.Members.GetAllPlans().ToList();
            //cbPersonalTrainer.DataSource = DataLayer.Members.GetAllTrainers();
            SetUpSearch();
        }

        /// <summary>
        /// Loads the data for the specified member
        /// </summary>
        /// <param name="id">The member's id</param>
        private void LoadMember(int id)
        {
            DataLayer.Members members = new DataLayer.Members();
            DataLayer.Member member = new DataLayer.Member();

            try
            {
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
                //txtPersonalTrainer.Text = DataLayer.Trainers.GetTrainerNameById(id);
                txtPersonalTrainer.Text = member.PersonalTrainer;
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
                DataTable table = DataLayer.Members.GetMembership(id);
                dataGridView1.DataSource = table;
                resetTextBoxes();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// Clears membership textboxes
        /// </summary>
        private void resetTextBoxes()
        {
            dataGridView1.Rows[0].Selected = false; //deselect first row
            programmeComboBox.Text = "";
            startDateDateTimePicker.Text = "";
            priceTextBox.Text = "";
            endDateDateTimePicker.Text = "";
        }


        // sets up auto-complete search box
        private void SetUpSearch()
        {
            txtSearch.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtSearch.AutoCompleteSource = AutoCompleteSource.CustomSource;
            AutoCompleteStringCollection coll = new AutoCompleteStringCollection();
            coll.AddRange(DataLayer.Members.AutoCompleteSearch().ToArray());
            txtSearch.AutoCompleteCustomSource = coll;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // create a new member and save its details to the db
            DataLayer.Members members = new DataLayer.Members();
            DataLayer.Member member = new DataLayer.Member();

            if (!String.IsNullOrEmpty(txtLastName.Text))
            {
                member.MemberID = this.id;
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
            }
            else
            {
                MessageBox.Show("Last Name cannot be empty!", "Gym Manager Pro", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            // Deletes the current member
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete this member? All related data will be lost!!!", "Gym Manager Pro", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dialogResult == DialogResult.Yes)
            {
                DataLayer.Members.DeleteMember(id);
                MessageBox.Show("Member deleted!", "Gym Manager Pro", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // display the previous member
                id = DataLayer.Members.GetPrevMember(id);
                LoadMember(id);
            }
        }


        private void btnNext_Click(object sender, EventArgs e)
        {
            // Retrieves and displays the next member (if any)
            if (DataLayer.Members.MemberHasNext(id))
            {
                id = DataLayer.Members.GetNextMember(id);
                LoadMember(id);
            }
            else
            {
                MessageBox.Show("There are no more members!", "Gym Manager Pro");
            }
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (DataLayer.Members.MemberHasPrevious(id))
            {
                id = DataLayer.Members.GetPrevMember(id);
                LoadMember(id);
            }
            else
            {
                MessageBox.Show("There are no more members!", "Gym Manager Pro");
            }
        }

        private void btnCheckin_Click(object sender, EventArgs e)
        {
            if (DataLayer.Members.MemberCheckin(id) > 0)
            {
                MessageBox.Show(lblName.Text + " just Checked-in!", "Gym Manager Pro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Console.Beep();
            }
            else
            {
                MessageBox.Show("Couldnot check-in. Please try again", "Gym Manager Pro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadMember(id);
        }

        private void btnNewMembership_Click(object sender, EventArgs e)
        {
            AddNewContract newContract = new AddNewContract(id);
            newContract.Show();
        }

        private void btnNewMember_Click(object sender, EventArgs e)
        {
            AddNewMember addmember = new AddNewMember();
            addmember.Show();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (btnEdit.Text == "Edit")
            {
                programmeComboBox.Enabled = true;
                startDateDateTimePicker.Enabled = true;
                btnEdit.Text = "Cancel";
            }
            else if (btnEdit.Text == "Cancel")
            {
                programmeComboBox.Enabled = false;
                startDateDateTimePicker.Enabled = false;
                btnEdit.Text = "Edit";
                resetTextBoxes();
            }
        }

        private void btnSaveMembership_Click(object sender, EventArgs e)
        {
            if (btnEdit.Text == "Cancel")
            {
                try
                {
                    if (DataLayer.Members.UpdateMembership(id, membershipId, programmeComboBox.Text, startDateDateTimePicker.Value, endDateDateTimePicker.Value) > 0)
                    {
                        MessageBox.Show("Membership Updated!", "Gym Manager Pro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Failed to update membership. Please try again.", "Gym Manager Pro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    LoadMember(id);     //refresh
                    programmeComboBox.Enabled = false;
                    startDateDateTimePicker.Enabled = false;
                    btnEdit.Text = "Edit";
                    resetTextBoxes();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // populates textboxes with datagridview data based on the selected row
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];

                programmeComboBox.SelectedItem = row.Cells["Name"].Value.ToString();
                startDateDateTimePicker.Text = row.Cells["StartDate"].Value.ToString();
                endDateDateTimePicker.Text = row.Cells["EndDate"].Value.ToString();
                membershipId = Convert.ToInt32(row.Cells["Id"].Value.ToString());
                //priceTextBox.Text = row.Cells["Price"].Value.ToString();
            }
        }

        
        private void btnSearch_Click(object sender, EventArgs e)
        {
            // Displays the selected member from the search box
            LoadMember(DataLayer.Members.QuickSearch(txtSearch.Text));
        }

        private void startDateDateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            // calculates the end date of the selected plan based on the start date
            endDateDateTimePicker.Value = startDateDateTimePicker.Value.AddMonths(DataLayer.Members.GetProgrammeDuration(programmeComboBox.Text)).AddDays(-1);
        }

    
        private void programmeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Calculates the duration of the selected plan and dispays its price to priceTextBox
            endDateDateTimePicker.Value = startDateDateTimePicker.Value.AddMonths(DataLayer.Members.GetProgrammeDuration(programmeComboBox.Text)).AddDays(-1);
            priceTextBox.Text = DataLayer.Members.GetProgrammePrice(programmeComboBox.Text).ToString();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            //loads an image for the member
            OpenFileDialog fd = new OpenFileDialog();
            fd.Filter = "JPG Files(*.jpg)|*.jpg|PNG Files(*.png)|*.png|All Files(*.*)|*.*";
            if (fd.ShowDialog() == DialogResult.OK)
            {
                string picLoc = fd.FileName.ToString();
                pictureBox1.ImageLocation = picLoc;
            }
        }

        private void btnDeleteMembership_Click(object sender, EventArgs e)
        {
            // deletes/expires the selected membership
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete this membership? The selected membership will expire and the operation cannot be undone!!!", "Gym Manager Pro", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dialogResult == DialogResult.Yes)
            {
                int delid = (int)dataGridView1.SelectedRows[0].Cells[0].Value;
                DataLayer.Members.DeleteMembership(delid);
                MessageBox.Show("Membership deleted and has been expired!", "Gym Manager Pro", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnFindMembers_Click(object sender, EventArgs e)
        {

        }
    }
}
