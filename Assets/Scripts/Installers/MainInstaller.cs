using LudumDare50.Client.Game;
using LudumDare50.Client.Game.Implementation;
using LudumDare50.Client.Infrastructure;
using LudumDare50.Client.Infrastructure.Implementation;
using LudumDare50.Client.Settings;
using UnityEngine;
using Zenject;

namespace LudumDare50.Client.Installers
{
    public class MainInstaller : MonoInstaller
    {
        [SerializeField]
        private ViewManager _viewManager;

        [Header("Settings")]
        [SerializeField]
        private TiredReasonSettings _tiredReasonSettings;
        [SerializeField]
        private SleepSettings _sleepSettings;

        public override void InstallBindings()
        {
            //Infrastructure
            Container.Bind<IScreenService>().To<ScreenService>().AsSingle().WithArguments(_viewManager);

            //Game
            Container.Bind(typeof(ISleepService), typeof(ITickable)).To<SleepService>().AsSingle();
            Container.Bind<IInventory>().To<Inventory>().AsSingle();
            Container.Bind<IGameTime>().To<GameTime>().AsSingle();
            Container.Bind(typeof(ITaskService), typeof(ITickable)).To<TaskService>().AsSingle();

            //Settings
            Container.Bind<TiredReasonSettings>().FromInstance(_tiredReasonSettings);
            Container.Bind<SleepSettings>().FromInstance(_sleepSettings);
        }
    }
}
