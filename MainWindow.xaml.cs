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
using System.Windows.Forms;
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


        private void zeigeInhalt()
        {
            string pfad = pfadLeiste.Text;
            string[] dirs = Directory.GetDirectories(pfad);
            string[] dirs2 = Directory.GetFiles(pfad);

            //bei jedem Löschvorgang schrumpft das Array um ein Element, weshalb nach dem ersten Löschen error mit 
            //der Meldung "out of range" erscheint. Deshalb kein i++.
            //variante 1
            int items = lb_DirectoryItems.Items.Count;
            for (int i = 0; i < lb_DirectoryItems.Items.Count;)
            {
                lb_DirectoryItems.Items.Remove(lb_DirectoryItems.Items[i]);
                
            }

            //Variante2
            //for (int i = lb_DirectoryItems.Items.Count - 1; i > -1 ; i--)
            //{
            //    lb_DirectoryItems.Items.Remove(lb_DirectoryItems.Items[i]);
            //}

            //jedes gefundene Element im suchergebnis-array wird als Item in der listbox ausgegeben
            lb_DirectoryItems.Items.Add("Ordner:");
            foreach (string dir in dirs)
            {
                //Image icon = new Image();
                //BitmapImage bi = new BitmapImage();
                //bi.BeginInit();
                //bi.UriSource = new Uri(@"c:\\users\\mohamed\\desktop\\praktikum\\dateien-explorer\\icons\\leerer-ordner.png", UriKind.RelativeOrAbsolute);
                //bi.EndInit();
                //icon.Source = bi;
                //icon.Width = 15;

                string ordnerName = dir.Remove(0, dir.LastIndexOf('\\') + 1);
                //Image "c:\\users\\mohamed\\desktop\\praktikum\\dateien-explorer\\icons\\leerer-ordner.png"
                lb_DirectoryItems.Items.Add(dir.Remove(0, dir.LastIndexOf('\\') + 1));
                //lb_DirectoryItems.Items.Add(new {icon, ordnerName});
            }

            lb_DirectoryItems.Items.Add("");
            lb_DirectoryItems.Items.Add("Dateien:");
            foreach (string dir2 in dirs2)
            {
                lb_DirectoryItems.Items.Add(dir2.Remove(0, dir2.LastIndexOf('\\') + 1));
            }

            items = lb_DirectoryItems.Items.Count;
            aktuellerPfad = pfadLeiste.Text;
        }

        private void öffnenBttn_Click(object sender, RoutedEventArgs e)
        {
            zeigeInhalt();
        }


        private void ordner_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog öffneDialog = new OpenFileDialog();
            öffneDialog.ShowDialog();
        }

        private void zurückBttn_Click(object sender, RoutedEventArgs e)
        {
            string pfad = pfadLeiste.Text.TrimEnd('\\');
            int letzterSlash = (pfad.LastIndexOf('\\')) + 1;

            if (letzterSlash >= 2)
            {
                pfadLeiste.Text = pfad.Remove(letzterSlash, pfad.Length - letzterSlash);
                zeigeInhalt();
            }

        }

        private void lbDirectoryItems_Doubleclick(object sender, EventArgs e)
        {
            string itemToOpen = lb_DirectoryItems.SelectedItem.ToString();

            FileAttributes attr = File.GetAttributes(@aktuellerPfad + itemToOpen);

                if (attr.HasFlag(FileAttributes.Directory))
                {
                    pfadLeiste.Text = aktuellerPfad + itemToOpen +'\\';
                    zeigeInhalt();
                }

                else
                {
                    try
                    {
                        using (Process myProcess = new Process())
                        {
                            myProcess.StartInfo.UseShellExecute = true;
                            myProcess.StartInfo.FileName = aktuellerPfad + itemToOpen;
                            myProcess.StartInfo.CreateNoWindow = true;
                            myProcess.Start();
                        }
                    }
                    catch
                    {
                        pfadLeiste.Text = itemToOpen;
                    }

                }
        }    
    }
}
