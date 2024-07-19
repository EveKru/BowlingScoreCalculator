using BowlingCalculator.Models;
using BowlingCalculator.Services;
using System.Text.RegularExpressions;

namespace BowlingCalculator
{
    public partial class MainPage : ContentPage
    {
        private readonly MainPageViewModel _model;
        private BowlingService _bowlingService;

        public MainPage(MainPageViewModel viewModel, BowlingService bowlingService)
        {
            InitializeComponent();
            _model = viewModel;
            _bowlingService = bowlingService; 
            _model.Frames = _bowlingService.ResetGame();
            BindingContext = _model;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Window.MinimumWidth = 480; 
        }

        // calculates the score
        private void OnCalculateScoreClicked(object sender, EventArgs e)
        {
            _bowlingService.CalculateScore(_model.Frames, out int totalScore);
            _model.TotalScore = totalScore;
            ResultLabel.Text = $"Total Score: {_model.TotalScore}";
        }

        // resets the calculator
        private void OnStartOverClicked(object sender, EventArgs e)
        {
            _model.Frames = _bowlingService.ResetGame();
            _model.TotalScore = 0;
            ResultLabel.Text = "Total Score: 0";
            BindingContext = null;
            BindingContext = _model; 
        }

        // calls the "EnterScoreForFrame" method on MainPageViewModel
        private void Entry_TextChanged(object sender, TextChangedEventArgs e)
        {
            var entry = sender as Entry;
            if (entry != null)
            {
                var frameData = entry.BindingContext as FrameData;

                if (frameData != null)
                {
                    int currentFirstThrow = _bowlingService.ParseThrow(frameData.FirstThrow ?? "");
                    int currentSecondThrow = _bowlingService.ParseThrow(frameData.SecondThrow ?? "");

                   var newText = entry.Text;

                    if (IsTextAllowed(newText, currentFirstThrow, currentSecondThrow))
                    {
                        var viewModel = BindingContext as MainPageViewModel;
                        viewModel?.EnterScoreForFrame(frameData, newText);
                    }
                    else
                    {
                        // Revert to old text if new text is invalid
                        entry.Text = e.OldTextValue;
                    }
                }
            }
        }

        private static bool IsTextAllowed(string text, int currentFirstThrow, int currentSecondThrow)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return true; // Allow empty or whitespace input
            }

            // Allow only valid characters or numbers from 0 to 10
            var regex = new Regex("^[Xx/0-9]*$");
            if (!regex.IsMatch(text))
            {
                return false; // Invalid characters present
            }

            // Check if input is a valid number and ensure it's <= 10 and does not exceed max points with first throw
            if (int.TryParse(text, out int number))
            {
                return number >= 0 && number <= 10 && (currentFirstThrow + currentSecondThrow <= 10);
            }

            // Check for valid characters 'X' or '/'
            if (text.Equals("X", StringComparison.OrdinalIgnoreCase) || text == "/")
            {
                return true;
            }

            return false;
        }


        // navigate to "ExtraInfoPage"
        private async void OnInfoPageClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ExtraInfoPage());
        }
    }
}

