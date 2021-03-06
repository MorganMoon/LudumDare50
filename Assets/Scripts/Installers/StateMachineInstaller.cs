using Cerberus;
using Cerberus.Builder;
using Cerberus.IoC;
using LudumDare50.Client.StateHandler;
using LudumDare50.Client.States;
using LudumDare50.Client.States.Credits;
using LudumDare50.Client.States.GameIntroduction;
using LudumDare50.Client.States.GameOver;
using LudumDare50.Client.States.Gameplay;
using LudumDare50.Client.States.MainMenu;
using LudumDare50.Client.States.MiniGame;
using LudumDare50.Client.States.MiniGame.ClickABunch;
using LudumDare50.Client.States.MiniGame.CollectApples;
using LudumDare50.Client.States.MiniGame.Initialize;
using LudumDare50.Client.States.MiniGame.SpamPopups;
using LudumDare50.Client.States.MiniGame.EnterPassword;
using LudumDare50.Client.States.OfficeState;
using LudumDare50.Client.States.Settings;
using LudumDare50.Client.States.Startup;
using System;
using Zenject;
using LudumDare50.Client.States.MiniGame.SelectWifi;
using LudumDare50.Client.States.MiniGame.Captcha;

namespace LudumDare50.Client.Installers
{
    public class StateMachineInstaller : MonoInstaller<StateMachineInstaller>
    {
        public override void InstallBindings()
        {
            var stateMachine = CreateStateMachine();
            Container.Bind<IStateMachine<GameState>>().FromInstance(stateMachine);
            foreach (var binding in stateMachine.StateControllerProvider.StateControllers)
            {
                Container.Bind(binding.ContractTypes).FromInstance(binding.Instance);
            }

            Container.BindInitializableExecutionOrder<StateMachineInitializer>(int.MinValue);
            Container.Bind<IInitializable>().To<StateMachineInitializer>().AsSingle();
        }

