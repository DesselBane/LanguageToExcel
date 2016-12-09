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

namespace Language2Excel
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string NO_FILE_SELECTED = "Please select a file.";

        #region Properties

        public string firstFile
        {
            get { return (string)GetValue(firstFileProperty); }
            set { SetValue(firstFileProperty, value); }
        }

        public string secondFile
        {
            get { return (string)GetValue(secondFileProperty); }
            set { SetValue(secondFileProperty, value); }
        }

        public string thirdFile
        {
            get { return (string)GetValue(thirdFileProperty); }
            set { SetValue(thirdFileProperty, value); }
        }

        #region Normal

        #endregion

        #region Dependencey

        public static DependencyProperty firstFileProperty = DependencyProperty.Register(
            "firstFile",typeof(string),typeof(MainWindow)
            );

        public static DependencyProperty secondFileProperty = DependencyProperty.Register(
            "secondFile", typeof(string), typeof(MainWindow)
            );

        public static DependencyProperty thirdFileProperty = DependencyProperty.Register(
            "thirdFile", typeof(string), typeof(MainWindow)
            );

        #endregion

        #endregion

        #region Constructors

        public MainWindow()
        {
            InitializeComponent();
            thirdFile = secondFile = firstFile = NO_FILE_SELECTED;
        }

        #endregion

        #region Event Handlers

        private void btn_firstFileBrowse_Click(object sender, RoutedEventArgs e)
        {
            firstFile = selectFile();
        }

        private void btn_secondFileBrowse_Click(object sender, RoutedEventArgs e)
        {
            secondFile = selectFile();
        }

        private void btn_thirdFileBrowse_Click(object sender, RoutedEventArgs e)
        {
            thirdFile = selectFile();
        }

        private void btn_go_Click(object sender, RoutedEventArgs e)
        {
            if( chBox_first.IsChecked == true && firstFile == NO_FILE_SELECTED ||
                chBox_second.IsChecked == true && secondFile == NO_FILE_SELECTED ||
                chBox_third.IsChecked == true && thirdFile == NO_FILE_SELECTED      )
            {
                MessageBox.Show(NO_FILE_SELECTED, "No file selected", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            parseFiles();
        }

        private void btn_outputBrowse_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveWin = new SaveFileDialog();

            saveWin.DefaultExt = ".xml";
            
        }

        private void btn_quit_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        #endregion

        #region private

        private string selectFile()
        {
            OpenFileDialog openWin = new OpenFileDialog();
            openWin.DefaultExt = ".properties";
            openWin.Filter = "Properties|*.properties|All Files|*.*";

            bool? result = openWin.ShowDialog();

            if (result != null && result == true)
            {
                return openWin.FileName;
            }

            return NO_FILE_SELECTED;
        }

        private async void parseFiles()
        {
            output("Initializing...");

            output("Validating files...");
            var result = await validateFilesAsync(firstFile,secondFile,thirdFile);
            if(result.Item1 != null)
            {
                output("File validation failed (see below message for more information)");
                output(result.Item1.Message);
                return;
            }


            output("Done...");
        }

        private Task<Tuple<Exception, FileStream[]>> validateFilesAsync(params string[] files)
        {
            return Task.Run(() => validateFiles(files));
        }

        private Tuple<Exception,FileStream[]> validateFiles(params string[] files)
        {
            FileStream[] streams = new FileStream[files.Length];

            try
            {
                for(int i = 0; i < files.Length;i ++)
                {
                    streams[i] = File.OpenRead(files[i]);
                }
            }
            catch (Exception e)
            {
                return new Tuple<Exception, FileStream[]>(e, streams);
            }

            return new Tuple<Exception, FileStream[]>(null,streams);
        }

        private void output(string msg,bool newLine = true)
        {
            txt_output.Text += msg;
            if (newLine)
                txt_output.Text += "\n";
        }

        #endregion

    }
}
