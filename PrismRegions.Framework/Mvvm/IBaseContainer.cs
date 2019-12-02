using Prism.Navigation;
using PrismRegions.Framework.Mvvm.Regions;

namespace PrismRegions.Framework.Mvvm
{
    public interface IBaseContainer
    {
        IRegionService RegionService { get; set; }
        INavigationService NavigationService { get; set; }
    }
}
