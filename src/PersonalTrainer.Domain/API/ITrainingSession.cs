using System;
using ScriptCs.Contracts;

namespace Figroll.PersonalTrainer.Domain.API
{
    /// <summary>
    /// Main entry point for the Personal Trainer API.
    /// </summary>
    public interface ITrainingSession: IScriptPackContext, IDisposable
    {
        IUserSettings Settings { get; }
        ITrainer Trainer { get; }
        IRandomNumberGenerator RNG { get; }
        ITimer Timer { get; }
        IMetronome Metronome { get; }
        ISequencer Sequencer { get; }
        IContentCollection Content { get; }
        IContentViewer Viewer { get; }
    }
}