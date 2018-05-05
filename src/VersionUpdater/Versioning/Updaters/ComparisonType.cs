using System.Xml.Serialization;

namespace XWolfe.VersionUpdater.Versioning.Updaters {
    /// <summary>
    /// Specifies an operator to use when performing comparisons.
    /// </summary>
    public enum ComparisonType {
        /// <summary>
        /// Specifies the values must be equal.
        /// </summary>
        [XmlEnum("equals")]
        Equal,

        /// <summary>
        /// Specifies the property must end with the value.
        /// </summary>
        [XmlEnum("ends-with")]
        EndsWith,

        /// <summary>
        /// Specifies the property must start with the value.
        /// </summary>
        [XmlEnum("starts-with")]
        StartsWith,

        /// <summary>
        /// Specifies the property must contain the value.
        /// </summary>
        [XmlEnum("contains")]
        Contains
    }
}