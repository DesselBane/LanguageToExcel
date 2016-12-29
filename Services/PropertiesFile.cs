using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Contracts.DomainModels;

namespace Services
{
    public class PropertiesFile : IPropertiesFile
    {
        #region Const

        private static readonly string _linePattern = "[^=]+=[^=]+";

        #endregion

        #region Vars

        private IReadOnlyDictionary<string, string> _data;
        private Dictionary<string, string> _dataWriteable = new Dictionary<string, string>();
        private FileInfo _filePath;
        private string _language;

        #endregion

        #region Properties

        public FileInfo FilePath
        {
            get { return _filePath; }
            set { _filePath = value; }
        }

        public string Language
        {
            get { return _language; }
            set { _language = value; }
        }

        public IReadOnlyDictionary<string, string> Data => _data;

        private Dictionary<string, string> DataWriteable
        {
            get { return _dataWriteable; }
            set
            {
                _dataWriteable = value;
                _data = new ReadOnlyDictionary<string, string>(_dataWriteable);
            }
        }

        #endregion

        public void ReadData()
        {
            if (FilePath == null)
                throw new ValidationException("No File selected");
            if (!FilePath.Exists)
                throw new ValidationException("File not found");

            DataWriteable = new Dictionary<string, string>();

            try
            {
                var streamReader = new StreamReader(FilePath.OpenRead());
                int counter = 0;
                string line;

                while ((line = streamReader.ReadLine()) != null)
                {
                    if (Regex.IsMatch(line, _linePattern))
                    {
                        var strings = line.Split('=');

                        if (_dataWriteable.ContainsKey(strings[0]))
                            throw new ValidationException($"Line {counter}: Duplicate Key");
                        else
                        {
                            _dataWriteable.Add(strings[0], strings[1]);
                        }
                    }
                    else
                    {
                        throw new ValidationException($"Line {counter}: Invalid line format");
                    }
                    counter++;
                }
            }
            catch (UnauthorizedAccessException uae)
            {
                throw new ValidationException("Path is a directory", uae);
            }
            catch (IOException ioe)
            {
                throw new ValidationException("The file is already Open", ioe);
            }
            catch (Exception ex)
            {
                throw new ValidationException("An unknown Error ocurred",ex);
            }
        }

        public Task ReadDataAsync()
        {
            return Task.Run(() => ReadData());
        }
    }
}