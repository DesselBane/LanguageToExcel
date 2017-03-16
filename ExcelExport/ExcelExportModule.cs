using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts;
using ExcelExport.Contracts;
using ExcelExport.Contracts.Services;
using ExcelExport.Controls;
using ExcelExport.Files;
using ExcelExport.Infrastructure;
using ExcelExport.Validation;
using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Regions;
using Services;

namespace ExcelExport
{
    public class ExcelExportModule : IModule
    {
        private IUnityContainer _container;
        private IRegionManager _regionManager;

        public ExcelExportModule(IUnityContainer container, IRegionManager regionManager)
        {
            _container = container;
            _regionManager = regionManager;
        }

        #region Implementation of IModule

        public void Initialize()
        {
            Register();
            AddViews();
        }

        #endregion

        private void Register()
        {
            _container.RegisterType<Factory>(new ContainerControlledLifetimeManager());
            _container.RegisterType<IPropertiesFileService, PropertiesFileService>(new ContainerControlledLifetimeManager());
            _container.RegisterType<IPropertyFileValidationService, PropertyFileValidationService>(new ContainerControlledLifetimeManager());
            _container.RegisterType<IExcelExportService, ExcelExportService>(new ContainerControlledLifetimeManager());
        }

        private void AddViews()
        {
            _regionManager.RegisterViewWithRegion(RegionNames.LeftMainArea, () => _container.Resolve<PropertiesFilesListView>());
            _regionManager.RegisterViewWithRegion(RegionNames.ControlArea, () => _container.Resolve<ControlsView>());
        }
    }
}
