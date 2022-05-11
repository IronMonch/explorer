﻿using System;
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


        private void zeigeInhalt()
        {
            string pfad = pfadLeiste.Text;
            string[] dirs = Directory.GetDirectories(pfad);
            string[] dirs2 = Directory.GetFiles(pfad);

            //bei jedem Löschvorgang schrumpft das Array um ein Element, weshalb nach dem ersten Löschen error mit 
            //der Meldung "out of range" erscheint
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

                lb_DirectoryItems.Items.Add(dir.Remove(0, dir.LastIndexOf('\\') + 1));
            }
            lb_DirectoryItems.Items.Add("");
            lb_DirectoryItems.Items.Add("Dateien:");
            foreach (string dir2 in dirs2)
            {
                lb_DirectoryItems.Items.Add(dir2.Remove(0, dir2.LastIndexOf('\\') + 1));
            }

            items = lb_DirectoryItems.Items.Count;
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
            int letzterSlash = pfad.LastIndexOf('\\');

            if (letzterSlash >= 4)
            {
                pfadLeiste.Text = pfad.Remove(letzterSlash, pfad.Length - letzterSlash);
                zeigeInhalt();
            }


            //string pfad = pfadLeiste.Text;
            //durchlaufe string rüchwärts;
            //solange (zeichen in pfad != "\")
            //    { 
            //        lösche zeichen;
            //    }
            //TextBox.Text = pfad;

            //rufe methode öffneBttn auf
            // 
            //hole Ordner und Dateien aus pfad und gebe sie aus als arrayDateien und arrayOrdner;
            //für (jedes Elemnt in arrayDateien/arrayOrdner)
            //{
            //    füge in listbox item = Element;        
            //}
            //string pfad = pfadLeiste.Text;
            //int stringLänge;
            //int löschPunkt;
            //if (pfad.Substring(pfad.Length - 1) == "\\")
            //{
            //char trennzeichen = '\\';
            //pfad = pfad.TrimEnd('\\');
            //pfadLeiste.Text = pfad;

            //int stringLänge = pfad.Length;

            //int löschPunkt = pfad.LastIndexOf(trennzeichen);
            //pfadLeiste.Text = pfad.Remove(löschPunkt, stringLänge - löschPunkt);
            //}
            //else
            //{
            //    pfadLeiste.Text = pfad.Remove(löschPunkt, stringLänge - löschPunkt);
            //    zeigeInhalt();
            //}
        }

        private void lbDirectoryItems_Doubleclick(object sender, EventArgs e)
        {
            string pfad = pfadLeiste.Text;
            string[] dirs = Directory.GetDirectories(pfadLeiste.Text);
            string[] dirs2 = Directory.GetFiles(pfadLeiste.Text);
            string itemToOpen = lb_DirectoryItems.SelectedItem.ToString();
            //öffne Ordner
            //if (Array.Exists(dirs, element => element == pfad + '\\' + itemToOpen))
            //{
            //    pfadLeiste.Text = pfad + '\\' + itemToOpen;
            //    zeigeInhalt();
            //}
            ////öffne Datei
            //if (Array.Exists(dirs2, element => element == pfad + '\\' + itemToOpen))
            //{
            //    try
            //    {
            //        using (Process myProcess = new Process())
            //        {
            //            myProcess.StartInfo.UseShellExecute = true;
            //            myProcess.StartInfo.FileName = pfad + '\\' + itemToOpen;
            //            myProcess.StartInfo.CreateNoWindow = true;
            //            myProcess.Start();
            //        }
            //    }
            //    catch
            //    {
            //        pfadLeiste.Text = itemToOpen;
            //    }
            //}

            FileAttributes attr = File.GetAttributes(@pfad + '\\' + itemToOpen);

            if (attr.HasFlag(FileAttributes.Directory))
            {
                pfadLeiste.Text = pfad + '\\' + itemToOpen;
                zeigeInhalt();
            }

            else
            {
                try
                {
                    using (Process myProcess = new Process())
                    {
                        myProcess.StartInfo.UseShellExecute = true;
                        myProcess.StartInfo.FileName = pfad + '\\' + itemToOpen;
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
