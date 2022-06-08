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
//using Microsoft.Win32;
using System.Runtime;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;
using System.Collections;
using Image = System.Windows.Controls.Image;
using System.Collections.ObjectModel;
using MessageBox = System.Windows.MessageBox;
using ListViewItem = System.Windows.Controls.ListViewItem;

namespace Dateien_Explorer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string aktuellerPfad;


        public MainWindow()
        {
            InitializeComponent();
        }

        public class dateiOrdner
        {
            public object Typ { get; set; }

            public string Name { get; set; }
        }


        private void zeigeInhalt()
        {
            string pfad = pfadLeiste.Text;
            string[] dirs = Directory.GetDirectories(pfad);
            string[] dirs2 = Directory.GetFiles(pfad);

            //Angezeigte Suchergebnisse, wenn vorhanden, löschen.
            while (lb_DirectoryItems.Items.Count > 0)
            {
                lb_DirectoryItems.Items.Remove(lb_DirectoryItems.Items[0]);
            }

            foreach (string dir in dirs)
            {

                string name = dir.Remove(0, dir.LastIndexOf('\\') + 1);
                string typ = "Ordner";

                lb_DirectoryItems.Items.Add(new dateiOrdner() { Typ = typ , Name = name });

            }

            foreach (string dir2 in dirs2)
            {

                string name = dir2.Remove(0, dir2.LastIndexOf('\\') + 1);
                string typ = "Datei";

                lb_DirectoryItems.Items.Add(new dateiOrdner() { Typ = typ, Name = name });
            }
            aktuellerPfad = pfadLeiste.Text;
        }

        void öffnenBttn_Click(object sender, RoutedEventArgs e)
        {
            zeigeInhalt();
        }

        void ordner_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog öffneDialog = new OpenFileDialog();
            öffneDialog.ShowDialog();
        }

        void zurückBttn_Click(object sender, RoutedEventArgs e)
        {
            string pfad = pfadLeiste.Text.TrimEnd('\\');
            int letzterSlash = (pfad.LastIndexOf('\\')) + 1;

            if (letzterSlash >= 2)
            {
                pfadLeiste.Text = pfad.Remove(letzterSlash, pfad.Length - letzterSlash);
                zeigeInhalt();
            }

        }

        private void Öffnen()
        {
            dateiOrdner lll = (dateiOrdner)lb_DirectoryItems.SelectedItem;

            if (lll != null)
            {
                FileAttributes attr = File.GetAttributes(@aktuellerPfad + lll.Name);

                if (attr.HasFlag(FileAttributes.Directory))
                {
                    pfadLeiste.Text = aktuellerPfad + lll.Name + '\\';
                    zeigeInhalt();
                }

                else
                {
                    try
                    {
                        using (Process myProcess = new Process())
                        {
                            myProcess.StartInfo.UseShellExecute = true;
                            myProcess.StartInfo.FileName = aktuellerPfad + lll.Name;
                            myProcess.StartInfo.CreateNoWindow = true;
                            myProcess.Start();
                        }
                    }
                    catch
                    {
                        pfadLeiste.Text = lll.Name;
                    }
                }
            }
            else
            {
                pfadLeiste.Text = pfadLeiste.Text;
            }
        }

        void lbDirectoryItems_Open(object sender, RoutedEventArgs e)
        {
            Öffnen();
        }

        void lbDirectoryItems_Delete(object sender, RoutedEventArgs e)
        {
            dateiOrdner itemName = (dateiOrdner)lb_DirectoryItems.SelectedItem;

            MessageBoxResult result = MessageBox.Show("Willst du wirklich " + "\"" + itemName.Name + "\"" + " löschen?", "Achtung!", MessageBoxButton.YesNo);
            if (result != MessageBoxResult.Yes)
            {
                return;
            }

            string toDelete = aktuellerPfad + itemName.Name;
            FileAttributes auswahl = File.GetAttributes(toDelete);

            if (auswahl == FileAttributes.Directory)
            {
                Directory.Delete(toDelete);
            }
            else
            {
                File.Delete(toDelete);
            }
            zeigeInhalt();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Öffnen();
        }
    }
}
