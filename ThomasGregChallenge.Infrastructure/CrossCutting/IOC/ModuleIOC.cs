using Autofac;

namespace ThomasGregChallenge.Infrastructure.CrossCutting.IOC
{
    public sealed class ModuleIOC : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            ConfigurationIOC.Load(builder);
        }
        
    }
}
