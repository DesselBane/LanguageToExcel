using System.Windows.Controls;

namespace ExcelExport.Files
{
    /// <summary>
    /// Interaction logic for PropertiesFilesListView.xaml
    /// </summary>
    public partial class PropertiesFilesListView : UserControl
    {
        public PropertiesFilesListView(PropertiesFileListViewModel viewModel)
        {
            DataContext = viewModel;
            InitializeComponent();
        }
    }
}
