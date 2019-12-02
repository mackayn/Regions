using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Prism.Commands;
using Prism.Modularity;
using Prism.Navigation;
using PrismRegions.Framework.Model;
using PrismRegions.Framework.Mvvm;

namespace PrismRegions.Shell.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        private readonly IModuleManager _moduleManager;
        private readonly IModuleCatalog _moduleCatalog;

        public DelegateCommand NavigateToRegionsCommand { get; }

        private string _information;
        public string Information
        {
            get => _information;
            set => SetProperty(ref _information, value);
        }

        public MainPageViewModel(IBaseContainer baseContainer, IModuleCatalog moduleCatalog, IModuleManager moduleManager)
            : base(baseContainer)
        {
            _moduleManager = moduleManager;
            _moduleCatalog = moduleCatalog;

            NavigateToRegionsCommand = new DelegateCommand(async () => await NavigateToTeamsModule()).ObservesCanExecute(() => CanExecute);
        }

        internal async Task NavigateToTeamsModule()
        {
            Busy = true;
            await base.Navigation.NavigateAsync(KnownPages.TeamsPage, null, false, false);
            Busy = false;
        }

        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            if (parameters.GetNavigationMode() == NavigationMode.New)
            {
                await OnNavigatedToAsync(parameters);
            }

            base.OnNavigatedTo(parameters);
        }

        public override async Task OnNavigatedToAsync(INavigationParameters parameters)
        {
            Information = "Setting up modules";
            await InitialiseModules(_moduleCatalog.Modules.Select(m => m.ModuleName).ToList());
            Information = string.Empty;
            await base.OnNavigatedToAsync(parameters);
        }

        internal async Task InitialiseModules(List<string> modules)
        {
            if (!modules.Any())
            {
                return;
            }

            await Task.Delay(1000); // Just to demonstrate a slow loading module
            await Task.Run(() =>
            {
                foreach (var mod in from mod in modules
                    let module = _moduleCatalog.Modules.FirstOrDefault(m => m.ModuleName == mod)
                    where !Equals(module, null)
                    where module.State != ModuleState.Initialized
                    select mod)
                {
                    _moduleManager.LoadModule(mod);
                }
            }).ConfigureAwait(false);
        }
    }
}
