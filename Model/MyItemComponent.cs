using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Project
{
    public class MyItemComponent : INotifyPropertyChanged
    {

        private string imageName;
        private string firstText;
        private string secondText;

        public string ImageName
        {
            get { return imageName; }
            set
            {
                imageName = value;
                OnPropertyChanged("ImageName");
            }
        }
        public string FirstText
        {
            get { return firstText; }
            set
            {
                firstText = value;
                OnPropertyChanged("FirstText");
            }
        }
        public string SecondText
        {
            get { return secondText; }
            set
            {
                secondText = value;
                OnPropertyChanged("SecondText");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
