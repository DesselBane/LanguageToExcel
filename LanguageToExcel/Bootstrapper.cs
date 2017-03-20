using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Common.Wpf.Services;
using Contracts.Presentation;
using MaterialDesignThemes.Wpf;
using Prism.Modularity;
using Prism.Unity;
using Microsoft.Practices.Unity;

namespace LanguageToExcel
{
    public class Bootstrapper : UnityBootstrapper
    {
        #region Overrides of UnityBootstrapper

        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();

            //Adding Application Services
            Container.RegisterType<IFileService, WpfFileService>();
        }

        protected override IModuleCatalog CreateModuleCatalog()
        {
            var catalog = new DirectoryModuleCatalog();

            if (!Directory.Exists(@".\Modules"))
                Directory.CreateDirectory(@".\Modules");

            catalog.ModulePath = @".\Modules";

            return catalog;
        }


        protected override DependencyObject CreateShell()
        {
            return new MainWindow();
        }

        protected override void InitializeShell()
        {
            base.InitializeShell();

            (Shell as MainWindow)?.Show();
        }

        #endregion
    }
}
