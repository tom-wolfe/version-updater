using XWolfe.VersionUpdater.CommandLine;

namespace XWolfe.VersionUpdater {
    /// <summary>
    /// Describes the command-line argument definition for the application.
    /// </summary>
    internal class ApplicationCommandLineDefinition : CommandLineDefinition {
        #region -  Constructors  -

            /// <summary>
            /// Initializes a new instance of the <see cref="ApplicationCommandLineDefinition" /> class.
            /// </summary>
            public ApplicationCommandLineDefinition() {
                Add(new CommandLineArgument("?", "help", false, false, "Print the usage instructions for the application."));
                Add(new CommandLineArgument("i", "input", true, true, "The file containing the version attribute(s)."));
                Add(new CommandLineArgument("u", "update", false, true, "The versions to update. Values: (assembly|file|both)."));
                Add(new CommandLineArgument("c", "config", false, true, "The version structure config file."));
                Add(new CommandLineArgument("p", "properties", false, true, "The key=value pairs for the config file."));
            }

        #endregion
    }
}