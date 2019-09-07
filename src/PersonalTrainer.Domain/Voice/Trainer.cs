using System.Threading;
using Figroll.PersonalTrainer.Domain.API;
using Figroll.PersonalTrainer.Domain.Utilities;

namespace Figroll.PersonalTrainer.Domain.Voice
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public sealed class Trainer : Voice, ITrainer
    {
        public void Say(string text)
        {
            Speak(text);
        }

        public void Say(string text, int thenPause)
        {
            Say(text);
            Thread.Sleep(thenPause.ToMilliseconds());
        }

        public void SayAsync(string text)
        {
            SpeakAsync(text);
        }
    }
}