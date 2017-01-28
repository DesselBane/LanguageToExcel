using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExcelExport.Contracts.Model;

namespace ExcelExport.Contracts.Services
{
    public interface IPropertyFileValidationService
    {
        /// <summary>
        /// Validates the PropertiesFile against all registered ValidationRules
        /// </summary>
        /// <param name="propertiesFile">The PropertiesFile to validate</param>
        /// <returns>Returns an Enumerable of Validation Results. This enumerable will be empty if the validation succeeded</returns>
        Task<IEnumerable<ValidationResult>> ValidateFile(IPropertiesFile propertiesFile);
    }
}
