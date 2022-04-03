using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace LudumDare50.Client.ViewModels
{
    public abstract class ViewModel : INotifyPropertyChanged, IDisposable
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public virtual void Prepare()
        {

        }

        public virtual void Dispose()
        {
            PropertyChanged = null;
        }

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }

    public abstract class ViewModel<TParameter> : ViewModel
    {
        public abstract void Prepare(TParameter parameter);
    }
}