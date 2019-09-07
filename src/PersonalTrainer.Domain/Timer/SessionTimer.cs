using System;
using System.Threading;
using Figroll.PersonalTrainer.Domain.API;
using Figroll.PersonalTrainer.Domain.Utilities;

namespace Figroll.PersonalTrainer.Domain.Timer
{
    // ReSharper disable once ClassWithVirtualMembersNeverInherited.Global
    public class SessionTimer : ITimer
    {
        private static System.Threading.Timer timer;

        public void WaitMilliseconds(int milliseconds)
        {
            Thread.Sleep(milliseconds);
        }

        public void Wait(int seconds)
        {
            Thread.Sleep(seconds * 1000);
        }

        public void RunAsync(int seconds)
        {
            if (timer != null)
            {
                return;
            }

            timer = new System.Threading.Timer(OnComplete, null, seconds.ToMilliseconds(), Timeout.Infinite);
        }

        public event EventHandler<TimerEventArgs> TimerTicked;

        public event EventHandler<EventArgs> TimerEnded;

        private void OnComplete(object state)
        {
            if (timer == null)
            {
                return;
            }

            timer.Change(Timeout.Infinite, Timeout.Infinite);
            timer.Dispose();
            timer = null;

            OnTimerEnded();
        }

        protected virtual void OnTimerTicked(TimerEventArgs e)
        {
            TimerTicked?.Invoke(this, e);
        }

        protected virtual void OnTimerEnded()
        {
            TimerEnded?.Invoke(this, EventArgs.Empty);
        }
    }
}