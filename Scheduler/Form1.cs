using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Scheduler.Scripts;

namespace Scheduler
{
    public partial class Form1 : Form
    {
        private readonly string Path = "Plans.pln";
        private List<string> Plans;

        public Form1()
        {
            InitializeComponent();
            
            Plans = new List<string>();
            int sec = System.DateTime.Now.Second;
            Time.Text = $"{System.DateTime.Now.Hour}:{System.DateTime.Now.Minute}:{System.DateTime.Now.Second}";
            while (sec == System.DateTime.Now.Second) { }
            dateTimePicker1.Value = DateTime.Now;
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
            //else MessageBox.Show("Don't find data!", "Error", MessageBoxButtons.OK);
            Create.Enabled = true;
            EventTimer.Enabled = true;
        }

        private void Create_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Name is missing", "Warring", MessageBoxButtons.OK);
                return;
            } else if ((Hours.SelectedItem == null) || (Minutes.SelectedItem == null) || (Seconds.SelectedItem == null))
            {
                MessageBox.Show("Wrong time", "Warring", MessageBoxButtons.OK);
                return;
            }
            if (listBox1.Items.Count > 399)
            {
                MessageBox.Show("Event overflow", "Warring", MessageBoxButtons.OK);
                return;
            }
            for (int i = 1; i < listBox1.Items.Count; i += 4)
            {
                string bufferDate = $"{dateTimePicker1.Value.Day}.{dateTimePicker1.Value.Month}.{dateTimePicker1.Value.Year}";
                string bufferTime = $"{Hours.SelectedItem}:{Minutes.SelectedItem}:{Seconds.SelectedItem}";
                if ((Convert.ToDateTime(listBox1.Items[i]) == Convert.ToDateTime(bufferDate)) && (Convert.ToDateTime(listBox1.Items[i + 1 ]) == Convert.ToDateTime(bufferTime)) && (listBox1.Items[i - 1].ToString() == textBox1.Text))
                {
                    MessageBox.Show("Dublicate data", "Warning", MessageBoxButtons.OK);
                    return;
                }
            }

            List<string> PlansWrite = new List<string>();

            PlansWrite.Add(textBox1.Text);
            PlansWrite.Add($"{dateTimePicker1.Value.Day}.{dateTimePicker1.Value.Month}.{dateTimePicker1.Value.Year}");
            PlansWrite.Add($"{Hours.SelectedItem}:{Minutes.SelectedItem}:{Seconds.SelectedItem}");
            PlansWrite.Add("--------------------------------------");
            for (int i = 0; i < PlansWrite.Count; i++)
            {
                listBox1.Items.Add(PlansWrite[i]);
                Plans.Add(PlansWrite[i]);
            }

            PlansWrite PW = new PlansWrite();
            PW.Write(Path, Plans);
        }

        private void EventTimer_Tick(object sender, EventArgs e)
        {
            EventNow @event = new EventNow();
            List<string> EventActive = @event.Check(Plans);
            listBox1.Items.Clear();
            for (int i = 0; i < EventActive.Count; i++)
            {
                listBox1.Items.Add(EventActive[i]);
            }
            //MessageBox.Show(EventActive.ToString());
        }
    }
}
