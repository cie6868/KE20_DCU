using KE20_DCU.GPIO;
using KE20_DCU.Model;
using KE20_DCU.UI;
using System;
using System.Threading;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml.Controls;

namespace KE20_DCU
{
    public sealed partial class MainPage : Page
    {
        private static readonly TimeSpan GPIO_LOOP_INTERVAL = TimeSpan.FromMilliseconds(100);

        private Input mInput;
        private Output mOutput;
        private SpeedRevs mSpeedRevs = new SpeedRevs { Kmph = 24, Rpm = 1975 };

        public MainPage()
        {
            this.InitializeComponent();

            ApplicationView.PreferredLaunchViewSize = new Size(1024, 600);
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;

            uiSpeedoTacho.SpeedRevs = mSpeedRevs;

            IGpioAdapter gpioAdapter = GetGpio();

            // fake it till you make it
            if (gpioAdapter == null)
                gpioAdapter = new FakeGpioAdapter();

            mInput = new Input(gpioAdapter);
            mOutput = new Output(gpioAdapter);

            funLowbeam.Output = mOutput;
            funHighbeam.Output = mOutput;
            uiBrakeIndicator.Output = mOutput;
            uiReverseIndicator.Output = mOutput;
            uiLeftTurnSignal.Output = mOutput;
            uiRightTurnSignal.Output = mOutput;
            
            new Thread(GpioLoop).Start();
        }

        private IGpioAdapter GetGpio()
        {
            try
            {
                return new GpioAdapter();
            }
            catch (NoGpioException)
            {
                OnGpioError();
                return null;
            }
        }

        private async void OnGpioError()
        {
            ContentDialog dialog = new ContentDialog
            {
                Title = "GPIO Required",
                Content = "Could not find GPIO ports.",
                PrimaryButtonText = "Ignore",
                SecondaryButtonText = "Exit"
            };

            ContentDialogResult result = await dialog.ShowAsync();
            if (result == ContentDialogResult.Secondary)
                CoreApplication.Exit();
            
        }

        private void GpioLoop()
        {
            while (true)
            {
                // poll inputs
                mOutput.SetBrakePressed(mInput.GetBrakePressed());
                mOutput.SetReversePressed(mInput.GetReversePressed());

                mOutput.SetTurnLeftPressed(mInput.GetTurnLeftPressed());
                mOutput.SetTurnRightPressed(mInput.GetTurnRightPressed());

                Thread.Sleep(GPIO_LOOP_INTERVAL);
            }
        }
    }
}
