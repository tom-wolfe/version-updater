using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Xml.Serialization;

namespace XWolfe.VersionUpdater.Versioning.Updaters {
    /// <summary>
    /// Allows the building of compound version numbers.
    /// </summary>
    [XmlRoot("multi-part")]
    public class MultiPartVersionUpdater : VersionPartUpdater {
        #region -  Constructors  -

            /// <summary>
            /// Initializes a new instance of the <see cref="MultiPartVersionUpdater"/> class.
            /// </summary>
            public MultiPartVersionUpdater() {
                Updaters = new List<VersionPartUpdater>();
                Mode = MultiPartMode.Concatenate;
            }

        #endregion

        #region -  Properties  -

            /// <summary>
            /// Gets or sets the collection of updaters that will comprise this version part.
            /// </summary>
            [XmlElement("build-type", typeof(BuildTypeVersionUpdater))]
            [XmlElement("fixed", typeof(FixedVersionUpdater))]
            [XmlElement("if", typeof(ConditionalVersionUpdater))]
            [XmlElement("increment", typeof(IncrementVersionUpdater))]
            [XmlElement("multi-part", typeof(MultiPartVersionUpdater))]
            [XmlElement("no-change", typeof(NoChangeVersionUpdater))]
            [XmlElement("property", typeof(PropertyVersionUpdater))]
            [XmlElement("substring", typeof(SubstringTypeVersionUpdater))]
            [XmlElement("sum", typeof(SumVersionUpdater))]
            [XmlElement("time-since", typeof(TimeSinceVersionUpdater))]
            public List<VersionPartUpdater> Updaters { get; set; }

            /// <summary>
            /// Gets or sets the operation mode of the updater.
            /// </summary>
            [XmlAttribute("mode")]
            public MultiPartMode Mode { get; set; }

        #endregion

        #region -  Methods  -

            /// <summary>
            /// Updates the given version number.
            /// </summary>
            /// <param name="versionPart">The version number to update.</param>
            /// /// <param name="properties">The properties passed to the updater.</param>
            /// <returns>The new version number.</returns>
            public override string Update(int versionPart, Dictionary<string, string> properties) {
                if (Updaters.Count == 0) { return versionPart.ToString(CultureInfo.InvariantCulture); }
                string output;
                switch (Mode) {
                    case MultiPartMode.Concatenate:
                        output = Updaters.Aggregate(string.Empty, (current, updater) => current + updater.Update(versionPart, properties));
                        break;
                    case MultiPartMode.Sum:
                        output = Updaters.Aggregate(0, (current, updater) => current + int.Parse(updater.Update(versionPart, properties))).ToString(CultureInfo.InvariantCulture);
                        break;
                    default:
                        output = Updaters.Aggregate(string.Empty, (current, updater) => current + updater.Update(versionPart, properties));
                        break;
                }

                return output;
            }

        #endregion
    }
}