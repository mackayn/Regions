using Prism;
using Prism.Ioc;
using Prism.Modularity;
using PrismRegions.Framework.Mvvm;
using PrismRegions.Framework.Mvvm.Regions;
using PrismRegions.Shell.ViewModels;
using PrismRegions.Shell.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace PrismRegions.Shell
{
    public partial class App
    {
        /* 
         * The Xamarin Forms XAML Previewer in Visual Studio uses System.Activator.CreateInstance.
         * This imposes a limitation in which the App class must have a default constructor. 
         * App(IPlatformInitializer initializer = null) cannot be handled by the Activator.
         */
        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            await NavigationService.NavigateAsync("NavigationPage/MainPage");
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            // Register modules here
            moduleCatalog.AddModule<PrismRegions.CarModule.CarModule>();
            moduleCatalog.AddModule<PrismRegions.DriverModule.DriverModule>();
            moduleCatalog.AddModule<PrismRegions.TeamModule.TeamModule>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IRegionService, RegionService>();
            containerRegistry.Register<IBaseContainer, BaseContainer>();
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
        }
    }
}
