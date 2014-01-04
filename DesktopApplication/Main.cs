using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GymManager
{
    public partial class Main : Form
    {
        //private int childFormNumber = 0;
        private FindMembers findMembers;
        private Plans plans;
        //public CheckinMonitorOLD va;
        //public MembershipPlansOLD mp;
        //public TrainersOLD tr;

        public Main()
        {
            InitializeComponent();
        }

        private void ShowNewForm(object sender, EventArgs e)
        {
            //Form childForm = new Form();
            //childForm.MdiParent = this;
            //childForm.Text = "Window " + childFormNumber++;
            //childForm.Show();
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

        //creates only one instance of the viewMembers window
        private void findMemberToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //if (viewMembers == null)
            //{
            //    viewMembers = new FindMembersOLD();
            //    viewMembers.MdiParent = this;
            //    viewMembers.FormClosed += viewMembers_FormClosed;
            //    viewMembers.Show();
            //}
            //else
            //{
            //    viewMembers.Activate();
            //}
        }

        void viewMembers_FormClosed(object sender, FormClosedEventArgs e)
        {
            //viewMembers = null;
        }

        private void MDIParent_Load(object sender, EventArgs e)
        {

        }

        private void addMemberToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //AddMemberOLD am = new AddMemberOLD();
            //am.ShowDialog(this);
        }

        private void MDIParent_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void viewAttedanceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //if (va == null)
            //{
            //    va = new CheckinMonitorOLD();
            //    va.MdiParent = this;
            //    va.FormClosed += va_FormClosed;
            //    va.Show();
            //}
            //else
            //{
            //    va.Activate();
            //}
        }

        void va_FormClosed(object sender, FormClosedEventArgs e)
        {
            //va = null;
        }

        private void btnMemberManager_Click(object sender, EventArgs e)
        {
            MemberManager mn = new MemberManager(1);
            mn.MdiParent = this;
            mn.Show();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            AddNewMember addmember = new AddNewMember();
            addmember.Show();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (findMembers == null)
            {
                findMembers = new FindMembers();
                findMembers.MdiParent = this;
                findMembers.FormClosed += viewMembers_FormClosed;
                findMembers.Show();
            }
            else
            {
                findMembers.Activate();
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            Attedance at = new Attedance();
            at.MdiParent = this;
            at.Show();
            //if (va == null)
            //{
            //    va = new CheckinMonitorOLD();
            //    va.MdiParent = this;
            //    va.FormClosed += va_FormClosed;
            //    va.Show();
            //}
            //else
            //{
            //    va.Activate();
            //}
        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //if (plans == null)
            //{
                plans = new Plans();
                plans.MdiParent = this;
                //plans.FormClosed += plans_FormClosed;
                plans.Show();
            //}
            //else
            //{
            //    plans.Activate();
            //}
        }

        private void btnTrainers_Click(object sender, EventArgs e)
        {
            Trainers trainers = new Trainers();
            trainers.MdiParent = this;
            trainers.Show();
    
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
            Report report = new Report();
            report.MdiParent = this;
            report.Show();
        }

    }
}
