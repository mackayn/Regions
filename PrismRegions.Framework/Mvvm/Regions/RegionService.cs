using DryIoc;
using Prism.Common;
using Prism.Mvvm;
using Prism.Navigation;
using PrismRegions.Framework.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

            await PageUtilities.OnInitializedAsync(myView, navigationArgs);
            regionManager.SetRegion(myView);
            PageUtilities.OnNavigatedTo(myView, navigationArgs);
            CurrentRegion = toNavigate.RegionName;
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

        private static Page GetCurrentPage()
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

            if (region.AutoResolve)
            {
                ViewModelLocator.SetAutowirePartialView(myView, GetCurrentPage());
            }
            else
            {
                myView.BindingContext = _container.Resolve(region.RegionType);
            }

            return myView;
        }
    }
}
