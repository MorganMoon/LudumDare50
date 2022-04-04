using System;
using LudumDare50.Client.Data;

namespace LudumDare50.Client.Game
{
    public interface ITaskService
    {
        event Action OnTaskAvailabilityChanged;

        SupervisorAwareness SupervisorAwareness { get; set; }

        void Start();

        GameTask GetTask();
        void AcceptTask();
        void Reset();

        void TaskSuccess();
        void TaskFailure();
    }
}