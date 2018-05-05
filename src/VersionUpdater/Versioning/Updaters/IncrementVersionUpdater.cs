using System.Collections.Generic;
using System.Globalization;
using System.Xml.Serialization;

namespace XWolfe.VersionUpdater.Versioning.Updaters {
    /// <summary>
    /// Returns a fixed number regardless of the current version part.
    /// </summary>
    [XmlRoot("increment")]
    public class IncrementVersionUpdater : MultiPartVersionUpdater {
        #region -  Constructors  -

            /// <summary>
            /// Initializes a new instance of the <see cref="IncrementVersionUpdater"/> class.
            /// </summary>
            public IncrementVersionUpdater() : this(1) {
                Mode = MultiPartMode.Sum;
            }

            /// <summary>
            /// Initializes a new instance of the <see cref="IncrementVersionUpdater"/> class.
            /// </summary>
            /// <param name="value">The value that the version number will be incremented by.</param>
            public IncrementVersionUpdater(int value) {
                Value = value;
            }

        #endregion

        #region -  Properties  -

            /// <summary>
            /// Gets the value that the version number will be incremented by.
            /// </summary>
            [XmlAttribute("value")]
            public int Value { get; set; }

        #endregion

        #region -  Methods  -

            /// <summary>
            /// Updates the given version number.
            /// </summary>
            /// <param name="versionPart">The version number to update.</param>
            /// /// <param name="properties">The properties passed to the updater.</param>
            /// <returns>The new version number.</returns>
            public override string Update(int versionPart, Dictionary<string, string> properties) {
                if (Updaters.Count == 0) return (versionPart + Value).ToString(CultureInfo.InvariantCulture);
                return (versionPart + int.Parse(base.Update(versionPart, properties))).ToString(CultureInfo.InvariantCulture);
            }

        #endregion
    }
}