using System.ComponentModel;

namespace KE20_DCU.Model
{
    public class SpeedRevs : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public int Kmph { get; set; }
        public int Rpm { get; set; }

        public string KmphString => Kmph.ToString("000");
        public string RpmString => Rpm.ToString("0000");
    }
}
