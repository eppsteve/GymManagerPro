using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GymManagerPro
{
    public partial class Trainers : Form
    {
        private int id = 1; //holds the id value of the current trainer

        public Trainers()
        {
            InitializeComponent();
        }

        /// <summary>
        /// loads the specified trainer's data
        /// </summary>
        /// <param name="id"></param>
        public void LoadTrainer(int id)
        {
            DataLayer.Trainer trainer = new DataLayer.Trainer();
            try
            {
                // display trainer details
                trainer = DataLayer.Trainers.GetTrainer(id);
                SetUpTextBoxes(trainer);

                // display associated members
                DataTable membersTable = DataLayer.Trainers.GetAssociatedMembers(id);
                dataGridView1.DataSource = membersTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        // populates listbox with the names of all the trainers
        public void LoadAllTrainersNames()
        {
            listBox1.DataSource = new BindingSource( DataLayer.Trainers.GetAllTrainers(), null );
            listBox1.DisplayMember = "Value";
            listBox1.ValueMember = "Key";
        }

        /// <summary>
        /// loads data to the textboxes
        /// </summary>
        /// <param name="trainer"></param>
        public void SetUpTextBoxes(DataLayer.Trainer trainer)
        {
            txtFName.Text = trainer.FName;
            txtLName.Text = trainer.LName;
            txtCellPhone.Text = trainer.CellPhone;
            txtCity.Text = trainer.City;
            txtDateOfBirth.Value = trainer.DateOfBirth.Date;
            txtEmail.Text = trainer.Email;
            txtHomePhone.Text = trainer.HomePhone;
            txtId.Text = id.ToString();
            txtNotes.Text = trainer.Notes;
            if (trainer.Sex == "Male")
                rbMale.Checked = true;
            else if (trainer.Sex == "Female")
                rbFemale.Checked = true;
            //txtPostalCode.Text = trainer.PostalCode.ToString();
            txtSalary.Text = trainer.Salary.ToString();
            txtSuburb.Text = trainer.Suburb;
            label15.Text = txtFName.Text + " " + txtLName.Text + " is set as a Personal Trainer for the following members.";
            
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

        private void Trainers_Load(object sender, EventArgs e)
        {
            //LoadTrainers();
            LoadTrainer(id);
            LoadAllTrainersNames();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                try
                {
                    id = Convert.ToInt32(listBox1.SelectedValue.ToString());
                    LoadTrainer(id);
                }
                catch (Exception ex)
                {
                    //MessageBox.Show(ex.ToString());
                }
            }
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (dataGridView1.SelectedRows.Count != 0)
            {
                try
                {
                    // get the id of the member to be removed
                    int memberToRemove = (int) dataGridView1.SelectedCells[0].Value; //first cell of the selected row
                    
                    if (DataLayer.Trainers.RemoveMember(memberToRemove) > 0)
                    {
                        MessageBox.Show("Member removed!", "Gym Manager Pro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        
                        // reload associated members
                        DataTable membersTable = DataLayer.Trainers.GetAssociatedMembers(id);
                        dataGridView1.DataSource = membersTable;
                    }
                    else
                    {
                        MessageBox.Show("Failed to remove member. Please try again", "Gym Manager Pro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            else
            {
                MessageBox.Show("Select a member to remove", "Gym Manager Pro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            TrainerAssignmentDialog tad = new TrainerAssignmentDialog(id);
            tad.Show();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadTrainer(id);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            DataLayer.Trainer trainer = new DataLayer.Trainer();
            if (!String.IsNullOrEmpty(txtLName.Text))
            {
                trainer.TrainerID = this.id;
                trainer.FName = txtFName.Text.Trim();
                trainer.LName = txtLName.Text.Trim();
                trainer.CellPhone = txtCellPhone.Text.Trim();
                trainer.City = txtCity.Text.Trim();
                trainer.DateOfBirth = txtDateOfBirth.Value;
                trainer.Email = txtEmail.Text.Trim();
                trainer.HomePhone = txtHomePhone.Text.Trim();
                if (txtSalary.Text.Length > 0)
                {
                    trainer.Salary = Decimal.Parse(txtSalary.Text.ToString());
                }
                else
                {
                    trainer.Salary = 0;
                }
                trainer.Street = txtStreet.Text.Trim();
                trainer.Suburb = txtSuburb.Text.Trim();
                if (rbMale.Checked)
                {
                    trainer.Sex = "Male";
                }
                else if (rbFemale.Checked)
                {
                    trainer.Sex = "Female";
                }
                trainer.Notes = txtNotes.Text.Trim();

                // holds the trainer's picture
                //byte[] imageBt = null;
                //if (pictureBox1.ImageLocation != null)
                //{
                //    FileStream fstream = new FileStream(pictureBox1.ImageLocation, FileMode.Open, FileAccess.Read);
                //    BinaryReader br = new BinaryReader(fstream);
                //    trainer.Image = br.ReadBytes((int)fstream.Length);
                //}
                //else
                //{
                //    //byte[] empty_array = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 };
                //    trainer.Image = imageBt;
                //}

                //save data
                if (DataLayer.Trainers.UpdateTrainer(trainer) > 0)
                {
                    MessageBox.Show("Trainer Updated successfully!", "Gym Manager Pro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadAllTrainersNames();
                }
                else
                {
                    MessageBox.Show("Failed to Update Trainer!", "Gym Manager Pro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Last Name cannot be empty!", "Gym Manager Pro", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnNewTrainer_Click(object sender, EventArgs e)
        {
            DataLayer.Trainer trainer = new DataLayer.Trainer();
            //initialize new trainer
            trainer.FName = "New Trainer";
            trainer.LName = "New Trainer";
            trainer.Sex = "Male";
            trainer.DateOfBirth = DateTime.Now;
            trainer.Street = String.Empty;
            trainer.Suburb = String.Empty;
            trainer.Salary = 0;
            trainer.HomePhone = String.Empty;
            trainer.CellPhone = String.Empty;
            trainer.Notes = String.Empty;
            trainer.City = String.Empty;
            trainer.Email = String.Empty;

            if (DataLayer.Trainers.NewTrainer(trainer) > 0)
            {
                //MessageBox.Show("success");
                id = DataLayer.Trainers.GetLastInsertedTrainer();
                LoadAllTrainersNames();
                this.listBox1.SelectedIndex = this.listBox1.Items.Count - 1;
            }
            else
            {
                MessageBox.Show("could not add");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (DataLayer.Trainers.DeleteTrainer(this.id) > 0)
            {
                MessageBox.Show("Trainer deleted!", "Gym Manager Pro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadAllTrainersNames();
            }
            else
            {
                MessageBox.Show("Could not delete. Please try again.", "Gym Manager Pro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
