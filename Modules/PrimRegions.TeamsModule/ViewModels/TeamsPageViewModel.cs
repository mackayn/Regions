using Prism.Mvvm;

namespace PrismRegions.TeamModule.ViewModels
{
    public class TeamsPageViewModel : BindableBase
    {
        private string _title;
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        public TeamsPageViewModel()
        {
            Title = "View A";
        }
    }
}
