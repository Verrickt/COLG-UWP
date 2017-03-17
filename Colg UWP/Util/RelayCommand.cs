using System;
using System.Windows.Input;

namespace Colg_UWP.Util
{
    public class RelayCommand : ICommand
    {
        private Action _action { get; set; }

        private Func<bool> _canExecute { get; set; }

        public event EventHandler CanExecuteChanged;

        public RelayCommand(Action action, Func<bool> canExecute)
        {
            _action = action;
            _canExecute = canExecute;
        }

        public RelayCommand(Action action) : this(action, null)
        {
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute?.Invoke() ?? true;
        }

        public void Execute(object parameter)
        {
            _action();
        }
    }

    public class RelayCommand<T> : ICommand
    {
        private Action<T> _action { get; set; }

        private Func<bool> _canExecute { get; set; }

        public RelayCommand(Action<T> action, Func<bool> canExecute)
        {
            _action = action;
            _canExecute = canExecute;
        }

        public RelayCommand(Action<T> action) : this(action, null)
        {
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute?.Invoke() ?? true;
        }

        public void Execute(object parameter)
        {
            _action((T)parameter);
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }

        public event EventHandler CanExecuteChanged;
    }
}