using System;
using System.Collections.Generic;
using Autofac;
using Common.Logging;
using ScriptCs.Contracts;
using ScriptCs.Hosting.Package;

namespace ScriptCs.Hosting
{
    public class InitializationServices : ScriptServicesRegistration, IInitializationServices
    {
        public InitializationServices(ILog logger, IDictionary<Type, object> overrides = null)
            : base(logger, overrides)
        {
        }

        protected override IContainer CreateContainer()
        {
        	Console.WriteLine("1");
            var builder = new ContainerBuilder();
            Console.WriteLine("2");
            this.Logger.Debug("Registering initialization services");
            Console.WriteLine("3");
            builder.RegisterInstance<ILog>(this.Logger);
            Console.WriteLine("4");
            builder.RegisterType<ScriptServicesBuilder>().As<IScriptServicesBuilder>();
            Console.WriteLine("5");
            RegisterOverrideOrDefault<IFileSystem>(builder, b => b.RegisterType<FileSystem>().As<IFileSystem>().SingleInstance());
            Console.WriteLine("6");
            RegisterOverrideOrDefault<IAssemblyUtility>(builder, b => b.RegisterType<AssemblyUtility>().As<IAssemblyUtility>().SingleInstance());
            Console.WriteLine("7");
            RegisterOverrideOrDefault<IPackageContainer>(builder, b => b.RegisterType<PackageContainer>().As<IPackageContainer>().SingleInstance());
            Console.WriteLine("8");
            RegisterOverrideOrDefault<IPackageAssemblyResolver>(builder, b => b.RegisterType<PackageAssemblyResolver>().As<IPackageAssemblyResolver>().SingleInstance());
            Console.WriteLine("9");
            RegisterOverrideOrDefault<IAssemblyResolver>(builder, b => b.RegisterType<AssemblyResolver>().As<IAssemblyResolver>().SingleInstance());
            Console.WriteLine("A");
            RegisterOverrideOrDefault<IInstallationProvider>(builder, b => b.RegisterType<NugetInstallationProvider>().As<IInstallationProvider>().SingleInstance());
            Console.WriteLine("B");
            RegisterOverrideOrDefault<IPackageInstaller>(builder, b => b.RegisterType<PackageInstaller>().As<IPackageInstaller>().SingleInstance());
            Console.WriteLine("C");
            RegisterOverrideOrDefault<IModuleLoader>(builder, b => b.RegisterType<ModuleLoader>().As<IModuleLoader>().SingleInstance());
            Console.WriteLine("D");
            RegisterOverrideOrDefault<IAppDomainAssemblyResolver>(builder, b => b.RegisterType<AppDomainAssemblyResolver>().As<IAppDomainAssemblyResolver>().SingleInstance());
            Console.WriteLine("E");
            return builder.Build();
        }

        private IAssemblyResolver _assemblyResolver;

        public IAssemblyResolver GetAssemblyResolver()
        {
            if (_assemblyResolver == null)
            {
                this.Logger.Debug("Resolving AssemblyResolver");
                _assemblyResolver = Container.Resolve<IAssemblyResolver>();
            }

            return _assemblyResolver;
        }

        private IModuleLoader _moduleLoader;

        public IModuleLoader GetModuleLoader()
        {
            if (_moduleLoader == null)
            {
                this.Logger.Debug("Resolving ModuleLoader");
                _moduleLoader = Container.Resolve<IModuleLoader>();
            }

            return _moduleLoader;
        }

        private IFileSystem _fileSystem;

        public IFileSystem GetFileSystem()
        {
            if (_fileSystem == null)
            {
            	
                this.Logger.Debug("Resolving FileSystem");
                _fileSystem = Container.Resolve<IFileSystem>();
            }

            return _fileSystem;
        }

        private IInstallationProvider _installationProvider;

        public IInstallationProvider GetInstallationProvider()
        {
            if (_installationProvider == null)
            {
                this.Logger.Debug("Resolving Installation Provider");
                _installationProvider = Container.Resolve<IInstallationProvider>();
            }

            return _installationProvider;
        }

        private IPackageAssemblyResolver _packageAssemblyResolver;

        public IPackageAssemblyResolver GetPackageAssemblyResolver()
        {
            if (_packageAssemblyResolver == null)
            {
                this.Logger.Debug("Resolving Package Assembly Resolver");
                _packageAssemblyResolver = Container.Resolve<IPackageAssemblyResolver>();
            }

            return _packageAssemblyResolver;
        }

        private IPackageInstaller _packageInstaller;

        public IPackageInstaller GetPackageInstaller()
        {
            if (_packageInstaller == null)
            {
                this.Logger.Debug("Resolving Package Installer");
                _packageInstaller = Container.Resolve<IPackageInstaller>();
            }

            return _packageInstaller;
        }

        private IAppDomainAssemblyResolver _appDomainAssemblyResolver;

        public IAppDomainAssemblyResolver GetAppDomainAssemblyResolver()
        {
            if (_appDomainAssemblyResolver == null)
            {
                this.Logger.Debug("Resolving App Domain Assembly Resolver");
                _appDomainAssemblyResolver = Container.Resolve<IAppDomainAssemblyResolver>();
            }

            return _appDomainAssemblyResolver;
        }
    }
}