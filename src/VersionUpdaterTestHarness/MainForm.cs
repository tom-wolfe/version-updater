using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Serialization;
using XWolfe.VersionUpdater.Versioning;

namespace VersionUpdaterTestHarness {
    public partial class MainForm : Form {
        #region -  Constructors  -

            public MainForm() {
                InitializeComponent();
            }

        #endregion

        #region -  Event Handlers  -

            private void btnClose_Click(object sender, EventArgs e) {
                Close();
            }

            private void btnUpdate_Click(object sender, EventArgs e) {
                var config = CreateVersionConfig(txtConfig.Text);

                var textVersion = string.Format("AssemblyVersion(\"{0}\")", txtVersion.Text);
                var applicator = new VersionApplicator(textVersion);

                var version = applicator.GetVersion(VersionType.Assembly);
                version = config.Update(version, CreateProperties(txtProperties.Text));
                applicator.SetVersions(VersionType.Assembly, version);
                txtVersion.Text = applicator.GetVersion(VersionType.Assembly).ToString();
            }

        #endregion

        #region -  Methods  -

            private static VersionUpdaterConfiguration CreateVersionConfig(string xml) {
                using(var stream = new StringReader(xml)) {
                    var xmlSerializer = new XmlSerializer(typeof(VersionUpdaterConfiguration));
                    return (VersionUpdaterConfiguration)xmlSerializer.Deserialize(stream);
                }
            }

            private static Dictionary<string, string> CreateProperties(string input) {
                var properties = new Dictionary<string, string>(StringComparer.CurrentCultureIgnoreCase);
                var kvps = from property in input.ToString(CultureInfo.InvariantCulture).Split(new [] { ";" }, StringSplitOptions.RemoveEmptyEntries)
                           let kvp = property.Split(new [] { "=" }, StringSplitOptions.RemoveEmptyEntries)
                           select new KeyValuePair<string, string>(kvp[0], kvp[1]);
                foreach (var kvp in kvps) {
                    properties.Add(kvp.Key, kvp.Value);
                }
                return properties;
            }

        #endregion
    }
}
