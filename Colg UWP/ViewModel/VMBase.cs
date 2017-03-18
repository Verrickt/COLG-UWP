using System.Collections.Generic;
using System.ComponentModel;

namespace Colg_UWP.ViewModel
{
    using System.Runtime.CompilerServices;

    public class VMBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected void SetProperty<T>(ref T property, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(property, value))
                return;
            property = value;
            OnPropertyChanged(propertyName);
        }
    }
}