using System.Collections.Generic;
using System.Xml.Serialization;
using System;

namespace XWolfe.VersionUpdater.Versioning.Updaters {
    /// <summary>
    /// Divides the number by a specified value.
    /// </summary>
    [XmlRoot("if")]
    public class ConditionalVersionUpdater : MultiPartVersionUpdater {
        #region -  Constructors  -

            /// <summary>
            /// Initializes the comparison->delegate mappings.
            /// </summary>
            static ConditionalVersionUpdater() {
                Comparisons = new Dictionary<ComparisonType, Comparison<string>> {
                    {ComparisonType.Equal, ComparisonType_Equals},
                    {ComparisonType.EndsWith, ComparisonType_EndsWith},
                    {ComparisonType.StartsWith, ComparisonType_StartsWith},
                    {ComparisonType.Contains, ComparisonType_Contains}
                };
            }

            /// <summary>
            /// Initializes a new instance of the <see cref="ConditionalVersionUpdater"/> class.
            /// </summary>
            public ConditionalVersionUpdater() : this(string.Empty, ComparisonType.Equal, string.Empty) { }

            /// <summary>
            /// Initializes a new instance of the <see cref="ConditionalVersionUpdater"/> class.
            /// </summary>
            /// <param name="property">The name of the property whose value will be compared.</param>
            /// <param name="comparison">The comparison method to use when comparing the property and value.</param>
            /// <param name="value">The value that the property will be compared to.</param>
            public ConditionalVersionUpdater(string property, ComparisonType comparison, string value) {
                Property = property;
                Comparison = comparison;
                Value = value;
            }

        #endregion

        #region -  Properties  -

            /// <summary>
            /// Gets the value that the property will be compared to.
            /// </summary>
            [XmlAttribute("value")]
            public string Value { get; set; }

            /// <summary>
            /// Gets or sets the name of the property whose value will be compared.
            /// </summary>
            [XmlAttribute("property")]
            public string Property { get; set; }

            /// <summary>
            /// Gets or sets the comparison method to use when comparing the property and value.
            /// </summary>
            [XmlAttribute("compare")]
            public ComparisonType Comparison { get; set; }

            /// <summary>
            /// Gets or sets a Boolean value indicating whether to reverse the behaviour of the condition.
            /// (The equivalent of using a 'not' operator).
            /// </summary>
            [XmlAttribute("unless")]
            public bool Unless { get; set; }

            /// <summary>
            /// The dictionary that contains comparison mappings for the <see cref="ComparisonType"/> enumeration.
            /// </summary>
            [XmlIgnore]
            private static readonly Dictionary<ComparisonType, Comparison<string>> Comparisons;

        #endregion

        #region -  Methods  -

            /// <summary>
            /// Updates the given version number.
            /// </summary>
            /// <param name="versionPart">The version number to update.</param>
            /// /// <param name="properties">The properties passed to the updater.</param>
            /// <returns>The new version number.</returns>
            public override string Update(int versionPart, Dictionary<string, string> properties) {
                if (!properties.ContainsKey(Property)) return string.Empty;
                var retVal = string.Empty;

                var val1 = properties[Property];
                var val2 = Value;

                if (Comparisons.ContainsKey(Comparison)) {
                    if ((Comparisons[Comparison].Invoke(val1, val2) == 0) ^ Unless) {
                        retVal = base.Update(versionPart, properties);    
                    }
                } else if (String.Equals(val1, val2, StringComparison.Ordinal)) {
                    retVal = base.Update(versionPart, properties);
                }
                return retVal;
            }

        #endregion

        #region -  Comaparison Methods  -

            private delegate bool CompareVersions(string value1, string value2);

            private static int ComparisonType_Equals(string value1, string value2) {
                return String.Compare(value1, value2, StringComparison.OrdinalIgnoreCase);
            }

            private static int ComparisonType_EndsWith(string value1, string value2) {
                return value1.ToLower().EndsWith(value2.ToLower()) ? 0 : -1;
            }

            private static int ComparisonType_StartsWith(string value1, string value2) {
                return value1.ToLower().StartsWith(value2.ToLower()) ? 0 : -1;
            }

            private static int ComparisonType_Contains(string value1, string value2) {
                return value1.ToLower().Contains(value2.ToLower()) ? 0 : -1;
            }

        #endregion
    }
}