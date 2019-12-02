using Prism.Navigation;
using PrismRegions.Framework.Mvvm.Regions;

namespace PrismRegions.Framework.Mvvm
{
    public class BaseContainer : IBaseContainer
    {
        public BaseContainer(INavigationService navigationService, IRegionService regionService)
        {
            NavigationService = navigationService;
            RegionService = regionService;
        }

        public IRegionService RegionService { get; set; }
        public INavigationService NavigationService { get; set; }
    }
}
