using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrideDependencyInjectionSystem
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class InjectAttribute : Attribute
    {
        public InjectionType InjectionType { get; }

        public InjectAttribute(InjectionType injectionType = InjectionType.Static)
        {
            InjectionType = injectionType;
        }

    }

    public enum InjectionType
    {
        Static,
        dynamic
    }

}
