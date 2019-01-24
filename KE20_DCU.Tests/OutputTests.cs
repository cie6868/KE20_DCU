using KE20_DCU.GPIO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;

namespace KE20_DCU_Units
{
    [TestClass]
    public class OutputTests
    {
        private Output mLighting;

        [TestInitialize]
        public void Preparation()
        {
            IGpioAdapter gpioAdapter = new FakeGpioAdapter();

            mLighting = new Output(gpioAdapter);

            Assert.IsFalse(mLighting.Lowbeam);
            Assert.IsFalse(mLighting.LowbeamGpio);
            Assert.IsFalse(mLighting.Highbeam);
            Assert.IsFalse(mLighting.HighbeamGpio);

            Assert.IsFalse(mLighting.BrakePressed);
            Assert.IsFalse(mLighting.BrakeLight);

            Assert.IsFalse(mLighting.ReversePressed);
            Assert.IsFalse(mLighting.ReverseLight);
            
            Assert.IsFalse(mLighting.TurnLeftPressed);
            Assert.IsFalse(mLighting.TurnLeftLight);
            Assert.IsFalse(mLighting.TurnRightPressed);
            Assert.IsFalse(mLighting.TurnRightLight);
        }

        [TestMethod]
        public void LowbeamOnThenOffThenOn()
        {
            mLighting.ToggleLowbeam();
            Assert.IsTrue(mLighting.Lowbeam);
            Assert.IsTrue(mLighting.LowbeamGpio);
            Assert.IsFalse(mLighting.Highbeam);
            Assert.IsFalse(mLighting.HighbeamGpio);
            
            mLighting.ToggleLowbeam();
            Assert.IsFalse(mLighting.Lowbeam);
            Assert.IsFalse(mLighting.LowbeamGpio);
            Assert.IsFalse(mLighting.Highbeam);
            Assert.IsFalse(mLighting.HighbeamGpio);

            mLighting.ToggleLowbeam();
            Assert.IsTrue(mLighting.Lowbeam);
            Assert.IsTrue(mLighting.LowbeamGpio);
            Assert.IsFalse(mLighting.Highbeam);
            Assert.IsFalse(mLighting.HighbeamGpio);
        }

        [TestMethod]
        public void HighbeamOnThenOffThenOn()
        {
            mLighting.ToggleHighbeam();
            Assert.IsFalse(mLighting.Lowbeam);
            Assert.IsFalse(mLighting.LowbeamGpio);
            Assert.IsTrue(mLighting.Highbeam);
            Assert.IsTrue(mLighting.HighbeamGpio);

            mLighting.ToggleHighbeam();
            Assert.IsFalse(mLighting.Lowbeam);
            Assert.IsFalse(mLighting.LowbeamGpio);
            Assert.IsFalse(mLighting.Highbeam);
            Assert.IsFalse(mLighting.HighbeamGpio);

            mLighting.ToggleHighbeam();
            Assert.IsFalse(mLighting.Lowbeam);
            Assert.IsFalse(mLighting.LowbeamGpio);
            Assert.IsTrue(mLighting.Highbeam);
            Assert.IsTrue(mLighting.HighbeamGpio);
        }

        [TestMethod]
        public void LowbeamOnThenHighbeamOnThenHighbeamOff()
        {
            mLighting.ToggleLowbeam();
            Assert.IsTrue(mLighting.Lowbeam);
            Assert.IsTrue(mLighting.LowbeamGpio);
            Assert.IsFalse(mLighting.Highbeam);
            Assert.IsFalse(mLighting.HighbeamGpio);

            mLighting.ToggleHighbeam();
            Assert.IsTrue(mLighting.Lowbeam);
            Assert.IsFalse(mLighting.LowbeamGpio);
            Assert.IsTrue(mLighting.Highbeam);
            Assert.IsTrue(mLighting.HighbeamGpio);

            mLighting.ToggleHighbeam();
            Assert.IsTrue(mLighting.Lowbeam);
            Assert.IsTrue(mLighting.LowbeamGpio);
            Assert.IsFalse(mLighting.Highbeam);
            Assert.IsFalse(mLighting.HighbeamGpio);
        }

