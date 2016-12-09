using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ViewModels.Commands
{
    public abstract class CommandBase : ICommand, IDisposable
    {

        #region Vars

        private bool _isDisposed;

        #endregion

        #region Properties

        public bool IsDisposed => _isDisposed;

        #endregion

        #region Methods

        public virtual bool CanExecute(object parameter)
        {
            return !IsDisposed;
        }

        #endregion

        #region Events

        public event EventHandler CanExecuteChanged;

        protected void OnCanExecuteChanged()
        {
            EventHandler handler = CanExecuteChanged;
            handler?.Invoke(this, new EventArgs());
        }

        public event EventHandler Executed;

        protected void FireExecuted()
        {
            EventHandler handler = Executed;
            handler?.Invoke(this, new EventArgs());
        }

        #endregion

        #region Public

        protected abstract void ExecuteImpl(object param);

        public void Execute(object parameter)
        {
            if (!CanExecute(parameter))
                throw new InvalidOperationException("Cannot Execute Command if CanExecute() returns false");

            ExecuteImpl(parameter);
            FireExecuted();
        }

        public virtual void ReevaluatePermissions()
        {
            OnCanExecuteChanged();
        }

        #endregion

        #region Implementation of IDisposable

        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        public virtual void Dispose()
        {
            _isDisposed = true;
        }

        #endregion
    }
}
