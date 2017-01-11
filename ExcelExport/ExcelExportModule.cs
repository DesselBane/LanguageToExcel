using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExcelExport.ViewModels;
using Microsoft.Practices.Unity;
using Prism.Modularity;

namespace ExcelExport
{
    public class ExcelExportModule : IModule
    {
        private IUnityContainer _container;

        public ExcelExportModule(IUnityContainer container)
        {
            _container = container;
        }

        #region Implementation of IModule

        public void Initialize()
        {
            Register();
        }

        #endregion

        private void Register()
        {
            _container.RegisterType<MainViewModel>(new ContainerControlledLifetimeManager());
        }
    }
}
