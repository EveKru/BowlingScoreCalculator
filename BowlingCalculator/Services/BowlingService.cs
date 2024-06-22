using System.Collections.ObjectModel;
using BowlingCalculator.Models;

namespace BowlingCalculator.Services
{
    public class BowlingService
    {
        public void CalculateScore(ObservableCollection<FrameData> frames, out int totalScore)
        {
            totalScore = 0;

            for (int i = 0; i < 10; i++)
            {
                var frame = frames[i];

                int firstThrow = ParseThrow(frame.FirstThrow ?? "");
                int secondThrow = ParseThrow(frame.SecondThrow ?? "");
                int thirdThrow = frame.IsBonusThrow ? ParseThrow(frame.ThirdThrow ?? "") : 0;


                totalScore += firstThrow + secondThrow;

                // Check for strike
                if (firstThrow == 10)
                {
                    if (i + 1 < 10)
                    {
                        var nextFrame = frames[i + 1];
                        int nextFirstThrow = ParseThrow(nextFrame.FirstThrow ?? "");
                        totalScore += nextFirstThrow;

                        if (nextFirstThrow == 10 && i + 2 < 10) // Consecutive strikes
                        {
                            var nextNextFrame = frames[i + 2];
                            totalScore += ParseThrow(nextNextFrame.FirstThrow ?? "");
                        }
                        else
                        {
                            totalScore += ParseThrow(nextFrame.SecondThrow ?? "");
                        }
                    }
                }
                // Check for spare
                else if (firstThrow + secondThrow == 10)
                {
                    if (i + 1 < 10)
                    {
                        var nextFrame = frames[i + 1];
                        totalScore += ParseThrow(nextFrame.FirstThrow ?? "");
                    }
                }

                // Handle third throw in the 10th frame
                if (i == 9)
                {
                    totalScore += thirdThrow;
                }
            }
        }

        // method to parse throw values
        public int ParseThrow(string throwValue)
        {
            if (int.TryParse(throwValue, out int parsedValue))
            {
                return parsedValue;
            }
            return 0; // Treat empty or invalid entries as 0
        }

        public ObservableCollection<FrameData> ResetGame()
        {
            var frames = new ObservableCollection<FrameData>();

            for (int i = 1; i <= 9; i++)
            {
                frames.Add(new FrameData { RoundNumber = i });
            }

            frames.Add(new FrameData { RoundNumber = 10, IsBonusThrow = true });

            return frames;
        }
    }
}


