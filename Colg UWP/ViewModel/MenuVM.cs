using System;

namespace Colg_UWP.ViewModel
{
    public class MenuVM: VMBase
    {
        public string DisplayName { get; set; }
        public string Glyph { get; set; }
        public Type TargetPage { get; set; }
    }
}
