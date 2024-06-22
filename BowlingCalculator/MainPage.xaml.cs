namespace BowlingCalculator
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            // do not remove!!!
            InitializeComponent();

            // Instantiate the view model and set as BindingContext
            MainPageViewModel viewModel = new();
            BindingContext = viewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Window.MinimumWidth = 480;
        }
    }
}
