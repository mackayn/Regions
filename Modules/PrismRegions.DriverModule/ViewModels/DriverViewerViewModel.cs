using Prism.Navigation;
using PrismRegions.Framework.Model;
using PrismRegions.Framework.Mvvm;

namespace PrismRegions.DriverModule.ViewModels
{
    public class DriverViewerViewModel : BaseViewModel
    {
        private NavigationItem _selectedTeam;
        public NavigationItem SelectedTeam
        {
            get => _selectedTeam;
            set => SetProperty(ref _selectedTeam, value);
        }

        public DriverViewerViewModel(IBaseContainer baseContainer) : base(baseContainer)
        {
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            if (parameters.ContainsKey(KnownParameters.TeamParam))
            {
                SelectedTeam = parameters.GetValue<NavigationItem>(KnownParameters.TeamParam);
            }

            base.OnNavigatedTo(parameters);
        }
    }
}

