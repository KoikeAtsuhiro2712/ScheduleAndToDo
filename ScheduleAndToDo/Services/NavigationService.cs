using Microsoft.Extensions.DependencyInjection;
using ScheduleAndToDo.ViewModels;
using System.Windows;
namespace ScheduleAndToDo.Services;

public class NavigationService : INavigationService
{
    public void Navigate<TViewModel>(object parameter = null)
    {
        Window window = null;
        if (window!=null)
        {
            if (parameter != null && window.DataContext is BaseViewModel vm)
            {
                vm.OnNavigatedTo(parameter);
            }
        }
        window.Show();
    }
}