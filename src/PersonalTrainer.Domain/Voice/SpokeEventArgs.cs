using System;

namespace Figroll.PersonalTrainer.Domain.Voice
{
    public class SpokeEventArgs : EventArgs
    {
        public SpokeEventArgs(string words)
        {
            Words = words;
        }

        public string Words { get; }
    }
}