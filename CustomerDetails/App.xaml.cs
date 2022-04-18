using CommunityToolkit.Mvvm.DependencyInjection;
using CustomerDetails.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using CustomerDetails.Provider;

namespace CustomerDetails
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var namingConvenionViewFactory = new NamingConvenionViewFactory();
           
            Ioc.Default.ConfigureServices(
                    new ServiceCollection()
                    //Services
                    .AddHttpClient()
                    .AddSingleton<IGetProfessionProvider, GetProfessionProvider>()
                    .AddSingleton<IGetAllPersonsProvider, GetAllPersonsProvider>()
                    .AddSingleton<IAddPersonsProvider, AddPersonsProvider>()
                    .AddSingleton<IUpdatePersonsProvider, UpdatePersonsProvider>()
                    .AddSingleton<IDeletePersonsProvider, DeletePersonsProvider>()
                    .AddTransient<MainPageViewModel>()
                    //WPF
                    .AddSingleton<MainViewModel>()
                    .AddSingleton<IViewFactory>(namingConvenionViewFactory)
                    .BuildServiceProvider());
        }
    }
}
