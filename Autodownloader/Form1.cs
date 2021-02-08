using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Autodownloader
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            if (!Directory.Exists(@"C:\App"))
            {
                Directory.CreateDirectory(@"C:\App");
            }
        }
   
        Uri uri = new Uri("https://www.istripper.com/fileaccess/software");
        string filename = @"C:\App\setup.exe";
      
        private void BtnStart_Click(object sender, EventArgs e)
        {
            //This Code will Start Downloading the file automatically without any notice.
            //File will be saved in the predefined directory.
            try
            {
                if (File.Exists(filename))
                {
                    File.Delete(filename);
                }
                else
                {
                    WebClient wc = new WebClient();
                    wc.DownloadFileAsync(uri, filename);

                    wc.DownloadFileCompleted += new AsyncCompletedEventHandler(wc_DownloadFileCompleted);


                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

            

        }

        // This method will open the command prompt and try to install the exe silently.
        // All processes will done quietly without disturbing the end user.

        public static void StartInstallationOfExeSilently()
        {
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = "/K C:\\App\\setup.exe /VERYSILENT";
            process.StartInfo = startInfo;
            process.Start();
            process.WaitForExit();
        }

        private void wc_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                //MessageBox.Show("Download complete!, running exe", "Completed!");
                //Process.Start(filename);

                StartInstallationOfExeSilently();

            }
            else
            {
                //MessageBox.Show("Unable to download exe, please check your connection", "Download failed!");
            }
        }
    }
}