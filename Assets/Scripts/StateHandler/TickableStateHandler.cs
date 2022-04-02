using Cerberus;
using Zenject;

namespace LudumDare50.Client.StateHandler
{
    public class TickableStateHandler : IStateHandler<ITickable>
    {
        private readonly TickableManager _tickableManager;

        [Inject]
        public TickableStateHandler(TickableManager tickableManager)
        {
            _tickableManager = tickableManager;
        }

        public void OnEnterState(ITickable stateInstance)
        {
            _tickableManager.Add(stateInstance);
        }

        public void OnExitState(ITickable stateInstance)
        {
            _tickableManager.Remove(stateInstance);
        }
    }
}
