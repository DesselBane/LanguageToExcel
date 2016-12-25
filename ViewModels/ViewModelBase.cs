using System.ComponentModel;
using System.Runtime.CompilerServices;
using Prism.Mvvm;

namespace ViewModels
{
    public abstract class ViewModelBase : BindableBase
    {
        protected void FirePropertyChanged([CallerMemberName] string propName = null)
        {
            OnPropertyChanged(propName);
        }
    }
}
