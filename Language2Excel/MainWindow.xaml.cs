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
using OfficeOpenXml;
using System.Data;

namespace Language2Excel
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string NO_FILE_SELECTED = "Please select a file.";
        private const string ERR_01 = "Err01: Target file format is invalid.";
        private const string NO_OUTPUT = "Please select where to save the output.";


        #region Properties

        #region Normal / vars

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

        public string outputFile
        {
            get { return (string)GetValue(outputFileProperty); }
            set { SetValue(outputFileProperty, value); }
        }

        public Encoding Encoding
        {
            get { return (Encoding) GetValue(encodingProperty); }
            set { SetValue(encodingProperty,value);}
        }

        private FileStream[] _fileStreams;
        private string[] _filePaths;
        private Dictionary<string, string[]> _parsedValues;
        private string[] _columnNames;

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

        public static DependencyProperty outputFileProperty = DependencyProperty.Register(
            "outputFile", typeof(string), typeof(MainWindow)
            );

        public static DependencyProperty encodingProperty = DependencyProperty.Register(
            nameof(Encoding), typeof(Encoding),typeof(MainWindow));

        #endregion

        #endregion

        #region Constructors

        public MainWindow()
        {
            InitializeComponent();
            thirdFile = secondFile = firstFile = NO_FILE_SELECTED;
            outputFile = NO_OUTPUT;
            Encoding = Encoding.ASCII;
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
                chBox_third.IsChecked == true && thirdFile == NO_FILE_SELECTED)
            {
                MessageBox.Show(NO_FILE_SELECTED, "No file selected", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if(outputFile == NO_OUTPUT)
            {
                MessageBox.Show(NO_OUTPUT, "No file selected", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            parseFiles();
        }

        private void btn_outputBrowse_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveWin = new SaveFileDialog();

            saveWin.DefaultExt = "*.xlsx";
            saveWin.Filter = "Excel 2010|*.xlsx";

            bool? res = saveWin.ShowDialog();

            if(res == true)
            {
                outputFile = saveWin.FileName;
            }
        }

        private void btn_quit_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void btn_openExcel_Click(object sender, RoutedEventArgs e)
        {
            FileInfo file = new FileInfo(outputFile);
            if(!file.Exists)
            {
                MessageBox.Show("No file to open");
                return;
            }

            System.Diagnostics.Process.Start(outputFile);
        }

        private void btn_clear_Click(object sender, RoutedEventArgs e)
        {
            txt_output.Text = "";
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

        private void initParsing()
        {
            int counter = 0;
            if (chBox_first.IsChecked == true)
                counter++;
            if (chBox_second.IsChecked == true)
                counter++;
            if (chBox_third.IsChecked == true)
                counter++;

            _filePaths = new string[counter];
            _columnNames = new string[counter];
            counter = 0;

            if (chBox_first.IsChecked == true)
            {
                _filePaths[counter] = firstFile;
                _columnNames[counter] = txt_firstLanguage.Text;
                counter++;
            }
            if (chBox_second.IsChecked == true)
            {
                _filePaths[counter] = secondFile;
                _columnNames[counter] = txt_secondLanguage.Text;
                counter++;
            }
            if (chBox_third.IsChecked == true)
            {
                _filePaths[counter] = thirdFile;
                _columnNames[counter] = txt_thirdLanguage.Text;
                counter++;
            }
        }

        private async void parseFiles()
        {
            output("Initializing...");

            initParsing();

            output("Validating files...",false);
            var result = await validateFilesAsync(_filePaths);
            if(result.Item1 != null)
            {
                output("\t\t[FAIL]");
                output("File validation failed (see below message for more information)");
                output(result.Item1.Message);
                return;
            }
            output("\t\t\t\t\t\t\t\t[OK]");
            _fileStreams = result.Item2;

            _parsedValues = new Dictionary<string, string[]>();


            for(int i = 0; i < _columnNames.Length; i++)
            {
                output("Parsing File:" + _filePaths[i], false);
                var res1 = await parseFilesAsync(_fileStreams[i], _parsedValues, i, _fileStreams.Length, GetEncoding(Encoding));
                if (res1 != null)
                {
                    output("\t\t[FAIL]");
                    output("Parsing failed (see below message for more information)");
                    output(res1.Message);
                    return;
                }
                output("\t\t[OK]");
            }

            output("Writing data to Excel file...", false);
            var res = await writeDataToExcelAsync(outputFile, _parsedValues,_columnNames);

            if (res != null)
            {
                output("\t\t[FAIL]");
                output("Writing to Excel failed (see below message for more information)");
                output(res.Message);
                return;
            }
            output("\t\t\t\t\t\t\t[OK]");

            output("Done...");
            output("");
            output("=================================================================================");
            output("");
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


        private Task<Exception> parseFilesAsync(FileStream Stream,Dictionary<string,string[]> destination,int id,int total,System.Text.Encoding enc)
        {
            return Task.Run(() => parseFiles(Stream,destination,id,total,enc));
        }
        private Exception parseFiles(FileStream Stream, Dictionary<string, string[]> destination, int id,int total,System.Text.Encoding enc)
        {
            StreamReader sr = new StreamReader(Stream, enc);
            
            string line;

            while((line = sr.ReadLine()) != null)
            {
                string[] splitted = line.Split('=');
                if (splitted.Length < 2)
                    return new Exception(ERR_01);

                if (!destination.ContainsKey(splitted[0]))
                    destination.Add(splitted[0], new string[total]);

                (destination[splitted[0]])[id] = "";

                for (int i = 1;i<splitted.Length;i++)
                {
                    (destination[splitted[0]])[id] += splitted[i];
                }
                
            }


            return null;
        }


        private Task<Exception> writeDataToExcelAsync(string targetFile, Dictionary<string,string[]> parsedValues, string[] columnNames)
        {
            return Task.Run(() => writeDataToExcel(targetFile, parsedValues,columnNames));
        }

        private Exception writeDataToExcel(string targetFile, Dictionary<string,string[]> parsedValues,string[] columnNames)
        {
            FileInfo outputFile = new FileInfo(targetFile);

            if(outputFile.Exists)
            {
                outputFile.Delete();
                outputFile = new FileInfo(targetFile);
            }


            using (ExcelPackage pkg = new ExcelPackage(outputFile))
            {
                ExcelWorksheet sheet = pkg.Workbook.Worksheets.Add("Languages");
                int lineCount = 2;

                sheet.Cells["A1"].Value = "Keys";
                for(int i = 0; i < _columnNames.Length; i++)
                {
                    char pos = (char)('B' + i);
                    sheet.Cells[pos + "1"].Value = _columnNames[i];
                }

                foreach(KeyValuePair<string,string[]> kvp in parsedValues)
                {
                    sheet.Cells["A" + lineCount].Value = kvp.Key;
                    for (int i = 0; i < kvp.Value.Length; i++)
                    {
                        char pos = (char)('B' + i);
                        sheet.Cells[pos.ToString() + lineCount.ToString()].Value = kvp.Value[i];
                    }
                    lineCount++;
                }

                pkg.Save();
            }

                return null;
        }


        private void output(string msg,bool newLine = true)
        {
            txt_output.Text += msg;
            if (newLine)
                txt_output.Text += "\n";

            txt_output.ScrollToEnd();
        }

        private System.Text.Encoding GetEncoding(Encoding enc)
        {
            switch (enc)
            {
                case Encoding.ASCII:
                    return System.Text.Encoding.ASCII;
                case Encoding.UTF_8:
                    return System.Text.Encoding.UTF8;
                case Encoding.UTF_7:
                    return System.Text.Encoding.UTF7;
                case Encoding.UTF_32:
                    return System.Text.Encoding.UTF32;
                case Encoding.UNICODE:
                    return System.Text.Encoding.Unicode;
                default:
                    throw new ArgumentOutOfRangeException(nameof(enc), enc, null);
            }
        }

        #endregion
    }
}
