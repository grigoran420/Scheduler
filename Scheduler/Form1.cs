using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Scheduler.Scripts;

namespace Scheduler
{
    public partial class Form1 : Form
    {
        public string name;
        readonly string Path = "Plans.pln";
        List<string> Plans;
        public Form1()
        {
            InitializeComponent();

            Plans = new List<string>();
            int sec = System.DateTime.Now.Second;
            Time.Text = $"{System.DateTime.Now.Hour}:{System.DateTime.Now.Minute}:{System.DateTime.Now.Second}";
            while (sec == System.DateTime.Now.Second)
            {
                
            }
            timer1.Enabled = true;
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            Time.Text = $"{System.DateTime.Now.Hour}:{System.DateTime.Now.Minute}:{System.DateTime.Now.Second}";
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            PlansRead read = new PlansRead();
            
            Plans = read.Read(Path);
            if (Plans != null)
            {
                for (int i = 0; i < Plans.Count; i++)
                {
                    listBox1.Items.Add(Plans[i]);
                }
            }
            else MessageBox.Show("Don't find data!", "Error", MessageBoxButtons.OK);
            Create.Enabled = true;
        }

        private void Create_Click(object sender, EventArgs e)
        {
            List<string> PlansWrite = new List<string>();
            name = textBox1.Text;

            PlansWrite.Add(name);
            PlansWrite.Add($"{dateTimePicker1.Value.Day}.{dateTimePicker1.Value.Month}.{dateTimePicker1.Value.Year}");
            PlansWrite.Add($"{Hours.SelectedItem}:{Minutes.SelectedItem}:{Seconds.SelectedItem}");
            PlansWrite.Add("---------------------------");
            for (int i = 0; i < PlansWrite.Count; i++)
            {
                listBox1.Items.Add(PlansWrite[i]);
                Plans.Add(PlansWrite[i]);
            }

            PlansWrite PW = new PlansWrite();
            PW.Write(Path, Plans);
        }

    }
}
