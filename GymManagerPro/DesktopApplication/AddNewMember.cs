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
    public partial class AddNewMember : Form
    {
        public AddNewMember()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // create a new member and save its details to the db
            //DataLayer.Members members = new DataLayer.Members();
            DataLayer.Member member = new DataLayer.Member();

            if (!String.IsNullOrEmpty(txtLastName.Text) && !String.IsNullOrEmpty(txtCardNumber.Text))
            {
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

                // insert data
                if (DataLayer.Members.AddNewMember(member) > 0)
                {
                    MessageBox.Show("Member added successfully! Continue to add a membership.", "Gym Manager Pro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    AddNewContract ac = new AddNewContract(DataLayer.Members.GetLastInsertedMember());
                    ac.ShowDialog(this);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Failed to add a new member. Please try again.", "Gym Manager Pro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("The Last Name and Card Number cannot be empty!", "Gym Manager Pro", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            OpenFileDialog fd = new OpenFileDialog();
            fd.Filter = "JPG Files(*.jpg)|*.jpg|PNG Files(*.png)|*.png|All Files(*.*)|*.*";
            if (fd.ShowDialog() == DialogResult.OK)
            {
                string picLoc = fd.FileName.ToString();
                pictureBox1.ImageLocation = picLoc;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AddNewMember_Load(object sender, EventArgs e)
        {
            //cbPersonalTrainer.DataSource = DataLayer.Members.GetAllTrainers();
        }

    }
}
