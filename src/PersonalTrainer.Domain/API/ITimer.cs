using System;
using Figroll.PersonalTrainer.Domain.Timer;

namespace Figroll.PersonalTrainer.Domain.API
{
    public interface ITimer
    {
        void Wait(int seconds);
        void WaitMilliseconds(int milliseconds);

        void RunAsync(int seconds);

        event EventHandler<TimerEventArgs> TimerTicked;
        event EventHandler<EventArgs> TimerEnded;
    }
}