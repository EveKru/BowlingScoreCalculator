using System.ComponentModel;

namespace BowlingCalculator.Models
{
    public class FrameData : INotifyPropertyChanged
    {
        private string? _firstThrow;
        private string? _secondThrow;
        private string? _thirdThrow;
        private bool _isSecondThrowEnabled;

        public int RoundNumber { get; set; }
        public bool IsBonusThrow { get; set; }

        public string? FirstThrow
        {
            get => _firstThrow;
            set
            {
                if (_firstThrow != value)
                {
                    _firstThrow = value;
                    OnPropertyChanged(nameof(FirstThrow));

                    // Disable second throw if strike (either "X" or "10")
                    IsSecondThrowEnabled = !(value?.ToLower() == "x" || value == "10");
                }
            }
        }

        public string? SecondThrow
        {
            get => _secondThrow;
            set
            {
                if (_secondThrow != value)
                {
                    _secondThrow = value;
                    OnPropertyChanged(nameof(SecondThrow));
                }
            }
        }

        public string? ThirdThrow
        {
            get => _thirdThrow;
            set
            {
                if (_thirdThrow != value)
                {
                    _thirdThrow = value;
                    OnPropertyChanged(nameof(ThirdThrow));
                }
            }
        }

        public bool IsSecondThrowEnabled
        {
            get => _isSecondThrowEnabled;
            set
            {
                if (_isSecondThrowEnabled != value)
                {
                    _isSecondThrowEnabled = value;
                    OnPropertyChanged(nameof(IsSecondThrowEnabled));
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}



