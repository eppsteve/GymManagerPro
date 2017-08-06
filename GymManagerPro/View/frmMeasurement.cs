using GymManagerPro.DataLayer;
using System;
using System.Windows.Forms;

namespace GymManagerPro.View
{
    public partial class frmMeasurement : Form
    {
        int MemberID;
        int id;
        bool _edit_mode = false;

        public frmMeasurement(int memberid)
        {
            InitializeComponent();
            this.MemberID = memberid;
        }

        public frmMeasurement(int memberid, int id)
        {
            InitializeComponent();
            this.MemberID = memberid;
            this.id = id;
            LoadMeasurement(id);
            _edit_mode = true;
        }

        private void LoadMeasurement(int id)
        {
            var measurement = Measurement.GetMeasurement(id);
            txtAbdomen.Text = measurement.Abdomen.ToString();
            txtBodyfat.Text = measurement.Bodyfat.ToString();
            txtChest.Text = measurement.Chest.ToString();
            txtHeight.Text = measurement.Height.ToString();
            txtHips.Text = measurement.Hips.ToString();
            txtLArm.Text = measurement.LArm.ToString();
            txtLCalf.Text = measurement.LCalf.ToString();
            txtLThigh.Text = measurement.LThigh.ToString();
            txtRArm.Text = measurement.RArm.ToString();
            txtRCalf.Text = measurement.RCalf.ToString();
            txtRThigh.Text = measurement.RThigh.ToString();
            txtWaist.Text = measurement.Waist.ToString();
            txtWeight.Text = measurement.Weight.ToString();
        }

        private void CalculateBMI()
        {
            if (!String.IsNullOrEmpty(txtWeight.Text) && !String.IsNullOrEmpty(txtHeight.Text))
            {
                var height = Decimal.Parse(txtHeight.Text);
                var weight = Decimal.Parse(txtWeight.Text);
                if (weight !=0)
                    lblBMI.Text = (weight / (height * height)).ToString("#.##");
            }
        }

        private void txtHeight_ValueChanged(object sender, EventArgs e)
        {
            CalculateBMI();
        }

        private void txtWeight_ValueChanged(object sender, EventArgs e)
        {
            CalculateBMI();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            var measurement = new Measurement()
            {
                Id = this.id,
                MemberId = this.MemberID,
                Datecreated = dtpDateCreated.Value,
                Height = txtHeight.Text != String.Empty ? Decimal.Parse(txtHeight.Text) : 0,
                Weight = txtWeight.Text != String.Empty ? Decimal.Parse(txtWeight.Text) : 0,
                Bodyfat = txtBodyfat.Text != String.Empty ? Decimal.Parse(txtBodyfat.Text) : 0,
                Chest = txtChest.Text != String.Empty ? Decimal.Parse(txtChest.Text) : 0,
                LArm = txtLArm.Text != String.Empty ? Decimal.Parse(txtLArm.Text) : 0,
                RArm = txtRArm.Text != String.Empty ? Decimal.Parse(txtRArm.Text) : 0,
                Waist = txtWaist.Text != String.Empty ? Decimal.Parse(txtWaist.Text) : 0,
                Abdomen = txtAbdomen.Text != String.Empty ? Decimal.Parse(txtAbdomen.Text) : 0,
                Hips = txtHips.Text != String.Empty ? Decimal.Parse(txtHips.Text) : 0,
                LThigh = txtLThigh.Text != String.Empty ? Decimal.Parse(txtLThigh.Text) : 0,
                RThigh = txtRThigh.Text != String.Empty ? Decimal.Parse(txtRThigh.Text) : 0,
                LCalf = txtLCalf.Text != String.Empty ? Decimal.Parse(txtLCalf.Text) : 0,
                RCalf = txtRCalf.Text != String.Empty ? Decimal.Parse(txtRCalf.Text) : 0,
            };

            if (_edit_mode)
            {
                if (measurement.Update() !=1)
                    MessageBox.Show("Failed to update measures, please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            if (measurement.Save() != 1)
                MessageBox.Show("Failed to save measures, please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            this.Dispose();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
