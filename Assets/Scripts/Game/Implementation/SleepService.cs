using LudumDare50.Client.Data;
using UnityEngine;
using Zenject;

namespace LudumDare50.Client.Game.Implementation
{
    public class SleepService : ISleepService, ITickable
    {
        public Energy Energy { get; private set; }

        private float _wakeTimer = 0;
        private bool _isAsleep;
        private bool _isRunning = false;

        public void Start()
        {
            if(_isRunning)
            {
                return;
            }

            _isRunning = true;
            Energy = new Energy(100, 100);
        }

        public void Stop()
        {
            _isRunning = false;
        }

        public bool TrySleep()
        {
            if(_wakeTimer > 0)
            {
                return false;
            }

            _isAsleep = true;
            return true;
        }

        public void WakeUp()
        {
            _isAsleep = false;
            _wakeTimer = 3;
        }

        public void Tick()
        {
            if(!_isRunning)
            {
                return;
            }

            if(_isAsleep)
            {
                Energy = new Energy(Energy.Max, Energy.Current + (Time.deltaTime / 2f));
            }
            else
            {
                Energy = new Energy(Energy.Max, Energy.Current - Time.deltaTime);

                if (_wakeTimer > 0)
                {
                    _wakeTimer -= Time.deltaTime;
                }
            }
        }
    }
}
