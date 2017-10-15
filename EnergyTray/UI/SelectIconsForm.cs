using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using EnergyTray.Application.PowerManagement;

namespace EnergyTray.UI
{
    public partial class SelectIconsForm : Form
    {
        private readonly IPowerProcessor _powerProcessor;

        public SelectIconsForm(IPowerProcessor powerProcessor)
        {
            _powerProcessor = powerProcessor;
            InitializeComponent();
        }

        private void SelectIconsForm_Load(object sender, EventArgs e)
        {
            var powerschemes = _powerProcessor.GetAllPowerSchemes().ToList();
            var activeScheme = powerschemes.Single(j => j.IsActive);

            comboBox1.Items.AddRange(powerschemes.Select(i => (object) new ComboboxItem
            {
                Text = i.Name,
                Value = i.Name
            }).ToArray());

            comboBox1.SelectedIndex = comboBox1.FindStringExact(activeScheme.Name);
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            var openFileDialog = CreateOpenFileDialog();

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                var file = TryCopySelectedFile(openFileDialog);
                pictureBox1.Image = Bitmap.FromHicon(new Icon(file, new Size(pictureBox1.Width, pictureBox1.Height)).Handle);
            }
        }

        private static OpenFileDialog CreateOpenFileDialog()
        {
            var openFileDialog = new OpenFileDialog
            {
                InitialDirectory = "c:\\",
                Filter = "ico files (*.ico)|*.ico",
                FilterIndex = 2,
                RestoreDirectory = true
            };
            return openFileDialog;
        }

        private static string TryCopySelectedFile(OpenFileDialog openFileDialog)
        {
            try
            {
                using (var sourceStream = openFileDialog.OpenFile())
                {
                    return CopyFile(openFileDialog.SafeFileName, sourceStream);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
            }
            return null;
        }

        private static string CopyFile(string fileName, Stream sourceStream)
        {
            var fileLocation = System.Windows.Forms.Application.ExecutablePath.Replace("EnergyTray.exe", "") + fileName;
            using (var destinationStream = new FileStream(fileLocation, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                sourceStream.CopyTo(destinationStream);
                sourceStream.Flush();
                return fileLocation;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, System.EventArgs e)
        {
        }

        private void pictureBox1_Click(object sender, System.EventArgs e)
        {
        }
    }
}