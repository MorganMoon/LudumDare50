using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LudumDare50.Client.Settings
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Settings/EnterPasswordSettings")]
    public class EnterPasswordSettings : ScriptableObject
    {
        public string[] passwords;
    }
}
