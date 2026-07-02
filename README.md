# Stride Dependency Injection

Lightweight dependency injection for [Stride](https://stride3d.net) scripts. Register your types
once, mark public `get`/`set` properties with `[Inject]`, and the library fills them in for you —
no service-locator boilerplate in every script.

## Usage

**1 — Register your services once** (e.g. from a `StartupScript` in your scene):

```csharp
using StrideDependencyInjectionSystem;

public class GameServices : StartupScript
{
    public override void Start()
    {
        InjectionServicesHelper.SetGetAndConfigureServices(Services, out _, out _, e =>
        {
            e.Register(typeof(int), 42);
            e.Register(typeof(WeaponDataProvider), new WeaponDataProvider { ProviderUrl = "127.0.0.1" });
        });
    }
}
```

**2 — Inject into any script** — mark a public `get`/`set` property with `[Inject]`:

```csharp
using StrideDependencyInjectionSystem;

public class Sword : SyncScript
{
    [Inject, DataMemberIgnore]
    public WeaponDataProvider? Weapons { get; set; }

    [Inject, DataMemberIgnore]
    public int Damage { get; set; }

    public override void Update()
        => DebugText.Print($"{Damage} dmg via {Weapons?.ProviderUrl}", new(10, 10));
}
```

> Injected properties must be **public with both a getter and a setter**. `[DataMemberIgnore]`
> keeps them out of the Game Studio inspector.

## License

MIT.
