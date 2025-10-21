using CommunityToolkit.Mvvm.Messaging;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using ScheduleAndToDo.Services;
using ScheduleAndToDo.ViewModels;
using ScheduleAndToDo.Views.Message;
using ScheduleAndToDo.Views;
namespace ScheduleAndToDo;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    public static IServiceProvider? ServiceProvider { get; set; }
    private Window? _window;
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        var services = new ServiceCollection();
        services.AddSingleton<INavigationService,NavigationService>();
        services.AddSingleton<MainWindowViewModel>();
        services.AddSingleton<ScheduleWindowViewModel>();
        services.AddSingleton<ScheduleAddWindowViewModel>();
        services.AddSingleton<ToDoWindowViewModel>();
        services.AddSingleton<DayScheduleWindowViewModel>();
        services.AddSingleton<MonthScheduleWindowViewModel>();

        services.AddTransient<MainWindow>();
        services.AddTransient<ToDoWindow>();
        services.AddTransient<ScheduleAddWindow>();
        services.AddTransient<ScheduleWindow>();
        services.AddTransient<ToDoWindow>();
        services.AddTransient<DayScheduleWindow>();
        services.AddTransient<MonthScheduleWindow>();

        ServiceProvider = services.BuildServiceProvider();

        var mainWindow=ServiceProvider.GetRequiredService<MainWindow>();
        mainWindow.Show();
        _window = mainWindow;
        WeakReferenceMessenger.Default.Register<NavigationMessage>(this, (r, msg) =>
        {
            switch (msg.Target)
            {
                //スケジュール管理ページに遷移
                //メインページに遷移
                case "Main":
                    NavigateTo<MainWindow, MainWindowViewModel>();
                    break;
                case "Schedule":
                    NavigateTo<ScheduleWindow,ScheduleWindowViewModel>();
                    
                    break;
                //ToDoリストページに遷移
                case "Todo":
                    NavigateTo<ToDoWindow, ToDoWindowViewModel>();
                    break;
                //スケジュール追加ページに遷移
                case "ScheduleAdd":
                    NavigateTo<ScheduleAddWindow,ScheduleAddWindowViewModel>();
                    break;
                case "DaySchedule":
                    NavigateTo<DayScheduleWindow,DayScheduleWindowViewModel>();
                    break;
                case "MonthSchedule":
                    NavigateTo<MonthScheduleWindow,MonthScheduleWindowViewModel>();
                    break;
                case "ReloadSchedule":
                    var vm = ServiceProvider.GetRequiredService<ScheduleWindowViewModel>();
                    vm.LoadSchedule();
                    break;
            }
        });
    }
    private void NavigateTo<TWindow,TViewModel>()
        where TWindow : Window
        where TViewModel : class
    {
        var viewModel = ServiceProvider!.GetRequiredService<TViewModel>();
        var window = ServiceProvider!.GetRequiredService<TWindow>();
        window.DataContext = viewModel;
        if (viewModel is ScheduleAddWindowViewModel svm)
        {   
            
        }
        window.Show();//新しいページを開く
        _window?.Close();//前に開いていたWindowを閉じる
        _window = window;//現在開いているWindowを保存しておく
    }
}
