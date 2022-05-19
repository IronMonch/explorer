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

namespace Dateien_Explorer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string aktuellerPfad;
        int itemsCount;

        public MainWindow()
        {
            InitializeComponent();
        }

        public class dateiOrdner
        {
            public string Typ { get; set; }

            public string Name { get; set; }
            
        }

        private void zeigeInhalt()
        {
            string pfad = pfadLeiste.Text;
            string[] dirs = Directory.GetDirectories(pfad);
            string[] dirs2 = Directory.GetFiles(pfad);

            //bei jedem Löschvorgang schrumpft das Array um ein Element, weshalb nach dem ersten Löschen error mit 
            //der Meldung "out of range" erscheint. Deshalb kein i++.
            //variante 1

            while (lb_DirectoryItems.Items.Count > 0)
            {
                lb_DirectoryItems.Items.Remove(lb_DirectoryItems.Items[0]);
            }

            //Variante2
            //for (int i = lb_DirectoryItems.Items.Count - 1; i > -1; i--)
            //{
            //    lb_DirectoryItems.Items.Remove(lb_DirectoryItems.Items[i]);
            //}

            //jedes gefundene Element im suchergebnis-array wird als Item in der listbox ausgegeben
            //lb_DirectoryItems.Items.Add("Ordner:");

            foreach (string dir in dirs)
            {
                string name = dir.Remove(0, dir.LastIndexOf('\\') + 1);
                string typ = "Ordner";

                lb_DirectoryItems.Items.Add(new dateiOrdner() {Typ = typ, Name = name});
                //lb_DirectoryItems.ItemsSource = items;


                //Image "c:\\users\\mohamed\\desktop\\praktikum\\dateien-explorer\\icons\\leerer-ordner.png"
                //lb_DirectoryItems.Items.Add(dir.Remove(0, dir.LastIndexOf('\\') + 1));
            }

            //lb_DirectoryItems.Items.Add("");
            //lb_DirectoryItems.Items.Add("Dateien:");
            foreach (string dir2 in dirs2)
            {
                string name = dir2.Remove(0, dir2.LastIndexOf('\\') + 1);
                string typ = "Datei";

                lb_DirectoryItems.Items.Add(new dateiOrdner() {Typ = typ, Name = name});
                //lb_DirectoryItems.ItemsSource = items;


                //lb_DirectoryItems.Items.Add(dir2.Remove(0, dir2.LastIndexOf('\\') + 1));
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

        void lbDirectoryItems_Doubleclick(object sender, EventArgs e)
        {
            //string selectedFile = lb_DirectoryItems.SelectedItem.ToString();
            //string itemToOpen = selectedFile.Remove(0, selectedFile.LastIndexOf("+") + 1);
            //pfadLeiste.Text = itemToOpen;
            dateiOrdner lll = (dateiOrdner)lb_DirectoryItems.Items[0];
            



            //FileAttributes attr = File.GetAttributes(@aktuellerPfad + itemToOpen);

            //if (attr.HasFlag(FileAttributes.Directory))
            //{
            //    pfadLeiste.Text = aktuellerPfad + itemToOpen + '\\';
            //    zeigeInhalt();
            //}

            //else
            //{
            //    try
            //    {
            //        using (Process myProcess = new Process())
            //        {
            //            myProcess.StartInfo.UseShellExecute = true;
            //            myProcess.StartInfo.FileName = aktuellerPfad + itemToOpen;
            //            myProcess.StartInfo.CreateNoWindow = true;
            //            myProcess.Start();
            //        }
            //    }
            //    catch
            //    {
            //        pfadLeiste.Text = itemToOpen;
            //    }

            //}
        }
    }
}
