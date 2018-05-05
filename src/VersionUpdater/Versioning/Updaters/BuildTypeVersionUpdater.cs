using System.Collections.Generic;
using System.Globalization;
using System.Xml.Serialization;

namespace XWolfe.VersionUpdater.Versioning.Updaters {
    /// <summary>
    /// Returns a version number based on the "build" property passed to the configuration.
    /// </summary>
    [XmlRoot("build-type")]
    public class BuildTypeVersionUpdater : VersionPartUpdater {
        #region -  Constants  -

            private const string BUILD_TYPE_PROPERTY = "build";
            private const string BUILD_TYPE_DEBUG = "debug";
            private const string BUILD_TYPE_ALPHA = "alpha";
            private const string BUILD_TYPE_BETA = "beta";
            private const string BUILD_TYPE_CANDIDATE = "candidate";
            private const string BUILD_TYPE_RELEASE = "release";

        #endregion

        #region -  Constructors  -

            /// <summary>
            /// Initializes a new instance of the <see cref="BuildTypeVersionUpdater"/> class.
            /// </summary>
            public BuildTypeVersionUpdater() : this(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty) { }

            /// <summary>
            /// Initializes a new instance of the <see cref="BuildTypeVersionUpdater"/> class.
            /// </summary>
            /// <param name="debug">The value to return when the version is a debug build.</param>
            /// <param name="alpha">The value to return when the version is an alpha build.</param>
            /// <param name="beta">The value to return when the version is an beta build.</param>
            /// <param name="candidate">The value to return when the version is an candidate build.</param>
            /// <param name="release">The value to return when the version is an release build.</param>
            public BuildTypeVersionUpdater(string debug, string alpha, string beta, string candidate, string release) {
                Alpha = alpha;
                Beta = beta;
                Candidate = candidate;
                Release = release;
            }

        #endregion

        #region -  Properties  -

            /// <summary>
            /// Gets the version number this updater will return when updating the version of a debug build.
            /// </summary>
            [XmlAttribute("debug")]
            public string Debug { get; set; }

            /// <summary>
            /// Gets the version number this updater will return when updating the version of an alpha build.
            /// </summary>
            [XmlAttribute("alpha")]
            public string Alpha { get; set; }

            /// <summary>
            /// Gets the version number this updater will return when updating the version of a beta build.
            /// </summary>
            [XmlAttribute("beta")]
            public string Beta { get; set; }

            /// <summary>
            /// Gets the version number this updater will return when updating the version of a release candidate build.
            /// </summary>
            [XmlAttribute("candidate")]
            public string Candidate { get; set; }

            /// <summary>
            /// Gets the version number this updater will return when updating the version of a release build.
            /// </summary>
            [XmlAttribute("release")]
            public string Release { get; set; }

        #endregion

        #region -  Methods  -

            /// <summary>
            /// Updates the given version number.
            /// </summary>
            /// <param name="versionPart">The version number to update.</param>
            /// /// <param name="properties">The properties passed to the updater.</param>
            /// <returns>The new version number.</returns>
            public override string Update(int versionPart, Dictionary<string, string> properties) {
                if (!properties.ContainsKey(BUILD_TYPE_PROPERTY)) return versionPart.ToString(CultureInfo.InvariantCulture);
                switch (properties[BUILD_TYPE_PROPERTY].ToLower()) {
                    case BUILD_TYPE_ALPHA : return Alpha;
                    case BUILD_TYPE_BETA : return Beta;
                    case BUILD_TYPE_CANDIDATE : return Candidate;
                    case BUILD_TYPE_RELEASE : return Release;
                    default: return Debug;
                }
            }

        #endregion
    }
}