using Xamarin.Forms;

namespace PrismRegions.Framework.Mvvm.Regions
{
    public interface IRegionManager
    {
        void DestroyRegion();

        void SetRegion(View view);
    }
}
