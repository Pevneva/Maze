using Zenject;

namespace Common.Infrastructure
{
    public class LocationInstaller : MonoInstaller
    {
        public Player Player;
        
        public override void InstallBindings()
        {
            Container
                .Bind<Player>()
                .FromInstance(Player)
                .AsSingle();
        }
    }
}