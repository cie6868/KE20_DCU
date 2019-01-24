using KE20_DCU.GPIO;
using Windows.UI.Xaml.Controls;

namespace KE20_DCU.UI.Functions
{
    public sealed partial class HighbeamFunction : UserControl
    {
        public Output Output;

        public HighbeamFunction()
        {
            this.InitializeComponent();
        }

        private void btn_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            Output.ToggleHighbeam();
        }
    }
}
