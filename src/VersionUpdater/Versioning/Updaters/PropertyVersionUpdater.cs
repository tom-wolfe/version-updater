using System.Collections.Generic;
using System.Xml.Serialization;

namespace XWolfe.VersionUpdater.Versioning.Updaters {
    /// <summary>
    /// Returns the value of a property regardless of the current version part.
    /// </summary>
    [XmlRoot("property")]
    public class PropertyVersionUpdater : VersionPartUpdater {
        #region -  Constructors  -

            /// <summary>
            /// Initializes a new instance of the <see cref="PropertyVersionUpdater"/> class.
            /// </summary>
            public PropertyVersionUpdater() : this(string.Empty) { }

            /// <summary>
            /// Initializes a new instance of the <see cref="PropertyVersionUpdater"/> class.
            /// </summary>
            /// <param name="name">The name of the property whose value the version number will be set to.</param>
            public PropertyVersionUpdater(string name) {
                Name = name;
            }

        #endregion

        #region -  Properties  -

            /// <summary>
            /// Gets the name of the property whose value the version number will be set to.
            /// </summary>
            [XmlAttribute("name")]
            public string Name { get; set; }

        #endregion

        #region -  Methods  -

            /// <summary>
            /// Updates the given version number.
            /// </summary>
            /// <param name="versionPart">The version number to update.</param>
            /// /// <param name="properties">The properties passed to the updater.</param>
            /// <returns>The new version number.</returns>
            public override string Update(int versionPart, Dictionary<string, string> properties) {
                return !properties.ContainsKey(Name) ? string.Empty : properties[Name];
            }

        #endregion
    }
}