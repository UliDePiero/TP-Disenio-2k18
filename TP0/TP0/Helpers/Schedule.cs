using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Timers;

namespace TP0.Helpers
{
    public class Schedule
    {
        Recomendacion rec;
        Timer aTimer;

        public Schedule(Recomendacion c)
        {
            rec = c;
            CrearTimer();      
        }

        public void CrearTimer()
        {
            aTimer = new Timer();
            aTimer.Interval = 24*60*60*1000;
            aTimer.Elapsed += HandleTimerElapsed;
            aTimer.Start();
        }
        public void CambiarPeriodo(double hs)
        {
            aTimer.Interval = hs * 60 * 60 * 1000;
        }
        public void HandleTimerElapsed(object sender, ElapsedEventArgs e)
        {
            rec.ejecutarRecomendacion();
        }
    }
}