using System;
using LudumDare50.Client.Data;
using LudumDare50.Client.Settings;
using Zenject;

namespace LudumDare50.Client.Game.Implementation
{
    public class TaskService : ITaskService, ITickable
    {
        public event Action OnTaskAvailabilityChanged;

        public SupervisorAwareness SupervisorAwareness { get; set; }

        private readonly SupervisorAwarenessSettings _supervisorAwarenessSettings;

        private GameTask _currentTask;
        private bool _isAccepted;

        private int _succeededTaskCount = 0;

        [Inject]
        public TaskService(SupervisorAwarenessSettings supervisorAwarenessSettings)
        {
            _supervisorAwarenessSettings = supervisorAwarenessSettings;
        }

        public void Start()
        {
            SupervisorAwareness = new SupervisorAwareness(_supervisorAwarenessSettings.MaxFailures, 0);
            Reset();
        }

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
                if(!_currentTask.Equals(default(GameTask)))
                {
                    SupervisorAwareness = new SupervisorAwareness(SupervisorAwareness.Max, SupervisorAwareness.Current + 1);
                }
                SetNewTask();
            }
        }

        public void TaskSuccess()
        {
            if(SupervisorAwareness.Current > 0)
            {
                _succeededTaskCount++;
                if(_succeededTaskCount >= _supervisorAwarenessSettings.NumberOfTasksToRecover)
                {
                    SupervisorAwareness = new SupervisorAwareness(SupervisorAwareness.Max, SupervisorAwareness.Current - 1);
                }
            }
        }

        public void TaskFailure()
        {
            SupervisorAwareness = new SupervisorAwareness(SupervisorAwareness.Max, SupervisorAwareness.Current + 1);
        }

        private void SetNewTask()
        {
            var startTime = DateTime.UtcNow.AddSeconds(UnityEngine.Random.Range(2f, 8f));
            _currentTask =  new GameTask(startTime, startTime.AddSeconds(UnityEngine.Random.Range(5f, 10f)), 1, false);
            OnTaskAvailabilityChanged?.Invoke();
        }
    }
}