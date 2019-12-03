using Prism.Navigation;
using Xamarin.Forms;

namespace PrismRegions.TeamModule.Views
{
    public partial class TeamsPage : ContentPage, IDestructible
    {
        public TeamsPage()
        {
            InitializeComponent();
        }

        public void Destroy()
        {
            // release currently loaded region on page teardown
            ComponentRegion.DestroyRegion();
            ComponentRegion = null;
        }
    }
}