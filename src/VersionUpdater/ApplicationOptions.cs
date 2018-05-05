using System.Collections.Generic;
using System.Linq;
using XWolfe.VersionUpdater.CommandLine;

namespace XWolfe.VersionUpdater {
    /// <summary>
    /// Represents the options passed to the application.
    /// </summary>
    public class ApplicationOptions : CommandLineParseResult {
        #region -  Constructors  -

            /// <summary>
            /// Initializes a new instance of the <see cref="ApplicationOptions"/> class.
            /// </summary>
            public ApplicationOptions() { }

        #endregion

        #region -  Properties  -
            
            /// <summary>
            /// Gets a Boolean value indicating whether or not to display the application usage instructions.
            /// </summary>
            public bool ShowHelp {
                get { return Arguments.ContainsKey("help"); }
            }

            /// <summary>
            /// Gets the input file containing the version to update.
            /// </summary>
            public string InputFile {
                get {
                    return Arguments.ContainsKey("input") ? Arguments["input"] : string.Empty;
                }
            }

            /// <summary>
            /// Gets the configuration file that contains the new version number description.
            /// </summary>
            public string ConfigFile {
                get {
                    return Arguments.ContainsKey("config") ? Arguments["config"] : "version.config";
                }
            }

            /// <summary>
            /// Gets the type of version to update
            /// </summary>
            public string Update {
                get {
                    return Arguments.ContainsKey("update") ? Arguments["update"] : "both";
                }
            }

            /// <summary>
            /// Gets the semi-colon delimited key=value pairs passed to the application.
            /// </summary>
            public Dictionary<string, string> Properties {
                get { 
                    var props = new Dictionary<string, string>();
                    if (Arguments.ContainsKey("properties")) {
                        var pairs = from kvp in Arguments["properties"].Split(';')
                                    let pair = kvp.Split('=')
                                    where pair.Length == 2
                                    select pair;
                        foreach (var pair in pairs) {
                            props.Add(pair[0], pair[1]);
                        }
                    }
                    return props;
                }
            }

        #endregion
    }
}