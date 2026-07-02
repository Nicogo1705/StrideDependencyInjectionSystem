using System;
using System.Collections.Generic;

namespace StrideDependencyInjectionSystem
{
    /// <summary>
    /// A tiny type → instance registry backing the <see cref="InjectAttribute"/>. Register a value
    /// for a type, then any public settable property marked <c>[Inject]</c> of that type is filled
    /// automatically by <see cref="InjectionProcessor"/>.
    /// </summary>
    public class InjectionService
    {
        private readonly Dictionary<Type, object?> _registrations = new();

        /// <summary>
        /// Registers (or replaces) the value returned for <paramref name="type"/>. Pass
        /// <c>null</c> to have a singleton lazily created from a public parameterless constructor
        /// on first use. Safe to call more than once for the same type (last registration wins).
        /// </summary>
        public void Register(Type type, object? instance = null) => _registrations[type] = instance;

        /// <summary>Generic convenience overload of <see cref="Register(Type, object?)"/>.</summary>
        public void Register<T>(T instance) => _registrations[typeof(T)] = instance;

        /// <summary>Returns whether a value is registered for <paramref name="type"/>.</summary>
        public bool IsRegistered(Type type) => _registrations.ContainsKey(type);

        /// <summary>
        /// Returns the shared (singleton) instance for the type, lazily creating it from a public
        /// parameterless constructor if it was registered as <c>null</c>. Returns <c>null</c> if
        /// the type was never registered.
        /// </summary>
        internal object? GetStatic(Type type)
        {
            if (!_registrations.TryGetValue(type, out var instance))
                return null;

            if (instance == null)
            {
                instance = Activator.CreateInstance(type);
                _registrations[type] = instance;
            }
            return instance;
        }

        /// <summary>
        /// Returns a fresh (transient) instance of the type on every call, provided the type was
        /// registered. Returns <c>null</c> if the type was never registered.
        /// </summary>
        internal object? GetDynamic(Type type)
            => _registrations.ContainsKey(type) ? Activator.CreateInstance(type) : null;
    }
}
