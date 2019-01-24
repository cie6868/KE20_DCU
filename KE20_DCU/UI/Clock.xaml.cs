using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace KE20_DCU.UI
{
    public sealed partial class Clock : UserControl
    {
        private string mTimeString = DateTime.Now.ToString("HH:mm:ss");

        public Clock()
        {
            this.InitializeComponent();
            
            DispatcherTimer t = new DispatcherTimer();
            t.Tick += (s, e) =>
            {
                mTimeString = DateTime.Now.ToString("HH : mm : ss");
                Bindings.Update();
            };
            t.Interval = TimeSpan.FromMilliseconds(500);
            t.Start();
        }
    }
}
