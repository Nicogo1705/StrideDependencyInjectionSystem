using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Stride.Core.Annotations;
using Stride.Engine;

namespace StrideDependencyInjectionSystem
{
    public class InjectionProcessor : EntityProcessor<ScriptComponent>
    {
        private InjectionService _injectionService;

        protected override void OnSystemAdd()
        {
            InjectionServicesHelper.SetGetAndConfigureServices(Services, out _injectionService, out _, e => e.Register(e.GetType(), e));
            base.OnSystemAdd();
        }

#warning Maybe add to "OnSystemAdd" a foreach of all ScriptComponent & set their Injection fields

        protected override void OnEntityComponentAdding(Entity entity, [NotNull] ScriptComponent component, [NotNull] ScriptComponent data)
        {
            var componentType = component.GetType();
            foreach (var item in componentType.GetProperties(BindingFlags.Instance | BindingFlags.Public))
            {
                var injectAttribute = item.GetCustomAttribute<InjectAttribute>();
                if (injectAttribute == null)
                    continue;

                if (injectAttribute.InjectionType == InjectionType.Static)
                    item.SetValue(component, _injectionService.GetStatic(item.PropertyType));
                else if (injectAttribute.InjectionType == InjectionType.dynamic)
                    item.SetValue(component, _injectionService.GetDynamic(item.PropertyType));
                else
                    throw new NotImplementedException($"{injectAttribute.InjectionType}");
            }
            base.OnEntityComponentAdding(entity, component, data);
        }

        protected override void OnEntityComponentRemoved(Entity entity, [NotNull] ScriptComponent component, [NotNull] ScriptComponent data)
        {

            var componentType = component.GetType();
            foreach (var item in componentType.GetProperties(BindingFlags.Instance | BindingFlags.Public))
            {
                var injectAttribute = item.GetCustomAttribute<InjectAttribute>();
                if (injectAttribute == null)
                    continue;

                item.SetValue(component, null);
            }
            base.OnEntityComponentRemoved(entity, component, data);
        }

    }
}
