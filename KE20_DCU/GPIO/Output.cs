using System;
using System.ComponentModel;
using System.Threading;
using Windows.Devices.Gpio;

namespace KE20_DCU.GPIO
{
    public class Output : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public bool Lowbeam { get; private set; }
        public bool LowbeamGpio { get; private set; }
        public bool Highbeam { get; private set; }
        public bool HighbeamGpio { get; private set; }

        public bool BrakePressed { get; private set; }
        public bool BrakeLight { get; private set; }

        public bool ReversePressed { get; private set; }
        public bool ReverseLight { get; private set; }

        public bool TurnLeftPressed { get; private set; }
        public bool TurnLeftLight { get; private set; }
        public bool TurnRightPressed { get; private set; }
        public bool TurnRightLight { get; private set; }

        private static readonly TimeSpan BLINK_INTERVAL = TimeSpan.FromMilliseconds(500);

        private IGpioAdapter mGpio;
        
        private GpioPin oLowbeamPin;
        private GpioPin oHighbeamPin;
        
        private GpioPin oBrakePin;
        
        private GpioPin oReversePin;
        
        private GpioPin oTurnLeftPin;
        private GpioPin oTurnRightPin;

        public Output(IGpioAdapter gpioAdapter)
        {
            mGpio = gpioAdapter;
            InitializePins();
        }

        ~Output()
        {
            ClosePins();
        }

        public void ToggleLowbeam()
        {
            if (Highbeam && Lowbeam)
            {
                Lowbeam = false;
                TurnOffLowbeam();
            }
            else if (Highbeam && !Lowbeam)
            {
                Highbeam = false;
                TurnOffHighbeam();

                Lowbeam = true;
                TurnOnLowbeam();
            }
            else if (!Highbeam && Lowbeam)
            {
                Lowbeam = false;
                TurnOffLowbeam();
            }
            else if (!Highbeam && !Lowbeam)
            {
                Lowbeam = true;
                TurnOnLowbeam();
            }
        }

        public void ToggleHighbeam()
        {
            if (Highbeam && Lowbeam)
            {
                Highbeam = false;
                TurnOffHighbeam();
                
                TurnOnLowbeam();
            }
            else if (Highbeam && !Lowbeam)
            {
                Highbeam = false;
                TurnOffHighbeam();
            }
            else if (!Highbeam && Lowbeam)
            {
                TurnOffLowbeam();

                Highbeam = true;
                TurnOnHighbeam();
            }
            else if (!Highbeam && !Lowbeam)
            {
                Highbeam = true;
                TurnOnHighbeam();
            }
        }

        public void SetBrakePressed(bool pressed)
        {
            BrakePressed = pressed;

            if (BrakePressed)
                TurnOnBrakeLight();
            else
                TurnOffBrakeLight();
        }

        public void SetReversePressed(bool pressed)
        {
            ReversePressed = pressed;

            if (ReversePressed)
                TurnOnReverseLight();
            else
                TurnOffReverseLight();
        }

        public void SetTurnLeftPressed(bool pressed)
        {
            // ignore same state
            if (TurnLeftPressed == pressed)
                return;

            TurnLeftPressed = pressed;

            if (TurnLeftPressed)
            {
                TurnRightPressed = false;
                BlinkTurnLeft();
            }
        }

        public void SetTurnRightPressed(bool pressed)
        {
            // ignore same state
            if (TurnRightPressed == pressed)
                return;
            
            TurnRightPressed = pressed;

            if (TurnRightPressed)
            {
                TurnLeftPressed = false;
                BlinkTurnRight();
            }
        }

        #region GpioActivity

        private void InitializePins()
        {
            oLowbeamPin = mGpio.OpenPin(Pins.O_LOWBEAM_PIN);
            mGpio.SetDriveMode(oLowbeamPin, GpioPinDriveMode.Output);

            oHighbeamPin = mGpio.OpenPin(Pins.O_HIGHBEAM_PIN);
            mGpio.SetDriveMode(oHighbeamPin, GpioPinDriveMode.Output);
            
            oBrakePin = mGpio.OpenPin(Pins.O_BRAKE_PIN);
            mGpio.SetDriveMode(oBrakePin, GpioPinDriveMode.Output);
            
            oReversePin = mGpio.OpenPin(Pins.O_REVERSE_PIN);
            mGpio.SetDriveMode(oReversePin, GpioPinDriveMode.Output);
            
            oTurnLeftPin = mGpio.OpenPin(Pins.O_TURNLEFT_PIN);
            mGpio.SetDriveMode(oTurnLeftPin, GpioPinDriveMode.Output);
            oTurnRightPin = mGpio.OpenPin(Pins.O_TURNRIGHT_PIN);
            mGpio.SetDriveMode(oTurnRightPin, GpioPinDriveMode.Output);
        }

        private void ClosePins()
        {
            oLowbeamPin.Dispose();
            oHighbeamPin.Dispose();

            oBrakePin.Dispose();

            oReversePin.Dispose();

            oTurnLeftPin.Dispose();
            oTurnRightPin.Dispose();
        }

        private void TurnOnLowbeam()
        {
            mGpio.Write(oLowbeamPin, GpioPinValue.High);
            LowbeamGpio = true;
        }

        private void TurnOffLowbeam()
        {
            mGpio.Write(oLowbeamPin, GpioPinValue.Low);
            LowbeamGpio = false;
        }

        private void TurnOnHighbeam()
        {
            mGpio.Write(oHighbeamPin, GpioPinValue.High);
            HighbeamGpio = true;
        }

        private void TurnOffHighbeam()
        {
            mGpio.Write(oHighbeamPin, GpioPinValue.Low);
            HighbeamGpio = false;
        }

        private void TurnOnBrakeLight()
        {
            mGpio.Write(oBrakePin, GpioPinValue.High);
            BrakeLight = true;
        }

        private void TurnOffBrakeLight()
        {
            mGpio.Write(oBrakePin, GpioPinValue.Low);
            BrakeLight = false;
        }

        private void TurnOnReverseLight()
        {
            mGpio.Write(oReversePin, GpioPinValue.High);
            ReverseLight = true;
        }

        private void TurnOffReverseLight()
        {
            mGpio.Write(oReversePin, GpioPinValue.Low);
            ReverseLight = false;
        }

        private void BlinkTurnLeft()
        {
            new Thread(() =>
            {
                while (true)
                {
                    if (!TurnLeftPressed)
                    {
                        TurnLeftLight = false;
                        return;
                    }

                    TurnLeftLight = !TurnLeftLight;
                    if (TurnLeftLight)
                        mGpio.Write(oTurnLeftPin, GpioPinValue.High);
                    else
                        mGpio.Write(oTurnLeftPin, GpioPinValue.Low);

                    Thread.Sleep(BLINK_INTERVAL);
                }
            }).Start();
        }

        private void BlinkTurnRight()
        {
            new Thread(() =>
            {
                while (true)
                {
                    if (!TurnRightPressed)
                    {
                        TurnRightLight = false;
                        return;
                    }

                    TurnRightLight = !TurnRightLight;
                    if (TurnRightLight)
                        mGpio.Write(oTurnRightPin, GpioPinValue.High);
                    else
                        mGpio.Write(oTurnRightPin, GpioPinValue.Low);
                    
                    Thread.Sleep(BLINK_INTERVAL);
                }
            }).Start();
        }

        #endregion
    }
}
