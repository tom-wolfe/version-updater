using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace XWolfe.VersionUpdater.CommandLine {
    /// <summary>
    /// Represents a collection of command-line arguments that define a valid command-line argument string passed to an application.
    /// </summary>
    public class CommandLineDefinition : Collection<CommandLineArgument> {
        #region -  Properties  -

            /// <summary>
            /// Gets the argument with the given name.
            /// </summary>
            /// <param name="longName">The name of the argument to find.</param>
            /// <returns>The command-line argument with the matching long name if one is found; otherwise, null.</returns>
            public CommandLineArgument this[string longName] {
                get {
                    return (
                        from arg in this
                        where arg.LongName.Equals(longName, StringComparison.InvariantCultureIgnoreCase)
                        select arg
                    ).FirstOrDefault();
                }
            }

        #endregion

        #region -  Methods  -

            /// <summary>
            /// Finds a command-line argument by its long or short name.
            /// </summary>
            /// <param name="longOrShortName">The long or short name of the argument to search for.</param>
            /// <returns>The command-line argument with the matching long or short name if one is found; otherwise, null.</returns>
            public CommandLineArgument FindArgument(string longOrShortName) {
                return (
                    from arg in this
                    where
                        arg.LongName.Equals(longOrShortName, StringComparison.InvariantCultureIgnoreCase)
                        || arg.ShortName.Equals(longOrShortName, StringComparison.InvariantCultureIgnoreCase)
                    select arg
                ).FirstOrDefault();
            }

            /// <summary>
            /// Inserts an element into the <see cref="T:System.Collections.ObjectModel.Collection`1"/> at the specified index.
            /// </summary>
            /// <param name="index">The zero-based index at which <paramref name="item"/> should be inserted.</param><param name="item">The object to insert. The value can be null for reference types.</param><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="index"/> is less than zero.-or-<paramref name="index"/> is greater than <see cref="P:System.Collections.ObjectModel.Collection`1.Count"/>.</exception>
            protected override void InsertItem(int index, CommandLineArgument item) {
                if (FindArgument(item.ShortName) != null || FindArgument(item.LongName) != null) {
                    throw new DuplicateCommandLineArgumentException(item);
                }
                base.InsertItem(index, item);
            }

            /// <summary>
            /// Replaces the element at the specified index.
            /// </summary>
            /// <param name="index">The zero-based index of the element to replace.</param><param name="item">The new value for the element at the specified index. The value can be null for reference types.</param><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="index"/> is less than zero.-or-<paramref name="index"/> is greater than <see cref="P:System.Collections.ObjectModel.Collection`1.Count"/>.</exception>
            protected override void SetItem(int index, CommandLineArgument item) {
                var dupItem = FindArgument(item.ShortName) ?? FindArgument(item.LongName);
                if (dupItem != null && IndexOf(dupItem) == index) {
                    throw new DuplicateCommandLineArgumentException(item);
                }
                base.SetItem(index, item);
            }

        #endregion
    }
}