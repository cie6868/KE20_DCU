using KE20_DCU.GPIO;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace KE20_DCU.UI
{
    public sealed partial class RightTurnSignal : UserControl
    {
        public Output Output;

        public RightTurnSignal()
        {
            this.InitializeComponent();
        }

        public Visibility BoolToVisibility(bool value)
        {
            if (value)
                return Visibility.Visible;
            else
                return Visibility.Collapsed;
        }
    }
}
