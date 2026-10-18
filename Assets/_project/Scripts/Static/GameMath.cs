using Clicker.Business.Component;
using Clicker.Business.Config;

namespace Clicker.Common
{
    public static class GameMath
    {
        public static int CalculateIncome(BusinessComponent data, BusinessConfig config)
            => CalculateIncome(
                data.Level, 
                config.BaseIncome, 
                data.Upgraded1 ? config.Grades1.Multiplier : 0, 
                data.Upgraded2 ? config.Grades2.Multiplier : 0);
        
        public static int CalculateLevelCost(BusinessComponent data, BusinessConfig config)
            => CalculateLevelCost(data.Level, config.BaseCost);
        
        public static int CalculateIncome(int level, int baseIncome, int modification1, int modification2)
            => level * baseIncome * (1 +  modification1 +   modification2);
        public static int CalculateLevelCost(int level, int baseCost)
            => (level + 1) * baseCost;
    }
}