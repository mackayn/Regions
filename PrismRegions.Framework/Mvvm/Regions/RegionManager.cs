using Prism.Mvvm;
using Prism.Navigation;
using Xamarin.Forms;

namespace PrismRegions.Framework.Mvvm.Regions
{
    public class RegionManager : ContentView, IRegionManager
    {
        public void DestroyRegion()
        {
            if (Equals(Content, null))
            {
                return;
            }

            var currentView = GetCurrentView();
            ViewModelLocator.SetAutowirePartialView(currentView, null);

            // Cleanup the viewmodel
            if (Content.BindingContext is IDestructible vm)
            {
                vm.Destroy();
            }

            Content.BindingContext = null;

            // Cleanup the view/component
            if (Content is IDestructible lifetime)
            {
                lifetime.Destroy();
            }

            Content.Behaviors?.Clear();
            Content.Resources?.Clear();

            Device.BeginInvokeOnMainThread(() =>
            {
                Content = null;
            });
        }

        public void SetRegion(View view)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                Content = view;
            });
        }

        public View GetCurrentView()
        {
            return Content;
        }
    }
}