namespace XWolfe.VersionUpdater {
    internal static class NumericUtils {
        /// <summary>
        /// Converts a string to a number.
        /// </summary>
        /// <param name="value">The string value to convert.</param>
        /// <returns>The numeric value of the string or zero.</returns>
        public static int ParseOrZero(string value) {
            if (string.IsNullOrEmpty(value)) return 0;
            int retVal;
            int.TryParse(value, out retVal);
            return retVal;
        }
    }
}