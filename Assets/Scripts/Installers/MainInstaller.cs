using LudumDare50.Client.Game;
using LudumDare50.Client.Game.Implementation;
using LudumDare50.Client.Infrastructure;
using LudumDare50.Client.Infrastructure.Implementation;
using LudumDare50.Client.Settings;
using LudumDare50.Client.ViewModels.ClickABunch;
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
        [SerializeField]
        private StatusEffectSettings _statusEffectSettings;
        [SerializeField]
        private MiniGameSpamPopupsSettings _miniGameSpamPopupsSettings;
        [SerializeField]
        private EnterPasswordSettings _enterPasswordSettings;
        [SerializeField]
        private MiniGameCollectApplesSettings _miniGameCollectApplesSettings;
        [SerializeField]
        private SoundsSettings _soundsSettings;

        [Header("Prefabs")]
        [SerializeField]
        private MiniGameSpamPopupsViewPopupEntry _miniGameSpamPopupsViewPopupEntry;

        public override void InstallBindings()
        {
            //Infrastructure
            Container.Bind<IScreenService>().To<ScreenService>().AsSingle().WithArguments(_viewManager);

            //Game
            Container.Bind(typeof(ISleepService), typeof(ITickable)).To<SleepService>().AsSingle();
            Container.Bind<IInventory>().To<Inventory>().AsSingle();
            Container.Bind<IGameTime>().To<GameTime>().AsSingle();
            Container.Bind<IStatusEffectService>().To<StatusEffectService>().AsSingle();
            Container.Bind(typeof(ITaskService), typeof(ITickable)).To<TaskService>().AsSingle();

            //Settings
            Container.Bind<TiredReasonSettings>().FromInstance(_tiredReasonSettings);
            Container.Bind<SleepSettings>().FromInstance(_sleepSettings);
            Container.Bind<EnterPasswordSettings>().FromInstance(_enterPasswordSettings);
            Container.Bind<StatusEffectSettings>().FromInstance(_statusEffectSettings);
            Container.Bind<MiniGameSpamPopupsSettings>().FromInstance(_miniGameSpamPopupsSettings);
            Container.Bind<MiniGameCollectApplesSettings>().FromInstance(_miniGameCollectApplesSettings);
            Container.Bind<SoundsSettings>().FromInstance(_soundsSettings);

            //Factoires
            Container.BindFactory<int, MiniGameSpamPopupsView, MiniGameSpamPopupsViewPopupEntry, MiniGameSpamPopupsViewPopupEntry.Factory>().FromComponentInNewPrefab(_miniGameSpamPopupsViewPopupEntry);
        }
    }
}
