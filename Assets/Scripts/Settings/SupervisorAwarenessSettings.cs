using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LudumDare50.Client.Settings
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Settings/SupervisorAwarenessSettings")]
    public class SupervisorAwarenessSettings : ScriptableObject
    {
        public int MaxFailures;
        public int NumberOfTasksToRecover;
    }
}
