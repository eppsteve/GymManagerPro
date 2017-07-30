using GymManagerPro.View;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace GymManagerPro.Presenter
{
    public class MemberPresenter
    {
        private IMember view;
        DataTable dataset;
        int SelectedMember;

        public MemberPresenter(IMember View)
        {
            this.view = View;
            InitDataGrid();
        }

        private void InitDataGrid()
        {
            // get all members and bind them to the members datagridview
            BindingSource bSource = new BindingSource();
            dataset = DataLayer.Members.GetAllMembers();
            bSource.DataSource = dataset;
            view.MembersGrid.DataSource = bSource;
        }

        public void FilterByLastName()
        {
            // filter datagridview data by last name
            DataView dv = new DataView(dataset);
            dv.RowFilter = string.Format("LastName LIKE '%{0}%'", view.FLastName);
            view.MembersGrid.DataSource = dv;
        }

        public void FilterByFirstName()
        {
            DataView dv = new DataView(dataset);
            dv.RowFilter = string.Format("FirstName LIKE '%{0}%'", view.FFirstName);
            view.MembersGrid.DataSource = dv;
        }

        public void FilterByPlan()
        {
            // retrieve the members who have the selected plan and bind them to datagridview
            int plan_id = view.SelectedPlanIndex;                 // get id of the selected plan

            BindingSource bSource = new BindingSource();
            if (plan_id != 0 )              // if the selected plan is not 'All'
                dataset = DataLayer.Members.GetMembersByPlan(plan_id);
            else                            // if the selected plan is 'ALL'
                dataset = DataLayer.Members.GetMembersByPlan(plan_id);
            bSource.DataSource = dataset;
            view.MembersGrid.DataSource = bSource;

            // set personal trainer filter combobox to default value
            view.SelectedPersonalTrainerIndex = 0;
        }

        public void FilterByPersonalTrainer()
        {
            // retrieve the members who are assigned to the the selected trainer and bind them to datagridview            
            int trainer_id = view.SelectedPersonalTrainerIndex;

            // get all members and bind them to the datagridview
            BindingSource bSource = new BindingSource();
            if (trainer_id != 0)                                                           // if the selected trainer is not set to 'All'
                dataset = DataLayer.Members.GetMembersByPersonalTrainer(trainer_id);
            else
                dataset = DataLayer.Members.GetAllMembers();
            bSource.DataSource = dataset;
            view.MembersGrid.DataSource = bSource;

            // set plan filter combobox to default value
            view.SelectedPlanIndex = 0;
        }

        public void SearchAction()
        {

            if (!String.IsNullOrWhiteSpace( view.Keyword ))
            {
                BindingSource bSource = new BindingSource();
                dataset = DataLayer.Members.AdvancedSearch(view.SearchBy, view.Keyword);
                bSource.DataSource = dataset;
                view.MembersGrid.DataSource = bSource;
            }
            else
            {
                RefreshAllMembersDataGrid();
            }
        }

        public void Refresh()
        {
            RefreshAllMembersDataGrid();
            view.FFirstName = String.Empty;
            view.FLastName = String.Empty;
        }

        // reloads data to AllMembers datagridview to refresh
        public void RefreshAllMembersDataGrid()
        {
            // get all members and bind them to the members datagridview to reload
            BindingSource bSource = new BindingSource();
            dataset = DataLayer.Members.GetAllMembers();
            bSource.DataSource = dataset;
            view.MembersGrid.DataSource = bSource;

            //set comboboxes to default value
            view.SelectedSearchByIndex = 0;
            view.SelectedPlanIndex = 0;
        }

        public void Export()
        {
            view.ExportFileDialog.InitialDirectory = "C:";
            view.ExportFileDialog.Title = "Save as Excel File";
            view.ExportFileDialog.FileName = "data";
            view.ExportFileDialog.Filter = "Excel Files(2003)|*.xls";
            if (view.ExportFileDialog.ShowDialog() != DialogResult.Cancel)
            {
                Util.Common.ToCsV(view.MembersGrid, view.ExportFileDialog.FileName);
                MessageBox.Show("Data exported successfully.");
            }
        }

        public void LoadMember()
        {
            
            var id = view.SelectedMember;    // get member's id from selected row

            DataLayer.Members members = new DataLayer.Members();
            DataLayer.Member member = new DataLayer.Member();

            try
            {
                member = members.GetMember(id);     // retrieves member data from db

                // populate controls with the data  
                view.CardNumber = member.CardNumber;
                //txtCardNumber2.Text = member.CardNumber.ToString();
                view.LastName = member.LName;
                view.FirstName = member.FName;

                if (member.Sex == "male")
                    view.Male_IsSelected = true;
                else if (member.Sex == "female")
                    view.Female_IsSelected = true;

                view.DateOfBirth = member.DateOfBirth;
                view.Street = member.Street;
                view.Suburb = member.Suburb;
                view.City = member.City;
                view.PostalCode = member.PostalCode;
                view.HomePhone = member.HomePhone;
                view.CellPhone = member.CellPhone;
                view.Email = member.Email;
                view.Occupation = member.Occupation;
                view.Notes = member.Notes;
                view.PersonalTrainer = member.PersonalTrainer;
                view.MemberId = id;

                //display the member's picture
                view.MemberImage = null; // clears the picturebox
                byte[] img = member.Image;
                if (member.Image != null)
                {
                    try
                    {
                        MemoryStream mstream = new MemoryStream(img);
                        view.MemberImage = Image.FromStream(mstream);
                    }
                    catch { }
                }

                //load membership data
                LoadMemberships(id);

                //resetTextBoxes();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void LoadMemberships(int id)
        {
            //load membership data
            DataTable table = DataLayer.Memberships.GetMembershipByMemberId(id);
            view.MembershipsGridDataSource = table;

            // add column to show if membership is active
            table.Columns.Add(new DataColumn("Status", typeof(string)));

            // loop through all rows to calculate and display if membership is active
            foreach (DataGridViewRow row in view.MDGVRows)
            {
                DateTime end_date = Convert.ToDateTime(row.Cells["End Date"].Value).Date;
                DateTime now = DateTime.Now.Date;
                TimeSpan diff = now - end_date;
                if (diff.TotalDays > 0)
                {
                    row.Cells["Status"].Value = "Inactive";
                    // dataGridViewMemberships.Columns["Status"].DefaultCellStyle.ForeColor = Color.Red;
                }

                else
                {
                    row.Cells["Status"].Value = "Active";
                }
            }
        }

        public void SaveMember()
        {
            DataLayer.Members members = new DataLayer.Members();
            DataLayer.Member member = new DataLayer.Member()
            {
                MemberID = view.SelectedMember,
                CardNumber = view.CardNumber,
                LName = view.LastName,
                FName = view.FirstName,
                Sex = view.Female_IsSelected ? "female" : "male",
                DateOfBirth = view.DateOfBirth,
                Street = view.Street,
                Suburb = view.Suburb,
                City = view.City,
                PostalCode = view.PostalCode,
                HomePhone = view.HomePhone,
                CellPhone = view.CellPhone,
                Email = view.Email,
                Occupation = view.Occupation,
                Notes = view.Notes,
                PersonalTrainer = view.PersonalTrainer
            };

            if (view.MemberImageLocation != null)
            {
                FileStream fstream = new FileStream(view.MemberImageLocation, FileMode.Open, FileAccess.Read);
                BinaryReader br = new BinaryReader(fstream);
                member.Image = br.ReadBytes((int)fstream.Length);
            }
            else
            {
                byte[] empty_array = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 };
                member.Image = empty_array;
            }

            if (DataLayer.Members.UpdateMember(member) > 0)
                MessageBox.Show("Member Updated successfully!", "Gym Manager Pro", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("Failed to Update Member!", "Gym Manager Pro", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public void DeleteMember()
        {
            if (view.IsMemberPanelVisible)
            {
                // Deletes the current member
                DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete this member? All related data will be lost!!!", "Gym Manager Pro", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dialogResult == DialogResult.Yes)
                {
                    DataLayer.Members.DeleteMember(view.SelectedMember);
                    MessageBox.Show("Member deleted!", "Gym Manager Pro", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // display the previous member
                    //var member_id = DataLayer.Members.GetPrevMember(view.SelectedMember);
                    SelectedMember = DataLayer.Members.GetPrevMember(view.SelectedMember);
                    //LoadMember(member_id);
                    LoadMember();
                }
            }
            else if (view.IsAllMembersPanelVisible)
            {
                if (view.SelectedRowsCount > 0)
                {
                    //if a row is selected
                    //display confirmation dialog
                    DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete this member? All related data will be lost!!!", "Gym Manager Pro", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (dialogResult == DialogResult.Yes)
                    {
                        // get member's id from selected row
                        //member_id = int.Parse(membersDataGridViewX.SelectedRows[0].Cells[0].Value.ToString());

                        // delete selected member
                        if (DataLayer.Members.DeleteMember(view.SelectedMember) > 0)
                        {
                            MessageBox.Show("Member deleted!", "Gym Manager Pro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            RefreshAllMembersDataGrid();
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a member first!");
                //SwitchToPanel(panelAllMembers);
            }
        }

        public void NextMember()
        {
            // Retrieves and displays the next member (if any)
            if (DataLayer.Members.MemberHasNext(view.SelectedMember))
            {
                view.SelectedMember = DataLayer.Members.GetNextMember(view.SelectedMember);
                LoadMember();
            }
            else
            {
                MessageBox.Show("There are no more members!", "Gym Manager Pro", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public void PrevMember()
        {
            if (DataLayer.Members.MemberHasPrevious(view.SelectedMember))
            {
                view.SelectedMember = DataLayer.Members.GetPrevMember(view.SelectedMember);
                LoadMember();
            }
            else
            {
                MessageBox.Show("There are no more members!", "Gym Manager Pro", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public void CheckInMember()
        {
            if (view.IsAllMembersPanelVisible || view.IsMemberPanelVisible) 
            {
                if (view.SelectedMember != 0)             // if a member has been selected
                {
                    if (DataLayer.Members.MemberCheckin(view.SelectedMember) > 0)
                    {
                        MessageBox.Show(view.FirstName +" "+view.LastName + " just Checked-in!", "Gym Manager Pro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Console.Beep();
                        //refresh
                        //SetUpAttedance();
                    }
                    else
                    {
                        MessageBox.Show("Couldnot check-in. Please try again", "Gym Manager Pro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Please select a user first!", "Gym Manager Pro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Please select a member first!");
                //SwitchToPanel(panelAllMembers);
            }
        }

        public void DeleteMembership()
        {
            // deletes/expires the selected membership
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete this membership? The selected membership will expire and the operation cannot be undone!!!", "Gym Manager Pro", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dialogResult == DialogResult.Yes)
            {
                // delete the membership
                DataLayer.Memberships.DeleteMembership(view.SelectedMembership);
                MessageBox.Show("Membership removed!", "Gym Manager Pro", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // reload data
                LoadMemberships(view.SelectedMember);
            }
        }
    }
}
