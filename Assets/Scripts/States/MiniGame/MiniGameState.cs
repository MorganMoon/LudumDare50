using Cerberus;
using LudumDare50.Client.Data;
using LudumDare50.Client.Game;
using LudumDare50.Client.Infrastructure;
using LudumDare50.Client.Settings;
using LudumDare50.Client.ViewModels.MiniGame;
using UnityEngine;
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
        EnterPasswordMiniGame,
        CollectApplesMiniGame,
        SelectWifiMiniGame,
        CaptchaMiniGame,
    }

    public class MiniGameState : State
    {
        private readonly ITaskService _taskService;
        private readonly IInventory _inventory;
        private readonly IScreenService _screenService;
        private readonly SoundsSettings _soundsSettings;

        [Inject]
        public MiniGameState(ITaskService taskService, IInventory inventory, IScreenService screenService, SoundsSettings soundsSettings)
        {
            _taskService = taskService;
            _inventory = inventory;
            _screenService = screenService;
            _soundsSettings = soundsSettings;
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
            AudioSource.PlayClipAtPoint(_soundsSettings.miniGameSuccess, Vector3.zero, _soundsSettings.miniGameSuccessVolume);
            var money = _inventory.GetItem(InventoryItemType.Money);
            _inventory.SetItem(new InventoryItem(InventoryItemType.Money, money.Count + _taskService.GetTask().Payout));
            _taskService.TaskSuccess();
        }

        public void OnFailure()
        {
            _taskService.TaskFailure();
        }
    }
}
