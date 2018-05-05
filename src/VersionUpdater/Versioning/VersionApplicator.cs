using System;
using System.Text.RegularExpressions;

namespace XWolfe.VersionUpdater.Versioning {
    /// <summary>
    /// Provides functionality for retrieving or applying version attributes to source code.
    /// </summary>
    public class VersionApplicator {
        #region -  Constants  - 

            private const string ASSEMBLY_VERSION_REPLACE = "AssemblyVersion(\"{0}\")";
            private const string ASSEMBLY_VERSION_REGEX = "AssemblyVersion(?:Attribute)?\\(\\s*?\"(?<version>(?<major>[0-9]+)\\.(?<minor>[0-9]+)\\.(?<build>[0-9]+)\\.(?<revision>[0-9]+))\"\\s*?\\)";

            private const string ASSEMBLY_FILE_VERSION_REPLACE = "AssemblyFileVersion(\"{0}\")";
            private const string ASSEMBLY_FILE_VERSION_REGEX = "AssemblyFileVersion(?:Attribute)?\\(\\s*?\"(?<version>(?<major>[0-9]+)\\.(?<minor>[0-9]+)\\.(?<build>[0-9]+)\\.(?<revision>[0-9]+))\"\\s*?\\)";

        #endregion

        #region -  Constructors  -

            /// <summary>
            /// Initializes a new instance of the <see cref="VersionApplicator"/> class.
            /// </summary>
            /// <param name="source">The source code that contains the versioning information.</param>
            public VersionApplicator(string source) {
                Source = source;
            }

        #endregion

        #region -  Properties  -

            /// <summary>
            /// Gets the source code that contains the versioning information.
            /// </summary>
            public string Source { get; protected set; }

        #endregion

        #region -  Methods  -

            /// <summary>
            /// Gets the version set in the current source, or null if one does not exist.
            /// </summary>
            /// <param name="versionType">The type of version to retrieve from the source.</param>
            /// <returns>The found version, or null if one does not exist.</returns>
            public Version GetVersion(VersionType versionType) {
                var regex = CreateVersionRegex(versionType);
                var match = regex.Match(Source);
                return new Version(match.Groups["version"].Value);
            }

            /// <summary>
            /// Sets the version in the current source.
            /// </summary>
            /// <param name="versionTypes">The types of version to set in the source.</param>
            /// <param name="newVersion">The new version object to apply, or null to remove the version.</param>
            public void SetVersions(VersionType versionTypes, Version newVersion) {
                if ((versionTypes & VersionType.Assembly) == VersionType.Assembly) { SetVersionCore(VersionType.Assembly, newVersion); }
                if ((versionTypes & VersionType.File) == VersionType.File) { SetVersionCore(VersionType.File, newVersion); }
            }

            private void SetVersionCore(VersionType versionType, Version newVersion) {
                var regex = CreateVersionRegex(versionType);
                Source = regex.Replace(Source, GetVersionReplacevalue(versionType, newVersion));
            }

        #endregion

        #region -  Utility Methods  -

            /// <summary>
            /// Creates a regular expression to match the version attribute, based on the version type.
            /// </summary>
            /// <param name="versionType">The type of version to retrieve the regular expression for.</param>
            /// <returns>The created regular expression.</returns>
            private static Regex CreateVersionRegex(VersionType versionType) {
                switch (versionType) {
                    case VersionType.File: return new Regex(ASSEMBLY_FILE_VERSION_REGEX);
                    default: return new Regex(ASSEMBLY_VERSION_REGEX);
                }
            }

            /// <summary>
            /// Gets the replacement value for a version of the given type, containing the specified version information.
            /// </summary>
            /// <param name="versionType">The type of version to create the attribute for.</param>
            /// <param name="version">The version that the attribute shall contain.</param>
            /// <returns>The full version attribute as text.</returns>
            private static string GetVersionReplacevalue(VersionType versionType, Version version) {
                string newVersion;
                switch (versionType) {
                    case VersionType.File: newVersion = ASSEMBLY_FILE_VERSION_REPLACE; break;
                    default: newVersion = ASSEMBLY_VERSION_REPLACE; break;
                }
                return string.Format(newVersion, version.ToString(4));
            }

        #endregion
    }
}