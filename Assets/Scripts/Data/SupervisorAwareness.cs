namespace LudumDare50.Client.Data
{
    public struct SupervisorAwareness
    {
        public int Max { get; }
        public int Current { get; }

        public SupervisorAwareness(int max, int current)
        {
            Max = max;
            Current = current;
        }
    }
}
