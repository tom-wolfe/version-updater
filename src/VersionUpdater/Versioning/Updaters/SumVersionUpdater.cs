using System.Collections.Generic;
using System.Globalization;
using System.Xml.Serialization;
using System;

namespace XWolfe.VersionUpdater.Versioning.Updaters {
    /// <summary>
    /// Performs a calculation between two numbers.
    /// </summary>
    [XmlRoot("sum")]
    public class SumVersionUpdater : VersionPartUpdater {
        #region -  Constructors  -

            /// <summary>
            /// Initializes a new instance of the <see cref="SumVersionUpdater"/> class.
            /// </summary>
            static SumVersionUpdater() {
                Operators = new Dictionary<OperatorType, Func<int, int, int>> {
                    {OperatorType.Add, OperatorType_Add},
                    {OperatorType.Divide, OperatorType_Divide},
                    {OperatorType.Multiply, OperatorType_Multiply},
                    {OperatorType.Subtract, OperatorType_Subtract}
                };
            }

        #endregion

        #region -  Properties  -

            /// <summary>
            /// Gets or sets the left-hand-side of the sum.
            /// </summary>
            [XmlElement("left")]
            public MultiPartVersionUpdater Left { get; set; }

            /// <summary>
            /// Gets or sets the operator to use when performing the calculation.
            /// </summary>
            [XmlAttribute("operator")]
            public OperatorType Operator { get; set; }

            /// <summary>
            /// Gets or sets the right-hand-side of the sum.
            /// </summary>
            [XmlElement("right")]
            public MultiPartVersionUpdater Right { get; set; }

            /// <summary>
            /// The dictionary that contains operator mappings for the <see cref="OperatorType"/> enumeration.
            /// </summary>
            [XmlIgnore]
            private static readonly Dictionary<OperatorType, Func<int, int, int>> Operators;

        #endregion

        #region -  Methods  -

            /// <summary>
            /// Updates the given version number.
            /// </summary>
            /// <param name="versionPart">The version number to update.</param>
            /// /// <param name="properties">The properties passed to the updater.</param>
            /// <returns>The new version number.</returns>
            public override string Update(int versionPart, Dictionary<string, string> properties) {
                var left = NumericUtils.ParseOrZero((Left ?? new MultiPartVersionUpdater()).Update(versionPart, properties));
                var right = NumericUtils.ParseOrZero((Right ?? new MultiPartVersionUpdater()).Update(versionPart, properties));
                if (!Operators.ContainsKey(Operator)) return (left + right).ToString(CultureInfo.InvariantCulture);
                return Operators[Operator].Invoke(left, right).ToString(CultureInfo.InvariantCulture);
            }

        #endregion

        #region -  Operator Methods  -

            private static int OperatorType_Add(int value1, int value2) {
                return value1 + value2;
            }

            private static int OperatorType_Subtract(int value1, int value2) {
                return value1 - value2;
            }

            private static int OperatorType_Multiply(int value1, int value2) {
                return value1 * value2;
            }

            private static int OperatorType_Divide(int value1, int value2) {
                if (value2 == 0) { return 0; }
                return value1 / value2;
            }

        #endregion
    }
}