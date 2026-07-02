using System;
using Stride.Core;
using Stride.Core.Mathematics;
using Stride.Engine;
using Stride.Input;
using StrideDependencyInjectionSystem;

namespace Demo
{

    public class Sword : SyncScript
    {
        [Inject]
        [DataMemberIgnore]
        public WeaponDataProvider? Wdp { get; set; } = null;

        [Inject]
        [DataMemberIgnore]
        public int Val { get; set; } = 0;

        public override void Update()
        {
            // These fields were never set in code — they are filled by the DI library
            // from [Inject]. Showing them on screen proves the injection resolved.
            DebugText.Print("Stride Dependency Injection — resolved via [Inject]:", new Int2(20, 20));
            DebugText.Print($"[Inject] int Val            = {Val}", new Int2(28, 44));
            DebugText.Print($"[Inject] Wdp.ProviderUrl    = {Wdp?.ProviderUrl ?? "<null>"}", new Int2(28, 64));
            DebugText.Print($"[Inject] Wdp.GetDamage(33)  = {(Wdp is null ? "<null>" : Wdp.GetDamage(33).ToString())}", new Int2(28, 84));
        }
    }

}
