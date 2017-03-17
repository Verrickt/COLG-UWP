using Colg_UWP.Util;
using Colg_UWP.View.Pages;
using System.Collections.Generic;
using Windows.UI.Xaml.Controls;

namespace Colg_UWP.ViewModel
{
    public class MainPageVM
    {
        public List<MenuVM> TopMenuItems
        {
            get
            {
                return new List<MenuVM>()
                {
                    new MenuVM() {DisplayName="主页",Glyph="\uE10F",TargetPage=typeof(View.Pages.HomePage) },
                    new MenuVM() {DisplayName="论坛",Glyph="\uE8BD",TargetPage=typeof(View.Pages.ForumNavigationPage) },
                    new MenuVM() {DisplayName="热门讨论",Glyph="\uE8F2",TargetPage = typeof(View.Pages.PopularPostPage)},
                };
            }
        }
        public List<MenuVM> BottomMenuItems
        {
            get
            {
                return new List<MenuVM>()
                {
                     new MenuVM() {DisplayName="设置",Glyph="\uE115",TargetPage=typeof(View.Pages.SettingPage) },
                     new MenuVM() {DisplayName="评价",Glyph= "\uE8E1",TargetUri=new System.Uri($"ms-windows-store:REVIEW?PFN={Windows.ApplicationModel.Package.Current.Id.FamilyName}") },
                     new MenuVM(){DisplayName="反馈",Glyph="\xE939",TargetUri=new System.Uri($"mailto:colg@hohm.in?subject=COLG UWP 用户反馈")}
                };
            }
        }

        public RelayCommand<Frame> JumpToUserAccountPage { get; set; }

        public MainPageVM()
        {
            JumpToUserAccountPage = new RelayCommand<Frame>(
                (frame) =>
                {
                    var targetPage = UserDataManager.GetActiveUser() == null ? typeof(LoginPage) :
                    typeof(MySpace);
                    frame.Navigate(targetPage);
                });
        }
    }
}
