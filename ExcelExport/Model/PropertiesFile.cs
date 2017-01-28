using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ExcelExport.Contracts;
using ExcelExport.Contracts.Model;

namespace ExcelExport.Model
{
    public class PropertiesFile : IPropertiesFile
    {
        #region Const

        private static readonly string _linePattern = "[^=]+=[^=]+";

        #endregion

        #region Vars

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



        #endregion

    
    }
}