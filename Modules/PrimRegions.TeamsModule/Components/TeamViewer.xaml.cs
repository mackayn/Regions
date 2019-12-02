
using Prism.Navigation;
using Xamarin.Forms;

namespace PrismRegions.TeamModule.Components
{
    public partial class TeamViewer : ContentView, IDestructible
    {
        public TeamViewer()
        {
            InitializeComponent();
        }

        public void Destroy()
        {
            ImageTeam.Source = null;
        }
    }
}