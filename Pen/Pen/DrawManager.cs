using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Pen
{
    public static class DrawManager
    {
        public static Brush SelectedColor { get; set; } = Brushes.Black;
        public static double SelectedPenThickness { get; set; }
        public static double SelectedMarkerThickness { get; set; }
        public static string SelectedBrush { get; set; }


    }
}
