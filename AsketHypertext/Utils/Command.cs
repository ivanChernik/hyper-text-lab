using System;
using System.Windows.Input;

namespace AsketHypertext.Utils
{
    public class Command : ICommand
    {   
        private readonly Action execute;
        private readonly Func<bool> canExecute;

        public Command(Action execute)
            : this(execute, null)
        {
        }
        
        public Command(Action execute, Func<bool> canExecute)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        public void Execute(object parameter)
        {
            execute();
        }

        public bool CanExecute(object parameter)
        {
            return canExecute == null || canExecute();
        }

        public event EventHandler CanExecuteChanged = delegate { };

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged(this, new EventArgs());
        }
    }
}
