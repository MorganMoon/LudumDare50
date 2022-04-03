using System;
using LudumDare50.Client.Data;

namespace LudumDare50.Client.Game
{
    public interface ITaskService
    {
        event Action OnTaskAvailabilityChanged;

        GameTask GetTask();
        void AcceptTask();
        void Reset();
    }
}