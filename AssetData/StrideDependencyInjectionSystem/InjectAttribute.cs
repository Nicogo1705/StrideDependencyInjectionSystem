using System;

namespace StrideDependencyInjectionSystem
{
    /// <summary>
    /// Marks a public, settable property of a <see cref="Stride.Engine.ScriptComponent"/> to be
    /// filled automatically by the <see cref="InjectionProcessor"/> from the values registered on
    /// the <see cref="InjectionService"/>. The property is set when the component is added to the
    /// scene and cleared when it is removed.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class InjectAttribute : Attribute
    {
        /// <summary>Whether the injected value is a shared singleton or a fresh instance per component.</summary>
        public InjectionType InjectionType { get; }

        public InjectAttribute(InjectionType injectionType = InjectionType.Static)
        {
            InjectionType = injectionType;
        }
    }

    /// <summary>How an <see cref="InjectAttribute"/> value is resolved.</summary>
    public enum InjectionType
    {
        /// <summary>One shared instance is reused for every injection of the type (singleton).</summary>
        Static,

        /// <summary>A fresh instance is created for each injection of the type (transient).</summary>
        Dynamic,
    }
}
