using EnemizerLibrary;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RomViewer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public class VM
        {
            public SpriteGroupCollection spriteGroupCollection { get; set; }
            public RoomCollection roomCollection { get; set; }
        }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void loadRomButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if(ofd.ShowDialog() == true)
            {
                RomData romData = LoadRom(ofd.FileName);
                var spriteRequirements = new SpriteRequirementCollection();

                SpriteGroupCollection sgc = new SpriteGroupCollection(romData, new Random(), spriteRequirements);
                sgc.LoadSpriteGroups();

                RoomCollection rc = new RoomCollection(romData, new Random(), spriteRequirements);
                rc.LoadRooms();

                var vm = new VM();
                vm.spriteGroupCollection = sgc;
                vm.roomCollection = rc;

                this.DataContext = vm;
            }
        }

        public static RomData LoadRom(string filename)
        {
            FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
            byte[] rom_data = new byte[fs.Length];
            fs.Read(rom_data, 0, (int)fs.Length);
            fs.Close();

            RomData romData = new RomData(rom_data);

            return romData;
        }

    }
}
