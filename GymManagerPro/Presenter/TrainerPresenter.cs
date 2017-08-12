using GymManagerPro.View;
using System;
using System.Windows.Forms;

namespace GymManagerPro.Presenter
{
    public class TrainerPresenter
    {
        private ITrainer view;
        private int SelectedTrainer;

        public TrainerPresenter(ITrainer View)
        {
            this.view = View;
            LoadTrainers();
        }

        public void LoadTrainers()
        {
            view.lbTrainers.DataSource = new BindingSource(DataLayer.Trainers.GetAllTrainers(), null);
            view.lbTrainers.DisplayMember = "Value";
            view.lbTrainers.ValueMember = "Key";
        }

        public void ChangeSelectedTrainer()
        {
            if (view.lbTrainers.SelectedIndex != -1)
            {
                //get trainer's id and load trainer's data
                SelectedTrainer = Convert.ToInt32(view.lbTrainers.SelectedValue.ToString());
                LoadTrainer();
            }
        }

        public void LoadTrainer()
        {
           // var trainer = new DataLayer.Trainer();
            try
            {
                var trainer = DataLayer.Trainers.GetTrainer(SelectedTrainer);
                view.FirstName = trainer.FName;
                view.LastName = trainer.LName;
                view.CellPhone = trainer.CellPhone;
                view.City = trainer.City;
                view.DateOfBirth = trainer.DateOfBirth;
                view.Email = trainer.Email;
                view.HomePhone = trainer.HomePhone;
                view.TrainerID = trainer.TrainerID;
                view.Notes = trainer.Notes;
                if (trainer.Sex == "Male")
                    view.Male_IsSelected = true;
                else if (trainer.Sex == "Female")
                    view.Female_IsSelected = true;
                view.Salary = trainer.Salary;
                view.Suburb = trainer.Suburb;
                view.Street = trainer.Street;

                // load associated members
                var membersTable = DataLayer.Trainers.GetAssociatedMembers(SelectedTrainer);
                view.dgLinkedMembers.DataSource = membersTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void AddTrainer()
        {
            // switch to trainers panel

            // create a new trainer
            var trainer = new DataLayer.Trainer()
            {
                FName = "New Trainer",
                LName = "New Trainer",
                Sex = "Male",
                DateOfBirth = DateTime.Now,
                Street = String.Empty,
                Suburb = String.Empty,
                Salary = 0,
                HomePhone = String.Empty,
                CellPhone = String.Empty,
                Notes = String.Empty,
                City = String.Empty,
                Email = String.Empty
            };

            // add to db
            if (DataLayer.Trainers.NewTrainer(trainer) > 0)
            {
                SelectedTrainer = DataLayer.Trainers.GetLastInsertedTrainer();
                LoadTrainers();
                view.lbTrainers.SelectedIndex = view.lbTrainers.Items.Count - 1;
            }
            else
            {
                MessageBox.Show("could not add");
            }

            // set textboxes editable
            //EditTrainer();

        }

        public void SaveTrainer()
        {
            if (view.IsPanelVisible)
            {
                if (view.lbTrainers.SelectedIndex != 0)
                {
                    if (!String.IsNullOrEmpty(view.LastName))
                    {
                        var trainer = new DataLayer.Trainer()
                        {
                            TrainerID = SelectedTrainer,
                            FName = view.FirstName,
                            LName = view.LastName,
                            CellPhone = view.CellPhone,
                            City = view.City,
                            DateOfBirth = view.DateOfBirth,
                            Email = view.Email,
                            HomePhone = view.HomePhone,
                            Salary = view.Salary,
                            Street = view.Street,
                            Suburb = view.Suburb,
                            Sex = view.Male_IsSelected ? "Male" : "Female",
                            Notes = view.Notes
                        };

                        //save to db
                        if (DataLayer.Trainers.UpdateTrainer(trainer) > 0)
                        {
                            MessageBox.Show("Trainer Updated successfully!", "Gym Manager Pro", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // refresh Trainers listbox
                            LoadTrainers();
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

                    //// set textboxes to read only
                    //txtTrainerFName.ReadOnly = true;
                    //txtTrainerLName.ReadOnly = true;
                    //txtTrainerCellPhone.ReadOnly = true;
                    //txtTrainerCity.ReadOnly = true;
                    //txtTrainerEmail.ReadOnly = true;
                    //txtTrainerHomePhone.ReadOnly = true;
                    //txtTrainerNotes.ReadOnly = true;
                    //txtTrainerSalary.ReadOnly = true;
                    //txtTrainerStreet.ReadOnly = true;
                    //txtTrainerSuburb.ReadOnly = true;

                    ////change button text and icon of btnTrainersEdit
                    //btnTrainersEdit.Text = "Edit";
                    //ComponentResourceManager resources = new ComponentResourceManager(typeof(frmMain));
                    //btnTrainersEdit.Icon = ((System.Drawing.Icon)(resources.GetObject("btnTrainersEdit.Icon")));
                }
            }
        }

        public void DeleteMember()
        {
            if (view.IsPanelVisible)
            {
                if (view.lbTrainers.SelectedIndex != 0)
                {
                    DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete this Trainer? This cannot be undone!", "Gym Manager Pro", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (dialogResult == DialogResult.Yes)
                    {
                        if (DataLayer.Trainers.DeleteTrainer(SelectedTrainer) > 0)
                        {
                            MessageBox.Show("Trainer deleted!", "Gym Manager Pro", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // refresh trainers listbox
                            LoadTrainers();
                        }
                        else
                        {
                            MessageBox.Show("Could not delete. Please try again.", "Gym Manager Pro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Please select a trainer first!");
                }
            }
            else
            {
                MessageBox.Show("Please select a trainer first!");
                //SwitchToPanel(panelTrainers);
            }
        }

        public void LinkMember()
        {
            if (view.IsPanelVisible)
            {

                if (view.lbTrainers.SelectedIndex != -1)
                {
                    TrainerAssignmentDialog tad = new TrainerAssignmentDialog(SelectedTrainer, view.dgLinkedMembers);
                    tad.Show();
                }
                else
                {
                    MessageBox.Show("Please select a trainer first!", "Gym Manager Pro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Please select a trainer first!", "Gym Manager Pro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //SwitchToPanel(panelTrainers);
            }
        }

        public void UnlinkMember()
        {
            if (view.IsPanelVisible)
            {
                if (view.dgLinkedMembers.SelectedRows.Count != 0)
                {
                    // get the id of the member to be removed
                    int memberToRemove = (int)view.dgLinkedMembers.SelectedCells[0].Value; //first cell of the selected row contains the id

                    if (DataLayer.Trainers.RemoveMember(memberToRemove) > 0)
                    {
                        MessageBox.Show("Member removed!", "Gym Manager Pro", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // reload associated members
                        var membersTable = DataLayer.Trainers.GetAssociatedMembers(SelectedTrainer);
                        view.dgLinkedMembers.DataSource = membersTable;
                    }
                    else
                    {
                        MessageBox.Show("Failed to remove member. Please try again", "Gym Manager Pro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please Select a Trainer first!");
                //SwitchToPanel(panelTrainers);
            }
        }
    }
}
