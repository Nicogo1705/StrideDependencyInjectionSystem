using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stride.Core;
using Stride.Engine;
using Stride.Engine.Design;
using Stride.Games;

namespace StrideDependencyInjectionSystem
{
    public static class InjectionServicesHelper
    {

        public static void SetGetAndConfigureServices(IServiceRegistry services, out InjectionService injectionService, out InjectionProcessor injectionProcessor, Action<InjectionService>? configure = null)
        {
            injectionService = services.GetService<InjectionService>();
            if (injectionService == null)
            {
                injectionService = new();
                services.AddService(injectionService);
            }
            configure?.Invoke(injectionService);

            var sceneSystem = services.GetService<SceneSystem>();
            injectionProcessor = sceneSystem.SceneInstance.Processors.Get<InjectionProcessor>();
            if (injectionProcessor == null)
            {
                injectionProcessor = new InjectionProcessor();
                sceneSystem.SceneInstance.Processors.Add(injectionProcessor);
            }
        }
    }
}
