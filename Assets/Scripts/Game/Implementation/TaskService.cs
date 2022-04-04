using System;
using LudumDare50.Client.Data;
using Zenject;

namespace LudumDare50.Client.Game.Implementation
{
    public class TaskService : ITaskService, ITickable
    {
        public event Action OnTaskAvailabilityChanged;

        private GameTask _currentTask;
        private bool _isAccepted;

        public void AcceptTask()
        {
            if (_currentTask.AvailableUntil > DateTime.UtcNow)
            {
                _isAccepted = true;
                _currentTask = new GameTask(_currentTask.AvailableAt, _currentTask.AvailableUntil, _currentTask.Payout, true);
                OnTaskAvailabilityChanged?.Invoke();
            }
        }

        public GameTask GetTask()
        {
            return _currentTask;
        }

        public void Reset()
        {
            _isAccepted = false;
            SetNewTask();
        }

        public void Tick()
        {
            if(!_isAccepted && _currentTask.AvailableUntil <= DateTime.UtcNow)
            {
                SetNewTask();
            }
        }

        private void SetNewTask()
        {
            var startTime = DateTime.UtcNow.AddSeconds(UnityEngine.Random.Range(2f, 8f));
            _currentTask =  new GameTask(startTime, startTime.AddSeconds(UnityEngine.Random.Range(5f, 10f)), 1, false);
            OnTaskAvailabilityChanged?.Invoke();
        }
    }
}