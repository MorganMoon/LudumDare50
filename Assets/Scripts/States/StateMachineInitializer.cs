using Cerberus;
using Zenject;

namespace LudumDare50.Client.States
{
    public class StateMachineInitializer : IInitializable
    {
        private readonly IStateMachine<GameState> _stateMachine;

        [Inject]
        public StateMachineInitializer(IStateMachine<GameState> stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public void Initialize()
        {
            _stateMachine.Start();
        }
    }
}