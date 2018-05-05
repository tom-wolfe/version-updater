using System;
using System.Reflection;

namespace XWolfe.VersionUpdater.Versioning {
    /// <summary>
    /// Represents an assembly version attribute type.
    /// </summary>
    [Flags]
    public enum VersionType {
        /// <summary>
        /// Specifies the version contained within the <see cref="AssemblyVersionAttribute"/>.
        /// </summary>
        Assembly = 1,

        /// <summary>
        /// Specifies the version contained within the <see cref="AssemblyFileVersionAttribute"/>.
        /// </summary>
        File = 2
    }
}