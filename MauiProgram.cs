using Microsoft.Extensions.Logging;
using LifeHub.Services;
using LifeHub.ViewModels.HabitTracker;
using LifeHub.Views.HabitTracker;
using LifeHub.ViewModels.Planner;
using LifeHub.Views.Planner;	
using LifeHub.ViewModels.MoodJournal;
using LifeHub.Views.MoodJournal;
using LifeHub.ViewModels.Dashboard;
using LifeHub.Views.Dashboard;


namespace LifeHub;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		// 1. Servis Kaydı (Singleton olması önemli)
		builder.Services.AddSingleton<IHabitService, HabitService>();
		builder.Services.AddSingleton<IPlannerService, PlannerService>();
		builder.Services.AddSingleton<IMoodService, MoodService>();


		// 2. ViewModel Kayıtları
		builder.Services.AddTransient<HabitListViewModel>();
		builder.Services.AddTransient<HabitAddViewModel>();
		builder.Services.AddTransient<HabitEditViewModel>();
		builder.Services.AddTransient<PlannerViewModel>();
		builder.Services.AddTransient<MoodJournalViewModel>();
		builder.Services.AddTransient<MoodHistoryViewModel>();
		builder.Services.AddSingleton<DashboardViewModel>();
		builder.Services.AddTransient<StatsViewModel>();

		// 3. Sayfa (View) Kayıtları
		builder.Services.AddTransient<HabitListPage>();
		builder.Services.AddTransient<HabitAddPage>();
		builder.Services.AddTransient<HabitEditPage>();
		builder.Services.AddTransient<PlannerListPage>();
		builder.Services.AddTransient<MoodJournalPage>();
		builder.Services.AddTransient<MoodHistoryPage>();
		builder.Services.AddSingleton<DashboardPage>();
		builder.Services.AddTransient<StatsPage>();

		
	
	

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
