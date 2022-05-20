using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace ReplaySelectorDanser
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

        private void replaybt_Click(object sender, RoutedEventArgs e)
        {
            string test = Fgen.GetReplayFileYep(replaytb.Text);
            replaytb.Text = test;
            replaytb.Select(replaytb.Text.Length, 0);
        }

        private void replaytb_TextChanged(object sender, TextChangedEventArgs e)
        {
            Bkend.mreplaypath = replaytb.Text;

            Bkend.UpdateInfoTB(infotb);
            Bkend.UpdateLaunchBT(launchbutton);

            replaytb.Focus();

        }

        private void settingbt_Click(object sender, RoutedEventArgs e)
        {
            string test = Fgen.GetSettingFileYep(settingtb.Text);
            settingtb.Text = test;
            settingtb.Select(settingtb.Text.Length, 0);
        }

        private void settingtb_TextChanged(object sender, TextChangedEventArgs e)
        {
            Bkend.msettingpath = settingtb.Text;
            Bkend.UpdateInfoTB(infotb);

            settingtb.Focus();
        }

        private void launchbutton_Click(object sender, RoutedEventArgs e)
        {
            Bkend.StartRender(Bkend.ArgsMaker(this));

           
            DispatcherTimer dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();

            launchbutton.IsEnabled = false;

            void dispatcherTimer_Tick(object? sender, EventArgs e)
            {
                launchbutton.IsEnabled = true;
                dispatcherTimer.Stop();
            }

        }
    }
}
