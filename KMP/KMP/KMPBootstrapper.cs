using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.Logging;
using Microsoft.Practices.Prism.MefExtensions;
using Microsoft.Practices.Prism.Modularity;
using System.ComponentModel.Composition.Hosting;
using System.Windows;
using Infranstructure.Behaviors;

namespace KMP
{
    public class KMPBootstrapper : MefBootstrapper
    {
        private readonly LoggerAdapter _logger = new LoggerAdapter();

        protected override ILoggerFacade CreateLogger()
        {
            return _logger;
        }

        protected override DependencyObject CreateShell()
        {
            return this.Container.GetExportedValue<TestWindow>();
        }

        protected override void InitializeShell()
        {
            base.InitializeShell();

            Application.Current.MainWindow = (Window)this.Shell;
            Application.Current.MainWindow.Show();


        }

        protected override void ConfigureAggregateCatalog()
        {
            base.ConfigureAggregateCatalog();
            this.AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(KMPBootstrapper).Assembly));
            // 添加Common 模块
            DirectoryCatalog catalog = new DirectoryCatalog("Common");
            this.AggregateCatalog.Catalogs.Add(catalog);
            // 添加Modules 模块
            catalog = new DirectoryCatalog("Module");
            this.AggregateCatalog.Catalogs.Add(catalog);
            // 添加Services 模块
            catalog = new DirectoryCatalog("Service");
            this.AggregateCatalog.Catalogs.Add(catalog);
        }

        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();

            // Because we created the CallbackLogger and it needs to be used immediately, we compose it to satisfy any imports it has.

        }
        protected override Microsoft.Practices.Prism.Regions.IRegionBehaviorFactory ConfigureDefaultRegionBehaviors()
        {
            var factory = base.ConfigureDefaultRegionBehaviors();

            factory.AddIfMissing("AutoPopulateExportedViewsBehavior", typeof(AutoPopulateExportedViewsBehavior));

            return factory;
        }
        protected override IModuleCatalog CreateModuleCatalog()
        {
            // When using MEF, the existing Prism ModuleCatalog is still the place to configure modules via configuration files.
            return new ConfigurationModuleCatalog();
        }
    }
}
