using System;

namespace Figroll.PersonalTrainer.Domain.Timer
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class TimerEventArgs : EventArgs
    {
        public TimerEventArgs(TimeSpan time, TimeSpan timeRemaining)
        {
            Time = time;
            TimeRemaining = timeRemaining;
        }

        // ReSharper disable UnusedAutoPropertyAccessor.Local
        private TimeSpan Time { get; }
        private TimeSpan TimeRemaining { get; }
        // ReSharper enable UnusedAutoPropertyAccessor.Local
    }
}