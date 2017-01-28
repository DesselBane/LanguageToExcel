using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using ExcelExport.Contracts.Model;

namespace ExcelExport.Contracts.Services
{
    public interface IPropertiesFileService
    {
        /// <summary>
        /// Adds a File to be tracked as a Properties File
        /// </summary>
        /// <param name="fileInfo">File Path to the properties File</param>
        /// <returns>Returns an instance of IPropertiesFile</returns>
        /// <exception cref="ArgumentException">Thrown if there is a Properties file with the same FilePath</exception>
        IPropertiesFile AddPropertiesFile(FileInfo fileInfo);

        /// <summary>
        /// Removes a File from the tracked Property Files Collection
        /// </summary>
        /// <param name="file">The Propertyfile to remove</param>
        /// <exception cref="ArgumentException">Thrown if the file instance doesnt exist</exception>
        void RemovePropertiesFile(IPropertiesFile file);

        /// <summary>
        /// Returns all tracked Files
        /// </summary>
        /// <returns>Returns all tracked Files</returns>
        IEnumerable<IPropertiesFile> RegisteredFiles();

        /// <summary>
        /// Parses a File and returns a Dictionary of Key Value pairs
        /// </summary>
        /// <returns>Returns a Dictionary of Key Value pairs</returns>
        Task<IReadOnlyDictionary<string, string>> ParseFileAsync(IPropertiesFile propertiesFile);
    }
}
