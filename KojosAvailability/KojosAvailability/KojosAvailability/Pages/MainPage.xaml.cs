using KojosAvailability.Controllers.Graphs.PieChart;
using KojosAvailability.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KojosAvailability.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var status = await GartanHelper.GetOnCallStatus();

            string onCall = status.OnCall ? string.Empty : "not";

            lblMain.Text = $"You are {onCall} on call";

            pcAvailabilty.Data = new List<PieChartDataSet>()
            {
                new PieChartDataSet()
                {
                    Colour = new Color(0.0, 1, 0),
                    Value = 120,
                    Label = "One"
                },
                new PieChartDataSet()
                {
                    Colour = new Color(1, 0, 0),
                    Value = 120,
                    Label = "Two"
                },
                new PieChartDataSet()
                {
                    Colour = new Color(0.0, 0, 1),
                    Value = 120,
                    Label = "Three"
                },
            };
        }

    }
}