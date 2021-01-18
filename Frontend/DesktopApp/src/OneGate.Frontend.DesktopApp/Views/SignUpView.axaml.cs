using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace OneGate.Frontend.DesktopApp.Views
{
    public class SignUpView : UserControl
    {
        public SignUpView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}