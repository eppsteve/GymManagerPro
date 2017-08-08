using GymManagerPro.DataLayer;
using GymManagerPro.View;
using System;
using System.Linq;
using System.Windows.Forms;

namespace GymManagerPro.Presenter
{
    public class PlanPresenter
    {
        IPlan view;
        Plan selectedPlan;

        public PlanPresenter(IPlan View)
        {
            this.view = View;
            LoadPlans();
        }

        private void LoadPlans()
        {
            view.lbPlans.DataSource = DataLayer.Plan.GetAllPlans().ToList();
            view.lbPlans.ValueMember = "Key";
            view.lbPlans.DisplayMember = "Value";
            view.lbPlans.ClearSelected();
        }

        public void ChangeSelectedPlan()
        {
            if (view.lbPlans.SelectedIndex != -1)
            {
                // get selected plan
                int planId = Convert.ToInt32(view.lbPlans.SelectedValue.ToString());
                selectedPlan = DataLayer.Plan.GetPlan(planId);

                //populate textboxes with plan's data
                view.PlanName = selectedPlan.Name;
                view.Duration = selectedPlan.Duration;
                view.Price = selectedPlan.Price;
                view.Notes = selectedPlan.Notes;
            }
        }

        public void NewPlan()
        {
            new EditPlan(view.lbPlans).ShowDialog();
            //SwitchToPanel(panelPlans);
        }

        public void EditPlan()
        {
            if (view.IsPlansPanelVisible)
            {
                if (view.lbPlans.SelectedIndex != -1)
                {
                    new EditPlan(selectedPlan, view.lbPlans).ShowDialog();
                }
            }
            else
            {
                MessageBox.Show("Please select a plan first");
                //SwitchToPanel(panelPlans);
            }
        }

        public void DeletePlan()
        {
            if (view.IsPlansPanelVisible)
            {
                DialogResult dialogResult = MessageBox.Show("Warning! Deleting the selected plan will also expire (delete) all the associated memberships! Are you sure you want to continue?", "Gym Manager Pro", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                if (dialogResult == DialogResult.Yes)
                {
                    DataLayer.Plan.DeletePlan(selectedPlan.Id);

                    // refresh the listbox
                    LoadPlans();
                }
            }
            else
            {
                MessageBox.Show("Please select a plan first!");
                //SwitchToPanel(panelPlans);
            }
        }
    }
}