        [TestMethod]
        public void HighbeamOnThenLowbeamOnThenLowbeamOff()
        {
            mLighting.ToggleHighbeam();
            Assert.IsFalse(mLighting.Lowbeam);
            Assert.IsFalse(mLighting.LowbeamGpio);
            Assert.IsTrue(mLighting.Highbeam);
            Assert.IsTrue(mLighting.HighbeamGpio);

            mLighting.ToggleLowbeam();
            Assert.IsTrue(mLighting.Lowbeam);
            Assert.IsTrue(mLighting.LowbeamGpio);
            Assert.IsFalse(mLighting.Highbeam);
            Assert.IsFalse(mLighting.HighbeamGpio);

            mLighting.ToggleLowbeam();
            Assert.IsFalse(mLighting.Lowbeam);
            Assert.IsFalse(mLighting.LowbeamGpio);
            Assert.IsFalse(mLighting.Highbeam);
            Assert.IsFalse(mLighting.HighbeamGpio);
        }

        [TestMethod]
        public void LowbeamOnThenHighbeamOnThenLowbeamOffThenHighbeamOff()
        {
            mLighting.ToggleLowbeam();
            Assert.IsTrue(mLighting.Lowbeam);
            Assert.IsTrue(mLighting.LowbeamGpio);
            Assert.IsFalse(mLighting.Highbeam);
            Assert.IsFalse(mLighting.HighbeamGpio);

            mLighting.ToggleHighbeam();
            Assert.IsTrue(mLighting.Lowbeam);
            Assert.IsFalse(mLighting.LowbeamGpio);
            Assert.IsTrue(mLighting.Highbeam);
            Assert.IsTrue(mLighting.HighbeamGpio);

            mLighting.ToggleLowbeam();
            Assert.IsFalse(mLighting.Lowbeam);
            Assert.IsFalse(mLighting.LowbeamGpio);
            Assert.IsTrue(mLighting.Highbeam);
            Assert.IsTrue(mLighting.HighbeamGpio);

            mLighting.ToggleHighbeam();
            Assert.IsFalse(mLighting.Lowbeam);
            Assert.IsFalse(mLighting.LowbeamGpio);
            Assert.IsFalse(mLighting.Highbeam);
            Assert.IsFalse(mLighting.HighbeamGpio);
        }

        [TestMethod]
        public void BrakesPressed()
        {
            mLighting.SetBrakePressed(true);
            Assert.IsTrue(mLighting.BrakePressed);
            Assert.IsTrue(mLighting.BrakeLight);

            mLighting.SetBrakePressed(false);
            Assert.IsFalse(mLighting.BrakePressed);
            Assert.IsFalse(mLighting.BrakeLight);
        }

        [TestMethod]
        public void ReversePressed()
        {
            mLighting.SetReversePressed(true);
            Assert.IsTrue(mLighting.ReversePressed);
            Assert.IsTrue(mLighting.ReverseLight);

            mLighting.SetReversePressed(false);
            Assert.IsFalse(mLighting.ReversePressed);
            Assert.IsFalse(mLighting.ReverseLight);
        }

        [TestMethod]
        public void TurnLeftPressed()
        {
            mLighting.SetTurnLeftPressed(true);
            Assert.IsTrue(mLighting.TurnLeftPressed);
            Assert.IsFalse(mLighting.TurnRightPressed);

            Thread.Sleep(50);

            // blink
            Assert.IsTrue(mLighting.TurnLeftPressed);
            Assert.IsTrue(mLighting.TurnLeftLight);
            Assert.IsFalse(mLighting.TurnRightPressed);
            Assert.IsFalse(mLighting.TurnRightLight);

            Thread.Sleep(550);
            
            Assert.IsTrue(mLighting.TurnLeftPressed);
            Assert.IsFalse(mLighting.TurnLeftLight);
            Assert.IsFalse(mLighting.TurnRightPressed);
            Assert.IsFalse(mLighting.TurnRightLight);

            Thread.Sleep(550);

            // blink
            Assert.IsTrue(mLighting.TurnLeftPressed);
            Assert.IsTrue(mLighting.TurnLeftLight);
            Assert.IsFalse(mLighting.TurnRightPressed);
            Assert.IsFalse(mLighting.TurnRightLight);
            
            Thread.Sleep(550);

            Assert.IsTrue(mLighting.TurnLeftPressed);
            Assert.IsFalse(mLighting.TurnLeftLight);
            Assert.IsFalse(mLighting.TurnRightPressed);
            Assert.IsFalse(mLighting.TurnRightLight);

            Thread.Sleep(550);

            // blink
            Assert.IsTrue(mLighting.TurnLeftPressed);
            Assert.IsTrue(mLighting.TurnLeftLight);
            Assert.IsFalse(mLighting.TurnRightPressed);
            Assert.IsFalse(mLighting.TurnRightLight);
            
            mLighting.SetTurnLeftPressed(false);
            Assert.IsFalse(mLighting.TurnLeftPressed);
            Assert.IsFalse(mLighting.TurnRightPressed);
            
            Thread.Sleep(550);

            Assert.IsFalse(mLighting.TurnLeftPressed);
            Assert.IsFalse(mLighting.TurnLeftLight);
            Assert.IsFalse(mLighting.TurnRightPressed);
            Assert.IsFalse(mLighting.TurnRightLight);
        }

