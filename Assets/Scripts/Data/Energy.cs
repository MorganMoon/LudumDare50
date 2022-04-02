namespace LudumDare50.Client.Data
{
    public struct Energy
    {
        public float Max { get; }
        public float Current { get; }

        public Energy(float max, float current)
        {
            Max = max;
            Current = current;
        }
    }
}
