﻿using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace Scheduler.Scripts
{
    
    class PlansRead
    {
        private StreamReader sr;
        private readonly List<string> Plans;
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
                }else if (sr == null)
                {
                    return Plans;
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
