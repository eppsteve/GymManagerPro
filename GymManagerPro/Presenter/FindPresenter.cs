using GymManagerPro.View;
using System;
using System.Data;
using System.Windows.Forms;

namespace GymManagerPro.Presenter
{
    public class FindPresenter
    {
        private IFind view;
        DataTable dataset;

        public FindPresenter(IFind findView)
        {
            view = findView;
            InitDataGrid();
        }

        private void InitDataGrid()
        {
            // get all members and bind them to the members datagridview
            BindingSource bSource = new BindingSource();
            dataset = DataLayer.Members.GetAllMembers();
            bSource.DataSource = dataset;
            view.MembersGridDataSource = bSource;
        }

        public void FilterByLastName()
        {
            // filter datagridview data by last name
            DataView dv = new DataView(dataset);
            dv.RowFilter = string.Format("LastName LIKE '%{0}%'", view.LastName);
            view.MembersGridDataSource = dv;
        }

        public void FilterByFirstName()
        {
            DataView dv = new DataView(dataset);
            dv.RowFilter = string.Format("FirstName LIKE '%{0}%'", view.FirstName);
            view.MembersGridDataSource = dv;
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
            view.MembersGridDataSource = bSource;

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
            view.MembersGridDataSource = bSource;

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
                view.MembersGridDataSource = bSource;
            }
            else
            {
                RefreshAllMembersDataGrid();
            }
        }

        public void Refresh()
        {
            RefreshAllMembersDataGrid();
            view.FirstName = String.Empty;
            view.LastName = String.Empty;
        }

        // reloads data to AllMembers datagridview to refresh
        public void RefreshAllMembersDataGrid()
        {
            // get all members and bind them to the members datagridview to reload
            BindingSource bSource = new BindingSource();
            dataset = DataLayer.Members.GetAllMembers();
            bSource.DataSource = dataset;
            view.MembersGridDataSource = bSource;

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
                Util.Common.ToCsV(view.MembersGridRows, view.MembersGridColumns, view.ExportFileDialog.FileName);
                MessageBox.Show("Data exported successfully.");
            }
        }
    }
}
