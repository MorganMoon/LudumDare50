using Cerberus;
using LudumDare50.Client.Data;
using LudumDare50.Client.Game;
using LudumDare50.Client.Infrastructure;
using LudumDare50.Client.ViewModels;
using Zenject;

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
        ClickABunchMiniGame,
        SpamPopupsMiniGame,
        CollectApplesMiniGame,
    }

    public class MiniGameState : State
    {
        private readonly ITaskService _taskService;
        private readonly IInventory _inventory;
        private readonly IScreenService _screenService;

        [Inject]
        public MiniGameState(ITaskService taskService, IInventory inventory, IScreenService screenService)
        {
            _taskService = taskService;
            _inventory = inventory;
            _screenService = screenService;
        }

        public override void OnEnter()
        {
            _screenService.AddToScreen<MiniGameViewModel>();
        }

        public override void OnExit()
        {
            _screenService.RemoveFromScreen<MiniGameViewModel>();
            _taskService.Reset();
        }

        public void OnSuccess()
        {
            var money = _inventory.GetItem(InventoryItemType.Money);
            _inventory.SetItem(new InventoryItem(InventoryItemType.Money, money.Count + _taskService.GetTask().Payout));
        }
    }
}
