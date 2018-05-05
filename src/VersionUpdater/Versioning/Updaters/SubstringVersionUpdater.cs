using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace XWolfe.VersionUpdater.Versioning.Updaters {
    /// <summary>
    /// Returns a version number based on the "build" property passed to the configuration.
    /// </summary>
    [XmlRoot("substring")]
    public class SubstringTypeVersionUpdater : MultiPartVersionUpdater {
        #region -  Constructors  -

            /// <summary>
            /// Initializes a new instance of the <see cref="SubstringTypeVersionUpdater"/> class.
            /// </summary>
            public SubstringTypeVersionUpdater() : this(0, -1) {}

            /// <summary>
            /// Initializes a new instance of the <see cref="SubstringTypeVersionUpdater"/> class.
            /// </summary>
            /// <param name="start">The zero-based index at which to start retrieving characters.</param>
            /// <param name="length">The maximum number of characters to retrieve, or -1 to retrieve until the end of the component.</param>
            public SubstringTypeVersionUpdater(int start, int length) {
                Start = start;
                Length = length;
            }

        #endregion

        #region -  Properties  -

            /// <summary>
            /// Gets the version number this updater will return when updating the version of a debug build.
            /// </summary>
            [XmlAttribute("start")]
            public int Start { get; set; }

            /// <summary>
            /// Gets the version number this updater will return when updating the version of an alpha build.
            /// </summary>
            [XmlAttribute("length")]
            public int Length { get; set; }

        #endregion

        #region -  Methods  -

            /// <summary>
            /// Updates the given version number.
            /// </summary>
            /// <param name="versionPart">The version number to update.</param>
            /// /// <param name="properties">The properties passed to the updater.</param>
            /// <returns>The new version number.</returns>
            public override string Update(int versionPart, Dictionary<string, string> properties) {
                var versionString = base.Update(versionPart, properties);
                var start = Math.Max(Start, 0);
                if (versionString.Length - start < Length || Length < 0) {
                    return versionString.Substring(Math.Max(start, 0));
                }
                return versionString.Substring(Math.Max(start, 0), Length);
            }

        #endregion
    }
}