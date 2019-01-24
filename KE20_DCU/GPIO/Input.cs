using Windows.Devices.Gpio;

namespace KE20_DCU.GPIO
{
    class Input
    {
        private IGpioAdapter mGpio;

        private GpioPin iBrakePin;

        private GpioPin iReversePin;

        private GpioPin iTurnLeftPin;
        private GpioPin iTurnRightPin;

        public Input(IGpioAdapter gpioAdapter)
        {
            mGpio = gpioAdapter;
            InitializePins();
        }

        ~Input()
        {
            ClosePins();
        }

        public bool GetBrakePressed()
        {
            return (mGpio.Read(iBrakePin) == GpioPinValue.High);
        }

        public bool GetReversePressed()
        {
            return (mGpio.Read(iReversePin) == GpioPinValue.High);
        }

        public bool GetTurnLeftPressed()
        {
            return (mGpio.Read(iTurnLeftPin) == GpioPinValue.High);
        }

        public bool GetTurnRightPressed()
        {
            return (mGpio.Read(iTurnRightPin) == GpioPinValue.High);
        }

        #region GpioActivity

        private void InitializePins()
        {
            iBrakePin = mGpio.OpenPin(Pins.I_BRAKE_PIN);
            mGpio.SetDriveMode(iBrakePin, GpioPinDriveMode.Input);

            iReversePin = mGpio.OpenPin(Pins.I_REVERSE_PIN);
            mGpio.SetDriveMode(iReversePin, GpioPinDriveMode.Input);

            iTurnLeftPin = mGpio.OpenPin(Pins.I_TURNLEFT_PIN);
            mGpio.SetDriveMode(iTurnLeftPin, GpioPinDriveMode.Input);
            iTurnRightPin = mGpio.OpenPin(Pins.I_TURNRIGHT_PIN);
            mGpio.SetDriveMode(iTurnRightPin, GpioPinDriveMode.Input);
        }

        private void ClosePins()
        {
            iBrakePin.Dispose();
            iReversePin.Dispose();
            iTurnLeftPin.Dispose();
            iTurnRightPin.Dispose();
        }

        #endregion
    }
}
