namespace Figroll.PersonalTrainer.Domain.API
{
    public interface ITrainer : IVoice
    {
        void Say(string text);
        void Say(string text, int thenPause);
        void SayAsync(string text);
    }
}