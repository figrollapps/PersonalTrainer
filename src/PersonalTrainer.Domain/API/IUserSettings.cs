namespace Figroll.PersonalTrainer.Domain.API
{
    /// <summary>
    /// User settings loaded from settings.txt at startup.
    /// </summary>
    public interface IUserSettings
    {
        /// <summary>
        /// If true PERSONAL TRAINER will attempt to use TTS when speaking.  If false, speech will be subtitles only.
        /// </summary>
        bool UseTTS { get; }

        /// <summary>
        /// The voice to use for TTS when speaking. If blank then a random, adult female voice will be used.
        /// Supports imprecise matching so, for example, "Amy" will match "PurchasedVoice Amy UK English".
        /// </summary>
        string DefaultVoice { get; }

        /// <summary>
        /// The default location from which pictures will be loaded into the ContentCollection.
        /// </summary>
        string ContentLocation { get; }
    }
}