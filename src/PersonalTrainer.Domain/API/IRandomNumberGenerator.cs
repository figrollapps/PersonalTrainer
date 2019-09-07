namespace Figroll.PersonalTrainer.Domain.API
{
    public interface IRandomNumberGenerator
    {
        bool CoinFlipIsHeads();
        bool CoinFlipIsTails();
        bool IsPercentageChance(int percentageChance);
        int Between(int min, int max);
        int Between(int min, int max, int step);
    }
}