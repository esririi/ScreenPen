using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Xceed.Wpf.Toolkit;

namespace Pen
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DrawingWindow _drawingWindow;
        

        public MainWindow()
        {
            InitializeComponent();
            _drawingWindow = new DrawingWindow();
            this.Closed += MainWindow_Closed;

        }

        private void btnSwitchPen_Click(object sender, RoutedEventArgs e)
        {
            _drawingWindow.Show();
            DrawManager.SelectedBrush = "pen";
        }

        private void MainWindow_Closed(object sender, System.EventArgs e)
        {
            if (_drawingWindow != null)
            {
                _drawingWindow.Close();
            }
        }

        private void btnSwitchMouse_Click(object sender, RoutedEventArgs e)
        {
            if (_drawingWindow != null)
            {
                _drawingWindow.Hide();
            }
        }

        private void btnSwitchColor_Click(object sender, RoutedEventArgs e)
        {
            ColorPicker colorPicker = new ColorPicker();
            Button btnApply = new Button
            {
                Content = "Uygula",
                Width = 100,
                Height = 30,
                Margin = new Thickness(10)
            };

            

            // Grid oluşturun
            Grid grid = new Grid();
            grid.RowDefinitions.Add(new RowDefinition());
            grid.RowDefinitions.Add(new RowDefinition());

            // ColorPicker'ı ve butonu Grid'e ekleyin
            Grid.SetRow(colorPicker, 0);
            Grid.SetRow(btnApply, 1);
            grid.Children.Add(colorPicker);
            grid.Children.Add(btnApply);

            // Pencerenin içeriğini Grid olarak ayarlayın
            Window colorPickerWindow = new Window
            {
                Content = grid,
                Width = 200,
                Height = 200,
                Title = "Renk Seçici"
            };

            btnApply.Click += (s, a) =>
            {
                Brush selectedColor = new SolidColorBrush((Color)colorPicker.SelectedColor);
                DrawManager.SelectedColor = selectedColor;
                colorPickerWindow.Close();
            };
            // Pencereyi gösterin
            colorPickerWindow.Show();
        }

        private void sldrPenThickness_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            DrawManager.SelectedPenThickness = e.NewValue;
        }

        private void sldrMarkerThickness_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            DrawManager.SelectedMarkerThickness = e.NewValue;
        }

        private void btnSwitchMarker_Click(object sender, RoutedEventArgs e)
        {
            _drawingWindow.Show();
            DrawManager.SelectedBrush = "marker";

        }

        private void btnSwitchEraser_Click(object sender, RoutedEventArgs e)
        {
            _drawingWindow.Show();
            DrawManager.SelectedBrush = "eraser";
        }
    }
}
