using System;
using System.Collections.Generic;
using System.IO;
using XWolfe.VersionUpdater.CommandLine;
using XWolfe.VersionUpdater.Versioning;

namespace XWolfe.VersionUpdater {
    /// <summary>
    /// Houses the entry point for the application.
    /// </summary>
    static class Program {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// <param name="args">The arguments passed to the application.</param>
        static void Main(string[] args) {
            var definition = new ApplicationCommandLineDefinition();
            var parser = new CommandLineParser();
            var options = new ApplicationOptions();
            try {
                parser.Parse(definition, args, options);

                if (options.ShowHelp) {
                    Console.WriteLine(parser.GenerateHelpText(definition, "VersionUpdater", string.Empty));
                    Console.ReadLine();
                    return;
                }

                var config = VersionUpdaterConfiguration.Load(options.ConfigFile);
                var applicator = new VersionApplicator(File.ReadAllText(options.InputFile));

                var properties = options.Properties;

                // Update the appropriate version numbers.
                if (options.Update == "both" || options.Update == "assembly") {
                    UpdateVersion(applicator, config, properties, VersionType.Assembly);
                }

                if (options.Update == "both" || options.Update == "file") {
                    UpdateVersion(applicator, config, properties, VersionType.File);
                }

                // Write the changes back.
                File.WriteAllText(options.InputFile, applicator.Source);
                Console.WriteLine("Versions written successfully.");
            } catch (Exception ex) {
                if (!options.ShowHelp) {
                    LogError("Unable to update version options:", ex);    
                }
                Console.WriteLine(parser.GenerateHelpText(definition, "VersionUpdater", string.Empty));
                Console.ReadLine();
            }
        }

        static void UpdateVersion(VersionApplicator applicator, VersionUpdaterConfiguration config, Dictionary<string, string> properties, VersionType type) {
            var oldVersion = applicator.GetVersion(type);
            var newVersion = config.Update(oldVersion, properties);
            applicator.SetVersions(type, newVersion);
            Console.WriteLine("{2} version updated from {0} to {1}.", oldVersion, newVersion, type);
        }

        static void LogError(string message, Exception ex) {
            Console.WriteLine(message);
            Console.WriteLine(ex.Message);
            if (ex.InnerException != null) {
                Console.WriteLine(ex.InnerException.Message);
            }
        }
    }
}