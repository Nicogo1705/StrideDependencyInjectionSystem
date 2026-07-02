# Stride Dependency Injection

Lightweight dependency injection for [Stride](https://www.stride3d.net/) scripts. Register your
types once, mark public `get`/`set` properties with `[Inject]`, and the library fills them in for
you — no service-locator boilerplate in every script.

## What's in the box

| File | Role |
|------|------|
| `InjectAttribute` | Marks a public settable property for injection (`Static` singleton or `Dynamic` transient). |
| `InjectionService` | The type → instance registry: `Register` / `Register<T>` / `IsRegistered`. |
| `InjectionServicesHelper` | One-call setup: get/create the service, register your bindings, attach the processor. |
| `InjectionProcessor` | Fills every `[Inject]` property as scripts enter the scene, clears them on exit. |

## Quick start

**1 — Register your services once** (e.g. from a `StartupScript`):

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
    [Inject, DataMemberIgnore] public WeaponDataProvider? Weapons { get; set; }
    [Inject, DataMemberIgnore] public int Damage { get; set; }

    public override void Update()
        => DebugText.Print($"{Damage} dmg via {Weapons?.ProviderUrl}", new(10, 10));
}
```

> Injected properties must be **public with both a getter and a setter**. `[DataMemberIgnore]`
> keeps them out of the Game Studio inspector.

## Demo

Open `StrideDependencyInjectionSystem.sln`, set **Demo.Windows** as the startup project and run.
The `Sword` script prints its injected values on screen — proof the library wired them up.

## License

MIT. See [LICENSE.md](LICENSE.md).
