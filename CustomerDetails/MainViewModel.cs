using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;
using CustomerDetails.ViewModels;

namespace CustomerDetails
{
    public class MainViewModel : ObservableObject
    {
        private ObservableObject? _selectedViewModel;

        public MainViewModel()
        {
           SelectedViewModel = Ioc.Default.GetRequiredService<MainPageViewModel>();
        }

        public ObservableObject? SelectedViewModel 
        { 
            get => _selectedViewModel; 
            set => SetProperty(ref _selectedViewModel, value);
        }
    }
}
