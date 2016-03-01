using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace updateSteamMachineManager
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string applicationPath = System.Reflection.Assembly.GetEntryAssembly().Location;
            int idx = applicationPath.LastIndexOf('\\');
            applicationPath = applicationPath.Substring(0, idx);


            if (File.Exists(applicationPath + "\\update\\update.exe") == true)
            {
                //Kill the Steam Machine Manager application if it's still running
                try
                {
                    Process[] proc = Process.GetProcessesByName("Steam Machine Manager");
                    proc[0].Kill();
                }
                catch (Exception ex)
                {
                    ErrorLog(ex.Message.ToString());
                }

                Thread.Sleep(1000);

                //Move the update to the program path
                try
                {
                    File.Move(applicationPath + "\\update\\update.exe", applicationPath + "\\update.exe");
                }
                catch(Exception ex)
                {
                    ErrorLog(ex.Message.ToString());
                }

                Thread.Sleep(1000);

                //Delete the old file
                try
                {
                    File.Delete(applicationPath + "\\Steam Machine Manager.exe");
                }
                catch(Exception ex)
                {
                    ErrorLog(ex.Message.ToString());
                }

                Thread.Sleep(1000);

                //Rename the updated file
                try
                {
                    File.Move(applicationPath + "\\update.exe", applicationPath + "\\Steam Machine Manager.exe");
                }
                catch(Exception ex)
                {
                    ErrorLog(ex.Message.ToString());
                }
                

                //Start the new application
                try
                {
                    ProcessStartInfo startInfo = new ProcessStartInfo(applicationPath + "\\Steam Machine Manager.exe");
                    startInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    Process.Start(startInfo);
                }
                catch(Exception ex)
                {
                    ErrorLog(ex.Message.ToString());
                }
                

                //Close the updater
                Application.Exit();
                }
            else
            {
                Application.Exit();
            }


            }



        



        private void Form_Shown(object sender, EventArgs e)
        {
            Visible = false;
            Opacity = 100;
        }


        public void ErrorLog(string sErrMsg)
        {
            try
            {
                string sPathName = "updateerror.txt";
                string sErrorTime = DateTime.Now.ToString();
                StreamWriter sw = new StreamWriter(sPathName, true);
                sw.WriteLine(sErrorTime + " --  " + sErrMsg);
                sw.Flush();
                sw.Close();
            }
            catch (Exception)
            {

            }

        }
    }
}
