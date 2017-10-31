using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using EnergyTray.Application.AppSettings;
using EnergyTray.Application.AppSettings.Consumer;
using EnergyTray.Application.Exceptions;
using EnergyTray.Application.Model;
using EnergyTray.Application.PowerManagement;
using EnergyTray.Worker;

namespace EnergyTray.UI
{
    public partial class SelectIconsForm : Form
    {
        private readonly IPowerProcessor _powerProcessor;
        private readonly IIconSettings _iconSettings;
        private readonly IWorkerSettings _workerSettings;
        private readonly IMonitorCheckWorker _monitorCheckWorker;

        public SelectIconsForm(
            IPowerProcessor powerProcessor, 
            IIconSettings iconSettings,
            IWorkerSettings workerSettings,
            IMonitorCheckWorker monitorCheckWorker)
        {
            _powerProcessor = powerProcessor;
            _iconSettings = iconSettings;
            _workerSettings = workerSettings;
            _monitorCheckWorker = monitorCheckWorker;
            InitializeComponent();
        }

        private void SelectIconsForm_Load(object sender, EventArgs e)
        {
            SetupComboBoxes();
            SetupCheckBoxes();
        }

        private void SetupComboBoxes()
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

        private void SetupCheckBoxes()
        {
            checkBox1.Checked = _monitorCheckWorker.AutoEnabled;
            panel1.Enabled = checkBox1.Checked;
            checkBoxMultipleDisplays.Checked = _workerSettings.IsMonitorConditionEnabled;
            checkBoxPluggedIn.Checked = _workerSettings.IsPowerConditionEnabled;
        }

        private PowerScheme GetSelectedPowerScheme()
        {
            var item = (ComboboxItem) comboBox1.SelectedItem;
            var selectedScheme = _powerProcessor.GetAllPowerSchemes().Single(i => i.Name == item.Text);
            return selectedScheme;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var openFileDialog = CreateOpenFileDialog();

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                var file = TryCopySelectedFile(openFileDialog);
                SetPictureBoxImage(file);
                _iconSettings.SaveIcon(GetSelectedPowerScheme().Id, file);
            }
        }

        private void SetPictureBoxImage(string file)
        {
            pictureBox1.Image =
                Bitmap.FromHicon(new Icon(file, new Size(pictureBox1.Width, pictureBox1.Height)).Handle);
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

        private string TryCopySelectedFile(OpenFileDialog openFileDialog)
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
                Close();
                throw new EnergyTrayException(ex);
            }
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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedScheme = GetSelectedPowerScheme();
            var file = _iconSettings.GetIconById(selectedScheme.Id);
            if (file != null)
                SetPictureBoxImage(file);
            else
                ResetPictureBoxImage();
        }

        private void ResetPictureBoxImage()
        {
            pictureBox1.Image = null;
            pictureBox1.InitialImage = null;
        }

        private void pictureBox1_Click(object sender, System.EventArgs e)
        {
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            var isChecked = checkBox1.Checked;
            _monitorCheckWorker.AutoEnabled = isChecked;
            panel1.Enabled = isChecked;
        }

        private void checkBoxMultipleDisplays_CheckedChanged(object sender, EventArgs e)
        {
            _workerSettings.IsMonitorConditionEnabled = checkBoxMultipleDisplays.Checked;
        }

        private void checkBoxPluggedIn_CheckedChanged(object sender, EventArgs e)
        {
            _workerSettings.IsPowerConditionEnabled = checkBoxPluggedIn.Checked;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
        }
    }
}