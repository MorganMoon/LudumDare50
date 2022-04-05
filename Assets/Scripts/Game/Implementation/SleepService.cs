using LudumDare50.Client.Data;
using LudumDare50.Client.Settings;
using System;
using UnityEngine;
using Zenject;

namespace LudumDare50.Client.Game.Implementation
{
    public class SleepService : ISleepService, ITickable
    {
        public Energy Energy { get; set; }

        public bool IsAsleep { get; private set; }

        public bool IsCaught => _wakeTimer > 0;

        private readonly SleepSettings _sleepSettings;
        private readonly IGameTime _gameTime;

        private float _wakeTimer = 0;
        private bool _canSleep = true;
        private bool _isRunning = false;

        [Inject]
        public SleepService(SleepSettings sleepSettings, IGameTime gameTime)
        {
            _sleepSettings = sleepSettings;
            _gameTime = gameTime;
        }

        public void Start()
        {
            if(_isRunning)
            {
                return;
            }

            _isRunning = true;
            IsAsleep = false;
            Energy = new Energy(_sleepSettings.StartingEnergy, _sleepSettings.StartingEnergy);
        }

        public void Stop()
        {
            _isRunning = false;
        }

        public bool TrySleep()
        {
            if(!_canSleep)
            {
                CaughtPenalty();
                return false;
            }

            if(_wakeTimer > 0)
            {
                return false;
            }

            IsAsleep = true;
            return true;
        }

        public void WakeUp()
        {
            //If were caught asleep, we want to stop the player from trying to sleep again for some time
            if (IsAsleep)
            {
                CaughtPenalty();
            }
            IsAsleep = false;
            _canSleep = false;
        }

        public void Tick()
        {
            if(!_isRunning)
            {
                return;
            }

            if(IsAsleep)
            {
                Energy = new Energy(Energy.Max, Mathf.Min(Energy.Current + (Time.deltaTime * _sleepSettings.EnergySleepAdditionMultiplier), Energy.Max));
                IsAsleep = false;
            }
            else
            {
                var timeSinceStart = DateTime.UtcNow - _gameTime.StartTime;
                var subtractionTimeIncrease = timeSinceStart.TotalSeconds * _sleepSettings.EnergyAwakeSubtractionTimeIncrease;
                Energy = new Energy(Energy.Max, Mathf.Max(Energy.Current - (Time.deltaTime * (_sleepSettings.EnergyAwakeSubtractionMultiplier + (float)subtractionTimeIncrease)), 0));

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

        private void CaughtPenalty()
        {
            _wakeTimer = _sleepSettings.CaughtAsleepForceAwakeTimeAmount;
            Energy = new Energy(Energy.Max, Mathf.Max(Energy.Current - _sleepSettings.CaughtAsleepEnergyPenalty, 0));
        }
    }
}
