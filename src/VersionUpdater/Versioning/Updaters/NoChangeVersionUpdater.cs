using System.Collections.Generic;
using System.Globalization;
using System.Xml.Serialization;

namespace XWolfe.VersionUpdater.Versioning.Updaters {
    /// <summary>
    /// Makes no change/update to the existing version number.
    /// </summary>
    [XmlRoot("no-change")]
    public class NoChangeVersionUpdater : VersionPartUpdater {
        #region -  Methods  -

            /// <summary>
            /// Updates the given version number.
            /// </summary>
            /// <param name="versionPart">The version number to update.</param>
            /// /// <param name="properties">The properties passed to the updater.</param>
            /// <returns>The new version number.</returns>
            public override string Update(int versionPart, Dictionary<string, string> properties) {
                return versionPart.ToString(CultureInfo.InvariantCulture);
            }

        #endregion
    }
}