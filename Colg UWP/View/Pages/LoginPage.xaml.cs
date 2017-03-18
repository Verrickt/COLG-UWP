using Colg_UWP.Model;
using Colg_UWP.ViewModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Colg_UWP.View.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LoginPage : MenuPage
    {
        public LoginPage()
        {
            this.InitializeComponent();
        }

        private ViewModel.LoginVM VM;

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.NavigationMode == NavigationMode.New)
            {
                VM = new LoginVM();
            }
            base.OnNavigatedTo(e);
        }

        private void Delete_OnClick(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var user = button.DataContext as User;
            VM.RemoveUser(user);
        }
    }
}