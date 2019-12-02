using Prism.Navigation;
using PrismRegions.Framework.Model;
using PrismRegions.Framework.Mvvm;

namespace PrismRegions.CarModule.ViewModels
{
    public class CarViewerViewModel : BaseViewModel
    {
        private NavigationItem _selectedTeam;
        public NavigationItem SelectedTeam
        {
            get => _selectedTeam;
            set => SetProperty(ref _selectedTeam, value);
        }

        public CarViewerViewModel(IBaseContainer baseContainer) : base(baseContainer)
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
