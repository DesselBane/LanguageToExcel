using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Commands
{
    public class RelayCommand : CommandBase
    {
        #region Vars

        private Func<object, bool> _canExecute;
        private Action<object> _execute;

        #endregion

        #region Constructors

        public RelayCommand(Action<object> executeDelegate, Func<object, bool> canExecuteDelegate)
        {
            if (executeDelegate == null)
                throw new ArgumentNullException("ExecutionDelegate must not be null.");

            _execute = executeDelegate;
            _canExecute = canExecuteDelegate;
        }

        public RelayCommand(Action executeDelegate, Func<bool> canExecuteDelegate)
            : this(arg0 => executeDelegate(), arg0 => canExecuteDelegate())
        {
        }

        public RelayCommand(Action executeDelegate)
            : this(executeDelegate, null as Func<object, bool>)
        {
        }

        public RelayCommand(Action<object> executeDelegate)
            : this(executeDelegate, null as Func<object, bool>)
        {
        }

        public RelayCommand(Action executeDelegate, Func<object, bool> canExecuteDelegate)
            : this(arg0 => executeDelegate(), canExecuteDelegate)
        {
        }

        public RelayCommand(Action<object> executionDelegate, Func<bool> canExecuteDelegate)
            : this(executionDelegate, arg0 => canExecuteDelegate())
        {
        }

        #endregion

        #region Methods

        public override bool CanExecute(object parameter)
        {
            if (_canExecute != null)
                return _canExecute(parameter);

            return base.CanExecute(parameter);
        }

        protected override void ExecuteImpl(object parameter)
        {
            _execute(parameter);
        }

        #endregion
    }
}
