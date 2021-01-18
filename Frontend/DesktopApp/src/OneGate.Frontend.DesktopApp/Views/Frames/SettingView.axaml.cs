using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace OneGate.Frontend.DesktopApp.Views.Frames
{
    public class SettingView : UserControl
    {
        public SettingView()
        {
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
