using Game;
using Game.InputSystem;
using Game.InventorySystem;
using UserInterface;
using Zenject;

public class MainInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container
            .BindInterfacesTo<UserInterfaceManager>()
            .FromComponentsInHierarchy()
            .AsSingle();
        
        Container
            .Bind<IPlayerContext>()
            .To<PlayerContext>()
            .AsSingle();
        Container
            .Bind<IInventory>()
            .To<Inventory>()
            .AsSingle();
        Container
            .Bind<IInputProcessor>()
            .To<InputProcessor>()
            .AsSingle();
    }
}