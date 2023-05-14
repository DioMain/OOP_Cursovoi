using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;

namespace PCBuilder.ViewModel
{
    public abstract class WindowViewModel<T> : BaseViewModel
        where T : Window
    {
        public T Owner { get; private set; }

        public WindowViewModel(T owner)
        {
            Owner = owner;
        }
    }
}
