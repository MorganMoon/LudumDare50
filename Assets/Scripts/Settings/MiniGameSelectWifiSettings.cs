using UnityEngine;

namespace LudumDare50.Client.Settings
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Settings/MiniGameSelectWifiSettings")]
    public class MiniGameSelectWifiSettings : ScriptableObject
    {
        public string[] WifiNames;
    }
}