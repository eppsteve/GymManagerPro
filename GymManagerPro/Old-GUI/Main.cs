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
    public partial class Main : Form
    {
        private Plans plans;

        public Main()
        {
            InitializeComponent();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddNewMember addmember = new AddNewMember();
            addmember.Show();
        }

        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void TileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }

        private void findMemberToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        void viewMembers_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void MDIParent_Load(object sender, EventArgs e)
        {
            // set background image of MdiParent form
            foreach (Control ctl in this.Controls)
            {
                if (ctl is MdiClient)
                {
                    ctl.BackgroundImageLayout = ImageLayout.Stretch;
                    ctl.BackgroundImage = Properties.Resources.bg;
                    
                    break;
                }
            }
        }

        private void addMemberToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void MDIParent_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void viewAttedanceToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        void va_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void btnMemberManager_Click(object sender, EventArgs e)
        {
            MemberManager mn = new MemberManager(DataLayer.Members.GetFirstMemberId());
            mn.MdiParent = this;
            mn.Show();
        }

        private void btnAddMember_Click(object sender, EventArgs e)
        {
            AddNewMember addmember = new AddNewMember();
            addmember.Show();
        }

        private void btnFindMembers_Click(object sender, EventArgs e)
        {
            if (System.Windows.Forms.Application.OpenForms["FindMembers"] as FindMembers == null)
            {
                // the form does not exist, create a new one
                FindMembers fm = new FindMembers();
                fm.MdiParent = this;
                fm.Show();
            }
            else
            {
                // the form exists, show it up
                FindMembers fm = (FindMembers)Application.OpenForms["FindMembers"];
                if (fm.WindowState == FormWindowState.Minimized)
                    fm.WindowState = FormWindowState.Normal;
                fm.Focus();
            }
        }

        private void btnAttedance_Click(object sender, EventArgs e)
        {
            if (System.Windows.Forms.Application.OpenForms["Attedance"] as Attedance == null)
            {
                // the form does not exist, create a new one
                Attedance at = new Attedance();
                at.MdiParent = this;
                at.Show();
            }
            else
            {
                // the form exists, show it up
                Attedance at = (Attedance)Application.OpenForms["Attedance"];
                if (at.WindowState == FormWindowState.Minimized)
                    at.WindowState = FormWindowState.Normal;
                at.Focus();
            }
        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {

                plans = new Plans();
                plans.MdiParent = this;
                plans.Show();

        }

        private void btnTrainers_Click(object sender, EventArgs e)
        {
            if (System.Windows.Forms.Application.OpenForms["Trainers"] as Trainers == null)
            {
                // the form does not exist, create a new one
                Trainers tr = new Trainers();
                tr.MdiParent = this;
                tr.Show();
            }
            else
            {
                // the form exists, show it up
                Trainers tr = (Trainers)Application.OpenForms["Trainers"];
                if (tr.WindowState == FormWindowState.Minimized)
                    tr.WindowState = FormWindowState.Normal;
                tr.Focus();
            }
        }

        private void exportToExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox about = new AboutBox();
            about.ShowDialog(this);
        }

        private void memberDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

    }
}
