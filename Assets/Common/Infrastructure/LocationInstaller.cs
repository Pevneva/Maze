using UnityEngine;
using Zenject;

namespace Common.Infrastructure
{
    public class LocationInstaller : MonoInstaller
    {
        public Player Player;
        public ScreenUiPanel ScreenUiPanel;
        public Transform Finish;
        
        public override void InstallBindings()
        {
            BindPlayer();
            BindScreenUiPanel();
            BindFinishTransform();
        }

        private void BindFinishTransform()
        {
            Container
                .Bind<Transform>()
                .FromInstance(Finish)
                .AsSingle();
        }

        private void BindScreenUiPanel()
        {
            Container
                .Bind<ScreenUiPanel>()
                .FromInstance(ScreenUiPanel)
                .AsSingle();
        }

        private void BindPlayer()
        {
            Container
                .Bind<Player>()
                .FromInstance(Player)
                .AsSingle();
        }
    }
}