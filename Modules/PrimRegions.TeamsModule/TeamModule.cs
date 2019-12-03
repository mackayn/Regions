using Prism.Ioc;
using Prism.Modularity;
using PrismRegions.Framework.Model;
using PrismRegions.Framework.Mvvm.Regions;
using PrismRegions.TeamModule.Components;
using PrismRegions.TeamModule.ViewModels;
using PrismRegions.TeamModule.Views;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace PrismRegions.TeamModule
{
    public class TeamModule : IModule
    {

        private readonly IRegionService _regionService;

        public TeamModule(IRegionService regionService)
        {
            _regionService = regionService;
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            _regionService.RegisterRegion(new PageRegion {RegionName = KnownRegions.TeamsRegion, RegionType = (typeof(TeamViewer))});
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<TeamsPage, TeamsPageViewModel>();
            containerRegistry.RegisterForNavigation<ChampionshipPage, ChampionshipPageViewModel>();
        }
    }
}
