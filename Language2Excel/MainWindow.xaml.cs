using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using OfficeOpenXml;

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

        public string FirstFile
        {
            get { return (string)GetValue(FirstFileProperty); }
            set { SetValue(FirstFileProperty, value); }
        }

        public string SecondFile
        {
            get { return (string)GetValue(SecondFileProperty); }
            set { SetValue(SecondFileProperty, value); }
        }

        public string ThirdFile
        {
            get { return (string)GetValue(ThirdFileProperty); }
            set { SetValue(ThirdFileProperty, value); }
        }

        public string OutputFile
        {
            get { return (string)GetValue(OutputFileProperty); }
            set { SetValue(OutputFileProperty, value); }
        }

        public Encoding Encoding
        {
            get { return (Encoding) GetValue(_EncodingProperty); }
            set { SetValue(_EncodingProperty,value);}
        }

        private FileStream[] _fileStreams;
        private string[] _filePaths;
        private Dictionary<string, string[]> _parsedValues;
        private string[] _columnNames;

        #endregion

        #region Dependencey

        public static readonly DependencyProperty FirstFileProperty = DependencyProperty.Register(
            "FirstFile",typeof(string),typeof(MainWindow)
            );

        public static readonly DependencyProperty SecondFileProperty = DependencyProperty.Register(
            "SecondFile", typeof(string), typeof(MainWindow)
            );

        public static readonly DependencyProperty ThirdFileProperty = DependencyProperty.Register(
            "ThirdFile", typeof(string), typeof(MainWindow)
            );

        public static readonly DependencyProperty OutputFileProperty = DependencyProperty.Register(
            "OutputFile", typeof(string), typeof(MainWindow)
            );

        public static readonly DependencyProperty _EncodingProperty = DependencyProperty.Register(
            nameof(Encoding), typeof(Encoding),typeof(MainWindow));

        #endregion

        #endregion

        #region Constructors

        public MainWindow()
        {
            InitializeComponent();
            ThirdFile = SecondFile = FirstFile = NO_FILE_SELECTED;
            OutputFile = NO_OUTPUT;
            Encoding = Encoding.ASCII;
        }

        #endregion

        #region Event Handlers

        private void btn_firstFileBrowse_Click(object sender, RoutedEventArgs e)
        {
            FirstFile = SelectFile();
        }

        private void btn_secondFileBrowse_Click(object sender, RoutedEventArgs e)
        {
            SecondFile = SelectFile();
        }

        private void btn_thirdFileBrowse_Click(object sender, RoutedEventArgs e)
        {
            ThirdFile = SelectFile();
        }

        private void btn_go_Click(object sender, RoutedEventArgs e)
        {
            if( chBox_first.IsChecked == true && FirstFile == NO_FILE_SELECTED ||
                chBox_second.IsChecked == true && SecondFile == NO_FILE_SELECTED ||
                chBox_third.IsChecked == true && ThirdFile == NO_FILE_SELECTED)
            {
                MessageBox.Show(NO_FILE_SELECTED, "No file selected", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if(OutputFile == NO_OUTPUT)
            {
                MessageBox.Show(NO_OUTPUT, "No file selected", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            ParseFiles();
        }

        private void btn_outputBrowse_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveWin = new SaveFileDialog();

            saveWin.DefaultExt = "*.xlsx";
            saveWin.Filter = "Excel 2010|*.xlsx";

            bool? res = saveWin.ShowDialog();

            if(res == true)
            {
                OutputFile = saveWin.FileName;
            }
        }

        private void btn_quit_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void btn_openExcel_Click(object sender, RoutedEventArgs e)
        {
            FileInfo file = new FileInfo(OutputFile);
            if(!file.Exists)
            {
                MessageBox.Show("No file to open");
                return;
            }

            System.Diagnostics.Process.Start(OutputFile);
        }

        private void btn_clear_Click(object sender, RoutedEventArgs e)
        {
            txt_output.Text = "";
        }

        #endregion

        #region private

        private string SelectFile()
        {
            var openWin = new OpenFileDialog();
            openWin.DefaultExt = ".properties";
            openWin.Filter = "Properties|*.properties|All Files|*.*";

            bool? result = openWin.ShowDialog();

            if (result != null && result == true)
            {
                return openWin.FileName;
            }

            return NO_FILE_SELECTED;
        }

        private void InitParsing()
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
                _filePaths[counter] = FirstFile;
                _columnNames[counter] = txt_firstLanguage.Text;
                counter++;
            }
            if (chBox_second.IsChecked == true)
            {
                _filePaths[counter] = SecondFile;
                _columnNames[counter] = txt_secondLanguage.Text;
                counter++;
            }
            if (chBox_third.IsChecked == true)
            {
                _filePaths[counter] = ThirdFile;
                _columnNames[counter] = txt_thirdLanguage.Text;
                counter++;
            }
        }

        private async void ParseFiles()
        {
            Output("Initializing...");

            InitParsing();

            Output("Validating files...",false);
            var result = await ValidateFilesAsync(_filePaths);
            if(result.Item1 != null)
            {
                Output("\t\t[FAIL]");
                Output("File validation failed (see below message for more information)");
                Output(result.Item1.Message);
                return;
            }
            Output("\t\t\t\t\t\t\t\t[OK]");
            _fileStreams = result.Item2;

            _parsedValues = new Dictionary<string, string[]>();


            for(int i = 0; i < _columnNames.Length; i++)
            {
                Output("Parsing File:" + _filePaths[i], false);
                var res1 = await ParseFilesAsync(_fileStreams[i], _parsedValues, i, _fileStreams.Length, GetEncoding(Encoding));
                if (res1 != null)
                {
                    Output("\t\t[FAIL]");
                    Output("Parsing failed (see below message for more information)");
                    Output(res1.Message);
                    return;
                }
                Output("\t\t[OK]");
            }

            Output("Writing data to Excel file...", false);
            var res = await WriteDataToExcelAsync(OutputFile, _parsedValues,_columnNames);

            if (res != null)
            {
                Output("\t\t[FAIL]");
                Output("Writing to Excel failed (see below message for more information)");
                Output(res.Message);
                return;
            }
            Output("\t\t\t\t\t\t\t[OK]");

            Output("Done...");
            Output("");
            Output("=================================================================================");
            Output("");
        }

        private Task<Tuple<Exception, FileStream[]>> ValidateFilesAsync(params string[] files)
        {
            return Task.Run(() => ValidateFiles(files));
        }
        private Tuple<Exception,FileStream[]> ValidateFiles(params string[] files)
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


        private Task<Exception> ParseFilesAsync(FileStream stream,Dictionary<string,string[]> destination,int id,int total,System.Text.Encoding enc)
        {
            return Task.Run(() => ParseFiles(stream,destination,id,total,enc));
        }
        private Exception ParseFiles(FileStream stream, Dictionary<string, string[]> destination, int id,int total,System.Text.Encoding enc)
        {
            StreamReader sr = new StreamReader(stream, enc);
            
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


        private Task<Exception> WriteDataToExcelAsync(string targetFile, Dictionary<string,string[]> parsedValues, string[] columnNames)
        {
            return Task.Run(() => WriteDataToExcel(targetFile, parsedValues,columnNames));
        }

        private Exception WriteDataToExcel(string targetFile, Dictionary<string,string[]> parsedValues,string[] columnNames)
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


        private void Output(string msg,bool newLine = true)
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
