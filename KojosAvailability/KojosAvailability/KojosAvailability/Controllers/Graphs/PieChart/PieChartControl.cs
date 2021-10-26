using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace KojosAvailability.Controllers.Graphs.PieChart
{
    public class PieChartControl : BoxView
    {
        public static readonly BindableProperty DataProperty =
                    BindableProperty.Create(nameof(Data),
                                            typeof(List<PieChartDataSet>),
                                            typeof(PieChartControl),
                                            new List<PieChartDataSet>());

        public static readonly BindableProperty ShadowEnabledProperty =
            BindableProperty.Create(nameof(ShadowEnabled),
                                    typeof(bool),
                                    typeof(PieChartControl),
                                    false);

        public List<PieChartDataSet> Data
        {
            get { return (List<PieChartDataSet>)GetValue(DataProperty); }
            set { SetValue(DataProperty, value); }
        }

        public bool ShadowEnabled
        {
            get { return (bool)GetValue(ShadowEnabledProperty); }
            set { SetValue(ShadowEnabledProperty, value); }
        }

    }
}
