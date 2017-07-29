using System.Windows.Forms;

namespace GymManagerPro.View
{
    public interface IFind
    {
        string FirstName { get; set; }
        string LastName { get; set; }
        int SelectedPlanIndex { get; set; }
        int SelectedPersonalTrainerIndex { get; set; }
        int SelectedSearchByIndex { get; set; }
        string SearchBy { get; }
        string Keyword { get; set; }       
        object MembersGridDataSource { get; set; }
        DataGridViewRowCollection MembersGridRows { get; }
        DataGridViewColumnCollection MembersGridColumns { get; }
        SaveFileDialog ExportFileDialog { get; }
    }
}
