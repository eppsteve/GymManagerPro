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
            // save file as a txt document
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Text Files | *.txt";
            sfd.DefaultExt = "txt";
            sfd.Title = "Save as text file";
            if (sfd.ShowDialog() == DialogResult.OK)
                System.IO.File.WriteAllText(sfd.FileName, richTextBox1.Text);
        }

        private void SetUpSearch()
        {
            // setup autocomplete search box
            txtSearch.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtSearch.AutoCompleteSource = AutoCompleteSource.CustomSource;
            AutoCompleteStringCollection coll = new AutoCompleteStringCollection();
            coll.AddRange(DataLayer.Members.AutoCompleteSearch().ToArray());
            txtSearch.AutoCompleteCustomSource = coll;
        }

        private void Attedance_Load(object sender, EventArgs e)
        {
            SetUpData();
            SetUpSearch();
        }

        private void SetUpData()
        {
            //get data
            richTextBox1.Text = DataLayer.Members.GetAttedance().ToString();
            // format textbox
            string active = "Active - Entrance allowed";
            string inactive = "Inactive - Entrance denied";
            Utility.HighlightText(richTextBox1, active, Color.Green);
            Utility.HighlightText(richTextBox1, inactive, Color.Red);
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            // NOT WORKING SINCE UPDATE IN RIBBON UI VERSION
            //if (txtSearch.Text.Length > 0)
            //{
            //    if (DataLayer.Members.MemberCheckin(DataLayer.Members.GetMemberIdByName(txtSearch.Text.Trim())) > 0)
            //    {
            //        MessageBox.Show("Member just Checked-In!", "Gym Manager Pro", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        richTextBox1.Clear();
            //        txtSearch.Clear();
            //        SetUpData();
            //    }
            //    else
            //    {
            //        MessageBox.Show("Failed to check-in. Please try again", "Gym Manager Pro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    }
            //}
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
            SetUpData();
        }

        private void txtSearch_Click(object sender, EventArgs e)
        {

        }
    }

    static class Utility{

        // highlights specified text
        // http://stackoverflow.com/questions/11851908/highlight-all-searched-word-in-richtextbox-c-sharp
        //
        public static void HighlightText(this RichTextBox myRtb, string word, Color color)
        {
            int s_start = myRtb.SelectionStart, startIndex = 0, index;

            while ((index = myRtb.Text.IndexOf(word, startIndex)) != -1)
            {
                myRtb.Select(index, word.Length);
                myRtb.SelectionColor = color;

                startIndex = index + word.Length;
            }
            myRtb.SelectionStart = s_start;
            myRtb.SelectionLength = 0;
            myRtb.SelectionColor = Color.Black;
        }
    }

}
