using System;
using System.Windows.Input;

namespace AndroidXmlDemo.Commands
{
    public class EventCommand<T> : ICommand where T : class
    {
        public EventCommand() : this(o => true) {}

        public EventCommand(Predicate<T> canExecute)
        {
            _canExecute = canExecute;
        }

        #region Executed event

        public event EventHandler<ArgumentEventArgs<T>> Executed = delegate { };

        #endregion

        #region CanExecute property

        private readonly Predicate<T> _canExecute;

        #endregion

        #region Implementation of ICommand

        /// <summary>
        /// Defines the method to be called when the command is invoked.
        /// </summary>
        /// <param name="parameter">Data used by the command.  If the command does not require data to be passed, this object can be set to null.</param>
        public void Execute(object parameter)
        {
            Executed(this, new ArgumentEventArgs<T> {Argument = parameter as T});
        }

        /// <summary>
        /// Defines the method that determines whether the command can execute in its current state.
        /// </summary>
        /// <returns>
        /// true if this command can be executed; otherwise, false.
        /// </returns>
        /// <param name="parameter">Data used by the command.  If the command does not require data to be passed, this object can be set to null.</param>
        bool ICommand.CanExecute(object parameter)
        {
            return _canExecute(parameter as T);
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        #endregion
    }
}