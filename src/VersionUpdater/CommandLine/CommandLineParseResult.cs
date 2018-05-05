using System.Collections.Generic;

namespace XWolfe.VersionUpdater.CommandLine {
    /// <summary>
    /// Represents the result of parsing command-line arguments.
    /// </summary>
    public class CommandLineParseResult {
        #region -  Constructors  -

            /// <summary>
            /// Initializes a new instance of the <see cref="CommandLineParseResult"/> class.
            /// </summary>
            public CommandLineParseResult() {
                Arguments = new Dictionary<string, string>();
            }

        #endregion

        #region -  Properties  -

            /// <summary>
            /// Gets the result of the command-line argument parsing.
            /// </summary>
            public Dictionary<string, string> Arguments { get; private set; }

        #endregion
    }
}