using System.Xml.Serialization;

namespace XWolfe.VersionUpdater.Versioning.Updaters {
    /// <summary>
    /// Specifies the operation mode of a <see cref="MultiPartVersionUpdater"/> object.
    /// </summary>
    public enum MultiPartMode {
        /// <summary>
        /// Specifies that version number parts should be concatenated as text.
        /// </summary>
        [XmlEnum("concat")]
        Concatenate,

        /// <summary>
        /// Specifies that the version number parts should be added together.
        /// </summary>
        [XmlEnum("sum")]
        Sum
    }
}