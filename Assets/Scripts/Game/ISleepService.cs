using LudumDare50.Client.Data;

namespace LudumDare50.Client.Game
{
    public interface ISleepService
    {
        Energy Energy { get; }

        void Start();
        void Stop();

        bool TrySleep();
        void WakeUp();
    }
}