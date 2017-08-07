using DevComponents.DotNetBar.Controls;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace GymManagerPro.View
{
    public interface IMember
    {
        string FFirstName { get; set; }
        string FLastName { get; set; }
        int SelectedPlanIndex { get; set; }
        int SelectedPersonalTrainerIndex { get; set; }
        ComboBox cbFindPersonalTrainer { get; set; }
        int SelectedSearchByIndex { get; set; }
        string SearchBy { get; }
        string Keyword { get; set; }       
        DataGridViewX MembersGrid { get; set; }
        SaveFileDialog ExportFileDialog { get; }
        ComboBox cbFindPlan { get; set; }

        int SelectedMember { get; set; }
        int SelectedMembership { get; }
        int MemberId { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        int CardNumber { get; set; }
        bool Male_IsSelected { get; set; }
        bool Female_IsSelected { get; set; }
        DateTime DateOfBirth { get; set; }
        string Street { get; set; }
        string Suburb { get; set; }
        string City { get; set; }
        int PostalCode { get; set; }
        string HomePhone { get; set; }
        string CellPhone { get; set; }
        string Email { get; set; }
        string Occupation { get; set; }
        int PersonalTrainer { get; set; }
        ComboBoxEx cbPersonalTrainer { get; set; }
        string Notes { get; set; }
        Image MemberImage { get; set; }
        string MemberImageLocation { get; set; }
        //public byte[] Image { get; set; }

        object MembershipsGridDataSource { get; set; }
        DataGridViewRowCollection MDGVRows { get; }
        int SelectedRowsCount { get; }
        string MembershipsNotifications { get; set; }

        bool IsMemberPanelVisible { get; set; }
        bool IsAllMembersPanelVisible { get; set; }

        DataGridViewX MeasurementsGrid { get; set; }
        ContextMenuStrip MeasurementsContextMenu { get; set; }
    }
}
