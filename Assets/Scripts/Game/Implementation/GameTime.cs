using System;

namespace LudumDare50.Client.Game.Implementation
{
    public class GameTime : IGameTime
    {
        public DateTime StartTime { get; private set; }

        public DateTime EndTime { get; private set; }

        public void Start()
        {
            StartTime = DateTime.UtcNow;
            EndTime = default;
        }

        public void Stop()
        {
            EndTime = DateTime.UtcNow;
        }
    }
}