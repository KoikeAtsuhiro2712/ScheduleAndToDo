using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ScheduleAndToDo.Views.CommonUI
{
    /// <summary>
    /// NormarlButton.xaml の相互作用ロジック
    /// </summary>
    public partial class NormarlButton : UserControl
    {
        public NormarlButton()
        {
            InitializeComponent();
        }
        public static readonly DependencyProperty ButtonTextProperty =
            DependencyProperty.Register(nameof(ButtonText),typeof(object),typeof(NormarlButton),new PropertyMetadata(null));
        public object ButtonText
        {
            get => GetValue(ButtonTextProperty);
            set => SetValue(ButtonTextProperty, value);
        }
        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.Register(nameof(Command),typeof(ICommand),typeof(NormarlButton));
        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }
    }
}
