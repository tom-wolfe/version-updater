using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml.Serialization;

namespace XWolfe.VersionUpdater.Versioning.Updaters {
    /// <summary>
    /// Returns a fixed number regardless of the current version part.
    /// </summary>
    [XmlRoot("time-since")]
    public class TimeSinceVersionUpdater : VersionPartUpdater {
        #region -  Constructors  -

            /// <summary>
            /// Initializes a new instance of the <see cref="TimeSinceVersionUpdater"/> class.
            /// </summary>
            public TimeSinceVersionUpdater() : this(new DateTime(2000, 1, 1, 0, 0, 0)) {}

            /// <summary>
            /// Initializes a new instance of the <see cref="TimeSinceVersionUpdater"/> class.
            /// </summary>
            /// <param name="date">The date that the 'time since' will be measured from.</param>
            public TimeSinceVersionUpdater(DateTime date) {
                Date = date.ToString(CultureInfo.InvariantCulture);
            }

        #endregion

        #region -  Properties  -

            /// <summary>
            /// Gets the fixed version number that this updater will return.
            /// </summary>
            [XmlAttribute("date")]
            public string Date { get; set; }

            /// <summary>
            /// Gets or sets the unit of measurement that the result will be returned in.
            /// </summary>
            [XmlAttribute("unit")]
            public TimeUnit Unit { get; set; }

        #endregion

        #region -  Methods  -

            /// <summary>
            /// Updates the given version number.
            /// </summary>
            /// <param name="versionPart">The version number to update.</param>
            /// /// <param name="properties">The properties passed to the updater.</param>
            /// <returns>The new version number.</returns>
            public override string Update(int versionPart, Dictionary<string, string> properties) {
                var timeSpan = DateTime.Now - CalculateDate(Date);
                switch(Unit) {
                    case TimeUnit.Days: return ((int)Math.Floor(timeSpan.TotalDays)).ToString(CultureInfo.InvariantCulture);
                    case TimeUnit.Hours: return ((int)Math.Floor(timeSpan.TotalHours)).ToString(CultureInfo.InvariantCulture);
                    case TimeUnit.Minutes: return ((int)Math.Floor(timeSpan.TotalMinutes)).ToString(CultureInfo.InvariantCulture);
                    case TimeUnit.Seconds: return ((int)Math.Floor(timeSpan.TotalSeconds)).ToString(CultureInfo.InvariantCulture);
                    case TimeUnit.Milliseconds: return ((int)Math.Floor(timeSpan.TotalMilliseconds)).ToString(CultureInfo.InvariantCulture);
                    default: return ((int)Math.Floor(timeSpan.TotalDays)).ToString(CultureInfo.InvariantCulture);
                }
            }

            private static DateTime CalculateDate(string date) {
                DateTime retDate;
                if (!DateTime.TryParse(date, out retDate)) {
                    var now = DateTime.Now;
                    switch (date.ToLower()) {
                        case "midnight":
                            retDate = new DateTime(now.Year, now.Month, now.Day, 0, 0, 0);
                            break;
                    }
                }
                return retDate;
            }

        #endregion
    }
}