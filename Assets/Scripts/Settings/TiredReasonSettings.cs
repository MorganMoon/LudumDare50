using UnityEngine;

namespace LudumDare50.Client.Settings
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Settings/TiredReasons")]
    public class TiredReasonSettings : ScriptableObject
    {
        public string[] TiredReasons;
    }
}