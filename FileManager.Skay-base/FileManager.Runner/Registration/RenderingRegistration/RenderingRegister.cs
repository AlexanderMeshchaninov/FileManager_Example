using Autofac;
using FileManager.CommonLogic.Rendering;
using FileManager.Core.Rendering;

namespace FileManager.Runner.Registration.RenderingRegistration
{
    public static class RenderingRegister
    {
        public static void RegisterRendering(this ContainerBuilder builder)
        {
            builder
                .RegisterType<Rendering>()
                .As<IRendering>()
                .SingleInstance();
        }
    }
}