using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelExport.Contracts.Model
{
    public interface IValidationRule<in TEntity>
    {
        Task<IEnumerable<ValidationResult>> ValidateAsync(TEntity entity);
    }
}
