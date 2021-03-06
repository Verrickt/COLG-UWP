﻿using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Colg_UWP.View.Pages
{
    using ViewModel;

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class HomePage : MenuPage
    {
        public HomePage()
        {
            this.NavigationCacheMode = NavigationCacheMode.Required;
            VM = new HomeVM();
            this.InitializeComponent();
        }

        public HomeVM VM;

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.NavigationMode == NavigationMode.New)
            {
                await VM.RefreshAsync();
            }
            Bindings.Update();
            base.OnNavigatedTo(e);
        }

        private void GridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var clicked = e.ClickedItem as Model.Article;
            ContentFrame.Navigate(typeof(ArticlePage), clicked);
        }

        private void GridView_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var gridview = sender as GridView;
            var wrapgrid = (ItemsWrapGrid)gridview.ItemsPanelRoot;
            double desiredWidth = (double)Resources["DesiredItemWidth"];
            double margin = 5;
            double actualWidth = desiredWidth + margin;
            int count = (int)(e.NewSize.Width / actualWidth);
            if (count == 0)
            {
                count = 1;
            }
            wrapgrid.ItemWidth = e.NewSize.Width / count - margin;
        }

       
    }
}