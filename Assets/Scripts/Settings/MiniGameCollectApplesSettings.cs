using UnityEngine;

namespace LudumDare50.Client.Settings
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Settings/MiniGameCollectApplesSettings")]
    public class MiniGameCollectApplesSettings : ScriptableObject
    {
        public int AppleAmount;
        public float PlayerSpeed;
    }
}