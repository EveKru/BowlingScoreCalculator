using System.Collections.ObjectModel;

namespace BowlingCalculator
{
    public partial class MainPageViewModel
    {
        public ObservableCollection<FrameData> Frames { get; set; }

        public MainPageViewModel()
        {
            Frames = [];

            for (int i = 1; i <= 9; i++)
            {
                Frames.Add(new FrameData { RoundNumber = i });
            }

            Frames.Add(new FrameData { RoundNumber = 10,  IsBonusThrow = true }); 
        }
    }

    public class FrameData
    {
        public int RoundNumber { get; set; }
        public string? FirstThrow { get; set; }
        public string? SecondThrow { get; set; }
        public string? ThirdThrow { get; set; } // Third throw for bonus round
        public bool IsBonusThrow { get; set; } = false;
    }
}

