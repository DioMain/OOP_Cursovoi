using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PCBuilder.CustomUI
{
    /// <summary>
    /// Логика взаимодействия для UpWindowPanel.xaml
    /// </summary>
    public partial class UpWindowPanel : UserControl
    {
        public static readonly DependencyProperty IsCloseProp;
        public static readonly DependencyProperty IsMinimizeProp;

        #region Props

        public bool IsClose
        {
            get => (bool)GetValue(IsCloseProp);
            set => SetValue(IsCloseProp, value);
        }
        public bool IsMinimize
        {
            get => (bool)GetValue(IsMinimizeProp);
            set => SetValue(IsMinimizeProp, value);
        }

        #endregion

        public static readonly RoutedEvent OnCloseEvent;
        public static readonly RoutedEvent OnMinimizeEvent;

        #region Events

        public event RoutedEventHandler OnClose
        {
            add
            {
                // добавление обработчика
                AddHandler(OnCloseEvent, value);
            }
            remove
            {
                // удаление обработчика
                RemoveHandler(OnCloseEvent, value);
            }
        }
        public event RoutedEventHandler OnMininize
        {
            add
            {
                // добавление обработчика
                AddHandler(OnMinimizeEvent, value);
            }
            remove
            {
                // удаление обработчика
                RemoveHandler(OnMinimizeEvent, value);
            }
        }

        #endregion

        static UpWindowPanel()
        {
            IsCloseProp = DependencyProperty.Register(
                "Close", typeof(bool), typeof(UpWindowPanel),
                new PropertyMetadata(true)
                );
            IsMinimizeProp = DependencyProperty.Register(
                "Minimize", typeof(bool), typeof(UpWindowPanel),
                new PropertyMetadata(true)
                );

            OnCloseEvent = EventManager.RegisterRoutedEvent(
                "OnClose", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(UpWindowPanel)
                );
            OnMinimizeEvent = EventManager.RegisterRoutedEvent(
                "OnMinimize", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(UpWindowPanel)
                );  
        }

        public UpWindowPanel()
        {
            InitializeComponent();
        }

        #region События

        private void DockPanel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Window.GetWindow(this).DragMove();
        }

        private void Image0_MouseDown(object sender, MouseButtonEventArgs e)
        {
            RaiseEvent(new RoutedEventArgs(OnCloseEvent, this));

            if (IsClose) Window.GetWindow(this).Close();
        }

        private void Image1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            RaiseEvent(new RoutedEventArgs(OnMinimizeEvent, this));

            if (IsMinimize) Window.GetWindow(this).WindowState = WindowState.Minimized;
        }

        #endregion
    }
}
