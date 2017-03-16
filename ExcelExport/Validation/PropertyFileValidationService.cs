using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using ExcelExport.Contracts.Model;
using ExcelExport.Contracts.Services;
using Microsoft.Practices.Unity;

namespace ExcelExport.Validation
{
    public class PropertyFileValidationService : IPropertyFileValidationService
    {
        private IUnityContainer _container;

        public PropertyFileValidationService(IUnityContainer container)
        {
            _container = container;
        }

        #region Implementation of IPropertyFileValidationService

        public async Task<IEnumerable<ValidationResult>> ValidateFile(IPropertiesFile propertiesFile)
        {
            var validationRules = _container.ResolveAll<IValidationRule<IPropertiesFile>>();
            List<ValidationResult> results = new List<ValidationResult>();

            foreach (IValidationRule<IPropertiesFile> validationRule in validationRules)
            {
                try
                {
                    results.AddRange(await validationRule.ValidateAsync(propertiesFile));
                }
                catch (Exception e)
                {
                    return new[] {new ValidationResult(e.Message)};
                }
            }

            return results;
        }

        #endregion
    }
}
