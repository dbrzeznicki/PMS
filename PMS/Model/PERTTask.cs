using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.DataVisualization.Charting;

namespace PMS.Model
{
    public class PERTTask
    {
        public string Name { get; set; }
        public bool CC { get; set; } //critical condition
        public int ES { get; set; }
        public int EF { get; set; }
        public int LS { get; set; }
        public int LF { get; set; }
        public int Slack { get; set; }
        public int CS { get; set; }
        public int Expected_t { get; set; }
        public int Optimistic_a { get; set; }
        public int MostLikely_m { get; set; }
        public int Pessimistic_b { get; set; }
        public static int maxCost { get; set; }
        public List<PERTTask> PostTask { get; set; }
        public List<PERTTask> PreTask { get; set; }

        public double Variance { get; set; }
        public double StandardDeviation { get; set; }

        public PERTTask()
        {
        }

        public PERTTask(String name, int optimistic_a, int mostLikely_m, int pessimistic_b, List<PERTTask> postTask)
        {
            Name = name;
            Optimistic_a = optimistic_a;
            Pessimistic_b = pessimistic_b;
            MostLikely_m = mostLikely_m;
            EF = -1;
            PostTask = new List<PERTTask>();
            PreTask = new List<PERTTask>();
            foreach (PERTTask t in postTask)
                PreTask.Add(t);

        }

        public void setLSAndLF()
        {
            LS = maxCost - CS;
            LF = Expected_t + LS;
        }

        public void setCC(PERTTask t)
        {
            if (t.ES == t.LS)
                t.CC = true;
            else
                t.CC = false;
        }

        public void setSlack(PERTTask t)
        {
            t.Slack = t.LS - t.ES;
        }

        public void calcESAndEF(List<PERTTask> initTasks)
        {
            foreach (PERTTask t in initTasks)
            {
                t.ES = 0;
                t.EF = t.Expected_t;
                setESAndEFOneTask(t);
            }
        }


        public void setESAndEFOneTask(PERTTask initTask)
        {
            int ct = initTask.EF;
            foreach (PERTTask t in initTask.PostTask)
            {
                if (t.ES < ct)
                {
                    t.ES = ct;
                    t.EF = ct + t.Expected_t;
                }
                setESAndEFOneTask(t);
            }
        }

        public List<PERTTask> getInitTask(List<PERTTask> tasks)
        {
            List<PERTTask> initTask = new List<PERTTask>(tasks);
            foreach (PERTTask t in tasks)
                foreach (PERTTask td in t.PostTask)
                    initTask.Remove(td);

            return initTask; // zwraca zdarzenia init, z listy all zdarzen usuwa te co nie maja zdarzen poprzedzjacych
        }

        public void setMC(List<PERTTask> tasks) //set max cost
        {
            int max = -1;
            foreach (PERTTask t in tasks)
                if (t.CS > max)
                    max = t.CS;
            maxCost = max;

            foreach (PERTTask t in tasks)
                t.setLSAndLF();
        }

        public string StringPreTask
        {
            get
            {
                List<string> tmp = new List<string>(PreTask.Select(x => x.Name));
                return string.Join(",", tmp);
            }
        }

        public void setExpectedTime(List<PERTTask> tasks)
        {
            foreach (PERTTask t in tasks)
                t.Expected_t = (t.Optimistic_a + 4 * t.MostLikely_m + t.Pessimistic_b) / 6;
        }

        public void setStandardDeviation(List<PERTTask> tasks)
        {
            foreach (PERTTask t in tasks)
                t.StandardDeviation = Math.Round((t.Pessimistic_b - t.Optimistic_a) / 6.0, 2);
        }

        public void setVariance(List<PERTTask> tasks)
        {
            foreach (PERTTask t in tasks)
                t.Variance = Math.Round(Math.Pow((t.Pessimistic_b - t.Optimistic_a) / 6.0, 2), 2);
        }

    }
}


