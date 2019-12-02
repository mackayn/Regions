using System.Threading.Tasks;
using Prism.Commands;
using Prism.Navigation;
using PrismRegions.Framework.Model;
using PrismRegions.Framework.Mvvm;

namespace PrismRegions.Shell.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        public DelegateCommand NavigateToRegionsCommand { get; }

        public MainPageViewModel(IBaseContainer baseContainer)
            : base(baseContainer)
        {
            NavigateToRegionsCommand = new DelegateCommand(async () => await NavigateToTeamsModule()).ObservesCanExecute(() => CanExecute);
        }

        internal async Task NavigateToTeamsModule()
        {
            Busy = true;
            await base.Navigation.NavigateAsync(KnownPages.TeamsPage, null, false, false);
            Busy = false;
        }
    }
}
