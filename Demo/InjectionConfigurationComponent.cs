using System;
using Stride.Core;
using Stride.Core.Mathematics;
using Stride.Engine;
using Stride.Input;
using StrideDependencyInjectionSystem;

namespace Demo
{
    public class InjectionConfigurationComponent : StartupScript
    {

        public override void Start()
        {
            InjectionServicesHelper.SetGetAndConfigureServices(Services, out _, out _, (e) =>
            {
                e.Register(typeof(int), -1);
                e.Register(typeof(WeaponDataProvider), new WeaponDataProvider() { ProviderUrl = "127.0.0.2" });
            });
        }

    }

    public class WeaponDataProvider
    {
        public string ProviderUrl = "http://127.0.0.1";

        public int GetDamage(int index)
        {
            return index * 2; //Load in from BDD, web request, blockchain,..
        }

    }

}
