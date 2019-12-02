using Prism.Mvvm;
using Prism.Navigation;
using PrismRegions.Framework.Mvvm.Regions;

namespace PrismRegions.Framework.Mvvm
{
    public class BaseViewModel : BindableBase, IInitialize, INavigationAware, IDestructible
    {
        private readonly IBaseContainer _baseContainer;

        private bool _busy;
        public bool Busy
        {
            get => _busy;
            set
            {
                if (SetProperty(ref _busy, value))
                {
                    RaisePropertyChanged(nameof(CanExecute));
                }
            }
        }

        public bool CanExecute => !Busy;

        public BaseViewModel(IBaseContainer baseContainer)
        {
            _baseContainer = baseContainer;
        }

        public virtual void Initialize(INavigationParameters parameters)
        {
            Busy = true;
        }

        public virtual void OnNavigatedFrom(INavigationParameters parameters)
        {

        }

        public virtual void OnNavigatedTo(INavigationParameters parameters)
        {
            Busy = false;
        }

        public virtual void Destroy()
        {

        }

        public IRegionService Regions => _baseContainer.RegionService;
        public INavigationService Navigation => _baseContainer.NavigationService;
    }
}
