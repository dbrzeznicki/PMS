using DlhSoft.Windows.Controls.Pert;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PMS
{
    /// <summary>
    /// Interaction logic for UserControlPERTManager.xaml
    /// </summary>
    public partial class UserControlPERTManager : UserControl
    {

        //int i = 0;

        public UserControlPERTManager()
        {
            InitializeComponent();
            PERTManagerViewModel vm = new PERTManagerViewModel();
            this.DataContext = vm;
        }


        
        
        
        
        
        
        
        
        /*private void CriticalPathButton(object sender, RoutedEventArgs e)
        {

            

            if (i == 0)
            {
                foreach (PertChartItem item in PertChartView.ManagedItems)
                {
                    SetCriticalPathHighlighting(item, false);
                    if (item.Predecessors != null)
                    {
                        foreach (PredecessorItem predecessorItem in item.Predecessors)
                            SetCriticalPathHighlighting(predecessorItem, false);
                    }
                }
                foreach (PertChartItem item in PertChartView.GetCriticalItems())
                    SetCriticalPathHighlighting(item, true);
                foreach (PredecessorItem predecessorItem in PertChartView.GetCriticalDependencies())
                    SetCriticalPathHighlighting(predecessorItem, true);

                i = 1;
            } else
            {
                foreach (PredecessorItem predecessorItem in PertChartView.GetCriticalDependencies())
                    SetCriticalPathHighlighting(predecessorItem, false);
                foreach (PertChartItem item in PertChartView.GetCriticalItems())
                    SetCriticalPathHighlighting(item, false);
                i = 0;
            }





        }

        private void SetCriticalPathHighlighting(PertChartItem item, bool isHighlighted)
        {
            DlhSoft.Windows.Controls.Pert.PertChartView.SetShapeFill(item, isHighlighted ? Resources["CustomShapeFill"] as Brush : PertChartView.ShapeFill);
            DlhSoft.Windows.Controls.Pert.PertChartView.SetShapeStroke(item, isHighlighted ? Resources["CustomShapeStroke"] as Brush : PertChartView.ShapeStroke);
            DlhSoft.Windows.Controls.Pert.PertChartView.SetTextForeground(item, isHighlighted ? Resources["CustomShapeStroke"] as Brush : PertChartView.TextForeground);
        }


        private void SetCriticalPathHighlighting(PredecessorItem predecessorItem, bool isHighlighted)
        {
            DlhSoft.Windows.Controls.Pert.PertChartView.SetDependencyLineStroke(predecessorItem, isHighlighted ? Resources["CustomDependencyLineStroke"] as Brush : PertChartView.DependencyLineStroke);
            DlhSoft.Windows.Controls.Pert.PertChartView.SetDependencyTextForeground(predecessorItem, isHighlighted ? Resources["CustomDependencyLineStroke"] as Brush : PertChartView.DependencyTextForeground);
        }*/
    }
}
