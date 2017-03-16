using System.Windows.Controls;

namespace ExcelExport.Controls
{
    /// <summary>
    /// Interaction logic for ControlView.xaml
    /// </summary>
    public partial class ControlsView : UserControl
    {
        public ControlsView(ControlsViewModel viewModel)
        {
            DataContext = viewModel;

            InitializeComponent();
        }
    }
}
