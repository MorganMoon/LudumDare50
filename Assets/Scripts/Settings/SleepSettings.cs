using UnityEngine;

namespace LudumDare50.Client.Settings
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Settings/SleepSettings")]
    public class SleepSettings : ScriptableObject
    {
        public float StartingEnergy;
        public float EnergySleepAdditionMultiplier;
        public float EnergyAwakeSubtractionMultiplier;
        public float CaughtAsleepForceAwakeTimeAmount;
        public float CaughtAsleepEnergyPenalty;
    }
}