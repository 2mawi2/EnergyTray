using System;
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

        private void SelectIconsForm_Load(object sender, System.EventArgs e)
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
                TryReadSelectedFile(openFileDialog);
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

        private static void TryReadSelectedFile(OpenFileDialog openFileDialog)
        {
            Stream fileStream;
            try
            {
                if ((fileStream = openFileDialog.OpenFile()) != null)
                {
                    using (fileStream)
                    {
                        //TODO read icon file and persist somewhere
                        //TODO show icon file on pictureBox
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, System.EventArgs e)
        {
        }

        private void pictureBox1_Click(object sender, System.EventArgs e)
        {
        }
    }

    public class ComboboxItem
    {
        public string Text { get; set; }
        public object Value { get; set; }

        public override string ToString()
        {
            return Text;
        }
    }
}