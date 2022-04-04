using UnityEngine;

namespace LudumDare50.Client.Settings
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Settings/MiniGameSpamPopupsSettings")]
    public class MiniGameSpamPopupsSettings : ScriptableObject
    {
        public int AmountOfPopups;
    }
}