using Cerberus;
using LudumDare50.Client.Infrastructure;
using LudumDare50.Client.ViewModels.CollectApples;
using UnityEngine.SceneManagement;
using Zenject;

namespace LudumDare50.Client.States.MiniGame.CollectApples
{
    public enum MiniGameCollectApplesStateEvent
    {
        AppleCollected
    }

    public class MiniGameCollectApplesState : State
    {
        private readonly IScreenService _screenService;
        private readonly IStateController<MiniGameStateEvent> _miniGameStateController;
        private readonly ZenjectSceneLoader _zenjectSceneLoader;

        private int _collectedApples = 0;

        [Inject]
        public MiniGameCollectApplesState(IScreenService screenService, IStateController<MiniGameStateEvent> miniGameStateController, ZenjectSceneLoader zenjectSceneLoader)
        {
            _screenService = screenService;
            _miniGameStateController = miniGameStateController;
            _zenjectSceneLoader = zenjectSceneLoader;
        }

        public override void OnEnter()
        {
            _zenjectSceneLoader.LoadScene("MiniGameCollectApples", LoadSceneMode.Additive);
            _screenService.AddToScreen<MiniGameCollectApplesViewModel>();
        }

        public override void OnExit()
        {
            SceneManager.UnloadSceneAsync("MiniGameCollectApples");
            _screenService.RemoveFromScreen<MiniGameCollectApplesViewModel>();
        }

        public void OnAppleCollected()
        {
            _collectedApples++;
            if(_collectedApples >= 5)
            {
                _miniGameStateController.TriggerEvent(MiniGameStateEvent.Success);
            }
        }
    }
}