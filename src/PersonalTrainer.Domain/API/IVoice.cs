using System;
using Figroll.PersonalTrainer.Domain.Voice;

namespace Figroll.PersonalTrainer.Domain.API
{
    public interface IVoice
    {
        void UseDefaultVoice();
        void UseVoice(string voiceHint);
        void VoiceOff();

        event EventHandler<SpokeEventArgs> Spoke;
    }
}