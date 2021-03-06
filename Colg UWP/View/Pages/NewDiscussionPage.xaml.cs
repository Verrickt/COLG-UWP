﻿using Colg_UWP.ViewModel;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Colg_UWP.View.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class NewDiscussionPage : Page
    {
        public NewDiscussionVM VM { get; set; }

        public NewDiscussionPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            VM = e.Parameter as NewDiscussionVM;
            Bindings.Update();
            base.OnNavigatedTo(e);
        }
    }
}