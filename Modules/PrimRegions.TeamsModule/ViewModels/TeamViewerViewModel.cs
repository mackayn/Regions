using System.Threading.Tasks;
using Prism.Commands;
using Prism.Navigation;
using PrismRegions.Framework.Model;
using PrismRegions.Framework.Mvvm;

namespace PrismRegions.TeamModule.ViewModels
{
    public class TeamViewerViewModel : BaseViewModel
    {
        private NavigationItem _selectedTeam;
        public NavigationItem SelectedTeam
        {
            get => _selectedTeam;
            set => SetProperty(ref _selectedTeam, value);
        }

        public DelegateCommand NavigateToCarsCommand { get; }
        public DelegateCommand NavigateToDriversCommand { get; }
        public DelegateCommand NavigateToChampionship { get; }

        public TeamViewerViewModel(IBaseContainer baseContainer) : base(baseContainer)
        {
            NavigateToCarsCommand = new DelegateCommand(async ()=> await NavigateToRegion(KnownRegions.CarsRegion)).ObservesCanExecute(() => CanExecute);
            NavigateToDriversCommand = new DelegateCommand(async () => await NavigateToRegion(KnownRegions.DriversRegion)).ObservesCanExecute(() => CanExecute);
            NavigateToChampionship = new DelegateCommand(async () => await NavigateToChampionshipPage()).ObservesCanExecute(() => CanExecute);
        }

        internal async Task NavigateToRegion(string regionName)
        {
            Busy = true;
            var navContext = (NavigationItem)SelectedTeam.Clone();
            await Regions.NavigateToRegion(regionName, new NavigationParameters {{ KnownParameters.TeamParam, navContext }});
            Busy = false;
        }

        internal async Task NavigateToChampionshipPage()
        {
            Busy = true;
            await Navigation.NavigateAsync("NavigationPage/ChampionshipPage", null, true, false);
            Busy = false;
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            if (parameters.ContainsKey(KnownParameters.TeamParam))
            {
                SelectedTeam = parameters.GetValue<NavigationItem>(KnownParameters.TeamParam);
            }

            base.OnNavigatedTo(parameters);
        }

        public override void Destroy()
        {
            base.Destroy();
            SelectedTeam = null;
        }
    }
}
