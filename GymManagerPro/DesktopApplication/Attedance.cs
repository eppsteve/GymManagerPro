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
    public partial class Attedance : Form
    {
        public Attedance()
        {
            InitializeComponent();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Text Files | *.txt";
            sfd.DefaultExt = "txt";
            sfd.Title = "Save as text file";
            if (sfd.ShowDialog() == DialogResult.OK)
                System.IO.File.WriteAllText(sfd.FileName, textBox1.Text);
        }

        private void SetUpSearch()
        {
            txtSearch.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtSearch.AutoCompleteSource = AutoCompleteSource.CustomSource;
            AutoCompleteStringCollection coll = new AutoCompleteStringCollection();
            coll.AddRange(DataLayer.Members.AutoCompleteSearch().ToArray());
            txtSearch.AutoCompleteCustomSource = coll;
        }

        private void Attedance_Load(object sender, EventArgs e)
        {
            textBox1.Text = DataLayer.Members.GetAttedance().ToString();
            SetUpSearch();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (txtSearch.Text.Length > 0)
            {
                if (DataLayer.Members.CheckInMember(DataLayer.Members.GetMemberIdByName(txtSearch.Text.Trim())) > 0)
                {
                    MessageBox.Show("Member just Checked-In!", "Gym Manager Pro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    textBox1.Clear();
                    textBox1.Text = DataLayer.Members.GetAttedance().ToString();
                }
                else
                {
                    MessageBox.Show("Failed to check-in. Please try again", "Gym Manager Pro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox1.Text = DataLayer.Members.GetAttedance().ToString();
        }
    }
}
