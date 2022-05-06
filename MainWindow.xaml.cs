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
using Microsoft.Win32;
using System.Runtime;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;
using System.Collections;

namespace Dateien_Explorer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void öffnenBttn_Click(object sender, RoutedEventArgs e)
        {
            string pfad = pfadLeiste.Text;
            string[] dirs = Directory.GetDirectories(pfad);
            string[] dirs2 = Directory.GetFiles(pfad);

            //bestehende suchergebnisse löschen um Wiederholungen zu vermeiden

            lb_DirectoryItems.Items.Clear();
            //for (int i = 0; i < dirs.Length; i++)
            //{
            //    lb_DirectoryItems.Items.Remove(dirs[i]);
            //}
            //for (int i = 0; i < dirs2.Length; i++)
            //{
            //    lb_DirectoryItems.Items.Remove(dirs2[i]);
            //}
            //lb_DirectoryItems.Items.Remove("Ordner:");
            //lb_DirectoryItems.Items.Remove("Dateien:");
            //lb_DirectoryItems.Items.Remove("");


            //jedes gefundene Element im suchergebnis-array wird als Item in der listbox ausgegeben
            lb_DirectoryItems.Items.Add("Ordner:");
            foreach (string dir in dirs)
            {
                lb_DirectoryItems.Items.Add(dir);
            }
            lb_DirectoryItems.Items.Add("");
            lb_DirectoryItems.Items.Add("Dateien:");
            foreach (string dir2 in dirs2)
            {
                lb_DirectoryItems.Items.Add(dir2);
            }
        }
    }
}
