namespace Figroll.PersonalTrainer.Domain.API
{
    public interface ISequencer: IMetronome
    {
        void SetPattern(string pattern);
    }
}