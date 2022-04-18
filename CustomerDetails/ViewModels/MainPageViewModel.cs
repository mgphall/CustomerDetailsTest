using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CustomerDetails.Core;
using CustomerDetails.Provider;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace CustomerDetails.ViewModels;

public class MainPageViewModel : ObservableObject
{
    public MainPageViewModel(IGetProfessionProvider getProfessionProvider,
        IGetAllPersonsProvider getAllPersonProvider, IAddPersonsProvider addPersonsProvider,
        IUpdatePersonsProvider updatePersonsProvider, IDeletePersonsProvider deletePersonsProvider)
    {
        _getProfessionProvider = getProfessionProvider;
        _getAllPersonProvider = getAllPersonProvider;
        _addPersonsProvider = addPersonsProvider;
        _updatePersonsProvider = updatePersonsProvider;
        _deletePersonsProvider = deletePersonsProvider;

        AddSelectedPerson = new AsyncRelayCommand(AddSelectedPersonAsync);
        GetApplicationData = new AsyncRelayCommand(GetApplicationDataAsync);
        UpdateSelectedPerson = new AsyncRelayCommand(UpdateSelectedPersonAsync);

        DeleteSelectedPerson = new AsyncRelayCommand(DeleteSelectedPersonAsync);

        Task.Run(async () => await InitAsync());
    }

    public async Task InitAsync()
    {
        await GetApplicationData.ExecuteAsync(true);

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

    private readonly IGetProfessionProvider _getProfessionProvider;
    private readonly IGetAllPersonsProvider _getAllPersonProvider;
    private readonly IAddPersonsProvider _addPersonsProvider;
    private readonly IUpdatePersonsProvider _updatePersonsProvider;
    private readonly IDeletePersonsProvider _deletePersonsProvider;

    private async Task GetApplicationDataAsync()
    {
        Profession = new ObservableCollection<Profession>(await _getProfessionProvider.GetProductAsync());
        Persons = new ObservableCollection<Person>(await _getAllPersonProvider.GetAsync());
    }

    private async Task AddSelectedPersonAsync()
    {
        var result = await _addPersonsProvider.PostAsync(SelectedPerson);

        Persons.Add(result);
    }

    private async Task UpdateSelectedPersonAsync()
    {
        var result = await _updatePersonsProvider.PostAsync(SelectedPerson);
    }

    private async Task DeleteSelectedPersonAsync()
    {
        var result = await _deletePersonsProvider.DeleteAsync(SelectedPerson);

        if (result)
        {
            Persons.Remove(SelectedPerson);
        }
    }
}
