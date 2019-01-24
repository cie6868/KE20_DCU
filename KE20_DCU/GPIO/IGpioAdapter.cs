using System;
using Windows.Devices.Gpio;

namespace KE20_DCU.GPIO
{
    public interface IGpioAdapter
    {
        GpioPin OpenPin(int pinNumber);
        void SetDriveMode(GpioPin pin, GpioPinDriveMode mode);
        void Write(GpioPin pin, GpioPinValue value);
        GpioPinValue Read(GpioPin pin);
    }

    public class GpioAdapter : IGpioAdapter
    {
        private GpioController mController;

        public GpioAdapter()
        {
            mController = GpioController.GetDefault();
            if (mController == null)
                throw new NoGpioException();
        }

        public GpioPin OpenPin(int pinNumber)
        {
            return mController.OpenPin(pinNumber);
        }

        public void SetDriveMode(GpioPin pin, GpioPinDriveMode mode)
        {
            pin.SetDriveMode(mode);
        }

        public void Write(GpioPin pin, GpioPinValue value)
        {
            pin.Write(value);
        }

        public GpioPinValue Read(GpioPin pin)
        {
            return pin.Read();
        }
    }

    public class FakeGpioAdapter : IGpioAdapter
    {
        public FakeGpioAdapter()
        {
            // do nothing
        }

        public GpioPin OpenPin(int pinNumber)
        {
            return null;
        }

        public void SetDriveMode(GpioPin pin, GpioPinDriveMode mode)
        {
            // do nothing
        }

        public void Write(GpioPin pin, GpioPinValue value)
        {
            // do nothing
        }

        public GpioPinValue Read(GpioPin pin)
        {
            return GpioPinValue.Low;
        }
    }

    public class NoGpioException : Exception { }
}
