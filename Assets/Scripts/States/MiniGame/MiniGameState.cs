using Cerberus;
using LudumDare50.Client.Data;
using LudumDare50.Client.Game;

namespace LudumDare50.Client.States.MiniGame
{
    public enum MiniGameStateEvent
    {
        Success,
        Failure
    }

    public enum MiniGameStateSubState
    {
        Initialize,
        ClickABunchMiniGame
    }

    public class MiniGameState : State
    {
        private readonly ITaskService _taskService;
        private readonly IInventory _inventory;

        public MiniGameState(ITaskService taskService, IInventory inventory)
        {
            _taskService = taskService;
            _inventory = inventory;
        }

        public override void OnExit()
        {
            _taskService.Reset();
        }

        public void OnSuccess()
        {
            var money = _inventory.GetItem(InventoryItemType.Money);
            _inventory.SetItem(new InventoryItem(InventoryItemType.Money, money.Count + _taskService.GetTask().Payout));
        }
    }
}
