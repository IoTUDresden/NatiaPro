using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Surface;
using Microsoft.Surface.Presentation;
using Microsoft.Surface.Presentation.Controls;
using Microsoft.Surface.Presentation.Input;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows;
using System.Globalization;

namespace Prototype_05
{
    /// <summary>
    /// Eigene TickBar zur Anzeige von individuellen Labels unter einem Tick.
    /// </summary>
    class CustomTickBar : TickBar
    {
        protected override void OnRender(System.Windows.Media.DrawingContext dc)
        {
            double num = this.Maximum - this.Minimum;
            double y = this.ReservedSpace * 0.5;
            FormattedText formattedText = null;
            double x = 0;
            int iterationSteps;
            if (this.Minimum == 0) iterationSteps = Convert.ToInt32(num);
            else iterationSteps = Convert.ToInt32(num) + 1;
            for (double i = this.Minimum; i <= iterationSteps; i += this.TickFrequency)
            {
                formattedText = new FormattedText(i.ToString(), CultureInfo.GetCultureInfo("de-DE"), FlowDirection.LeftToRight,
                    new Typeface("Segoe360"), 8, Brushes.White);
                if (this.Minimum == i)
                    x = this.Minimum;
                else
                    x += this.ActualWidth / (num / this.TickFrequency);
                dc.DrawText(formattedText, new Point(x, 10));
            }
        }
    }
}
