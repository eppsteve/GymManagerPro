using GymManagerPro.View;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace GymManagerPro.Presenter
{
    public class WizardPresenter
    {
        IWizard view;

        public WizardPresenter(IWizard View)
        {
            this.view = View;
            SetUpComboBoxes();
        }

        private void SetUpComboBoxes()
        {
            // fill combobox with all plans, in new member wizard
            SortedDictionary<int, string> plans = new SortedDictionary<int, string>(DataLayer.Plan.GetAllPlans());           // get all the plans and put them to a sorted dictionary
            plans.Remove(0);                                                    //remove 'All' option
            plans.Add(0, "None");                                               // add 'None' option 
            view.cbWizardPlans.DataSource = new BindingSource(plans, null);
            view.cbWizardPlans.DisplayMember = "Value";
            view.cbWizardPlans.ValueMember = "Key";
        }

        public bool AddNewMember()
        {
            // Create a new Member
            var member = new DataLayer.Member()
            {
                CardNumber = view.CardNumber,
                LName = view.LastName,
                FName = view.FirstName,
                Sex = view.IsMaleChecked ? "Male" : "Female",
                DateOfBirth = view.DateOfBirth,
                Street = view.Street,
                Suburb = view.Suburb,
                City = view.City,
                PostalCode = view.PostalCode,
                HomePhone = view.HomePhone,
                CellPhone = view.CellPhone,
                Email = view.Email,
                Occupation = view.Occupation,
                Notes = view.Notes
            };

            // Check if Card Number is valid
            if (DataLayer.Members.CardNumberExists(member.CardNumber))
            {
                MessageBox.Show("This Card Number already exists!", "Gym Manager Pro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            //Check if Last Name is empty
            if (String.IsNullOrWhiteSpace(member.LName))
            {
                MessageBox.Show("The Last Name cannot be empty!", "Gym Manager Pro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            // holds the member's picture
            //byte[] imageBt = null;
            if (view.ImageLocation != null)
            {
                FileStream fstream = new FileStream(view.ImageLocation, FileMode.Open, FileAccess.Read);
                BinaryReader br = new BinaryReader(fstream);
                member.Image = br.ReadBytes((int)fstream.Length);
            }
            else
            {
                byte[] empty_array = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 };
                member.Image = empty_array;
            }


            // Save Member to db
            if (DataLayer.Members.AddNewMember(member) == 0)
            {
                MessageBox.Show("Failed to add new member. Please try again", "Gym Manager Pro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ClearTextBoxes();
                return false;
            }

            // Create Membership for the same member
            if (view.SelectedPlanName != "None")
            {
                // create a new membership and fill with data
                var membership = new DataLayer.Membership()
                {
                    //to find member's id we get the last inserted id and increment by 1 because we haven't inserted the new member yet
                    MemberId = DataLayer.Members.GetLastInsertedMember(),        // member's id
                                                                                 //membership.MemberId++;
                    Plan = view.PlanId,                 // id of the selected plan
                    start = view.MembershipStart,       // when the membership starts
                    end = view.MembershipEnd            // when the membership expires
                };

                // Save Membership to db
                if (DataLayer.Memberships.NewMembership(membership) > 0)
                    MessageBox.Show("A New Member has been added successfully!", "Gym Manager Pro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("Failed to add new membership. Please add manually from Member Manager", "Gym Manager Pro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show("A New Member has been added successfully!", "Gym Manager Pro", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            view.NewMemberWizard.NavigateCancel();
            ClearTextBoxes();
            return true;

        }

        public void AddInitializationFee()
        {
            var plan_id = view.PlanId;
            var plan_fee = DataLayer.Plan.GetPlanPrice(plan_id);                                                // get selected plan's price
            view.TotalFee = plan_fee + view.InitializationFee;
        }

        public void ClearTextBoxes()
        {
            view.CardNumber = 0;
            view.LastName = String.Empty;
            view.FirstName = String.Empty;
            view.IsMaleChecked = true;
            view.Street = String.Empty;
            view.Suburb = String.Empty;
            view.City = String.Empty;
            view.PostalCode = 0;
            view.HomePhone = String.Empty;
            view.CellPhone = String.Empty;
            view.Email = String.Empty;
            view.Occupation = String.Empty;
            view.Notes = String.Empty;
            view.InitializationFee = 0;
            view.cbWizardPlans.SelectedIndex = 0;
        }
    }
}
