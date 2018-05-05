using System.Collections.Generic;
using System.Globalization;
using System.Xml.Serialization;

namespace XWolfe.VersionUpdater.Versioning.Updaters {
    /// <summary>
    /// Returns a fixed number regardless of the current version part.
    /// </summary>
    [XmlRoot("fixed")]
    public class FixedVersionUpdater : VersionPartUpdater {
        #region -  Constructors  -

            /// <summary>
            /// Initializes a new instance of the <see cref="FixedVersionUpdater"/> class.
            /// </summary>
            public FixedVersionUpdater() : this(0) {}

            /// <summary>
            /// Initializes a new instance of the <see cref="FixedVersionUpdater"/> class.
            /// </summary>
            /// <param name="value">The fixed version number that this updater will return.</param>
            public FixedVersionUpdater(int value) {
                Value = value;
            }

        #endregion

        #region -  Properties  -

            /// <summary>
            /// Gets the fixed version number that this updater will return.
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
                return Value.ToString(CultureInfo.InvariantCulture);
            }

        #endregion
    }
}