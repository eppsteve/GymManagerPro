using System;
using System.Windows.Forms;

namespace GymManagerPro.View
{
    public interface IWizard
    {
        string FirstName { get; }
        string LastName { get; }
        int CardNumber { get; }
        bool Male_IsSelected { get; }
        bool Female_IsSelected { get; }
        DateTime DateOfBirth { get; }
        string Street { get; }
        string Suburb { get; }
        string City { get; }
        int PostalCode { get; }
        string HomePhone { get; }
        string CellPhone { get; }
        string Email { get; }
        string Occupation { get; }
        bool IsMaleChecked { get; }
        bool IsFemaleChecked { get; }
        string ImageLocation { get; }
        //int PersonalTrainer { get; }
        string Notes { get; }
        //Image MemberImage { get; }
        //string MemberImageLocation { get; }
        //public byte[] Image { get; }
        string SelectedPlanName { get; }
        int PlanId { get; }
        DateTime MembershipStart { get; }
        DateTime MembershipEnd { get; }
        bool IsPanelVisible { get; set; }
        decimal InitializationFee { get; set; }
        decimal TotalFee { get; set; }

        ComboBox cbWizardPlans { get; set; }
    }
}
