using System.Xml.Serialization;

namespace XWolfe.VersionUpdater.Versioning.Updaters {
    /// <summary>
    /// Specifies a unit of time measurement.
    /// </summary>
    public enum TimeUnit {
        /// <summary>
        /// Specifies the measurement of time in days.
        /// </summary>
        [XmlEnum("days")]
        Days,

        /// <summary>
        /// Specifies the measurement of time in hours.
        /// </summary>
        [XmlEnum("hours")]
        Hours,

        /// <summary>
        /// Specifies the measurement of time in minutes.
        /// </summary>
        [XmlEnum("minutes")]
        Minutes,

        /// <summary>
        /// Specifies the measurement of time in seconds.
        /// </summary>
        [XmlEnum("seconds")]
        Seconds,

        /// <summary>
        /// Specifies the measurement of time in milliseconds.
        /// </summary>
        [XmlEnum("milliseconds")]
        Milliseconds
    }
}