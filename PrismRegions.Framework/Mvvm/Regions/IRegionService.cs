using Prism.Navigation;
using PrismRegions.Framework.Model;
using System.Threading.Tasks;

namespace PrismRegions.Framework.Mvvm.Regions
{
    public interface IRegionService
    {
        /// <summary>
        /// Gets the current region name
        /// </summary>
        string CurrentRegion { get; }

        /// <summary>
        /// Register a dynamic region
        /// </summary>
        /// <param name="item">region to add</param>
        void RegisterRegion(PageRegion item);

        /// <summary>
        /// Navigate to a region in a page
        /// </summary>
        /// <param name="region">region to navigate to</param>
        /// <param name="navParams">parameters</param>
        /// <returns>Navigation result</returns>
        Task<NavigationResult> NavigateToRegion(string region, INavigationParameters navParams);
    }
}
