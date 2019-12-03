using Xamarin.Forms;

namespace PrismRegions.Framework.Mvvm.Regions
{
    public interface IRegionManager
    {
        /// <summary>
        /// Cleans up view in region and binding context
        /// </summary>
        void DestroyRegion();

        /// <summary>
        /// Set the view to display in the region
        /// </summary>
        /// <param name="view"></param>
        void SetRegion(View view);

        /// <summary>
        /// Returns the currently loaded view in the region
        /// </summary>
        /// <returns>View</returns>
        View GetCurrentView();
    }
}
