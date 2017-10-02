using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Prototype_05
{
    /// <summary>
    /// Klasse beinhaltet zentral wichtige Enum-Definitionen
    /// </summary>
    public class Enums
    {
        /// <summary>
        /// Enum-Definitionen für Modellkategorien
        /// </summary>
        public enum Category
        {
            Favourites,
            Safety,
            Housekeeping,
            Convenience,
            Entertainment
        }

        /// <summary>
        /// Enum-Definitionen für Prozessstadien
        /// </summary>
        public enum ProcessState
        {
            executing,
            executed,
            stopped,
            killed,
            failed,
            paused,
            waiting,
            faulty
        }

        /// <summary>
        /// Enum-Definitionen für Prozesselement-stadien
        /// </summary>
        public enum ProcessElementState
        {
            active,
            inactive
        }


        /// <summary>
        /// Enum-Definitionen für Prozesscharakter
        /// </summary>
        public enum Character
        {
            Process,
            Invoke,
            Or,
            If
        }

        /// <summary>
        /// Enum-Definitionen für Datentypen
        /// </summary>
        public enum DataType
        {
            DoubleType,
            IntType,
            TimeType,
            LocationType,
            ObjectType
        }
    }
}
