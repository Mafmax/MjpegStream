using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MjpegStreamReciever.ViewModel
{
    public class RelayCommand : ICommand
    {
        private readonly Action<object> onExecute;
        private readonly Func<object, bool> onCanExecute;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove{ CommandManager.RequerySuggested -= value; }

        }
        public RelayCommand(Action<object> onExecute, Func<object, bool> onCanExecute = null)
        {
            this.onExecute = onExecute;
            this.onCanExecute = onCanExecute;
        }
        public bool CanExecute(object parameter)
        {
            return onCanExecute == null || onCanExecute(parameter);
        }

        public void Execute(object parameter)
        {
            onExecute(parameter);
        }
    }
}
