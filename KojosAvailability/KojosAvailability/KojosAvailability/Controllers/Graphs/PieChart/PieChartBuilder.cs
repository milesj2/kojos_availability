using System;
using System.Collections.Generic;
using System.Text;

namespace KojosAvailability.Controllers.Graphs.PieChart
{
    public class PieChartBuilder
    {
        private PieChartControl _pieChart = new PieChartControl();

        private PieChartControl AddData(List<PieChartDataSet> data, string dataProperty)
        {
            _pieChart.Data = data;
            return _pieChart;
        }

        public PieChartControl AddShadow()
        {
            _pieChart.ShadowEnabled = true;
            return _pieChart;
        }

        public PieChartControl Build() => _pieChart;

    }
}
