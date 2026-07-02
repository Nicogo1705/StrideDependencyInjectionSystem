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
            Console.WriteLine($"val : {Val}\nWdp : {Wdp.GetDamage(33)}");
        }
    }

}
