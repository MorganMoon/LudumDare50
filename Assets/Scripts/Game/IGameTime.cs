using System;

namespace LudumDare50.Client.Game
{
    public interface IGameTime
    {
        DateTime StartTime { get; }
        DateTime EndTime { get; }

        public void Start();
        public void Stop();
    }
}