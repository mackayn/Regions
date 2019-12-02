using Prism.Ioc;
using Prism.Modularity;
using PrismRegions.DriverModule.Views;
using PrismRegions.DriverModule.ViewModels;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace PrismRegions.DriverModule
{
    public class DriverModule : IModule
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
