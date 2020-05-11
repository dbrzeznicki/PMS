using PMS.Model;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Algorithm
{
    public class PERT
    {

        public bool check(List<PERTTask> calc, List<PERTTask> postTask)
        {
            if (postTask.Count() == 0)
                return true;
            else
                foreach (var tmp in postTask)
                    if (!calc.Contains(tmp))
                        return false;

            return true;
        }

        public List<PERTTask> criticalPath(List<PERTTask> tasks)
        {

            PERTTask tt = new PERTTask();
            tt.setExpectedTime(tasks);
            tt.setStandardDeviation(tasks);
            tt.setVariance(tasks);
            List<PERTTask> calc = new List<PERTTask>();
            List<PERTTask> tmpTaskList = new List<PERTTask>(tasks);

            while (tmpTaskList.Count() != 0)
            {
                bool error = false;

                foreach (PERTTask task in tmpTaskList.ToList())
                {
                    bool tmp = check(calc, task.PostTask);

                    if (tmp)
                    {
                        int critical = 0;
                        foreach (PERTTask t in task.PostTask)
                            if (t.CS > critical)
                                critical = t.CS;

                        task.CS = critical + task.Expected_t;

                        calc.Add(task);
                        tmpTaskList.Remove(task);
                        error = true;
                    }
                }

                if (!error)
                {
                    ErrorMessage er = new ErrorMessage("Cyclic dependency!");
                    er.ShowDialog();
                    return new List<PERTTask>();
                }
            }

            tt.setMC(tasks);
            List<PERTTask> initialNodes = tt.getInitTask(tasks);

            tt.calcESAndEF(initialNodes);




            //ustawienie slack i CC

            foreach (var task in calc)
            {
                tt.setCC(task);
                tt.setSlack(task);
            }


            calc = calc.OrderBy(x => x.Name).ToList();

            return calc;
        }


    }
}
