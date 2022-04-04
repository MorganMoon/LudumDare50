using LudumDare50.Client.Settings;
using UnityEngine;
using Zenject;

namespace LudumDare50.Client.Visuals.MiniGame.CollectApples
{
    public class AppleCreator : MonoBehaviour
    {
        [Inject]
        private MiniGameCollectApplesSettings _miniGameCollectApplesSettings;
        [Inject]
        private IInstantiator _instantiator;
        [SerializeField]
        private GameObject _applePrefab;

        private void Start()
        {
            for(int i =0; i < _miniGameCollectApplesSettings.AppleAmount; i++)
            {
                var apple = _instantiator.InstantiatePrefab(_applePrefab);
                apple.transform.SetParent(transform);
                apple.transform.localPosition = new Vector3(Random.Range(-4.5f, 4.5f), Random.Range(-1.55f, 3.6f), apple.transform.position.z);
            }
        }
    }
}