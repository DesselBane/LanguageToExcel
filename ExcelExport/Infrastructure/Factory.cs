using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExcelExport.Contracts.Model;
using ExcelExport.Files;
using Microsoft.Practices.Unity;

namespace ExcelExport.Infrastructure
{
    public sealed class Factory
    {
        private IUnityContainer _container;

        public Factory(IUnityContainer container)
        {
            _container = container;
        }

        public FileViewModel CreateFileViewModel(params ResolverOverride[] overrides)
        {
            return _container.Resolve<FileViewModel>(overrides);
        }
    }
}
