using DevComponents.DotNetBar.Controls;
using System;
using System.Windows.Forms;

namespace GymManagerPro.View
{
    public interface ITrainer
    {
        int TrainerID { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
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
        decimal Salary { get; set; }
        string Notes { get; set; }

        ListBox lbTrainers { get; set; }
        DataGridViewX dgLinkedMembers { get; set; }
        bool IsPanelVisible { get; set; }
    }
}
