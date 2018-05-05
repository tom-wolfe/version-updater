using System.Collections.Generic;

namespace XWolfe.VersionUpdater.Versioning.Updaters {
    /// <summary>
    /// Specifies the contract for updating one part of a version number.
    /// </summary>
    public abstract class VersionPartUpdater {
        /// <summary>
        /// Updates the given version number.
        /// </summary>
        /// <param name="versionPart">The version number to update.</param>
        /// /// <param name="properties">The properties passed to the updater.</param>
        /// <returns>The new version number.</returns>
        public abstract string Update(int versionPart, Dictionary<string, string> properties);
    }
}