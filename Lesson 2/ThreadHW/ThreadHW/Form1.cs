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
                    System.Threading.Timer timer = new System.Threading.Timer(timerCallback, null, 3000, 3000);


        }

        private void GetProcess(object obj)
        {
            
                listView1.Invoke(new Action(() => {
                    listView1.Items.Clear();
                    foreach (var process in Process.GetProcesses().OrderBy(p=>p.ProcessName))
                    {
                        listView1.Items.Add($"--Name: {process.ProcessName} - Id {process.Id} ");
                        listView1.Items.Add($"Count thread: {process.Threads.Count} - Count handle: {process.HandleCount}");
                        listView1.Items.Add("");
                    }
                }));
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (FileStream fstream = new FileStream("New file.txt", FileMode.Create))
            {
                byte[] arr;
                arr = new byte[fstream.Length];
                int interval = 0;
                progressBar1.Invoke(new Action(() =>
                {
                    progressBar1.Maximum = Convert.ToInt32(listView1.Items.Count / 10);
                }));

                for (int i = 0; i < listView1.Items.Count / 10; i++)
                {
                    fstream.Read(arr, interval, 100);
                    interval += 100;
                    progressBar1.Invoke(new Action(() =>
                    {
                        progressBar1.Value = i + 1;
                    }));
                    Thread.Sleep(50);
                }
                using (StreamWriter sw = new StreamWriter(fstream))
                {

                    foreach (ListViewItem item in listView1.Items)
                    {
                        sw.WriteLine(item.Text);
                    }
                    
                }
                MessageBox.Show("File saved");

            }
        }

       
    }
}