        [TestMethod]
        public void TurnRightPressed()
        {
            mLighting.SetTurnRightPressed(true);
            Assert.IsFalse(mLighting.TurnLeftPressed);
            Assert.IsTrue(mLighting.TurnRightPressed);

            Thread.Sleep(50);

            // blink
            Assert.IsFalse(mLighting.TurnLeftPressed);
            Assert.IsFalse(mLighting.TurnLeftLight);
            Assert.IsTrue(mLighting.TurnRightPressed);
            Assert.IsTrue(mLighting.TurnRightLight);

            Thread.Sleep(550);

            Assert.IsFalse(mLighting.TurnLeftPressed);
            Assert.IsFalse(mLighting.TurnLeftLight);
            Assert.IsTrue(mLighting.TurnRightPressed);
            Assert.IsFalse(mLighting.TurnRightLight);

            Thread.Sleep(550);

            // blink
            Assert.IsFalse(mLighting.TurnLeftPressed);
            Assert.IsFalse(mLighting.TurnLeftLight);
            Assert.IsTrue(mLighting.TurnRightPressed);
            Assert.IsTrue(mLighting.TurnRightLight);

            Thread.Sleep(550);

            Assert.IsFalse(mLighting.TurnLeftPressed);
            Assert.IsFalse(mLighting.TurnLeftLight);
            Assert.IsTrue(mLighting.TurnRightPressed);
            Assert.IsFalse(mLighting.TurnRightLight);

            Thread.Sleep(550);

            // blink
            Assert.IsFalse(mLighting.TurnLeftPressed);
            Assert.IsFalse(mLighting.TurnLeftLight);
            Assert.IsTrue(mLighting.TurnRightPressed);
            Assert.IsTrue(mLighting.TurnRightLight);

            mLighting.SetTurnRightPressed(false);
            Assert.IsFalse(mLighting.TurnLeftPressed);
            Assert.IsFalse(mLighting.TurnRightPressed);

            Thread.Sleep(550);

            Assert.IsFalse(mLighting.TurnLeftPressed);
            Assert.IsFalse(mLighting.TurnLeftLight);
            Assert.IsFalse(mLighting.TurnRightPressed);
            Assert.IsFalse(mLighting.TurnRightLight);
        }

        [TestMethod]
        public void TurnLeftPressedThenTurnRightPressed()
        {
            mLighting.SetTurnLeftPressed(true);
            Assert.IsTrue(mLighting.TurnLeftPressed);
            Assert.IsFalse(mLighting.TurnRightPressed);

            mLighting.SetTurnRightPressed(true);
            Assert.IsFalse(mLighting.TurnLeftPressed);
            Assert.IsTrue(mLighting.TurnRightPressed);
        }

        [TestMethod]
        public void TurnRightPressedThenTurnLeftPressed()
        {
            mLighting.SetTurnRightPressed(true);
            Assert.IsFalse(mLighting.TurnLeftPressed);
            Assert.IsTrue(mLighting.TurnRightPressed);
            
            mLighting.SetTurnLeftPressed(true);
            Assert.IsTrue(mLighting.TurnLeftPressed);
            Assert.IsFalse(mLighting.TurnRightPressed);
        }

    }
}