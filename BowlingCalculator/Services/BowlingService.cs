using System.Collections.ObjectModel;
using BowlingCalculator.Models;

namespace BowlingCalculator.Services
{
    public class BowlingService
    {
        public void CalculateScore(ObservableCollection<FrameData> frames, out int totalScore)
        {
            totalScore = 0;

            // iterates all the frames
            for (int i = 0; i < 10; i++)
            {
                var frame = frames[i];

                int firstThrow = ParseThrow(frame.FirstThrow ?? "");
                int secondThrow = frame.IsSecondThrowEnabled ? ParseThrow(frame.SecondThrow ?? "", firstThrow) : 0;
                int thirdThrow = frame.IsBonusThrow ? ParseThrow(frame.ThirdThrow ?? "") : 0;

                // Add frame score
                totalScore += firstThrow + secondThrow;

                // Check for strike
                if (firstThrow == 10)
                {
                    if (i + 1 < 10)
                    {
                        var nextFrame = frames[i + 1];
                        int nextFirstThrow = ParseThrow(nextFrame.FirstThrow ?? "");
                        totalScore += nextFirstThrow;

                        if (nextFirstThrow == 10 && i + 2 < 10)
                        {
                            var nextNextFrame = frames[i + 2];
                            totalScore += ParseThrow(nextNextFrame.FirstThrow ?? "");
                        }
                        else
                        {
                            totalScore += ParseThrow(nextFrame.SecondThrow ?? "", firstThrow = nextFirstThrow); 
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

        // parses throw values
        public int ParseThrow(string throwValue, int firstThrow = 0) 
        {
            if (throwValue.ToLower() == "x")
            {
                return 10; // Strike
            }

            if (throwValue == "/") 
            {
                var value = 10 - firstThrow; 
                return value; 
            }

            if (int.TryParse(throwValue, out int parsedValue))
            {
                return parsedValue;
            }

            return 0; // Treat empty or invalid entries as 0
        }

        // resets game
        public ObservableCollection<FrameData> ResetGame()
        {
            var frames = new ObservableCollection<FrameData>();

            for (int i = 1; i <= 9; i++)
            {
                frames.Add(new FrameData { RoundNumber = i, IsSecondThrowEnabled = true });
            }

            frames.Add(new FrameData { RoundNumber = 10, IsBonusThrow = true, IsSecondThrowEnabled = true });

            return frames;
        }

        //  handles the enabling and disabling of the second throw frame when entering a score
        public void EnterScoreForFrame(FrameData frame, string input)
        {
            if (input.ToLower() == "x" || input == "10")
            {
                frame.FirstThrow = input.ToLower() == "x" ? "X" : "10";
                frame.IsSecondThrowEnabled = false; // Disable second throw if strike
            }
            else
            {
                frame.FirstThrow = input;
                frame.IsSecondThrowEnabled = true; // Enable second throw for other cases
            }
        }

    }
}






