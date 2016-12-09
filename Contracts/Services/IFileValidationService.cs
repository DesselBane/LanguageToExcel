using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts.DomainModels;

namespace Contracts.Services
{
    public interface IFileValidationService
    {
        Task<IEnumerable<ValidationResult>> ValidateFileAsync(IPropertiesFile file);
    }
}
