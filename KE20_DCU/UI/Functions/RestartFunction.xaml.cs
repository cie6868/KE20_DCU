using System;
using Windows.System;
using Windows.UI.Xaml.Controls;

namespace KE20_DCU.UI.Functions
{
    public sealed partial class RestartFunction : UserControl
    {
        public RestartFunction()
        {
            this.InitializeComponent();
        }

        private void Button_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            ConfirmRestart();
        }

        private async void ConfirmRestart()
        {
            ContentDialog dialog = new ContentDialog
            {
                Title = "Reboot Requested",
                Content = "Confirm that you wish to reboot the DCU.",
                PrimaryButtonText = "Reboot Now",
                CloseButtonText = "Cancel"
            };

            ContentDialogResult result = await dialog.ShowAsync();
            if (result == ContentDialogResult.Primary)
                Reboot();
        }

        private void Reboot()
        {
            ShutdownManager.BeginShutdown(ShutdownKind.Restart, TimeSpan.FromSeconds(1));
        }
    }
}
