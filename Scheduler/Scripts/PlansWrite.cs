using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Scheduler.Scripts
{
    class PlansWrite
    {
        private StreamWriter sw;


        internal void Write (string Path, List<string> Plans)
        {
            sw = new StreamWriter(Path);

            for (int i = 0; i < Plans.Count; i++)
            {
                sw.WriteLine(Plans[i]);
            }
            sw.Close();
        }
    }
}
