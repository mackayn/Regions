using PrismRegions.Framework.Model;
using PrismRegions.Framework.Mvvm;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Prism.Commands;
using Prism.Navigation;

namespace PrismRegions.TeamModule.ViewModels
{
    public class TeamsPageViewModel : BaseViewModel
    {
        public ObservableCollection<NavigationItem> Teams { get; private set; } = new ObservableCollection<NavigationItem>();

        private string _title;
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        public DelegateCommand<NavigationItem> SelectTeamCommand { get; }

        public TeamsPageViewModel(IBaseContainer baseContainer) : base(baseContainer)
        {
            Title = "View A";

            SelectTeamCommand = new DelegateCommand<NavigationItem>(async (p) => await NavigateToRegion(p)).ObservesCanExecute(() => CanExecute);
        }

        internal async Task NavigateToRegion(NavigationItem nav)
        {
            Busy = true;
            await Regions.NavigateToRegion(KnownRegions.TeamsRegion, 
                                           new NavigationParameters {{ KnownParameters.TeamParam, nav }});
            Busy = false;
        }

        public override void Initialize(INavigationParameters parameters)
        {
            base.Initialize(parameters);
            if (parameters.GetNavigationMode() == NavigationMode.New)
            {
                SetupTeamsList();
            }
        }

        public override void Destroy()
        {
            Teams?.Clear();
            Teams = null;
        }

        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            if (!parameters.ContainsKey(KnownParameters.TeamParam))
            {
                parameters.Add(KnownParameters.TeamParam, Teams.First());
            }

            await OnNavigatedToAsync(parameters);
        }

        public override async Task OnNavigatedToAsync(INavigationParameters parameters)
        {
            await NavigateToRegion(parameters.GetValue<NavigationItem>(KnownParameters.TeamParam));
            await base.OnNavigatedToAsync(parameters);
        }

        internal void SetupTeamsList()
        {
            Teams.Add(new NavigationItem { KindName = "Mercedes", ImageName = "Mercedes.png" });
            Teams.Add(new NavigationItem { KindName = "Ferrari", ImageName = "Ferrari.png" });
            Teams.Add(new NavigationItem { KindName = "Red Bull", ImageName = "NoLogo.png" });
            Teams.Add(new NavigationItem { KindName = "McLaren", ImageName = "NoLogo.png" });
            Teams.Add(new NavigationItem { KindName = "Renault", ImageName = "Renault.png" });
            Teams.Add(new NavigationItem { KindName = "Torro Rosso", ImageName = "NoLogo.png" });
            Teams.Add(new NavigationItem { KindName = "Racing Point", ImageName = "NoLogo.png" });
            Teams.Add(new NavigationItem { KindName = "Alfa Romeo", ImageName = "NoLogo.png" });
            Teams.Add(new NavigationItem { KindName = "Hass F1", ImageName = "NoLogo.png" });
            Teams.Add(new NavigationItem { KindName = "Williams", ImageName = "NoLogo.png" });
        }
    }
}
