using LudumDare50.Client.Data;
using LudumDare50.Client.Settings;
using UnityEngine;
using Zenject;

namespace LudumDare50.Client.Game.Implementation
{
    public class SleepService : ISleepService, ITickable
    {
        public Energy Energy { get; set; }

        private readonly SleepSettings _sleepSettings;

        private float _wakeTimer = 0;
        private bool _isAsleep;
        private bool _canSleep = true;
        private bool _isRunning = false;

        [Inject]
        public SleepService(SleepSettings sleepSettings)
        {
            _sleepSettings = sleepSettings;
        }

        public void Start()
        {
            if(_isRunning)
            {
                return;
            }

            _isRunning = true;
            _isAsleep = false;
            Energy = new Energy(_sleepSettings.StartingEnergy, _sleepSettings.StartingEnergy);
        }

        public void Stop()
        {
            _isRunning = false;
        }

        public bool TrySleep()
        {
            if(!_canSleep || _wakeTimer > 0)
            {
                return false;
            }

            _isAsleep = true;
            return true;
        }

        public void WakeUp()
        {
            //If were caught asleep, we want to stop the player from trying to sleep again for some time
            if (_isAsleep)
            {
                _wakeTimer = _sleepSettings.CaughtAsleepForceAwakeTimeAmount;
                Energy = new Energy(Energy.Max, Mathf.Max(Energy.Current - _sleepSettings.CaughtAsleepEnergyPenalty, 0));
            }
            _isAsleep = false;
            _canSleep = false;
        }

        public void Tick()
        {
            if(!_isRunning)
            {
                return;
            }

            if(_isAsleep)
            {
                Energy = new Energy(Energy.Max, Mathf.Min(Energy.Current + (Time.deltaTime * _sleepSettings.EnergySleepAdditionMultiplier), Energy.Max));
                _isAsleep = false;
            }
            else
            {
                Energy = new Energy(Energy.Max, Mathf.Max(Energy.Current - (Time.deltaTime * _sleepSettings.EnergyAwakeSubtractionMultiplier), 0));

                if (_wakeTimer > 0)
                {
                    _wakeTimer -= Time.deltaTime;
                }
            }

            if(!_canSleep)
            {
                _canSleep = true;
            }
        }
    }
}
