using LudumDare50.Client.Game;
using LudumDare50.Client.Game.Implementation;
using LudumDare50.Client.Infrastructure;
using LudumDare50.Client.Infrastructure.Implementation;
using UnityEngine;
using Zenject;

namespace LudumDare50.Client.Installers
{
    public class MainInstaller : MonoInstaller
    {
        [SerializeField]
        private ViewManager _viewManager;

        public override void InstallBindings()
        {
            //Infrastructure
            Container.Bind<IScreenService>().To<ScreenService>().AsSingle().WithArguments(_viewManager);

            //Game
            Container.Bind<ISleepService>().To<SleepService>().AsSingle();
        }
    }
}
