using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CustomerDetails.Core; 
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CustomerDetails.Providers.Provider;
using MahApps.Metro.Controls.Dialogs;

namespace CustomerDetails.ViewModels;

public class MainPageViewModel : ObservableObject
{
    public MainPageViewModel(IGetProfessionProvider getProfessionProvider,
        IGetAllPersonsProvider getAllPersonProvider, IAddPersonsProvider addPersonsProvider,
        IUpdatePersonsProvider updatePersonsProvider, IDeletePersonsProvider deletePersonsProvider, IDialogCoordinator instance)
    {
        _getProfessionProvider = getProfessionProvider;
        _getAllPersonProvider = getAllPersonProvider;
        _addPersonsProvider = addPersonsProvider;
        _updatePersonsProvider = updatePersonsProvider;
        _deletePersonsProvider = deletePersonsProvider;
        this.dialogCoordinator = instance;
        AddSelectedPerson = new AsyncRelayCommand(AddSelectedPersonAsync);
        GetApplicationData = new AsyncRelayCommand(GetApplicationDataAsync);
        UpdateSelectedPerson = new AsyncRelayCommand(UpdateSelectedPersonAsync);

        DeleteSelectedPerson = new AsyncRelayCommand(DeleteSelectedPersonAsync);

        var result = Task.Run(async () => await InitAsync());


       



    }

    public async Task InitAsync()
    {
        await GetApplicationData.ExecuteAsync(true);

        SelectedPerson = Persons.FirstOrDefault();
    }

    private ObservableCollection<Person> _persons;
    public ObservableCollection<Person> Persons
    {
        get => _persons;
        set => SetProperty(ref _persons, value);
    }

    private ObservableCollection<Profession> _profession;
    public ObservableCollection<Profession> Profession
    {
        get => _profession;
        set => SetProperty(ref _profession, value);
    }


    private Person _selectedPerson;
    public Person SelectedPerson
    {
        get => _selectedPerson;
        set
        {
            SetProperty(ref _selectedPerson, value);
        }
    }

    public IAsyncRelayCommand GetApplicationData { get; }
    public IAsyncRelayCommand AddSelectedPerson { get; }
    public IAsyncRelayCommand DeleteSelectedPerson { get; }
    public IAsyncRelayCommand UpdateSelectedPerson { get; }
    private IDialogCoordinator dialogCoordinator;
    private readonly IGetProfessionProvider _getProfessionProvider;
    private readonly IGetAllPersonsProvider _getAllPersonProvider;
    private readonly IAddPersonsProvider _addPersonsProvider;
    private readonly IUpdatePersonsProvider _updatePersonsProvider;
    private readonly IDeletePersonsProvider _deletePersonsProvider;

    private async Task GetApplicationDataAsync()
    {
        Profession = new ObservableCollection<Profession>(await _getProfessionProvider.GetProductAsync());
        Persons = new ObservableCollection<Person>(await _getAllPersonProvider.GetAsync());

        SelectedPerson = Persons.FirstOrDefault();
    }

    private async Task AddSelectedPersonAsync()
    {
        var result = await _addPersonsProvider.PostAsync(SelectedPerson);

        Persons.Add(result);

        await this.dialogCoordinator.ShowMessageAsync(this, "Test App", "Selected Person Add");
    }

    private async Task UpdateSelectedPersonAsync()
    {
        var result = await _updatePersonsProvider.PostAsync(SelectedPerson);
        await this.dialogCoordinator.ShowMessageAsync(this, "Test App", "Selected Person Updated");
    }

    private async Task DeleteSelectedPersonAsync()
    {
        if (Persons.Count == 1)
        {
            await this.dialogCoordinator.ShowMessageAsync(this, "Sorry", "Need one Person for demo Puroposes");
            return;
        }

        var result = await _deletePersonsProvider.DeleteAsync(SelectedPerson);

        if (result)
        {
            Persons.Remove(SelectedPerson);
            SelectedPerson = Persons.FirstOrDefault();
            await this.dialogCoordinator.ShowMessageAsync(this, "Test App", "Selected Person Removed");
        }
    }
}
