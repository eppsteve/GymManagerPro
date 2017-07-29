using System.Windows.Forms;

namespace GymManagerPro.View
{
    public interface IPlan
    {
        string PlanName { get; set; }
        int Duration { get; set; }
        decimal Price { get; set; }
        string Notes { get; set; }
        bool IsPlansPanelVisible { get; set; }
        ListBox lbPlans { get; set; }
    }
}
