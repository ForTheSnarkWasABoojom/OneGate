using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace OneGate.Frontend.DesktopApp.Views
{
    public class BaseSignInUpView : UserControl
    {
        public BaseSignInUpView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
