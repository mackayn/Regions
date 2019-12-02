using Prism.Ioc;
using Prism.Modularity;
using PrismRegions.CarModule.Views;
using PrismRegions.CarModule.ViewModels;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace PrismRegions.CarModule
{
    public class CarModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {

        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<ViewA, ViewAViewModel>();
        }
    }
}
