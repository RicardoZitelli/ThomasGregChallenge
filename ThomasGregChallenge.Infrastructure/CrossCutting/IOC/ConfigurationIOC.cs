using Autofac;
using FluentValidation;
using ThomasGregChallenge.Application.DTOs.Requests;
using ThomasGregChallenge.Application.Interfaces.Services;
using ThomasGregChallenge.Application.Services;
using ThomasGregChallenge.Application.Validators;
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
            ConfigureRepositories(builder);

            ConfigureServices(builder);

            ConfigureApplication(builder);

            ConfigureValidators(builder);
        }

        private static void ConfigureValidators(ContainerBuilder builder)
        {
            builder
                .RegisterType<ClienteLogradouroRequestDtoValidator>()
                .As<IValidator<ClienteLogradouroRequestDto>>()
                .InstancePerLifetimeScope();

            builder
                .RegisterType<ClienteRequestDtoValidator>()
                .As<IValidator<ClienteRequestDto>>()
                .InstancePerLifetimeScope();

            builder
                .RegisterType<LogradouroRequestDtoValidator>()
                .As<IValidator<LogradouroRequestDto>>()
                .InstancePerLifetimeScope();
        }

        private static void ConfigureApplication(ContainerBuilder builder)
        {
            builder
                .RegisterType<ClienteApplicationService>()
                .As<IClienteApplicationService>()
                .InstancePerLifetimeScope(); ;

            builder
                .RegisterType<LogradouroApplicationService>()
                .As<ILogradouroApplicationService>()
                .InstancePerLifetimeScope(); ;
        }

        private static void ConfigureServices(ContainerBuilder builder)
        {
            builder
                .RegisterType<ClienteService>()
                .As<IClienteService>()
                .InstancePerLifetimeScope(); ;

            builder
                .RegisterType<LogradouroService>()
                .As<ILogradouroService>()
                .InstancePerLifetimeScope(); ;
        }

        private static void ConfigureRepositories(ContainerBuilder builder)
        {
            builder
                .RegisterType<ClienteRepository>()
                .As<IClienteRepository>()
                .InstancePerLifetimeScope(); ;

            builder
                .RegisterType<LogradouroRepository>()
                .As<ILogradouroRepository>()
                .InstancePerLifetimeScope(); ;            
        }
    }
}