using System;
using System.Globalization;
using System.Linq;
using System.Reflection;
using Prism;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Mvvm;
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
            moduleCatalog.AddModule<CarModule.CarModule>(InitializationMode.OnDemand);
            moduleCatalog.AddModule<DriverModule.DriverModule>(InitializationMode.OnDemand);
            moduleCatalog.AddModule<TeamModule.TeamModule>(InitializationMode.OnDemand);
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IRegionService, RegionService>();
            containerRegistry.Register<IBaseContainer, BaseContainer>();
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
        }

        protected override void ConfigureViewModelLocator()
        {
            base.ConfigureViewModelLocator();

            // Override default viewmodel locator
            ViewModelLocationProvider.SetDefaultViewTypeToViewModelTypeResolver(AlternateResolver);
        }

        private static Type AlternateResolver(Type viewType)
        {
            // Look for unknown navigation types, these will be pages with no view-models or ContentViews using Prism auto wire, don't use for regular navigation
            var viewName = viewType.FullName;
            if (string.IsNullOrEmpty(viewName))
            {
                return null;
            }

            // Strip out namespace
            viewName = viewName.Split('.').LastOrDefault();
            var viewAssemblyName = viewType.GetTypeInfo().Assembly.FullName;
            var module = viewType.Assembly.ManifestModule.Name.Replace(".dll", "");
            var viewModelName = string.Format(CultureInfo.InvariantCulture, $"{module}.ViewModels.{viewName}ViewModel, {viewAssemblyName}");
            return Type.GetType(viewModelName);
        }
    }
}
