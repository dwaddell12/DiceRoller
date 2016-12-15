using DiceRoller.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DiceRoller
{

    public static class RollHelper
    {
        public static List<RollResult> RollCollectedDice(List<BaseDie> dice)
        {
            List<RollResult> results = new List<RollResult>();
            foreach (BaseDie die in dice)
            {
                RollResult result = die.RollDie;
                results.Add(result);
            }
            return results;
        }

        public static int CumulativeRollValue(List<RollResult> results)
        {
            int total = 0;
            foreach (RollResult result in results)
            {
                var val = int.Parse(result.Side.Name);
                total += val;
            }
            return total;
        }
    }
}