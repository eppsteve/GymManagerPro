using System;
using System.Windows.Forms;

namespace GymManagerPro.View
{
    public interface IWizard
    {
        string FirstName { get; set; }
        string LastName { get; set; }
        int CardNumber { get; set; }
        DateTime DateOfBirth { get; set; }
        string Street { get; set; }
        string Suburb { get; set; }
        string City { get; set; }
        int PostalCode { get; set; }
        string HomePhone { get; set; }
        string CellPhone { get; set; }
        string Email { get; set; }
        string Occupation { get; set; }
        bool IsMaleChecked { get; set; }
        bool IsFemaleChecked { get; set; }
        string ImageLocation { get; set; }
        string Notes { get; set; }
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

        DevComponents.DotNetBar.Wizard NewMemberWizard { get; set; }
    }
}
