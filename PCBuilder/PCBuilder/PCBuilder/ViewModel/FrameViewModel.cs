﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace PCBuilder.ViewModel
{
    public abstract class FrameViewModel<F, W> : BaseViewModel
        where F : Page
        where W : Window
    {
        public F Owner { get; private set; }
        public W OwnerWindow { get; private set; }

        public FrameViewModel(F owner, W window)
        {
            Owner = owner;
            OwnerWindow = window;
        }
    }
}
