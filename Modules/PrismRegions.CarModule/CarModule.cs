using Prism.Ioc;
using Prism.Modularity;
using PrismRegions.CarModule.Components;
using PrismRegions.Framework.Model;
using PrismRegions.Framework.Mvvm.Regions;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace PrismRegions.CarModule
{
    public class CarModule : IModule
    {
        private readonly IRegionService _regionService;

        public CarModule(IRegionService regionService)
        {
            _regionService = regionService;
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            _regionService.RegisterRegion(new PageRegion { RegionName = KnownRegions.CarsRegion, RegionType = typeof(CarViewer) });
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
        }
    }
}
