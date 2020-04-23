using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Model
{
    public class PERTTask
    {
        public string Name { get; set; }
        public bool CC { get; set; } //critical condition
        public int ES { get; set; }
        public int EF{ get; set; }
        public int LS{ get; set; }
        public int LF{ get; set; }
        public int  Slack { get; set; }
        public int CS { get; set; }
        public int Cost { get; set; } //early start  early finish  latest start   latest finish   luz   critical cost  
        public static int maxCost { get; set; }
        public List<PERTTask> PostTask { get; set; } 
        public List<PERTTask> PreTask { get; set; } 

        public PERTTask()
        {
        }

        public PERTTask(String name, int cost, List<PERTTask> postTask)
        {
            Name = name;
            Cost = cost;
            EF = -1;
            PostTask = new List<PERTTask>();
            PreTask = new List<PERTTask>();
            foreach (PERTTask t in postTask)
                PreTask.Add(t);

        }

        public void setLSAndLF()
        {
            LS = maxCost - CS;
            LF = Cost + LS;
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
                t.EF = t.Cost;
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
                    t.EF = ct + t.Cost;
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
    }
}

