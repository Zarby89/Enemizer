using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EnemizerLibrary;
using Newtonsoft.Json;

namespace Patcher
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void patchButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog romDialog = new OpenFileDialog();
            OpenFileDialog patchDialog = new OpenFileDialog();
            SaveFileDialog saveDialog = new SaveFileDialog();
            romDialog.Filter = "*.sfc|*.sfc|*.*|*.*";
            patchDialog.Filter = "*.patch|*.patch|*.*|*.*";
            saveDialog.Filter = "*.sfc|*.sfc|*.*|*.*";

            if(romDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            if (patchDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            var romFilename = romDialog.FileName;
            var patchFilename = patchDialog.FileName;
            saveDialog.FileName = Path.GetFileNameWithoutExtension(patchFilename) + ".sfc";

            var bytes = File.ReadAllBytes(romFilename);
            Array.Resize(ref bytes, 4 * 1024 * 1024);
            var patches = JsonConvert.DeserializeObject<List<PatchObject>>(File.ReadAllText(patchFilename));

            foreach (var patch in patches)
            {
                var patchDataArray = patch.patchData.ToArray();
                Array.Copy(patchDataArray, 0, bytes, patch.address, patchDataArray.Length);
            }

            if (saveDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            File.WriteAllBytes(saveDialog.FileName, bytes);
        }
    }
}
