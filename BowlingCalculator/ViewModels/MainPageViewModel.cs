using BowlingCalculator.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace BowlingCalculator.Models
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        private BowlingService? _bowlingService;
        private ObservableCollection<FrameData>? _frames;

        // OnPropertyChanged implementation
        public ObservableCollection<FrameData> Frames
        {
            get { return _frames!; }
            set
            {
                _frames = value;
                OnPropertyChanged(nameof(Frames));
            }
        }

        // no actual use and implementation of the OnPropertyChanged for TotalScore at the moment. Is currently manually triggered.
        private int _totalScore;
        public int TotalScore
        {
            get { return _totalScore; }
            set
            {
                _totalScore = value;
                OnPropertyChanged(nameof(TotalScore));
            }
        }

        // INotifyPropertyChanged implementation
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // iterates trough all the frames
        public MainPageViewModel() 
        {
            Frames = [];

            for (int i = 1; i <= 9; i++)
            {
                Frames.Add(new FrameData { RoundNumber = i });
            }

            Frames.Add(new FrameData { RoundNumber = 10, IsBonusThrow = true });
        }

        // calls the "EnterScoreForFrame" method from bowlingservice
        public void EnterScoreForFrame(FrameData frame, string input)
        {
            _bowlingService?.EnterScoreForFrame(frame, input);
            OnPropertyChanged(nameof(Frames)); // Notify UI of changes
        }

    }
}

