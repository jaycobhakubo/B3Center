using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace GameTech.Elite.Client.Modules.B3Center.Helper
{
    class CommandHandler : ICommand
    {
        private Action m_action;
        private bool m_canExecute;

        public CommandHandler(Action action, bool canExecute)
        {
            m_action = action;
            m_canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return m_canExecute;
        }

       public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            m_action();
        }
    }
}
