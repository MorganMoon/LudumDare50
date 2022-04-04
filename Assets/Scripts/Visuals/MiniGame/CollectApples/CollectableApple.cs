using Cerberus;
using LudumDare50.Client.States.MiniGame.CollectApples;
using UnityEngine;
using Zenject;

namespace LudumDare50.Client.Visuals.MiniGame.CollectApples
{
    public class CollectableApple : MonoBehaviour
    {
        [Inject]
        private IStateController<MiniGameCollectApplesStateEvent> _miniGameCollectApplesStateController;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.gameObject.CompareTag("Player"))
            {
                Destroy(gameObject);
                _miniGameCollectApplesStateController.TriggerEvent(MiniGameCollectApplesStateEvent.AppleCollected);
            }
        }
    }
}