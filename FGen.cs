using System;
using System.Diagnostics;
using System.IO;

namespace fastd2
{
    class FGen
    {
       
        public static string GetReplayFileYep(string oldtext = "")
        {
            Microsoft.Win32.OpenFileDialog ofdo = new Microsoft.Win32.OpenFileDialog
            {
                Multiselect = false,
                Filter = "Osu replay file (*.osr)|*.osr"              
            };

            // If default osu location is not found, use "This PC" directory instead.
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "osu!");
            if (Directory.Exists(path)) { ofdo.InitialDirectory = path; }
            else { ofdo.InitialDirectory = "::{20D04FE0-3AEA-1069-A2D8-08002B30309D}"; }

            switch (ofdo.ShowDialog())
            {
                case true:
                    string sSelectedPath = ofdo.FileName;
                    return sSelectedPath;
                case false:
                    return oldtext;
                default:
                    return oldtext;
            }
        }

        public static string GetSettingFileYep(string oldtext = "")
        {
            Microsoft.Win32.OpenFileDialog ofdo = new Microsoft.Win32.OpenFileDialog
            {
                Multiselect = false,
                Filter = "Danser setting file (*.json)|*.json",               
            };

            #pragma warning disable CS8604 // I don't think this can NOT be null.
            string path = Path.GetFullPath(Path.Combine(Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName), @"../settings"));
            string path2 =  Path.GetFullPath(Path.Combine(Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName), @"settings"));
            #pragma warning restore CS8604 // Possible null reference argument.

            if (Directory.Exists(path)) { ofdo.InitialDirectory = path; }
            else if (Directory.Exists(path2)) { ofdo.InitialDirectory = path2; }
            else { ofdo.InitialDirectory = path; }
            

            switch (ofdo.ShowDialog())
            {
                case true:
                    string sSelectedPath = ofdo.FileName;
                    return sSelectedPath;
                case false:
                    return oldtext;
                default:
                    return oldtext;
            }
        }
    }
}
