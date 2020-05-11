using DlhSoft.Windows.Controls.Pert;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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

        int i = 0;
        public UserControlPERTManager()
        {
            InitializeComponent();
            PERTManagerViewModel vm = new PERTManagerViewModel();
            this.DataContext = vm;
        }

        private void EIButton_Click(object sender, RoutedEventArgs e)
        {
            NDV.Export((Action)delegate
            {
                SaveFileDialog SFD = new SaveFileDialog
                {
                    Filter = "PNG image files|*.png|JPEG image files|*.jpeg",
                    FileName = "PERT",
                    Title = "Export PERT diagram to png"
                };
                if (SFD.ShowDialog() != true)
                    return;
                BitmapSource BS = NDV.GetExportBitmapSource(194);
                Stream S = SFD.OpenFile();
                BitmapEncoder(S, BS);
            });
        }

        private void CriticalPathButton(object sender, RoutedEventArgs e)
        {

            if(NDV.ManagedItems.Count() > 0 )
            {
                if (i == 0)
                {
                    foreach (NetworkDiagramItem i in NDV.ManagedItems)
                    {
                        NetworkDiagramView.SetShapeFill(i, false ? Resources["CustomShapeFill"] as Brush : NDV.ShapeFill);
                        NetworkDiagramView.SetShapeStroke(i, false ? Resources["CustomShapeStroke"] as Brush : NDV.ShapeStroke);
                        if (i.Predecessors != null)
                            foreach (NetworkPredecessorItem PI in i.Predecessors)
                                NetworkDiagramView.SetDependencyLineStroke(PI, false ? Resources["CustomDependencyLineStroke"] as Brush : NDV.DependencyLineStroke);
                    }
                    foreach (NetworkDiagramItem i in NDV.GetCriticalItems())
                    {
                        NetworkDiagramView.SetShapeFill(i, true ? Resources["CustomShapeFill"] as Brush : NDV.ShapeFill);
                        NetworkDiagramView.SetShapeStroke(i, true ? Resources["CustomShapeStroke"] as Brush : NDV.ShapeStroke);
                    }
                    foreach (NetworkPredecessorItem PI in NDV.GetCriticalDependencies())
                        NetworkDiagramView.SetDependencyLineStroke(PI, true ? Resources["CustomDependencyLineStroke"] as Brush : NDV.DependencyLineStroke);

                    i = 1;
                }
                else
                {
                    foreach (NetworkPredecessorItem PI in NDV.GetCriticalDependencies())
                        NetworkDiagramView.SetDependencyLineStroke(PI, false ? Resources["CustomDependencyLineStroke"] as Brush : NDV.DependencyLineStroke);
                    foreach (NetworkDiagramItem i in NDV.GetCriticalItems())
                    {
                        NetworkDiagramView.SetShapeFill(i, false ? Resources["CustomShapeFill"] as Brush : NDV.ShapeFill);
                        NetworkDiagramView.SetShapeStroke(i, false ? Resources["CustomShapeStroke"] as Brush : NDV.ShapeStroke);
                    }
                    i = 0;
                }
            }
        }

        private void BitmapEncoder(Stream S, BitmapSource BS)
        {
            PngBitmapEncoder PBE = new PngBitmapEncoder();
            PBE.Frames.Add(BitmapFrame.Create(BS));
            PBE.Save(S);
        }
    }
}
