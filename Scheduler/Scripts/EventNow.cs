﻿using System;
using System.Collections.Generic;
using System.Media;
using System.Windows.Forms;
using System.IO;

namespace Scheduler.Scripts
{
    class EventNow
    {
        private readonly List<string> Stack;
        internal EventNow()
        {
            Stack = new List<string>();
        }

        internal List<string> Check (List<string> Plans)
        {
            DateTime DaysNow = DateTime.MinValue;
            for (int i = 0; i < Plans.Count; i += 4)
            {
                for (int j = i; j < i + 4; j++) Stack.Add(Plans[j]);

                DaysNow = _dayEvent(Stack[1], Stack[2]);

                if (DaysNow < DateTime.Now)
                {
                    Plans.RemoveRange(i, 4);
                    SoundPlayer player = new SoundPlayer(@"Sound.wav");
                    player.Load(); 
                    player.Play();
                    MessageBox.Show("The event happened", "Congratulations", MessageBoxButtons.OK);
                }

                Stack.Clear();
            }
            return Plans;
        }

        private DateTime _dayEvent (string date, string Time)
        {
            DateTime time;
            string buf = date + " " + Time;
            time = Convert.ToDateTime(buf);
            return time;
        }
    }
}
