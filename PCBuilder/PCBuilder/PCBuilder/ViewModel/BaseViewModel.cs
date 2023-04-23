using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PCBuilder.ViewModel
{
    public abstract class BaseViewModel<T> : INotifyPropertyChanged, IDisposable
    {
        public T Owner { get; private set; }

        public BaseViewModel(T owner)
        {
            Owner = owner;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        public virtual void Dispose() { }
    }
}
