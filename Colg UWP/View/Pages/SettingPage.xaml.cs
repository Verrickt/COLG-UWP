using Colg_UWP.ViewModel;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Colg_UWP.View.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SettingPage : Page
    {
        public SettingPage()
        {
            this.InitializeComponent();
        }

        private SettingVM VM;

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            VM = VM ?? new SettingVM();
            await VM.UpdateCacheSize();
            base.OnNavigatedTo(e);
        }
    }
}