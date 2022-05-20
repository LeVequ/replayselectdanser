using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace ReplaySelectorDanser
{
    internal class Bkend
    {
        public static string mreplaypath = "";
        public static string msettingpath = "";
        public static string replayn = "";
        public static string settingn = "";
        public static bool mrpvalid = false;
        public static bool mspvalid = false;

        public static void StartRender()
        {
            Process danser = new Process();
            ProcessStartInfo sargs = new ProcessStartInfo
            {
                FileName = "danser.exe",
                Arguments = " -md5=727"
            };
            
            danser.StartInfo = sargs;
            try
            {
                danser.Start();
            }
            catch (Win32Exception)
            {
                MessageBox.Show("danser.exe not found in the same directory as the program!", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to launch danser.\nError: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public static string ArgsMaker(MainWindow window)
        {
            string args = " -record";
            args += $" -r=\"{mreplaypath}\"";
            if (window.skipcb.IsChecked == true) { args += " -skip"; }
            if (mspvalid) { args += $" -settings={RemoveJSTrailPath()}"; }
            if (window.nodbcb.IsChecked == true) { args += " -nodbcheck"; }
            return args;
        }

        public static void TestArgs(MainWindow window)
        {
            Trace.WriteLine(ArgsMaker(window));
        }

        public static void StartRender(string args)
        {
            Process danser = new Process();
            ProcessStartInfo sargs = new ProcessStartInfo
            {
                
                Arguments = args
            };
            if (File.Exists("danser.exe")) { sargs.FileName = "danser.exe";  }
            else if (File.Exists(@"..\danser.exe")) { sargs.FileName = @"..\danser.exe"; }
            else { sargs.FileName = @"..\danser.exe"; }

            danser.StartInfo = sargs;
            try
            {
                danser.Start();
            }
            catch (Win32Exception)
            {
                MessageBox.Show("danser.exe not found!\nPlace this program on its own folder inside the danser directory.\n(It's also possible to use directly in danser directory.)", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to launch danser.\nError: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public static string RemoveJSTrailPath()
        {
            string bruh = System.IO.Path.GetFileNameWithoutExtension(msettingpath);
            return bruh;
        }
        public static string RemoveJSTrailName()
        {
            string bruh = settingn.Remove(settingn.Length - 4);
            return bruh;
        }

        public static void UpdateInfoTB(TextBlock infotb)
        {
            if ((String.IsNullOrWhiteSpace(Bkend.mreplaypath) || !Bkend.mreplaypath.Contains("\\")) || !Bkend.mreplaypath.EndsWith(".osr"))
            {
                infotb.Text = "No replay selected.";
                mrpvalid = false;
            }
            if (Bkend.mreplaypath.Contains("\\") && Bkend.mreplaypath.EndsWith(".osr"))
            {
                replayn = Bkend.mreplaypath.Substring(Bkend.mreplaypath.LastIndexOf("\\") + 1);
                mrpvalid = true;
                if (String.IsNullOrWhiteSpace(Bkend.msettingpath) || !Bkend.msettingpath.EndsWith(".json"))
                {
                    infotb.Text = $"Using replay:\n{replayn}\n- - - - - - - - - -\nNo setting selected, using default.json.";
                    mspvalid = false;
                }
                if (Bkend.mreplaypath.Contains("\\") && Bkend.msettingpath.EndsWith(".json"))
                {
                    settingn = Bkend.msettingpath.Substring(Bkend.msettingpath.LastIndexOf("\\") + 1);
                    infotb.Text = $"Using replay:\n{replayn}\n- - - - - - - - - -\nWith setting:\n{settingn}";
                    mspvalid = true;
                }
            }
        }

        public static void UpdateLaunchBT(Button launchbutton)
        {
            if (mrpvalid == false)
            {
                launchbutton.IsEnabled = false;
            }
            if (mrpvalid == true)
            {
                launchbutton.IsEnabled = true;
            }
        }
    }
}
