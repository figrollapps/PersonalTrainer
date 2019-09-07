using Figroll.PersonalTrainer.Domain.API;

namespace Figroll.PersonalTrainer.Domain
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class UserSettings : IUserSettings
    {
        public bool UseTTS { get; set; }
        public string DefaultVoice { get; set; }
        public string ContentLocation { get; set; }
    }
}