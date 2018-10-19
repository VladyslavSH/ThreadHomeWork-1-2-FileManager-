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

namespace ThreadHW
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
                
                    TimerCallback timerCallback = new TimerCallback(GetProcess);
                    System.Threading.Timer timer = new System.Threading.Timer(timerCallback, null, 0, 10000);


        }

        private void GetProcess(object obj)
        {
            
                listView1.Invoke(new Action(() => {
                    listView1.Items.Clear();
                    foreach (var process in Process.GetProcesses())
                    {
                        listView1.Items.Add($"--Name: {process.ProcessName} - Id {process.Id} ");
                        listView1.Items.Add($"Count thread: {process.Threads.Count} - Count handle: {process.HandleCount}");
                        listView1.Items.Add("");
                    }
                }));
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string[] str;
            foreach (var item in listView1.Items)
            {
                File.WriteAllText("New file.txt", item);
            }
        }
    }
}
