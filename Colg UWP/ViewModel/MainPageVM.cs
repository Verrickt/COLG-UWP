using System.Collections.Generic;

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
                    new MenuVM() {DisplayName="主页",Glyph="\uE10F",TargetPageType=typeof(View.Pages.HomePage) },
                    new MenuVM() {DisplayName="论坛",Glyph="\uE8BD",TargetPageType=typeof(View.Pages.ForumNavi) },
                    new MenuVM() {DisplayName="热门讨论",Glyph="\uE8F2",TargetPageType = typeof(View.Pages.PopularPostView)},
                    new MenuVM() {DisplayName="个人中心",Glyph="\uE187",TargetPageType = typeof(View.Pages.LoginPage)}
                };
            }
        }
        public List<MenuVM> BottomMenuItems
        {
            get
            {
                return new List<MenuVM>()
                {
                    new MenuVM() {DisplayName="登陆",Glyph="\uE1E2" },
                     new MenuVM() {DisplayName="设置",Glyph="\uE115" },
                    new MenuVM() {DisplayName="关于",Glyph= "\uE946" },
                    new MenuVM() {DisplayName="评价",Glyph= "\uE8E1" }


                };
            }
        }
    }
}
