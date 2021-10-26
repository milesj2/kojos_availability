using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using KojosAvailability.Controllers.Graphs.PieChart;
using KojosAvailability.Droid.Renderers.Graphs;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Color = Xamarin.Forms.Color;

[assembly: ExportRenderer(typeof(PieChartControl), typeof(PieChartRenderer))]
namespace KojosAvailability.Droid.Renderers.Graphs
{
    public class PieChartRenderer : VisualElementRenderer<BoxView>
    {

        public PieChartRenderer(Context context) : base(context) { }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnElementChanged(ElementChangedEventArgs<BoxView> e)
        {
            base.OnElementChanged(e);

            SetWillNotDraw(false);

            Invalidate();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == PieChartControl.DataProperty.PropertyName)
            {
                Invalidate();
            }
        }


        public override void Draw(Canvas canvas)
        {
            var chart = Element as PieChartControl;
            base.Draw(canvas);

            Paint paint = new Paint();

            paint.SetStyle(Paint.Style.Stroke);
            paint.StrokeWidth = (float)10;

            var size = GetSmallestDimension(canvas.Height, canvas.Width) - paint.StrokeWidth;

            var total = chart.Data.Sum(ds => ds.Value);

            paint.SetStyle(Paint.Style.Stroke);
            paint.StrokeWidth = (float)10;
            paint.SetShadowLayer(20, 0, 5, Android.Graphics.Color.Argb(100, 0, 0, 0));

            double lastPosition = 0;
            foreach (PieChartDataSet dataSet in chart.Data)
            {
                double length = Math.Ceiling(360 * (dataSet.Value / total));
                SetARGBColour(paint, dataSet.Colour);
                canvas.DrawArc(new RectF(0, 0, size, size), (float)lastPosition, (float)length, false, paint);
                lastPosition += length;
            }
            SetLayerType(Android.Views.LayerType.Software, paint);
        }

        private int GetSmallestDimension(int height, int width) => height > width ? width : height;

        private void SetARGBColour(Paint paint, Color color) =>
            paint.SetARGB(convertTo255ScaleColor(color.A), convertTo255ScaleColor(color.R), convertTo255ScaleColor(color.G), convertTo255ScaleColor(color.B));

        private int convertTo255ScaleColor(double color)
        {
            return (int)Math.Ceiling(color * 255);
        }


    }
}