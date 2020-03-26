using System.Windows;
using AsketHypertext.Services;
using Autofac;

namespace AsketHypertext
{
    public partial class App : Application
    {
        public static IContainer Container { get; private set; }

        public App()
        {
            Container = SetUpContainer();
        }

        private IContainer SetUpContainer()
        {
            var builder = new ContainerBuilder();
            RegisterTypes(builder);
            var container = builder.Build();
            return container;
        }

        private void RegisterTypes(ContainerBuilder builder)
        {
            builder.RegisterType<SourcesLoader>().As<ISourcesLoader>();
            builder.RegisterType<AsketParser>().As<IAsketParser>();
        }
    }
}
