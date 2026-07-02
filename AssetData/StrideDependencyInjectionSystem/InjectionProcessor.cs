using System;
using System.Reflection;
using Stride.Core.Annotations;
using Stride.Engine;

namespace StrideDependencyInjectionSystem
{
    /// <summary>
    /// Scans every <see cref="ScriptComponent"/> as it enters the scene, fills its
    /// <see cref="InjectAttribute"/>-marked properties from the <see cref="InjectionService"/>,
    /// and clears them when the component leaves.
    /// </summary>
    public class InjectionProcessor : EntityProcessor<ScriptComponent>
    {
        private InjectionService _injectionService = null!;

        protected override void OnSystemAdd()
        {
            InjectionServicesHelper.SetGetAndConfigureServices(Services, out _injectionService, out _, e => e.Register(e.GetType(), e));
            base.OnSystemAdd();
        }

        protected override void OnEntityComponentAdding(Entity entity, [NotNull] ScriptComponent component, [NotNull] ScriptComponent data)
        {
            foreach (var property in component.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public))
            {
                var injectAttribute = property.GetCustomAttribute<InjectAttribute>();
                if (injectAttribute == null || !property.CanWrite)
                    continue;

                object? value = injectAttribute.InjectionType switch
                {
                    InjectionType.Static => _injectionService.GetStatic(property.PropertyType),
                    InjectionType.Dynamic => _injectionService.GetDynamic(property.PropertyType),
                    _ => throw new NotImplementedException($"{injectAttribute.InjectionType}"),
                };
                property.SetValue(component, value);
            }
            base.OnEntityComponentAdding(entity, component, data);
        }

        protected override void OnEntityComponentRemoved(Entity entity, [NotNull] ScriptComponent component, [NotNull] ScriptComponent data)
        {
            foreach (var property in component.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public))
            {
                if (property.GetCustomAttribute<InjectAttribute>() == null || !property.CanWrite)
                    continue;

                property.SetValue(component, null);
            }
            base.OnEntityComponentRemoved(entity, component, data);
        }
    }
}
