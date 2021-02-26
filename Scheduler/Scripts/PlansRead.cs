using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace Scheduler.Scripts
{
    
    class PlansRead
    {
        StreamReader sr;
        readonly List<string> Plans;
        internal PlansRead()
        {
            Plans = new List<string>();
        }

        internal List<string> Read (string Path)
        {
            try
            {
                sr = new StreamReader(Path);
            }
            catch (FileNotFoundException)
            {
                if (DialogResult.Yes == MessageBox.Show("File not found. Want to create it?", "Error", MessageBoxButtons.YesNo))
                {
                    StreamWriter create = new StreamWriter(Path);
                    create.Close();
                    sr = new StreamReader(Path);
                }
                
            }
            
            if (sr.BaseStream.Length == 0)
            {
                sr.Close();
                return Plans;
            }

            while (!sr.EndOfStream)
            {
                Plans.Add(sr.ReadLine());
            }
            sr.Close();
            return Plans;
            
        }

    }
}
