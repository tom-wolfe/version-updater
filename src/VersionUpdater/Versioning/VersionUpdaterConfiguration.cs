using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using XWolfe.VersionUpdater.Versioning.Updaters;

namespace XWolfe.VersionUpdater.Versioning {
    /// <summary>
    /// Defines a configuration for updating version objects.
    /// </summary>
    [XmlRoot("version")]
    public class VersionUpdaterConfiguration {
        #region -  Constructors  -

            /// <summary>
            /// Initializes a new instance of the <see cref="VersionUpdaterConfiguration"/> class.
            /// </summary>
            public VersionUpdaterConfiguration() {
                MajorUpdater = new MultiPartVersionUpdater();
                MinorUpdater = new MultiPartVersionUpdater();
                BuildUpdater = new MultiPartVersionUpdater();
                RevisionUpdater = new MultiPartVersionUpdater();
            }

            /// <summary>
            /// Initializes a new instance of the <see cref="VersionUpdaterConfiguration"/> class.
            /// </summary>
            public VersionUpdaterConfiguration(VersionPartUpdater major, VersionPartUpdater minor, VersionPartUpdater build, VersionPartUpdater revision) {
                MajorUpdater.Updaters.Add(major);
                MinorUpdater.Updaters.Add(minor);
                BuildUpdater.Updaters.Add(build);
                RevisionUpdater.Updaters.Add(revision);
            }

        #endregion

        #region -  Properties  -

            /// <summary>
            /// Gets the updater responsible for calculating the major version number.
            /// </summary>
            [XmlElement("major")]
            public MultiPartVersionUpdater MajorUpdater { get; set; }

            /// <summary>
            /// Gets the updater responsible for calculating the minor version number.
            /// </summary>
            [XmlElement("minor")]
            public MultiPartVersionUpdater MinorUpdater { get; set; }

            /// <summary>
            /// Gets the updater responsible for calculating the build number.
            /// </summary>
            [XmlElement("build")]
            public MultiPartVersionUpdater BuildUpdater { get; set; }

            /// <summary>
            /// Gets the updater responsible for calculating the revision number.
            /// </summary>
            [XmlElement("revision")]
            public MultiPartVersionUpdater RevisionUpdater { get; set; }

        #endregion

        #region -  Methods  -

            /// <summary>
            /// Updates a version object.
            /// </summary>
            /// <param name="version">The version to update.</param>
            /// <returns>A new version object containing the updated version.</returns>
            public virtual Version Update(Version version) {
                return Update(version, new Dictionary<string, string>(StringComparer.CurrentCultureIgnoreCase));
            }

            /// <summary>
            /// Updates a version object.
            /// </summary>
            /// <param name="version">The version to update.</param>
            /// <param name="properties">The properties passed to the updater.</param>
            /// <returns>A new version object containing the updated version.</returns>
            public virtual Version Update(Version version, Dictionary<string, string> properties) {
                var major = NumericUtils.ParseOrZero(MajorUpdater.Update(version.Major, properties));
                var minor = NumericUtils.ParseOrZero(MinorUpdater.Update(version.Minor, properties));
                var build = NumericUtils.ParseOrZero(BuildUpdater.Update(version.Build, properties));
                var revision = NumericUtils.ParseOrZero(RevisionUpdater.Update(version.Revision, properties));
                return new Version(major, minor, build, revision);
            }

            /// <summary>
            /// Saves the configuration in XML format.
            /// </summary>
            /// <param name="file">The file to save the configuration to.</param>
            public void Save(string file) {
                using (var configStream = new FileStream(file, FileMode.Create)) {
                    Save(configStream);
                }
            }

            /// <summary>
            /// Saves the configuration in XML format.
            /// </summary>
            /// <param name="stream">The stream to write the configuration to.</param>
            public void Save(Stream stream) {
                var xmlSerializer = new XmlSerializer(typeof(VersionUpdaterConfiguration));
                var emptyNamespace = new XmlSerializerNamespaces();
                emptyNamespace.Add(string.Empty, string.Empty);
                xmlSerializer.Serialize(stream, this, emptyNamespace);
            }

            /// <summary>
            /// Loads a configuration from a file.
            /// </summary>
            /// <param name="file">The file from which to load the configuration.</param>
            /// <returns>The loaded configuration.</returns>
            public static VersionUpdaterConfiguration Load(string file) {
                using (var configStream = new FileStream(file, FileMode.Open, FileAccess.Read)) {
                    return Load(configStream);
                }
            }

            /// <summary>
            /// Loads a configuration from a file.
            /// </summary>
            /// <param name="stream">The stream from which to load the configuration.</param>
            /// <returns>The loaded configuration.</returns>
            public static VersionUpdaterConfiguration Load(Stream stream) {
                var xmlSerializer = new XmlSerializer(typeof(VersionUpdaterConfiguration));
                return (VersionUpdaterConfiguration)xmlSerializer.Deserialize(stream);
            }

        #endregion
    }
}