        private IStateMachine<GameState> CreateStateMachine()
        {
            return new StateMachineBuilder<GameState>(new ZenjectStateMachineContainer(Container))
                .AddStateHandler<ITickable, TickableStateHandler>()
                .State<StartupState, StartupStateEvent, StartupStateSubState>(GameState.Startup)
                    .AddEvent(StartupStateEvent.PlayGame, (stateEvent) => stateEvent.ChangeState(GameState.GameIntroduction))
                    .State<MainMenuState, MainMenuStateEvent>(StartupStateSubState.MainMenu)
                        .AddEvent(MainMenuStateEvent.Settings, (stateEvent) => stateEvent.ChangeState(StartupStateSubState.SettingsMenu))
                        .AddEvent(MainMenuStateEvent.Credits, (stateEvent) => stateEvent.ChangeState(StartupStateSubState.CreditsMenu))
                    .End()
                    .State<SettingsState, SettingsStateEvent>(StartupStateSubState.SettingsMenu)
                        .AddEvent(SettingsStateEvent.GoBack, (stateEvent) => stateEvent.ChangeState(StartupStateSubState.MainMenu))
                    .End()
                    .State<CreditsState, CreditsStateEvent>(StartupStateSubState.CreditsMenu)
                        .AddEvent(CreditsStateEvent.GoBack, (stateEvent) => stateEvent.ChangeState(StartupStateSubState.MainMenu))
                    .End()
                .End()
                .State<GameIntroductionState, GameIntroductionStateEvent>(GameState.GameIntroduction)
                    .AddEvent(GameIntroductionStateEvent.Continue, (stateEvent) => stateEvent.ChangeState(GameState.Gameplay))
                .End()
                .State<GameplayState, GameplayStateEvent, GameplayStateSubState>(GameState.Gameplay)
                    .AddEvent(GameplayStateEvent.GameOver, (stateEvent) => stateEvent.ChangeState(GameState.GameOver))
                    .State<OfficeState, OfficeStateEvent>(GameplayStateSubState.Office)
                        .AddEvent(OfficeStateEvent.StartMiniGame, (stateEvent) => stateEvent.ChangeState(GameplayStateSubState.MiniGame))
                    .End()
                    .State<MiniGameState, MiniGameStateEvent, MiniGameStateSubState>(GameplayStateSubState.MiniGame)
                        .AddEvent(MiniGameStateEvent.Success, (stateEvent) =>
                        {
                            stateEvent.StateInstance.OnSuccess();
                            stateEvent.ChangeState(GameplayStateSubState.Office);
                        })
                        .AddEvent(MiniGameStateEvent.Failure, (stateEvent) =>
                        {
                            stateEvent.StateInstance.OnFailure();
                            stateEvent.ChangeState(GameplayStateSubState.Office);
                        })
                        .State<MiniGameInitializeState, MiniGameInitializeStateEvent>(MiniGameStateSubState.Initialize)
                            .AddEvent(MiniGameInitializeStateEvent.PlayClickABunch, (stateEvent) => stateEvent.ChangeState(MiniGameStateSubState.ClickABunchMiniGame))
                            .AddEvent(MiniGameInitializeStateEvent.PlayEnterPassword, (stateEvent) => stateEvent.ChangeState(MiniGameStateSubState.EnterPasswordMiniGame))
                            .AddEvent(MiniGameInitializeStateEvent.PlaySpamPopups, (stateEvent) => stateEvent.ChangeState(MiniGameStateSubState.SpamPopupsMiniGame))
                            .AddEvent(MiniGameInitializeStateEvent.PlayCollectApples, (stateEvent) => stateEvent.ChangeState(MiniGameStateSubState.CollectApplesMiniGame))
                            .AddEvent(MiniGameInitializeStateEvent.PlaySelectWifi, (stateEvent) => stateEvent.ChangeState(MiniGameStateSubState.SelectWifiMiniGame))
                            .AddEvent(MiniGameInitializeStateEvent.PlayCaptcha, (stateEvent) => stateEvent.ChangeState(MiniGameStateSubState.CaptchaMiniGame))
                        .End()
                        .State<MiniGameClickABunchState, MiniGameClickABunchStateEvent>(MiniGameStateSubState.ClickABunchMiniGame)
                        .End()
                        .State<MiniGameSpamPopupsState, MiniGameSpamPopupsStateEvent>(MiniGameStateSubState.SpamPopupsMiniGame)
                        .End()
                        .State<MiniGameCollectApplesState, MiniGameCollectApplesStateEvent>(MiniGameStateSubState.CollectApplesMiniGame)
                            .AddEvent(MiniGameCollectApplesStateEvent.AppleCollected, (stateEvent) => stateEvent.StateInstance.OnAppleCollected())
                        .End()
                        .State<MiniGameEnterPasswordState, MiniGameEnterPasswordStateEvent>(MiniGameStateSubState.EnterPasswordMiniGame)
                        .End()
                        .State<MiniGameSelectWifiState, MiniGameSelectWifiStateEvent>(MiniGameStateSubState.SelectWifiMiniGame)
                        .End()
                        .State<MiniGameCaptchaState, MiniGameCaptchaStateEvent>(MiniGameStateSubState.CaptchaMiniGame)
                        .End()
                    .End()
                .End()
                .State<GameOverState, GameOverStateEvent>(GameState.GameOver)
                    .AddEvent(GameOverStateEvent.Continue, (stateEvent) => stateEvent.ChangeState(GameState.Startup))
                    .AddEvent(GameOverStateEvent.PlayAgain, (stateEvent) => stateEvent.ChangeState(GameState.GameIntroduction))
                .End()
                .Build();
        }

        private class ZenjectStateMachineContainer : IStateMachineContainer
        {
            private readonly DiContainer _diContainer;

            public ZenjectStateMachineContainer(DiContainer diContainer)
            {
                _diContainer = diContainer;
            }

            public T Resolve<T>()
            {
                if (TryResolve<T>(out var result))
                {
                    return result;
                }
                else
                {
                    return _diContainer.Instantiate<T>();
                }
            }

            public object Resolve(Type type)
            {
                if (TryResolve(type, out var result))
                {
                    return result;
                }
                else
                {
                    return _diContainer.Instantiate(type);
                }
            }

            private bool TryResolve<T>(out T result)
            {
                try
                {
                    result = _diContainer.Resolve<T>();
                    return true;
                }
                catch
                {
                    result = default;
                    return false;
                }
            }

            private bool TryResolve(Type contract, out object result)
            {
                try
                {
                    result = _diContainer.Resolve(contract);
                    return true;
                }
                catch
                {
                    result = default;
                    return false;
                }
            }
        }
    }
}