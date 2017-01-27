using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts.Presentation;
using Microsoft.Win32;

namespace Common.Wpf.Services
{
    public class WpfFileService : IFileService
    {
        #region Implementation of IFileService

        public FileInfo OpenFile(IFileServiceOptions options = null)
        {
            var openWin = new OpenFileDialog
            {
                DefaultExt = options?.DefaultExtensions,
                Filter = options?.Filter,
                DereferenceLinks = options?.DereferenceLinks ?? true,
                InitialDirectory = options?.InitialDirectory,
                Title = options?.Title ?? "Open File"
            };

            bool? result = openWin.ShowDialog();

            if (result != null && result == true)
            {
                return new FileInfo(openWin.FileName);
            }

            return null;
        }

        public FileInfo SaveFile(IFileServiceOptions options = null)
        {
            var saveWin = new SaveFileDialog
            {
                DefaultExt = options?.DefaultExtensions,
                Filter = options?.Filter,
                DereferenceLinks = options?.DereferenceLinks ?? true,
                InitialDirectory = options?.InitialDirectory,
                Title = options?.Title ?? "Save File"
            };


            bool? res = saveWin.ShowDialog();

            return res == true ? new FileInfo(saveWin.FileName) : null;
        }

        #endregion
    }
}
