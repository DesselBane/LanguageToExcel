using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts;
using ExcelExport.Contracts;
using ExcelExport.Model;
using ExcelExport.ViewModels;
using ExcelExport.Views;
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
            _container.RegisterType<IExcelExportService, ExcelExportService>(new ContainerControlledLifetimeManager());
            _container.RegisterType<IExportPropertiesFileService, PropertiesFileService>(new ContainerControlledLifetimeManager());
        }

        private void AddViews()
        {
            //TODO fix regions
            //_regionManager.RegisterViewWithRegion(ExcelRegionNames.ControlView, typeof(ControlView));
            //_regionManager.RegisterViewWithRegion(ExcelRegionNames.PropertyFileView, typeof(PropertiesFilesListView));
        }
    }
}
