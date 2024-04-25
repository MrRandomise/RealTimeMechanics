using System;

namespace Game.TimeReward
{
    public interface IRealtimeTimer
    {
        event Action<string> TimerStarted;
        void SynchronizeTime(float time);
        string SaveKey { get; }
    }
}