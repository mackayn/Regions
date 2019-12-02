using Prism.Ioc;
using Prism.Modularity;
using PrismRegions.DriverModule.Components;
using PrismRegions.Framework.Model;
using PrismRegions.Framework.Mvvm.Regions;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace PrismRegions.DriverModule
{
    public class DriverModule : IModule
    {
        private readonly IRegionService _regionService;

        public DriverModule(IRegionService regionService)
        {
            _regionService = regionService;
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            _regionService.RegisterRegion(new PageRegion { RegionName = KnownRegions.DriversRegion, RegionType = typeof(DriverViewer) });
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            
        }
    }
}
