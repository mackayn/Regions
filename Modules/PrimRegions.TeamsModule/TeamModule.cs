using Prism.Ioc;
using Prism.Modularity;
using PrismRegions.TeamModule.ViewModels;
using PrismRegions.TeamModule.Views;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace PrismRegions.TeamModule
{
    public class TeamModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {

        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<TeamsPage, TeamsPageViewModel>();
        }
    }
}
