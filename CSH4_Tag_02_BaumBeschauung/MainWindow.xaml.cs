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

namespace CSH4_Tag_02_BaumBeschauung
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            List<DriveInfo> di = DriveInfo.GetDrives().ToList();
            di.ForEach(d =>
               tvBauemchen.Items.Add(Baumschule(d))
            );
        }

        TreeViewItem Baumschule(object obj)
        {
            TreeViewItem tvi = new TreeViewItem();
            tvi.Header = obj.ToString();
            tvi.Tag = obj;
            tvi.Items.Add(null);
            return tvi;
        }

        private void tvBauemchen_Expanded(object sender, RoutedEventArgs e)
        {
            TreeViewItem tvi = e.Source as TreeViewItem;
          
            try
            {
                tvi.Items.Clear();
                DirectoryInfo dirInfo = null;
                if (tvi.Tag is DriveInfo)
                    dirInfo = (tvi.Tag as DriveInfo).RootDirectory;
                if (tvi.Tag is DirectoryInfo)
                    dirInfo = tvi.Tag as DirectoryInfo;

                foreach (DirectoryInfo di in dirInfo.GetDirectories())
                {
                    tvi.Items.Add(Baumschule(di));
                }
            }
            catch { }
        }

        private void tvBauemchen_Selected(object sender, RoutedEventArgs e)
        {
            TreeViewItem tvi = e.Source as TreeViewItem;

            lvListe.Items.Clear();

            try
            {
                if (tvi.Tag is DriveInfo)
                {
                    DriveInfo di = tvi.Tag as DriveInfo;
                    FileInfo[] fi = di.RootDirectory.GetFiles();
                    foreach (FileInfo f in fi)
                    {
                        lvListe.Items.Add(f.Name);
                    }
                }
                if (tvi.Tag is DirectoryInfo)
                {
                    DirectoryInfo di = tvi.Tag as DirectoryInfo;
                    FileInfo[] fi = di.GetFiles();
                    foreach (FileInfo f in fi)
                    {
                        lvListe.Items.Add(f.Name);
                    }
                }
            }
            catch { }
        }
    }
}
