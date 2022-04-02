using System;

namespace LudumDare50.Client.Data
{
    public struct GameTask
    {
        public DateTime AvailableAt { get; }
        public DateTime AvailableUntil { get; }
        public int Payout { get; }
        public bool Accepted { get; }

        public GameTask(DateTime availableAt, DateTime availableUntil, int payout, bool accepted)
        {
            AvailableAt = availableAt;
            AvailableUntil = availableUntil;
            Payout = payout;
            Accepted = accepted;
        }
    }
}