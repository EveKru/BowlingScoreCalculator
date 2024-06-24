using BowlingCalculator.Models;
using BowlingCalculator.Services;

namespace BowlingCalculator
{
    public partial class MainPage : ContentPage
    {
        private readonly MainPageViewModel _model;
        private BowlingService _bowlingService;

        public MainPage()
        {
            InitializeComponent();
            _model = new MainPageViewModel();
            _bowlingService = new BowlingService(); 
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
                    var viewModel = BindingContext as MainPageViewModel;
                    viewModel?.EnterScoreForFrame(frameData, entry.Text);
                }
            }
        }
        // navigate to "ExtraInfoPage"
        private async void OnInfoPageClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ExtraInfoPage());
        }
    }
}

