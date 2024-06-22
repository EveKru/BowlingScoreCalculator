﻿using BowlingCalculator.Models;
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
            BindingContext = _model;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Window.MinimumWidth = 480;
        }

        private void OnCalculateScoreClicked(object sender, EventArgs e)
        {
            _bowlingService.CalculateScore(_model.Frames, out int totalScore);
            _model.TotalScore = totalScore;
            ResultLabel.Text = $"Total Score: {_model.TotalScore}";
        }

        private void OnStartOverClicked(object sender, EventArgs e)
        {
            _model.Frames = _bowlingService.ResetGame();
            _model.TotalScore = 0;
            ResultLabel.Text = "Total Score: 0";
            BindingContext = null;
            BindingContext = _model; // Reset
        }
    }
}

