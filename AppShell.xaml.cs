namespace LifeHub;
using LifeHub.Views.HabitTracker; 
using LifeHub.Views.Planner;
using LifeHub.Views.MoodJournal;
using LifeHub.Views.Dashboard;


public partial class AppShell : Shell
{
	public AppShell()
{
    InitializeComponent();
    // Sayfalar arası geçiş rotalarını tanımlıyoruz
    Routing.RegisterRoute(nameof(HabitAddPage), typeof(HabitAddPage));
    Routing.RegisterRoute(nameof(HabitEditPage), typeof(HabitEditPage));
    Routing.RegisterRoute(nameof(PlannerSettingsPage), typeof(PlannerSettingsPage));
    Routing.RegisterRoute(nameof(MoodHistoryPage), typeof(MoodHistoryPage));
    // Routing.RegisterRoute(nameof(DashboardPage), typeof(DashboardPage));
    // Routing.RegisterRoute(nameof(MoodJournalPage), typeof(MoodJournalPage));
    // Routing.RegisterRoute(nameof(HabitListPage), typeof(HabitListPage));
    // Routing.RegisterRoute(nameof(PlannerListPage), typeof(PlannerListPage));
    Routing.RegisterRoute(nameof(StatsPage), typeof(StatsPage));


}
}
