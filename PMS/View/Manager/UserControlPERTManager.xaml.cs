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


        
        
        private void CriticalPathButton(object sender, RoutedEventArgs e)
        {


            int i = 0;


            if (i == 0)
            {
                foreach (NetworkDiagramItem item in NetworkDiagramView.ManagedItems)
                {
                    SetCriticalPathHighlighting(item, false);
                    if (item.Predecessors != null)
                    {
                        foreach (NetworkPredecessorItem predecessorItem in item.Predecessors)
                            SetCriticalPathHighlighting(predecessorItem, false);
                    }
                }
                foreach (NetworkDiagramItem item in NetworkDiagramView.GetCriticalItems())
                    SetCriticalPathHighlighting(item, true);
                foreach (NetworkPredecessorItem predecessorItem in NetworkDiagramView.GetCriticalDependencies())
                    SetCriticalPathHighlighting(predecessorItem, true);

                i = 1;
            } else
            {
                foreach (NetworkPredecessorItem predecessorItem in NetworkDiagramView.GetCriticalDependencies())
                    SetCriticalPathHighlighting(predecessorItem, false);
                foreach (NetworkDiagramItem item in NetworkDiagramView.GetCriticalItems())
                    SetCriticalPathHighlighting(item, false);
                i = 0;
            }





        }

        private void SetCriticalPathHighlighting(NetworkDiagramItem item, bool isHighlighted)
        {
            DlhSoft.Windows.Controls.Pert.NetworkDiagramView.SetShapeFill(item, isHighlighted ? Resources["CustomShapeFill"] as Brush : NetworkDiagramView.ShapeFill);
            DlhSoft.Windows.Controls.Pert.NetworkDiagramView.SetShapeStroke(item, isHighlighted ? Resources["CustomShapeStroke"] as Brush : NetworkDiagramView.ShapeStroke);
        }
        private void SetCriticalPathHighlighting(NetworkPredecessorItem predecessorItem, bool isHighlighted)
        {
            DlhSoft.Windows.Controls.Pert.NetworkDiagramView.SetDependencyLineStroke(predecessorItem, isHighlighted ? Resources["CustomDependencyLineStroke"] as Brush : NetworkDiagramView.DependencyLineStroke);
        }
    }
}
