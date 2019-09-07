using System;

namespace Figroll.PersonalTrainer.Domain.Beats
{
    public class MetronomeEventArgs : EventArgs
    {
        public MetronomeEventArgs(int ticksDone, int ticksRemaining)
        {
            TicksDone = ticksDone;
            TicksRemaining = ticksRemaining;
        }

        public int TicksDone { get; }
        public int TicksRemaining { get; }
    }
}