using Autofac;
using ThomasGregChallenge.Application.Interfaces.Services;
using ThomasGregChallenge.Application.Services;
using ThomasGregChallenge.Domain.Interfaces.Repositories;
using ThomasGregChallenge.Domain.Interfaces.Services;
using ThomasGregChallenge.Domain.Services;
using ThomasGregChallenge.Infrastructure.Data.Repositories;

namespace ThomasGregChallenge.Infrastructure.CrossCutting.IOC
{
    public class ConfigurationIOC
    {
        public static void Load(ContainerBuilder builder)
        {
            ConfigureApplication(builder);

            ConfigureServices(builder);

            ConfigureRepositories(builder);
        }

        private static void ConfigureApplication(ContainerBuilder builder)
        {
            builder.RegisterType<ClienteApplicationService>().As<IClienteApplicationService>();

            builder.RegisterType<LogradouroApplicationService>().As<ILogradouroApplicationService>();
        }

        private static void ConfigureServices(ContainerBuilder builder)
        {
            builder.RegisterType<ClienteService>().As<IClienteService>();

            builder.RegisterType<LogradouroService>().As<ILogradouroService>();
        }

        private static void ConfigureRepositories(ContainerBuilder builder)
        {
            builder.RegisterType<ClienteRepository>().As<IClienteRepository>();

            builder.RegisterType<LogradouroRepository>().As<ILogradouroRepository>();            
        }
    }
}