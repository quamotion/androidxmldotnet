using System;
using System.Windows.Input;

namespace AndroidXmlDemo.Commands
{
    public class DelegateCommand<T> : ICommand
    {
        private readonly Func<T, bool> _canExecuteDelegate;
        private readonly Action<T> _execute;
        private bool _canExecuteValue;

        public DelegateCommand(Action<T> execute) : this(execute, true) {}

        public DelegateCommand(Action<T> execute, Func<T, bool> canExecute) : this(execute, canExecute, true) {}

        public DelegateCommand(Action<T> execute, bool canExecuteInitial) : this(execute, null, canExecuteInitial) {}

        public DelegateCommand(Action<T> execute, Func<T, bool> canExecute, bool canExecuteInitial)
        {
            _execute = execute;
            _canExecuteDelegate = canExecute;
            _canExecuteValue = canExecuteInitial;
        }

        protected bool CanExecuteValue
        {
            get { return _canExecuteValue; }
            set
            {
                if (value == _canExecuteValue) return;
                _canExecuteValue = value;
                RaiseCanExecuteChanged();
            }
        }

        #region ICommand Members

        public void Execute(object parameter)
        {
            if (_execute == null) return;
            try
            {
                _execute((T) parameter);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in Command.Execute: {0}", ex);
            }
        }

        public bool CanExecute(object parameter)
        {
            if (_canExecuteDelegate == null) return _canExecuteValue;
            try
            {
                bool result = _canExecuteDelegate((T) parameter);
                CanExecuteValue = result;
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in Command.CanExecute: {0}", ex);
                return false;
            }
        }

        public event EventHandler CanExecuteChanged;

        #endregion

        public void RaiseCanExecuteChanged()
        {
            RaiseCanExecuteChanged(EventArgs.Empty);
        }

        public void RaiseCanExecuteChanged(EventArgs e)
        {
            EventHandler handler = CanExecuteChanged;
            if (handler != null) handler(this, e);
        }
    }

    public class DelegateCommand : DelegateCommand<object>
    {
        public DelegateCommand(Action execute) : base(o => execute()) {}
        public DelegateCommand(Action execute, Func<bool> canExecute) : base(o => execute(), o => canExecute()) {}
        public DelegateCommand(Action execute, bool canExecuteInitial) : base(o => execute(), canExecuteInitial) {}

        public DelegateCommand(Action execute, Func<bool> canExecute, bool canExecuteInitial)
            : base(o => execute(), o => canExecute(), canExecuteInitial) {}
    }
}