using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LudumDare50.Client.Settings
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Settings/SoundsSettings")]
    public class SoundsSettings : ScriptableObject
    {
        public AudioClip miniGameSuccess;
        [Range(0, 1)]
        public float miniGameSuccessVolume;
    }
}
