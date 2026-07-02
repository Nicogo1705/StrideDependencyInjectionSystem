using System;
using Stride.Core;
using Stride.Engine;

namespace StrideDependencyInjectionSystem
{
    /// <summary>
    /// One-call setup for the injection system: gets (or creates) the shared
    /// <see cref="InjectionService"/>, lets you register your bindings via
    /// <paramref name="configure"/>, and makes sure the <see cref="InjectionProcessor"/> is
    /// attached to the current scene.
    /// </summary>
    public static class InjectionServicesHelper
    {
        /// <summary>
        /// Ensures the injection service + processor exist and runs <paramref name="configure"/>
        /// against the service so you can register your bindings.
        /// </summary>
        /// <param name="services">The game service registry (e.g. <c>Services</c> in a script).</param>
        /// <param name="injectionService">The shared injection service.</param>
        /// <param name="injectionProcessor">The scene's injection processor.</param>
        /// <param name="configure">Optional callback to register bindings on the service.</param>
        public static void SetGetAndConfigureServices(
            IServiceRegistry services,
            out InjectionService injectionService,
            out InjectionProcessor injectionProcessor,
            Action<InjectionService>? configure = null)
        {
            injectionService = services.GetService<InjectionService>();
            if (injectionService == null)
            {
                injectionService = new InjectionService();
                services.AddService(injectionService);
            }

            configure?.Invoke(injectionService);

            var sceneSystem = services.GetService<SceneSystem>()
                ?? throw new InvalidOperationException("No SceneSystem is registered; call this once the scene is running (e.g. from a StartupScript).");

            injectionProcessor = sceneSystem.SceneInstance.Processors.Get<InjectionProcessor>();
            if (injectionProcessor == null)
            {
                injectionProcessor = new InjectionProcessor();
                sceneSystem.SceneInstance.Processors.Add(injectionProcessor);
            }
        }
    }
}
