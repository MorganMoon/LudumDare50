using Cerberus;
using Cerberus.Builder;
using Cerberus.IoC;
using LudumDare50.Client.States;
using LudumDare50.Client.States.Credits;
using LudumDare50.Client.States.Gameplay;
using LudumDare50.Client.States.MainMenu;
using LudumDare50.Client.States.Settings;
using LudumDare50.Client.States.Startup;
using System;
using Zenject;

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
                .State<StartupState, StartupStateEvent, StartupStateSubState>(GameState.Startup)
                    .AddEvent(StartupStateEvent.PlayGame, (stateEvent) => stateEvent.ChangeState(GameState.Gameplay))
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
                .State<GameplayState, GameplayStateEvent>(GameState.Gameplay)
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