using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using DryIoc;
using Prism.Common;
using Prism.Mvvm;
using Prism.Navigation;
using PrismRegions.Framework.Model;
using Xamarin.Forms;

namespace PrismRegions.Framework.Mvvm.Regions
{
    public class RegionService : IRegionService
    {
        private const string NavigationModeParam = "__NavigationMode";

        private readonly IContainer _container;
        private readonly IList<PageRegion> _knownRegions;

        public RegionService(IContainer container)
        {
            _container = container;
            _knownRegions = new List<PageRegion>();
        }

        public string CurrentRegion { get; private set; }

        public void RegisterRegion(PageRegion item)
        {
            _knownRegions.Add(item);
        }

        public async Task<NavigationResult> NavigateToRegion(string region, INavigationParameters navParams)
        {
            await Task.Delay(100);

            var toInject = _knownRegions.FirstOrDefault(r => r.RegionName == region);
            if (Equals(toInject, null))
            {
                throw new InvalidOperationException($"{region} is not available");
            }

            var currentPage = GetCurrentPage();
            if (Equals(currentPage, null))
            {
                throw new InvalidOperationException("Current page could not be found");
            }

            var children = currentPage.LogicalChildren[0].LogicalChildren;

            foreach (var child in children)
            {
                if (child is IRegionManager regionArea)
                {
                    await DoNavigation(regionArea, toInject, navParams);
                    return new NavigationResult { Exception = null, Success = true };
                }
            }
            throw new InvalidOperationException($"Region '{toInject.RegionName}' Not found");
        }

        private async Task DoNavigation(IRegionManager regionManager, PageRegion toNavigate, INavigationParameters navArgs)
        {
            var navigationArgs = GetValidParameters(navArgs);

            regionManager.DestroyRegion();
            CurrentRegion = string.Empty;

                var myView = GetView(toNavigate);
            ViewModelLocator.SetAutowirePartialView(myView, GetCurrentPage());

            await InvokeBeforeNavigatedEvents(myView, navigationArgs);
            regionManager.SetRegion(myView);
            InvokeAfterNavigatedEvents(myView, navigationArgs);
            CurrentRegion = toNavigate.RegionName;
        }

        private static async Task InvokeBeforeNavigatedEvents(BindableObject viewToShow, INavigationParameters args)
        {
            if (viewToShow.BindingContext is IInitialize nonAsyncInit)
            {
                nonAsyncInit.Initialize(args);
            }

            if (viewToShow.BindingContext is IInitializeAsync asyncInit)
            {
                await asyncInit.InitializeAsync(args);
            }
        }

        private static void InvokeAfterNavigatedEvents(BindableObject viewToShow, INavigationParameters args)
        {
            if (viewToShow.BindingContext is INavigatedAware onNavigated)
            {
                onNavigated.OnNavigatedTo(args);
            }
        }

        private static INavigationParameters GetValidParameters(INavigationParameters parameters)
        {
            if (parameters == null)
            {
                parameters = new NavigationParameters();
            }

            // If we're navigating to regions in a page there won't be a navigation mode so provide one
            var internalParams = (INavigationParametersInternal)parameters;
            if (!internalParams.ContainsKey(NavigationModeParam))
            {
                ((INavigationParametersInternal)parameters).Add(NavigationModeParam, NavigationMode.New);
            }
            return parameters;
        }

        private Page GetCurrentPage()
        {
            try
            {
                return PageUtilities.GetCurrentPage(Application.Current.MainPage);
            }
            catch (Exception)
            {
                return null;
            }
        }

        private View GetView(PageRegion region)
        {
            if (!(Activator.CreateInstance(region.RegionType) is ContentView myView))
            {
                throw new NotSupportedException("Only PartialView region type supported");
            }
            return myView;
        }

        private static Type AlternateResolver(Type viewType)
        {
            var viewName = viewType.FullName;
            if (string.IsNullOrEmpty(viewName))
            {
                return null;
            }

            viewName = viewName.Split('.').LastOrDefault();
            var viewAssemblyName = viewType.GetTypeInfo().Assembly.FullName;
            var module = viewType.Assembly.ManifestModule.Name.Replace(".dll", string.Empty);
            var viewModelName = string.Format(CultureInfo.InvariantCulture, "{0}.ViewModels.{1}ViewModel, {2}", module, viewName, viewAssemblyName);
            var type = Type.GetType(viewModelName);
            return type;
        }
    }
}
