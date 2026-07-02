using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrideDependencyInjectionSystem
{
    public class InjectionService
    {
        private Dictionary<Type, object?> _typesToStatic = new();

        public void Register(Type type, object? staticInstance = null)
        {
            _typesToStatic.Add(type, staticInstance);
        }

        internal object? GetStatic(Type type)
        {
            if (_typesToStatic.TryGetValue(type, out object? staticInstance))
            {
                if (staticInstance == null)
                {
                    staticInstance = Activator.CreateInstance(type);
                    _typesToStatic[type] = staticInstance;
                }
                return staticInstance;
            }
            return null;
        }
        internal object? GetDynamic(Type type)
        {
            if (_typesToStatic.TryGetValue(type, out object? staticInstance))
            {
                var dynInstance = Activator.CreateInstance(type);
                return dynInstance;
            }
            return null;
        }

    }
}
