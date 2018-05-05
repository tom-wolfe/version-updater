using System.Xml.Serialization;

namespace XWolfe.VersionUpdater.Versioning.Updaters {
    /// <summary>
    /// Specifies an operator to use during a calculation.
    /// </summary>
    public enum OperatorType {
        /// <summary>
        /// Specifies the '+', addition operator.
        /// </summary>
        [XmlEnum("add")]
        Add,

        /// <summary>
        /// Specifies the '-', subtraction operator.
        /// </summary>
        [XmlEnum("subtract")]
        Subtract,

        /// <summary>
        /// Specifies the '*', multiplication operator.
        /// </summary>
        [XmlEnum("multiply")]
        Multiply,

        /// <summary>
        /// Specifies the '/', division operator.
        /// </summary>
        [XmlEnum("divide")]
        Divide
    }
}
