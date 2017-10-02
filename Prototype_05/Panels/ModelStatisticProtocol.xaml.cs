using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Prototype_05
{
    /// <summary>
    /// Interaktionslogik für ModelStatisticProtocol.xaml
    /// Zeigt Fake-Historydaten zu einem Prozessmodell an.
    /// </summary>
    public partial class ModelStatisticProtocol : UserControl
    {
        /// <summary>
        /// zufällige Anzahl Fehler im Zeitraum
        /// </summary>
        private Random randomErrors;
        /// <summary>
        /// zufällige Anzahl Instanzen im Zeitraum
        /// </summary>
        private Random randomInstances;
        /// <summary>
        /// zufällige Abweichung von druchschnittlicher Ausführungsdauer im Zeitraum
        /// </summary>
        private Random randomMinutes;

        /// <summary>
        /// Konstruktor
        /// </summary>
        public ModelStatisticProtocol()
        {
            this.InitializeComponent();
            randomErrors = new Random();
            randomInstances = new Random();
            randomMinutes = new Random();
        }

        /// <summary>
        /// Generiert Zufallswerte anhand von gegebenen Daten.
        /// Berücksichtigt vorhergehende Zufallsdaten damit mehrere Durchläufe weil sonst immer wieder die gleichen "Zufallszahlen" kommen
        /// </summary>
        /// <param name="datespan">Zeitraum</param>
        /// <param name="errorcause">Fehlerursache</param>
        /// <param name="avgHours">durchschnittliche Ausführungsdauer (stunden)</param>
        /// <param name="avgMinutes">durchschnittliche Ausführungsdauer (Minuten)</param>
        /// <param name="preRandoms">vorhergehende ermittelte Zufallswerte</param>
        /// <returns></returns>
        public int[] AdaptContent(String datespan, String errorcause, int avgHours, int avgMinutes, int[] preRandoms)
        {
            int[] currentRandoms = preRandoms;
            int rndminutes = randomMinutes.Next(-5, 6);

            DateText.Text = datespan;

            int instances = 0;
            for (int i = 0; i < 3; i++)
            {
                instances = randomInstances.Next(0, 10);
                if (instances != 0 && instances != preRandoms[0]) break;
            }
            Instances.Text = instances.ToString();
            if (instances != 0)
            {
                while (rndminutes == preRandoms[1])
                {
                    rndminutes = randomMinutes.Next(-5, 6);
                }

                int minutes = avgMinutes + rndminutes;
                AvgDuration.Text = "0" + avgHours + "h " + minutes + "'";

                int errors = randomInstances.Next(0, 4);
                if (errors != 0)
                {
                    Errors.Text = errors.ToString();
                    ErrorCause.Text = errorcause;
                }
                else
                {
                    Errors.Text = "none";
                    ErrorCause.Text = "-";
                }
            }
            else
            {
                AvgDuration.Text = "-";
                Errors.Text = "none";
                ErrorCause.Text = "-";
            }
            currentRandoms[0] = instances;
            currentRandoms[1] = rndminutes;
            return currentRandoms;
        }
    }
}