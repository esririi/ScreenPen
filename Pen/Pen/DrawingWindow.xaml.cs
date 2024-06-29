using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Pen
{
    /// <summary>
    /// Interaction logic for DrawingWindow.xaml
    /// </summary>
    public partial class DrawingWindow : Window
    {
        private Polyline _currentPolyline;
        private bool _isDrawing;
        
       

        public DrawingWindow()
        {
            InitializeComponent();
        }
       

        private void Canvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            if (DrawManager.SelectedBrush == "eraser")
            {
                _isDrawing = false;
                Erase(e.GetPosition(DrawingCanvas));
                return;
            }

            _isDrawing = true;
            double thickness = 2;
            Brush stroke = Brushes.Black;
            
            Point startPoint = e.GetPosition(DrawingCanvas);


            if (DrawManager.SelectedBrush == "marker")
            {
                SolidColorBrush opaqueColor = new SolidColorBrush(((SolidColorBrush)DrawManager.SelectedColor).Color);
                opaqueColor.Opacity = 0.5;
                stroke = opaqueColor;
                thickness = DrawManager.SelectedMarkerThickness;
            }
            else if (DrawManager.SelectedBrush == "pen")
            {
                stroke = new SolidColorBrush(((SolidColorBrush)DrawManager.SelectedColor).Color);
                thickness = DrawManager.SelectedPenThickness;
            }
            
            _currentPolyline = new Polyline
            {
                Stroke = stroke,
                StrokeThickness = thickness,
            };
            _currentPolyline.Points.Add(startPoint);
            DrawingCanvas.Children.Add(_currentPolyline);
        }

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (_isDrawing)
            {
                Point currentPoint = e.GetPosition(DrawingCanvas);
                _currentPolyline.Points.Add(currentPoint);
            }
            else if (DrawManager.SelectedBrush == "eraser" && e.LeftButton == MouseButtonState.Pressed)
            {
                Erase(e.GetPosition(DrawingCanvas));
            }
        }

        private void Canvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            _isDrawing = false;
        }
        private void Erase(Point position)
        {
            var hitResults = new HitTestResultCallback(MyHitTestResult);
            VisualTreeHelper.HitTest(DrawingCanvas, null, hitResults, new PointHitTestParameters(position));
        }

        private HitTestResultBehavior MyHitTestResult(HitTestResult result)
        {
            if (result.VisualHit is Polyline polyline)
            {
                DrawingCanvas.Children.Remove(polyline);
            }

            return HitTestResultBehavior.Stop;
        }

    }
}
